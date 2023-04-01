// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

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
        var identifier = this.EatToken(SyntaxKind.IdentifierToken);
        return this._syntaxFactory.IdentifierName(identifier);
    }

    private IdentifierNameSyntax ParseAsIdentifierName()
    {
        var identifier = this.EatTokenAsKind(SyntaxKind.IdentifierToken);
        return this._syntaxFactory.IdentifierName(identifier);
    }

#if TESTING
    internal
#else
    private
#endif
        NameSyntax ParseName()
    {
        NameSyntax left = this.ParseIdentifierName();
        // QualifiedName
        while (this.CurrentTokenKind == SyntaxKind.DotToken)
        {
            var dot = this.EatToken(SyntaxKind.DotToken);
            IdentifierNameSyntax right;
            if (this.CurrentTokenKind != SyntaxKind.IdentifierToken)
            {
                dot = this.AddError(dot, ErrorCode.ERR_IdentifierExpected);
                right = this.CreateMissingIdentifierName();
                left = this._syntaxFactory.QualifiedName(left, dot, right);
            }
            else
            {
                right = this.ParseIdentifierName();
                left = this._syntaxFactory.QualifiedName(left, dot, right);
            }
        }

        // ImplicitSelfParameterName
        if (this.CurrentTokenKind == SyntaxKind.ColonToken)
        {
            var colon = this.EatToken(SyntaxKind.ColonToken);
            IdentifierNameSyntax right;
            if (this.CurrentTokenKind != SyntaxKind.IdentifierToken)
            {
                colon = this.AddError(colon, ErrorCode.ERR_IdentifierExpected);
                right = this.CreateMissingIdentifierName();
                left = this._syntaxFactory.ImplicitSelfParameterName(left, colon, right);
            }
            else
            {
                right = this.ParseIdentifierName();
                left = this._syntaxFactory.ImplicitSelfParameterName(left, colon, right);
            }

            // 将后续可能的QualifiedName及ImplicitSelfParameterName结构视为错误。
            if (this.CurrentTokenKind is SyntaxKind.DotToken or SyntaxKind.ColonToken)
            {
                var unexpectedChar = SyntaxFacts.GetText(this.CurrentTokenKind);
                var builder = this._pool.Allocate<SyntaxToken>();
                do
                {
                    builder.Add(this.EatToken());
                    if (this.CurrentTokenKind == SyntaxKind.IdentifierToken)
                        builder.Add(this.EatToken());
                }
                while (this.CurrentTokenKind is SyntaxKind.DotToken or SyntaxKind.ColonToken);
                var skippedTokensTrivia = this._syntaxFactory.SkippedTokensTrivia(this._pool.ToListAndFree(builder));
                skippedTokensTrivia = this.AddError(skippedTokensTrivia, ErrorCode.ERR_UnexpectedCharacter, unexpectedChar);

                left = this.AddTrailingSkippedSyntax(left, skippedTokensTrivia);
            }
        }

        return left;
    }

#if TESTING
    internal
#else
    private
#endif
        IdentifierNameSyntax CreateMissingIdentifierName() => this._syntaxFactory.IdentifierName(LanguageParser.CreateMissingIdentifierToken());

#if TESTING
    internal
#else
    private
#endif
        static SyntaxToken CreateMissingIdentifierToken() => SyntaxFactory.MissingToken(SyntaxKind.IdentifierToken);
}
