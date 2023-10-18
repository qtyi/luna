// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis;

/// <inheritdoc/>
public abstract class SemanticModel : Microsoft.CodeAnalysis.SemanticModel
{
    /// <summary>
    /// Gets the source language.
    /// </summary>
    public abstract override string Language { get; }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.SemanticModel.GetSymbolInfo(SyntaxNode, CancellationToken)"/>
    internal new SymbolInfo GetSymbolInfo(SyntaxNode node, CancellationToken cancellationToken = default)
    {
        return (SymbolInfo)this.GetSymbolInfoCore(node, cancellationToken);
    }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.SemanticModel.GetSpeculativeSymbolInfo(int, SyntaxNode, SpeculativeBindingOption)"/>
    internal new SymbolInfo GetSpeculativeSymbolInfo(int position, SyntaxNode expression, SpeculativeBindingOption bindingOption)
    {
        return (SymbolInfo)this.GetSpeculativeSymbolInfoCore(position, expression, bindingOption);
    }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.SemanticModel.GetTypeInfo(SyntaxNode, CancellationToken)"/>
    internal new TypeInfo GetTypeInfo(SyntaxNode node, CancellationToken cancellationToken = default)
    {
        return (TypeInfo)this.GetTypeInfoCore(node, cancellationToken);
    }

    /// <inheritdoc cref="Microsoft.CodeAnalysis.SemanticModel.GetSpeculativeTypeInfo(int, SyntaxNode, SpeculativeBindingOption)"/>
    internal new TypeInfo GetSpeculativeTypeInfo(int position, SyntaxNode expression, SpeculativeBindingOption bindingOption)
    {
        return (TypeInfo)this.GetSpeculativeTypeInfoCore(position, expression, bindingOption);
    }

    /// <summary>
    /// Gets the symbol associated with a declaration syntax node.
    /// </summary>
    /// <param name="declaration">A syntax node that is a declaration.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The symbol declared by the node or null if the node is not a declaration.</returns>
    internal new ISymbol? GetDeclaredSymbolForNode(SyntaxNode declaration, CancellationToken cancellationToken = default)
    {
        return (ISymbol?)this.GetDeclaredSymbolCore(declaration, cancellationToken);
    }

    /// <summary>
    /// Gets the symbol associated with a declaration syntax node. Unlike <see cref="GetDeclaredSymbolForNode(SyntaxNode, CancellationToken)"/>,
    /// this method returns all symbols declared by a given declaration syntax node.
    /// </summary>
    /// <param name="declaration">A syntax node that is a declaration.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The symbols declared by the node.</returns>
    internal new ImmutableArray<ISymbol> GetDeclaredSymbolsForNode(SyntaxNode declaration, CancellationToken cancellationToken = default)
    {
        return this.GetDeclaredSymbolsCore(declaration, cancellationToken).CastDown<ISymbol, Microsoft.CodeAnalysis.ISymbol>();
    }

    /// <summary>
    /// Gets the available named symbols in the context of the specified location and optional container. Only
    /// symbols that are accessible and visible from the given location are returned.
    /// </summary>
    /// <param name="position">The character position for determining the enclosing declaration scope and
    /// accessibility.</param>
    /// <param name="container">The container to search for symbols within. If null then the enclosing declaration
    /// scope around position is used.</param>
    /// <param name="name">The name of the symbol to find. If null is specified then symbols
    /// with any names are returned.</param>
    /// <param name="includeReducedExtensionMethods">Consider (reduced) extension methods.</param>
    /// <returns>A list of symbols that were found. If no symbols were found, an empty list is returned.</returns>
    /// <remarks>
    /// The "position" is used to determine what variables are visible and accessible. Even if "container" is
    /// specified, the "position" location is significant for determining which members of "containing" are
    /// accessible.
    ///
    /// Labels are not considered (see <see cref="LookupLabels"/>).
    ///
    /// Non-reduced extension methods are considered regardless of the value of <paramref name="includeReducedExtensionMethods"/>.
    /// </remarks>
    public ImmutableArray<ISymbol> LookupSymbols(
        int position,
        IModuleSymbol? container = null,
        string? name = null,
        bool includeReducedExtensionMethods = false)
    {
        return LookupSymbolsCore(position, container, name, includeReducedExtensionMethods);
    }

    /// <summary>
    /// Backing implementation of <see cref="LookupSymbols(int, IModuleSymbol?, string?, bool)"/>.
    /// </summary>
    protected abstract ImmutableArray<ISymbol> LookupSymbolsCore(
        int position,
        IModuleSymbol? container,
        string? name,
        bool includeReducedExtensionMethods);

    /// <inheritdoc/>
    protected override ImmutableArray<Microsoft.CodeAnalysis.ISymbol> LookupSymbolsCore(int position, INamespaceOrTypeSymbol? container, string? name, bool includeReducedExtensionMethods)
    {
        Debug.Assert(container is null or IModuleSymbol);
        return this.LookupSymbolsCore(position, (IModuleSymbol?)container, name, includeReducedExtensionMethods).Cast<ISymbol, Microsoft.CodeAnalysis.ISymbol>();
    }

    /// <inheritdoc/>
    protected override ImmutableArray<Microsoft.CodeAnalysis.ISymbol> LookupNamespacesAndTypesCore(int position, INamespaceOrTypeSymbol? container, string? name)
    {
        Debug.Assert(container is null or IModuleSymbol);
        return this.LookupModulesCore(position, (IModuleSymbol?)container, name)
            .WhereAsArray(static symbol => symbol is INamespaceOrTypeSymbol)
            .Cast<ISymbol, Microsoft.CodeAnalysis.ISymbol>();
    }

    /// <summary>
    /// Gets the available named module symbols in the context of the specified location and optional container.
    /// Only members that are accessible and visible from the given location are returned.
    /// </summary>
    /// <param name="position">The character position for determining the enclosing declaration scope and
    /// accessibility.</param>
    /// <param name="container">The container to search for symbols within. If null then the enclosing declaration
    /// scope around position is used.</param>
    /// <param name="name">The name of the symbol to find. If null is specified then symbols
    /// with any names are returned.</param>
    /// <returns>A list of symbols that were found. If no symbols were found, an empty list is returned.</returns>
    /// <remarks>
    /// The "position" is used to determine what variables are visible and accessible. Even if "container" is
    /// specified, the "position" location is significant for determining which members of "containing" are
    /// accessible.
    /// </remarks>
    public ImmutableArray<ISymbol> LookupModules(
        int position,
        IModuleSymbol? container = null,
        string? name = null)
    {
        return this.LookupModulesCore(position, container, name);
    }

    /// <summary>
    /// Backing implementation of <see cref="LookupModules(int, IModuleSymbol?, string?)"/>.
    /// </summary>
    protected abstract ImmutableArray<ISymbol> LookupModulesCore(
        int position,
        IModuleSymbol? container,
        string? name);

    /// <summary>
    /// Gets the available named label symbols in the context of the specified location and optional container.
    /// Only members that are accessible and visible from the given location are returned.
    /// </summary>
    /// <param name="position">The character position for determining the enclosing declaration scope and
    /// accessibility.</param>
    /// <param name="name">The name of the symbol to find. If null is specified then symbols
    /// with any names are returned.</param>
    /// <returns>A list of symbols that were found. If no symbols were found, an empty list is returned.</returns>
    /// <remarks>
    /// The "position" is used to determine what variables are visible and accessible. Even if "container" is
    /// specified, the "position" location is significant for determining which members of "containing" are
    /// accessible.
    /// </remarks>
    public new ImmutableArray<ISymbol> LookupLabels(
        int position,
        string? name = null)
    {
        return this.LookupLabelsCore(position, name).CastDown<ISymbol, Microsoft.CodeAnalysis.ISymbol>();
    }
}
