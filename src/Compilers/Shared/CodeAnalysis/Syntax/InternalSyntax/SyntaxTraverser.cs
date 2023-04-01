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

internal abstract partial class
#if LANG_LUA
    LuaSyntaxTraverser
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxTraverser
# endif
{
    protected readonly bool VisitIntoStructuredTrivia;

    public
#if LANG_LUA
    LuaSyntaxTraverser
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxTraverser
#endif
    (bool visitIntoStructuredTrivia = false) => this.VisitIntoStructuredTrivia = visitIntoStructuredTrivia;

    public override void Visit(ThisInternalSyntaxNode? node)
    {
        if (node is null) return;

        // 是否进入表示结构语法琐碎内容的语法节点进行访问。
        if (node.IsStructuredTrivia && !this.VisitIntoStructuredTrivia) return;

        base.Visit(node);
    }

    public void VisitList<TNode>(SyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode
    {
        for (int i = 0, n = list.Count; i < n; i++)
        {
            var item = list[i];
            this.Visit(item);
        }
    }

    public void VisitList<TNode>(SeparatedSyntaxList<TNode> list) where TNode : ThisInternalSyntaxNode
    {
        var withSeps = (SyntaxList<ThisInternalSyntaxNode>)list.GetWithSeparators();
        this.VisitList(withSeps);
    }
}
