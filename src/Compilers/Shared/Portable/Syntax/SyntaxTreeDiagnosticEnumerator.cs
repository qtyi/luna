// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

using SyntaxToken = Syntax.InternalSyntax.SyntaxToken;

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
        _current = null;
        _position = position;
        if (node is not null && node.ContainsDiagnostics)
        {
            _syntaxTree = syntaxTree;
            _stack = new(DefaultStackCapacity);
            _stack.PushNodeOrToken(node);
        }
        else
        {
            _syntaxTree = null;
            _stack = new();
        }
    }

    public ThisDiagnostic Current
    {
        get
        {
            Debug.Assert(_current is not null);
            return _current;
        }
    }

    public bool MoveNext()
    {
        while (_stack.Any())
        {
            var diagnosticIndex = _stack.Top.DiagnosticIndex;
            var node = _stack.Top.Node;
            var diagnostics = node.GetDiagnostics();
            if (diagnosticIndex < diagnostics.Length - 1)
            {
                var sdi = (SyntaxDiagnosticInfo)diagnostics[++diagnosticIndex];

                // node既可能是标识符也可能是节点。
                // 当node是标识符时，起始语法琐碎内容已经在栈中计算过，因此需要回滚。
                // 当node是节点时，正好在计算起始语法琐碎内容。
                var leadingWidtchAlreadyCounted = node.IsToken ? node.GetLeadingTriviaWidth() : 0;

                // 避免产生超出树范围的位置信息。
                Debug.Assert(_syntaxTree is not null);
                var length = _syntaxTree.GetRoot().FullSpan.Length;
                var spanStart = Math.Min(_position - leadingWidtchAlreadyCounted + sdi.Offset, length);
                var spanWidth = Math.Min(spanStart + sdi.Width, length) - spanStart;

                _current = new(sdi, new SourceLocation(_syntaxTree, new(spanStart, spanWidth)));

                _stack.UpdateDiagnosticIndexForStackTop(diagnosticIndex);
                return true;
            }

            var slotIndex = _stack.Top.SlotIndex;
tryAgain:
            if (slotIndex < node.SlotCount - 1)
            {
                slotIndex++;
                var child = node.GetSlot(slotIndex);
                if (child == null) goto tryAgain;

                if (!child.ContainsDiagnostics)
                {
                    _position += child.FullWidth;
                    goto tryAgain;
                }

                _stack.UpdateSlotIndexForStackTop(slotIndex);
                _stack.PushNodeOrToken(child);
            }
            else
            {
                if (node.SlotCount == 0)
                    _position += node.Width;

                _stack.Pop();
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
            Node = node;
            SlotIndex = -1;
            DiagnosticIndex = -1;
        }
    }

    private struct NodeIterationStack
    {
        private NodeIteration[] _stack;
        private int _count;

        internal NodeIterationStack(int capacity)
        {
            Debug.Assert(capacity > 0);
            _stack = new NodeIteration[capacity];
            _count = 0;
        }

        internal NodeIteration Top
        {
            get
            {
                Debug.Assert(_stack is not null);
                Debug.Assert(_count > 0);
                return _stack[_count - 1];
            }
        }

        internal NodeIteration this[int index]
        {
            get
            {
                Debug.Assert(_stack is not null);
                Debug.Assert(index >= 0 && index < _count);
                return _stack[index];
            }
        }

        /// <summary>
        /// 向栈中压入一个绿树节点或语法标识符。
        /// </summary>
        /// <param name="node">绿树节点或语法标识符。</param>
        internal void PushNodeOrToken(GreenNode node)
        {
            if (node is SyntaxToken token)
                PushToken(token);
            else
                Push(node);
        }

        /// <summary>
        /// 向栈中压入一个语法标识符。
        /// </summary>
        /// <param name="token">语言特化的语法标识符。</param>
        private void PushToken(SyntaxToken token)
        {
            var trailing = token.GetTrailingTrivia();
            if (trailing is not null) Push(trailing); // 压入结束的语法琐碎内容。

            Push(token); // 压入语法标记。
            var leading = token.GetLeadingTrivia();
            if (leading is not null) Push(leading); // 压入起始的语法琐碎内容。
        }

        /// <summary>
        /// 向栈中压入一个绿树节点。
        /// </summary>
        /// <param name="node">要压入的绿树节点。</param>
        private void Push(GreenNode node)
        {
            Debug.Assert(_stack is not null);
            if (_count >= _stack.Length) // 需要扩容。
            {
                var temp = new NodeIteration[_stack.Length + Math.Min(_stack.Length, 1024)];
                Array.Copy(_stack, temp, _stack.Length);
                _stack = temp;
            }

            _stack[_count] = new(node);
            _count++;
        }

        /// <summary>
        /// 弹出一个绿树节点。
        /// </summary>
        internal void Pop()
        {
            Debug.Assert(_count > 0);
            _count--;
        }

        /// <summary>
        /// 此节点迭代栈中是否含有元素。
        /// </summary>
        /// <returns>若为<see langword="true"/>时表示包含元素，否则为<see langword="false"/>。</returns>
        internal bool Any() => _count > 0;

        /// <summary>
        /// 更新栈顶元素的<see cref="NodeIteration.SlotIndex"/>。
        /// </summary>
        /// <param name="slotIndex">新的插入索引。</param>
        internal void UpdateSlotIndexForStackTop(int slotIndex)
        {
            Debug.Assert(_stack is not null);
            Debug.Assert(_count > 0);
            _stack[_count - 1].SlotIndex = slotIndex;
        }

        /// <summary>
        /// 更新栈顶元素的<see cref="NodeIteration.DiagnosticIndex"/>。
        /// </summary>
        /// <param name="diagnosticIndex">新的消息索引。</param>
        internal void UpdateDiagnosticIndexForStackTop(int diagnosticIndex)
        {
            Debug.Assert(_stack is not null);
            Debug.Assert(_count > 0);
            _stack[_count - 1].DiagnosticIndex = diagnosticIndex;
        }
    }
}
