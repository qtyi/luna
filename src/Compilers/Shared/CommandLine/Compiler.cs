// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CommandLine;
using Microsoft.CodeAnalysis.ErrorReporting;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.CommandLine;

using Thisc = Luac;
using ThisCompiler = LuaCompiler;
using ThisCommandLineParser = LuaCommandLineParser;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.CommandLine;

using Thisc = Moonc;
using ThisCompiler = MoonScriptCompiler;
using ThisCommandLineParser = MoonScriptCommandLineParser;
#endif

internal sealed class
#if LANG_LUA
    Luac
#elif LANG_MOONSCRIPT
    Moonc
#endif
    : ThisCompiler
{
    internal
#if LANG_LUA
        Luac
#elif LANG_MOONSCRIPT
        Moonc
#endif
    (string responseFile, BuildPaths buildPaths, string[] args, IAnalyzerAssemblyLoader analyzerLoader)
        : base(ThisCommandLineParser.Default, responseFile, args, buildPaths, Environment.GetEnvironmentVariable("LIB"), analyzerLoader)
    {
    }

    internal static int Run(string[] args, BuildPaths buildPaths, TextWriter textWriter, IAnalyzerAssemblyLoader analyzerLoader)
    {
        FatalError.Handler = FailFast.Handler;

        var responseFile = Path.Combine(buildPaths.ClientDirectory, ThisCompiler.ResponseFileName);
        var compiler = new Thisc(responseFile, buildPaths, args, analyzerLoader);
        return ConsoleUtil.RunWithUtf8Output(compiler.Arguments.Utf8Output, textWriter, tw => compiler.Run(tw));
    }
}
