// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

internal partial class SyntaxToken
{
    internal const SyntaxKind FirstTokenWithWellKnownText = SyntaxKind.PlusToken;
    internal const SyntaxKind LastTokenWithWellKnownText = SyntaxKind.MultiLineCommentTrivia;

    /// <summary>
    /// Gets value of this token.
    /// </summary>
    /// <value>
    /// <list type="bullet">
    /// <item>
    /// <term>Keyword <c>true</c></term>
    /// <description><see langword="true"/>.</description>
    /// </item>
    /// <item>
    /// <term>Keyword <c>false</c></term>
    /// <description><see langword="false"/>.</description>
    /// </item>
    /// <item>
    /// <term>Keyword <c>nil</c></term>
    /// <description><see langword="null"/>.</description>
    /// </item>
    /// <item>
    /// <term>Others</term>
    /// <description>A string the same as <see cref="ThisInternalSyntaxNode.KindText"/>.</description>
    /// </item>
    /// </list>
    /// </value>
    public virtual partial object? Value => Kind switch
    {
        SyntaxKind.TrueKeyword => Boxes.BoxedTrue,
        SyntaxKind.FalseKeyword => Boxes.BoxedFalse,
        SyntaxKind.NilKeyword => null,
        _ => KindText
    };

    /// <summary>
    /// 获取此语法标记的空白缩进量。
    /// </summary>
    public virtual int GetWhiteSpaceIndent()
    {
        var indent = 0;
        var isContinuedWhiteSpace = true;
        foreach (var node in LeadingTrivia)
        {
            Debug.Assert(node is SyntaxTrivia);
            var trivia = (SyntaxTrivia)node;
            if (trivia.IsTriviaWithEndOfLine())
            {
                indent = 0;
                isContinuedWhiteSpace = true;
            }
            else if (trivia.IsWhitespace)
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

    internal static SyntaxToken IndentedWithValue<T>(SyntaxKind kind, string text, T value, int innerIndent) where T : notnull
        => new IndentedSyntaxTokenWithValue<T>(kind, text, value, innerIndent);

    internal static SyntaxToken IndentedWithValue<T>(SyntaxKind kind, GreenNode? leading, string text, T value, int innerIndent, GreenNode? trailing) where T : notnull
        => new IndentedSyntaxTokenWithValueAndTrivia<T>(kind, text, value, innerIndent, leading, trailing);

    internal static SyntaxToken StringLiteral(string text, int innerIndent) => new IndentedSyntaxTokenWithValue<string>(SyntaxKind.StringLiteralToken, text, text, innerIndent);

    internal static SyntaxToken StringLiteral(MoonScriptSyntaxNode leading, string text, int innerIndent, MoonScriptSyntaxNode trailing) => new IndentedSyntaxTokenWithValueAndTrivia<string>(SyntaxKind.StringLiteralToken, text, text, innerIndent, leading, trailing);

    internal static SyntaxToken InterpolatedStringLiteral(string text, ImmutableArray<SyntaxToken> tokens, int innerIndent) => new IndentedSyntaxTokenWithValue<ImmutableArray<SyntaxToken>>(SyntaxKind.InterpolatedStringLiteralToken, text, tokens, innerIndent);

    internal static SyntaxToken InterpolatedStringLiteral
        (MoonScriptSyntaxNode leading, string text, ImmutableArray<SyntaxToken> tokens, int innerIndent, MoonScriptSyntaxNode trailing) => new IndentedSyntaxTokenWithValueAndTrivia<ImmutableArray<SyntaxToken>>(SyntaxKind.InterpolatedStringLiteralToken, text, tokens, innerIndent, leading, trailing);
}
