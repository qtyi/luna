// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents a type other than an array, a pointer, a type parameter.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
public interface INamedTypeSymbol : ITypeSymbol,
    Microsoft.CodeAnalysis.INamedTypeSymbol
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.TypeArguments"/>
    new ImmutableArray<ITypeSymbol> TypeArguments { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.OriginalDefinition"/>
    new INamedTypeSymbol OriginalDefinition { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.DelegateInvokeMethod"/>
    new IModuleSymbol? DelegateInvokeMethod { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.EnumUnderlyingType"/>
    new INamedTypeSymbol? EnumUnderlyingType { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.ConstructedFrom"/>
    new INamedTypeSymbol ConstructedFrom { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.Construct(Microsoft.CodeAnalysis.ITypeSymbol[])"/>
    INamedTypeSymbol Construct(params ITypeSymbol[] typeArguments);

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.Construct(ImmutableArray{Microsoft.CodeAnalysis.ITypeSymbol}, ImmutableArray{Microsoft.CodeAnalysis.NullableAnnotation})"/>
    INamedTypeSymbol Construct(ImmutableArray<ITypeSymbol> typeArguments, ImmutableArray<Microsoft.CodeAnalysis.NullableAnnotation> typeArgumentNullableAnnotations);

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.ConstructUnboundGenericType"/>
    new INamedTypeSymbol ConstructUnboundGenericType();

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.InstanceConstructors"/>
    new ImmutableArray<IModuleSymbol> InstanceConstructors { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.StaticConstructors"/>
    new ImmutableArray<IModuleSymbol> StaticConstructors { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.Constructors"/>
    new ImmutableArray<IModuleSymbol> Constructors { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.AssociatedSymbol"/>
    new ISymbol? AssociatedSymbol { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.TupleUnderlyingType"/>
    new INamedTypeSymbol? TupleUnderlyingType { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.TupleElements"/>
    new ImmutableArray<IModuleSymbol> TupleElements { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamedTypeSymbol.NativeIntegerUnderlyingType"/>
    new INamedTypeSymbol? NativeIntegerUnderlyingType { get; }
}
