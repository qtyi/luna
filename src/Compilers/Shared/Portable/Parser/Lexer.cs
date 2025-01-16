// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal partial class Lexer : AbstractLexer
{
    /// <summary>Initial capacity of identifier characters buffer.</summary>
    private const int IdentifierBufferInitialCapacity = 32;
    /// <summary>Initial capacity of trivia list.</summary>
    private const int TriviaListInitialCapacity = 8;

    /// <summary>Parse options, like language version, documentation mode, source code kind, features, etc..</summary>
    private readonly ThisParseOptions _options;

    /// <summary>Current mode of this lexer.</summary>
    private LexerMode _mode;
    /// <summary><see cref="char"/> buffer for UTF-16 text.</summary>
    private readonly StringBuilder _builder;
    /// <summary><see cref="byte"/> buffer for UTF-8 text.</summary>
    private readonly ArrayBuilder<byte> _utf8Builder;
    /// <summary>Identifier characters buffer.</summary>
    private char[] _identifierBuffer;
    /// <summary>Count of available characters in <see cref="_identifierBuffer"/>.</summary>
    private int _identifierLength;
    /// <summary>Cache of this lexer.</summary>
    private readonly LexerCache _cache;
    /// <summary>Cumulative count of bad tokens produced.</summary>
    private int _badTokenCount;

    /// <summary>
    /// Gets parse options for this lexer.
    /// </summary>
    /// <value>
    /// Parse options.
    /// </value>
    public ThisParseOptions Options => _options;

    /// <summary>
    /// Gets a value indicating whether the documentation comments are treated as regular comments.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if the documentation comments are treated as regular comments; otherwise, <see langword="false"/>.
    /// </value>
    public bool SuppressDocumentationCommentParse => _options.DocumentationMode < DocumentationMode.Parse;

    /// <summary>All directives we parsed.</summary>
    private DirectiveStack _directives;
    /// <summary>Parser to parse directives.</summary>
    /// <remarks>PERF: Expensive to continually recreate.  So just initialize/reinitialize on demand.</remarks>
    private DirectiveParser? _directiveParser;

    /// <summary>
    /// Gets a stack of directives we parsed.
    /// </summary>
    public DirectiveStack Directives => _directives;

    /// <summary>
    /// Create a new instance of <see cref="Lexer"/> type.
    /// </summary>
    /// <param name="options">Parse options for the lexer.</param>
    /// <param name="allowPreprocessorDirectives">A value indecates whether we allow preprocessor directives.</param>
    /// <inheritdoc/>
    public Lexer(SourceText text, ThisParseOptions options) : base(text)
    {
        Debug.Assert(options is not null);

        _options = options;
        _builder = new();
        _utf8Builder = ArrayBuilder<byte>.GetInstance();
        _identifierBuffer = new char[IdentifierBufferInitialCapacity];
        _cache = new();
    }

    /// <inheritdoc/>
    public override void Dispose()
    {
        _cache.Free();
        _utf8Builder.Free();

        _directiveParser?.Dispose();

        base.Dispose();
    }

    /// <summary>
    /// Reset offset of text windows of this lexer to specified character position and reset directive stack.
    /// </summary>
    /// <param name="position">Character position to reset.</param>
    /// <param name="directives">Stack of directives to reset.</param>
    public void Reset(int position, DirectiveStack directives)
    {
        TextWindow.Reset(position);
        _directives = directives;
    }

    /// <summary>
    /// Add specified trivia to trivia list, report diagnostics if trivia contains errors.
    /// </summary>
    /// <param name="trivia">Syntax trivia to add.</param>
    /// <param name="list">Syntax trivia list to add to, not <see langword="null"/> when returns.</param>
    private void AddTrivia(ThisInternalSyntaxNode trivia, [NotNull] ref SyntaxListBuilder? list)
    {
        if (HasErrors)
            trivia = trivia.WithDiagnosticsGreen(GetErrors(leadingTriviaWidth: 0));

        list ??= new(TriviaListInitialCapacity);

        list.Add(trivia);
    }

    /// <summary>
    /// Gets lex mode flags.
    /// </summary>
    /// <param name="mode">Lexer mode enum value.</param>
    /// <returns><paramref name="mode"/>Lex mode flags of <paramref name="mode"/>.</returns>
    private static partial LexerMode ModeOf(LexerMode mode);

    /// <summary>
    /// Checks if lex mode flags of current lexer mode the same as specified enum value.
    /// </summary>
    /// <param name="mode">Lexer mode enum value to compare.</param>
    /// <returns>Returns <see langword="true"/> if lex mode flags of <see cref="_mode"/> the same as <paramref name="mode"/>; otherwise, <see langword="false"/>.</returns>
    private bool ModeIs(LexerMode mode) => ModeOf(_mode) == mode;

    /// <summary>
    /// Lexes a token using specified lexer mode and adjustify to new lexer mode that is the current mode after lexing.
    /// </summary>
    /// <param name="mode">Lexer mode enum values for lexing.</param>
    /// <returns>Syntax token the result of lexing.</returns>
    public SyntaxToken Lex(ref LexerMode mode)
    {
        var result = Lex(mode);
        mode = _mode;
        return result;
    }

    /// <summary>
    /// Lexes a token using specified lexer mode。
    /// </summary>
    /// <param name="mode">Lexer mode enum values for lexing.</param>
    /// <returns>Syntax token the result of lexing.</returns>
    public partial SyntaxToken Lex(LexerMode mode);

    /// <summary>Cache syntax list for leading trivia.</summary>
    private SyntaxListBuilder _leadingTriviaCache = new(TriviaListInitialCapacity);
    /// <summary>Cache syntax list for trailing trivia.</summary>
    private SyntaxListBuilder _trailingTriviaCache = new(TriviaListInitialCapacity);
    /// <summary>Cache syntax list for directive trivia.</summary>
    /// <remarks>PERF: Expensive to continually recreate.  So just initialize/reinitialize on demand.</remarks>
    private SyntaxListBuilder? _directiveTriviaCache;

    /// <summary>
    /// Creates a syntax token.
    /// </summary>
    /// <param name="info">An object collects information about a syntax token.</param>
    /// <param name="leading">Syntax list for leading trivia.</param>
    /// <param name="trailing">Syntax list for trailing trivia.</param>
    /// <param name="errors">Syntax diagnostics collected.</param>
    /// <returns>A syntax token</returns>
    private partial SyntaxToken Create(
        in TokenInfo info,
        SyntaxListBuilder? leading,
        SyntaxListBuilder? trailing,
        SyntaxDiagnosticInfo[]? errors);

    /// <summary>
    /// Gets full character width of trivia list. Returns 0 if <paramref name="builder"/> is <see langword="null"/>.
    /// </summary>
    private static int GetFullWidth(SyntaxListBuilder? builder)
    {
        if (builder is null) return 0;

        var width = 0;
        for (var i = 0; i < builder.Count; i++)
        {
            var node = builder[i];
            Debug.Assert(node is not null);
            width += node.FullWidth;
        }

        return width;
    }

    /// <summary>
    /// Lexes a syntax token.
    /// </summary>
    /// <returns>Syntax token the result of lexing.</returns>
    private SyntaxToken LexSyntaxToken()
    {
        // Scan leading trivia of a token.
        ScanSyntaxLeadingTrivia();
        var leading = _leadingTriviaCache;

        // Scan token and get info.
        TokenInfo tokenInfo = default;
        Start();
        ScanSyntaxToken(ref tokenInfo);
        var errors = GetErrors(GetFullWidth(leading));

        // Scan trailing trivia of a token.
        ScanSyntaxTrailingTrivia();
        var trailing = _trailingTriviaCache;

        return Create(in tokenInfo, leading, trailing, errors);
    }

    /// <summary>
    /// Lexes a series of leading syntax trivia and returns in a syntax list.
    /// </summary>
    /// <returns>Syntax trivia list the result of lexing.</returns>
    internal SyntaxTriviaList LexSyntaxLeadingTrivia()
    {
        ScanSyntaxLeadingTrivia();
        return new(
            token: default,
            node: _leadingTriviaCache.ToListNode(),
            position: 0,
            index: 0);
    }

    /// <summary>
    /// Lexes a new-line sequence
    /// </summary>
    /// <returns>Syntax trivia the result of lexing.</returns>
    private partial SyntaxTrivia? LexEndOfLine();

    /// <summary>
    /// Lexes a comment.
    /// </summary>
    /// <returns>Syntax trivia the result of lexing.</returns>
    private partial SyntaxTrivia LexComment();

    /// <summary>
    /// Lexes a directive syntax token.
    /// </summary>
    /// <returns>Directive syntax token the result of lexing.</returns>
    private SyntaxToken LexDirectiveToken()
    {
        // Scan directive token and get info.
        TokenInfo info = default;
        Start();
        ScanDirectiveToken(ref info);
        var errors = GetErrors(leadingTriviaWidth: 0);

        // Scan directive trailing trivia of a token.
        _directiveTriviaCache?.Clear();
        ScanDirectiveTrailingTrivia(
            includeEndOfLine: info.Kind == SyntaxKind.EndOfDirectiveToken,
            triviaList: ref _directiveTriviaCache);

        return Create(in info, leading: null, trailing: _directiveTriviaCache, errors);
    }

    /// <summary>
    /// Lexes a directive syntax trivia.
    /// </summary>
    /// <returns>Directive syntax trivia the result of lexing.</returns>
    private partial ThisInternalSyntaxNode? LexDirectiveTrivia();

    /// <summary>
    /// Lexes a directive syntax trivia and ignores its preprocessing message.
    /// </summary>
    /// <returns>Directive syntax trivia the result of lexing.</returns>
    public SyntaxToken LexEndOfDirectiveWithOptionalPreprocessingMessage(bool trimEnd = false)
    {
        var builder = PooledStringBuilder.GetInstance();
        // Skip the rest of the line until we hit a EOL or EOF.  This follows the PP_Message portion of the specification.
        ScanToEndOfLine(builder: builder.Builder, trimStart: true, trimEnd: trimEnd);

        SyntaxTrivia? leading;
        if (builder.Length == 0)
        {
            leading = null;
            builder.Free();
        }
        else
            leading = ThisInternalSyntaxFactory.PreprocessingMessage(builder.ToStringAndFree());

        // now try to consume the EOL if there.
        ScanDirectiveTrailingTrivia(
            includeEndOfLine: true,
            triviaList: ref _directiveTriviaCache);
        var trailing = _directiveTriviaCache?.ToListNode();
        var endOfDirective = ThisInternalSyntaxFactory.Token(leading, SyntaxKind.EndOfDirectiveToken, trailing);

        return endOfDirective;
    }

    /// <summary>
    /// Scans a syntax token.
    /// </summary>
    /// <param name="info">An object that collects information about a syntax token.</param>
    private partial void ScanSyntaxToken(ref TokenInfo info);

    /// <summary>
    /// Scans a directive syntax token.
    /// </summary>
    /// <param name="info">An object that collects information about a directive syntax token.</param>
    private partial bool ScanDirectiveToken(ref TokenInfo info);

    /// <summary>
    /// Scans a numeric literal.
    /// </summary>
    /// <param name="info">An object that collects information about a syntax token.</param>
    /// <returns>Returns <see langword="true"/> if we find a numeric literal; otherwise, <see langword="false"/>.</returns>
    private partial bool ScanNumericLiteral(ref TokenInfo info);

    /// <summary>
    /// Scans a string literal.
    /// </summary>
    /// <param name="info">An object that collects information about a syntax token.</param>
    /// <returns>Returns <see langword="true"/> if we find a string literal; otherwise, <see langword="false"/>.</returns>
    private partial bool ScanStringLiteral(ref TokenInfo info);

    /// <summary>
    /// Scans a multi-line raw string literal.
    /// </summary>
    /// <param name="info">An object that collects information about a syntax token.</param>
    /// <returns>Returns <see langword="true"/> if we find a string literal; otherwise, <see langword="false"/>.</returns>
    private partial void ScanMultiLineRawStringLiteral(ref TokenInfo info, int level = -1);

    /// <summary>
    /// 将UTF-16字符串从<see cref="_builder"/>中转换成UTF-8字节序列，并输入到<see cref="_utf8Builder"/>中。
    /// </summary>
    /// <param name="additionalBytes">在后方追加的字节序列。</param>
#warning Documentation comment needs globalize.
    // TODO: Intern Utf8String directly by TextWindow.
    private void FlushToUtf8Builder(params byte[] additionalBytes)
    {
        if (_builder.Length != 0)
        {
            var strValue = TextWindow.Intern(_builder);
            _builder.Length = 0;

            var utf8Bytes = Encoding.UTF8.GetBytes(strValue);
            _utf8Builder.AddRange(utf8Bytes);
        }

        _utf8Builder.AddRange(additionalBytes);
    }

    /// <summary>
    /// Scans an identifier or a keyword.
    /// </summary>
    /// <param name="info">An object that collects information about a syntax token.</param>
    /// <returns>Returns <see langword="true"/> if we find an identifier or a keyword; otherwise, <see langword="false"/>.</returns>
    private partial bool ScanIdentifierOrKeyword(ref TokenInfo info);

    /// <summary>
    /// Scans a syntax trivia.
    /// </summary>
    /// <param name="afterFirstToken">Set to <see langword="false"/> if we are scanning before the first syntax token; otherwise, <see langword="true"/>.</param>
    /// <param name="isTrailing">Set to <see langword="false"/> if we are scanning leading syntax trivia; otherwise, set to <see langword="true"/> that we are scanning trailing syntax trivia.</param>
    /// <param name="triviaList">Syntax List to collect syntax trivia we scanned.</param>
    private partial void ScanSyntaxTrivia(
        bool afterFirstToken,
        bool isTrailing,
        ref SyntaxListBuilder triviaList);

    /// <summary>
    /// Lexes a series of trailing syntax trivia and returns in a syntax list.
    /// </summary>
    /// <returns>Syntax trivia list the result of lexing.</returns>
    internal SyntaxTriviaList LexSyntaxTrailingTrivia()
    {
        ScanSyntaxTrailingTrivia();
        return new(
            token: default,
            node: _trailingTriviaCache.ToListNode(),
            position: 0,
            index: 0);
    }

    /// <summary>
    /// Scans a series of leading syntax trivia and adds to cache list.
    /// </summary>
    private void ScanSyntaxLeadingTrivia()
    {
        _leadingTriviaCache.Clear();
        ScanSyntaxTrivia(
            afterFirstToken: TextWindow.Position > 0,
            isTrailing: false,
            triviaList: ref _leadingTriviaCache);
    }

    /// <summary>
    /// Scans a series of trailing syntax trivia and adds to cache list.
    /// </summary>
    private void ScanSyntaxTrailingTrivia()
    {
        _trailingTriviaCache.Clear();
        ScanSyntaxTrivia(
            afterFirstToken: true,
            isTrailing: true,
            triviaList: ref _trailingTriviaCache);
    }

    /// <summary>
    /// Scans a syntax trivia.
    /// </summary>
    /// <param name="afterFirstToken">Set to <see langword="false"/> if we are scanning before the first syntax token; otherwise, <see langword="true"/>.</param>
    /// <param name="afterNonWhitespaceOnLine"></param>
    /// <param name="triviaList">Syntax List to collect syntax trivia we scanned.</param>
#warning Needs documentation for parameter `afterNonWhitespaceOnLine`.
    private partial void ScanDirectiveAndExcludedTrivia(
        bool afterFirstToken,
        bool afterNonWhitespaceOnLine,
        ref SyntaxListBuilder triviaList);

    /// <summary>
    /// Scans a series of trailing syntax trivia of directive trivia and adds to cache list.
    /// </summary>
    /// <param name="includeEndOfLine">Set to <see langword="true"/> if EOL should be included; otherwise, <see langword="false"/>.</param>
    /// <param name="triviaList">Syntax List to collect syntax trivia we scanned.</param>
    private void ScanDirectiveTrailingTrivia(bool includeEndOfLine, ref SyntaxListBuilder? triviaList)
    {
        while (true)
        {
            var position = TextWindow.Position;
            var trivia = LexDirectiveTrivia();
            if (trivia is null)
                break;
            else if (trivia.Kind == SyntaxKind.EndOfLineTrivia)
            {
                if (includeEndOfLine)
                    AddTrivia(trivia, ref triviaList);
                else
                    TextWindow.Reset(position);

                break;
            }
            else
                AddTrivia(trivia, ref triviaList);
        }
    }

    /// <summary>
    /// Scans to the end of the line and buffers characters in a <see cref="StringBuilder"/>.
    /// </summary>
    /// <param name="builder">String builder to collect characters we scanned.</param>
    /// <param name="trimStart">Set to <see langword="true"/> to remove all whitespaces at the start; otherwise, <see langword="false"/>.</param>
    /// <param name="trimEnd">Set to <see langword="true"/> to remove all whitespaces at the end; otherwise, <see langword="false"/>.</param>
    private void ScanToEndOfLine(
        StringBuilder? builder = null,
        bool trimStart = true,
        bool trimEnd = true)
    {
        builder?.Clear();

        var length = 0;
        for (
            var c = TextWindow.PeekChar();
            !SyntaxFacts.IsNewLine(c) && (c != SlidingTextWindow.InvalidCharacter || !TextWindow.IsReallyAtEnd());
            c = TextWindow.PeekChar()
        )
        {
            var isWhitespace = SyntaxFacts.IsWhitespace(c);
            if (builder is not null &&
                (!trimStart || length != 0 || !isWhitespace))
            {
                builder.Append(c);

                if (!trimEnd || !isWhitespace)
                    length = builder.Length;
            }
            TextWindow.AdvanceChar();
        }

        if (builder is not null)
            builder.Length = length;
    }

    /// <summary>
    /// Scans a paired long brackets (<c>[=[ ]=]</c>) (for string literal and comment).
    /// </summary>
    /// <param name="closed">Sets to <see langword="true"/> if the brackets closed correctly; otherwise, <see langword="false"/>.</param>
    /// <param name="level">Represents the level of long brackets if set to non negative; otherwise any possible level.</param>
    /// <returns>Returns <see langword="true"/> if we find a long brackets with correct level; otherwise, <see langword="false"/>.</returns>
    private bool ScanLongBrackets(out bool closed, int level = -1)
    {
        _builder.Clear();

        var start = TextWindow.Position;

        // First character must be `[` and second character must be `[` or `=`.
        if (TextWindow.PeekChar() != '[' || TextWindow.PeekChar(1) is not '[' and not '=')
            goto Invalid;

        // Check or get level
        if (level >= 0)
        {
            TextWindow.AdvanceChar();
            for (var i = 0; i < level; i++)
            {
                if (TextWindow.NextChar() != '=')
                    goto Invalid;
            }
            if (TextWindow.NextChar() != '[')
                goto Invalid;
        }
        else
        {
            TextWindow.AdvanceChar();
            level = 0;
            while (true)
            {
                var c = TextWindow.NextChar();
                if (c == '=')
                    level++;
                else if (c == '[')
                    break;
                else // invalid
                    goto Invalid;
            }
        }

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

            // Meet possible close short brackets.
            if (c == ']')
            {
                // Check level.
                for (var i = 1; i <= level; i++)
                {
                    if (TextWindow.PeekChar(i) != '=') // not match
                        goto NormalChar;
                }

                // Correctly paired.
                if (TextWindow.PeekChar(level + 1) == ']')
                {
                    TextWindow.AdvanceChar(level + 2);
                    closed = true;
                    break;
                }
            }

NormalChar:
            
#pragma warning disable IDE0055
            // Append LF if we meet EOL.
#pragma warning restore IDE0055
            if (SyntaxFacts.IsNewLine(c))
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

        return true;

Invalid:
        TextWindow.Reset(start);
        closed = default;
        return false;
    }

    /// <summary>
    /// Add character to <see cref="_identifierBuffer"/>;
    /// </summary>
    /// <param name="c">要添加的字符，将会是标识符的一部分。</param>
    private void AddIdentifierChar(char c)
    {
        if (_identifierLength >= _identifierBuffer.Length)
            GrowIdentifierBuffer();

        _identifierBuffer[_identifierLength++] = c;
    }

    /// <summary>
    /// Expand capacity of <see cref="_identifierBuffer"/>.
    /// </summary>
    private void GrowIdentifierBuffer()
    {
        var tmp = new char[_identifierBuffer.Length * 2];
        Array.Copy(_identifierBuffer, tmp, _identifierBuffer.Length);
        _identifierBuffer = tmp;
    }

    /// <summary>
    /// Clear <see cref="_identifierBuffer"/>.
    /// </summary>
    private void ResetIdentifierBuffer()
    {
        _identifierLength = 0;
    }

    /// <summary>
    /// Checks if a punctuation token is avaliable in current language version.
    /// </summary>
    /// <param name="kind">Syntax kind that represents a punctuation.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> represents an avaliable punctuation token in current language version specified by <see cref="Options"/>; otherwise, <see langword="false"/>.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private bool IsPunctuationAvaliable(SyntaxKind kind) => SyntaxFacts.IsPunctuation(kind, Options.LanguageVersion);

    /// <summary>
    /// Checks if we ignore the new-line directly after open brackets of multi-line string literal in current language version.
    /// </summary>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> represents an avaliable punctuation token in current language version specified by <see cref="Options"/>; otherwise, <see langword="false"/>.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    private partial bool IgnoreNewLineDirectlyAfterOpenBrackets();

    private void CheckFeatureAvaliability(MessageID feature)
    {
        var info = feature.GetFeatureAvailabilityDiagnosticInfo(Options);
        if (info is not null)
            AddError(info.Code, info.Arguments);
    }
}
