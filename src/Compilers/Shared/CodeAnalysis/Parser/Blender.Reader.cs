// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using Qtyi.CodeAnalysis.Lua;
using ThisSyntaxNode = Qtyi.CodeAnalysis.Lua.LuaSyntaxNode;
using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using Qtyi.CodeAnalysis.MoonScript;
using ThisSyntaxNode = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxNode;
using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.MoonScriptSyntaxNode;
#endif

internal partial struct Blender
{
    internal struct Reader
    {
        private readonly Lexer _lexer;
        private Cursor _oldTreeCursor;
        private ImmutableStack<TextChangeRange> _changes;
        private int _newPosition;
        private int _changeDelta;
        private LexerMode _newLexerDrivenMode;

        public Reader(Blender blender)
        {
            this._lexer = blender._lexer;
            this._oldTreeCursor = blender._oldTreeCursor;
            this._changes = blender._changes;
            this._newPosition = blender._newPosition;
            this._changeDelta = blender._changeDelta;
            this._newLexerDrivenMode = blender._newLexerDrivenMode;
        }

        internal BlendedNode ReadNodeOrToken(LexerMode mode, bool asToken)
        {
            while (true)
            {
                if (this._oldTreeCursor.IsFinished)
                    return this.ReadNewToken(mode);

                if (this._changeDelta < 0)
                    this.SkipOldToken();
                else if (this._changeDelta > 0)
                    return this.ReadNewToken(mode);
                else
                {
                    if (this.TryTakeOldNodeOrToken(asToken, out var blendedNode))
                        return blendedNode;

                    if (this._oldTreeCursor.CurrentNodeOrToken.IsNode)
                        this._oldTreeCursor = this._oldTreeCursor.MoveToFirstChild();
                    else
                        this.SkipOldToken();
                }
            }
        }

        private void SkipOldToken()
        {
            Debug.Assert(!this._oldTreeCursor.IsFinished);

            this._oldTreeCursor = this._oldTreeCursor.MoveToFirstToken();
            var node = this._oldTreeCursor.CurrentNodeOrToken;

            this._changeDelta += node.FullWidth;

            this.SkipPastChanges();
        }

        private void SkipPastChanges()
        {
            var oldPosition = this._oldTreeCursor.CurrentNodeOrToken.Position;
            while(!this._changes.IsEmpty && oldPosition >= this._changes.Peek().Span.End)
            {
                var change = this._changes.Peek();

                this._changes = this._changes.Pop();
                this._changeDelta += change.NewLength - change.Span.Length;
            }
        }

        private BlendedNode ReadNewToken(LexerMode mode)
        {
            Debug.Assert(this._changeDelta > 0 || this._oldTreeCursor.IsFinished);

            var token = this.LexNewToken(mode);

            var width = token.FullWidth;
            this._newPosition += width;
            this._changeDelta -= width;

            this.SkipPastChanges();

            return this.CreateBlendedNode(node: null, token: token);
        }

        private SyntaxToken LexNewToken(LexerMode mode)
        {
            if (this._lexer.TextWindow.Position != this._newPosition)
                this._lexer.Reset(this._newPosition);

            var token = this._lexer.Lex(ref mode);
            this._newLexerDrivenMode = mode;
            return token;
        }

        private bool TryTakeOldNodeOrToken(bool asToken, out BlendedNode blendedNode)
        {
            if (asToken)
                this._oldTreeCursor = this._oldTreeCursor.MoveToFirstToken();

            var currentNodeOrToken = this._oldTreeCursor.CurrentNodeOrToken;
            if (!this.CanReuse(currentNodeOrToken))
            {
                blendedNode = default;
                return false;
            }

            this._newPosition += currentNodeOrToken.FullWidth;
            this._oldTreeCursor = this._oldTreeCursor.MoveToNextSibling();

            blendedNode = this.CreateBlendedNode(node: (ThisSyntaxNode?)currentNodeOrToken.AsNode(), token: (SyntaxToken)currentNodeOrToken.AsToken().Node);
            return true;
        }

        private bool CanReuse(SyntaxNodeOrToken nodeOrToken)
        {
            if (nodeOrToken.FullWidth == 0) return false;

            if (nodeOrToken.ContainsAnnotations) return false;

            if (this.IntersectsNextChange(nodeOrToken)) return false;

            if (nodeOrToken.ContainsDiagnostics ||
                (nodeOrToken.IsToken && ((ThisInternalSyntaxNode)nodeOrToken.AsToken().Node).ContainsSkippedText && nodeOrToken.Parent.ContainsDiagnostics)
            ) return false;

            if (Reader.IsFabricatedToken(nodeOrToken.Kind())) return false;

            if (
                (nodeOrToken.IsToken && nodeOrToken.AsToken().IsMissing) ||
                (nodeOrToken.IsNode && Reader.IsIncomplete((ThisSyntaxNode)nodeOrToken.AsNode()))
            ) return false;

            return true;
        }

        private bool IntersectsNextChange(SyntaxNodeOrToken nodeOrToken)
        {
            if (this._changes.IsEmpty) return false;

            var oldSpan = nodeOrToken.FullSpan;
            var changeSpan = this._changes.Peek().Span;

            return oldSpan.IntersectsWith(changeSpan);
        }

        /// <summary>
        /// 判断一个语法节点是否为未完成节点。
        /// </summary>
        private static bool IsIncomplete(ThisSyntaxNode node) =>
            // 若最后一个标记为缺失标记，则这个节点为未完成节点。
            // 使用绿树API比红树API更快。
            node.Green.GetLastTerminal()!.IsMissing;

        internal static bool IsFabricatedToken(SyntaxKind kind) => kind switch
        {
            _ => SyntaxFacts.IsContextualKeyword(kind)
        };

        private BlendedNode CreateBlendedNode(ThisSyntaxNode? node, SyntaxToken token) => new(node, token,
            new(
                this._lexer,
                this._oldTreeCursor,
                this._changes,
                this._newPosition,
                this._changeDelta,
                this._newLexerDrivenMode
            ));
    }
}
