// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
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
        private DirectiveStack _newDirectives;
        private DirectiveStack _oldDirectives;
        private LexerMode _newLexerDrivenMode;

        public Reader(Blender blender)
        {
            _lexer = blender._lexer;
            _oldTreeCursor = blender._oldTreeCursor;
            _changes = blender._changes;
            _newPosition = blender._newPosition;
            _changeDelta = blender._changeDelta;
            _newDirectives = blender._newDirectives;
            _oldDirectives = blender._oldDirectives;
            _newLexerDrivenMode = blender._newLexerDrivenMode;
        }

        internal BlendedNode ReadNodeOrToken(LexerMode mode, bool asToken)
        {
            while (true)
            {
                if (_oldTreeCursor.IsFinished)
                    return ReadNewToken(mode);

                if (_changeDelta < 0)
                    SkipOldToken();
                else if (_changeDelta > 0)
                    return ReadNewToken(mode);
                else
                {
                    if (TryTakeOldNodeOrToken(asToken, out var blendedNode))
                        return blendedNode;

                    if (_oldTreeCursor.CurrentNodeOrToken.IsNode)
                        _oldTreeCursor = _oldTreeCursor.MoveToFirstChild();
                    else
                        SkipOldToken();
                }
            }
        }

        private void SkipOldToken()
        {
            Debug.Assert(!_oldTreeCursor.IsFinished);

            _oldTreeCursor = _oldTreeCursor.MoveToFirstToken();
            var node = _oldTreeCursor.CurrentNodeOrToken;

            _changeDelta += node.FullWidth;

            SkipPastChanges();
        }

        private void SkipPastChanges()
        {
            var oldPosition = _oldTreeCursor.CurrentNodeOrToken.Position;
            while (!_changes.IsEmpty && oldPosition >= _changes.Peek().Span.End)
            {
                var change = _changes.Peek();

                _changes = _changes.Pop();
                _changeDelta += change.NewLength - change.Span.Length;
            }
        }

        private BlendedNode ReadNewToken(LexerMode mode)
        {
            Debug.Assert(_changeDelta > 0 || _oldTreeCursor.IsFinished);

            var token = LexNewToken(mode);

            var width = token.FullWidth;
            _newPosition += width;
            _changeDelta -= width;

            SkipPastChanges();

            return CreateBlendedNode(node: null, token: token);
        }

        private SyntaxToken LexNewToken(LexerMode mode)
        {
            if (_lexer.TextWindow.Position != _newPosition)
                _lexer.Reset(_newPosition, _newDirectives);

            var token = _lexer.Lex(ref mode);
            _newDirectives = _lexer.Directives;
            _newLexerDrivenMode = mode;
            return token;
        }

        private bool TryTakeOldNodeOrToken(bool asToken, out BlendedNode blendedNode)
        {
            if (asToken)
                _oldTreeCursor = _oldTreeCursor.MoveToFirstToken();

            var currentNodeOrToken = _oldTreeCursor.CurrentNodeOrToken;
            if (!CanReuse(currentNodeOrToken))
            {
                blendedNode = default;
                return false;
            }

            _newPosition += currentNodeOrToken.FullWidth;
            _oldTreeCursor = _oldTreeCursor.MoveToNextSibling();

            _newDirectives = currentNodeOrToken.ApplyDirectives(_newDirectives);
            _oldDirectives = currentNodeOrToken.ApplyDirectives(_oldDirectives);

            blendedNode = CreateBlendedNode(node: (ThisSyntaxNode?)currentNodeOrToken.AsNode(), token: (SyntaxToken)currentNodeOrToken.AsToken().Node);
            return true;
        }

        private bool CanReuse(SyntaxNodeOrToken nodeOrToken)
        {
            if (nodeOrToken.FullWidth == 0) return false;

            if (nodeOrToken.ContainsAnnotations) return false;

            if (IntersectsNextChange(nodeOrToken)) return false;

            if (nodeOrToken.ContainsDiagnostics ||
                (nodeOrToken.IsToken && ((ThisInternalSyntaxNode)nodeOrToken.AsToken().Node).ContainsSkippedText && nodeOrToken.Parent.ContainsDiagnostics)
            ) return false;

            if (IsFabricatedToken(nodeOrToken.Kind(), _lexer.Options.LanguageVersion)) return false;

            if (
                (nodeOrToken.IsToken && nodeOrToken.AsToken().IsMissing) ||
                (nodeOrToken.IsNode && IsIncomplete((ThisSyntaxNode)nodeOrToken.AsNode()))
            ) return false;

            if (!nodeOrToken.ContainsDirectives) return true;

            return _newDirectives.IncrementallyEquivalent(_oldDirectives);
        }

        private bool IntersectsNextChange(SyntaxNodeOrToken nodeOrToken)
        {
            if (_changes.IsEmpty) return false;

            var oldSpan = nodeOrToken.FullSpan;
            var changeSpan = _changes.Peek().Span;

            return oldSpan.IntersectsWith(changeSpan);
        }

        /// <summary>
        /// 判断一个语法节点是否为未完成节点。
        /// </summary>
        private static bool IsIncomplete(ThisSyntaxNode node) =>
            // 若最后一个标记为缺失标记，则这个节点为未完成节点。
            // 使用绿树API比红树API更快。
            node.Green.GetLastTerminal()!.IsMissing;

        internal static bool IsFabricatedToken(SyntaxKind kind, LanguageVersion version) => kind switch
        {
            _ => SyntaxFacts.IsContextualKeyword(kind, version)
        };

        private BlendedNode CreateBlendedNode(ThisSyntaxNode? node, SyntaxToken token) => new(node, token,
            new(
                _lexer,
                _oldTreeCursor,
                _changes,
                _newPosition,
                _changeDelta,
                _newDirectives,
                _oldDirectives,
                _newLexerDrivenMode
            ));
    }
}
