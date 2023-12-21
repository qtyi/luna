// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxVisitor<T> = LuaSyntaxVisitor<T>;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxVisitor<T> = MoonScriptSyntaxVisitor<T>;
#endif

internal partial class DirectiveParser : SyntaxParser
{
    internal DirectiveParser(Lexer lexer) : base(lexer, LexerMode.Directive, null, null, allowModeReset: false) { }

    public ThisSyntaxNode ParseDirective(bool isAfterFirstTokenInFile, bool isAfterNonWhitespaceOnLine)
    {
        switch (this.CurrentTokenKind)
        {
            case SyntaxKind.HashExclamationToken:
                {
                    var hashExclamation = this.EatToken(SyntaxKind.HashExclamationToken);
                    if (isAfterNonWhitespaceOnLine)
                        hashExclamation = this.AddError(hashExclamation, ErrorCode.ERR_BadDirectivePlacement);

                    if (this.lexer.Options.Kind == SourceCodeKind.Script && !isAfterFirstTokenInFile && !hashExclamation.HasTrailingTrivia)
                        return this.ParseShebangDirective(hashExclamation);

                    return this.ParseBadDirective(hashExclamation);
                }

            case SyntaxKind.HashToken:
                {
                    var hash = this.EatToken(SyntaxKind.HashToken);
                    if (isAfterNonWhitespaceOnLine)
                        hash = this.AddError(hash, ErrorCode.ERR_BadDirectivePlacement);

                    if (this.lexer.Options.Kind == SourceCodeKind.Script && !isAfterFirstTokenInFile)
                        return this.ParseCommentDirective(hash);

                    return this.ParseBadDirective(hash);
                }

            default:
                throw ExceptionUtilities.Unreachable();
        }
    }

    private BadDirectiveTriviaSyntax ParseBadDirective(SyntaxToken promptToken) =>
        SyntaxFactory.BadDirectiveTrivia(promptToken, this.ParseEndOfDirectiveToken(ignoreErrors: false));

    private ShebangDirectiveTriviaSyntax ParseShebangDirective(SyntaxToken hashExclamation) =>
        SyntaxFactory.ShebangDirectiveTrivia(hashExclamation, this.ParseEndOfDirectiveTokenWithOptionalPreprocessingMessage());

    private CommentDirectiveTriviaSyntax ParseCommentDirective(SyntaxToken hash) => SyntaxFactory.CommentDirectiveTrivia(hash, this.ParseEndOfDirectiveTokenWithOptionalPreprocessingMessage());

    private SyntaxToken ParseEndOfDirectiveTokenWithOptionalPreprocessingMessage() =>
        this.lexer.LexEndOfDirectiveWithOptionalPreprocessingMessage();

    private SyntaxToken ParseEndOfDirectiveToken(bool ignoreErrors)
    {
        var skippedTokens = SyntaxListBuilder<SyntaxToken>.Create();
        this.SkipTokens(skippedTokens, static token => token.Kind is not SyntaxKind.EndOfDirectiveToken or SyntaxKind.EndOfFileToken, visitor: new DirectiveTokenVisitor(ignoreErrors));

        var endOfDirective = this.CurrentTokenKind == SyntaxKind.EndOfDirectiveToken ? this.EatToken() : SyntaxFactory.Token(SyntaxKind.EndOfDirectiveToken);

        if (skippedTokens.Count > 0)
            endOfDirective = endOfDirective.TokenWithLeadingTrivia(SyntaxFactory.SkippedTokensTrivia(skippedTokens.ToList()));

        return endOfDirective;
    }

    private sealed class DirectiveTokenVisitor : ThisSyntaxVisitor<SyntaxToken>
    {
        private readonly bool _ignoreErrors;

        public DirectiveTokenVisitor(bool ignoreErrors)
        {
            this._ignoreErrors = ignoreErrors;
        }

        public override SyntaxToken? VisitToken(SyntaxToken token)
        {
            if (token is not null && this._ignoreErrors)
                return token.WithoutDiagnosticsGreen();

            return token;
        }
    }
}
