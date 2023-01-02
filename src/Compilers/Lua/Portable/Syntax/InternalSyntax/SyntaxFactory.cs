// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

internal static partial class SyntaxFactory
{
    private static partial void ValidateTokenKind(SyntaxKind kind)
    {
        Debug.Assert(SyntaxFacts.IsAnyToken(kind));
        Debug.Assert(kind != SyntaxKind.IdentifierToken);
        Debug.Assert(kind != SyntaxKind.NumericLiteralToken);
    }

    internal static partial IEnumerable<SyntaxTrivia> GetWellKnownTrivia()
    {
        yield return SyntaxFactory.CarriageReturnLineFeed;
        yield return SyntaxFactory.LineFeed;
        yield return SyntaxFactory.CarriageReturn;
        yield return SyntaxFactory.Space;
        yield return SyntaxFactory.Tab;

        yield return SyntaxFactory.ElasticCarriageReturnLineFeed;
        yield return SyntaxFactory.ElasticLineFeed;
        yield return SyntaxFactory.ElasticCarriageReturn;
        yield return SyntaxFactory.ElasticSpace;
        yield return SyntaxFactory.ElasticTab;

        yield return SyntaxFactory.ElasticZeroSpace;
    }
}
