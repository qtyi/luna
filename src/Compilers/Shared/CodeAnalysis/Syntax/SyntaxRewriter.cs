// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxNode = MoonScriptSyntaxNode;
#endif

using Syntax;

/// <summary>
/// Represents a <see cref="ThisSyntaxVisitor{TResult}"/> which descends an entire <see cref="ThisSyntaxNode"/> graph and
/// may replace or remove visited SyntaxNodes in depth-first order.
/// </summary>
public abstract partial class
#if LANG_LUA
    LuaSyntaxRewriter
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxRewriter
#endif
{
    private readonly bool _visitIntoStructuredTrivia;

    public virtual bool VisitIntoStructuredTrivia => this._visitIntoStructuredTrivia;

    public
#if LANG_LUA
        LuaSyntaxRewriter
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxRewriter
#endif
        (bool visitIntoStructuredTrivia = false) => this._visitIntoStructuredTrivia = visitIntoStructuredTrivia;

    /// <summary>
    /// 当前的递归深度。
    /// </summary>
    private int _recursionDepth;

    [return: NotNullIfNotNull(nameof(node))]
    public override SyntaxNode? Visit(SyntaxNode? node)
    {
        if (node is null) return null;

        // 确保当前递归深度不超出安全限制。
        this._recursionDepth++;
        StackGuard.EnsureSufficientExecutionStack(this._recursionDepth);

        var result = ((ThisSyntaxNode)node).Accept(this); // 获取访问结果。
        Debug.Assert(result is not null);

        this._recursionDepth--;

        return result;
    }

    /// <summary>
    /// 处理语法标记并产生结果。
    /// </summary>
    /// <param name="token">要进行处理的语法标记。</param>
    /// <returns>产生的结果。</returns>
    public virtual SyntaxToken VisitToken(SyntaxToken token)
    {
        // 此方法为频繁调用方法，所以应优化以下几项：
        // 1. 虚方法调用；
        // 2. 结构的复制；
        // 3. 重复的null检查。

        // 避免一次以上的null检查。
        var node = token.Node;
        if (node is null) return token;

        // 调用虚方法获取起始和结尾的语法琐碎内容。
        var leadingTrivia = node.GetLeadingTriviaCore();
        var trailingTrivia = node.GetTrailingTriviaCore();

        // 语法琐碎内容必定为null或非空列表两种情况。
        Debug.Assert(leadingTrivia is null || !leadingTrivia.IsList || leadingTrivia.SlotCount > 0);
        Debug.Assert(trailingTrivia is null || !trailingTrivia.IsList || trailingTrivia.SlotCount > 0);

        if (leadingTrivia is not null)
        {
            // 当节点不为null时展开token.LeadingTrivia。
            var leading = this.VisitList(new SyntaxTriviaList(token, leadingTrivia));

            if (trailingTrivia is null) // 只有起始的语法琐碎内容
            {
                return leading.Node != leadingTrivia ? token.WithLeadingTrivia(leading) : token;
            }
            else // 同时具有起始和结尾的语法琐碎内容
            {
                // 当节点和起始的语法琐碎内容均不为null时展开token.TrailingTrivia。
                // 避免调用node.Width，因为它会调用虚方法GetText，所以使用node.FullWidth。
                var index = leadingTrivia.IsList ? leadingTrivia.SlotCount : 1;
                var trailing = this.VisitList(new SyntaxTriviaList(token, trailingTrivia, token.Position + (node.FullWidth - trailingTrivia.FullWidth), index));

                if (leading.Node != leadingTrivia)
                    token = token.WithLeadingTrivia(leading);

                return trailing.Node != trailingTrivia ? token.WithTrailingTrivia(trailing) : token;
            }
        }
        else if (trailingTrivia is not null) // 只有结尾的语法琐碎内容
        {
            // 当节点不为null且起始的语法琐碎内容为null时展开token.TrailingTrivia。
            // 避免调用node.Width，因为它会调用虚方法GetText，所以使用node.FullWidth。
            var trailing = this.VisitList(new SyntaxTriviaList(token, trailingTrivia, token.Position + (node.FullWidth - trailingTrivia.FullWidth), index: 0));
            return trailing.Node != trailingTrivia ? token.WithTrailingTrivia(trailing) : token;
        }
        else // 没有语法琐碎内容。
            return token;
    }

    /// <summary>
    /// 处理语法琐碎内容并产生结果。
    /// </summary>
    /// <param name="trivia">要进行处理的语法琐碎内容。</param>
    /// <returns>产生的结果。</returns>
    public virtual SyntaxTrivia VisitTrivia(SyntaxTrivia trivia)
    {
        if (this.VisitIntoStructuredTrivia && trivia.HasStructure)
        {
            var structure = (ThisSyntaxNode)trivia.GetStructure()!;
            var newStructure = (StructuredTriviaSyntax)this.Visit(structure);
            if (newStructure != structure)
                return SyntaxFactory.Trivia(newStructure);
        }

        return trivia;
    }

    public virtual SyntaxList<TNode> VisitList<TNode>(SyntaxList<TNode> list) where TNode : SyntaxNode
    {
        SyntaxListBuilder? alternate = null;
        for (int i = 0, n = list.Count; i < n; i++)
        {
            var item = list[i];
            var visited = this.VisitListElement(item);
            if (item != visited && alternate is null)
            {
                alternate = new(n);
                alternate.AddRange(list, 0, i);
            }

            if (alternate is not null && visited is not null && !visited.IsKind(SyntaxKind.None))
            {
                alternate.Add(visited);
            }
        }

        if (alternate is null) return list;
        else return alternate.ToList();
    }

    [return: NotNullIfNotNull(nameof(node))]
    public virtual TNode? VisitListElement<TNode>(TNode? node) where TNode : SyntaxNode
        => (TNode?)this.Visit(node);

    public virtual SeparatedSyntaxList<TNode> VisitList<TNode>(SeparatedSyntaxList<TNode> list) where TNode : SyntaxNode
    {
        var count = list.Count;
        var sepCount = list.SeparatorCount;

        SeparatedSyntaxListBuilder<TNode> alternate = default;

        var i = 0;
        for (; i < sepCount; i++)
        {
            var node = list[i];
            var visitedNode = this.VisitListElement(node);

            var separator = list.GetSeparator(i);
            var visitedSeparator = this.VisitListSeparator(separator);

            if (alternate.IsNull)
            {
                if (node != visitedNode || separator != visitedSeparator)
                {
                    alternate = new(count);
                    alternate.AddRange(list, i);
                }
            }

            if (!alternate.IsNull)
            {
                if (visitedNode is null)
                    throw new InvalidOperationException(CodeAnalysisResources.ElementIsExpected);

                alternate.Add(visitedNode);

                if (visitedSeparator.IsKind(SyntaxKind.None))
                    throw new InvalidOperationException(CodeAnalysisResources.SeparatorIsExpected);
                alternate.AddSeparator(visitedSeparator);
            }
        }

        if (i < count)
        {
            var node = list[i];
            var visitedNode = this.VisitListElement(node);

            if (alternate.IsNull)
            {
                if (node != visitedNode)
                {
                    alternate = new(count);
                    alternate.AddRange(list, i);
                }
            }

            if (!alternate.IsNull)
            {
                if (visitedNode is not null)
                {
                    alternate.Add(visitedNode);
                }
            }
        }

        if (alternate.IsNull) return list;
        else return alternate.ToList();
    }

    public virtual SyntaxToken VisitListSeparator(SyntaxToken separator) => this.VisitToken(separator);

    public virtual SyntaxTokenList VisitList(SyntaxTokenList list)
    {
        var count = list.Count;
        if (count != 0)
        {
            SyntaxTokenListBuilder? alternate = null;
            var index = -1;

            foreach (var token in list)
            {
                index++;
                var visited = this.VisitToken(token);

                if (visited != token && alternate is null)
                {
                    alternate = new(count);
                    alternate.Add(list, 0, index);
                }

                if (alternate is not null && !visited.IsKind(SyntaxKind.None))
                {
                    alternate.Add(visited);
                }
            }

            if (alternate is not null) return alternate.ToList();
        }

        return list;
    }

    public virtual SyntaxTriviaList VisitList(SyntaxTriviaList list)
    {
        var count = list.Count;
        if (count != 0)
        {
            SyntaxTriviaListBuilder? alternate = null;
            var index = -1;

            foreach (var trivia in list)
            {
                index++;
                var visited = this.VisitListElement(trivia);

                if (visited != trivia && alternate is null)
                {
                    alternate = new(count);
                    alternate.Add(list, 0, index);
                }

                if (alternate is not null && !visited.IsKind(SyntaxKind.None))
                {
                    alternate.Add(visited);
                }
            }

            if (alternate is not null) return alternate.ToList();
        }

        return list;
    }

    public virtual SyntaxTrivia VisitListElement(SyntaxTrivia trivia) => this.VisitTrivia(trivia);
}
