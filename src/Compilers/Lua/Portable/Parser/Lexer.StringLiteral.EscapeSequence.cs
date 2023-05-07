// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

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
        var start = this.TextWindow.Position;

        var c = this.TextWindow.NextChar();
        SyntaxDiagnosticInfo? error;

        Debug.Assert(c == '\\');

        c = this.TextWindow.NextChar();
        switch (c)
        {
            // 转义后返回自己的字符
            case '\'':
            case '"':
            case '\\':
                this._builder.Append(c);
                break;

            // 常用转义字符
            case 'a':
                this._builder.Append('\a');
                break;
            case 'b':
                this._builder.Append('\b');
                break;
            case 'f':
                this._builder.Append('\f');
                break;
            case 'n':
                this._builder.Append('\n');
                break;
            case 'r':
                this._builder.Append('\r');
                break;
            case 't':
                this._builder.Append('\t');
                break;
            case 'v':
                this._builder.Append('\v');
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
                this.TextWindow.Reset(start);
                var b = this.TextWindow.NextByteEscape(out error);
                if (error is null)
                    this.FlushToUtf8Builder(b);
                this.AddError(error);
                break;

            // 十六进制数字表示的Unicode字符
            case 'u':
                this.TextWindow.Reset(start);
                c = this.TextWindow.NextUnicodeEscape(out error, out var surrogate);
                if (c != SlidingTextWindow.InvalidCharacter)
                    this._builder.Append(c);
                if (surrogate != SlidingTextWindow.InvalidCharacter)
                    this._builder.Append(surrogate);
                this.AddError(error);
                break;

            // 后方紧跟的连续的字面量的空白字符和换行字符。
            case 'z':
                c = this.TextWindow.PeekChar();
                while (SyntaxFacts.IsWhiteSpace(c) || SyntaxFacts.IsNewLine(c))
                {
                    this.TextWindow.AdvanceChar();

                    // 跳过这些字符。

                    c = this.TextWindow.PeekChar();
                }
                break;

            // 插入换行字符序列本身。
            // Windows系统的换行字符序列为“\r\n”；
            // Unix系统的换行字符序列为“\n”；
            // Mac系统的换行字符序列为“\r”。
            case '\r':
            case '\n':
                this._builder.Append('\n');
                if (c == '\r' && this.TextWindow.PeekChar() == '\n')
                    this.TextWindow.AdvanceChar(); // 跳过这个字符。
                break;

            default:
                this.AddError(
                    start,
                    this.TextWindow.Position - start,
                    ErrorCode.ERR_IllegalEscape);
                break;
        }
    }
}
