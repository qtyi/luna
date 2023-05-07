// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisSyntaxNode = Qtyi.CodeAnalysis.Lua.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisSyntaxNode = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxNode;
#endif

internal partial struct Blender
{
    /// <summary>
    /// 表示指向一个节点或标记的指针。
    /// </summary>
    private struct Cursor
    {
        /// <summary>当前节点或标记。</summary>
        public readonly SyntaxNodeOrToken CurrentNodeOrToken;
        /// <summary>当前节点或标记在父节点中的索引位置。</summary>
        private readonly int _indexInParent;

        /// <summary>
        /// 判断指针指向的节点是否是结束节点。
        /// </summary>
        public bool IsFinished => this.CurrentNodeOrToken.Kind() switch
        {
            SyntaxKind.None or SyntaxKind.EndOfFileToken => true,
            _ => false
        };

        /// <summary>
        /// 初始化指向指定节点的<see cref="Cursor"/>的新实例。
        /// </summary>
        /// <param name="node">要指向的节点。</param>
        /// <param name="indexInParent"><paramref name="node"/>在父节点中的索引位置。</param>
        private Cursor(SyntaxNodeOrToken node, int indexInParent)
        {
            this.CurrentNodeOrToken = node;
            this._indexInParent = indexInParent;
        }

        /// <summary>
        /// 获取指向指定根节点的指针。
        /// </summary>
        /// <param name="node">要指向的根节点。</param>
        /// <returns>指向<paramref name="node"/>的指针。</returns>
        public static Cursor FromRoot(ThisSyntaxNode node) => new(node, indexInParent: 0);

        /// <summary>
        /// 判断一个节点是否为零宽标记或文件结尾标记。
        /// </summary>
        /// <returns>若为<see langword="true"/>，则节点是零宽标记或文件结尾标记；若为<see langword="false"/>，则节点不是零宽标记或文件结尾标记。</returns>
        private static bool IsNonZeroWidthOrIsEndOfFile(SyntaxNodeOrToken token) => token.Kind() == SyntaxKind.EndOfFileToken || token.FullWidth != 0;

        /// <summary>
        /// 移动指针到下一个同属节点。
        /// </summary>
        /// <returns>指向下一个同属节点的指针。</returns>
        public Cursor MoveToNextSibling()
        {
            if (this.CurrentNodeOrToken.Parent is not null)
            {
                // 首先在同父级节点下依次向右查看，找到下一个符合条件的同级节点。
                var siblings = this.CurrentNodeOrToken.Parent.ChildNodesAndTokens();
                for (int i = this._indexInParent + 1, n = siblings.Count; i < n; i++)
                {
                    var sibling = siblings[i];
                    if (Cursor.IsNonZeroWidthOrIsEndOfFile(sibling))
                        return new(sibling, i);
                }

                // 同级节点均不符合要求，则移动到父节点，查找父节点右侧符合要求的节点。
                return this.MoveToParent().MoveToNextSibling();
            }

            // 找不到符合条件的节点。
            return default;
        }

        /// <summary>
        /// 移动指针到父级节点。
        /// </summary>
        private Cursor MoveToParent()
        {
            var parent = this.CurrentNodeOrToken.Parent;
            Debug.Assert(parent is not null);
            var index = Cursor.IndexOfNodeInParent(parent);
            return new(parent, index);
        }

        /// <summary>
        /// 获取指定节点在父节点中的索引位置。
        /// </summary>
        private static int IndexOfNodeInParent(SyntaxNode node)
        {
            if (node.Parent is null) return 0;

            var children = node.Parent.ChildNodesAndTokens();
            var index = SyntaxNodeOrToken.GetFirstChildIndexSpanningPosition(children, ((ThisSyntaxNode)node).Position);
            for (int i = index, n = children.Count; i < n; i++)
            {
                var child = children[i];
                if (child == node) return i;
            }

            throw ExceptionUtilities.Unreachable;
        }

        /// <summary>
        /// 移动到第一个子节点。
        /// </summary>
        /// <returns>指向第一个子节点的指针。</returns>
        public Cursor MoveToFirstChild()
        {
            Debug.Assert(this.CurrentNodeOrToken.IsNode);

            var node = this.CurrentNodeOrToken.AsNode();
            Debug.Assert(node is not null);
            if (node.SlotCount > 0)
            {
                // 首先快速检查当前节点的第一个子节点是否符合要求。
                var child = ChildSyntaxList.ItemInternal(node, 0);
                if (Cursor.IsNonZeroWidthOrIsEndOfFile(child))
                    return new(child, 0);

                // 遍历所有子节点，查找符合要求的节点。
                var children = node.ChildNodesAndTokens();
                for (int i = 0, n = children.Count; i < n; i++)
                {
                    child = children[i];
                    if (Cursor.IsNonZeroWidthOrIsEndOfFile(child))
                        return new(child, i);
                }
            }

            // 找不到符合条件的节点。
            return new();
        }

        /// <summary>
        /// 移动到第一个标记。
        /// </summary>
        /// <returns>指向第一个标记的指针。</returns>
        public Cursor MoveToFirstToken()
        {
            var cursor = this;
            if (!cursor.IsFinished)
            {
                for (
                    var node = cursor.CurrentNodeOrToken;
                    node.Kind() != SyntaxKind.None && !SyntaxFacts.IsAnyToken(node.Kind());
                    node = cursor.CurrentNodeOrToken)
                    cursor = cursor.MoveToFirstChild();
            }

            return cursor;
        }
    }
}
