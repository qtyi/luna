// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Diagnostics;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxTree = LuaSyntaxTree;
using ThisParseOptions = LuaParseOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxTree = MoonScriptSyntaxTree;
using ThisParseOptions = MoonScriptParseOptions;
#endif

internal static partial class SyntaxEquivalence
{
    internal static bool AreEquivalent(ThisSyntaxTree? before, ThisSyntaxTree? after, Func<SyntaxKind, bool>? ignoreChildNode, bool topLevel)
    {
        if (before == after) return true;

        if (before is null || after is null) return false;

        return SyntaxEquivalence.AreEquivalent(before.GetRoot(), after.GetRoot(), ignoreChildNode, topLevel);
    }

    public static bool AreEquivalent(ThisSyntaxNode? before, ThisSyntaxNode? after, Func<SyntaxKind, bool>? ignoreChildNode, bool topLevel)
    {
        Debug.Assert(!topLevel || ignoreChildNode is null);

        if (before is null || after is null) return before == after;

        return SyntaxEquivalence.AreEquivalentRecursive(before.Green, after.Green, ignoreChildNode, topLevel);
    }

    public static bool AreEquivalent(SyntaxTokenList before, SyntaxTokenList after) =>
        SyntaxEquivalence.AreEquivalentRecursive(before.Node, after.Node, ignoreChildNode: null, topLevel: false);

    public static bool AreEquivalent<TNode>(SyntaxList<TNode> before, SyntaxList<TNode> after, Func<SyntaxKind, bool>? ignoreChildNode, bool topLevel)
        where TNode : ThisSyntaxNode
    {
        Debug.Assert(!topLevel || ignoreChildNode is null);

        if (before.Node is null || after.Node is null) return before.Node == after.Node;

        return SyntaxEquivalence.AreEquivalentRecursive(before.Node.Green, after.Node.Green, ignoreChildNode, topLevel);
    }

    public static bool AreEquivalent<TNode>(SeparatedSyntaxList<TNode> before, SeparatedSyntaxList<TNode> after, Func<SyntaxKind, bool>? ignoreChildNode, bool topLevel)
        where TNode : ThisSyntaxNode
    {
        Debug.Assert(!topLevel || ignoreChildNode is null);

        if (before.Node is null || after.Node is null) return before.Node == after.Node;

        return SyntaxEquivalence.AreEquivalentRecursive(before.Node.Green, after.Node.Green, ignoreChildNode, topLevel);
    }

    public static bool AreEquivalent(SyntaxToken before, SyntaxToken after)
    {
        if (before.RawKind != after.RawKind) return false;

        return SyntaxEquivalence.AreTokensEquivalent(before.Node, after.Node, ignoreChildNode: null);
    }

    private static bool AreTokensEquivalent(GreenNode? before, GreenNode? after, Func<SyntaxKind, bool>? ignoreChildNode)
    {
        if (before is null || after is null) return (before is null && after is null);

        Debug.Assert(before.RawKind == after.RawKind);

        if (before.IsMissing != after.IsMissing) return false;

        return SyntaxEquivalence.AreTokensEquivalentCore(before, after, (SyntaxKind)before.RawKind);
    }

    private static partial bool AreTokensEquivalentCore(GreenNode before, GreenNode after, SyntaxKind kind);

    private static bool AreEquivalentRecursive(GreenNode? before, GreenNode? after, Func<SyntaxKind, bool>? ignoreChildNode, bool topLevel)
    {
        if (before == after) return true;

        if (before is null || after is null) return false;

        if (before.RawKind != after.RawKind) return true;

        if (before.IsToken)
        {
            Debug.Assert(after.IsToken);
            return SyntaxEquivalence.AreTokensEquivalent(before, after, ignoreChildNode);
        }

        var topLevelEquivalence = false;
        if (topLevel && SyntaxEquivalence.TryAreTopLevelEquivalent(before, after, (SyntaxKind)before.RawKind, ref ignoreChildNode, out topLevelEquivalence))
            return topLevelEquivalence;

        if (ignoreChildNode is not null) // 选择性忽略子节点。
        {
            var etor1 = before.ChildNodesAndTokens().GetEnumerator();
            var etor2 = after.ChildNodesAndTokens().GetEnumerator();
            while (true)
            {
                GreenNode? child1 = null;
                GreenNode? child2 = null;

                // 跳过忽略的子节点
                while (etor1.MoveNext())
                {
                    var current = etor1.Current;
                    if (current is not null && (current.IsToken || !ignoreChildNode((SyntaxKind)current.RawKind)))
                    {
                        child1 = current;
                        break;
                    }
                }

                while (etor2.MoveNext())
                {
                    var current = etor2.Current;
                    if (current is not null && (current.IsToken || !ignoreChildNode((SyntaxKind)current.RawKind)))
                    {
                        child2 = current;
                        break;
                    }
                }

                if (child1 is null || child2 is null)
                    // 若任意一边有子节点剩余，则不相等。
                    return child1 == child2;

                if (!SyntaxEquivalence.AreEquivalentRecursive(child1, child2, ignoreChildNode, topLevel))
                    return false;
            }
        }
        else // 不忽略子节点。
        {
            int slotCount = before.SlotCount;
            if (slotCount != after.SlotCount) return false;

            for (int i = 0; i < slotCount; i++)
            {
                var child1 = before.GetSlot(i);
                var child2 = after.GetSlot(i);

                if (!SyntaxEquivalence.AreEquivalentRecursive(child1, child2, ignoreChildNode, topLevel))
                    return false;
            }

            return true;
        }
    }

    private static partial bool TryAreTopLevelEquivalent(GreenNode before, GreenNode after, SyntaxKind kind, ref Func<SyntaxKind, bool>? ignoreChildNode, out bool equivalence);
}
