// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.CommandLine;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.CommandLine;

using Thisc = Luac;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.CommandLine;

using Thisc = Moonc;
#endif

public partial class Program
{
    public static int Main(string[] args)
    {
        try
        {
            return MainCore(args);
        }
        catch (FileNotFoundException e)
        {
            // Catch exception from missing compiler assembly.
            // Report the exception message and terminate the process.
            Console.WriteLine(e.Message);
            return CommonCompiler.Failed;
        }
    }

    private static int MainCore(string[] args)
    {
        using var logger = new CompilerServerLogger($"{Thisc.ExecutableName} {Process.GetCurrentProcess().Id}");

#if BOOTSTRAP
        ExitingTraceListener.Install(logger);
#endif

        const RequestLanguage requestLanguage =
#if LANG_LUA
            RequestLanguage.LuaCompile
#elif LANG_MOONSCRIPT
            RequestLanguage.MoonScriptCompile
#endif
            ;
        return BuildClient.Run(args, requestLanguage, Thisc.Run, BuildClient.GetCompileOnServerFunc(logger));
    }

    public static int Run(string[] args, string clientDir, string workingDir, string sdkDir, string tempDir, TextWriter textWriter, IAnalyzerAssemblyLoader analyzerLoader)
        => Thisc.Run(args, new BuildPaths(clientDir: clientDir, workingDir: workingDir, sdkDir: sdkDir, tempDir: tempDir), textWriter, analyzerLoader);
}
