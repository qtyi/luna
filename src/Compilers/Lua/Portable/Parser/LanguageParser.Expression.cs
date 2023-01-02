// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Diagnostics;
using System.Xml.Linq;
using MSCA::Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using MSCA::Roslyn.Utilities;

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

        return this.ParseSeparatedSyntaxList(
            predicateNode: index => index < minCount || this.IsPossibleExpression(),
            parseNode: (_, missing) =>
            {
                if (!missing && this.IsPossibleExpression())
                    return this.ParseExpression();
                else
                    return this.ReportMissingExpression(this.CreateMissingIdentifierName());
            },
            predicateSeparator: _ => this.CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => this.EatToken(SyntaxKind.CommaToken),
            minCount: minCount);
    }

#if TESTING
    internal
#else
    private
#endif
        SeparatedSyntaxList<ExpressionSyntax> CreateMissingExpressionList() =>
        new(SyntaxList.List(
            this.ReportMissingExpression(this.CreateMissingIdentifierName())
        ));

#if TESTING
    internal
#else
    private
#endif
        SeparatedSyntaxList<ExpressionSyntax> CreateMissingExpressionList(ErrorCode code, params object[] args) =>
        new(SyntaxList.List(
            this.AddError(this.CreateMissingIdentifierName(), code, args)
        ));

#if TESTING
    internal
#else
    private
#endif
        bool IsPossibleExpression() => this.CurrentTokenKind is
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
        if (this.IsPossibleExpression())
            return this.ParseExpressionCore();
        else
        {
            var missing = this.CreateMissingIdentifierName();
            if (reportError)
                return this.ReportMissingExpression(missing);
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
        var kind = this.CurrentTokenKind;
        if (kind == SyntaxKind.EndOfFileToken)
            return this.AddError(expr, ErrorCode.ERR_ExpressionExpected);
        else
            return this.AddError(expr, ErrorCode.ERR_InvalidExprTerm, SyntaxFacts.GetText(kind));
    }

    private ExpressionSyntax ParseExpressionCore()
    {
        Debug.Assert(this.IsPossibleExpression(), "必须先检查当前标志是否可能为表达式的开始，请使用ParseExpression。");

        ExpressionSyntax expr;
        if (SyntaxFacts.IsUnaryExpressionOperatorToken(this.CurrentTokenKind))
            expr = this.ParseExpressionWithOperator();
        else
        {
            expr = this.ParseExpressionWithoutOperator();
            if (SyntaxFacts.IsBinaryExpressionOperatorToken(this.CurrentTokenKind))
                expr = this.ParseExpressionWithOperator(expr);
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
        ExpressionSyntax expr = this.CurrentTokenKind switch
        {
            // 字面量
            SyntaxKind.NilKeyword =>
                this.ParseLiteralExpression(SyntaxKind.NilLiteralExpression
#if DEBUG
                    , SyntaxKind.NilKeyword
#endif
                    ),
            SyntaxKind.FalseKeyword =>
                this.ParseLiteralExpression(SyntaxKind.FalseLiteralExpression
#if DEBUG
                    , SyntaxKind.FalseKeyword
#endif
                    ),
            SyntaxKind.TrueKeyword =>
                this.ParseLiteralExpression(SyntaxKind.TrueLiteralExpression
#if DEBUG
                    , SyntaxKind.TrueKeyword
#endif
                    ),
            SyntaxKind.NumericLiteralToken =>
                this.ParseLiteralExpression(SyntaxKind.NumericLiteralExpression
#if DEBUG
                    , SyntaxKind.NumericLiteralToken
#endif
                    ),
            SyntaxKind.StringLiteralToken =>
                this.ParseLiteralExpression(SyntaxKind.StringLiteralExpression
#if DEBUG
                    , SyntaxKind.StringLiteralToken
#endif
                    ),
            SyntaxKind.MultiLineRawStringLiteralToken =>
                this.ParseLiteralExpression(SyntaxKind.StringLiteralExpression
#if DEBUG
                    , SyntaxKind.MultiLineRawStringLiteralToken
#endif
                    ),
            SyntaxKind.DotDotDotToken =>
                this.ParseLiteralExpression(SyntaxKind.VariousArgumentsExpression
#if DEBUG
                    , SyntaxKind.DotDotDotToken
#endif
                    ),

            SyntaxKind.MinusToken or
            SyntaxKind.NotKeyword or
            SyntaxKind.HashToken or
            SyntaxKind.TildeToken =>
                this.ParseExpressionWithOperator(),

            SyntaxKind.OpenParenToken =>
                this.ParseParenthesizedExpression(),

            SyntaxKind.FunctionKeyword =>
                this.ParseFunctionDefinitionExpression(),

            SyntaxKind.OpenBraceToken =>
                this.ParseTableConstructorExpression(),

            SyntaxKind.IdentifierToken =>
                this.ParseIdentifierName(),

            _ =>
                throw ExceptionUtilities.Unreachable
        };

        int lastTokenPosition = -1;
        while (IsMakingProgress(ref lastTokenPosition))
        {
            switch (this.CurrentTokenKind)
            {
                case SyntaxKind.DotToken:
                    expr = this.ParseSimpleMemberAccessExpressionSyntax(expr);
                    break;
                case SyntaxKind.OpenBracketToken:
                    expr = this.ParseIndexMemberAccessExpressionSyntax(expr);
                    break;

                case SyntaxKind.ColonToken:
                    expr = this.ParseImplicitSelfParameterInvocationExpression(expr);
                    break;

                default:
                    if (this.IsPossibleInvocationArguments())
                    {
                        expr = this.ParseInvocationExpressionSyntax(expr);
                        break;
                    }
                    return expr;
            }
        }

        throw ExceptionUtilities.Unreachable;
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
        Debug.Assert(this.CurrentTokenKind == currentTokenKind);
#endif

        return this._syntaxFactory.LiteralExpression(kind, this.EatToken());
    }

#if TESTING
    internal
#else
    private
#endif
        ParenthesizedExpressionSyntax ParseParenthesizedExpression()
    {
        var openParen = this.EatToken(SyntaxKind.OpenParenToken);
        var expression = this.ParseExpression();
        var closeParen = this.EatToken(SyntaxKind.CloseParenToken);
        return this._syntaxFactory.ParenthesizedExpression(openParen, expression, closeParen);
    }

#if TESTING
    internal
#else
    private
#endif
        FunctionDefinitionExpressionSyntax ParseFunctionDefinitionExpression()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.FunctionKeyword);

        var function = this.EatToken(SyntaxKind.FunctionKeyword);
        this.ParseFunctionBody(SyntaxKind.FunctionDefinitionExpression, out var parameterList, out var block, out var end);
        return this._syntaxFactory.FunctionDefinitionExpression(function, parameterList, block, end);
    }

#if TESTING
    internal
#else
    private
#endif
        TableConstructorExpressionSyntax ParseTableConstructorExpression()
    {
        var openBrace = this.EatToken(SyntaxKind.OpenBraceToken);
        var field = this.ParseFieldList();
        var closeBrace = this.EatToken(SyntaxKind.CloseBraceToken);
        return this._syntaxFactory.TableConstructorExpression(openBrace, field, closeBrace);
    }

#if TESTING
    internal
#else
    private
#endif
        SimpleMemberAccessExpressionSyntax ParseSimpleMemberAccessExpressionSyntax(ExpressionSyntax self)
    {
        var dot = this.EatToken(SyntaxKind.DotToken);
        var member = this.ParseIdentifierName();
        return this._syntaxFactory.SimpleMemberAccessExpression(self, dot, member);
    }

#if TESTING
    internal
#else
    private
#endif
        IndexMemberAccessExpressionSyntax ParseIndexMemberAccessExpressionSyntax(ExpressionSyntax self)
    {
        var openBracket = this.EatToken(SyntaxKind.OpenBracketToken);
        var member = this.ParseExpression();
        var closeBracket = this.EatToken(SyntaxKind.CloseBracketToken);
        return this._syntaxFactory.IndexMemberAccessExpression(self, openBracket, member, closeBracket);
    }

#if TESTING
    internal
#else
    private
#endif
        InvocationExpressionSyntax ParseInvocationExpressionSyntax(ExpressionSyntax expr)
    {
        Debug.Assert(this.IsPossibleInvocationArguments());
        var arguments = this.ParseInvocationArguments();
        return this._syntaxFactory.InvocationExpression(expr, arguments);
    }

#if TESTING
    internal
#else
    private
#endif
        InvocationExpressionSyntax ParseImplicitSelfParameterInvocationExpression(ExpressionSyntax expr)
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.ColonToken);
        var colon = this.EatToken(SyntaxKind.ColonToken);
        var name = this.ParseIdentifierName();
        InvocationArgumentsSyntax arguments;
        if (this.IsPossibleInvocationArguments())
            arguments = this.ParseInvocationArguments();
        else
        {
            arguments = this._syntaxFactory.ArgumentList(
                SyntaxFactory.MissingToken(SyntaxKind.OpenParenToken),
                SyntaxFactory.SeparatedList<ArgumentSyntax>(),
                SyntaxFactory.MissingToken(SyntaxKind.CloseParenToken));
            arguments = this.AddError(arguments, ErrorCode.ERR_InvocationArgumentsExpected);
        }
        return this._syntaxFactory.InvocationExpression(this._syntaxFactory.ImplicitSelfParameterExpression(expr, colon, name), arguments);
    }
}
