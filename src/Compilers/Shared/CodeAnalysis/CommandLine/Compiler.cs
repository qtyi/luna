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
    /// <summary>响应文件名称。</summary>
    internal const string ResponseFileName = ExecutableName + ".rsp";

    /// <summary>命令行诊断格式化器。</summary>
    private readonly CommandLineDiagnosticFormatter _diagnosticFormatter;
    /// <summary>零时文件夹。</summary>
    private readonly string? _tempDirectory;

    public override DiagnosticFormatter DiagnosticFormatter => this._diagnosticFormatter;
    protected internal new ThisCommandLineParser Arguments => (ThisCommandLineParser)base.Arguments;

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
        : base(parser, responseFile, args, buildPaths, additionalReferenceDirectories, assemblyLoader, driverCache, fileSystem)
    {
        this._diagnosticFormatter = new(buildPaths.WorkingDirectory, this.Arguments.PrintFullPaths, this.Arguments.ShouldIncludeErrorEndLocation);
        this._tempDirectory = buildPaths.TempDirectory;
    }
}
