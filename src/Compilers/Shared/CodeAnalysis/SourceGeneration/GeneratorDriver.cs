// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisGeneratorDriver = LuaGeneratorDriver;
using ThisMessageProvider = MessageProvider;
using ThisParseOptions = LuaParseOptions;
using ThisSyntaxHelper = LuaSyntaxHelper;
using ThisSyntaxTree = LuaSyntaxTree;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisGeneratorDriver = MoonScriptGeneratorDriver;
using ThisMessageProvider = MessageProvider;
using ThisParseOptions = MoonScriptParseOptions;
using ThisSyntaxHelper = MoonScriptSyntaxHelper;
using ThisSyntaxTree = MoonScriptSyntaxTree;
#endif

#if LANG_LUA
/// <summary>
/// Lua语言的源生成器启动器的实现。
/// </summary>
#elif LANG_MOONSCRIPT
/// <summary>
/// MoonScript语言的源生成器启动器的实现。
/// </summary>
#endif
public sealed partial class
#if LANG_LUA
    LuaGeneratorDriver
#elif LANG_MOONSCRIPT
    MoonScriptGeneratorDriver
#endif
    : GeneratorDriver
{
    /// <summary>
    /// 获取消息提供器。
    /// </summary>
    /// <value>
    /// 消息提供器。
    /// </value>
    internal override CommonMessageProvider MessageProvider => ThisMessageProvider.Instance;

    /// <summary>
    /// 获取语法帮助器。
    /// </summary>
    /// <value>
    /// 语法帮助器。
    /// </value>
    internal override ISyntaxHelper SyntaxHelper => ThisSyntaxHelper.Instance;

    /// <summary>
    /// 创建生成器启动器的新实例。
    /// </summary>
    /// <param name="parseOptions">解析选项，用于解析生成的文件。</param>
    /// <param name="generators">所有的源生成器，将作为启动器的一部分运行。</param>
    /// <param name="optionsProvider">分析器配置选项提供器，用于通过此驱动程序中的生成器检索分析器配置值的。</param>
    /// <param name="additionalTexts">此启动器中，能提供给生成器使用的所有附加文本。</param>
    /// <param name="driverOptions">生成器启动器选项，用于配置此生成器启动器。</param>
    internal
#if LANG_LUA
        LuaGeneratorDriver
#elif LANG_MOONSCRIPT
        MoonScriptGeneratorDriver
#endif
    (
        ThisParseOptions parseOptions,
        ImmutableArray<ISourceGenerator> generators,
        AnalyzerConfigOptionsProvider optionsProvider,
        ImmutableArray<AdditionalText> additionalTexts,
        GeneratorDriverOptions driverOptions)
        : base(parseOptions, generators, optionsProvider, additionalTexts, driverOptions) { }

    /// <summary>
    /// 使用生成器启动器状态创建<see cref="ThisGeneratorDriver"/>的新实例。
    /// </summary>
    /// <param name="state">已有的生成器启动器状态。</param>
    private
#if LANG_LUA
        LuaGeneratorDriver
#elif LANG_MOONSCRIPT
        MoonScriptGeneratorDriver
#endif
    (GeneratorDriverState state) : base(state) { }

    /// <summary>
    /// 使用指定的源生成器和默认的选项创建<see cref="ThisGeneratorDriver"/>的新实例。
    /// </summary>
    /// <inheritdoc cref="Create(IEnumerable{ISourceGenerator}, IEnumerable{AdditionalText}?, ThisParseOptions?, AnalyzerConfigOptionsProvider?, GeneratorDriverOptions)"/>
    public static ThisGeneratorDriver Create(params ISourceGenerator[] generators) =>
        ThisGeneratorDriver.Create(generators, additionalTexts: null);

    /// <summary>
    /// 使用指定的增量源生成器和默认的选项创建<see cref="ThisGeneratorDriver"/>的新实例。
    /// </summary>
    /// <param name="incrementalGenerators">用于创建生成器启动器的增量源生成器。</param>
    /// <inheritdoc cref="Create(IEnumerable{ISourceGenerator}, IEnumerable{AdditionalText}?, ThisParseOptions?, AnalyzerConfigOptionsProvider?, GeneratorDriverOptions)"/>
    public static ThisGeneratorDriver Create(params IIncrementalGenerator[] incrementalGenerators) =>
        ThisGeneratorDriver.Create(incrementalGenerators.Select(GeneratorExtensions.AsSourceGenerator), additionalTexts: null);

    /// <summary>
    /// 使用指定的源生成器和选项（未指定的为默认）创建<see cref="ThisGeneratorDriver"/>的新实例。
    /// </summary>
    /// <param name="generators">用于创建生成器启动器的源生成器。</param>
    /// <param name="additionalTexts">此启动器中，能提供给生成器使用的所有附加文本。若没有则传入<see langword="null"/>。</param>
    /// <param name="parseOptions">解析选项，用于解析生成的文件。传入<see langword="null"/>时使用<see cref="ThisParseOptions.Default"/>。</param>
    /// <param name="optionsProvider">分析器配置选项提供器，用于通过此驱动程序中的生成器检索分析器配置值的。若没有则传入<see langword="null"/>。</param>
    /// <param name="driverOptions">生成器启动器选项，用于配置此生成器启动器。</param>
    /// <returns><see cref="ThisGeneratorDriver"/>的新实例。</returns>
    public static ThisGeneratorDriver Create(
        IEnumerable<ISourceGenerator> generators,
        IEnumerable<AdditionalText>? additionalTexts = null,
        ThisParseOptions? parseOptions = null,
        AnalyzerConfigOptionsProvider? optionsProvider = null,
        GeneratorDriverOptions driverOptions = default) =>
        new(
            parseOptions ?? ThisParseOptions.Default,
            generators.ToImmutableArray(),
            optionsProvider ?? CompilerAnalyzerConfigOptionsProvider.Empty,
            additionalTexts.AsImmutableOrEmpty(),
            driverOptions);

    /// <summary>
    /// 解析生成后的源文件为语法树。
    /// </summary>
    /// <param name="input">生成后的源文件。</param>
    /// <param name="fileName">源文件的文件名称。</param>
    /// <param name="cancellationToken">取消操作的标记。</param>
    /// <returns>解析后的与<paramref name="input"/>对应的语法树。</returns>
    internal override SyntaxTree ParseGeneratedSourceText(GeneratedSourceText input, string fileName, CancellationToken cancellationToken) =>
        ThisSyntaxTree.ParseTextLazy(input.Text, (ThisParseOptions)base._state.ParseOptions, fileName);

    /// <summary>
    /// 使用已有的生成器启动器状态创建生成器启动器的新实例。
    /// </summary>
    /// <param name="state">已有的生成器启动器状态。</param>
    /// <returns>生成器启动器的新实例。</returns>
    internal override GeneratorDriver FromState(GeneratorDriverState state) => new ThisGeneratorDriver(state);
}
