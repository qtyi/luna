﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Microsoft.CodeAnalysis.Text;
using GreenNode = Microsoft.CodeAnalysis.GreenNode;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisSyntaxNode = Lua.LuaSyntaxNode;
using ThisInternalSyntaxNode = LuaSyntaxNode;
using ThisInternalSyntaxVisitor_SyntaxToken = LuaSyntaxVisitor<SyntaxToken>;
using ThisInternalSyntaxVisitor_SyntaxNode = LuaSyntaxVisitor<LuaSyntaxNode>;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisSyntaxNode = MoonScript.MoonScriptSyntaxNode;
using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
using ThisInternalSyntaxVisitor_SyntaxToken = MoonScriptSyntaxVisitor<SyntaxToken>;
using ThisInternalSyntaxVisitor_SyntaxNode = MoonScriptSyntaxVisitor<MoonScriptSyntaxNode>;
#endif

internal partial class LanguageParser : SyntaxParser
{
    private readonly SyntaxListPool _pool = new();

    private readonly SyntaxFactoryContext _syntaxFactoryContext;
    private readonly ContextAwareSyntax _syntaxFactory;

    private int _recursionDepth; // 递归深度。
    private TerminatorState _terminatorState;

    internal LanguageParser(
        Lexer lexer,
        ThisSyntaxNode? oldTree,
        IEnumerable<TextChangeRange>? changes,
        LexerMode lexerMode = LexerMode.Syntax,
        CancellationToken cancellationToken = default
    ) : base(
        lexer,
        lexerMode,
        oldTree,
        changes,
        allowModeReset: false,
        preLexIfNotIncremental: true,
        cancellationToken: cancellationToken
    )
    {
        this._syntaxFactoryContext = new();
        this._syntaxFactory = new(this._syntaxFactoryContext);
    }

    private bool IsIncrementalAndFactoryContextMatches
    {
        get
        {
            if (!base.IsIncremental) return false;

            var node = this.CurrentNode;
            return node is not null && LanguageParser.MatchesFactoryContext(node.Green, this._syntaxFactoryContext);
        }
    }

    internal static partial bool MatchesFactoryContext(GreenNode green, SyntaxFactoryContext context);

#if TESTING
    internal
#else
    private
#endif
        bool IsTerminal()
    {
        if (this.CurrentTokenKind == SyntaxKind.EndOfFileToken) return true;

        for (var i = 1; i < LanguageParser.LastTerminatorState; i <<= 1)
        {
            var state = (TerminatorState)i;
            if (IsTerminalCore(state)) return true;
        }

        return false;
    }

    private partial bool IsTerminalCore(TerminatorState state);

    #region ParseWithStackGuard
    /// <summary>
    /// 在执行栈空间保护下解析节点。
    /// </summary>
    /// <typeparam name="TNode">要解析的节点的类型。</typeparam>
    /// <param name="parseFunc">解析节点的函数。</param>
    /// <param name="createEmptyNodeFunc">创建一个类型为<typeparamref name="TNode"/>的空节点的函数。</param>
    /// <returns>解析成功时，返回得到的节点；否则返回带有错误信息的空节点。</returns>
    internal TNode ParseWithStackGuard<TNode>(Func<TNode> parseFunc, Func<TNode> createEmptyNodeFunc)
        where TNode : ThisInternalSyntaxNode
    {
        // 确保每次调用此函数时递归深度均为初始值。
        // 若断言失败，则表示在此函数的内部再次调用了此函数。虽然不算错误，但因为这样很低效，所以应当避免。
        Debug.Assert(_recursionDepth == 0);

        try
        {
            return parseFunc();
        }
        catch (InsufficientExecutionStackException)
        {
            return this.CreateForGlobalFailure(lexer.TextWindow.Position, createEmptyNodeFunc());
        }
    }

    /// <summary>
    /// 将整个输入转换为单个语法标记，这个语法标记将被跳过。
    /// </summary>
    private TNode CreateForGlobalFailure<TNode>(int position, TNode node)
        where TNode : ThisInternalSyntaxNode
    {
        var builder = new SyntaxListBuilder(1);
        builder.Add(SyntaxFactory.BadToken(null, lexer.TextWindow.Text.ToString(), null));
        var fileAsTrivia = _syntaxFactory.SkippedTokensTrivia(builder.ToList<SyntaxToken>());

        node = AddLeadingSkippedSyntax(node, fileAsTrivia);
        this.ForceEndOfFile(); // 强制使当前的语法标记为文件结尾标记。

        return this.AddError(node, position, 0, ErrorCode.ERR_InsufficientStack);
    }
    #endregion

    #region SkipTokensAndNodes
    private GreenNode? SkipTokens(Func<SyntaxToken, bool> predicate, ThisInternalSyntaxVisitor_SyntaxToken? visitor = null)
    {
        if (predicate(this.CurrentToken))
        {
            var builder = this._pool.Allocate<SyntaxToken>();
            do
            {
                var token = this.EatToken();
                builder.Add(visitor is null ? token : visitor.Visit(token));
            }
            while (predicate(this.CurrentToken));
            return this._pool.ToListAndFree(builder).Node;
        }

        return null;
    }

    private GreenNode? SkipTokensAndExpressions(Func<SyntaxToken, bool> predicate, ThisInternalSyntaxVisitor_SyntaxNode? visitor = null)
    {
        var builder = this._pool.Allocate<ThisInternalSyntaxNode>();
        while (true)
        {
            if (this.IsPossibleExpression())
            {
                var expr = this.ParseExpressionCore();
                builder.Add(visitor is null ? expr : visitor.Visit(expr));
            }
            else if (predicate(this.CurrentToken))
            {
                var token = this.EatToken();
                builder.Add(visitor is null ? token : visitor.Visit(token));
            }
            else break;
        }

        if (builder.Count == 0)
        {
            this._pool.Free(builder);
            return null;
        }
        else
            return this._pool.ToListAndFree(builder).Node;
    }

    private GreenNode? SkipTokensAndStatements(Func<SyntaxToken, bool> predicate, ThisInternalSyntaxVisitor_SyntaxNode? visitor = null)
    {
        var builder = this._pool.Allocate<ThisInternalSyntaxNode>();
        while (true)
        {
            if (this.IsPossibleStatement())
            {
                var stat = this.ParseStatement();
                builder.Add(visitor is null ? stat : visitor.Visit(stat));
            }
            else if (predicate(this.CurrentToken))
            {
                var token = this.EatToken();
                builder.Add(visitor is null ? token : visitor.Visit(token));
            }
            else break;
        }

        if (builder.Count == 0)
        {
            this._pool.Free(builder);
            return null;
        }
        else
            return this._pool.ToListAndFree(builder).Node;
    }
    #endregion

    #region ParseSyntaxList & ParseSeparatedSyntaxList
    private void ParseSyntaxList<TNode>(
        in SyntaxListBuilder<TNode> builder,
        Func<int, bool> predicateNode,
        Func<int, bool, TNode> parseNode,
        int minCount = 0)
        where TNode : ThisInternalSyntaxNode
    {
        var lastTokenPosition = -1;
        var index = 0;
        while (this.CurrentTokenKind != SyntaxKind.EndOfFileToken &&
            this.IsMakingProgress(ref lastTokenPosition))
        {
            if (!predicateNode(index)) break;

            const bool missing = false;
            var node = parseNode(index, missing);
            builder.Add(node);

            index++;
        }
        // 处理缺失（最小数量不足）的部分。
        while (index < minCount)
        {
            const bool missing = true;
            var node = parseNode(index, missing);
            builder.Add(node);

            index++;
        }
    }

    private SyntaxList<TNode> ParseSyntaxList<TNode>(
        Func<int, bool> predicateNode,
        Func<int, bool, TNode> parseNode,
        int minCount = 0)
        where TNode : ThisInternalSyntaxNode
    {
        var builder = this._pool.Allocate<TNode>();
        this.ParseSyntaxList(builder, predicateNode, parseNode, minCount);
        var list = this._pool.ToListAndFree(builder);
        return list;
    }

    private TList? ParseSyntaxList<TNode, TList>(
        Func<int, bool> predicateNode,
        Func<int, bool, TNode> parseNode,
        Func<SyntaxList<TNode>, TList?> createListFunc,
        int minCount = 0)
        where TNode : ThisInternalSyntaxNode
        where TList : ThisInternalSyntaxNode
    {
        var list = createListFunc(this.ParseSyntaxList(predicateNode, parseNode, minCount));
        return list;
    }

    private void ParseSeparatedSyntaxList<TNode, TSeparator>(
        in SeparatedSyntaxListBuilder<TNode> builder,
        Func<int, bool> predicateNode,
        Func<int, bool, TNode> parseNode,
        Func<int, bool> predicateSeparator,
        Func<int, bool, TSeparator> parseSeparator,
        bool allowTrailingSeparator = false,
        int minCount = 0)
        where TNode : ThisInternalSyntaxNode
        where TSeparator : ThisInternalSyntaxNode
    {
        var index = 0;
        if (predicateNode(index))
        {
            const bool missing = false;

            var node = parseNode(index, missing);
            builder.Add(node);

            var lastTokenPosition = -1;
            index = 1;
            while (this.CurrentTokenKind != SyntaxKind.EndOfFileToken &&
                this.IsMakingProgress(ref lastTokenPosition))
            {
                if (!predicateSeparator(index - 1)) break;

                var resetPoint = this.GetResetPoint();

                var separator = parseSeparator(index - 1, missing);
                if (predicateNode(index))
                {
                    builder.AddSeparator(separator);

                    node = parseNode(index, missing);
                    builder.Add(node);

                    index++;
                    this.Release(ref resetPoint);
                }
                else if (allowTrailingSeparator) // 刚才解析的分隔为最后一个语法节点，处理结束后直接退出循环。
                {
                    builder.AddSeparator(separator);
                    this.Release(ref resetPoint);
                    break;
                }
                else // 无法继续，恢复到上一个重置点并退出循环。
                {
                    this.Reset(ref resetPoint);
                    this.Release(ref resetPoint);
                    break;
                }
            }
        }

        // 处理缺失（最小数量不足）的部分。
        if (index < minCount)
        {
            const bool missing = true;

            if (index == 0)
            {
                var node = parseNode(0, missing);
                builder.Add(node);
            }

            index = 1;
            while (index < minCount)
            {
                var separator = parseSeparator(index - 1, missing);
                builder.AddSeparator(separator);

                var node = parseNode(index, missing);
                builder.Add(node);

                index++;
            }
        }
    }

    private SeparatedSyntaxList<TNode> ParseSeparatedSyntaxList<TNode, TSeparator>(
        Func<int, bool> predicateNode,
        Func<int, bool, TNode> parseNode,
        Func<int, bool> predicateSeparator,
        Func<int, bool, TSeparator> parseSeparator,
        bool allowTrailingSeparator = false,
        int minCount = 0)
        where TNode : ThisInternalSyntaxNode
        where TSeparator : ThisInternalSyntaxNode
    {
        var builder = this._pool.AllocateSeparated<TNode>();
        this.ParseSeparatedSyntaxList(builder, predicateNode, parseNode, predicateSeparator, parseSeparator, allowTrailingSeparator, minCount);
        var list = this._pool.ToListAndFree(builder);
        return list;
    }

    private TList? ParseSeparatedSyntaxList<TNode, TSeparator, TList>(
        Func<int, bool> predicateNode,
        Func<int, bool, TNode> parseNodeFunc,
        Func<int, bool> predicateSeparator,
        Func<int, bool, TSeparator> parseSeparator,
        Func<SeparatedSyntaxList<TNode>, TList?> createListFunc,
        bool allowTrailingSeparator = false,
        int minCount = 0)
        where TNode : ThisInternalSyntaxNode
        where TSeparator : ThisInternalSyntaxNode
        where TList : ThisInternalSyntaxNode
    {
        var list = createListFunc(this.ParseSeparatedSyntaxList(predicateNode, parseNodeFunc, predicateSeparator, parseSeparator, allowTrailingSeparator, minCount));
        return list;
    }
    #endregion
}
