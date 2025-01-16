// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class LanguageParser
{
#if TESTING
    internal
#else
    private
#endif
        SeparatedSyntaxList<ExpressionSyntax> ParseExpressionList(int minCount = 1)
    {
        Debug.Assert(minCount >= 0);

        return ParseSeparatedSyntaxList(
            predicateNode: index => index < minCount || IsPossibleExpression(),
            parseNode: (_, missing) =>
            {
                if (!missing && IsPossibleExpression())
                    return ParseExpression();
                else
                    return ReportMissingExpression(CreateMissingIdentifierName());
            },
            predicateSeparator: _ => CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => EatToken(SyntaxKind.CommaToken),
            minCount: minCount);
    }

#if TESTING
    internal
#else
    private
#endif
        SeparatedSyntaxList<ExpressionSyntax> CreateMissingExpressionList() =>
        new(SyntaxList.List(
            ReportMissingExpression(CreateMissingIdentifierName())
        ));

#if TESTING
    internal
#else
    private
#endif
        SeparatedSyntaxList<ExpressionSyntax> CreateMissingExpressionList(ErrorCode code, params object[] args) =>
        new(SyntaxList.List(
            AddError(CreateMissingIdentifierName(), code, args)
        ));

    private partial bool IsPossibleExpression() => CurrentTokenKind is
        SyntaxKind.NilKeyword or
        SyntaxKind.FalseKeyword or
        SyntaxKind.TrueKeyword or
        SyntaxKind.NumericLiteralToken or
        SyntaxKind.StringLiteralToken or
        SyntaxKind.MultiLineRawStringLiteralToken or
        SyntaxKind.DotDotDotToken or
        SyntaxKind.OpenParenToken or
        SyntaxKind.FunctionKeyword or
        SyntaxKind.OpenBraceToken or
        SyntaxKind.MinusToken or
        SyntaxKind.NotKeyword or
        SyntaxKind.HashToken or
        SyntaxKind.TildeToken or
        SyntaxKind.IdentifierToken;

#if TESTING
    internal
#else
    private
#endif
        ExpressionSyntax ParseExpression(bool reportError = true)
    {
        if (IsPossibleExpression())
            return ParseExpressionCore();
        else
        {
            var missing = CreateMissingIdentifierName();
            if (reportError)
                return ReportMissingExpression(missing);
            else
                return missing;
        }
    }

    /// <summary>
    /// 报告缺失的表达式错误。
    /// </summary>
    /// <typeparam name="TExpression">表达式节点的类型。</typeparam>
    /// <param name="expr">要添加错误信息的表达式节点。</param>
    /// <returns>添加错误信息后的<paramref name="expr"/>。</returns>
    private TExpression ReportMissingExpression<TExpression>(TExpression expr) where TExpression : ExpressionSyntax
    {
        var kind = CurrentTokenKind;
        if (kind == SyntaxKind.EndOfFileToken)
            return AddError(expr, ErrorCode.ERR_ExpressionExpected);
        else
            return AddError(expr, ErrorCode.ERR_InvalidExprTerm, SyntaxFacts.GetText(kind));
    }

    private partial ExpressionSyntax ParseExpressionCore()
    {
        Debug.Assert(IsPossibleExpression(), "必须先检查当前标记是否可能为表达式的开始，请使用ParseExpression。");

        ExpressionSyntax expr;
        if (SyntaxFacts.IsUnaryExpressionOperatorToken(CurrentTokenKind, Options.LanguageVersion))
            expr = ParseExpressionWithOperator();
        else
        {
            expr = ParseExpressionWithoutOperator();
            if (SyntaxFacts.IsBinaryExpressionOperatorToken(CurrentTokenKind, Options.LanguageVersion))
                expr = ParseExpressionWithOperator(expr);
        }

        return expr;
    }

#if TESTING
    internal
#else
    private
#endif
        ExpressionSyntax ParseExpressionWithoutOperator()
    {
        ExpressionSyntax expr = CurrentTokenKind switch
        {
            // 字面量
            SyntaxKind.NilKeyword =>
                ParseLiteralExpression(SyntaxKind.NilLiteralExpression
#if DEBUG
                    , SyntaxKind.NilKeyword
#endif
                    ),
            SyntaxKind.FalseKeyword =>
                ParseLiteralExpression(SyntaxKind.FalseLiteralExpression
#if DEBUG
                    , SyntaxKind.FalseKeyword
#endif
                    ),
            SyntaxKind.TrueKeyword =>
                ParseLiteralExpression(SyntaxKind.TrueLiteralExpression
#if DEBUG
                    , SyntaxKind.TrueKeyword
#endif
                    ),
            SyntaxKind.NumericLiteralToken =>
                ParseLiteralExpression(SyntaxKind.NumericLiteralExpression
#if DEBUG
                    , SyntaxKind.NumericLiteralToken
#endif
                    ),
            SyntaxKind.StringLiteralToken =>
                ParseLiteralExpression(SyntaxKind.StringLiteralExpression
#if DEBUG
                    , SyntaxKind.StringLiteralToken
#endif
                    ),
            SyntaxKind.MultiLineRawStringLiteralToken =>
                ParseLiteralExpression(SyntaxKind.StringLiteralExpression
#if DEBUG
                    , SyntaxKind.MultiLineRawStringLiteralToken
#endif
                    ),
            SyntaxKind.DotDotDotToken =>
                ParseLiteralExpression(SyntaxKind.VariousArgumentsExpression
#if DEBUG
                    , SyntaxKind.DotDotDotToken
#endif
                    ),

            SyntaxKind.MinusToken or
            SyntaxKind.NotKeyword or
            SyntaxKind.HashToken or
            SyntaxKind.TildeToken =>
                ParseExpressionWithOperator(),

            SyntaxKind.OpenParenToken =>
                ParseParenthesizedExpression(),

            SyntaxKind.FunctionKeyword =>
                ParseFunctionDefinitionExpression(),

            SyntaxKind.OpenBraceToken =>
                ParseTableConstructorExpression(),

            SyntaxKind.IdentifierToken =>
                ParseIdentifierName(),

            _ =>
                throw ExceptionUtilities.Unreachable()
        };

        var lastTokenPosition = -1;
        while (IsMakingProgress(ref lastTokenPosition))
        {
            switch (CurrentTokenKind)
            {
                case SyntaxKind.DotToken:
                    expr = ParseSimpleMemberAccessExpressionSyntax(expr);
                    break;
                case SyntaxKind.OpenBracketToken:
                    expr = ParseIndexMemberAccessExpressionSyntax(expr);
                    break;

                case SyntaxKind.ColonToken:
                    expr = ParseImplicitSelfParameterInvocationExpression(expr);
                    break;

                default:
                    if (IsPossibleInvocationArguments())
                    {
                        expr = ParseInvocationExpressionSyntax(expr);
                        break;
                    }
                    return expr;
            }
        }

        throw ExceptionUtilities.Unreachable();
    }

#if TESTING
    internal
#else
    private
#endif
        ExpressionSyntax ParseExpressionWithOperator(ExpressionSyntax? first = null)
    {
        ExpressionWithOperatorParser innerParser;
        if (first is null)
            innerParser = new(this);
        else
            innerParser = new(this, first);

        return innerParser.ParseExpressionWithOperator();
    }

#if TESTING
    internal
#else
    private
#endif
        LiteralExpressionSyntax ParseLiteralExpression(SyntaxKind kind
#if DEBUG
        , SyntaxKind currentTokenKind
#endif
        )
    {
#if DEBUG
        Debug.Assert(CurrentTokenKind == currentTokenKind);
#endif

        return _syntaxFactory.LiteralExpression(kind, EatToken());
    }

#if TESTING
    internal
#else
    private
#endif
        ParenthesizedExpressionSyntax ParseParenthesizedExpression()
    {
        var openParen = EatToken(SyntaxKind.OpenParenToken);
        var expression = ParseExpression();
        var closeParen = EatToken(SyntaxKind.CloseParenToken);
        return _syntaxFactory.ParenthesizedExpression(openParen, expression, closeParen);
    }

#if TESTING
    internal
#else
    private
#endif
        FunctionDefinitionExpressionSyntax ParseFunctionDefinitionExpression()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.FunctionKeyword);

        var function = EatToken(SyntaxKind.FunctionKeyword);
        ParseFunctionBody(SyntaxKind.FunctionDefinitionExpression, out var parameterList, out var block, out var end);
        return _syntaxFactory.FunctionDefinitionExpression(function, parameterList, block, end);
    }

#if TESTING
    internal
#else
    private
#endif
        TableConstructorExpressionSyntax ParseTableConstructorExpression()
    {
        var openBrace = EatToken(SyntaxKind.OpenBraceToken);
        var field = ParseFieldList();
        var closeBrace = EatToken(SyntaxKind.CloseBraceToken);
        return _syntaxFactory.TableConstructorExpression(openBrace, field, closeBrace);
    }

#if TESTING
    internal
#else
    private
#endif
        SimpleMemberAccessExpressionSyntax ParseSimpleMemberAccessExpressionSyntax(ExpressionSyntax self)
    {
        var dot = EatToken(SyntaxKind.DotToken);
        var member = ParseIdentifierName();
        return _syntaxFactory.SimpleMemberAccessExpression(self, dot, member);
    }

#if TESTING
    internal
#else
    private
#endif
        IndexMemberAccessExpressionSyntax ParseIndexMemberAccessExpressionSyntax(ExpressionSyntax self)
    {
        var openBracket = EatToken(SyntaxKind.OpenBracketToken);
        var member = ParseExpression();
        var closeBracket = EatToken(SyntaxKind.CloseBracketToken);
        return _syntaxFactory.IndexMemberAccessExpression(self, openBracket, member, closeBracket);
    }

#if TESTING
    internal
#else
    private
#endif
        InvocationExpressionSyntax ParseInvocationExpressionSyntax(ExpressionSyntax expr)
    {
        Debug.Assert(IsPossibleInvocationArguments());
        var arguments = ParseInvocationArguments();
        return _syntaxFactory.InvocationExpression(expr, arguments);
    }

#if TESTING
    internal
#else
    private
#endif
        InvocationExpressionSyntax ParseImplicitSelfParameterInvocationExpression(ExpressionSyntax expr)
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.ColonToken);
        var colon = EatToken(SyntaxKind.ColonToken);
        var name = ParseIdentifierName();
        InvocationArgumentsSyntax arguments;
        if (IsPossibleInvocationArguments())
            arguments = ParseInvocationArguments();
        else
        {
            arguments = _syntaxFactory.ArgumentList(
                ThisInternalSyntaxFactory.MissingToken(SyntaxKind.OpenParenToken),
                ThisInternalSyntaxFactory.SeparatedList<ArgumentSyntax>(),
                ThisInternalSyntaxFactory.MissingToken(SyntaxKind.CloseParenToken));
            arguments = AddError(arguments, ErrorCode.ERR_InvocationArgumentsExpected);
        }
        return _syntaxFactory.InvocationExpression(_syntaxFactory.ImplicitSelfParameterExpression(expr, colon, name), arguments);
    }
}
