﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    private partial bool ScanStringLiteral(ref TokenInfo info)
    {
        var quote = this.TextWindow.NextChar();
        Debug.Assert(quote == '\'' || quote == '"');

        this._builder.Clear();

        while (true)
        {
            var c = this.TextWindow.PeekChar();
            if (c == '\\') // 转义字符前缀
                this.ScanEscapeSequence();
            else if (c == quote) // 字符串结尾
            {
                this.TextWindow.AdvanceChar();
                break;
            }
            // 字符串中可能包含非正规的UTF-16以外的字符，检查是否真正到达文本结尾来验证这些字符不是由用户代码引入的情况。
            else if (SyntaxFacts.IsNewLine(c) ||
                (c == SlidingTextWindow.InvalidCharacter && this.TextWindow.IsReallyAtEnd())
            )
            {
                Debug.Assert(this.TextWindow.Width > 0);
                this.AddError(ErrorCode.ERR_NewlineInConst);
                break;
            }
            else // 普通字符
            {
                this.TextWindow.AdvanceChar();
                this._builder.Append(c);
            }
        }

        info.Kind = SyntaxKind.StringLiteralToken;
        info.Text = this.TextWindow.GetText(intern: true);

        this.FlushToUtf8Builder();
        info.Utf8StringValue = this._utf8Builder.ToImmutableAndClear();

        return true;
    }
}
