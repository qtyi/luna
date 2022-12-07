// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCommandLineParser = LuaCommandLineParser;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCommandLineParser = MoonScriptCommandLineParser;
#endif

internal abstract partial class
#if LANG_LUA
    LuaCompiler
#elif LANG_MOONSCRIPT
    MoonScriptCompiler
#endif
    : CommonCompiler
{
    internal const string ResponseFileName = ExecutableName + ".rsp";

    protected
#if LANG_LUA
        LuaCompiler
#elif LANG_MOONSCRIPT
        MoonScriptCompiler
#endif
    (
        ThisCommandLineParser parser,
        string? responseFile,
        string[] args,
        BuildPaths buildPaths,
        string? additionalReferenceDirectories,
        IAnalyzerAssemblyLoader assemblyLoader,
        GeneratorDriverCache? driverCache = null,
        ICommonCompilerFileSystem? fileSystem = null)
        : base(parser, responseFile, args, buildPaths, additionalReferenceDirectories, assemblyLoader, driverCache, fileSystem);
}
