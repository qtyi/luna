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
