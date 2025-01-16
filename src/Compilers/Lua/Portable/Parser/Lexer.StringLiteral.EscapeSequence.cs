// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    /// <summary>
    /// Scans an escape sequence to UTF-8 buffer.
    /// </summary>
    private void ScanEscapeSequence()
    {
        Debug.Assert(TextWindow.PeekChar() == '\\');

        var start = TextWindow.Position;

        SyntaxDiagnosticInfo? error;

        var c = TextWindow.PeekChar(1);
        switch (c)
        {
            // Represents itself
            case '\'':
            case '"':
            case '\\':
                if (Options.LanguageVersion < LanguageVersion.Lua3_1)
                    goto default;
                _utf8Builder.Add((byte)c);
                TextWindow.AdvanceChar(2);
                break;
            case '[':
            case ']':
                if (Options.LanguageVersion != LanguageVersion.Lua5)
                    goto default;
                goto case '\'';

            // Well-known controls
            case 'a':
                if (Options.LanguageVersion < LanguageVersion.Lua3_1)
                    goto default;
                _utf8Builder.Add((byte)'\a');
                TextWindow.AdvanceChar(2);
                break;
            case 'b':
                if (Options.LanguageVersion < LanguageVersion.Lua3_1)
                    goto default;
                _utf8Builder.Add((byte)'\b');
                TextWindow.AdvanceChar(2);
                break;
            case 'f':
                if (Options.LanguageVersion < LanguageVersion.Lua3_1)
                    goto default;
                _utf8Builder.Add((byte)'\f');
                TextWindow.AdvanceChar(2);
                break;
            case 'n':
                _utf8Builder.Add((byte)'\n');
                TextWindow.AdvanceChar(2);
                break;
            case 'r':
                _utf8Builder.Add((byte)'\r');
                TextWindow.AdvanceChar(2);
                break;
            case 't':
                _utf8Builder.Add((byte)'\t');
                TextWindow.AdvanceChar(2);
                break;
            case 'v':
                if (Options.LanguageVersion < LanguageVersion.Lua3_1)
                    goto default;
                _utf8Builder.Add((byte)'\v');
                TextWindow.AdvanceChar(2);
                break;

            // Up to three decimal digits that represents a byte
            case '0':
            case '1':
            case '2':
            case '3':
            case '4':
            case '5':
            case '6':
            case '7':
            case '8':
            case '9':
                if (Options.LanguageVersion < LanguageVersion.Lua3_1)
                    goto default;
                {
                    var b = TextWindow.NextByteEscape(out error);
                    if (error is null)
                        _utf8Builder.Add(b);
                    else
                        TextWindow.Reset(start + 1); // reset to the character after '/'
                    AddError(error);
                }
                break;

            // Two hexadecimal digits that represents a byte
            case 'x':
                if (Options.LanguageVersion < LanguageVersion.Lua5_2)
                    goto default;
                {
                    var b = TextWindow.NextByteEscape(out error);
                    if (error is null)
                        _utf8Builder.Add(b);
                    else
                        TextWindow.Reset(start + 1); // reset to the character after '/'
                    AddError(error);
                }
                break;

            // Hexadecimal digits that represents a unicode
            case 'u':
                if (Options.LanguageVersion < LanguageVersion.Lua5_3)
                    goto default;
                {
                    var bs = TextWindow.NextUnicodeEscape(out error);
                    if (error is null)
                        _utf8Builder.AddRange(bs);
                    else
                        TextWindow.Reset(start + 1); // reset to the character after '/'
                    AddError(error);
                }
                break;

            // Ignores whitespaces and new-lines directly after
            case 'z':
                if (Options.LanguageVersion < LanguageVersion.Lua5_2)
                    goto default;
                TextWindow.AdvanceChar(2);
                c = TextWindow.PeekChar();
                while (SyntaxFacts.IsWhitespace(c) || SyntaxFacts.IsNewLine(c))
                {
                    TextWindow.AdvanceChar();

                    // 跳过这些字符。

                    c = TextWindow.PeekChar();
                }
                break;

            // Represents a single LF
            case '\r':
            case '\n':
                if (Options.LanguageVersion < LanguageVersion.Lua4)
                    goto default;
                _utf8Builder.Add((byte)'\n');
                TextWindow.AdvanceChar(2);
                LexEndOfLine();
                break;

            // Not supported
            default:
                TextWindow.AdvanceChar();
                AddError(position: start, width: 2, code: ErrorCode.ERR_IllegalEscape);
                break;
        }
    }
}
