// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents a symbol (module, type, field, parameter, etc.)
/// exposed by the compiler.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
[InternalImplementationOnly]
public interface ISymbol : IEquatable<ISymbol?>,
    Microsoft.CodeAnalysis.ISymbol
{
    new SymbolKind Kind { get; }

    new ISymbol? ContainingSymbol { get; }

    new IAssemblySymbol? ContainingAssembly { get; }

    INetmoduleSymbol? ContainingNetModule { get; }

    new IModuleSymbol? ContainingModule { get; }

    new INamedTypeSymbol? ContainingType { get; }

    new ISymbol OriginalDefinition { get; }

    void Accept(SymbolVisitor visitor);

    TResult? Accept<TResult>(SymbolVisitor<TResult> visitor);

    TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument);

    string ToMinimalDisplayString(SemanticModel semanticModel, int position, SymbolDisplayFormat? format = null);

    ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(SemanticModel semanticModel, int position, SymbolDisplayFormat? format = null);

    bool Equals([NotNullWhen(true)] ISymbol? other, SymbolEqualityComparer equalityComparer);
}
