// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax;
#endif

static partial class SyntaxReplacer
{
    public static SyntaxNode ReplaceNodeInList(SyntaxNode root, SyntaxNode nodeInList, IEnumerable<SyntaxNode> newNodes) =>
        new NodeListEditor(
            nodeInList,
            newNodes,
            ListEditKind.Replace)
        .Visit(root);

    public static SyntaxNode InsertNodeInList(SyntaxNode root, SyntaxNode nodeInList, IEnumerable<SyntaxNode> nodesToInsert, bool insertBefore) =>
        new NodeListEditor(
            nodeInList,
            nodesToInsert,
            insertBefore ? ListEditKind.InsertBefore : ListEditKind.InsertAfter)
        .Visit(root);

    public static SyntaxNode ReplaceTokenInList(SyntaxNode root, SyntaxToken tokenInList, IEnumerable<SyntaxToken> newTokens) =>
        new TokenListEditor(
            tokenInList,
            newTokens,
            ListEditKind.Replace)
        .Visit(root);

    public static SyntaxNode InsertTokenInList(SyntaxNode root, SyntaxToken tokenInList, IEnumerable<SyntaxToken> tokensToInsert, bool insertBefore) =>
        new TokenListEditor(
            tokenInList,
            tokensToInsert,
            insertBefore ? ListEditKind.InsertBefore : ListEditKind.InsertAfter)
        .Visit(root);

    public static SyntaxNode ReplaceTriviaInList(SyntaxNode root, SyntaxTrivia triviaInList, IEnumerable<SyntaxTrivia> newTrivia) =>
        new TriviaListEditor(
            triviaInList,
            newTrivia,
            ListEditKind.Replace)
        .Visit(root);

    public static SyntaxNode InsertTriviaInList(SyntaxNode root, SyntaxTrivia triviaInList, IEnumerable<SyntaxTrivia> triviaToInsert, bool insertBefore) =>
        new TriviaListEditor(
            triviaInList,
            triviaToInsert,
            insertBefore ? ListEditKind.InsertBefore : ListEditKind.InsertAfter)
        .Visit(root);

    #region 列表编辑器
    private enum ListEditKind
    {
        InsertBefore,
        InsertAfter,
        Replace
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static InvalidOperationException GetItemNotListElementException() =>
        new(LunaResources.MissingListItem);

    private abstract class BaseListEditor : ThisSyntaxRewriter
    {
        private readonly TextSpan _elementSpan;
        private readonly bool _visitTrivia;
        private readonly bool _visitInfoStructuredTrivia;

        protected readonly ListEditKind editKind;

        public BaseListEditor(
            TextSpan elementSpan,
            ListEditKind editKind,
            bool visitTrivia,
            bool visitInfoStructuredTrivia)
        {
            _elementSpan = elementSpan;
            this.editKind = editKind;
            _visitTrivia = visitTrivia || visitInfoStructuredTrivia;
            _visitInfoStructuredTrivia = visitInfoStructuredTrivia;
        }

        public override bool VisitIntoStructuredTrivia => _visitInfoStructuredTrivia;

        private bool ShouldVisit(TextSpan span) => span.IntersectsWith(_elementSpan);

        [return: NotNullIfNotNull(nameof(node))]
        public override SyntaxNode? Visit(SyntaxNode? node)
        {
            if (node is null) return null;

            SyntaxNode? rewritten;

            if (ShouldVisit(node.FullSpan))
                rewritten = base.Visit(node);
            else
                rewritten = node;

            return rewritten;
        }

        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            SyntaxToken rewritten;

            if (_visitTrivia && ShouldVisit(token.FullSpan))
                rewritten = base.VisitToken(token);
            else
                rewritten = token;

            return rewritten;
        }

        public override SyntaxTrivia VisitListElement(SyntaxTrivia trivia)
        {
            SyntaxTrivia rewritten;

            if (VisitIntoStructuredTrivia && trivia.HasStructure && ShouldVisit(trivia.FullSpan))
                rewritten = VisitTrivia(trivia);
            else
                rewritten = trivia;

            return rewritten;
        }
    }

    private class NodeListEditor : BaseListEditor
    {
        private readonly SyntaxNode _originalNode;
        private readonly IEnumerable<SyntaxNode> _newNodes;

        public NodeListEditor(
            SyntaxNode originalNode,
            IEnumerable<SyntaxNode> newNodes,
            ListEditKind editKind) : base(
                elementSpan: originalNode.Span,
                editKind: editKind,
                visitTrivia: false,
                visitInfoStructuredTrivia: originalNode.IsPartOfStructuredTrivia())
        {
            _originalNode = originalNode;
            _newNodes = newNodes;
        }

        [return: NotNullIfNotNull(nameof(node))]
        public override SyntaxNode? Visit(SyntaxNode? node)
        {
            if (node == _originalNode)
                throw GetItemNotListElementException();

            return base.Visit(node);
        }

        public override SeparatedSyntaxList<TNode> VisitList<TNode>(SeparatedSyntaxList<TNode> list)
        {
            if (_originalNode is TNode originalNode)
            {
                var index = list.IndexOf(originalNode);
                if (index >= 0 && index < list.Count)
                {
                    var newNodes = _newNodes.Cast<TNode>();
                    switch (editKind)
                    {
                        case ListEditKind.Replace:
                            return list.ReplaceRange(originalNode, newNodes);

                        case ListEditKind.InsertBefore:
                            return list.InsertRange(index, newNodes);

                        case ListEditKind.InsertAfter:
                            return list.InsertRange(index + 1, newNodes);
                    }
                }
            }

            return base.VisitList(list);
        }

        public override SyntaxList<TNode> VisitList<TNode>(SyntaxList<TNode> list)
        {
            if (_originalNode is TNode originalNode)
            {
                var index = list.IndexOf(originalNode);
                if (index >= 0 && index < list.Count)
                {
                    var newNodes = _newNodes.Cast<TNode>();
                    switch (editKind)
                    {
                        case ListEditKind.Replace:
                            return list.ReplaceRange(originalNode, newNodes);

                        case ListEditKind.InsertBefore:
                            return list.InsertRange(index, newNodes);

                        case ListEditKind.InsertAfter:
                            return list.InsertRange(index + 1, newNodes);
                    }
                }
            }

            return base.VisitList(list);
        }
    }

    private class TokenListEditor : BaseListEditor
    {
        private readonly SyntaxToken _originalToken;
        private readonly IEnumerable<SyntaxToken> _newTokens;

        public TokenListEditor(
            SyntaxToken originalToken,
            IEnumerable<SyntaxToken> newTokens,
            ListEditKind editKind) : base(
                elementSpan: originalToken.Span,
                editKind: editKind,
                visitTrivia: false,
                visitInfoStructuredTrivia: originalToken.IsPartOfStructuredTrivia())
        {
            _originalToken = originalToken;
            _newTokens = newTokens;
        }

        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            if (token == _originalToken)
                throw GetItemNotListElementException();

            return base.VisitToken(token);
        }

        public override SyntaxTokenList VisitList(SyntaxTokenList list)
        {
            var index = list.IndexOf(_originalToken);
            if (index >= 0 && index < list.Count)
            {
                switch (editKind)
                {
                    case ListEditKind.Replace:
                        return list.ReplaceRange(_originalToken, _newTokens);

                    case ListEditKind.InsertBefore:
                        return list.InsertRange(index, _newTokens);

                    case ListEditKind.InsertAfter:
                        return list.InsertRange(index + 1, _newTokens);
                }
            }

            return base.VisitList(list);
        }
    }

    private class TriviaListEditor : BaseListEditor
    {
        private readonly SyntaxTrivia _originalTrivia;
        private readonly IEnumerable<SyntaxTrivia> _newTrivia;

        public TriviaListEditor(
            SyntaxTrivia originalTrivia,
            IEnumerable<SyntaxTrivia> newTrivia,
            ListEditKind editKind) : base(
                elementSpan: originalTrivia.Span,
                editKind: editKind,
                visitTrivia: true,
                visitInfoStructuredTrivia: originalTrivia.IsPartOfStructuredTrivia())
        {
            _originalTrivia = originalTrivia;
            _newTrivia = newTrivia;
        }

        public override SyntaxTriviaList VisitList(SyntaxTriviaList list)
        {
            var index = list.IndexOf(_originalTrivia);
            if (index >= 0 && index < list.Count)
            {
                switch (editKind)
                {
                    case ListEditKind.Replace:
                        return list.ReplaceRange(_originalTrivia, _newTrivia);

                    case ListEditKind.InsertBefore:
                        return list.InsertRange(index, _newTrivia);

                    case ListEditKind.InsertAfter:
                        return list.InsertRange(index + 1, _newTrivia);
                }
            }

            return base.VisitList(list);
        }
    }
    #endregion
}
