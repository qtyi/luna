// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    private partial void ScanMultiLineRawStringLiteral(ref TokenInfo info, int level)
    {
        bool closed;
        if (IsLeveledMultiLineRawStringLiteralAvailable())
            ScanLongBrackets(out closed, level);
        else
            ScanShortBrackets(out closed, topmost: true);

        info.Kind = SyntaxKind.MultiLineRawStringLiteralToken;
        info.Text = TextWindow.GetText(intern: true);
        _utf8Builder.Clear();
        FlushToUtf8Builder();
        info.Utf8StringValue = new(_utf8Builder.ToArrayAndFree());

        if (!closed)
            AddError(ErrorCode.ERR_UnterminatedStringLiteral);
    }

    /// <summary>
    /// Scans a paired short brackets (<c>[[ ]]</c>) (for string literal and comment).
    /// </summary>
    /// <param name="closed">Sets to <see langword="true"/> if the brackets closed correctly; otherwise, <see langword="false"/>.</param>
    /// <param name="topmost">Indicates whether we are scanning the topmost brackets or the inner brackets.</param>
    private void ScanShortBrackets(out bool closed, bool topmost = true)
    {
        Debug.Assert(TextWindow.PeekChars(2) == "[[");

        // Clear buffer to initialize.
        if (topmost)
            _builder.Clear();

        // Skip or add open brackets.
        if (!topmost)
            _builder.Append("[[");
        TextWindow.AdvanceChar(2);

        // Ignores the EOL directly after open brackets if supported.
        if (SyntaxFacts.IsNewLine(TextWindow.PeekChar()) && IgnoreNewLineDirectlyAfterOpenBrackets())
            TextWindow.AdvancePastNewLine();

        while (true)
        {
            var c = TextWindow.PeekChar();

            // At EOF.
            if (c == SlidingTextWindow.InvalidCharacter && TextWindow.IsReallyAtEnd())
            {
                closed = false;
                break;
            }
            // Meet inner short brackets.
            else if (c == '[' && TextWindow.PeekChar(1) == '[')
            {
                ScanShortBrackets(out var innerClosed, topmost: false);
                if (!innerClosed)
                {
                    Debug.Assert(c == SlidingTextWindow.InvalidCharacter && TextWindow.IsReallyAtEnd());
                    closed = false;
                    break;
                }
            }
            // Meet close short brackets.
            else if (c == ']' && TextWindow.PeekChar(1) == ']')
            {
                if (!topmost)
                    _builder.Append("]]");
                TextWindow.AdvanceChar(2);
                closed = true;
                break;
            }
            // Append LF if we meet EOL.
            else if (SyntaxFacts.IsNewLine(c))
            {
                _builder.Append('\n');
                TextWindow.AdvancePastNewLine();
            }
            // Normal characters.
            else
            {
                var cs = TextWindow.SingleOrSurrogatePair(out var error);
                if (error is not null)
                    AddError(error);
                _builder.Append(new string(cs));
            }
        }
    }

    private partial bool IgnoreNewLineDirectlyAfterOpenBrackets() => Options.LanguageVersion >= LanguageVersion.Lua5;
}
