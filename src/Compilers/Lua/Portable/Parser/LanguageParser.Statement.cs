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
        void ParseStatements(in SyntaxListBuilder<StatementSyntax> statementsBuilder) =>
        this.ParseSyntaxList(
            statementsBuilder,
            predicateNode: _ =>
            {
                // 后续不是合法语句时停止。
                if (!this.IsPossibleStatement()) return false;

                // 正在解析if/elseif语句时遇到elseif关键字时停止。
                if (this._syntaxFactoryContext.IsInIfOrElseIf && this.CurrentTokenKind == SyntaxKind.ElseIfKeyword) return false;

                // 正常处理除返回语句外的其他语句。
                if (this.CurrentTokenKind != SyntaxKind.ReturnKeyword) return true;

                // 尝试解析这个返回语句。
                var resetPoint = this.GetResetPoint();
                var returnStat = this.ParseReturnStatement();

                // 跳过返回语句后方所有连续的空语句。
                while (this.CurrentTokenKind == SyntaxKind.SemicolonToken)
                    this.EatToken();

                if (this._syntaxFactoryContext.IsInIfOrElseIf && this.CurrentTokenKind == SyntaxKind.ElseIfKeyword) // 正在解析if/elseif语句时遇到elseif语句时停止。
                {
                    this.Reset(ref resetPoint);
                    this.Release(ref resetPoint);
                    return false;
                }
                else if (this.IsPossibleStatement()) // 后方还有合法的语句，则此返回语句仅为位置错误。
                {
                    this.Reset(ref resetPoint);
                    this.Release(ref resetPoint);
                    return true;
                }
                else // 否则此返回语句可能是块的最后一个语句。
                {
                    this.Reset(ref resetPoint);
                    this.Release(ref resetPoint);
                    return false;
                }
            },
            parseNode: (_, _) =>
            {
                var stat = this.ParseStatement();
                if (stat is ReturnStatementSyntax) // 此返回语句位置错误，报告错误。
                    return this.AddError(stat, ErrorCode.ERR_MisplacedReturnStat);
                else
                    return stat;
            });

#if TESTING
    internal
#else
    private
#endif
        bool IsPossibleStatement() =>
        this.CurrentTokenKind switch
        {
            SyntaxKind.SemicolonToken or
            SyntaxKind.ColonColonToken or
            SyntaxKind.BreakKeyword or
            SyntaxKind.GotoKeyword or
            SyntaxKind.ReturnKeyword or
            SyntaxKind.DoKeyword or
            SyntaxKind.WhileKeyword or
            SyntaxKind.RepeatKeyword or
            SyntaxKind.IfKeyword or
            SyntaxKind.ElseIfKeyword or
            SyntaxKind.ForKeyword or
            SyntaxKind.FunctionKeyword or
            SyntaxKind.LocalKeyword => true,

            SyntaxKind.EqualsToken => true, // 赋值操作符

            SyntaxKind.CommaToken or // 表达式列表的分隔符
            _ => this.IsPossibleExpression()
        };

#if TESTING
    internal
#else
    private
#endif
        StatementSyntax ParseStatement()
    {
        switch (this.CurrentTokenKind)
        {
            case SyntaxKind.SemicolonToken:
                return this._syntaxFactory.EmptyStatement(this.EatToken());
            case SyntaxKind.ColonColonToken:
                return this.ParseLabelStatement();
            case SyntaxKind.BreakKeyword:
                return this.ParseBreakStatement();
            case SyntaxKind.GotoKeyword:
                return this.ParseGotoStatement();
            case SyntaxKind.ReturnKeyword:
                return this.ParseReturnStatement();
            case SyntaxKind.DoKeyword:
                return this.ParseDoStatement();
            case SyntaxKind.WhileKeyword:
                return this.ParseWhileStatement();
            case SyntaxKind.RepeatKeyword:
                return this.ParseRepeatStatement();
            case SyntaxKind.IfKeyword:
                return this.ParseIfStatement();
            case SyntaxKind.ElseIfKeyword:
                return this.ParseMisplaceElseIf();
            case SyntaxKind.ForKeyword:
                return this.ParseForStatement();
            case SyntaxKind.FunctionKeyword:
                return this.ParseFunctionDefinitionStatement();
            case SyntaxKind.LocalKeyword:
                if (this.PeekToken(1).Kind == SyntaxKind.FunctionKeyword)
                    return this.ParseLocalFunctionDefinitionStatement();
                else
                    return this.ParseLocalDeclarationStatement();

            case SyntaxKind.EqualsToken: // 赋值操作符
                return this.ParseAssignmentStatement();

            case SyntaxKind.CommaToken: // 表达式列表的分隔符
            default:
                return this.ParseStatementStartsWithExpression();
        }
    }

#if TESTING
    internal
#else
    private
#endif
        AssignmentStatementSyntax ParseAssignmentStatement()
    {
        var left = this.ParseAssgLvalueList();
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.EqualsToken);
        var equals = this.EatToken();
        var right = this.ParseExpressionList(minCount: 1);
        return this._syntaxFactory.AssignmentStatement(left, equals, right);
    }

#if TESTING
    internal
#else
    private
#endif
        SeparatedSyntaxList<ExpressionSyntax> ParseAssgLvalueList()
    {
        if (this.CurrentTokenKind != SyntaxKind.CommaToken && !this.IsPossibleExpression())
            return this.CreateMissingExpressionList(ErrorCode.ERR_IdentifierExpected);

        return this.ParseSeparatedSyntaxList(
            predicateNode: _ => true,
            parseNode: (index, missing) =>
            {
                if (!missing && this.IsPossibleExpression())
                {
                    var expr = this.ParseExpression();
                    return expr switch
                    {
                        // 仅标识符语法和成员操作语法（普通或索引）能作为赋值符号左侧表达式。
                        IdentifierNameSyntax or
                        MemberAccessExpressionSyntax => expr,

                        _ => this.AddError(expr, ErrorCode.ERR_AssgLvalueExpected)
                    };
                }
                else
                    return this.AddError(this.CreateMissingIdentifierName(), ErrorCode.ERR_IdentifierExpected);
            },
            predicateSeparator: _ => this.CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => this.EatToken(SyntaxKind.CommaToken),
            minCount: 1);
    }

#if TESTING
    internal
#else
    private
#endif
        LabelStatementSyntax ParseLabelStatement()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.ColonColonToken);
        var leftColonColon = this.EatToken(SyntaxKind.ColonColonToken);
        var labelName = this.ParseIdentifierName();
        var rightColonColon = this.EatToken(SyntaxKind.ColonColonToken);
        return this._syntaxFactory.LabelStatement(leftColonColon, labelName, rightColonColon);
    }

#if TESTING
    internal
#else
    private
#endif
        BreakStatementSyntax ParseBreakStatement()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.BreakKeyword);
        var breakKeyword = this.EatToken(SyntaxKind.BreakKeyword);
        return this._syntaxFactory.BreakStatement(breakKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        GotoStatementSyntax ParseGotoStatement()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.GotoKeyword);
        var gotoKeyword = this.EatToken(SyntaxKind.GotoKeyword);
        var labelName = this.ParseIdentifierName();
        return this._syntaxFactory.GotoStatement(gotoKeyword, labelName);
    }

#if TESTING
    internal
#else
    private
#endif
        ReturnStatementSyntax ParseReturnStatement()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.ReturnKeyword);
        var returnKeyword = this.EatToken(SyntaxKind.ReturnKeyword);
        var expressions = this.ParseExpressionList(minCount: 0);
        return this._syntaxFactory.ReturnStatement(returnKeyword, expressions);
    }

#if TESTING
    internal
#else
    private
#endif
        DoStatementSyntax ParseDoStatement()
    {
        var doKeyword = this.EatToken(SyntaxKind.DoKeyword);
        var block = this.ParseBlock(SyntaxKind.DoStatement);
        var endKeyword = this.EatToken(SyntaxKind.EndKeyword);
        return this._syntaxFactory.DoStatement(doKeyword, block, endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        WhileStatementSyntax ParseWhileStatement()
    {
        var whileKeyword = this.EatToken(SyntaxKind.WhileKeyword);
        var condition = this.ParseExpression();
        var doKeyword = this.EatToken(SyntaxKind.DoKeyword);
        var block = this.ParseBlock(SyntaxKind.WhileStatement);
        var endKeyword = this.EatToken(SyntaxKind.EndKeyword);
        return this._syntaxFactory.WhileStatement(whileKeyword, condition, doKeyword, block, endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        RepeatStatementSyntax ParseRepeatStatement()
    {
        var repeatKeyword = this.EatToken(SyntaxKind.RepeatKeyword);
        var block = this.ParseBlock(SyntaxKind.RepeatStatement);
        var untilKeyword = this.EatToken(SyntaxKind.UntilKeyword);
        var condition = this.ParseExpression();
        return this._syntaxFactory.RepeatStatement(repeatKeyword, block, untilKeyword, condition);
    }

#if TESTING
    internal
#else
    private
#endif
        IfStatementSyntax ParseIfStatement()
    {
        var ifKeyword = this.EatToken(SyntaxKind.IfKeyword);
        var condition = this.ParseExpression();
        var thenKeyword = this.EatToken(SyntaxKind.ThenKeyword);
        var block = this.ParseBlock(SyntaxKind.IfStatement);
        var elseIfClauses = this.ParseElseIfClausesOpt();
        var elseClause = this.ParseElseClauseOpt();
        var endKeyword = this.EatToken(SyntaxKind.EndKeyword);
        return this._syntaxFactory.IfStatement(ifKeyword, condition, thenKeyword, block, elseIfClauses, elseClause, endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        SyntaxList<ElseIfClauseSyntax> ParseElseIfClausesOpt() =>
        this.ParseSyntaxList(
            predicateNode: _ => this.CurrentTokenKind == SyntaxKind.ElseIfKeyword,
            parseNode: (_, _) => this.ParseElseIfClause());

#if TESTING
    internal
#else
    private
#endif
        ElseClauseSyntax? ParseElseClauseOpt() =>
        this.CurrentTokenKind == SyntaxKind.ElseKeyword ?
            this.ParseElseClause() : null;

#if TESTING
    internal
#else
    private
#endif
        ElseIfClauseSyntax ParseElseIfClause()
    {
        var elseIfKeyword = this.EatToken(SyntaxKind.ElseIfKeyword);
        var condition = this.ParseExpression();
        var thenKeyword = this.EatToken(SyntaxKind.ThenKeyword);
        var block = this.ParseBlock(SyntaxKind.ElseIfClause);
        return this._syntaxFactory.ElseIfClause(elseIfKeyword, condition, thenKeyword, block);
    }

#if TESTING
    internal
#else
    private
#endif
        ElseClauseSyntax ParseElseClause()
    {
        var elseKeyword = this.EatToken(SyntaxKind.ElseKeyword);
        var block = this.ParseBlock(SyntaxKind.ElseClause);
        return this._syntaxFactory.ElseClause(elseKeyword, block);
    }

#if TESTING
    internal
#else
    private
#endif
        IfStatementSyntax ParseMisplaceElseIf()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.ElseIfKeyword);

        var ifKeyword = this.EatTokenAsKind(SyntaxKind.IfKeyword);
        var condition = this.ParseExpression();
        var thenKeyword = this.EatToken(SyntaxKind.ThenKeyword);
        var block = this.ParseBlock(SyntaxKind.IfStatement);
        var elseIfClauses = this.ParseElseIfClausesOpt();
        var elseClause = this.ParseElseClauseOpt();
        var endKeyword = this.EatToken(SyntaxKind.EndKeyword);
        return this._syntaxFactory.IfStatement(ifKeyword, condition, thenKeyword, block, elseIfClauses, elseClause, endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        StatementSyntax ParseForStatement()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.ForKeyword);
        var forKeyword = this.EatToken(SyntaxKind.ForKeyword);
        var namesBuilder = this._pool.AllocateSeparated<IdentifierNameSyntax>();
        this.ParseSeparatedSyntaxList(
            namesBuilder,
            predicateNode: _ => true,
            parseNode: (_, _) => this.ParseIdentifierName(),
            predicateSeparator: _ => this.CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => this.EatToken(SyntaxKind.CommaToken),
            minCount: 1);
        switch (this.CurrentTokenKind)
        {
            case SyntaxKind.InKeyword:// 是泛型for循环。
                return this.ParseGenericForStatement(forKeyword, this._pool.ToListAndFree(namesBuilder));
            case SyntaxKind.EqualsToken: // 是算术for循环。
                if (namesBuilder.Count == 1)
                    return this.ParseNumericalForStatement(forKeyword, (IdentifierNameSyntax)namesBuilder[0]!);
                else // 定义了多个标识符。
                {
                    // 将标识符标志及分隔符标志均处理为被跳过的语法标志。
                    var name = this.CreateMissingIdentifierName();
                    var skippedSyntax = this._pool.ToListAndFree(namesBuilder).Node;

                    // 添加被跳过的语法标志。
                    Debug.Assert(skippedSyntax is not null);
                    name = this.AddTrailingSkippedSyntax(name, skippedSyntax);

                    // 添加错误。
                    name = this.AddError(name, ErrorCode.ERR_TooManyIdentifiers);

                    return this.ParseNumericalForStatement(forKeyword, name);
                }
            default: // 不知道是什么结构，推断使用最适合的结构。
                if (namesBuilder.Count == 1) // 单个标识符，推断使用泛型for循环。
                    return this.ParseNumericalForStatement(forKeyword, (IdentifierNameSyntax)namesBuilder[0]!);
                else // 多个标识符，推断使用泛型for循环。
                    return this.ParseGenericForStatement(forKeyword, this._pool.ToListAndFree(namesBuilder));
        }
    }

#if TESTING
    internal
#else
    private
#endif
        ForInStatementSyntax ParseGenericForStatement(SyntaxToken forKeyword, SeparatedSyntaxList<IdentifierNameSyntax> names)
    {
        var inKeyword = this.EatToken(SyntaxKind.InKeyword);
        var expressions = this.ParseExpressionList(minCount: 1);
        var doKeyword = this.EatToken(SyntaxKind.DoKeyword);
        var block = this.ParseBlock(SyntaxKind.ForInStatement);
        var endKeyword = this.EatToken(SyntaxKind.EndKeyword);
        return this._syntaxFactory.ForInStatement(
            forKeyword,
            names,
            inKeyword,
            expressions,
            doKeyword,
            block,
            endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        ForStatementSyntax ParseNumericalForStatement(SyntaxToken forKeyword, IdentifierNameSyntax name)
    {
        var equals = this.EatToken(SyntaxKind.EqualsToken);
        var initial = this.ParseExpression();
        var firstComma = this.EatToken(SyntaxKind.CommaToken);
        var limit = this.ParseExpression();

        SyntaxToken? secondComma = null;
        ExpressionSyntax? step = null;
        if (this.CurrentTokenKind == SyntaxKind.CommaToken)
        {
            secondComma = this.EatToken(SyntaxKind.CommaToken);
            step = this.ParseExpression();
        }

        var doKeyword = this.EatToken(SyntaxKind.DoKeyword);
        var block = this.ParseBlock(SyntaxKind.ForStatement);
        var endKeyword = this.EatToken(SyntaxKind.EndKeyword);
        return this._syntaxFactory.ForStatement(
            forKeyword,
            name,
            equals,
            initial,
            firstComma,
            limit,
            secondComma,
            step,
            doKeyword,
            block,
            endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        FunctionDefinitionStatementSyntax ParseFunctionDefinitionStatement()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.FunctionKeyword);

        var function = this.EatToken(SyntaxKind.FunctionKeyword);
        var name = this.ParseName();
        this.ParseFunctionBody(SyntaxKind.FunctionDefinitionStatement, out var parameterList, out var block, out var end);
        return this._syntaxFactory.FunctionDefinitionStatement(function, name, parameterList, block, end);
    }

#if TESTING
    internal
#else
    private
#endif
        LocalFunctionDefinitionStatementSyntax ParseLocalFunctionDefinitionStatement()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.LocalKeyword);
        Debug.Assert(this.PeekToken(1).Kind == SyntaxKind.FunctionKeyword);

        var local = this.EatToken(SyntaxKind.LocalKeyword);
        var function = this.EatToken(SyntaxKind.FunctionKeyword);
        var name = this.ParseIdentifierName();
        this.ParseFunctionBody(SyntaxKind.LocalFunctionDefinitionStatement, out var parameterList, out var block, out var end);
        return this._syntaxFactory.LocalFunctionDefinitionStatement(local, function, name, parameterList, block, end);
    }

#if TESTING
    internal
#else
    private
#endif
        LocalDeclarationStatementSyntax ParseLocalDeclarationStatement()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.LocalKeyword);

        var local = this.EatToken(SyntaxKind.LocalKeyword);
        var nameAttributeLists = this.ParseSeparatedSyntaxList(
            predicateNode: _ => true,
            parseNode: (_, _) => this.ParseNameAttributeList(),
            predicateSeparator: _ => this.CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => this.EatToken(SyntaxKind.CommaToken),
            minCount: 1);
        var equalsValues = this.CurrentTokenKind == SyntaxKind.EqualsToken ? this.ParseEqualsValuesClause() : null;
        return this._syntaxFactory.LocalDeclarationStatement(local, nameAttributeLists, equalsValues);
    }

#if TESTING
    internal
#else
    private
#endif
        EqualsValuesClauseSyntax ParseEqualsValuesClause()
    {
        Debug.Assert(this.CurrentTokenKind == SyntaxKind.EqualsToken);

        var equals = this.EatToken(SyntaxKind.EqualsToken);
        var values = this.ParseExpressionList(minCount: 1);
        return this._syntaxFactory.EqualsValuesClause(equals, values);
    }

#if TESTING
    internal
#else
    private
#endif
        StatementSyntax ParseStatementStartsWithExpression()
    {
        var resetPoint = this.GetResetPoint();

        var exprList = this.ParseExpressionList(minCount: 1);
        if (this.CurrentTokenKind == SyntaxKind.EqualsToken)
        {
            // 按照赋值语句解析。
            this.Reset(ref resetPoint);
            this.Release(ref resetPoint);
            return this.ParseAssignmentStatement();
        }
        else if (exprList.Count == 1 && exprList[0] is InvocationExpressionSyntax invocationExpression)
        {
            // 按照调用语句解析。
            this.Release(ref resetPoint);
            return this._syntaxFactory.InvocationStatement(invocationExpression);
        }
        else
        {
            // 否则解析为空语句，报告不合法语句错误，将整个表达式列表添加入空语句的前方跳过的标志的语法琐碎。
            var semicolon = this.AddLeadingSkippedSyntax(
                SyntaxFactory.MissingToken(SyntaxKind.SemicolonToken),
                this.AddError(exprList.Node!, ErrorCode.ERR_IllegalStatement));
            this.Release(ref resetPoint);
            return this._syntaxFactory.EmptyStatement(semicolon);
        }
    }
}
