// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.IO.Pipes;
using System.Runtime.InteropServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CommandLine;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.CommandLine;

using RoslynBuildServerConnection = Microsoft.CodeAnalysis.CommandLine.BuildServerConnection;
using RoslynRequestLanguage = Microsoft.CodeAnalysis.CommandLine.RequestLanguage;

internal sealed class BuildServerConnection
{
    internal const int TimeOutMsExistingProcess = RoslynBuildServerConnection.TimeOutMsExistingProcess;

    internal const int TimeOutMsNewProcess = RoslynBuildServerConnection.TimeOutMsNewProcess;

    internal static bool IsCompilerServerSupported => RoslynBuildServerConnection.IsCompilerServerSupported;

    internal static BuildRequest CreateBuildRequest(
        Guid requestId,
        RequestLanguage language,
        List<string> arguments,
        string workingDirectory,
        string tempDirectory,
        string? keepAlive,
        string? libDirectory) =>
        RoslynBuildServerConnection.CreateBuildRequest(requestId, (RoslynRequestLanguage)language, arguments, workingDirectory, tempDirectory, keepAlive, libDirectory);

    internal static async Task<bool> RunServerShutdownRequestAsync(
        string pipeName,
        int? timeoutOverride,
        bool waitForProcess,
        ICompilerServerLogger logger,
        CancellationToken cancellationToken) =>
        await RoslynBuildServerConnection.RunServerShutdownRequestAsync(pipeName, timeoutOverride, waitForProcess, logger, cancellationToken).ConfigureAwait(false);

    internal static Task<BuildResponse> RunServerBuildRequestAsync(
        BuildRequest buildRequest,
        string pipeName,
        string clientDirectory,
        ICompilerServerLogger logger,
        CancellationToken cancellationToken) =>
        RoslynBuildServerConnection.RunServerBuildRequestAsync(buildRequest, pipeName, clientDirectory, logger, cancellationToken);

    internal static async Task<BuildResponse> RunServerBuildRequestAsync(
        BuildRequest buildRequest,
        string pipeName,
        int? timeoutOverride,
        Func<string, ICompilerServerLogger, bool> tryCreateServerFunc,
        ICompilerServerLogger logger,
        CancellationToken cancellationToken) =>
        await RoslynBuildServerConnection.RunServerBuildRequestAsync(buildRequest, pipeName, timeoutOverride, tryCreateServerFunc, logger, cancellationToken).ConfigureAwait(false);

    internal static async Task MonitorDisconnectAsync(
        PipeStream pipeStream,
        Guid requestId,
        ICompilerServerLogger logger,
        CancellationToken cancellationToken = default) =>
        await RoslynBuildServerConnection.MonitorDisconnectAsync(pipeStream, requestId, logger, cancellationToken).ConfigureAwait(false);

    internal static async Task<NamedPipeClientStream?> TryConnectToServerAsync(
        string pipeName,
        int timeoutMs,
        ICompilerServerLogger logger,
        CancellationToken cancellationToken) =>
        await RoslynBuildServerConnection.TryConnectToServerAsync(pipeName, timeoutMs, logger, cancellationToken).ConfigureAwait(false);

    internal static (string processFilePath, string commandLineArguments, string toolFilePath) GetServerProcessInfo(string clientDir, string pipeName)
    {
        var serverPathWithoutExtension = Path.Combine(clientDir, "LunaCompiler");
        var commandLineArgs = $@"""-pipename:{pipeName}""";
        return RuntimeHostInfo.GetProcessInfo(serverPathWithoutExtension, commandLineArgs);
    }

    internal static string GetPipeName(string clientDirectory) =>
        RoslynBuildServerConnection.GetPipeName(clientDirectory);

    internal static string GetPipeName(
        string userName,
        bool isAdmin,
        string clientDirectory) =>
        RoslynBuildServerConnection.GetPipeName(userName, isAdmin, clientDirectory);

    internal static bool WasServerMutexOpen(string mutexName) =>
        RoslynBuildServerConnection.WasServerMutexOpen(mutexName);

    internal static IServerMutex OpenOrCreateMutex(string name, out bool createdNew) =>
        RoslynBuildServerConnection.OpenOrCreateMutex(name, out createdNew);

    internal static string GetServerMutexName(string pipeName) =>
        RoslynBuildServerConnection.GetServerMutexName(pipeName);

    internal static string GetClientMutexName(string pipeName) =>
        RoslynBuildServerConnection.GetClientMutexName(pipeName);

    internal static string? GetTempPath(string? workingDir) =>
        RoslynBuildServerConnection.GetTempPath(workingDir);
}
