﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    private partial bool ScanStringLiteral(ref TokenInfo info)
    {
        var quote = TextWindow.PeekChar();
        Debug.Assert(quote == '\'' || quote == '"');

        if (quote == '"') // 可能是插值字符串字面量。
        {
            var start = TextWindow.Position; // 记录起始位置。

            var scanner = new InterpolatedStringScanner(this);
            if (scanner.ScanInterpolatedStringLiteral(ref info)) return true;

            // 将内部扫描器搜集的错误消息传递出来。
            if (scanner.Error is not null)
                AddError(scanner.Error);

            info.Text = TextWindow.GetText(start, TextWindow.Position - start, intern: true);
        }
        else
        {
            TextWindow.AdvanceChar();
            var spanBuilder = ArrayBuilder<string?>.GetInstance();
            _builder.Clear();

            while (true)
            {
                var c = TextWindow.PeekChar();
                if (c == quote) // 字符串结尾
                {
                    TextWindow.AdvanceChar();

                    if (_builder.Length > 0)
                        spanBuilder.Add(_builder.ToString());
                    break;
                }
                // 字符串中可能包含非正规的UTF-16以外的字符，检查是否真正到达文本结尾来验证这些字符不是由用户代码引入的情况。
                else if (c == SlidingTextWindow.InvalidCharacter && TextWindow.IsReallyAtEnd())
                {
                    Debug.Assert(TextWindow.Width > 0);
                    AddError(ErrorCode.ERR_UnterminatedStringLiteral);

                    if (_builder.Length > 0)
                        spanBuilder.Add(_builder.ToString());
                    break;
                }
                else if (SyntaxFacts.IsWhitespace(c))
                {
                    // 扫描缩进或内容（第一行）状态。
                    TextWindow.AdvanceChar();
                    _builder.Append(c);
                }
                else
                {
                    if (spanBuilder.Count % 2 == 1) // 处于扫描缩进状态。
                    {
                        if (_builder.Length > 0)
                        {
                            spanBuilder.Add(_builder.ToString());
                            _builder.Clear();
                        }
                        else
                            spanBuilder.Add(null);
                    }

                    if (c == '\\') // 转义字符前缀
                        ScanEscapeSequence();
                    else if (SyntaxFacts.IsNewLine(c))
                    {
                        TextWindow.AdvanceChar();
                        if (SyntaxFacts.IsNewLine(c, TextWindow.PeekChar()))
                            TextWindow.AdvanceChar();
                        _builder.Append('\n');

                        spanBuilder.Add(_builder.ToString());
                        _builder.Clear();
                    }
                    else // 普通字符
                    {
                        // 扫描内容状态。
                        TextWindow.AdvanceChar();
                        _builder.Append(c);
                    }
                }
            }

            // 缩进量修剪。
            if (spanBuilder.Count > 1)
            {
                // 找到最小缩进量。
                var minIndent = int.MaxValue;
                for (var i = 1; i < spanBuilder.Count; i += 2)
                {
                    var span = spanBuilder[i];
                    if (span is null) // 遇到无缩进量的行，快速退出。
                    {
                        minIndent = 0;
                        break;
                    }
                    minIndent = Math.Min(minIndent, span.Sum(SyntaxFacts.WhiteSpaceIndent));
                }

                TrimIndent(spanBuilder, minIndent);

                info.InnerIndent = minIndent;
            }
            else
                info.InnerIndent = 0; // 单行字符串。

            info.Text = TextWindow.GetText(intern: true);

            _builder.Clear();
            _builder.Append(string.Concat(spanBuilder.ToImmutableAndFree()));
        }

        info.Kind = SyntaxKind.StringLiteralToken;
        info.ValueKind = TokenValueType.String;

        if (_builder.Length == 0)
            info.StringValue = string.Empty;
        else
            info.StringValue = TextWindow.Intern(_builder);

        return true;
    }

    private static void TrimIndent(ArrayBuilder<string?> spans, int innerIndent)
    {
        // 修剪缩进量。
        for (var i = 1; i < spans.Count; i += 2)
        {
            var indent = 0;
            var start = 0;
            var span = spans[i];
            if (span is null) continue; // 遇到无缩进量的行，跳过。

            while (indent <= innerIndent && start < span.Length)
            {
                var nextIndent = SyntaxFacts.WhiteSpaceIndent(span[start]);
                if (indent == innerIndent && nextIndent != 0) break; // 缩进量正好相等。

                indent += nextIndent;
                start++;
            };
            if (indent > innerIndent)
            {
                var indentLength = indent - innerIndent;
                var spanLength = span.Length - start;
                var buffer = new char[spanLength + indentLength];
                for (var _i = 0; _i < indentLength; i++)
                    buffer[_i] = ' '; // 前导空格符。
                if (start < span.Length)
                {
                    span.CopyTo(start, buffer, indentLength, spanLength);
                }

                spans[i] = new string(buffer);
            }
            else
            {
                spans[i] = start == 0 ? span : span.Substring(start);
            }
        }
    }
}
