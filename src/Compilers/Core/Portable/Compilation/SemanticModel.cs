// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis;

/// <inheritdoc/>
public abstract class SemanticModel : Microsoft.CodeAnalysis.SemanticModel
{
    /// <summary>
    /// Gets the source language.
    /// </summary>
    /// <inheritdoc/>
    public abstract override string Language { get; }

    protected sealed override IAliasSymbol? GetAliasInfoCore(SyntaxNode nameSyntax, CancellationToken cancellationToken = default) => null;

    protected sealed override IAliasSymbol? GetSpeculativeAliasInfoCore(int position, SyntaxNode nameSyntax, SpeculativeBindingOption bindingOption) => null;
}
