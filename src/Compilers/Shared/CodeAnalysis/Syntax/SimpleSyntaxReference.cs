// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// This is a basic do-nothing implementation of a syntax reference
/// </summary>
internal sealed class SimpleSyntaxReference : SyntaxReference
{
    /// <summary>The syntax node referenced.</summary>
    private readonly SyntaxNode _node;

    /// <inheritdoc/>
    public override SyntaxTree SyntaxTree => this._node.SyntaxTree;

    /// <inheritdoc/>
    public override TextSpan Span => this._node.Span;

    /// <summary>
    /// Initializes a new instance of the <see cref="SimpleSyntaxReference"/> class。
    /// </summary>
    /// <param name="node">The syntax node to be referenced.</param>
    internal SimpleSyntaxReference(SyntaxNode node) => this._node = node;

    /// <summary>
    /// Retrieves the original referenced syntax node.
    /// </summary>
    /// <param name="cancellationToken">The token to monitor for cancellation requests. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>The original referenced syntax node, that is, the one passed into constructor.</returns>
    /// <remarks>
    /// As the method only returns the syntax node passed into constructor, any value to <paramref name="cancellationToken"/> will be ignored.
    /// </remarks>
    public override SyntaxNode GetSyntax(CancellationToken cancellationToken = default) => this._node;
}
