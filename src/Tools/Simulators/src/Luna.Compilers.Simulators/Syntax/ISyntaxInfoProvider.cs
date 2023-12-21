// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Luna.Compilers.Simulators.Syntax;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Simulators;

public interface ISyntaxInfoProvider
{
    SyntaxNodeInfo Root { get; }

    bool TryGetSyntaxTokenInfo(SyntaxToken token, [NotNullWhen(true)] out SyntaxTokenInfo? info);

    bool TryGetSyntaxTriviaInfo(SyntaxTrivia trivia, [NotNullWhen(true)] out SyntaxTriviaInfo? info);

    bool TryGetSyntaxNodeInfo(SyntaxNode node, [NotNullWhen(true)] out SyntaxNodeInfo? info);
}
