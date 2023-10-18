// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class ErrorTypeSymbol : IErrorTypeSymbol
{
    private readonly InternalModel.Symbols.ErrorTypeSymbol _underlying;

    internal override Symbols.NamedTypeSymbol UnderlyingNamedTypeSymbol => this._underlying;

    public ErrorTypeSymbol(InternalModel.Symbols.ErrorTypeSymbol underlying) : base()
    {
        Debug.Assert(underlying is not null);
        this._underlying = underlying;
    }

    #region Microsoft.CodeAnalysis.IErrorTypeSymbol
    ImmutableArray<Microsoft.CodeAnalysis.ISymbol> Microsoft.CodeAnalysis.IErrorTypeSymbol.CandidateSymbols => this._underlying.CandidateSymbols.GetPublicSymbols().Cast<ISymbol, Microsoft.CodeAnalysis.ISymbol>();

    Microsoft.CodeAnalysis.CandidateReason Microsoft.CodeAnalysis.IErrorTypeSymbol.CandidateReason => this._underlying.CandidateReason;
    #endregion

    #region IErrorTypeSymbol
    ImmutableArray<ISymbol> IErrorTypeSymbol.CandidateSymbols => this._underlying.CandidateSymbols.GetPublicSymbols();
    #endregion
}
