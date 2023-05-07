// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class LanguageParser
{
    [NonCopyable]
#if TESTING
    internal
#else
    private
#endif
        ref partial struct ExpressionWithOperatorParser
    {
        private readonly LanguageParser _parser;
        private ParseState _state;

        private enum ParseState : byte
        {
            Initial = 0,        // 初始状态
            UnaryStart,         // 一元运算符后方
            BinaryStart,        // 二元运算符后方
            Terminal,           // 可以结束
            Skip = Bad - 1,     // 跳过当前标记后继续
            Bad = byte.MaxValue // 立即错误
        }

        private static ParseState Transit(ParseState state, SyntaxKind opt, LanguageParser parser)
        {
            switch (state)
            {
                case ParseState.Initial:
                case ParseState.UnaryStart:
                case ParseState.BinaryStart:
                    if (SyntaxFacts.IsUnaryExpressionOperatorToken(opt))
                        return ParseState.UnaryStart;
                    else if (SyntaxFacts.IsBinaryExpressionOperatorToken(opt))
                        return ParseState.Skip;
                    else if (parser.IsPossibleExpression())
                        return ParseState.Terminal;
                    else
                        return ParseState.Bad;
                case ParseState.Terminal:
                    if (SyntaxFacts.IsBinaryExpressionOperatorToken(opt))
                        return ParseState.BinaryStart;
                    else
                        return ParseState.Bad;

                default:
                    throw ExceptionUtilities.Unreachable;
            };
        }

        public ExpressionWithOperatorParser(LanguageParser parser)
        {
            this._parser = parser;
            this._state = ParseState.Initial;
        }

        public ExpressionWithOperatorParser(LanguageParser parser, ExpressionSyntax expr)
        {
            this._parser = parser;
            this._state = ParseState.Terminal;
            this._exprStack.Push(expr);
        }

        internal void Reset()
        {
            this._state = ParseState.Initial;
            this._exprStack.Clear();
            this._optStack.Clear();
        }

        private bool TryEatTokenOrExpression(
            [NotNullWhen(true)] out LuaSyntaxNode? result,
            out SkippedTokensTriviaSyntax? skippedTokensTrivia)
        {
            result = null;
            skippedTokensTrivia = null;

            var skippedTokenListBuilder = this._parser._pool.Allocate<SyntaxToken>();
            while (this._state != ParseState.Bad && result is null)
            {
                var state = Transit(this._state, this._parser.CurrentTokenKind, this._parser);
                switch (state)
                {
                    case ParseState.UnaryStart:
                    case ParseState.BinaryStart:
                        result = this._parser.EatToken();
                        this._state = state;
                        break;

                    case ParseState.Terminal:
                        result = this._parser.ParseExpressionWithoutOperator();
                        this._state = state;
                        break;

                    case ParseState.Skip:
                        skippedTokenListBuilder.Add(this._parser.EatToken());
                        continue;

                    case ParseState.Bad:
                        result = null;
                        this._state = state;
                        break;

                    default:
                        throw ExceptionUtilities.Unreachable;
                }
            }

            skippedTokensTrivia = this.CreateSkippedTokensTrivia(skippedTokenListBuilder, ErrorCode.ERR_InvalidExprTerm);

            // 若遇到Bad情况直接退出，表示没有下一个可接受的标记或表达式语法，则将skippedTokensTrivia传出，交给调用方法处理；
            if (result is null) return false;
            // 若能找到下一个可接受的标记或表达式语法，则将skippedTokensTrivia添加到这个语法节点的前方语法琐碎内容中，不传出此方法。
            else
            {
                if (skippedTokensTrivia is not null)
                {
                    result = this._parser.AddLeadingSkippedSyntax(result, skippedTokensTrivia);
                    skippedTokensTrivia = null;
                }
                return true;
            }
        }

        private readonly Stack<ExpressionSyntax> _exprStack = new(10);
        private readonly Stack<(SyntaxToken opt, bool isUnary)> _optStack = new(10);

        private bool CurrentTokenIsUnary
        {
            get
            {
                Debug.Assert(this._state is ParseState.UnaryStart or ParseState.BinaryStart, "只能在UnaryStart或BinaryStart状态下调用此属性。");
                return this._state == ParseState.UnaryStart;
            }
        }

        private bool CanAssociate(SyntaxToken nextOpt)
        {
            var nextIsUnary = this.CurrentTokenIsUnary;
            (var opt, var isUnary) = this._optStack.Peek();

            if (!isUnary && nextIsUnary) return false; // 上一个运算符是二元运算符且下一个运算符是一元运算符时不可以。

            var precedence = SyntaxFacts.GetOperatorPrecedence(opt.Kind, isUnary);
            var nextPrecedence = SyntaxFacts.GetOperatorPrecedence(nextOpt.Kind, nextIsUnary);

            // 优先级不同情况下：
            if (nextPrecedence != precedence) return nextPrecedence < precedence; // 下一个运算符优先级比上一个运算符优先级低时才可。

            // 优先级相同情况下：
            if (isUnary) return false; // 上一个运算符是一元运算符时不可。
            if (SyntaxFacts.IsLeftAssociativeBinaryExpressionOperatorToken(opt.Kind)) return true; // 上一个运算符是左结合时才可。

            // 其他情况：
            return false;
        }

        internal ExpressionSyntax ParseExpressionWithOperator()
        {
Next:
            if (!this.TryEatTokenOrExpression(out var tokenOrExpression, out var skippedTokensTrivia)) goto Final;
            else if (tokenOrExpression is SyntaxToken nextOpt)
            {
                // 前方有多个可结合的运算符，全部结合。
                while (this._optStack.Count > 0 && this.CanAssociate(nextOpt))
                {
                    if (this._optStack.Peek().isUnary) //上一个运算符是一元运算符。
                    {
                        Debug.Assert(this._exprStack.Count >= 1);
                        var opt = this._optStack.Pop().opt;
                        this._exprStack.Push(this._parser._syntaxFactory.UnaryExpression(
                            SyntaxFacts.GetUnaryExpression(opt.Kind),
                            opt,
                            this._exprStack.Pop()
                        ));
                    }
                    else //上一个运算符是二元运算符。
                    {
                        Debug.Assert(this._exprStack.Count >= 2);
                        var opt = this._optStack.Pop().opt;
                        var right = this._exprStack.Pop();
                        var left = this._exprStack.Pop();
                        this._exprStack.Push(this._parser._syntaxFactory.BinaryExpression(
                            SyntaxFacts.GetBinaryExpression(opt.Kind),
                            left,
                            opt,
                            right
                        ));
                    }
                }
                this._optStack.Push((nextOpt, this.CurrentTokenIsUnary));
            }
            else if (tokenOrExpression is ExpressionSyntax expr)
            {
                this._exprStack.Push(expr);
            }
            goto Next;

Final:
/* 有两种情况到达这里：
 * 1. 表达式语法正确，结构没有缺失。
 * 2. 发生错误，无法继续。
 */
            {
                ExpressionSyntax expr;

                // 成功情况：表达式的数量比二元运算符的数量多一个。
                if (this._exprStack.Count == 1 + this._optStack.Count(tuple => tuple.isUnary == false))
                {
                    expr = this._exprStack.Pop();
                    if (skippedTokensTrivia is not null)
                        expr = this._parser.AddTrailingSkippedSyntax(expr, skippedTokensTrivia);
                }
                // 错误情况1：前方什么也没有。
                // 错误情况2：前方有多个一元运算符。
                // 错误情况3：前方有一个多余的二元运算符。
                // 处理方法：补充一个缺失的标识符名称语法（表达式），即可开始结合。
                else
                {
                    expr = this._parser.CreateMissingIdentifierName();
                    if (skippedTokensTrivia is not null)
                        expr = this._parser.AddLeadingSkippedSyntax(expr, skippedTokensTrivia);
                }

                // 开始结合。
                while (this._optStack.Count > 0)
                {
                    (var opt, var isUnary) = this._optStack.Pop();
                    if (isUnary)
                    {
                        Debug.Assert(this._exprStack.Count >= 0);
                        expr = this._parser._syntaxFactory.UnaryExpression(
                            SyntaxFacts.GetUnaryExpression(opt.Kind),
                            opt,
                            expr);
                    }
                    else
                    {
                        Debug.Assert(this._exprStack.Count >= 1);
                        expr = this._parser._syntaxFactory.BinaryExpression(
                            SyntaxFacts.GetBinaryExpression(opt.Kind),
                            this._exprStack.Pop(),
                            opt,
                            expr
                        );
                    }
                }

                Debug.Assert(this._exprStack.Count == 0 && this._optStack.Count == 0); // 如果触发断言，则表明我们遇到了麻烦。

                return expr;
            }
        }

        private SkippedTokensTriviaSyntax? CreateSkippedTokensTrivia(in SyntaxListBuilder<SyntaxToken> skippedTokenBuilder, in ErrorCode errorCode)
        {
            // 快速跳过没有错误的情况。
            if (skippedTokenBuilder.Count == 0) return null;

            var trivia = this._parser._syntaxFactory.SkippedTokensTrivia(this._parser._pool.ToListAndFree(skippedTokenBuilder));
            trivia = this._parser.AddError(trivia, errorCode); // 报告诊断错误。
            return trivia;
        }
    }
}
