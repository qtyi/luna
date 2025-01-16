// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class LanguageParser
{
#if TESTING
    internal
#else
    private
#endif
        void ParseStatements(in SyntaxListBuilder<StatementSyntax> statementsBuilder) =>
        ParseSyntaxList(
            statementsBuilder,
            predicateNode: _ =>
            {
                // 后续不是合法语句时停止。
                if (!IsPossibleStatement()) return false;

                // 正在解析if/elseif语句时遇到elseif关键字时停止。
                if (_syntaxFactoryContext.IsInIfOrElseIf && CurrentTokenKind == SyntaxKind.ElseIfKeyword) return false;

                // 正常处理除返回语句外的其他语句。
                if (CurrentTokenKind != SyntaxKind.ReturnKeyword) return true;

                // 尝试解析这个返回语句。
                var resetPoint = GetResetPoint();
                var returnStat = ParseReturnStatement();

                // 跳过返回语句后方所有连续的空语句。
                while (CurrentTokenKind == SyntaxKind.SemicolonToken)
                    EatToken();

                if (_syntaxFactoryContext.IsInIfOrElseIf && CurrentTokenKind == SyntaxKind.ElseIfKeyword) // 正在解析if/elseif语句时遇到elseif语句时停止。
                {
                    Reset(ref resetPoint);
                    Release(ref resetPoint);
                    return false;
                }
                else if (IsPossibleStatement()) // 后方还有合法的语句，则此返回语句仅为位置错误。
                {
                    Reset(ref resetPoint);
                    Release(ref resetPoint);
                    return true;
                }
                else // 否则此返回语句可能是块的最后一个语句。
                {
                    Reset(ref resetPoint);
                    Release(ref resetPoint);
                    return false;
                }
            },
            parseNode: (_, _) =>
            {
                var stat = ParseStatement();
                if (stat is ReturnStatementSyntax) // 此返回语句位置错误，报告错误。
                    return AddError(stat, ErrorCode.ERR_MisplacedReturnStat);
                else
                    return stat;
            });

    private partial bool IsPossibleStatement() =>
        CurrentTokenKind switch
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
            _ => IsPossibleExpression()
        };

    private partial StatementSyntax ParseStatement()
    {
        switch (CurrentTokenKind)
        {
            case SyntaxKind.SemicolonToken:
                return _syntaxFactory.EmptyStatement(EatToken());
            case SyntaxKind.ColonColonToken:
                return ParseLabelStatement();
            case SyntaxKind.BreakKeyword:
                return ParseBreakStatement();
            case SyntaxKind.GotoKeyword:
                return ParseGotoStatement();
            case SyntaxKind.ReturnKeyword:
                return ParseReturnStatement();
            case SyntaxKind.DoKeyword:
                return ParseDoStatement();
            case SyntaxKind.WhileKeyword:
                return ParseWhileStatement();
            case SyntaxKind.RepeatKeyword:
                return ParseRepeatStatement();
            case SyntaxKind.IfKeyword:
                return ParseIfStatement();
            case SyntaxKind.ElseIfKeyword:
                return ParseMisplaceElseIf();
            case SyntaxKind.ForKeyword:
                return ParseForStatement();
            case SyntaxKind.FunctionKeyword:
                return ParseFunctionDefinitionStatement();
            case SyntaxKind.LocalKeyword:
                if (PeekToken(1).Kind == SyntaxKind.FunctionKeyword)
                    return ParseLocalFunctionDefinitionStatement();
                else
                    return ParseLocalDeclarationStatement();

            case SyntaxKind.EqualsToken: // 赋值操作符
                return ParseAssignmentStatement();

            case SyntaxKind.CommaToken: // 表达式列表的分隔符
            default:
                return ParseStatementStartsWithExpression();
        }
    }

#if TESTING
    internal
#else
    private
#endif
        AssignmentStatementSyntax ParseAssignmentStatement()
    {
        var left = ParseAssgLvalueList();
        Debug.Assert(CurrentTokenKind == SyntaxKind.EqualsToken);
        var equals = EatToken();
        var right = ParseExpressionList(minCount: 1);
        return _syntaxFactory.AssignmentStatement(left, equals, right);
    }

#if TESTING
    internal
#else
    private
#endif
        SeparatedSyntaxList<ExpressionSyntax> ParseAssgLvalueList()
    {
        if (CurrentTokenKind != SyntaxKind.CommaToken && !IsPossibleExpression())
            return CreateMissingExpressionList(ErrorCode.ERR_IdentifierExpected);

        return ParseSeparatedSyntaxList(
            predicateNode: _ => true,
            parseNode: (index, missing) =>
            {
                if (!missing && IsPossibleExpression())
                {
                    var expr = ParseExpression();
                    return expr switch
                    {
                        // 仅标识符语法和成员操作语法（普通或索引）能作为赋值符号左侧表达式。
                        IdentifierNameSyntax or
                        MemberAccessExpressionSyntax => expr,

                        _ => AddError(expr, ErrorCode.ERR_AssgLvalueExpected)
                    };
                }
                else
                    return AddError(CreateMissingIdentifierName(), ErrorCode.ERR_IdentifierExpected);
            },
            predicateSeparator: _ => CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => EatToken(SyntaxKind.CommaToken),
            minCount: 1);
    }

#if TESTING
    internal
#else
    private
#endif
        LabelStatementSyntax ParseLabelStatement()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.ColonColonToken);
        var leftColonColon = EatToken(SyntaxKind.ColonColonToken);
        var labelName = ParseIdentifierName();
        var rightColonColon = EatToken(SyntaxKind.ColonColonToken);
        return _syntaxFactory.LabelStatement(leftColonColon, labelName, rightColonColon);
    }

#if TESTING
    internal
#else
    private
#endif
        BreakStatementSyntax ParseBreakStatement()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.BreakKeyword);
        var breakKeyword = EatToken(SyntaxKind.BreakKeyword);
        return _syntaxFactory.BreakStatement(breakKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        GotoStatementSyntax ParseGotoStatement()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.GotoKeyword);
        var gotoKeyword = EatToken(SyntaxKind.GotoKeyword);
        var labelName = ParseIdentifierName();
        return _syntaxFactory.GotoStatement(gotoKeyword, labelName);
    }

#if TESTING
    internal
#else
    private
#endif
        ReturnStatementSyntax ParseReturnStatement()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.ReturnKeyword);
        var returnKeyword = EatToken(SyntaxKind.ReturnKeyword);
        var expressions = ParseExpressionList(minCount: 0);
        return _syntaxFactory.ReturnStatement(returnKeyword, expressions);
    }

#if TESTING
    internal
#else
    private
#endif
        DoStatementSyntax ParseDoStatement()
    {
        var doKeyword = EatToken(SyntaxKind.DoKeyword);
        var block = ParseBlock(SyntaxKind.DoStatement);
        var endKeyword = EatToken(SyntaxKind.EndKeyword);
        return _syntaxFactory.DoStatement(doKeyword, block, endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        WhileStatementSyntax ParseWhileStatement()
    {
        var whileKeyword = EatToken(SyntaxKind.WhileKeyword);
        var condition = ParseExpression();
        var doKeyword = EatToken(SyntaxKind.DoKeyword);
        var block = ParseBlock(SyntaxKind.WhileStatement);
        var endKeyword = EatToken(SyntaxKind.EndKeyword);
        return _syntaxFactory.WhileStatement(whileKeyword, condition, doKeyword, block, endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        RepeatStatementSyntax ParseRepeatStatement()
    {
        var repeatKeyword = EatToken(SyntaxKind.RepeatKeyword);
        var block = ParseBlock(SyntaxKind.RepeatStatement);
        var untilKeyword = EatToken(SyntaxKind.UntilKeyword);
        var condition = ParseExpression();
        return _syntaxFactory.RepeatStatement(repeatKeyword, block, untilKeyword, condition);
    }

#if TESTING
    internal
#else
    private
#endif
        IfStatementSyntax ParseIfStatement()
    {
        var ifKeyword = EatToken(SyntaxKind.IfKeyword);
        var condition = ParseExpression();
        var thenKeyword = EatToken(SyntaxKind.ThenKeyword);
        var block = ParseBlock(SyntaxKind.IfStatement);
        var elseIfClauses = ParseElseIfClausesOpt();
        var elseClause = ParseElseClauseOpt();
        var endKeyword = EatToken(SyntaxKind.EndKeyword);
        return _syntaxFactory.IfStatement(ifKeyword, condition, thenKeyword, block, elseIfClauses, elseClause, endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        SyntaxList<ElseIfClauseSyntax> ParseElseIfClausesOpt() =>
        ParseSyntaxList(
            predicateNode: _ => CurrentTokenKind == SyntaxKind.ElseIfKeyword,
            parseNode: (_, _) => ParseElseIfClause());

#if TESTING
    internal
#else
    private
#endif
        ElseClauseSyntax? ParseElseClauseOpt() =>
        CurrentTokenKind == SyntaxKind.ElseKeyword ?
            ParseElseClause() : null;

#if TESTING
    internal
#else
    private
#endif
        ElseIfClauseSyntax ParseElseIfClause()
    {
        var elseIfKeyword = EatToken(SyntaxKind.ElseIfKeyword);
        var condition = ParseExpression();
        var thenKeyword = EatToken(SyntaxKind.ThenKeyword);
        var block = ParseBlock(SyntaxKind.ElseIfClause);
        return _syntaxFactory.ElseIfClause(elseIfKeyword, condition, thenKeyword, block);
    }

#if TESTING
    internal
#else
    private
#endif
        ElseClauseSyntax ParseElseClause()
    {
        var elseKeyword = EatToken(SyntaxKind.ElseKeyword);
        var block = ParseBlock(SyntaxKind.ElseClause);
        return _syntaxFactory.ElseClause(elseKeyword, block);
    }

#if TESTING
    internal
#else
    private
#endif
        IfStatementSyntax ParseMisplaceElseIf()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.ElseIfKeyword);

        var ifKeyword = EatTokenAsKind(SyntaxKind.IfKeyword);
        var condition = ParseExpression();
        var thenKeyword = EatToken(SyntaxKind.ThenKeyword);
        var block = ParseBlock(SyntaxKind.IfStatement);
        var elseIfClauses = ParseElseIfClausesOpt();
        var elseClause = ParseElseClauseOpt();
        var endKeyword = EatToken(SyntaxKind.EndKeyword);
        return _syntaxFactory.IfStatement(ifKeyword, condition, thenKeyword, block, elseIfClauses, elseClause, endKeyword);
    }

#if TESTING
    internal
#else
    private
#endif
        StatementSyntax ParseForStatement()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.ForKeyword);
        var forKeyword = EatToken(SyntaxKind.ForKeyword);
        var namesBuilder = _pool.AllocateSeparated<IdentifierNameSyntax>();
        ParseSeparatedSyntaxList(
            namesBuilder,
            predicateNode: _ => true,
            parseNode: (_, _) => ParseIdentifierName(),
            predicateSeparator: _ => CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => EatToken(SyntaxKind.CommaToken),
            minCount: 1);
        switch (CurrentTokenKind)
        {
            case SyntaxKind.InKeyword:// 是泛型for循环。
                return ParseGenericForStatement(forKeyword, _pool.ToListAndFree(namesBuilder));
            case SyntaxKind.EqualsToken: // 是算术for循环。
                if (namesBuilder.Count == 1)
                    return ParseNumericalForStatement(forKeyword, (IdentifierNameSyntax)namesBuilder[0]!);
                else // 定义了多个标识符。
                {
                    // 将标识符标记及分隔符标记均处理为被跳过的语法标记。
                    var name = CreateMissingIdentifierName();
                    var skippedSyntax = _pool.ToListAndFree(namesBuilder).Node;

                    // 添加被跳过的语法标记。
                    Debug.Assert(skippedSyntax is not null);
                    name = AddTrailingSkippedSyntax(name, skippedSyntax);

                    // 添加错误。
                    name = AddError(name, ErrorCode.ERR_TooManyIdentifiers);

                    return ParseNumericalForStatement(forKeyword, name);
                }
            default: // 不知道是什么结构，推断使用最适合的结构。
                if (namesBuilder.Count == 1) // 单个标识符，推断使用泛型for循环。
                    return ParseNumericalForStatement(forKeyword, (IdentifierNameSyntax)namesBuilder[0]!);
                else // 多个标识符，推断使用泛型for循环。
                    return ParseGenericForStatement(forKeyword, _pool.ToListAndFree(namesBuilder));
        }
    }

#if TESTING
    internal
#else
    private
#endif
        ForInStatementSyntax ParseGenericForStatement(SyntaxToken forKeyword, SeparatedSyntaxList<IdentifierNameSyntax> names)
    {
        var inKeyword = EatToken(SyntaxKind.InKeyword);
        var expressions = ParseExpressionList(minCount: 1);
        var doKeyword = EatToken(SyntaxKind.DoKeyword);
        var block = ParseBlock(SyntaxKind.ForInStatement);
        var endKeyword = EatToken(SyntaxKind.EndKeyword);
        return _syntaxFactory.ForInStatement(
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
        var equals = EatToken(SyntaxKind.EqualsToken);
        var initial = ParseExpression();
        var firstComma = EatToken(SyntaxKind.CommaToken);
        var limit = ParseExpression();

        SyntaxToken? secondComma = null;
        ExpressionSyntax? step = null;
        if (CurrentTokenKind == SyntaxKind.CommaToken)
        {
            secondComma = EatToken(SyntaxKind.CommaToken);
            step = ParseExpression();
        }

        var doKeyword = EatToken(SyntaxKind.DoKeyword);
        var block = ParseBlock(SyntaxKind.ForStatement);
        var endKeyword = EatToken(SyntaxKind.EndKeyword);
        return _syntaxFactory.ForStatement(
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
        Debug.Assert(CurrentTokenKind == SyntaxKind.FunctionKeyword);

        var function = EatToken(SyntaxKind.FunctionKeyword);
        var name = ParseName();
        ParseFunctionBody(SyntaxKind.FunctionDefinitionStatement, out var parameterList, out var block, out var end);
        return _syntaxFactory.FunctionDefinitionStatement(function, name, parameterList, block, end);
    }

#if TESTING
    internal
#else
    private
#endif
        LocalFunctionDefinitionStatementSyntax ParseLocalFunctionDefinitionStatement()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.LocalKeyword);
        Debug.Assert(PeekToken(1).Kind == SyntaxKind.FunctionKeyword);

        var local = EatToken(SyntaxKind.LocalKeyword);
        var function = EatToken(SyntaxKind.FunctionKeyword);
        var name = ParseIdentifierName();
        ParseFunctionBody(SyntaxKind.LocalFunctionDefinitionStatement, out var parameterList, out var block, out var end);
        return _syntaxFactory.LocalFunctionDefinitionStatement(local, function, name, parameterList, block, end);
    }

#if TESTING
    internal
#else
    private
#endif
        LocalDeclarationStatementSyntax ParseLocalDeclarationStatement()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.LocalKeyword);

        var local = EatToken(SyntaxKind.LocalKeyword);
        var nameAttributeLists = ParseSeparatedSyntaxList(
            predicateNode: _ => true,
            parseNode: (_, _) => ParseNameAttributeList(),
            predicateSeparator: _ => CurrentTokenKind == SyntaxKind.CommaToken,
            parseSeparator: (_, _) => EatToken(SyntaxKind.CommaToken),
            minCount: 1);
        var equalsValues = CurrentTokenKind == SyntaxKind.EqualsToken ? ParseEqualsValuesClause() : null;
        return _syntaxFactory.LocalDeclarationStatement(local, nameAttributeLists, equalsValues);
    }

#if TESTING
    internal
#else
    private
#endif
        EqualsValuesClauseSyntax ParseEqualsValuesClause()
    {
        Debug.Assert(CurrentTokenKind == SyntaxKind.EqualsToken);

        var equals = EatToken(SyntaxKind.EqualsToken);
        var values = ParseExpressionList(minCount: 1);
        return _syntaxFactory.EqualsValuesClause(equals, values);
    }

#if TESTING
    internal
#else
    private
#endif
        StatementSyntax ParseStatementStartsWithExpression()
    {
        var resetPoint = GetResetPoint();

        var exprList = ParseExpressionList(minCount: 1);
        if (CurrentTokenKind == SyntaxKind.EqualsToken)
        {
            // 按照赋值语句解析。
            Reset(ref resetPoint);
            Release(ref resetPoint);
            return ParseAssignmentStatement();
        }
        else if (exprList.Count == 1 && exprList[0] is InvocationExpressionSyntax invocationExpression)
        {
            // 按照调用语句解析。
            Release(ref resetPoint);
            return _syntaxFactory.InvocationStatement(invocationExpression);
        }
        else
        {
            // 否则解析为空语句，报告不合法语句错误，将整个表达式列表添加入空语句的前方跳过的标记的语法琐碎。
            var semicolon = AddLeadingSkippedSyntax(
                ThisInternalSyntaxFactory.MissingToken(SyntaxKind.SemicolonToken),
                AddError(exprList.Node!, ErrorCode.ERR_IllegalStatement));
            Release(ref resetPoint);
            return _syntaxFactory.EmptyStatement(semicolon);
        }
    }
}
