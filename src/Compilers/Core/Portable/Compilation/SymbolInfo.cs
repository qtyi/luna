// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis;

public readonly struct SymbolInfo : IEquatable<SymbolInfo>
{
    internal static readonly SymbolInfo None = default;

    /// <summary>
    /// The symbol that was referred to by the syntax node, if any. Returns null if the given expression did not
    /// bind successfully to a single symbol. If null is returned, it may still be that case that we have one or
    /// more "best guesses" as to what symbol was intended. These best guesses are available via the <see
    /// cref="CandidateSymbols"/> property.
    /// </summary>
    public ISymbol? Symbol { get; }

    /// <summary>
    /// If the expression did not successfully resolve to a symbol, but there were one or more symbols that may have
    /// been considered but discarded, this property returns those symbols. The reason that the symbols did not
    /// successfully resolve to a symbol are available in the <see cref="CandidateReason"/> property. For example,
    /// if the symbol was inaccessible, ambiguous, or used in the wrong context.
    /// </summary>
    /// <remarks>Will never return a <see langword="default"/> array.</remarks>
    public ImmutableArray<ISymbol> CandidateSymbols { get; }

    ///<summary>
    /// If the expression did not successfully resolve to a symbol, but there were one or more symbols that may have
    /// been considered but discarded, this property describes why those symbol or symbols were not considered
    /// suitable.
    /// </summary>
    public CandidateReason CandidateReason { get; }

    private SymbolInfo(ISymbol? symbol, ImmutableArray<ISymbol> candidateSymbols, CandidateReason candidateReason)
    {
        this.Symbol = symbol;
        this.CandidateSymbols = candidateSymbols;
        this.CandidateReason = candidateReason;
    }

    internal ImmutableArray<ISymbol> GetAllSymbols() =>
        this.Symbol == null ? this.CandidateSymbols : ImmutableArray.Create(this.Symbol);

    public override bool Equals(object? obj) =>
        obj is SymbolInfo info && Equals(info);

    public bool Equals(SymbolInfo other) =>
        this.CandidateReason == other.CandidateReason &&
        Equals(this.Symbol, other.Symbol) &&
        this.CandidateSymbols.SequenceEqual(other.CandidateSymbols);

    public override int GetHashCode() =>
        Hash.Combine(this.Symbol,
        Hash.Combine(
        Hash.CombineValues(this.CandidateSymbols, 4),
        (int)this.CandidateReason));

    internal bool IsEmpty => this.Symbol == null && this.CandidateSymbols.Length == 0;

    public static explicit operator SymbolInfo(Microsoft.CodeAnalysis.SymbolInfo symbolInfo) => new(
        (ISymbol?)symbolInfo.Symbol,
        symbolInfo.CandidateSymbols.CastDown<ISymbol, Microsoft.CodeAnalysis.ISymbol>(),
        symbolInfo.CandidateReason);
}
