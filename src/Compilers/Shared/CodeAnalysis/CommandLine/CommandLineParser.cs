// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

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

#if LANG_LUA
/// <summary>
/// The Lua command line parser.
/// </summary>
#elif LANG_MOONSCRIPT
/// <summary>
/// The MoonScript command line parser.
/// </summary>
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
    /// Gets a default command line parser for compiler.
    /// </summary>
    /// <value>
    /// An object that inherits <see cref="CommandLineParser"/> and serves as a command line parser.
    /// </value>
    public static ThisCommandLineParser Default { get; } = new();
    /// <summary>
    /// Gets a default command line parser for script.
    /// </summary>
    /// <value>
    /// An object that inherits <see cref="CommandLineParser"/> and serves as a command line parser.
    /// </value>
    public static ThisCommandLineParser Script { get; } = new(isScriptCommandLineParser: true);

    /// <summary>
    /// An array of <c>"</c> and <c>=</c>.
    /// </summary>
    private static readonly char[] s_quoteOrEquals = new[] { '"', '=' };
    /// <summary>
    /// An array of warning separators.
    /// </summary>
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
    /// Parses a command line.
    /// </summary>
    /// <param name="args">A collection of <see cref="string"/>s representing the command line arguments.</param>
    /// <param name="baseDirectory">The base directory used for qualifying file locations.</param>
    /// <param name="sdkDirectory">The directory to search for mscorlib, or <see langword="null"/> if not available.</param>
    /// <param name="additionalReferenceDirectories">A <see cref="string"/> representing additional reference paths.</param>
    /// <returns>A(n) <see cref="ThisCommandLineArguments"/> object representing the parsed command line.</returns>
    public new partial ThisCommandLineArguments Parse(
        IEnumerable<string> args,
        string? baseDirectory,
        string? sdkDirectory,
        string? additionalReferenceDirectories = null);

    #region AddDiagnostic
    /// <summary>
    /// Add diagnostic for the <paramref name="errorCode"/>.
    /// </summary>
    /// <inheritdoc cref="AddDiagnostic(IList{Diagnostic}, Dictionary{string, ReportDiagnostic}, ErrorCode, object[])"/>
    private static void AddDiagnostic(IList<Diagnostic> diagnostics, ErrorCode errorCode) =>
        diagnostics.Add(Diagnostic.Create(ThisMessageProvider.Instance, (int)errorCode));

    /// <summary>
    /// Add diagnostic for the <paramref name="errorCode"/> with message arguments.
    /// </summary>
    /// <inheritdoc cref="AddDiagnostic(IList{Diagnostic}, Dictionary{string, ReportDiagnostic}, ErrorCode, object[])"/>
    private static void AddDiagnostic(IList<Diagnostic> diagnostics, ErrorCode errorCode, params object[] arguments) =>
        diagnostics.Add(Diagnostic.Create(ThisMessageProvider.Instance, (int)errorCode, arguments));

    /// <summary>
    /// Diagnostic for the <paramref name="errorCode"/> added if the <paramref name="warningOptions"/> does not mention suppressed for the <paramref name="errorCode"/>.
    /// </summary>
    /// <param name="diagnostics">A list of diagnostics reported.</param>
    /// <param name="warningOptions">Describes how to report warning diagnostics.</param>
    /// <param name="errorCode">The error code of a warning diagnostic.</param>
    /// <param name="arguments">Arguments to the message of the diagnostic</param>
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
    /// Use <see cref="Parse(IEnumerable{string}, string?, string?, string?)"/> instead.
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
