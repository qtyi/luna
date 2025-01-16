// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal partial class DirectiveParser : SyntaxParser
{
    private DirectiveStack _context;

    internal DirectiveParser(Lexer lexer) : base(lexer, LexerMode.Directive, oldTree: null, changes: null, allowModeReset: false) { }

    public void ReInitialize(DirectiveStack context)
    {
        ReInitialize();
        _context = context;
    }

    public partial ThisInternalSyntaxNode ParseDirective(
        bool isActive,
        bool endIsActive,
        bool isAfterFirstTokenInFile,
        bool isAfterNonWhitespaceOnLine);

    private DirectiveTriviaSyntax ParseBadDirective(SyntaxToken prompt, bool isActive)
        => ThisInternalSyntaxFactory.BadDirectiveTrivia(prompt, ParseEndOfDirectiveToken(ignoreErrors: false), isActive);

    private DirectiveTriviaSyntax ParseShebangDirective(SyntaxToken hashExclamation)
        => ThisInternalSyntaxFactory.ShebangDirectiveTrivia(hashExclamation, ParseEndOfDirectiveTokenWithOptionalPreprocessingMessage(trimEnd: true), isActive: true);

    private SyntaxToken ParseEndOfDirectiveTokenWithOptionalPreprocessingMessage(bool trimEnd)
        => lexer.LexEndOfDirectiveWithOptionalPreprocessingMessage(trimEnd: trimEnd);

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
