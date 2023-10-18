// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Symbols;

internal interface ISymbolInternal :
    Microsoft.CodeAnalysis.Symbols.ISymbolInternal
{
    /// <summary>
    /// Gets the <see cref="SymbolKind"/> indicating what kind of symbol it is.
    /// </summary>
    new SymbolKind Kind { get; }

    /// <summary>
    /// Allows a symbol to support comparisons that involve child type symbols
    /// </summary>
    /// <remarks>
    /// Because TypeSymbol equality can differ based on e.g. nullability, any symbols that contain TypeSymbols can also differ in the same way
    /// This call allows the symbol to accept a comparison kind that should be used when comparing its contained types
    /// </remarks>
    bool Equals(ISymbolInternal? other, Microsoft.CodeAnalysis.TypeCompareKind compareKind);

    /// <summary>
    /// Gets the <see cref="ISymbolInternal"/> for the immediately containing symbol.
    /// </summary>
    new ISymbolInternal? ContainingSymbol { get; }

    /// <summary>
    /// Gets the <see cref="IAssemblySymbolInternal"/> for the containing assembly. Returns null if the
    /// symbol is shared across multiple assemblies.
    /// </summary>
    new IAssemblySymbolInternal? ContainingAssembly { get; }

    /// <summary>
    /// Gets the <see cref="INetmoduleSymbolInternal"/> for the containing module. Returns null if the
    /// symbol is shared across multiple modules.
    /// </summary>
    INetmoduleSymbolInternal? ContainingNetmodule { get; }

    /// <summary>
    /// Gets the <see cref="IModuleSymbolInternal"/> for the containing module. Returns null if the
    /// symbol is shared across multiple modules.
    /// </summary>
    new IModuleSymbolInternal? ContainingModule { get; }

    /// <summary>
    /// Gets the <see cref="INamedTypeSymbolInternal"/> for the containing type. Returns null if the
    /// symbol is not contained within a type.
    /// </summary>
    new INamedTypeSymbolInternal? ContainingType { get; }

    /// <summary>
    /// Returns an <see cref="ISymbol"/> instance associated with this symbol.
    /// </summary>
    new ISymbol GetISymbol();
}
