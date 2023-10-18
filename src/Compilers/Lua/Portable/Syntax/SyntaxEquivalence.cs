// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua;

using InternalSyntax = Syntax.InternalSyntax;

partial class SyntaxEquivalence
{
    private static partial bool AreTokensEquivalentCore(GreenNode before, GreenNode after, SyntaxKind kind) =>
        kind switch
        {
            SyntaxKind.IdentifierToken => ((InternalSyntax.SyntaxToken)before).ValueText == ((InternalSyntax.SyntaxToken)after).ValueText,

            SyntaxKind.NumericLiteralToken or
            SyntaxKind.StringLiteralToken or
            SyntaxKind.MultiLineRawStringLiteralToken => ((InternalSyntax.SyntaxToken)before).Text == ((InternalSyntax.SyntaxToken)after).Text,

            _ => true
        };

    private static partial bool TryAreTopLevelEquivalent(GreenNode before, GreenNode after, SyntaxKind kind, ref Func<SyntaxKind, bool>? ignoreChildNode, out bool equivalence)
    {
        equivalence = false;
        return false;
    }
}
