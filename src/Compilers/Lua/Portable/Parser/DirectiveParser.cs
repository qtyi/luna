// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class DirectiveParser
{
    public partial ThisInternalSyntaxNode ParseDirective(
        bool isActive,
        bool endIsActive,
        bool isAfterFirstTokenInFile,
        bool isAfterNonWhitespaceOnLine)
    {
        switch (CurrentTokenKind)
        {
            case SyntaxKind.HashExclamationToken:
                {
                    var hashExclamation = EatToken(SyntaxKind.HashExclamationToken);
                    if (isAfterNonWhitespaceOnLine)
                        hashExclamation = AddError(hashExclamation, ErrorCode.ERR_BadDirectivePlacement);

                    if (isAfterFirstTokenInFile || hashExclamation.HasTrailingTrivia)
                        return ParseBadDirective(hashExclamation, isActive: true);

                    // Shebang directives must appear at the first position in the file (before all other directives), so they should always be active.
                    Debug.Assert(isActive);
                    var shebangDirective = ParseShebangDirective(hashExclamation);
                    if (lexer.Options.Kind == SourceCodeKind.Regular)
                        shebangDirective = AddError(shebangDirective, ErrorCode.WRN_ShebangOnlySupportedInScript);
                    return shebangDirective;
                }

            case SyntaxKind.DollarToken:
                {
                    var dollar = EatToken(SyntaxKind.DollarToken);
                    if (isAfterNonWhitespaceOnLine)
                        dollar = AddError(dollar, ErrorCode.ERR_BadDirectivePlacement);

                    var contextualKind = CurrentToken.ContextualKind;
                    switch (contextualKind)
                    {
                        case SyntaxKind.DebugKeyword:
                            return ParseDebugDirective(dollar, EatContextualToken(contextualKind), isActive);
                        case SyntaxKind.NoDebugKeyword:
                            return ParseNoDebugDirective(dollar, EatContextualToken(contextualKind), isActive);
                        case SyntaxKind.IfKeyword:
                            return ParseIfDirective(dollar, EatContextualToken(contextualKind), isActive);
                        case SyntaxKind.IfNotKeyword:
                            return ParseIfNotDirective(dollar, EatContextualToken(contextualKind), isActive);
                        case SyntaxKind.ElseKeyword:
                            return ParseElseDirective(dollar, isActive, endIsActive);
                        case SyntaxKind.EndKeyword:
                            return ParseEndDirective(dollar, isActive, endIsActive);
                        case SyntaxKind.EndInputKeyword:
                            return ParseEndInputDirective(dollar, EatContextualToken(contextualKind), isActive);
                        default:
                            return ParseBadDirective(dollar, isActive);
                    }
                }

            default:
                throw ExceptionUtilities.Unreachable();
        }
    }

    private DirectiveTriviaSyntax ParseDebugDirective(SyntaxToken dollar, SyntaxToken keyword, bool isActive)
        => ThisInternalSyntaxFactory.DebugDirectiveTrivia(dollar, keyword, ParseEndOfDirectiveToken(ignoreErrors: false), isActive);

    private DirectiveTriviaSyntax ParseNoDebugDirective(SyntaxToken dollar, SyntaxToken keyword, bool isActive)
        => ThisInternalSyntaxFactory.NoDebugDirectiveTrivia(dollar, keyword, ParseEndOfDirectiveToken(ignoreErrors: false), isActive);

    private DirectiveTriviaSyntax ParseIfDirective(SyntaxToken dollar, SyntaxToken keyword, bool isActive)
    {
        var expr = ParseCondition();
        var eod = ParseEndOfDirectiveToken(ignoreErrors: false);
        var isTrue = Evaluate(expr);
        var branchTaken = isActive && isTrue;
        return ThisInternalSyntaxFactory.IfDirectiveTrivia(dollar, keyword, expr, eod, isActive, branchTaken, isTrue);
    }

    private DirectiveTriviaSyntax ParseIfNotDirective(SyntaxToken dollar, SyntaxToken keyword, bool isActive)
    {
        var expr = ParseCondition();
        var eod = ParseEndOfDirectiveToken(ignoreErrors: false);
        var isTrue = Evaluate(expr);
        var branchTaken = isActive && !isTrue;
        return ThisInternalSyntaxFactory.IfNotDirectiveTrivia(dollar, keyword, expr, eod, isActive, branchTaken, isTrue);
    }

    private DirectiveTriviaSyntax ParseElseDirective(SyntaxToken dollar, bool isActive, bool endIsActive)
    {
        if (_context.HasPreviousIf())
        {
            var keyword = EatContextualToken(SyntaxKind.ElseKeyword);
            var eod = ParseEndOfDirectiveToken(ignoreErrors: false);
            var branchTaken = endIsActive && !_context.PreviousBranchTaken();
            return ThisInternalSyntaxFactory.ElseDirectiveTrivia(dollar, keyword, eod, endIsActive, branchTaken);
        }
        else if (_context.HasUnfinishedIf())
            return this.AddError(ParseBadDirective(dollar, isActive), ErrorCode.ERR_EndDirectiveExpected);
        else
            return this.AddError(ParseBadDirective(dollar, isActive), ErrorCode.ERR_UnexpectedDirective);
    }

    private DirectiveTriviaSyntax ParseEndDirective(SyntaxToken dollar, bool isActive, bool endIsActive)
    {
        if (_context.HasUnfinishedIf())
        {
            var keyword = EatContextualToken(SyntaxKind.ElseKeyword);
            var eod = ParseEndOfDirectiveToken(ignoreErrors: false);
            var branchTaken = endIsActive && !_context.PreviousBranchTaken();
            return ThisInternalSyntaxFactory.EndDirectiveTrivia(dollar, keyword, eod, endIsActive);
        }
        else
            return this.AddError(ParseBadDirective(dollar, isActive), ErrorCode.ERR_UnexpectedDirective);
    }

    private ExpressionSyntax ParseCondition()
    {
        return CurrentTokenKind switch
        {
            SyntaxKind.NilKeyword => ThisInternalSyntaxFactory.LiteralExpression(SyntaxKind.NilLiteralExpression, EatToken()),
            SyntaxKind.NumericLiteralToken => ThisInternalSyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, EatToken()),
            SyntaxKind.IdentifierToken => ThisInternalSyntaxFactory.IdentifierName(EatToken()),

            _ => ThisInternalSyntaxFactory.IdentifierName(EatToken(SyntaxKind.IdentifierToken, ErrorCode.ERR_InvalidPreprocExpr))
        };
    }

    private DirectiveTriviaSyntax ParseEndInputDirective(SyntaxToken dollar, SyntaxToken keyword, bool isActive)
    {
        return ThisInternalSyntaxFactory.EndInputDirectiveTrivia(dollar, keyword, ParseEndOfDirectiveToken(ignoreErrors: false), isActive);
    }

    private bool Evaluate(ExpressionSyntax expr)
    {
        return expr switch
        {
            // nil
            LiteralExpressionSyntax { Kind: SyntaxKind.NilLiteralExpression } => false,
            // 1
            LiteralExpressionSyntax { Kind: SyntaxKind.NumericLiteralExpression, Token.Value: 1 } => true,
            // `name`
            IdentifierNameSyntax identifierName when !string.IsNullOrWhiteSpace(identifierName.Identifier.ValueText) => Options.PreprocessorSymbolNames.Contains(identifierName.Identifier.ValueText),

            _ => false,
        };
    }
}
