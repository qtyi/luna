// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Symbols;

internal interface ITypeSymbolInternal : IModuleSymbolInternal
{
    /// <inheritdoc cref="Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.TypeKind"/>
    new TypeKind TypeKind { get; }

    /// <summary>
    /// Returns an <see cref="ITypeSymbol"/> instance associated with this symbol.
    /// This API and <see cref="ISymbolInternal.GetISymbol"/> should return the same object.
    /// </summary>
    new ITypeSymbol GetITypeSymbol();
}
