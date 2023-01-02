// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilationOptions = LuaCompilationOptions;
using ThisParseOptions = LuaParseOptions;
using ThisCompilation = LuaCompilation;
using ThisCompiler = LuaCompiler;
using ThisCommandLineParser = LuaCommandLineParser;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilationOptions = MoonScriptCompilationOptions;
using ThisParseOptions = MoonScriptParseOptions;
using ThisCompilation = MoonScriptCompilation;
using ThisCompiler = MoonScriptCompiler;
using ThisCommandLineParser = MoonScriptCommandLineParser;
#endif

/// <summary>
/// 此类表示<see cref="ThisCompiler"/>的命令行参数。
/// </summary>
public sealed partial class
#if LANG_LUA
    LuaCommandLineArguments
#elif LANG_MOONSCRIPT
    MoonScriptCommandLineArguments
#endif
    : CommandLineArguments
{
    /// <summary>
    /// 获取或设置<see cref="ThisCompiler"/>创建的编译内容选项集。
    /// </summary>
    public new ThisCompilationOptions CompilationOptions { get; internal set; }

    /// <summary>
    /// 获取或设置<see cref="ThisCompilation"/>的解析选项集。
    /// </summary>
    public new ThisParseOptions ParseOptions { get; internal set; }

    /// <summary>
    /// 获取或设置一个值，指示命令行错误信息是否应当包含错误文本的结尾行号和列号。
    /// </summary>
    /// <value>
    /// 若命令行错误信息应当包含错误文本的结尾行号和列号，则为<see langword="true"/>；否则为<see langword="false"/>。
    /// </value>
    internal bool ShouldIncludeErrorEndLocation { get; set; }

    /// <remarks>
    /// 应由<see cref="ThisCommandLineParser.Parse(IEnumerable{string}, string?, string?, string?)"/>初始化各属性。
    /// </remarks>
#pragma warning disable CS8618
    internal
#if LANG_LUA
        LuaCommandLineArguments
#elif LANG_MOONSCRIPT
        MoonScriptCommandLineArguments
#endif
    ()
    { }
#pragma warning restore CS8618

    #region CommandLineArguments
    protected override CompilationOptions CompilationOptionsCore => this.CompilationOptions;
    protected override ParseOptions ParseOptionsCore => this.ParseOptions;
    #endregion
}
