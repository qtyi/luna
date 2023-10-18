// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents a type.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
public interface ITypeSymbol : IModuleSymbol
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.ITypeSymbol.TypeKind"/>
    new TypeKind TypeKind { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.ITypeSymbol.BaseType"/>
    new INamedTypeSymbol? BaseType { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.ITypeSymbol.Interfaces"/>
    new ImmutableArray<INamedTypeSymbol> Interfaces { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.ITypeSymbol.AllInterfaces"/>
    new ImmutableArray<INamedTypeSymbol> AllInterfaces { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.ITypeSymbol.OriginalDefinition"/>
    new ITypeSymbol OriginalDefinition { get; }

    new Microsoft.CodeAnalysis.NullableAnnotation NullableAnnotation { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.ITypeSymbol.FindImplementationForInterfaceMember(Microsoft.CodeAnalysis.ISymbol)"/>
    ISymbol? FindImplementationForInterfaceMember(ISymbol interfaceMember);

    /// <inheritdoc cref="Microsoft.CodeAnalysis.ITypeSymbol.WithNullableAnnotation(Microsoft.CodeAnalysis.NullableAnnotation)"/>
    new ITypeSymbol WithNullableAnnotation(Microsoft.CodeAnalysis.NullableAnnotation nullableAnnotation);
}
