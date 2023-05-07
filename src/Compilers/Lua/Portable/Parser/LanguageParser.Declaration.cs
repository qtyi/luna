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
#if TESTING
    internal
#else
    private
#endif
        void ParseFunctionBody(SyntaxKind structure, out ParameterListSyntax parameterList, out BlockSyntax block, out SyntaxToken endKeyword)
    {
        parameterList = this.ParseParameterList();
        block = this.ParseBlock(structure);
        endKeyword = this.EatToken(SyntaxKind.EndKeyword);
    }

    #region 形参
#if TESTING
    internal
#else
    private
#endif
        ParameterListSyntax ParseParameterList()
    {
        var openParen = this.EatToken(SyntaxKind.OpenParenToken);
        var parameters = this.ParseSeparatedSyntaxList(
            predicateNode: index =>
            {
                // 检查当前的标记的种类，决定是否解析第一个形参，即是否为空的形参列表。
                if (index == 0) return this.CurrentTokenKind is not (
                    SyntaxKind.CloseParenToken or // 紧跟着右圆括号，则是空列表。
                    SyntaxKind.EndOfFileToken // 到达文件结尾，则视为空列表。
                );
                // 从第二个形参开始，都必须要解析。
                else return true;
            },
            parseNode: (_, _) => this.ParseParameter(),
            predicateSeparator: _ => this.CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => this.EatToken(SyntaxKind.CommaToken));
        var closeParen = this.EatToken(SyntaxKind.CloseParenToken);
        return this._syntaxFactory.ParameterList(openParen, parameters, closeParen);
    }

#if TESTING
    internal
#else
    private
#endif
        ParameterSyntax ParseParameter()
    {
        var identifier = this.CurrentTokenKind switch
        {
            SyntaxKind.IdentifierToken or
            SyntaxKind.DotDotDotToken => this.EatToken(),

            _ => LanguageParser.CreateMissingIdentifierToken(),
        };
        if (identifier.IsMissing)
            identifier = this.AddError(identifier, ErrorCode.ERR_IdentifierExpected);
        return this._syntaxFactory.Parameter(identifier);
    }
    #endregion

    #region 字段
#if TESTING
    internal
#else
    private
#endif
        SeparatedSyntaxList<FieldSyntax> ParseFieldList() =>
        this.ParseSeparatedSyntaxList(
            predicateNode: _ =>
                this.IsPossibleField() ||                // 是可能的字段
                this.IsPossibleFieldListSeparator() &&   // 是可能的字段列表分隔（此时字段缺失）
                this.CurrentTokenKind is not (           // 遇到以下语法标记都将不尝试解析字段并直接退出：
                    SyntaxKind.CloseBraceToken or        // 表示字段列表的结尾
                    SyntaxKind.EndOfFileToken),          // 表示文件结尾
            parseNode: (_, _) => this.ParseField(),
            predicateSeparator: _ => this.IsPossibleFieldListSeparator(),
            parseSeparator: (_, missing) => missing ? this.CreateMissingFieldListSeparator() : this.EatToken(),
            allowTrailingSeparator: true);

#if TESTING
    internal
#else
    private
#endif
        bool IsPossibleFieldListSeparator() => LanguageParser.IsPossibleFieldListSeparator(this.CurrentTokenKind);

    private static bool IsPossibleFieldListSeparator(SyntaxKind kind) => kind is SyntaxKind.CommaToken or SyntaxKind.SemicolonToken;

    private SyntaxToken CreateMissingFieldListSeparator()
    {
        var separator = SyntaxFactory.MissingToken(SyntaxKind.CommaToken);
        return this.AddError(separator, ErrorCode.ERR_FieldSeparatorExpected);
    }

#if TESTING
    internal
#else
    private
#endif
        bool IsPossibleField() =>
        this.IsPossibleExpression() || this.CurrentTokenKind is SyntaxKind.OpenBracketToken or SyntaxKind.EqualsToken;

#if TESTING
    internal
#else
    private
#endif
        FieldSyntax ParseField()
    {
        // 解析键值对表字段。
        if (this.CurrentTokenKind == SyntaxKind.OpenBracketToken ||
            (SyntaxFacts.IsLiteralToken(this.CurrentTokenKind) && this.PeekToken(1).Kind == SyntaxKind.EqualsToken)) // 错误使用常量作为键。
            return this.ParseKeyValueField();
        // 解析名值对表字段。
        else if (this.CurrentTokenKind == SyntaxKind.EqualsToken || // 错误遗失标识符。
            (this.CurrentTokenKind == SyntaxKind.IdentifierToken && this.PeekToken(1).Kind == SyntaxKind.EqualsToken))
            return this.ParseNameValueField();
        // 解析列表项表字段。
        else
        {
            if (this.CurrentTokenKind == SyntaxKind.EndOfFileToken)
                return this._syntaxFactory.ItemField(this.CreateMissingIdentifierName());
            else
                return this._syntaxFactory.ItemField(this.ParseFieldValue());
        }
    }

#if TESTING
    internal
#else
    private
#endif
        NameValueFieldSyntax ParseNameValueField()
    {
        var name = this.ParseIdentifierName();
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.EqualsToken);
        var equals = this.EatToken(SyntaxKind.EqualsToken);
        var value = this.ParseFieldValue();
        return this._syntaxFactory.NameValueField(name, equals, value);
    }

#if TESTING
    internal
#else
    private
#endif
        KeyValueFieldSyntax ParseKeyValueField()
    {
        var openBracket = this.EatToken(SyntaxKind.OpenBracketToken);
        var key = this.ParseFieldKey();
        var closeBracket = this.EatToken(SyntaxKind.CloseBracketToken);
        var equals = this.EatToken(SyntaxKind.EqualsToken);
        var value = this.ParseFieldValue();
        return this._syntaxFactory.KeyValueField(openBracket, key, closeBracket, equals, value);
    }

#if TESTING
    internal
#else
    private
#endif
        ExpressionSyntax ParseFieldKey()
    {
        var expr = this.ParseExpression();

        // 跳过后方的标记和表达式直到等于符号或右方括号。
        var skippedSyntax = this.SkipTokensAndExpressions(
            token => token.Kind is not (SyntaxKind.EqualsToken or SyntaxKind.CloseBracketToken or SyntaxKind.EndOfFileToken),
            new FieldKeySkippedNodesVisitor(this));
        if (skippedSyntax is not null)
            expr = this.AddTrailingSkippedSyntax(expr, skippedSyntax);

        return expr;
    }

    /// <summary>
    /// 处理字段键表达式后方需要跳过的语法标记和语法节点的访问器。
    /// </summary>
    private sealed class FieldKeySkippedNodesVisitor : LuaSyntaxVisitor<LuaSyntaxNode>
    {
        private readonly LanguageParser _parser;

        public FieldKeySkippedNodesVisitor(LanguageParser parser) => this._parser = parser;

        /// <summary>
        /// 处理语法标记，向语法标记添加<see cref="ErrorCode.ERR_InvalidExprTerm"/>错误。
        /// </summary>
        /// <param name="token">要处理的语法标记。</param>
        /// <returns>处理后的<paramref name="token"/>。</returns>
        public override LuaSyntaxNode VisitToken(SyntaxToken token) =>
            this._parser.AddError(token, ErrorCode.ERR_InvalidExprTerm, SyntaxFacts.GetText(token.Kind));

        /// <summary>
        /// 处理所有语法节点。
        /// </summary>
        /// <remarks>若<paramref name="node"/>不是表达式语法，则抛出异常。</remarks>
        /// <param name="node">要处理的语法节点。</param>
        /// <returns>处理后的<paramref name="node"/>。</returns>
        /// <exception cref="ExceptionUtilities.Unreachable">当<paramref name="node"/>不是表达式语法时，这种情况不应发生。</exception>
        protected override LuaSyntaxNode DefaultVisit(LuaSyntaxNode node) =>
            node is ExpressionSyntax ? node : throw ExceptionUtilities.Unreachable;
    }

#if TESTING
    internal
#else
    private
#endif
        ExpressionSyntax ParseFieldValue()
    {
        ExpressionSyntax? expr = null;
        if (this.IsPossibleExpression())
            expr = this.ParseExpressionCore();

        // 跳过后方的标记和表达式直到字段结束。
        var skippedSyntax = this.SkipTokensAndExpressions(token => !LanguageParser.IsPossibleFieldListSeparator(token.Kind) && token.Kind is not (SyntaxKind.CloseBraceToken or SyntaxKind.EndOfFileToken));
        if (skippedSyntax is null) // 后方没有需要跳过的标记和表达式。
            expr ??= this.ReportMissingExpression(this.CreateMissingIdentifierName());
        else
        {
            skippedSyntax = this.AddError(skippedSyntax, ErrorCode.ERR_InvalidFieldValueTerm);
            expr = this.AddTrailingSkippedSyntax(
                expr ?? this.ReportMissingExpression(this.CreateMissingIdentifierName()),
                skippedSyntax);
        }
        return expr;
    }
    #endregion

    #region 实参
#if TESTING
    internal
#else
    private
# endif
        bool IsPossibleInvocationArguments() => this.CurrentTokenKind is
        SyntaxKind.OpenParenToken or
        SyntaxKind.OpenBraceToken or
        SyntaxKind.StringLiteralToken or
        SyntaxKind.MultiLineRawStringLiteralToken;

#if TESTING
    internal
#else
    private
#endif
        InvocationArgumentsSyntax ParseInvocationArguments() =>
        this.CurrentTokenKind switch
        {
            SyntaxKind.OpenParenToken => this.ParseArgumentList(),
            SyntaxKind.OpenBraceToken => this.ParseArgumentTable(),
            SyntaxKind.StringLiteralToken or
            SyntaxKind.MultiLineRawStringLiteralToken => this.ParseArgumentString(),
            _ => throw ExceptionUtilities.UnexpectedValue(this.CurrentTokenKind)
        };

#if TESTING
    internal
#else
    private
#endif
        ArgumentListSyntax ParseArgumentList()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.OpenParenToken);
        var openParen = this.EatToken(SyntaxKind.OpenParenToken);
        var arguments = this.ParseSeparatedSyntaxList(
            predicateNode: _ => this.IsPossibleExpression(),
            parseNode: (_, _) => this.ParseArgument(),
            predicateSeparator: _ => this.CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => this.EatToken(SyntaxKind.CommaToken));
        var closeParen = this.EatToken(SyntaxKind.CloseParenToken);
        return this._syntaxFactory.ArgumentList(openParen, arguments, closeParen);
    }

#if TESTING
    internal
#else
    private
#endif
        ArgumentTableSyntax ParseArgumentTable()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.OpenBraceToken);
        var table = this.ParseTableConstructorExpression();
        return this._syntaxFactory.ArgumentTable(table);
    }

#if TESTING
    internal
#else
    private
#endif
        ArgumentStringSyntax ParseArgumentString()
    {
        Debug.Assert(this.CurrentTokenKind is SyntaxKind.StringLiteralToken or SyntaxKind.MultiLineRawStringLiteralToken);
        var stringLiteral = this.EatToken();
        return this._syntaxFactory.ArgumentString(stringLiteral);
    }

#if TESTING
    internal
#else
    private
#endif
        ArgumentSyntax ParseArgument()
    {
        var expr = this.ParseExpression();
        return this._syntaxFactory.Argument(expr);
    }
    #endregion

    #region 特性
#if TESTING
    internal
#else
    private
#endif
        NameAttributeListSyntax ParseNameAttributeList()
    {
        var identifier = this.ParseIdentifierName();

        if (!this.TryParseAttributeList(out var attributeList, out var skippedSyntax) && skippedSyntax is not null)
            identifier = this.AddTrailingSkippedSyntax(identifier, skippedSyntax);

        return this._syntaxFactory.NameAttributeList(identifier, attributeList);
    }

#if TESTING
    internal
#else
    private
#endif
        bool TryParseAttributeList(
        [NotNullWhen(true)] out AttributeListSyntax? attributeList,
        out Microsoft.CodeAnalysis.GreenNode? skippedSyntax)
    {
        if (this.CurrentTokenKind != SyntaxKind.LessThanToken) // 无特性列表。
        {
            attributeList = null;
            skippedSyntax = null;
            return false;
        }

        var lessThan = this.EatToken(SyntaxKind.LessThanToken);
        var attributes = this.ParseSeparatedSyntaxList(
            predicateNode: _ => this.IsPossibleAttribute(),
            parseNode: (_, _) => this.ParseAttribute(),
            predicateSeparator: _ => this.CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => this.EatToken(SyntaxKind.CommaToken));
        var greaterThan = this.EatToken(SyntaxKind.GreaterThanToken);

        if (attributes.Count == 0) // 特性数量为0
        {
            greaterThan = this.AddError(greaterThan, ErrorCode.ERR_AttributeExpected);

            attributeList = null;
            skippedSyntax = SyntaxList.List(lessThan, greaterThan);
            return false;
        }
        else
        {
            attributeList = this._syntaxFactory.AttributeList(lessThan, attributes, greaterThan);
            skippedSyntax = null;
            return true;
        }
    }

#if TESTING
    internal
#else
    private
#endif
        bool IsPossibleAttribute() => this.CurrentToken.ContextualKind is SyntaxKind.CloseKeyword or SyntaxKind.ConstKeyword;

#if TESTING
    internal
#else
    private
#endif
        AttributeSyntax ParseAttribute()
    {
        Debug.Assert(this.IsPossibleAttribute());
        var token = this.EatContextualToken();

        var skippedSyntax = this.SkipTokens(token => token.Kind is not (
            SyntaxKind.CommaToken or        // 在分隔符中止。
            SyntaxKind.GreaterThanToken or  // 在特性列表结尾中止。
            SyntaxKind.EqualsToken or       // 在赋值符号提前中止。
            SyntaxKind.SemicolonToken or    // 在最近的语句结尾中止。
            SyntaxKind.EndOfFileToken       // 在文件结尾中止。
        ), new AttributeSkippedTokensVisitor(this));
        if (skippedSyntax is not null)
        {
            token = this.AddTrailingSkippedSyntax(token, skippedSyntax);
        }

        return this._syntaxFactory.Attribute(token);
    }

    /// <summary>
    /// 处理特性后方需要跳过的语法标记的访问器。
    /// </summary>
    private sealed class AttributeSkippedTokensVisitor : LuaSyntaxVisitor<SyntaxToken>
    {
        private readonly LanguageParser _parser;

        public AttributeSkippedTokensVisitor(LanguageParser parser) => this._parser = parser;

        /// <summary>
        /// 处理语法标记，向语法标记添加<see cref="ErrorCode.ERR_InvalidAttrTerm"/>错误。
        /// </summary>
        /// <param name="token">要处理的语法标记。</param>
        /// <returns>处理后的<paramref name="token"/>。</returns>
        public override SyntaxToken VisitToken(SyntaxToken token) =>
            this._parser.AddError(token, ErrorCode.ERR_InvalidAttrTerm, SyntaxFacts.GetText(token.Kind));

        /// <summary>
        /// 处理所有语法节点。
        /// </summary>
        /// <remarks>此方法必定抛出异常。</remarks>
        /// <param name="node">要处理的语法节点。</param>
        /// <returns>处理后的<paramref name="node"/>。</returns>
        /// <exception cref="ExceptionUtilities.Unreachable">当<paramref name="node"/>不是表达式语法时，这种情况不应发生。</exception>
        [DoesNotReturn]
        protected override SyntaxToken DefaultVisit(LuaSyntaxNode node) => throw ExceptionUtilities.Unreachable;
    }
    #endregion
}
