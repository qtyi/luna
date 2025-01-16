// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    private partial bool ScanStringLiteral(ref TokenInfo info)
    {
        var quote = TextWindow.NextChar();
        Debug.Assert(quote == '\'' || quote == '"');

        _utf8Builder.Clear();

        while (true)
        {
            var c = TextWindow.PeekChar();
            if (c == '\\') // 转义字符前缀
                ScanEscapeSequence();
            else if (c == quote) // 字符串结尾
            {
                TextWindow.AdvanceChar();
                break;
            }
            // 字符串中可能包含非正规的UTF-16以外的字符，检查是否真正到达文本结尾来验证这些字符不是由用户代码引入的情况。
            else if (SyntaxFacts.IsNewLine(c) ||
                (c == SlidingTextWindow.InvalidCharacter && TextWindow.IsReallyAtEnd())
            )
            {
                Debug.Assert(TextWindow.Width > 0);
                AddError(ErrorCode.ERR_NewlineInConst);
                break;
            }
            else // 普通字符
            {
#warning Should check character surrogate pair.
                TextWindow.AdvanceChar();
                _builder.Append(c);
            }
        }

        info.Kind = SyntaxKind.StringLiteralToken;
        info.Text = TextWindow.GetText(intern: true);

        FlushToUtf8Builder();
        info.Utf8StringValue = new(_utf8Builder.ToArrayAndFree());

        return true;
    }
}
