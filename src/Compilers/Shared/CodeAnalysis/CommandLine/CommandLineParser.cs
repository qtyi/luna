// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCommandLineArguments = LuaCommandLineArguments;
using ThisCommandLineParser = LuaCommandLineParser;
using ThisMessageProvider = MessageProvider;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCommandLineArguments = MoonScriptCommandLineArguments;
using ThisCommandLineParser = MoonScriptCommandLineParser;
using ThisMessageProvider = MessageProvider;
#endif

public partial class
#if LANG_LUA
    LuaCommandLineParser
#elif LANG_MOONSCRIPT
    MoonScriptCommandLineParser
#endif
    : CommandLineParser
{
    /// <summary>
    /// 获取默认的命令行解析器。
    /// </summary>
    public static ThisCommandLineParser Default { get; } = new();
    /// <summary>
    /// 获取默认的脚本的命令行解析器。
    /// </summary>
    public static ThisCommandLineParser Script { get; } = new(isScriptCommandLineParser: true);

    private static readonly char[] s_quoteOrEquals = new[] { '"', '=' };
    private static readonly char[] s_warningSeparators = new[] { ',', ';', ' ' };

    internal
#if LANG_LUA
        LuaCommandLineParser
#elif LANG_MOONSCRIPT
        MoonScriptCommandLineParser
#endif
        (bool isScriptCommandLineParser = false)
        : base(ThisMessageProvider.Instance, isScriptCommandLineParser) { }

    /// <summary>
    /// 解析命令行参数。
    /// </summary>
    /// <param name="args">尚未解析的命令行参数，可能仍保留一些供解析器识别的语法信息。</param>
    /// <param name="baseDirectory">编译环境的基文件夹。</param>
    /// <param name="sdkDirectory">SDK所在文件夹。</param>
    /// <param name="additionalReferenceDirectories">附加的引用文件夹。</param>
    /// <returns>解析后的结果。</returns>
    public new partial ThisCommandLineArguments Parse(
        IEnumerable<string> args,
        string? baseDirectory,
        string? sdkDirectory,
        string? additionalReferenceDirectories = null);

    #region AddDiagnostic
    /// <summary>
    /// 添加包含指定错误码的诊断错误。
    /// </summary>
    /// <inheritdoc cref="AddDiagnostic(IList{Diagnostic}, Dictionary{string, ReportDiagnostic}, ErrorCode, object[])"/>
    private static void AddDiagnostic(IList<Diagnostic> diagnostics, ErrorCode errorCode) =>
        diagnostics.Add(Diagnostic.Create(ThisMessageProvider.Instance, (int)errorCode));

    /// <summary>
    /// 添加包含指定错误码和其他参数的诊断错误。
    /// </summary>
    /// <inheritdoc cref="AddDiagnostic(IList{Diagnostic}, Dictionary{string, ReportDiagnostic}, ErrorCode, object[])"/>
    private static void AddDiagnostic(IList<Diagnostic> diagnostics, ErrorCode errorCode, params object[] arguments) =>
        diagnostics.Add(Diagnostic.Create(ThisMessageProvider.Instance, (int)errorCode, arguments));

    /// <summary>
    /// 若<paramref name="warningOptions"/>未提及抑制<paramref name="errorCode"/>，则添加包含其的诊断错误。
    /// </summary>
    /// <param name="diagnostics">容纳诊断错误的列表。</param>
    /// <param name="warningOptions">对警告的设置，决定是否抑制某些错误。</param>
    /// <param name="errorCode">要报告的诊断错误的错误码。</param>
    /// <param name="arguments">要报告的诊断错误的其他参数。</param>
    private static void AddDiagnostic(
        IList<Diagnostic> diagnostics,
        Dictionary<string, ReportDiagnostic> warningOptions,
        ErrorCode errorCode,
        params object[] arguments)
    {
        if (warningOptions.TryGetValue(ThisMessageProvider.Instance.GetIdForErrorCode((int)errorCode), out var value) &&
            value != ReportDiagnostic.Suppress)
            AddDiagnostic(diagnostics, errorCode, arguments);
    }
    #endregion

    #region CommandLineParser
    /// <remarks>
    /// 此为供基类调用的通用方法，使用子类时应调用<see cref="Parse(IEnumerable{string}, string?, string?, string?)"/>
    /// </remarks>
    /// <inheritdoc cref="Parse(IEnumerable{string}, string?, string?, string?)"/>
    internal override CommandLineArguments CommonParse(
        IEnumerable<string> args,
        string baseDirectory,
        string? sdkDirectory,
        string? additionalReferenceDirectories) =>
        this.Parse(args, baseDirectory, sdkDirectory, additionalReferenceDirectories);

    /// <summary>
    /// 当递归查找与模式相匹配的文件，但未找到任何文件时，产生诊断错误。
    /// </summary>
    /// <param name="path">要查找的文件路径</param>
    /// <param name="diagnostics">容纳诊断错误的列表。</param>
    internal override void GenerateErrorForNoFilesFoundInRecurse(string path, IList<Diagnostic> diagnostics)
    {
        // 不产生诊断错误。
        return;
    }
    #endregion
}
