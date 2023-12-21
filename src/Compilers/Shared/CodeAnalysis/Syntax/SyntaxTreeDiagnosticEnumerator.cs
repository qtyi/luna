// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
using ThisDiagnostic = Qtyi.CodeAnalysis.Lua.LuaDiagnostic;

namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
using ThisDiagnostic = Qtyi.CodeAnalysis.MoonScript.MoonScriptDiagnostic;

namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// 语法树的消息的枚举器。
/// </summary>
internal struct SyntaxTreeDiagnosticEnumerator
{
    private readonly SyntaxTree? _syntaxTree;
    private NodeIterationStack _stack;
    private ThisDiagnostic? _current;
    private int _position;
    private const int DefaultStackCapacity = 8;

    internal SyntaxTreeDiagnosticEnumerator(SyntaxTree syntaxTree, GreenNode? node, int position)
    {
        this._current = null;
        this._position = position;
        if (node is not null && node.ContainsDiagnostics)
        {
            this._syntaxTree = syntaxTree;
            this._stack = new(DefaultStackCapacity);
            this._stack.PushNodeOrToken(node);
        }
        else
        {
            this._syntaxTree = null;
            this._stack = new();
        }
    }

    public ThisDiagnostic Current
    {
        get
        {
            Debug.Assert(_current is not null);
            return this._current;
        }
    }

    public bool MoveNext()
    {
        while (this._stack.Any())
        {
            var diagnosticIndex = this._stack.Top.DiagnosticIndex;
            var node = this._stack.Top.Node;
            var diagnostics = node.GetDiagnostics();
            if (diagnosticIndex < diagnostics.Length - 1)
            {
                var sdi = (SyntaxDiagnosticInfo)diagnostics[++diagnosticIndex];

                // node既可能是标识符也可能是节点。
                // 当node是标识符时，起始语法琐碎内容已经在栈中计算过，因此需要回滚。
                // 当node是节点时，正好在计算起始语法琐碎内容。
                var leadingWidtchAlreadyCounted = node.IsToken ? node.GetLeadingTriviaWidth() : 0;

                // 避免产生超出树范围的位置信息。
                Debug.Assert(this._syntaxTree is not null);
                var length = this._syntaxTree.GetRoot().FullSpan.Length;
                var spanStart = Math.Min(this._position - leadingWidtchAlreadyCounted + sdi.Offset, length);
                var spanWidth = Math.Min(spanStart + sdi.Width, length) - spanStart;

                this._current = new(sdi, new SourceLocation(this._syntaxTree, new(spanStart, spanWidth)));

                this._stack.UpdateDiagnosticIndexForStackTop(diagnosticIndex);
                return true;
            }

            var slotIndex = this._stack.Top.SlotIndex;
tryAgain:
            if (slotIndex < node.SlotCount - 1)
            {
                slotIndex++;
                var child = node.GetSlot(slotIndex);
                if (child == null) goto tryAgain;

                if (!child.ContainsDiagnostics)
                {
                    this._position += child.FullWidth;
                    goto tryAgain;
                }

                this._stack.UpdateSlotIndexForStackTop(slotIndex);
                this._stack.PushNodeOrToken(child);
            }
            else
            {
                if (node.SlotCount == 0)
                    this._position += node.Width;

                this._stack.Pop();
            }
        }

        return false;
    }

    private struct NodeIteration
    {
        internal readonly GreenNode Node;
        internal int DiagnosticIndex;
        internal int SlotIndex;

        internal NodeIteration(GreenNode node)
        {
            this.Node = node;
            this.SlotIndex = -1;
            this.DiagnosticIndex = -1;
        }
    }

    private struct NodeIterationStack
    {
        private NodeIteration[] _stack;
        private int _count;

        internal NodeIterationStack(int capacity)
        {
            Debug.Assert(capacity > 0);
            this._stack = new NodeIteration[capacity];
            this._count = 0;
        }

        internal NodeIteration Top
        {
            get
            {
                Debug.Assert(this._stack is not null);
                Debug.Assert(this._count > 0);
                return this._stack[this._count - 1];
            }
        }

        internal NodeIteration this[int index]
        {
            get
            {
                Debug.Assert(this._stack is not null);
                Debug.Assert(index >= 0 && index < this._count);
                return this._stack[index];
            }
        }

        /// <summary>
        /// 向栈中压入一个绿树节点或语法标识符。
        /// </summary>
        /// <param name="node">绿树节点或语法标识符。</param>
        internal void PushNodeOrToken(GreenNode node)
        {
            if (node is Syntax.InternalSyntax.SyntaxToken token)
                this.PushToken(token);
            else
                this.Push(node);
        }

        /// <summary>
        /// 向栈中压入一个语法标识符。
        /// </summary>
        /// <param name="token">语言特化的语法标识符。</param>
        private void PushToken(Syntax.InternalSyntax.SyntaxToken token)
        {
            var trailing = token.GetTrailingTrivia();
            if (trailing is not null) this.Push(trailing); // 压入结束的语法琐碎内容。

            this.Push(token); // 压入语法标记。
            var leading = token.GetLeadingTrivia();
            if (leading is not null) this.Push(leading); // 压入起始的语法琐碎内容。
        }

        /// <summary>
        /// 向栈中压入一个绿树节点。
        /// </summary>
        /// <param name="node">要压入的绿树节点。</param>
        private void Push(GreenNode node)
        {
            Debug.Assert(this._stack is not null);
            if (this._count >= this._stack.Length) // 需要扩容。
            {
                var temp = new NodeIteration[this._stack.Length + Math.Min(this._stack.Length, 1024)];
                Array.Copy(this._stack, temp, this._stack.Length);
                this._stack = temp;
            }

            this._stack[this._count] = new(node);
            this._count++;
        }

        /// <summary>
        /// 弹出一个绿树节点。
        /// </summary>
        internal void Pop()
        {
            Debug.Assert(this._count > 0);
            this._count--;
        }

        /// <summary>
        /// 此节点迭代栈中是否含有元素。
        /// </summary>
        /// <returns>若为<see langword="true"/>时表示包含元素，否则为<see langword="false"/>。</returns>
        internal bool Any() => this._count > 0;

        /// <summary>
        /// 更新栈顶元素的<see cref="NodeIteration.SlotIndex"/>。
        /// </summary>
        /// <param name="slotIndex">新的插入索引。</param>
        internal void UpdateSlotIndexForStackTop(int slotIndex)
        {
            Debug.Assert(this._stack is not null);
            Debug.Assert(this._count > 0);
            this._stack[this._count - 1].SlotIndex = slotIndex;
        }

        /// <summary>
        /// 更新栈顶元素的<see cref="NodeIteration.DiagnosticIndex"/>。
        /// </summary>
        /// <param name="diagnosticIndex">新的消息索引。</param>
        internal void UpdateDiagnosticIndexForStackTop(int diagnosticIndex)
        {
            Debug.Assert(this._stack is not null);
            Debug.Assert(this._count > 0);
            this._stack[this._count - 1].DiagnosticIndex = diagnosticIndex;
        }
    }
}
