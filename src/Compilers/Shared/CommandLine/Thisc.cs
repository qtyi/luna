// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CommandLine;
using Microsoft.CodeAnalysis.ErrorReporting;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.CommandLine;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.CommandLine;
#endif

/// <summary>
/// Represents an implementation of <see cref="ThisCompiler"/>.
/// </summary>
internal sealed class
#if LANG_LUA
    Luac
#elif LANG_MOONSCRIPT
    Moonc
#endif
    : ThisCompiler
{
    /// <summary>
    /// Initialize a new instance of <see cref="Thisc"/>.
    /// </summary>
    /// <param name="responseFile">Response file path, <see langword="null"/> if not specified.</param>
    /// <param name="buildPaths">Paths for build.</param>
    /// <param name="args">Command-line arguments.</param>
    /// <param name="analyzerLoader">Analyzer loader that handles loading analyzer assemblies and their dependencies.</param>
    internal
#if LANG_LUA
        Luac
#elif LANG_MOONSCRIPT
        Moonc
#endif
    (string? responseFile, BuildPaths buildPaths, string[] args, IAnalyzerAssemblyLoader analyzerLoader)
        : base(ThisCommandLineParser.Default, responseFile, args, buildPaths, Environment.GetEnvironmentVariable("LIB"), analyzerLoader)
    {
    }

    /// <summary>
    /// Entry point of a compiler.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    /// <param name="buildPaths">Paths for build.</param>
    /// <param name="textWriter">Console output.</param>
    /// <param name="analyzerLoader">Analyzer loader that handles loading analyzer assemblies and their dependencies.</param>
    /// <returns>Returns error code, <c>0</c> for no error.</returns>
    internal static int Run(string[] args, BuildPaths buildPaths, TextWriter textWriter, IAnalyzerAssemblyLoader analyzerLoader)
    {
        FatalError.SetHandlers(FailFast.Handler, nonFatalHandler: null);

        var responseFile = Path.Combine(buildPaths.ClientDirectory, ResponseFileName);
        var compiler = new Thisc(responseFile, buildPaths, args, analyzerLoader);
        return ConsoleUtil.RunWithUtf8Output(compiler.Arguments.Utf8Output, textWriter, tw => compiler.Run(tw));
    }
}
