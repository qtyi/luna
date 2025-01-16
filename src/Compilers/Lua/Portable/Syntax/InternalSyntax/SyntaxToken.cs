// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

internal partial class SyntaxToken
{
    internal const SyntaxKind FirstTokenWithWellKnownText = SyntaxKind.PlusToken;
    internal const SyntaxKind LastTokenWithWellKnownText = SyntaxKind.MultiLineCommentTrivia;

    /// <summary>
    /// Gets value of this token.
    /// </summary>
    public virtual partial object? Value => Kind switch
    {
        SyntaxKind.TrueKeyword => Boxes.BoxedTrue,
        SyntaxKind.FalseKeyword => Boxes.BoxedFalse,
        SyntaxKind.NilKeyword => null,
        _ => KindText
    };

    internal static SyntaxToken StringLiteral(string text) => new SyntaxTokenWithValue<string>(SyntaxKind.StringLiteralToken, text, text);

    internal static SyntaxToken StringLiteral(ThisInternalSyntaxNode leading, string text, ThisInternalSyntaxNode trailing) => new SyntaxTokenWithValueAndTrivia<string>(SyntaxKind.StringLiteralToken, text, text, leading, trailing);
}
