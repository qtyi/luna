// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Linq;
using System.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class Lexer
{
    /// <summary>
    /// 扫描一个转义序列。
    /// </summary>
    private void ScanEscapeSequence()
    {
        var start = TextWindow.Position;

        var c = TextWindow.NextChar();
        SyntaxDiagnosticInfo? error;

        Debug.Assert(c == '\\');

        c = TextWindow.NextChar();
        switch (c)
        {
            // 转义后返回自己的字符
            case '\'':
            case '"':
            case '\\':
                _utf8Builder.Add((byte)c);
                break;

            // 常用转义字符
            case 'a':
                _utf8Builder.Add((byte)'\a');
                break;
            case 'b':
                _utf8Builder.Add((byte)'\b');
                break;
            case 'f':
                _utf8Builder.Add((byte)'\f');
                break;
            case 'n':
                _utf8Builder.Add((byte)'\n');
                break;
            case 'r':
                _utf8Builder.Add((byte)'\r');
                break;
            case 't':
                _utf8Builder.Add((byte)'\t');
                break;
            case 'v':
                _utf8Builder.Add((byte)'\v');
                break;

            // 十进制数字表示的Unicode字符
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

            // 十六进制数字表示的ASCII字符
            case 'x':
                TextWindow.Reset(start);
                var b = TextWindow.NextByteEscape(out error);
                if (error is null)
                    _utf8Builder.Add(b);
                AddError(error);
                break;

            // 十六进制数字表示的Unicode字符
            case 'u':
                TextWindow.Reset(start);
                var bs = TextWindow.NextUnicodeEscape(out error);
                if (error is null)
                    _utf8Builder.AddRange(bs);
                AddError(error);
                break;

            // 后方紧跟的连续的字面量的空白字符和换行字符。
            case 'z':
                c = TextWindow.PeekChar();
                while (SyntaxFacts.IsWhitespace(c) || SyntaxFacts.IsNewLine(c))
                {
                    TextWindow.AdvanceChar();

                    // 跳过这些字符。

                    c = TextWindow.PeekChar();
                }
                break;

            // 插入换行字符序列本身。
            // Windows系统的换行字符序列为“\r\n”；
            // Unix系统的换行字符序列为“\n”；
            // Mac系统的换行字符序列为“\r”。
            case '\r':
            case '\n':
                _utf8Builder.Add((byte)'\n');
                if (c == '\r' && TextWindow.PeekChar() == '\n')
                    TextWindow.AdvanceChar(); // 跳过这个字符。
                break;

            default:
                AddError(
                    start,
                    TextWindow.Position - start,
                    ErrorCode.ERR_IllegalEscape);
                break;
        }
    }
}
