﻿// Licensed to the Qtyi under one or more agreements.
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
#else
#error Not implemented
#endif

/// <summary>
/// The command line parser that produce <see cref="ThisCommandLineArguments"/>.
/// </summary>
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

    /// <summary>
    /// Generate diagnostics for no files found in recurse.
    /// </summary>
    /// <param name="path">Path of the file to search.</param>
    /// <param name="diagnostics">A list of diagnostics reported.</param>
    internal override void GenerateErrorForNoFilesFoundInRecurse(string path, IList<Diagnostic> diagnostics)
    {
        //  no error in this exe.
        return;
    }

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
    /// Use <see cref="ThisCommandLineParser.Parse(IEnumerable{string}, string?, string?, string?)"/> instead.
    /// </remarks>
    /// <inheritdoc cref="ThisCommandLineParser.Parse(IEnumerable{string}, string?, string?, string?)"/>
    internal override CommandLineArguments CommonParse(
        IEnumerable<string> args,
        string baseDirectory,
        string? sdkDirectory,
        string? additionalReferenceDirectories) =>
        this.Parse(args, baseDirectory, sdkDirectory, additionalReferenceDirectories);
    #endregion
}
