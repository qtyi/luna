// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents a parameter of a method or property.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
public interface IParameterSymbol : ISymbol,
    Microsoft.CodeAnalysis.IParameterSymbol
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.IParameterSymbol.Type"/>
    new ITypeSymbol Type { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.IParameterSymbol.OriginalDefinition"/>
    new IParameterSymbol OriginalDefinition { get; }
}
