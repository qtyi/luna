// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

[Flags]
internal enum LexerMode
{
    None = 0,

    Syntax = 0x0001,
    DebuggerSyntax = 0x0002,

    MaskLexMode = 0xFFFF
}

/// <summary>
/// 针对MoonScript语言特定的词法解析器。
/// </summary>
partial class Lexer
{
    private static partial LexerMode ModeOf(LexerMode mode) => mode & LexerMode.MaskLexMode;

    public partial SyntaxToken Lex(LexerMode mode)
    {
        this._mode = mode;

        switch (this._mode)
        {
            case LexerMode.Syntax:
            case LexerMode.DebuggerSyntax:
                return this.QuickScanSyntaxToken() ?? this.LexSyntaxToken();
        }

        switch (Lexer.ModeOf(this._mode))
        {
            default:
                throw ExceptionUtilities.UnexpectedValue(Lexer.ModeOf(this._mode));
        }
    }

    private partial SyntaxToken Create(
        in TokenInfo info,
        SyntaxListBuilder? leading,
        SyntaxListBuilder? trailing,
        SyntaxDiagnosticInfo[]? errors)
    {
        Debug.Assert(info.Kind != SyntaxKind.IdentifierToken || info.StringValue is not null);

        var leadingNode = leading?.ToListNode();
        var trailingNode = trailing?.ToListNode();

        var token = info.Kind switch
        {
            // 标识符标记
            SyntaxKind.IdentifierToken => SyntaxFactory.Identifier(info.ContextualKind, leadingNode, info.Text!, info.StringValue!, trailingNode),

            // 数字字面量标记
            SyntaxKind.NumericLiteralToken =>
                info.ValueKind switch
                {
                    // 64位整数
                    SpecialType.System_Int64 => SyntaxFactory.Literal(leadingNode, info.Text!, info.LongValue, trailingNode),
                    SpecialType.System_UInt64 => SyntaxFactory.Literal(leadingNode, info.Text!, info.ULongValue, trailingNode),
                    // 64位双精度浮点数
                    SpecialType.System_Double => SyntaxFactory.Literal(leadingNode, info.Text!, info.DoubleValue, trailingNode),
                    _ => throw ExceptionUtilities.UnexpectedValue(info.ValueKind),
                },

            // 字符串字面量标记
            SyntaxKind.StringLiteralToken => SyntaxFactory.Literal(leadingNode, info.Text!, info.Kind, info.StringValue!, info.InnerIndent, trailingNode),

            // 多行原始字符串字面量标记
            SyntaxKind.MultiLineRawStringLiteralToken => SyntaxFactory.Literal(leadingNode, info.Text!, info.Kind, info.Utf8StringValue!, trailingNode),

            // 插值字符串标记
            SyntaxKind.InterpolatedStringLiteralToken => SyntaxFactory.Literal(leadingNode, info.Text!, info.SyntaxTokenArrayValue, info.InnerIndent, trailingNode),

            // 文件结尾标记
            SyntaxKind.EndOfFileToken => SyntaxFactory.Token(leadingNode, SyntaxKind.EndOfFileToken, trailingNode),

            // 异常枚举值
            SyntaxKind.None => SyntaxFactory.BadToken(leadingNode, info.Text!, trailingNode),

            // 标点或关键字
            _ => SyntaxFactory.Token(leadingNode, info.Kind, trailingNode)
        };

        // 为标记添加诊断。
        if (errors is not null)
            token = token.WithDiagnosticsGreen(errors);

        return token;
    }

    private partial void ScanSyntaxToken(ref TokenInfo info)
    {
        // 初始化以准备新的标记扫描。
        info.Kind = SyntaxKind.None;
        info.ContextualKind = SyntaxKind.None;
        info.Text = null;
        char c;
        var startingPosition = this.TextWindow.Position;

        // 开始扫描标记。
        c = this.TextWindow.PeekChar();
        switch (c)
        {
            case '+':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '=')
                {
                    this.TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.PlusEqualsToken;
                }
                else
                    info.Kind = SyntaxKind.PlusToken;
                break;

            case '-':
                this.TextWindow.AdvanceChar();
                switch (this.TextWindow.PeekChar())
                {
                    case '=':
                        this.TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.MinusEqualsToken;
                        break;

                    case '>':
                        this.TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.MinusGreaterThanToken;
                        break;

                    default:
                        info.Kind = SyntaxKind.MinusToken;
                        break;
                }
                break;
            case '*':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '=')
                {
                    this.TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.AsteriskEqualsToken;
                }
                else
                    info.Kind = SyntaxKind.AsteriskToken;
                break;

            case '/':
                this.TextWindow.AdvanceChar();
                switch (this.TextWindow.PeekChar())
                {
                    case '/':
                        this.TextWindow.AdvanceChar();
                        if (this.TextWindow.PeekChar() == '=')
                        {
                            this.TextWindow.AdvanceChar();
                            info.Kind = SyntaxKind.SlashSlashEqualsToken;
                            this.CheckFeatureAvaliability(MessageID.IDS_FeatureFloorDivisionAssignmentOperator);
                        }
                        info.Kind = SyntaxKind.SlashSlashToken;
                        break;

                    case '=':
                        this.TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.SlashEqualsToken;
                        break;

                    default:
                        info.Kind = SyntaxKind.SlashToken;
                        break;
                }
                break;

            case '\\':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.BackSlashToken;
                break;

            case '^':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '=')
                {
                    this.TextWindow.AdvanceChar();
                    this.CheckFeatureAvaliability(MessageID.IDS_FeatureExponentiationAssignmentOperator);
                    info.Kind = SyntaxKind.CaretEqualsToken;
                }
                else
                    info.Kind = SyntaxKind.CaretToken;
                break;

            case '%':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '=')
                {
                    this.TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.PersentEqualsToken;
                }
                else
                    info.Kind = SyntaxKind.PersentToken;
                break;

            case '#':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.HashToken;
                break;

            case '&':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '=')
                {
                    this.TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.AmpersandEqualsToken;
                    this.CheckFeatureAvaliability(MessageID.IDS_FeatureBitwiseAndAssignmentOperator);
                }
                else
                    info.Kind = SyntaxKind.AmpersandToken;
                break;

            case '~':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '=')
                {
                    this.TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.TildeEqualsToken;
                }
                else
                    info.Kind = SyntaxKind.TildeToken;
                break;

            case '|':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '=')
                {
                    this.TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.BarEqualsToken;
                    this.CheckFeatureAvaliability(MessageID.IDS_FeatureBitwiseOrAssignmentOperator);
                }
                else
                    info.Kind = SyntaxKind.BarToken;
                break;

            case '<':
                this.TextWindow.AdvanceChar();
                switch (this.TextWindow.PeekChar())
                {
                    case '=':
                        this.TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.LessThanEqualsToken;
                        break;

                    case '<':
                        this.TextWindow.AdvanceChar();
                        if (this.TextWindow.PeekChar() == '=')
                        {
                            this.TextWindow.AdvanceChar();
                            this.CheckFeatureAvaliability(MessageID.IDS_FeatureBitwiseLeftShiftAssignmentOperator);
                            info.Kind = SyntaxKind.LessThanLessThanEqualsToken;
                        }
                        info.Kind = SyntaxKind.LessThanLessThanToken;
                        break;

                    default:
                        info.Kind = SyntaxKind.LessThanToken;
                        break;
                }
                break;

            case '>':
                this.TextWindow.AdvanceChar();
                switch (this.TextWindow.PeekChar())
                {
                    case '=':
                        this.TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.GreaterThanEqualsToken;
                        break;
                    case '>':
                        this.TextWindow.AdvanceChar();
                        if (this.TextWindow.PeekChar() == '=')
                        {
                            this.TextWindow.AdvanceChar();
                            this.CheckFeatureAvaliability(MessageID.IDS_FeatureBitwiseRightShiftAssignmentOperator);
                            info.Kind = SyntaxKind.GreaterThanGreaterThanEqualsToken;
                        }
                        info.Kind = SyntaxKind.GreaterThanGreaterThanToken;
                        break;

                    default:
                        info.Kind = SyntaxKind.GreaterThanToken;
                        break;
                }
                break;

            case '=':
                this.TextWindow.AdvanceChar();
                switch (this.TextWindow.PeekChar())
                {
                    case '=':
                        this.TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.EqualsEqualsToken;
                        break;

                    case '>':
                        this.TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.EqualsGreaterThanToken;
                        break;

                    default:
                        info.Kind = SyntaxKind.EqualsToken;
                        break;
                }
                break;

            case '!':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '=')
                {
                    this.TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.ExclamationEqualsToken;
                }
                else
                    info.Kind = SyntaxKind.ExclamationToken;
                break;

            case '(':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.OpenParenToken;
                break;

            case ')':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CloseParenToken;
                break;

            case '{':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.OpenBraceToken;
                break;

            case '}':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CloseBraceToken;
                break;

            case '[':
                switch (this.TextWindow.PeekChar(2))
                {
                    case '[':
                        this.ScanMultiLineRawStringLiteral(ref info);
                        break;

                    case '=':
                        for (var i = 2; ; i++)
                        {
                            var nextChar = this.TextWindow.PeekChar(i);
                            if (nextChar == '=') continue;
                            else if (nextChar == '[')
                            {
                                this.ScanMultiLineRawStringLiteral(ref info, i - 1);
                                break;
                            }
                            else goto default; // 未匹配到完整的多行原始字符字面量的起始语法。
                        }
                        break;

                    default:
                        this.TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.OpenBracketToken;
                        break;
                }
                break;

            case ']':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CloseBracketToken;
                break;

            case ':':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.ColonToken;
                break;

            case ',':
                this.TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CommaToken;
                break;

            case '.':
                if (!this.ScanNumericLiteral(ref info))
                {
                    this.TextWindow.AdvanceChar();
                    if (this.TextWindow.PeekChar() == '.')
                    {
                        this.TextWindow.AdvanceChar();
                        switch (this.TextWindow.PeekChar())
                        {
                            case '.':
                                this.TextWindow.AdvanceChar();
                                info.Kind = SyntaxKind.DotDotDotToken;
                                break;

                            case '=':
                                this.TextWindow.AdvanceChar();
                                info.Kind = SyntaxKind.DotDotEqualsToken;
                                break;

                            default:
                                info.Kind = SyntaxKind.DotDotToken;
                                break;
                        }
                    }
                    else
                        info.Kind = SyntaxKind.DotToken;
                }
                break;

            case '@':
                this.TextWindow.AdvanceChar();
                if (this.TextWindow.PeekChar() == '@')
                {
                    this.TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.CommercialAtCommercialAtToken;
                }
                else
                    info.Kind = SyntaxKind.CommercialAtToken;
                break;

            // 字符串字面量
            case '\"':
            case '\'':
                this.ScanStringLiteral(ref info);
                break;

            case 'a':
                if (this.TextWindow.PeekChars(4) == "and=")
                {
                    this.TextWindow.AdvanceChar(4);
                    info.Kind = SyntaxKind.AndEqualsToken;
                    break;
                }
                else goto case 'b';
            case 'b':
            case 'c':
            case 'd':
            case 'e':
            case 'f':
            case 'g':
            case 'h':
            case 'i':
            case 'j':
            case 'k':
            case 'l':
            case 'm':
            case 'n':
                goto case 'p';
            case 'o':
                if (this.TextWindow.PeekChars(3) == "or=")
                {
                    this.TextWindow.AdvanceChar(3);
                    info.Kind = SyntaxKind.OrEqualsToken;
                    break;
                }
                else goto case 'p';
            case 'p':
            case 'q':
            case 'r':
            case 's':
            case 't':
            case 'u':
            case 'v':
            case 'w':
            case 'x':
            case 'y':
            case 'z':
            case 'A':
            case 'B':
            case 'C':
            case 'D':
            case 'E':
            case 'F':
            case 'G':
            case 'H':
            case 'I':
            case 'J':
            case 'K':
            case 'L':
            case 'M':
            case 'N':
            case 'O':
            case 'P':
            case 'Q':
            case 'R':
            case 'S':
            case 'T':
            case 'U':
            case 'V':
            case 'W':
            case 'X':
            case 'Y':
            case 'Z':
            case '_':
                this.ScanIdentifierOrKeyword(ref info);
                break;

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
                this.ScanNumericLiteral(ref info);
                break;

            case SlidingTextWindow.InvalidCharacter:
                if (!this.TextWindow.IsReallyAtEnd())
                    goto default;
                else
                    info.Kind = SyntaxKind.EndOfFileToken;
                break;

            default:
                if (SyntaxFacts.IsIdentifierStartCharacter(this.TextWindow.PeekChar()))
                    goto case 'a';

                this.TextWindow.AdvanceChar();

                if (this._badTokenCount++ > 200)
                {
                    //当遇到大量无法决定的字符时，将剩下的输出也合并入。
                    var end = this.TextWindow.Text.Length;
                    var width = end - startingPosition;
                    info.Text = this.TextWindow.Text.ToString(new(startingPosition, width));
                    this.TextWindow.Reset(end);
                }
                else
                    info.Text = this.TextWindow.GetText(intern: true);

                this.AddError(ErrorCode.ERR_UnexpectedCharacter, info.Text);
                break;
        }
    }

    private partial void LexSyntaxTrivia(
        bool afterFirstToken,
        bool isTrailing,
        ref SyntaxListBuilder triviaList)
    {
        while (true)
        {
            this.Start();
            var c = this.TextWindow.PeekChar();
            if (c == ' ')
            {
                this.AddTrivia(this.ScanWhiteSpace(), ref triviaList);
                continue;
            }
            else if (c > 127)
            {
                if (SyntaxFacts.IsWhiteSpace(c))
                    c = ' ';
                else if (SyntaxFacts.IsNewLine(c))
                    c = '\n';
            }

            switch (c)
            {
                case ' ':
                case '\t':
                case '\v':
                case '\f':
                case '\u001A':
                    this.AddTrivia(this.ScanWhiteSpace(), ref triviaList);
                    continue;

                case '-':
                    if (this.TextWindow.PeekChar(1) == '-')
                    {
                        this.TextWindow.AdvanceChar(2);
                        this.AddTrivia(this.ScanComment(), ref triviaList);
                        continue;
                    }
                    else goto default;

                default:
                    {
                        var endOfLine = this.ScanEndOfLine();
                        if (endOfLine is not null)
                        {
                            this.AddTrivia(endOfLine, ref triviaList);

                            /* 为了适应MoonScript根据缩进来表示块体，在识别后方琐碎内容时，连续的语法琐碎内容应在行尾换行后截断。
                             * 并且将下一行起始的连续空白字符作为下一个语法标记的前方语法琐碎内容；
                             * 但是当识别前方琐碎内容时，遇到多个空行（行中没有或只有空白字符）时，不应截断。
                             * 应将所有空白字符和换行字符保存在同一个前方语法琐碎内容中，用于后续分析。
                             */
                            if (isTrailing)
                                // 当分析的是后方语法琐碎内容时，分析成功后直接退出。
                                return;
                            else
                                // 否则进行下一个语法琐碎内容的分析。
                                continue;
                        }
                    }

                    // 下一个字符不是空白字符，终止扫描。
                    return;
            }
        }
    }
}
