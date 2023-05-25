// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;
using System.Collections.Immutable;
using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisDiagnostic = LuaDiagnostic;
using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnostic = MoonScriptDiagnostic;
using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

/// <summary>
/// A <see cref="ThisDiagnosticInfo"/> object has information about a <see cref="ThisDiagnostic"/>, with information about symbols and additional locations.
/// </summary>
internal sealed class
#if LANG_LUA
    LuaDiagnosticInfo
#elif LANG_MOONSCRIPT
    MoonScriptDiagnosticInfo
#endif
    : DiagnosticInfoWithSymbols
{
    /// <summary>
    /// The unique instance of <see cref="DiagnosticInfo"/> which represent an empty error info.
    /// </summary>
    public static readonly DiagnosticInfo EmptyErrorInfo = new ThisDiagnosticInfo(default);
    /// <summary>
    /// The unique instance of <see cref="DiagnosticInfo"/> which represent a void diagnostic info.
    /// </summary>
    public static readonly DiagnosticInfo VoidDiagnosticInfo = new ThisDiagnosticInfo(ErrorCode.Void);

    /// <summary>A collection of additional locations of this diagnostic in source code.</summary>
    private readonly IReadOnlyList<Location> _additionalLocations;
    /// <summary>
    /// Gets additional locations of this diagnostic.
    /// </summary>
    /// <value>
    /// A collection of additional locations of this diagnostic in source code.
    /// </value>
    public override IReadOnlyList<Location> AdditionalLocations => this._additionalLocations;

    /// <summary>
    /// Gets the error code.
    /// </summary>
    /// <value>
    /// Error code of this diagnostic.
    /// </value>
    internal new ErrorCode Code => (ErrorCode)base.Code;

    /// <summary>
    /// Initialize an instance of <see cref="ThisDiagnosticInfo"/> class with specific error code.
    /// </summary>
    /// <param name="code">Error code of a diagnostic.</param>
    internal
#if LANG_LUA
        LuaDiagnosticInfo
#elif LANG_MOONSCRIPT
        MoonScriptDiagnosticInfo
#endif
        (ErrorCode code) : this(
            code,
            Array.Empty<object>(),
            ImmutableArray<Symbol>.Empty,
            ImmutableArray<Location>.Empty)
    { }

    /// <summary>
    /// Initialize an instance of <see cref="ThisDiagnosticInfo"/> class with specific error code and format arguments.
    /// </summary>
    /// <param name="code">Error code of a diagnostic.</param>
    /// <param name="args">Arguments for formatting a text.</param>
    internal
#if LANG_LUA
        LuaDiagnosticInfo
#elif LANG_MOONSCRIPT
        MoonScriptDiagnosticInfo
#endif
        (ErrorCode code, params object[] args) : this(
            code,
            args,
            ImmutableArray<Symbol>.Empty,
            ImmutableArray<Location>.Empty)
    { }

    /// <summary>
    /// Initialize an instance of <see cref="ThisDiagnosticInfo"/> class with specific error code and format arguments.
    /// </summary>
    /// <param name="code">Error code of a diagnostic.</param>
    /// <param name="args">Arguments for formatting a text.</param>
    /// <param name="symbols">A collection of symbols a diagnostic is in.</param>
    internal
#if LANG_LUA
        LuaDiagnosticInfo
#elif LANG_MOONSCRIPT
        MoonScriptDiagnosticInfo
#endif
        (ErrorCode code, object[] args, ImmutableArray<Symbol> symbols) : this(
            code,
            args,
            symbols,
            ImmutableArray<Location>.Empty)
    { }

    /// <summary>
    /// Initialize an instance of <see cref="ThisDiagnosticInfo"/> class with specific error code and format arguments.
    /// </summary>
    /// <param name="code">Error code of a diagnostic.</param>
    /// <param name="args">Arguments for formatting a text.</param>
    /// <param name="symbols">A collection of symbols a diagnostic is in.</param>
    /// <param name="additionalLocations">A collection of additional locations of this diagnostic in source code.</param>
    internal
#if LANG_LUA
        LuaDiagnosticInfo
#elif LANG_MOONSCRIPT
        MoonScriptDiagnosticInfo
#endif
        (ErrorCode code, object[] args, ImmutableArray<Symbol> symbols, ImmutableArray<Location> additionalLocations) : base(code, args, symbols)
    {
        Debug.Assert(code != ErrorCode.ERR_InternalError);
        this._additionalLocations = additionalLocations.IsDefaultOrEmpty ? SpecializedCollections.EmptyReadOnlyList<Location>() : additionalLocations;
    }

    /// <summary>
    /// Whether a <see cref="DiagnosticInfo"/> is empty.
    /// </summary>
    /// <param name="info">The diagnostic info to be tested.</param>
    /// <returns><see langword="tree"/> if <paramref name="info"/> is empty; otherwise, <see langword="false"/>.</returns>
    internal static bool IsEmpty(DiagnosticInfo info) => object.ReferenceEquals(info, ThisDiagnosticInfo.EmptyErrorInfo);
}
