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

        _builder.Clear();
        _utf8Builder.Clear();

        while (true)
        {
            var c = TextWindow.PeekChar();
            // The start of escape sequence
            if (c == '\\')
                ScanEscapeSequence();
            // Close quote that indicates the end of string literal
            else if (c == quote)
            {
                TextWindow.AdvanceChar();
                break;
            }
            // String and character literals can contain any Unicode character.
            // They are not limited to valid UTF-16 characters.
            // So if we get the SlidingTextWindow's sentinel value, double check that it was not real user-code contents.
            // This will be rare.
            else if (SyntaxFacts.IsNewLine(c) ||
                (c == SlidingTextWindow.InvalidCharacter && TextWindow.IsReallyAtEnd())
            )
            {
                Debug.Assert(TextWindow.Width > 0);
                AddError(ErrorCode.ERR_NewlineInConst);
                break;
            }
            // Normal UTF-16 characters.
            // We should check surrogate pair since we do not want to break encoding process to UTF-8.
            else
            {
                var cs = TextWindow.SingleOrSurrogatePair(out var error);
                if (error is not null)
                    AddError(error);
                _builder.Append(new string(cs));
                FlushToUtf8Builder();
            }
        }

        info.Kind = SyntaxKind.StringLiteralToken;
        info.Text = TextWindow.GetText(intern: true);
        info.Utf8StringValue = new(_utf8Builder.ToArrayAndFree());

        return true;
    }
}
