// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class ErrorTypeSymbol : IErrorTypeSymbol
{
    private readonly Symbols.ErrorTypeSymbol _underlying;

    internal override Symbols.NamedTypeSymbol UnderlyingNamedTypeSymbol => _underlying;

    public ErrorTypeSymbol(Symbols.ErrorTypeSymbol underlying) : base()
    {
        Debug.Assert(underlying is not null);
        _underlying = underlying;
    }

    #region Microsoft.CodeAnalysis.IErrorTypeSymbol
    ImmutableArray<ISymbol> IErrorTypeSymbol.CandidateSymbols => _underlying.CandidateSymbols.GetPublicSymbols().Cast<ISymbol, ISymbol>();

    CandidateReason IErrorTypeSymbol.CandidateReason => _underlying.CandidateReason;
    #endregion

    #region IErrorTypeSymbol
    ImmutableArray<ISymbol> IErrorTypeSymbol.CandidateSymbols => _underlying.CandidateSymbols.GetPublicSymbols();
    #endregion
}
