// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

[Flags]
internal enum LexerMode
{
    Syntax = 0x0001,
    DebuggerSyntax = 0x0002,
    Directive = 0x0004,

    MaskLexMode = 0xFFFF,

    None = 0,
}

/// <summary>
/// Lexer for Lua language.
/// </summary>
internal partial class Lexer
{
    private static partial LexerMode ModeOf(LexerMode mode) => mode & LexerMode.MaskLexMode;

#if DEBUG
    /// <summary>
    /// Number of tokens lexed.
    /// </summary>
    internal static int TokensLexed;
#endif

    public partial SyntaxToken Lex(LexerMode mode)
    {
#if DEBUG
        TokensLexed++;
#endif

        _mode = mode;

        switch (_mode)
        {
            case LexerMode.Syntax:
            case LexerMode.DebuggerSyntax:
                return QuickScanSyntaxToken() ?? LexSyntaxToken();

            case LexerMode.Directive:
                return LexDirectiveToken();
        }

        switch (ModeOf(_mode))
        {
            default:
                throw ExceptionUtilities.UnexpectedValue(ModeOf(_mode));
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
            // Identifier
            SyntaxKind.IdentifierToken => ThisInternalSyntaxFactory.Identifier(info.ContextualKind, leadingNode, info.Text!, info.StringValue!, trailingNode),

            // Numeric literal
            SyntaxKind.NumericLiteralToken => info.ValueKind switch
            {
                // 64-bit integer
                TokenValueType.Int64 => ThisInternalSyntaxFactory.Literal(leadingNode, info.Text!, info.LongValue, trailingNode),
                TokenValueType.UInt64 => ThisInternalSyntaxFactory.Literal(leadingNode, info.Text!, info.ULongValue, trailingNode),
                // 64-bit double-precision floating-point
                TokenValueType.Double => ThisInternalSyntaxFactory.Literal(leadingNode, info.Text!, info.DoubleValue, trailingNode),
                // Error case
                _ => throw ExceptionUtilities.UnexpectedValue(info.ValueKind),
            },

            // String literal
            SyntaxKind.StringLiteralToken or
            // Multi-line raw string literal
            SyntaxKind.MultiLineRawStringLiteralToken => ThisInternalSyntaxFactory.Literal(leadingNode, info.Text!, info.Kind, info.Utf8StringValue!, trailingNode),

            // End of file
            SyntaxKind.EndOfFileToken => ThisInternalSyntaxFactory.Token(leadingNode, SyntaxKind.EndOfFileToken, trailingNode),

            // Bad
            SyntaxKind.None => ThisInternalSyntaxFactory.BadToken(leadingNode, info.Text!, trailingNode),

            // Punctuation or keyword
            _ => ThisInternalSyntaxFactory.Token(leadingNode, info.Kind, trailingNode)
        };

        // Add diagnostics to token.
        if (errors is not null)
            token = token.WithDiagnosticsGreen(errors);

        return token;
    }

    private partial void ScanSyntaxToken(ref TokenInfo info)
    {
        // Initialize for new token scan.
        info.Kind = SyntaxKind.None;
        info.ContextualKind = SyntaxKind.None;
        info.Text = null;

        char c;
        var startingPosition = TextWindow.Position;

        // Start scanning the token.
        c = TextWindow.PeekChar();
        switch (c)
        {
            case '+':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.PlusToken;
                break;

            case '-':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.MinusToken;
                break;
            case '*':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.AsteriskToken;
                break;

            case '/':
                TextWindow.AdvanceChar();
                if (TextWindow.PeekChar() == '/' && IsPunctuationAvaliable(SyntaxKind.SlashSlashToken))
                {
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.SlashSlashToken;
                }
                else
                    info.Kind = SyntaxKind.SlashToken;
                break;

            case '^':
                if (!IsPunctuationAvaliable(SyntaxKind.CaretToken))
                    goto default;

                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CaretToken;
                break;

            case '%':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.PercentToken;
                break;

            case '@':
                if (!IsPunctuationAvaliable(SyntaxKind.CommercialAtToken))
                    goto default;

                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CommercialAtToken;
                break;

            case '#':
                if (!IsPunctuationAvaliable(SyntaxKind.HashToken))
                    goto default;

                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.HashToken;
                break;

            case '&':
                if (!IsPunctuationAvaliable(SyntaxKind.AmpersandToken))
                    goto default;

                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.AmpersandToken;
                break;

            case '~':
                if (TextWindow.PeekChar(1) == '=')
                {
                    TextWindow.AdvanceChar(2);
                    info.Kind = SyntaxKind.TildeEqualsToken;
                }
                else if (IsPunctuationAvaliable(SyntaxKind.TildeToken))
                {
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.TildeToken;
                }
                else
                    goto default;
                break;

            case '|':
                if (!IsPunctuationAvaliable(SyntaxKind.BarToken))
                    goto default;

                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.BarToken;
                break;

            case '<':
                TextWindow.AdvanceChar();
                switch (TextWindow.PeekChar())
                {
                    case '=':
                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.LessThanEqualsToken;
                        break;

                    case '<':
                        if (!IsPunctuationAvaliable(SyntaxKind.LessThanLessThanToken))
                            goto default;

                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.LessThanLessThanToken;
                        break;

                    default:
                        info.Kind = SyntaxKind.LessThanToken;
                        break;
                }
                break;

            case '>':
                TextWindow.AdvanceChar();
                switch (TextWindow.PeekChar())
                {
                    case '=':
                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.GreaterThanEqualsToken;
                        break;
                    case '>':
                        if (!IsPunctuationAvaliable(SyntaxKind.GreaterThanGreaterThanToken))
                            goto default;

                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.GreaterThanGreaterThanToken;
                        break;

                    default:
                        info.Kind = SyntaxKind.GreaterThanToken;
                        break;
                }
                break;

            case '=':
                TextWindow.AdvanceChar();
                if (TextWindow.PeekChar() == '=' && IsPunctuationAvaliable(SyntaxKind.EqualsEqualsToken))
                {
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.EqualsEqualsToken;
                }
                else
                    info.Kind = SyntaxKind.EqualsToken;
                break;

            case '(':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.OpenParenToken;
                break;

            case ')':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CloseParenToken;
                break;

            case '{':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.OpenBraceToken;
                break;

            case '}':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CloseBraceToken;
                break;

            case '[':
                switch (TextWindow.PeekChar(1))
                {
                    case '[':
                        if (!IsMultiLineRawStringLiteralAvailable())
                            goto default;

                        ScanMultiLineRawStringLiteral(ref info);
                        break;

                    case '=':
                        if (!IsLeveledMultiLineRawStringLiteralAvailable())
                            goto default;

                        for (var i = 2; ; i++)
                        {
                            var nextChar = TextWindow.PeekChar(i);
                            if (nextChar == '=') continue;
                            else if (nextChar == '[')
                            {
                                ScanMultiLineRawStringLiteral(ref info, i - 1);
                                break;
                            }
                            else goto default; // 未匹配到完整的多行原始字符字面量的起始语法。
                        }
                        break;

                    default:
                        TextWindow.AdvanceChar();
                        info.Kind = SyntaxKind.OpenBracketToken;
                        break;
                }
                break;

            case ']':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CloseBracketToken;
                break;

            case ':':
                if (!IsPunctuationAvaliable(SyntaxKind.ColonToken))
                    goto default;

                TextWindow.AdvanceChar();
                if (TextWindow.PeekChar() == ':' && IsPunctuationAvaliable(SyntaxKind.ColonColonToken))
                {
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.ColonColonToken;
                }
                else
                    info.Kind = SyntaxKind.ColonToken;
                break;

            case ';':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.SemicolonToken;
                break;

            case ',':
                TextWindow.AdvanceChar();
                info.Kind = SyntaxKind.CommaToken;
                break;

            case '.':
                if (!ScanNumericLiteral(ref info))
                {
                    TextWindow.AdvanceChar();
                    if (TextWindow.PeekChar() == '.')
                    {
                        TextWindow.AdvanceChar();
                        if (TextWindow.PeekChar() == '.' && IsPunctuationAvaliable(SyntaxKind.DotDotDotToken))
                        {
                            TextWindow.AdvanceChar();
                            info.Kind = SyntaxKind.DotDotDotToken;
                        }
                        else
                            info.Kind = SyntaxKind.DotDotToken;
                    }
                    else
                        info.Kind = SyntaxKind.DotToken;
                }
                break;

            // 字符串字面量
            case '\"':
            case '\'':
                ScanStringLiteral(ref info);
                break;

            case 'a':
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
            case 'o':
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
                ScanIdentifierOrKeyword(ref info);
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
                ScanNumericLiteral(ref info);
                break;

            case SlidingTextWindow.InvalidCharacter:
                if (!TextWindow.IsReallyAtEnd())
                    goto default;
                else
                    info.Kind = SyntaxKind.EndOfFileToken;
                break;

            default:
                if (SyntaxFacts.IsIdentifierStartCharacter(TextWindow.PeekChar()))
                    goto case 'a';

                TextWindow.AdvanceChar();

                if (_badTokenCount++ > 200)
                {
                    //当遇到大量无法决定的字符时，将剩下的输出也合并入。
                    var end = TextWindow.Text.Length;
                    var width = end - startingPosition;
                    info.Text = TextWindow.Text.ToString(new(startingPosition, width));
                    TextWindow.Reset(end);
                }
                else
                    info.Text = TextWindow.GetText(intern: true);

                AddError(ErrorCode.ERR_UnexpectedCharacter, info.Text);
                break;
        }
    }

    private partial void ScanSyntaxTrivia(
        bool afterFirstToken,
        bool isTrailing,
        ref SyntaxListBuilder triviaList)
    {
        var onlyWhitespaceOnLine = !isTrailing;
        while (true)
        {
            Start();
            var c = TextWindow.PeekChar();
            if (c == ' ')
            {
                AddTrivia(LexWhitespace(), ref triviaList);
                continue;
            }
            else if (c > 127)
            {
                if (SyntaxFacts.IsWhitespace(c))
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
                    AddTrivia(LexWhitespace(), ref triviaList);
                    continue;

                case '-':
                    if (TextWindow.PeekChar(1) == '-')
                    {
                        TextWindow.AdvanceChar(2);
                        AddTrivia(LexComment(), ref triviaList);
                        onlyWhitespaceOnLine = false;
                        continue;
                    }
                    goto default;

                case '#':
                    if (TextWindow.PeekChar(1) == '!' && TextWindow.Position == 0 && IsPunctuationAvaliable(SyntaxKind.HashExclamationToken))
                    {
                        ScanDirectiveAndExcludedTrivia(afterFirstToken, isTrailing || !onlyWhitespaceOnLine, ref triviaList);
                        continue;
                    }
                    goto default;

                case '$':
                    if (IsDirectiveAvailable())
                    {
                        ScanDirectiveAndExcludedTrivia(afterFirstToken, isTrailing || !onlyWhitespaceOnLine, ref triviaList);
                        continue;
                    }
                    goto default;

                default:
                    {
                        var endOfLine = LexEndOfLine();
                        if (endOfLine is not null)
                        {
                            AddTrivia(endOfLine, ref triviaList);
                            if (isTrailing)
                                // 当分析的是后方语法琐碎内容时，分析成功后直接退出。
                                return;
                            else
                            {
                                onlyWhitespaceOnLine = true;
                                // 否则进行下一个语法琐碎内容的分析。
                                continue;
                            }
                        }
                    }

                    // 下一个字符不是空白字符，终止扫描。
                    return;
            }
        }
    }

    private partial bool ScanDirectiveToken(ref TokenInfo info)
    {
        var c = TextWindow.PeekChar();
        switch (c)
        {
            case SlidingTextWindow.InvalidCharacter:
                if (TextWindow.IsReallyAtEnd())
                {
                    info.Kind = SyntaxKind.EndOfDirectiveToken;
                    break;
                }
                else goto default;
            case '\n':
            case '\r':
                info.Kind = SyntaxKind.EndOfDirectiveToken;
                break;

            case '#':
                TextWindow.AdvanceChar();
                if (TextWindow.PeekChar() == '!')
                {
                    TextWindow.AdvanceChar();
                    info.Kind = SyntaxKind.HashExclamationToken;
                }
                else
                    info.Kind = SyntaxKind.HashToken;
                break;

            default:
                ScanToEndOfLine(_builder, trimEnd: false);
                info.Kind = SyntaxKind.None;
                info.Text = TextWindow.GetText(intern: true);
                break;

        }

        return info.Kind != SyntaxKind.None;
    }

    private partial ThisInternalSyntaxNode? LexDirectiveTrivia()
    {
        Start();
        var c = TextWindow.PeekChar();
        if (c == ' ')
            return LexWhitespace();
        else if (c > 127)
        {
            if (SyntaxFacts.IsWhitespace(c))
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
                return LexWhitespace();

            case '-':
                if (TextWindow.PeekChar(1) == '-')
                {
                    TextWindow.AdvanceChar(2);
                    return LexComment();
                }
                else goto default;

            default:
                {
                    var endOfLine = LexEndOfLine();
                    if (endOfLine is not null)
                        return endOfLine;
                }

                // 下一个字符不是空白字符，终止扫描。
                return null;
        }
    }

    private partial void ScanDirectiveAndExcludedTrivia(
        bool afterFirstToken,
        bool afterNonWhitespaceOnLine,
        ref SyntaxListBuilder triviaList)
    {
        var directive = LexSingleDirective(isActive: true, endIsActive: true, afterFirstToken, afterNonWhitespaceOnLine, ref triviaList);

        // also scan excluded stuff
        if (directive is EndInputDirectiveTriviaSyntax)
        {
            var disabledText = LexDisabledTextToEOF();
            if (disabledText is not null)
                AddTrivia(disabledText, ref triviaList);
        }
        else if (directive is BranchingDirectiveTriviaSyntax { BranchTaken: false })
            ScanExcludedDirectivesAndTrivia(true, ref triviaList);
    }

    private void ScanExcludedDirectivesAndTrivia(bool endIsActive, ref SyntaxListBuilder triviaList)
    {
        while (true)
        {
            var text = LexDisabledTextToNextDirective(out var hasFollowingDirective);
            if (text is not null)
                AddTrivia(text, ref triviaList);

            if (!hasFollowingDirective)
                break;

            var directive = LexSingleDirective(false, endIsActive, false, false, ref triviaList);
            if (directive.Kind == SyntaxKind.EndDirectiveTrivia || directive is BranchingDirectiveTriviaSyntax { BranchTaken: true })
                break;
            else if (directive.Kind is SyntaxKind.IfDirectiveTrivia or SyntaxKind.IfNotDirectiveTrivia)
                ScanExcludedDirectivesAndTrivia(false, ref triviaList);
        }
    }

    private ThisInternalSyntaxNode LexSingleDirective(
        bool isActive,
        bool endIsActive,
        bool afterFirstToken,
        bool afterNonWhitespaceOnLine,
        ref SyntaxListBuilder triviaList)
    {
        if (SyntaxFacts.IsWhitespace(TextWindow.PeekChar()))
        {
            Start();
            AddTrivia(LexWhitespace(), ref triviaList);
        }

        var saveMode = _mode;

        _directiveParser ??= new DirectiveParser(this);
        _directiveParser.ReInitialize(_directives);

        var directive = _directiveParser.ParseDirective(isActive, endIsActive, afterFirstToken, afterNonWhitespaceOnLine);
        AddTrivia(directive, ref triviaList);
        _directives = directive.ApplyDirectives(_directives);

        _mode = saveMode;

        return directive;
    }

    // consume text up to the next directive
    private ThisInternalSyntaxNode? LexDisabledTextToNextDirective(out bool followedByDirective)
    {
        Start();

        var lastLineStart = TextWindow.Position;
        var lines = 0;
        var allWhitespace = true;
        while (true)
        {
            var c = TextWindow.PeekChar();
            switch (c)
            {
                case SlidingTextWindow.InvalidCharacter:
                    if (!TextWindow.IsReallyAtEnd())
                        goto default;

                    followedByDirective = false;
                    return TextWindow.Width > 0 ? ThisInternalSyntaxFactory.DisabledText(TextWindow.GetText(false)) : null;

                case '$':
                    if (!IsDirectiveAvailable())
                        goto default;

                    followedByDirective = true;
                    if (lastLineStart < TextWindow.Position && !allWhitespace)
                        goto default;

                    TextWindow.Reset(lastLineStart);  // reset so directive parser can consume the starting whitespace on this line
                    return TextWindow.Width > 0 ? ThisInternalSyntaxFactory.DisabledText(TextWindow.GetText(false)) : null;

                case '\r':
                case '\n':
                    LexEndOfLine();
                    lastLineStart = TextWindow.Position;
                    allWhitespace = true;
                    lines++;
                    break;

                default:
                    if (SyntaxFacts.IsNewLine(c))
                        goto case '\n';

                    allWhitespace = allWhitespace && SyntaxFacts.IsWhitespace(c);
                    TextWindow.AdvanceChar();
                    break;
            }
        }
    }

    // consume text up to the end of file
    private ThisInternalSyntaxNode? LexDisabledTextToEOF()
    {
        Start();

        while (true)
        {
            var c = TextWindow.PeekChar();
            if (c == SlidingTextWindow.InvalidCharacter && TextWindow.IsReallyAtEnd())
                return TextWindow.Width > 0 ? ThisInternalSyntaxFactory.DisabledText(TextWindow.GetText(false)) : null;
            TextWindow.AdvanceChar();
        }
    }

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private bool IsDirectiveAvailable() => SyntaxFacts.IsAnyToken(SyntaxKind.DollarToken, Options.LanguageVersion);

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private bool IsMultiLineRawStringLiteralAvailable() => SyntaxFacts.IsAnyToken(SyntaxKind.MultiLineRawStringLiteralToken, Options.LanguageVersion);

    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private bool IsLeveledMultiLineRawStringLiteralAvailable() => IsMultiLineRawStringLiteralAvailable() && Options.LanguageVersion >= LanguageVersion.Lua5_1;
}
