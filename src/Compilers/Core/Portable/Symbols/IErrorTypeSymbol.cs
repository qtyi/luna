// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis;

public interface IErrorTypeSymbol : INamedTypeSymbol,
    Microsoft.CodeAnalysis.IErrorTypeSymbol
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.IErrorTypeSymbol.CandidateSymbols"/>
    new ImmutableArray<ISymbol> CandidateSymbols { get; }
}
