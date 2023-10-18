// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents a field in a .NET class, struct or enum.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
public interface IFieldSymbol : IModuleSymbol
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.IFieldSymbol.AssociatedSymbol"/>
    new ISymbol? AssociatedSymbol { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.IFieldSymbol.Type"/>
    new ITypeSymbol Type { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.IFieldSymbol.OriginalDefinition"/>
    new IFieldSymbol OriginalDefinition { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.IFieldSymbol.CorrespondingTupleField"/>
    new IFieldSymbol? CorrespondingTupleField { get; }
}
