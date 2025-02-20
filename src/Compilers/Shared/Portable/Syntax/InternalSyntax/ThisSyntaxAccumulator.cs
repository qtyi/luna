﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal abstract partial class
#if LANG_LUA
    LuaSyntaxAccumulator
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxAccumulator
#endif
    <TResult>
{
    protected readonly bool VisitIntoStructuredTrivia;

    public
#if LANG_LUA
    LuaSyntaxAccumulator
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxAccumulator
#endif
    (bool visitIntoStructuredTrivia = false) => VisitIntoStructuredTrivia = visitIntoStructuredTrivia;

    /// <summary>
    /// 处理这个节点并产生累加结果。
    /// </summary>
    /// <returns>产生的累加结果。</returns>
    /// <inheritdoc/>
    [return: NotNullIfNotNull(nameof(node))]
    public override IEnumerable<TResult>? Visit(ThisInternalSyntaxNode? node)
    {
        if (node is null) return null;

        // 是否进入表示结构语法琐碎内容的语法节点进行访问。
        if (node.IsStructuredTrivia && !VisitIntoStructuredTrivia) return SpecializedCollections.EmptyEnumerable<TResult>();

        var results = base.Visit(node);
        Debug.Assert(results is not null);
        return results;
    }

    /// <summary>
    /// 处理这个标记并产生累加结果。
    /// </summary>
    /// <returns>产生的累加结果。</returns>
    /// <inheritdoc/>
    public override IEnumerable<TResult> VisitToken(SyntaxToken token) => DefaultVisit(token);

    /// <summary>
    /// 处理这个琐碎内容并产生累加结果。
    /// </summary>
    /// <returns>产生的累加结果。</returns>
    /// <inheritdoc/>
    public override IEnumerable<TResult> VisitTrivia(SyntaxTrivia trivia) => DefaultVisit(trivia);

    public IEnumerable<TResult> VisitList<TNode>(SyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode
    {
        for (int i = 0, n = list.Count; i < n; i++)
        {
            var item = list[i];
            if (item is null) continue;

            var results = Visit(item);
            foreach (var result in results)
                yield return result;
        }
    }

    public IEnumerable<TResult> VisitList<TNode>(SeparatedSyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode
    {
        var withSeps = (SyntaxList<ThisInternalSyntaxNode>)list.GetWithSeparators();
        return VisitList(withSeps);
    }

    /// <summary>
    /// 内部方法，默认的累加节点的方法。
    /// </summary>
    /// <returns>返回空的枚举结果。</returns>
    /// <inheritdoc/>
    protected override IEnumerable<TResult> DefaultVisit(ThisInternalSyntaxNode node)
    {
        yield break;
    }
}
