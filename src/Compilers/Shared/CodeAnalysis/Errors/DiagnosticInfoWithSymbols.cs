// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

using ThisMessageProvider = MessageProvider;

/// <summary>
/// A DiagnosticInfoWithSymbols object provides, more than normal <see cref="DiagnosticInfo"/> provides, access to symbols which the diagnostic is in.
/// </summary>
internal class DiagnosticInfoWithSymbols : DiagnosticInfo
{
    /// <summary>
    /// Symbols which the diagnostic is in.
    /// </summary>
    /// <remarks>
    /// NOTE: This field is not serialized.
    /// </remarks>
    internal readonly ImmutableArray<Symbol> Symbols;

    /// <inheritdoc cref="DiagnosticInfoWithSymbols.DiagnosticInfoWithSymbols(bool, ErrorCode, object[], ImmutableArray{Symbol})"/>
    internal DiagnosticInfoWithSymbols(
        ErrorCode code,
        object[] arguments,
        ImmutableArray<Symbol> symbols)
        : base(
            ThisMessageProvider.Instance,
            (int)code,
            arguments) =>
        this.Symbols = symbols;

    /// <summary>
    /// Create an instance of <see cref="DiagnosticInfoWithSymbols"/> class.
    /// </summary>
    /// <param name="isWarningAsError">A value indecate whether to treate a warning diagnostic as an error one.</param>
    /// <param name="code">The error code of a diagnostic.</param>
    /// <param name="arguments">The arguments to format a diagnostic text.</param>
    /// <param name="symbols">A collection of symbols which a diagnostic is in.</param>
    internal DiagnosticInfoWithSymbols(
        bool isWarningAsError,
        ErrorCode code,
        object[] arguments,
        ImmutableArray<Symbol> symbols)
        : base(
            ThisMessageProvider.Instance,
            isWarningAsError,
            (int)code,
            arguments) =>
        this.Symbols = symbols;

    /// <summary>
    /// Create an instance of <see cref="DiagnosticInfoWithSymbols"/> class with an original one and new diagnostic severity.
    /// </summary>
    /// <param name="original">The original diagnostic info.</param>
    /// <param name="severity">New diagnostic severity.</param>
    protected DiagnosticInfoWithSymbols(
        DiagnosticInfoWithSymbols original,
        DiagnosticSeverity severity)
        : base(original, severity) =>
        this.Symbols = original.Symbols;

    #region DiagnosticInfo
    /// <summary>
    /// Create a new instance of this diagnostic info with the Severity property changed.
    /// </summary>
    /// <param name="severity">New diagnostic severity.</param>
    /// <returns>A new diagnostic info.</returns>
    protected override DiagnosticInfo GetInstanceWithSeverityCore(DiagnosticSeverity severity) => new DiagnosticInfoWithSymbols(this, severity);
    #endregion
}
