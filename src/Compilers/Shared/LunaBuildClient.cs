// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.CommandLine;
using Microsoft.CodeAnalysis;
using System.Diagnostics;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.CommandLine;

internal delegate Task<BuildResponse> CompileOnServerFunc(BuildRequest buildRequest, string pipeName, CancellationToken cancellationToken);

/// <summary>
/// 生成客户端类，处理与服务端的通信。
/// </summary>
internal sealed class BuildClient
{
    private readonly RequestLanguage _language;
    private readonly CompileFunc _compileFunc;
    private readonly CompileOnServerFunc _compileOnServerFunc;

    /// <summary>
    /// When set it overrides all timeout values in milliseconds when communicating with the server.
    /// </summary>
    internal BuildClient(RequestLanguage language, CompileFunc compileFunc, CompileOnServerFunc compileOnServerFunc)
    {
        this._language = language;
        this._compileFunc = compileFunc;
        this._compileOnServerFunc = compileOnServerFunc;
    }

    /// <summary>
    /// Get the directory which contains the luac, moonc and LunaCompiler clients. 
    /// 
    /// Historically this is referred to as the "client" directory but maybe better if it was 
    /// called the "installation" directory.
    /// 
    /// It is important that this method exist here and not on <see cref="BuildServerConnection"/>. This
    /// can only reliably be called from our executable projects and this file is only linked into 
    /// those projects while <see cref="BuildServerConnection"/> is also included in the MSBuild 
    /// task.
    /// </summary>
    public static string GetClientDirectory() =>
        // LunaCompiler is installed in the same directory as luac.exe and moonc.exe which is also the 
        // location of the response files.
        AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    /// 获取系统SDK目录。
    /// </summary>
    /// <returns>
    /// <c>mscorlib</c>程序集所在的目录。若程序在.NET Core运行时（CoreCLR）上运行，则返回<see langword="null"/>。
    /// </returns>
    public static string? GetSystemSdkDirectory() => Microsoft.CodeAnalysis.CommandLine.BuildClient.GetSystemSdkDirectory();

    internal static int Run(IEnumerable<string> arguments, RequestLanguage language, CompileFunc compileFunc, CompileOnServerFunc compileOnServerFunc)
    {
        var sdkDir = BuildClient.GetSystemSdkDirectory();
        if (RuntimeHostInfo.IsCoreClrRuntime)
        {
            // 给控制台注册编码。
            // https://github.com/dotnet/roslyn/issues/10785
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }

        var client = new BuildClient(language, compileFunc, compileOnServerFunc);
        var clientDir = BuildClient.GetClientDirectory();
        var workingDir = Directory.GetCurrentDirectory();
        var tempDir = BuildServerConnection.GetTempPath(workingDir);
        var buildPaths = new BuildPaths(clientDir: clientDir, workingDir: workingDir, sdkDir: sdkDir, tempDir: tempDir);
        var originalArguments = GetCommandLineArgs(arguments);
        return client.RunCompilation(originalArguments, buildPaths).ExitCode;
    }

    /// <summary>
    /// Run a compilation through the compiler server and print the output
    /// to the console. If the compiler server fails, run the fallback
    /// compiler.
    /// </summary>
    internal RunCompilationResult RunCompilation(IEnumerable<string> originalArguments, BuildPaths buildPaths, TextWriter? textWriter = null, string? pipeName = null)
    {
        textWriter ??= Console.Out;

        var args = originalArguments.Select(arg => arg.Trim()).ToArray();

        if (CommandLineParser.TryParseClientArgs(
            args,
            out var parsedArgs,
            out var hasShared,
            out var keepAlive,
            out var commandLinePipeName,
            out var errorMessageOpt))
        {
            Debug.Assert(parsedArgs is not null);
            pipeName ??= commandLinePipeName;
        }
        else
        {
            textWriter.WriteLine(errorMessageOpt);
            return RunCompilationResult.Failed;
        }

        if (hasShared)
        {
            pipeName ??= BuildServerConnection.GetPipeName(buildPaths.ClientDirectory);
            var libDirectory = Environment.GetEnvironmentVariable("LIB");
            var serverResult = this.RunServerCompilation(textWriter, parsedArgs!, buildPaths, libDirectory, pipeName, keepAlive);
            if (serverResult.HasValue)
            {
                Debug.Assert(serverResult.Value.RanOnServer);
                return serverResult.Value;
            }
        }

        // It's okay, and expected, for the server compilation to fail.  In that case just fall 
        // back to normal compilation. 
        var exitCode = this.RunLocalCompilation(parsedArgs!.ToArray(), buildPaths, textWriter);
        return new RunCompilationResult(exitCode);
    }

    public Task<RunCompilationResult> RunCompilationAsync(IEnumerable<string> originalArguments, BuildPaths buildPaths, TextWriter? textWriter = null)
    {
        var tcs = new TaskCompletionSource<RunCompilationResult>();
        void action()
        {
            try
            {
                var result = this.RunCompilation(originalArguments, buildPaths, textWriter);
                tcs.SetResult(result);
            }
            catch (Exception ex)
            {
                tcs.SetException(ex);
            }
        }

        var thread = new Thread(action);
        thread.Start();

        return tcs.Task;
    }

    private int RunLocalCompilation(string[] arguments, BuildPaths buildPaths, TextWriter textWriter)
    {
        var loader = new DefaultAnalyzerAssemblyLoader();
        return this._compileFunc(arguments, buildPaths, textWriter, loader);
    }

    public static CompileOnServerFunc GetCompileOnServerFunc(ICompilerServerLogger logger) => (buildRequest, pipeName, cancellationToken) =>
        BuildServerConnection.RunServerBuildRequestAsync(
            buildRequest,
            pipeName,
            GetClientDirectory(),
            logger,
            cancellationToken);

    /// <summary>
    /// Runs the provided compilation on the server.  If the compilation cannot be completed on the server then null
    /// will be returned.
    /// </summary>
    private RunCompilationResult? RunServerCompilation(TextWriter textWriter, List<string> arguments, BuildPaths buildPaths, string? libDirectory, string pipeName, string? keepAlive)
    {
        if (!BuildClient.AreNamedPipesSupported()) return null;

        BuildResponse buildResponse;
        try
        {
            var requestId = Guid.NewGuid();
            var buildRequest = BuildServerConnection.CreateBuildRequest(
                requestId,
                this._language,
                arguments,
                workingDirectory: buildPaths.WorkingDirectory,
                tempDirectory: buildPaths.TempDirectory!,
                keepAlive: keepAlive,
                libDirectory: libDirectory);

            var buildResponseTask = this._compileOnServerFunc(
                buildRequest,
                pipeName,
                cancellationToken: default);

            buildResponse = buildResponseTask.Result;

            if (buildResponse is null)
            {
                // 任务执行成功时，buildResponse必不为空。
                Debug.Assert(false);
                return null;
            }
        }
        catch (Exception)
        {
            return null;
        }

        switch (buildResponse.Type)
        {
            case BuildResponse.ResponseType.Completed:
                {
                    var completedResponse = (CompletedBuildResponse)buildResponse;
                    return ConsoleUtil.RunWithUtf8Output(completedResponse.Utf8Output, textWriter, tw =>
                    {
                        tw.Write(completedResponse.Output);
                        return new RunCompilationResult(completedResponse.ReturnCode, ranOnServer: true);
                    });
                }

            case BuildResponse.ResponseType.MismatchedVersion:
            case BuildResponse.ResponseType.IncorrectHash:
            case BuildResponse.ResponseType.Rejected:
            case BuildResponse.ResponseType.AnalyzerInconsistency:
            case BuildResponse.ResponseType.CannotConnect:
                // Build could not be completed on the server.
                return null;
            default:
                // Will not happen with our server but hypothetically could be sent by a rogue server.  Should
                // not let that block compilation.
                Debug.Assert(false);
                return null;
        }
    }

    private static IEnumerable<string> GetCommandLineArgs(IEnumerable<string> args)
    {
        if (BuildClient.UseNativeArguments())
            return BuildClient.GetCommandLineWindows(args);

        return args;
    }

    private static bool UseNativeArguments()
    {
        if (!PlatformInformation.IsWindows)
            return false;
        else if (PlatformInformation.IsRunningOnMono)
            return false;
        else if (RuntimeHostInfo.IsCoreClrRuntime)
            // The native invoke ends up giving us both CoreRun and the exe file.
            // We've decided to ignore backcompat for CoreCLR,
            // and use the Main()-provided arguments
            // https://github.com/dotnet/roslyn/issues/6677
            return false;

        return true;
    }

    private static bool AreNamedPipesSupported()
    {
        if (!PlatformInformation.IsRunningOnMono)
            return true;

        IDisposable? npcs = null;
        try
        {
            var testPipeName = $"mono-{Guid.NewGuid()}";
            // Mono configurations without named pipe support will throw a PNSE at some point in this process.
            npcs = new NamedPipeClientStream(".", testPipeName, PipeDirection.InOut);
            npcs.Dispose();
            return true;
        }
        catch (PlatformNotSupportedException)
        {
            if (npcs is not null)
            {
                // Compensate for broken finalizer in older builds of mono
                // https://github.com/mono/mono/commit/2a731f29b065392ca9b44d6613abee2aa413a144
                GC.SuppressFinalize(npcs);
            }
            return false;
        }
    }

    /// <summary>
    /// When running on Windows we can't take the command line which was provided to the 
    /// Main method of the application.  That will go through normal windows command line 
    /// parsing which eliminates artifacts like quotes.  This has the effect of normalizing
    /// the below command line options, which are semantically different, into the same
    /// value:
    ///
    ///     /reference:a,b
    ///     /reference:"a,b"
    ///
    /// To get the correct semantics here on Windows we parse the original command line 
    /// provided to the process.
    /// </summary>
    private static IEnumerable<string> GetCommandLineWindows(IEnumerable<string> args)
    {
        var ptr = NativeMethods.GetCommandLine();
        if (ptr == IntPtr.Zero) return args;

        // This memory is owned by the operating system hence we shouldn't (and can't)
        // free the memory.
        var commandLine = Marshal.PtrToStringUni(ptr);
        Debug.Assert(commandLine is not null);

        // The first argument will be the executable name hence we skip it.
        return CommandLineParser.SplitCommandLineIntoArguments(commandLine!, removeHashComments: false).Skip(1);
    }
}
