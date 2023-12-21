// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

internal static partial class SyntaxFactory
{
    internal static SyntaxToken Literal(GreenNode? leading, string text, string value, int innerIndent, GreenNode? trailing) => SyntaxToken.IndentedWithValue(SyntaxKind.StringLiteralToken, leading, text, value, innerIndent, trailing);

    internal static SyntaxToken Literal(GreenNode? leading, string text, SyntaxKind kind, string value, int innerIndent, GreenNode? trailing) => SyntaxToken.IndentedWithValue(kind, leading, text, value, innerIndent, trailing);

    internal static SyntaxToken Literal(GreenNode? leading, string text, ImmutableArray<SyntaxToken> value, int innerIndent, GreenNode? trailing) => SyntaxToken.IndentedWithValue(SyntaxKind.InterpolatedStringLiteralToken, leading, text, value, innerIndent, trailing);

    private static partial void ValidateTokenKind(SyntaxKind kind)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind));
        Debug.Assert(kind != SyntaxKind.IdentifierToken);
        Debug.Assert(kind != SyntaxKind.NumericLiteralToken);
    }

    internal static partial IEnumerable<SyntaxTrivia> GetWellKnownTrivia()
    {
        yield return CarriageReturnLineFeed;
        yield return LineFeed;
        yield return CarriageReturn;
        yield return Space;
        yield return Tab;

        yield return ElasticCarriageReturnLineFeed;
        yield return ElasticLineFeed;
        yield return ElasticCarriageReturn;
        yield return ElasticSpace;
        yield return ElasticTab;

        yield return ElasticZeroSpace;
    }
}
