// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Qtyi.CodeAnalysis.Symbols;

namespace Qtyi.CodeAnalysis.Symbols;

internal interface INamedTypeSymbolInternal : ITypeSymbolInternal,
    Microsoft.CodeAnalysis.Symbols.INamedTypeSymbolInternal
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.Symbols.INamedTypeSymbolInternal.EnumUnderlyingType"/>
    new INamedTypeSymbolInternal? EnumUnderlyingType { get; }
}
