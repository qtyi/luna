// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal partial class DirectiveParser : SyntaxParser
{
    internal DirectiveParser(Lexer lexer) : base(lexer, LexerMode.Directive, null, null, allowModeReset: false) { }

    public ThisInternalSyntaxNode ParseDirective(bool isAfterFirstTokenInFile, bool isAfterNonWhitespaceOnLine)
    {
#warning Need two more parameter: bool isActive, bool endIsActive
        switch (CurrentTokenKind)
        {
            case SyntaxKind.HashExclamationToken:
                {
                    var hashExclamation = EatToken(SyntaxKind.HashExclamationToken);
                    if (isAfterNonWhitespaceOnLine)
                        hashExclamation = AddError(hashExclamation, ErrorCode.ERR_BadDirectivePlacement);

                    if (lexer.Options.Kind == SourceCodeKind.Script && !isAfterFirstTokenInFile && !hashExclamation.HasTrailingTrivia)
                        return ParseShebangDirective(hashExclamation, /*isActive*/true);

                    return ParseBadDirective(hashExclamation, /*isActive*/true);
                }

            case SyntaxKind.HashToken:
                {
                    var hash = EatToken(SyntaxKind.HashToken);
                    if (isAfterNonWhitespaceOnLine)
                        hash = AddError(hash, ErrorCode.ERR_BadDirectivePlacement);

                    if (lexer.Options.Kind == SourceCodeKind.Script && !isAfterFirstTokenInFile)
                        return ParseCommentDirective(hash, /*isActive*/true);

                    return ParseBadDirective(hash, /*isActive*/true);
                }

            default:
                throw ExceptionUtilities.Unreachable();
        }
    }

    private BadDirectiveTriviaSyntax ParseBadDirective(SyntaxToken promptToken, bool isActive) =>
        ThisInternalSyntaxFactory.BadDirectiveTrivia(promptToken, ParseEndOfDirectiveToken(ignoreErrors: false), isActive);

    private ShebangDirectiveTriviaSyntax ParseShebangDirective(SyntaxToken hashExclamation, bool isActive) =>
        ThisInternalSyntaxFactory.ShebangDirectiveTrivia(hashExclamation, ParseEndOfDirectiveTokenWithOptionalPreprocessingMessage(), isActive);

    private CommentDirectiveTriviaSyntax ParseCommentDirective(SyntaxToken hash, bool isActive) => ThisInternalSyntaxFactory.CommentDirectiveTrivia(hash, ParseEndOfDirectiveTokenWithOptionalPreprocessingMessage(), isActive);

    private SyntaxToken ParseEndOfDirectiveTokenWithOptionalPreprocessingMessage() =>
        lexer.LexEndOfDirectiveWithOptionalPreprocessingMessage();

    private SyntaxToken ParseEndOfDirectiveToken(bool ignoreErrors)
    {
        var skippedTokens = SyntaxListBuilder<SyntaxToken>.Create();
        SkipTokens(skippedTokens, static token => token.Kind is not SyntaxKind.EndOfDirectiveToken or SyntaxKind.EndOfFileToken, visitor: new DirectiveTokenVisitor(ignoreErrors));

        var endOfDirective = CurrentTokenKind == SyntaxKind.EndOfDirectiveToken ? EatToken() : ThisInternalSyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken);

        if (skippedTokens.Count > 0)
            endOfDirective = endOfDirective.TokenWithLeadingTrivia(ThisInternalSyntaxFactory.SkippedTokensTrivia(skippedTokens.ToList()));

        return endOfDirective;
    }

    private sealed class DirectiveTokenVisitor : ThisInternalSyntaxVisitor<SyntaxToken>
    {
        private readonly bool _ignoreErrors;

        public DirectiveTokenVisitor(bool ignoreErrors)
        {
            _ignoreErrors = ignoreErrors;
        }

        public override SyntaxToken? VisitToken(SyntaxToken token)
        {
            if (token is not null && _ignoreErrors)
                return token.WithoutDiagnosticsGreen();

            return token;
        }
    }
}
