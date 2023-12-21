// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Simulators.Syntax;

public abstract partial class SyntaxNodeOrTokenInfo : SyntaxNodeOrTokenOrTriviaInfo
{
    public sealed override bool IsTrivia => false;

    public static implicit operator SyntaxNodeOrTokenInfo(SyntaxNodeOrToken nodeOrToken) => nodeOrToken.IsNode ? (SyntaxNodeInfo)nodeOrToken.AsNode()! : (SyntaxTokenInfo)nodeOrToken.AsToken();
}
