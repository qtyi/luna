// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// 表示最基础的不做任何事的语法引用的实现。
/// </summary>
internal sealed class SimpleSyntaxReference : SyntaxReference
{
    private readonly SyntaxNode _node;

    public override SyntaxTree SyntaxTree => this._node.SyntaxTree;

    public override TextSpan Span => this._node.Span;

    internal SimpleSyntaxReference(SyntaxNode node) => this._node = node;

    public override SyntaxNode GetSyntax(CancellationToken cancellationToken = default) => this._node;
}
