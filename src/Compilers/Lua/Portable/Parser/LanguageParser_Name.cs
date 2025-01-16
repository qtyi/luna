// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class LanguageParser
{
#if TESTING
    internal
#else
    private
#endif
        IdentifierNameSyntax ParseIdentifierName()
    {
        var identifier = EatToken(SyntaxKind.IdentifierToken);
        return _syntaxFactory.IdentifierName(identifier);
    }

    private IdentifierNameSyntax ParseAsIdentifierName()
    {
        var identifier = EatTokenAsKind(SyntaxKind.IdentifierToken);
        return _syntaxFactory.IdentifierName(identifier);
    }

#if TESTING
    internal
#else
    private
#endif
        NameSyntax ParseName()
    {
        NameSyntax left = ParseIdentifierName();
        // QualifiedName
        while (CurrentTokenKind == SyntaxKind.DotToken)
        {
            var dot = EatToken(SyntaxKind.DotToken);
            IdentifierNameSyntax right;
            if (CurrentTokenKind != SyntaxKind.IdentifierToken)
            {
                dot = AddError(dot, ErrorCode.ERR_IdentifierExpected);
                right = CreateMissingIdentifierName();
                left = _syntaxFactory.QualifiedName(left, dot, right);
            }
            else
            {
                right = ParseIdentifierName();
                left = _syntaxFactory.QualifiedName(left, dot, right);
            }
        }

        // ImplicitSelfParameterName
        if (CurrentTokenKind == SyntaxKind.ColonToken)
        {
            var colon = EatToken(SyntaxKind.ColonToken);
            IdentifierNameSyntax right;
            if (CurrentTokenKind != SyntaxKind.IdentifierToken)
            {
                colon = AddError(colon, ErrorCode.ERR_IdentifierExpected);
                right = CreateMissingIdentifierName();
                left = _syntaxFactory.ImplicitSelfParameterName(left, colon, right);
            }
            else
            {
                right = ParseIdentifierName();
                left = _syntaxFactory.ImplicitSelfParameterName(left, colon, right);
            }

            // 将后续可能的QualifiedName及ImplicitSelfParameterName结构视为错误。
            if (CurrentTokenKind is SyntaxKind.DotToken or SyntaxKind.ColonToken)
            {
                var unexpectedChar = SyntaxFacts.GetText(CurrentTokenKind);
                var builder = _pool.Allocate<SyntaxToken>();
                do
                {
                    builder.Add(EatToken());
                    if (CurrentTokenKind == SyntaxKind.IdentifierToken)
                        builder.Add(EatToken());
                }
                while (CurrentTokenKind is SyntaxKind.DotToken or SyntaxKind.ColonToken);
                var skippedTokensTrivia = _syntaxFactory.SkippedTokensTrivia(_pool.ToListAndFree(builder));
                skippedTokensTrivia = AddError(skippedTokensTrivia, ErrorCode.ERR_UnexpectedCharacter, unexpectedChar);

                left = AddTrailingSkippedSyntax(left, skippedTokensTrivia);
            }
        }

        return left;
    }

#if TESTING
    internal
#else
    private
#endif
        IdentifierNameSyntax CreateMissingIdentifierName() => _syntaxFactory.IdentifierName(CreateMissingIdentifierToken());

#if TESTING
    internal
#else
    private
#endif
        static SyntaxToken CreateMissingIdentifierToken() => ThisInternalSyntaxFactory.MissingToken(SyntaxKind.IdentifierToken);
}
