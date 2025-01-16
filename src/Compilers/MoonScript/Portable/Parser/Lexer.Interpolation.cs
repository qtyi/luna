// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

partial class Lexer
{
    internal readonly struct Interpolation
    {
        /// <summary>
        /// 插值语法的起始语法（“#{”）的语法标记。
        /// </summary>
        public readonly SyntaxToken StartToken;

        /// <summary>
        /// 插值语法的起始语法（“#{”）的内部的语法标记数组。
        /// </summary>
        public readonly ImmutableArray<SyntaxToken> InnerTokens;

        /// <summary>
        /// 插值语法的结尾语法（“}”）的语法标记。
        /// </summary>
        public readonly SyntaxToken EndToken;

        /// <summary>
        /// 插值语法的起始语法（“#{”）的范围。
        /// </summary>
        public readonly Range StartRange;

        /// <summary>
        /// 插值语法的结尾语法（“}”）的范围。
        /// </summary>
        public readonly Range EndRange;

        public Interpolation(
            SyntaxToken startToken,
            in ImmutableArray<SyntaxToken> innerTokens,
            SyntaxToken endToken,
            in Range startRange,
            in Range endRange)
        {
            StartToken = startToken;
            InnerTokens = innerTokens;
            EndToken = endToken;
            StartRange = startRange;
            EndRange = endRange;
        }
    }

    [NonCopyable]
    private ref partial struct InterpolatedStringScanner
    {
        private readonly Lexer _lexer;
        private SyntaxDiagnosticInfo? _error = null;

        /// <summary>
        /// 获取或设置扫描过程中搜集到的错误。
        /// 一旦搜集到了一个错误，我们就应在下一个可能的结束位置停下解析，以避免混淆错误提示。
        /// </summary>
        public SyntaxDiagnosticInfo? Error { get => _error; set => _error ??= value; }

        public InterpolatedStringScanner(Lexer lexer) => _lexer = lexer;

        private bool IsAtEnd() => IsAtEnd(true);

        private bool IsAtEnd(bool allowNewline)
        {
            var c = _lexer.TextWindow.PeekChar();
            return
                (!allowNewline && SyntaxFacts.IsNewLine(c)) ||
                (c == SlidingTextWindow.InvalidCharacter && _lexer.TextWindow.IsReallyAtEnd());
        }

        public bool ScanInterpolatedStringLiteral(ref TokenInfo info)
        {
            var quote = _lexer.TextWindow.NextChar();
            Debug.Assert(quote == '"');

            var buffer = ArrayBuilder<BuilderStringLiteralToken>.GetInstance(); // 缓存需要重设缩进量的语法标记。
            var builder = ArrayBuilder<SyntaxToken>.GetInstance();
            /* 按照合法语法，插值字符串字面量由至少两个插值字符串字面量文本标记构成。
             * 因为进行向后扫描，所以仅有最后一个插值字符串字面量文本标记不会检测到存在插值语法。
             */
            var hasInterpolation = false;
            var minIndent = int.MaxValue;
            var isLastTokenAtEndOfLine = false;
            while (true)
            {
                // 扫描一个字符串字面量。
                if (ScanInterpolatedStringLiteralText(quote, ref hasInterpolation, out var textRange, out var spanBuilder, ref minIndent))
                {
                    // 扫描到符合字符串字面量格式的标记。
                    var errors = Error is null ? null : new[] { Error };
                    if (hasInterpolation) // 存在插值语法，则是插值字符串字面量文本标记。
                    {
                        var builderToken = createBuilderToken(_lexer, textRange, spanBuilder, errors);
                        buffer.Add(builderToken);
                        builder.Add(builderToken);
                        // 若上一个标记位于行尾，则表明需要检查扫描到的字符串字面量的缩进量。
                        if (isLastTokenAtEndOfLine)
                        {
                            minIndent = Math.Min(minIndent, builderToken.GetWhiteSpaceIndent());
                        }
                        isLastTokenAtEndOfLine = builderToken.IsTokenAtEndOfLine();

                        // 添加插值内容的语法标记。
                        var contents = ScanInterpolatedStringContent(
                            out var startRange,
                            out var endRange,
                            out var startToken,
                            out var endToken)
                            .ToImmutableOrEmptyAndFree();
                        var interpolation = new Interpolation(
                            startToken,
                            contents,
                            endToken,
                            startRange,
                            endRange);
                        var contentToken = createInterpolationToken(_lexer, interpolation, errors);
                        builder.Add(contentToken);
                        isLastTokenAtEndOfLine = false; // 无论如何，默认插值结束是在同一行上的。

                    }
                    else // 不存在插值语法。
                    {
                        if (builder.Count == 0) // 若是第一个扫描到的标记，则表示普通字符串字面量标记；
                            return false; // 此方法仅处理插值字符串字面量，因此返回失败。

                        // 最后一个也应为插值字符串字面量文本标记。
                        var builderToken = createBuilderToken(_lexer, textRange, spanBuilder, errors);
                        buffer.Add(builderToken);
                        builder.Add(builderToken);
                        // 若上一个标记位于行尾，则表明需要检查扫描到的字符串字面量的缩进量。
                        if (isLastTokenAtEndOfLine)
                        {
                            minIndent = Math.Min(minIndent, builderToken.GetWhiteSpaceIndent());
                        }
                        break;
                    }
                }
            }

            // 传播最小缩进量。
            foreach (var builderToken in buffer)
            {
                builderToken.InnerIndent = minIndent;
            }
            buffer.Free(); // 释放缓存。

            var tokens = builder.ToImmutableOrEmptyAndFree();
            // 复原前后方语法琐碎。
            _lexer._leadingTriviaCache.Clear();
            _lexer._leadingTriviaCache.AddRange(tokens[0].LeadingTrivia);
            _lexer._trailingTriviaCache.Clear();
            _lexer._trailingTriviaCache.AddRange(tokens[tokens.Length - 1].TrailingTrivia);

            info.Kind = SyntaxKind.InterpolatedStringLiteralToken;
            info.Text = _lexer.TextWindow.GetText(intern: true);
            info.SyntaxTokenArrayValue = tokens;

            return true;

            // 创建一个构建中的插值字符串字面量文本标记。
            static BuilderStringLiteralToken createBuilderToken(Lexer lexer, in Range textRange, ArrayBuilder<string?> spanBuilder, SyntaxDiagnosticInfo[]? errors) =>
                new(
                    SyntaxKind.InterpolatedStringTextToken,
                    lexer.TextWindow.GetText(
                        textRange.Start.Value,
                        textRange.End.Value - textRange.Start.Value + 1,
                        intern: true),
                    spanBuilder,
                    0, // 默认缩进量为0，将在之后更改。
                    lexer._leadingTriviaCache.ToListNode(),
                    lexer._trailingTriviaCache.ToListNode());

            static InterpolationToken createInterpolationToken(Lexer lexer, in Interpolation interpolation, SyntaxDiagnosticInfo[]? errors) =>
                new(
                    lexer.TextWindow.GetText(
                        interpolation.StartRange.Start.Value,
                        interpolation.EndRange.End.Value - interpolation.StartRange.Start.Value + 1,
                        intern: true),
                    interpolation,
                    lexer._leadingTriviaCache.ToListNode(),
                    lexer._trailingTriviaCache.ToListNode());
        }

        private bool ScanInterpolatedStringLiteralText(
            char quote,
            ref bool hasInterpolation,
            out Range textRange,
            out ArrayBuilder<string?> spanBuilder,
            ref int minIndent)
        {
            var textRangeStart = _lexer.TextWindow.Position;

            spanBuilder = ArrayBuilder<string?>.GetInstance();
            _lexer._builder.Clear();

            while (true)
            {
                var c = _lexer.TextWindow.PeekChar();
                if (c == quote) // 字符串结尾
                {
                    _lexer.TextWindow.AdvanceChar();

                    if (_lexer._builder.Length > 0)
                        spanBuilder.Add(_lexer._builder.ToString());

                    hasInterpolation = false;
                    break;
                }
                // 字符串中可能包含非正规的UTF-16以外的字符，检查是否真正到达文本结尾来验证这些字符不是由用户代码引入的情况。
                else if (c == SlidingTextWindow.InvalidCharacter && _lexer.TextWindow.IsReallyAtEnd())
                {
                    Debug.Assert(_lexer.TextWindow.Width > 0);
                    Error = MakeError(ErrorCode.ERR_UnterminatedStringLiteral);

                    if (_lexer._builder.Length > 0)
                        spanBuilder.Add(_lexer._builder.ToString());

                    hasInterpolation = false;
                    break;
                }
                else if (SyntaxFacts.IsWhitespace(c))
                {
                    // 扫描缩进或内容（第一行）状态。
                    _lexer.TextWindow.AdvanceChar();
                    _lexer._builder.Append(c);
                }
                else
                {
                    if (spanBuilder.Count % 2 == 1) // 处于扫描缩进状态。
                    {
                        if (_lexer._builder.Length > 0)
                        {
                            spanBuilder.Add(_lexer._builder.ToString());
                            _lexer._builder.Clear();
                        }
                        else
                            spanBuilder.Add(null);
                    }

                    if (c == '\\') // 转义字符前缀
                        _lexer.ScanEscapeSequence();
                    else if (c == '#' && _lexer.TextWindow.PeekChar(1) == '{')
                    {
                        if (_lexer._builder.Length > 0)
                            spanBuilder.Add(_lexer._builder.ToString());

                        hasInterpolation = true;
                        break;
                    }
                    else if (SyntaxFacts.IsNewLine(c))
                    {
                        _lexer.TextWindow.AdvanceChar();
                        if (SyntaxFacts.IsNewLine(c, _lexer.TextWindow.PeekChar()))
                            _lexer.TextWindow.AdvanceChar();
                        _lexer._builder.Append('\n');

                        spanBuilder.Add(_lexer._builder.ToString());
                        _lexer._builder.Clear();
                    }
                    else // 普通字符
                    {
                        // 扫描内容状态。
                        _lexer.TextWindow.AdvanceChar();
                        _lexer._builder.Append(c);
                    }
                }
            }

            // 找到最小缩进量。
            if (spanBuilder.Count > 1)
            {
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
            }

            var textRangeEnd = _lexer.TextWindow.Position - 1;
            textRange = textRangeStart..textRangeEnd;
            return true;
        }

        private ArrayBuilder<SyntaxToken> ScanInterpolatedStringContent(
            out Range startRange,
            out Range endRange,
            out SyntaxToken startToken,
            out SyntaxToken endToken)
        {
            startToken = SyntaxFactory.Token(SyntaxKind.InterpolationStartToken);
            startRange = _lexer.TextWindow.Position..(_lexer.TextWindow.Position + 1);
            _lexer.TextWindow.AdvanceChar(2);

            var tokens = ArrayBuilder<SyntaxToken>.GetInstance();

            var braceBalance = 0;
            var mode = LexerMode.Syntax;
            while (true)
            {
                var token = _lexer.Lex(mode);
                switch (token.Kind)
                {
                    case SyntaxKind.OpenBraceToken:
                        braceBalance++;
                        break;
                    case SyntaxKind.CloseBraceToken:
                        // 花括号已平衡，且下一个是右花括号标记，终止枚举。
                        if (braceBalance == 0)
                        {
                            _lexer.TextWindow.Reset(_lexer.TextWindow.Position - token.GetTrailingTriviaWidth()); // 回退到右花括号的下一个字符位置。
                            endToken = SyntaxFactory.Token(SyntaxKind.InterpolationEndToken);
                            endRange = (_lexer.TextWindow.Position - 1)..(_lexer.TextWindow.Position - 1);

                            appendTrailingTrivia(token);
                            return tokens;
                        }

                        braceBalance--;
                        break;

                    // 直到文件结尾也未能平衡花括号或查看到右花括号，则产生错误。
                    case SyntaxKind.EndOfFileToken:
                        {
                            Error = MakeError(ErrorCode.ERR_UnterminatedStringLiteral);

                            endToken = SyntaxFactory.MissingToken(SyntaxKind.InterpolationEndToken);
                            endRange = _lexer.TextWindow.Position..(_lexer.TextWindow.Position - 1);

                            appendTrailingTrivia(token);
                            return tokens;
                        }
                }

                tokens.Add(token); // 枚举识别到的内部标记。
            }

            void appendTrailingTrivia(SyntaxToken token)
            {
                // 将这个右花括号标记的前方琐碎内容组合到上一个标记的后方琐碎内容中。
                var lastToken = tokens[tokens.Count - 1];
                SyntaxListBuilder triviaBuilder = new(lastToken.TrailingTrivia.Count + token.LeadingTrivia.Count);
                triviaBuilder.AddRange(lastToken.TrailingTrivia);
                triviaBuilder.AddRange(token.LeadingTrivia);
                tokens[tokens.Count - 1] = lastToken.TokenWithTrailingTrivia(triviaBuilder.ToListNode());
            }
        }
    }
}
