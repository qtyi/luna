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
    private void ParseFunctionBody(SyntaxKind structure, out ParameterDeclarationListSyntax parameterList, out BlockSyntax block, out SyntaxToken endKeyword)
    {
        parameterList = ParseParameterDeclarationList();
        block = ParseBlock(structure);
        endKeyword = EatToken(SyntaxKind.EndKeyword);
    }

    #region 形参
    private ParameterDeclarationListSyntax ParseParameterDeclarationList()
    {
        var openParen = EatToken(SyntaxKind.OpenParenToken);
        var parameters = ParseSeparatedSyntaxList(
            predicateNode: index =>
            {
                // 检查当前的标记的种类，决定是否解析第一个形参，即是否为空的形参列表。
                if (index == 0) return CurrentTokenKind is not (
                    SyntaxKind.CloseParenToken or // 紧跟着右圆括号，则是空列表。
                    SyntaxKind.EndOfFileToken // 到达文件结尾，则视为空列表。
                );
                // 从第二个形参开始，都必须要解析。
                else return true;
            },
            parseNode: (_, _) => ParseParameterDeclaration(),
            predicateSeparator: _ => CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => EatToken(SyntaxKind.CommaToken));
        var closeParen = EatToken(SyntaxKind.CloseParenToken);
        return _syntaxFactory.ParameterDeclarationList(openParen, parameters, closeParen);
    }

    private ParameterDeclarationSyntax ParseParameterDeclaration()
    {
        var parameterKeyword = TryEatToken(SyntaxKind.ParameterKeyword);
        var identifier = CurrentTokenKind switch
        {
            SyntaxKind.IdentifierToken or
            SyntaxKind.DotDotDotToken => EatToken(),

            _ => CreateMissingIdentifierToken(),
        };
        if (identifier.IsMissing)
            identifier = AddError(identifier, ErrorCode.ERR_IdentifierExpected);
        return null;
        //return _syntaxFactory.ParameterDeclaration(identifier);
    }
    #endregion

    #region 字段
    private SeparatedSyntaxList<FieldSyntax> ParseFieldList() =>
        ParseSeparatedSyntaxList(
            predicateNode: _ =>
                IsPossibleField() ||                // 是可能的字段
                IsPossibleFieldListSeparator() &&   // 是可能的字段列表分隔（此时字段缺失）
                CurrentTokenKind is not (           // 遇到以下语法标记都将不尝试解析字段并直接退出：
                    SyntaxKind.CloseBraceToken or        // 表示字段列表的结尾
                    SyntaxKind.EndOfFileToken),          // 表示文件结尾
            parseNode: (_, _) => ParseField(),
            predicateSeparator: _ => IsPossibleFieldListSeparator(),
            parseSeparator: (_, missing) => missing ? CreateMissingFieldListSeparator() : EatToken(),
            allowTrailingSeparator: true);

    private bool IsPossibleFieldListSeparator() => IsPossibleFieldListSeparator(CurrentTokenKind);

    private static bool IsPossibleFieldListSeparator(SyntaxKind kind) => kind is SyntaxKind.CommaToken or SyntaxKind.SemicolonToken;

    private SyntaxToken CreateMissingFieldListSeparator()
    {
        var separator = ThisInternalSyntaxFactory.MissingToken(SyntaxKind.CommaToken);
        return AddError(separator, ErrorCode.ERR_FieldSeparatorExpected);
    }

    private bool IsPossibleField() =>
        IsPossibleExpression() || CurrentTokenKind is SyntaxKind.OpenBracketToken or SyntaxKind.EqualsToken;

    private FieldSyntax ParseField()
    {
        // 解析键值对表字段。
        if (CurrentTokenKind == SyntaxKind.OpenBracketToken ||
            (SyntaxFacts.IsLiteralExpressionToken(CurrentTokenKind, lexer.Options.LanguageVersion) && PeekToken(1).Kind == SyntaxKind.EqualsToken)) // 错误使用常量作为键。
            return ParseKeyValueField();
        // 解析名值对表字段。
        else if (CurrentTokenKind == SyntaxKind.EqualsToken || // 错误遗失标识符。
            (CurrentTokenKind == SyntaxKind.IdentifierToken && PeekToken(1).Kind == SyntaxKind.EqualsToken))
            return ParseNameValueField();
        // 解析列表项表字段。
        else
        {
            if (CurrentTokenKind == SyntaxKind.EndOfFileToken)
                return _syntaxFactory.ItemField(CreateMissingIdentifierName());
            else
                return _syntaxFactory.ItemField(ParseFieldValue());
        }
    }

    private NameValueFieldSyntax ParseNameValueField()
    {
        var name = ParseIdentifierName();
        Debug.Assert(CurrentTokenKind == SyntaxKind.EqualsToken);
        var equals = EatToken(SyntaxKind.EqualsToken);
        var value = ParseFieldValue();
        return _syntaxFactory.NameValueField(name, equals, value);
    }

    private KeyValueFieldSyntax ParseKeyValueField()
    {
        var openBracket = EatToken(SyntaxKind.OpenBracketToken);
        var key = ParseFieldKey();
        var closeBracket = EatToken(SyntaxKind.CloseBracketToken);
        var equals = EatToken(SyntaxKind.EqualsToken);
        var value = ParseFieldValue();
        return _syntaxFactory.KeyValueField(openBracket, key, closeBracket, equals, value);
    }

    private ExpressionSyntax ParseFieldKey()
    {
        var expr = ParseExpression();

        // 跳过后方的标记和表达式直到等于符号或右方括号。
        var skippedSyntax = SkipTokensAndExpressions(
            token => token.Kind is not (SyntaxKind.EqualsToken or SyntaxKind.CloseBracketToken or SyntaxKind.EndOfFileToken),
            new FieldKeySkippedNodesVisitor(this));
        if (skippedSyntax is not null)
            expr = AddTrailingSkippedSyntax(expr, skippedSyntax);

        return expr;
    }

    /// <summary>
    /// 处理字段键表达式后方需要跳过的语法标记和语法节点的访问器。
    /// </summary>
    private sealed class FieldKeySkippedNodesVisitor : ThisInternalSyntaxVisitor<ThisInternalSyntaxNode>
    {
        private readonly LanguageParser _parser;

        public FieldKeySkippedNodesVisitor(LanguageParser parser) => _parser = parser;

        /// <summary>
        /// 处理语法标记，向语法标记添加<see cref="ErrorCode.ERR_InvalidExprTerm"/>错误。
        /// </summary>
        /// <param name="token">要处理的语法标记。</param>
        /// <returns>处理后的<paramref name="token"/>。</returns>
        public override ThisInternalSyntaxNode VisitToken(SyntaxToken token) =>
            _parser.AddError(token, ErrorCode.ERR_InvalidExprTerm, SyntaxFacts.GetText(token.Kind));

        /// <summary>
        /// 处理所有语法节点。
        /// </summary>
        /// <remarks>若<paramref name="node"/>不是表达式语法，则抛出异常。</remarks>
        /// <param name="node">要处理的语法节点。</param>
        /// <returns>处理后的<paramref name="node"/>。</returns>
        /// <exception cref="ExceptionUtilities.Unreachable">当<paramref name="node"/>不是表达式语法时，这种情况不应发生。</exception>
        protected override ThisInternalSyntaxNode DefaultVisit(ThisInternalSyntaxNode node) =>
            node is ExpressionSyntax ? node : throw ExceptionUtilities.Unreachable();
    }

    private ExpressionSyntax ParseFieldValue()
    {
        ExpressionSyntax? expr = null;
        if (IsPossibleExpression())
            expr = ParseExpressionCore();

        // 跳过后方的标记和表达式直到字段结束。
        var skippedSyntax = SkipTokensAndExpressions(token => !IsPossibleFieldListSeparator(token.Kind) && token.Kind is not (SyntaxKind.CloseBraceToken or SyntaxKind.EndOfFileToken));
        if (skippedSyntax is null) // 后方没有需要跳过的标记和表达式。
            expr ??= ReportMissingExpression(CreateMissingIdentifierName());
        else
        {
            skippedSyntax = AddError(skippedSyntax, ErrorCode.ERR_InvalidFieldValueTerm);
            expr = AddTrailingSkippedSyntax(
                expr ?? ReportMissingExpression(CreateMissingIdentifierName()),
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
        bool IsPossibleInvocationArguments() => CurrentTokenKind is
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
        CurrentTokenKind switch
        {
            SyntaxKind.OpenParenToken => ParseArgumentList(),
            SyntaxKind.OpenBraceToken => ParseArgumentTable(),
            SyntaxKind.StringLiteralToken or
            SyntaxKind.MultiLineRawStringLiteralToken => ParseArgumentString(),
            _ => throw ExceptionUtilities.UnexpectedValue(CurrentTokenKind)
        };

#if TESTING
    internal
#else
    private
#endif
        ArgumentListSyntax ParseArgumentList()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.OpenParenToken);
        var openParen = EatToken(SyntaxKind.OpenParenToken);
        var arguments = ParseSeparatedSyntaxList(
            predicateNode: _ => IsPossibleExpression(),
            parseNode: (_, _) => ParseArgument(),
            predicateSeparator: _ => CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => EatToken(SyntaxKind.CommaToken));
        var closeParen = EatToken(SyntaxKind.CloseParenToken);
        return _syntaxFactory.ArgumentList(openParen, arguments, closeParen);
    }

#if TESTING
    internal
#else
    private
#endif
        ArgumentTableSyntax ParseArgumentTable()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.OpenBraceToken);
        var table = ParseTableConstructorExpression();
        return _syntaxFactory.ArgumentTable(table);
    }

#if TESTING
    internal
#else
    private
#endif
        ArgumentStringSyntax ParseArgumentString()
    {
        Debug.Assert(CurrentTokenKind is SyntaxKind.StringLiteralToken or SyntaxKind.MultiLineRawStringLiteralToken);
        var stringLiteral = EatToken();
        return _syntaxFactory.ArgumentString(stringLiteral);
    }

#if TESTING
    internal
#else
    private
#endif
        ArgumentSyntax ParseArgument()
    {
        var expr = ParseExpression();
        return _syntaxFactory.Argument(expr);
    }
    #endregion

    #region 特性
    private NameAttributeListSyntax ParseNameAttributeList()
    {
        var identifier = ParseIdentifierName();

        if (!TryParseAttributeList(out var attributeList, out var skippedSyntax) && skippedSyntax is not null)
            identifier = AddTrailingSkippedSyntax(identifier, skippedSyntax);

        return _syntaxFactory.NameAttributeList(identifier, attributeList);
    }

    private bool TryParseAttributeList(
        [NotNullWhen(true)] out VariableAttributeListSyntax? attributeList,
        out Microsoft.CodeAnalysis.GreenNode? skippedSyntax)
    {
        if (CurrentTokenKind != SyntaxKind.LessThanToken) // 无特性列表。
        {
            attributeList = null;
            skippedSyntax = null;
            return false;
        }

        var lessThan = EatToken(SyntaxKind.LessThanToken);
        var attributes = ParseSeparatedSyntaxList(
            predicateNode: _ => IsPossibleAttribute(),
            parseNode: (_, _) => ParseAttribute(),
            predicateSeparator: _ => CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => EatToken(SyntaxKind.CommaToken));
        var greaterThan = EatToken(SyntaxKind.GreaterThanToken);

        if (attributes.Count == 0) // 特性数量为0
        {
            greaterThan = AddError(greaterThan, ErrorCode.ERR_AttributeExpected);

            attributeList = null;
            skippedSyntax = SyntaxList.List(lessThan, greaterThan);
            return false;
        }
        else
        {
            attributeList = _syntaxFactory.VariableAttributeList(lessThan, attributes, greaterThan);
            skippedSyntax = null;
            return true;
        }
    }

    private bool IsPossibleAttribute() => CurrentToken.ContextualKind is SyntaxKind.CloseKeyword or SyntaxKind.ConstKeyword;

    private VariableAttributeSyntax ParseAttribute()
    {
        Debug.Assert(IsPossibleAttribute());
        var token = EatContextualToken();

        var skippedSyntax = SkipTokens(token => token.Kind is not (
            SyntaxKind.CommaToken or        // 在分隔符中止。
            SyntaxKind.GreaterThanToken or  // 在特性列表结尾中止。
            SyntaxKind.EqualsToken or       // 在赋值符号提前中止。
            SyntaxKind.SemicolonToken or    // 在最近的语句结尾中止。
            SyntaxKind.EndOfFileToken       // 在文件结尾中止。
        ), new AttributeSkippedTokensVisitor(this));
        if (skippedSyntax is not null)
        {
            token = AddTrailingSkippedSyntax(token, skippedSyntax);
        }

        return _syntaxFactory.VariableAttribute(token);
    }

    /// <summary>
    /// 处理特性后方需要跳过的语法标记的访问器。
    /// </summary>
    private sealed class AttributeSkippedTokensVisitor : ThisInternalSyntaxVisitor<SyntaxToken>
    {
        private readonly LanguageParser _parser;

        public AttributeSkippedTokensVisitor(LanguageParser parser) => _parser = parser;

        /// <summary>
        /// 处理语法标记，向语法标记添加<see cref="ErrorCode.ERR_InvalidAttrTerm"/>错误。
        /// </summary>
        /// <param name="token">要处理的语法标记。</param>
        /// <returns>处理后的<paramref name="token"/>。</returns>
        public override SyntaxToken VisitToken(SyntaxToken token) =>
            _parser.AddError(token, ErrorCode.ERR_InvalidAttrTerm, SyntaxFacts.GetText(token.Kind));

        /// <summary>
        /// 处理所有语法节点。
        /// </summary>
        /// <remarks>此方法必定抛出异常。</remarks>
        /// <param name="node">要处理的语法节点。</param>
        /// <returns>处理后的<paramref name="node"/>。</returns>
        /// <exception cref="ExceptionUtilities.Unreachable">当<paramref name="node"/>不是表达式语法时，这种情况不应发生。</exception>
        [DoesNotReturn]
        protected override SyntaxToken DefaultVisit(ThisInternalSyntaxNode node) => throw ExceptionUtilities.Unreachable();
    }
    #endregion
}
