// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxRewriter = LuaSyntaxRewriter;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxRewriter = MoonScriptSyntaxRewriter;
#endif

/// <summary>
/// 此类型提供替换语法节点、标志和琐碎内容的方法。
/// </summary>
internal static partial class SyntaxReplacer
{
    internal static ThisSyntaxNode Replace<TNode>(
        ThisSyntaxNode root,
        IEnumerable<TNode>? nodes = null,
        Func<TNode, TNode, ThisSyntaxNode>? computeReplacementNode = null,
        IEnumerable<SyntaxToken>? tokens = null,
        Func<SyntaxToken, SyntaxToken, SyntaxToken>? computeReplacementToken = null,
        IEnumerable<SyntaxTrivia>? trivia = null,
        Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? computeReplacementTrivia = null)
        where TNode : ThisSyntaxNode
    {
        var replacer = new Replacer<TNode>(
            nodes, computeReplacementNode,
            tokens, computeReplacementToken,
            trivia, computeReplacementTrivia);

        if (replacer.HasWork) return replacer.Visit(root);
        else return root;
    }

    internal static SyntaxToken Replace(
        SyntaxToken root,
        IEnumerable<ThisSyntaxNode>? nodes = null,
        Func<ThisSyntaxNode, ThisSyntaxNode, ThisSyntaxNode>? computeReplacementNode = null,
        IEnumerable<SyntaxToken>? tokens = null,
        Func<SyntaxToken, SyntaxToken, SyntaxToken>? computeReplacementToken = null,
        IEnumerable<SyntaxTrivia>? trivia = null,
        Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? computeReplacementTrivia = null)
    {
        var replacer = new Replacer<ThisSyntaxNode>(
            nodes, computeReplacementNode,
            tokens, computeReplacementToken,
            trivia, computeReplacementTrivia);

        if (replacer.HasWork) return replacer.VisitToken(root);
        else return root;
    }

    /// <summary>
    /// 进行替换工作的替换器。
    /// </summary>
    /// <typeparam name="TNode">替换器处理的语法节点的类型。</typeparam>
    private sealed class Replacer<TNode> : ThisSyntaxRewriter
        where TNode : ThisSyntaxNode
    {
        private readonly Func<TNode, TNode, ThisSyntaxNode>? _computeReplacementNode;
        private readonly Func<SyntaxToken, SyntaxToken, SyntaxToken>? _computeReplacementToken;
        private readonly Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? _computeReplacementTrivia;

        private readonly HashSet<ThisSyntaxNode> _nodeSet;
        private readonly HashSet<SyntaxToken> _tokenSet;
        private readonly HashSet<SyntaxTrivia> _triviaSet;
        private readonly HashSet<TextSpan> _spanSet;

        private static readonly HashSet<ThisSyntaxNode> s_noNodes = new();
        private static readonly HashSet<SyntaxToken> s_noTokens = new();
        private static readonly HashSet<SyntaxTrivia> s_noTrivia = new();

        private readonly TextSpan _totalSpan;
        private readonly bool _visitIntoStructuredTrivia;
        private readonly bool _shouldVisitTrivia;

        public override bool VisitIntoStructuredTrivia => this._visitIntoStructuredTrivia;
        public bool HasWork => this._nodeSet.Count + this._tokenSet.Count + this._triviaSet.Count > 0;

        public Replacer(
            IEnumerable<TNode>? nodes,
            Func<TNode, TNode, ThisSyntaxNode>? computeReplacementNode,
            IEnumerable<SyntaxToken>? tokens,
            Func<SyntaxToken, SyntaxToken, SyntaxToken>? computeReplacementToken,
            IEnumerable<SyntaxTrivia>? trivia,
            Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? computeReplacementTrivia)
        {
            this._computeReplacementNode = computeReplacementNode;
            this._computeReplacementToken = computeReplacementToken;
            this._computeReplacementTrivia = computeReplacementTrivia;

            this._nodeSet = nodes is null ? Replacer<TNode>.s_noNodes : new(nodes);
            this._tokenSet = tokens is null ? Replacer<TNode>.s_noTokens : new(tokens);
            this._triviaSet = trivia is null ? Replacer<TNode>.s_noTrivia : new(trivia);

            this._spanSet = new(
                new[]
                {
                    from n in this._nodeSet select n.FullSpan,
                    from t in this._tokenSet select t.FullSpan,
                    from t in this._triviaSet select t.FullSpan
                }.SelectMany(spans => spans)
            );

            // 快速计算总文本范围，缩小搜索范围。
            this._totalSpan = Replacer<TNode>.ComputeTotalSpan(this._spanSet);

            this._visitIntoStructuredTrivia =
                this._nodeSet.Any(n => n.IsPartOfStructuredTrivia()) ||
                this._tokenSet.Any(t => t.IsPartOfStructuredTrivia()) ||
                this._triviaSet.Any(t => t.IsPartOfStructuredTrivia());

            this._shouldVisitTrivia = this._triviaSet.Count > 0 || this._visitIntoStructuredTrivia;
        }

        /// <summary>
        /// 快速计算多个文本范围合并的总文本范围，包含中间未覆盖的范围。
        /// </summary>
        /// <param name="spans">要合并计算的多个文本范围。</param>
        /// <returns><paramref name="spans"/>合并后的总文本范围。</returns>
        private static TextSpan ComputeTotalSpan(HashSet<TextSpan> spans)
        {
            bool first = true;
            int start = 0;
            int end = 0;

            foreach (var span in spans)
            {
                if (first)
                {
                    start = span.Start;
                    end = span.End;
                    first = false;
                }
                else
                {
                    start = Math.Min(start, span.Start);
                    end = Math.Max(end, span.End);
                }
            }

            return new TextSpan(start, end - start);
        }

        /// <summary>
        /// 替换器是否应该进入指定的文本区域。
        /// </summary>
        /// <param name="span"></param>
        /// <returns>若<paramref name="span"/>与<see cref="Replacer{TNode}._spanSet"/>中的任何一项有交集，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
        private bool ShouldVisit(TextSpan span)
        {
            // 首先快速与总文本范围进行交集测试。
            if (!span.IntersectsWith(this._totalSpan)) return false;

            foreach (var s in this._spanSet)
            {
                if (span.IntersectsWith(s)) return true;
            }

            return false;
        }

        /// <summary>
        /// 处理语法节点并产生替换后的语法节点。
        /// </summary>
        /// <returns>替换后的语法节点。</returns>
        /// <remarks>
        /// 若<paramref name="node"/>不在需要替换的语法节点集中，或未指定用于计算替换后的语法节点的委托，则方法将返回<paramref name="node"/>本身。
        /// </remarks>
        /// <inheritdoc/>
        [return: NotNullIfNotNull(nameof(node))]
        public override ThisSyntaxNode? Visit(ThisSyntaxNode? node)
        {
            if (node is null) return null;

            ThisSyntaxNode rewritten;

            if (this.ShouldVisit(node.FullSpan))
                rewritten = base.Visit(node);
            else
                rewritten = node;

            if (this._nodeSet.Contains(node) && this._computeReplacementNode is not null)
                rewritten = this._computeReplacementNode((TNode)node, (TNode)rewritten);

            return rewritten;
        }

        /// <summary>
        /// 处理语法标志并产生替换后的语法标志。
        /// </summary>
        /// <returns>替换后的语法标志。</returns>
        /// <remarks>
        /// 若<paramref name="token"/>不在需要替换的语法标志集中，或未指定用于计算替换后的语法标志的委托，则方法将返回<paramref name="token"/>本身。
        /// </remarks>
        /// <inheritdoc/>
        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            SyntaxToken rewritten;

            if (this._shouldVisitTrivia && this.ShouldVisit(token.FullSpan))
                rewritten = base.VisitToken(token);
            else
                rewritten = token;

            if (this._tokenSet.Contains(token) && this._computeReplacementToken is not null)
                rewritten = this._computeReplacementToken(token, rewritten);

            return rewritten;
        }

        /// <summary>
        /// 处理语法琐碎内容并产生替换后的语法琐碎内容。
        /// </summary>
        /// <returns>替换后的语法琐碎内容。</returns>
        /// <remarks>
        /// 若<paramref name="trivia"/>不在需要替换的语法琐碎内容集中，或未指定用于计算替换后的语法琐碎内容的委托，则方法将返回<paramref name="trivia"/>本身。
        /// </remarks>
        /// <inheritdoc/>
        public override SyntaxTrivia VisitListElement(SyntaxTrivia trivia)
        {
            SyntaxTrivia rewritten;

            if (this.VisitIntoStructuredTrivia && trivia.HasStructure && this.ShouldVisit(trivia.FullSpan))
                rewritten = this.VisitTrivia(trivia);
            else
                rewritten = trivia;

            if (this._triviaSet.Contains(trivia) && this._computeReplacementTrivia is not null)
                rewritten = this._computeReplacementTrivia(trivia, rewritten);

            return rewritten;
        }
    }
}
