// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

internal partial class SyntaxToken
{
    /// <summary>
    /// 获取此语法标记的空白缩进量。
    /// </summary>
    public virtual int GetWhiteSpaceIndent()
    {
        var indent = 0;
        var isContinuedWhiteSpace = true;
        foreach (var node in this.LeadingTrivia)
        {
            Debug.Assert(node is SyntaxTrivia);
            var trivia = (SyntaxTrivia)node;
            if (trivia.IsTriviaWithEndOfLine())
            {
                indent = 0;
                isContinuedWhiteSpace = true;
            }
            else if (trivia.IsWhiteSpace)
            {
                // 在语法琐碎列表的较前位置中遇到了非空白语法琐碎，不继续累加缩进量。
                if (!isContinuedWhiteSpace) continue;

                indent += trivia.WhiteSpaceIndent;
            }
            else
            {
                isContinuedWhiteSpace = false;
            }
        }
        return indent;
    }

    public virtual bool TryGetInnerWhiteSpaceIndent(out int indent)
    {
        indent = 0;
        return false;
    }

    internal static SyntaxToken IndentedWithValue<T>(SyntaxKind kind, string text, T? value, int innerIndent) => new IndentedSyntaxTokenWithValue<T>(kind, text, value, innerIndent);

    internal static SyntaxToken IndentedWithValue<T>(SyntaxKind kind, GreenNode? leading, string text, T? value, int innerIndent, GreenNode? trailing) => new IndentedSyntaxTokenWithValueAndTrivia<T>(kind, text, value, innerIndent, leading, trailing);

    internal static SyntaxToken StringLiteral(string text, int innerIndent) => new IndentedSyntaxTokenWithValue<string>(SyntaxKind.StringLiteralToken, text, text, innerIndent);

    internal static SyntaxToken StringLiteral(MoonScriptSyntaxNode leading, string text, int innerIndent, MoonScriptSyntaxNode trailing) => new IndentedSyntaxTokenWithValueAndTrivia<string>(SyntaxKind.StringLiteralToken, text, text, innerIndent, leading, trailing);

    internal static SyntaxToken InterpolatedStringLiteral(string text, ImmutableArray<SyntaxToken> tokens, int innerIndent) => new IndentedSyntaxTokenWithValue<ImmutableArray<SyntaxToken>>(SyntaxKind.InterpolatedStringLiteralToken, text, tokens, innerIndent);

    internal static SyntaxToken InterpolatedStringLiteral
        (MoonScriptSyntaxNode leading, string text, ImmutableArray<SyntaxToken> tokens, int innerIndent, MoonScriptSyntaxNode trailing) => new IndentedSyntaxTokenWithValueAndTrivia<ImmutableArray<SyntaxToken>>(SyntaxKind.InterpolatedStringLiteralToken, text, tokens, innerIndent, leading, trailing);

    internal const SyntaxKind FirstTokenWithWellKnownText = SyntaxKind.PlusToken;
    internal const SyntaxKind LastTokenWithWellKnownText = SyntaxKind.MultiLineCommentTrivia;
}
