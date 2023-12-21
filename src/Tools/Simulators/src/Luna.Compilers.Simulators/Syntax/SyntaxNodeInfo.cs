// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Simulators.Syntax;

public sealed partial class SyntaxNodeInfo : SyntaxNodeOrTokenInfo
{
    public SyntaxNode Node { get; }
    public readonly IReadOnlyList<SyntaxNodeOrTokenInfo> ChildNodesAndTokens;

    public override bool IsNode => true;
    public override bool IsToken => false;

    private SyntaxNodeInfo(SyntaxNode node)
    {
        this.Node = node;
        this.ChildNodesAndTokens = new SyntaxNodeOrTokenInfoList(node.ChildNodesAndTokens());
    }

    public static implicit operator SyntaxNodeInfo(SyntaxNode node) => new(node);
}
