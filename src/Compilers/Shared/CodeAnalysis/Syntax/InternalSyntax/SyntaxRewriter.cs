// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.MoonScriptSyntaxNode;
#endif

internal partial class
#if LANG_LUA
    LuaSyntaxRewriter
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxRewriter
#endif
{
    protected readonly bool VisitIntoStructuredTrivia;

    public
#if LANG_LUA
    LuaSyntaxRewriter
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxRewriter
#endif
    (bool visitIntoStructuredTrivia = false) => this.VisitIntoStructuredTrivia = visitIntoStructuredTrivia;

    public override ThisInternalSyntaxNode VisitToken(SyntaxToken token)
    {
        var leading = this.VisitList(token.LeadingTrivia);
        var trailing = this.VisitList(token.TrailingTrivia);

        if (leading != token.LeadingTrivia)
            token = token.TokenWithLeadingTrivia(leading.Node);

        if (trailing != token.TrailingTrivia)
            token = token.TokenWithTrailingTrivia(trailing.Node);

        return token;
    }

    public override ThisInternalSyntaxNode VisitTrivia(SyntaxTrivia trivia) => trivia;

    public SyntaxList<TNode> VisitList<TNode>(SyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode
    {
        SyntaxListBuilder? alternate = null;
        for (int i = 0, n = list.Count; i < n; i++)
        {
            var item = list[i];
            var visited = this.Visit(item);
            if (item != visited && alternate is null)
            {
                alternate = new(n);
                alternate.AddRange(list, 0, i);
            }
            
            if (alternate is not null)
            {
                Debug.Assert(visited is not null && visited.Kind != SyntaxKind.None, "无法移除节点。");
                alternate.Add(visited);
            }
        }

        if (alternate is not null)
            return alternate.ToList();

        return list;
    }

    public SeparatedSyntaxList<TNode> VisitList<TNode>(SeparatedSyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode
    {
        var withSeps = (SyntaxList<ThisInternalSyntaxNode>)list.GetWithSeparators();
        var result = this.VisitList(withSeps);
        if (result != withSeps)
            return result.AsSeparatedList<TNode>();

        return list;
    }

    /// <remarks>在此类及子类中不应调用此方法。</remarks>
    /// <exception cref="ExceptionUtilities.Unreachable">当在此类及子类中调用此方法时。</exception>
    /// <inheritdoc/>
    protected sealed override ThisInternalSyntaxNode? DefaultVisit(ThisInternalSyntaxNode node) => throw ExceptionUtilities.Unreachable;
}
