// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;
using System.Collections.Immutable;
using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

internal sealed class
#if LANG_LUA
    LuaDiagnosticInfo
#elif LANG_MOONSCRIPT
    MoonScriptDiagnosticInfo
#endif
    : DiagnosticInfoWithSymbols
{
    public static readonly DiagnosticInfo EmptyErrorInfo = new ThisDiagnosticInfo(default);
    public static readonly DiagnosticInfo VoidDiagnosticInfo = new ThisDiagnosticInfo(ErrorCode.Void);

    private readonly IReadOnlyList<Location> _additionalLocations;
    public override IReadOnlyList<Location> AdditionalLocations => this._additionalLocations;

    internal new ErrorCode Code => (ErrorCode)base.Code;

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

    internal static bool IsEmpty(DiagnosticInfo info) => object.ReferenceEquals(info, ThisDiagnosticInfo.EmptyErrorInfo);
}
