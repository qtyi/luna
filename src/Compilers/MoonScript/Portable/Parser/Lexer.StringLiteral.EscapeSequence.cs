// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

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
        char surrogate;

        Debug.Assert(c == '\\');

        c = TextWindow.NextChar();
        switch (c)
        {
            // 转义后返回自己的字符
            case '\'':
            case '"':
            case '\\':
            case '#': // 插值字符串起始（“#{”）
                _builder.Append(c);
                break;

            // 常用转义字符
            case 'a':
                _builder.Append('\a');
                break;
            case 'b':
                _builder.Append('\b');
                break;
            case 'f':
                _builder.Append('\f');
                break;
            case 'n':
                _builder.Append('\n');
                break;
            case 'r':
                _builder.Append('\r');
                break;
            case 't':
                _builder.Append('\t');
                break;
            case 'v':
                _builder.Append('\v');
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
                    FlushToUtf8Builder(b);
                AddError(error);
                break;

            // 十六进制数字表示的Unicode字符
            case 'u':
                TextWindow.Reset(start);
                var bs = TextWindow.NextUnicodeEscape(out error);
                if (error is null)
                    FlushToUtf8Builder(bs);
                AddError(error);
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
