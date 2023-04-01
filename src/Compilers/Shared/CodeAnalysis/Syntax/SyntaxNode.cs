// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using Qtyi.CodeAnalysis.Lua.Syntax;
using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxTree = LuaSyntaxTree;
using InternalSyntaxNode = Syntax.InternalSyntax.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using Qtyi.CodeAnalysis.MoonScript.Syntax;
using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxTree = MoonScriptSyntaxTree;
using InternalSyntaxNode = Syntax.InternalSyntax.MoonScriptSyntaxNode;
#endif

#if LANG_LUA
/// <summary>
/// 表示语法树中的非终结节点。此节点类仅针对于Lua语言构造。
/// </summary>
#elif LANG_MOONSCRIPT
/// <summary>
/// 表示语法树中的非终结节点。此节点类仅针对于MoonScript语言构造。
/// </summary>
#endif
public abstract partial class
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
          : SyntaxNode, IFormattable
{
    /// <summary>
    /// 获取此语法节点所在的语法树。
    /// </summary>
    internal new ThisSyntaxTree SyntaxTree =>
        (this._syntaxTree as ThisSyntaxTree) ?? ThisSyntaxNode.ComputeSyntaxTree(this);

    /// <summary>
    /// 获取此语法节点的父节点。若父节点不存在，则返回<see langword="null"/>。
    /// </summary>
    internal new ThisSyntaxNode? Parent => (ThisSyntaxNode?)base.Parent;

    /// <summary>
    /// 获取此语法节点的父节点，或此节点作为结构化语法琐碎内容根节点时的父节点。
    /// </summary>
    internal new ThisSyntaxNode? ParentOrStructuredTriviaParent => (ThisSyntaxNode?)base.ParentOrStructuredTriviaParent;

    /// <summary>
    /// 获取此语法节点描述的语言名称。
    /// </summary>
    public override string Language => this.Green.Language;

    /// <summary>
    /// 获取节点的语法类型。
    /// </summary>
    /// <returns>节点的语法类型。</returns>
    public SyntaxKind Kind() => (SyntaxKind)this.Green.RawKind;

    /// <summary>
    /// 实例化一个语法节点，指定节点的父节点。
    /// </summary>
    /// <param name="green">内部绿树节点。</param>
    /// <param name="parent">节点的父节点。</param>
    /// <param name="position">节点所在位置。</param>
    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
          (InternalSyntaxNode green, ThisSyntaxNode? parent, int position) : base(green, parent, position) { }

    /// <summary>
    /// 实例化一个语法节点，仅用于语法琐碎内容，因为它们不会作为节点的子级，即父节点为<see langword="null"/>，因此实例化时需要明确指明所在的语法树。
    /// </summary>
    /// <param name="green">内部绿树节点。</param>
    /// <param name="position">节点所在位置。</param>
    /// <param name="syntaxTree">节点所在的语法树。</param>
    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
#pragma warning disable CS8604
          (InternalSyntaxNode green, int position, ThisSyntaxTree? syntaxTree) : base(green, position, syntaxTree) { }
#pragma warning restore CS8604

    /// <summary>
    /// 创建一个红树节点的副本，作为指定语法树的根节点使用。
    /// 新节点不存在父节点，位置为0，且位于指定的语法树上。
    /// </summary>
    /// <typeparam name="T">语法节点的类型。</typeparam>
    /// <param name="node">要复制的语法节点。</param>
    /// <param name="syntaxTree">语法节点所在的语法树。</param>
    /// <returns><paramref name="node"/>的副本。</returns>
    internal static T CloneNodeAsRoot<T>(T node, ThisSyntaxTree syntaxTree) where T : ThisSyntaxNode => SyntaxNode.CloneNodeAsRoot(node, syntaxTree);

    /// <summary>
    /// 计算得到一个语法树对象，这个语法树对象以指定的语法节点为根节点。
    /// </summary>
    /// <param name="node">要设置为根节点的语法节点。</param>
    /// <returns>以<paramref name="node"/>为根语法节点创建的新语法树对象。</returns>
    private static ThisSyntaxTree ComputeSyntaxTree(ThisSyntaxNode node)
    {
        ArrayBuilder<ThisSyntaxNode>? nodes = null;
        ThisSyntaxTree? tree;

        // 查找最近的具有非空语法树的父节点。
        while (true)
        {
            tree = node._syntaxTree as ThisSyntaxTree;
            if (tree is not null) break; // 节点自身的语法树非空。

            var parent = node.Parent;
            if (parent is null) // 节点自身即为根节点。
            {
                // 原子操作设置语法树到根节点。
                Interlocked.Exchange(ref node._syntaxTree, ThisSyntaxTree.CreateWithoutClone(node));
                tree = (ThisSyntaxTree)node._syntaxTree;
                break;
            }

            tree = parent._syntaxTree as ThisSyntaxTree;
            if (tree is not null)
            {
                // 将父节点的语法树设置到节点自身上。
                node._syntaxTree = tree;
                break;
            }

            nodes ??= ArrayBuilder<ThisSyntaxNode>.GetInstance();

            nodes.Add(node);
            node = parent;
        }

        // 自上而下传递语法树。
        if (nodes is not null)
        {
            foreach (var n in nodes)
            {
                var existingTree = n._syntaxTree;
                if (existingTree is not null)
                {
                    Debug.Assert(existingTree == tree, "至少有一个节点位于其他语法树。");
                    break;
                }

                n._syntaxTree = tree;
            }

            nodes.Free();
        }

        return tree;
    }

    /// <summary>
    /// 获取此节点在源代码中的位置。
    /// </summary>
    /// <returns>此节点在源代码中的位置。</returns>
    public new Location GetLocation() => new SourceLocation(this);

    public abstract TResult? Accept<TResult>(
#if LANG_LUA
        LuaSyntaxVisitor<TResult>
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxVisitor<TResult>
#endif
        visitor);

    public abstract void Accept(
#if LANG_LUA
        LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxVisitor
#endif
         visitor);

    #region 序列化
    /// <summary>
    /// 从字节流中反序列化语法节点。
    /// </summary>
    /// <param name="stream">从中读取数据的流。</param>
    /// <param name="cancellationToken">取消操作的标志。</param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"><paramref name="stream"/>的值为<see langword="null"/>。</exception>
    /// <exception cref="InvalidOperationException"><paramref name="stream"/>流不可读。</exception>
    /// <exception cref="ArgumentException"><paramref name="stream"/>流中含有不合法的数据。</exception>
    public static SyntaxNode DeserializeFrom(Stream stream, CancellationToken cancellationToken = default)
    {
        if (stream is null) throw new ArgumentNullException(nameof(stream));

        if (!stream.CanRead)
            throw new InvalidOperationException(CodeAnalysisResources.TheStreamCannotBeReadFrom);

        using var reader = ObjectReader.TryGetReader(stream, leaveOpen: true, cancellationToken);

        if (reader is null)
            throw new ArgumentException(CodeAnalysisResources.Stream_contains_invalid_data, nameof(stream));

        var root = (InternalSyntaxNode)reader.ReadValue();
        return root.CreateRed();
    }
    #endregion

    #region 查找标志
    /// <summary>
    /// 获取以此节点为根节点的语法树的第一个标志。
    /// </summary>
    /// <param name="predicate">筛选符合条件的标志的方法。若要允许所有标志，则传入<see langword="null"/>。</param>
    /// <param name="stepInto">若值不是<see langword="null"/>时深入语法琐碎内容。仅此委托返回<see langword="true"/>时语法琐碎内容才会被包含在内。</param>
    /// <returns>以此节点为根节点的语法树的第一个标志。</returns>
    internal SyntaxToken GetFirstToken(Func<SyntaxToken, bool>? predicate, Func<SyntaxTrivia, bool>? stepInto = null) =>
        SyntaxNavigator.Instance.GetFirstToken(this, predicate, stepInto);

    /// <summary>
    /// 获取以此节点为根节点的语法树的最后一个标志。
    /// </summary>
    /// <param name="predicate">筛选符合条件的标志的方法。</param>
    /// <param name="stepInto">若值不是<see langword="null"/>时深入语法琐碎内容。仅此委托返回<see langword="true"/>时语法琐碎内容才会被包含在内。</param>
    /// <returns>以此节点为根节点的语法树的最后一个标志。</returns>
    internal SyntaxToken GetLastToken(Func<SyntaxToken, bool> predicate, Func<SyntaxTrivia, bool>? stepInto = null) =>
        SyntaxNavigator.Instance.GetLastToken(this, predicate, stepInto);
    #endregion

    #region SyntaxNode
    /// <summary>
    /// 获取此语法节点所在的语法树。若节点不再任何一颗语法树上，则自动生成一颗语法树。
    /// </summary>
    protected override SyntaxTree SyntaxTreeCore => this.SyntaxTree;

    /// <summary>
    /// 确定此节点是否与另一个节点在结构上相等。
    /// </summary>
    /// <param name="other">相比较的另一个节点。</param>
    /// <returns>若两个节点在结构上相等，则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    /// <exception cref="ExceptionUtilities.Unreachable">默认抛出的异常。</exception>
    protected override bool EquivalentToCore(SyntaxNode other) => throw ExceptionUtilities.Unreachable;

    protected internal override SyntaxNode ReplaceCore<TNode>(
        IEnumerable<TNode>? nodes = null,
        Func<TNode, TNode, SyntaxNode>? computeReplacementNode = null,
        IEnumerable<SyntaxToken>? tokens = null,
        Func<SyntaxToken, SyntaxToken, SyntaxToken>? computeReplacementToken = null,
        IEnumerable<SyntaxTrivia>? trivia = null,
        Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia>? computeReplacementTrivia = null)
    {
        if (!typeof(ThisSyntaxNode).IsAssignableFrom(typeof(TNode)))
            throw new InvalidOperationException(string.Format(LunaResources.SyntaxNodeTypeMustBeDerivedFromCertainType, nameof(TNode), typeof(ThisSyntaxNode).FullName));

        return SyntaxReplacer.Replace(
            this,
            nodes?.Cast<ThisSyntaxNode>(), computeReplacementNode is null ? null : (node, rewritten) => (ThisSyntaxNode)computeReplacementNode((node as TNode)!, (rewritten as TNode)!),
            tokens, computeReplacementToken,
            trivia, computeReplacementTrivia)
            .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);
    }

    protected internal override SyntaxNode ReplaceNodeInListCore(
        SyntaxNode originalNode,
        IEnumerable<SyntaxNode> replacementNodes) =>
        SyntaxReplacer.ReplaceNodeInList(this,
            (ThisSyntaxNode)originalNode,
            replacementNodes.Cast<ThisSyntaxNode>())
        .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);

    protected internal override SyntaxNode InsertNodesInListCore(
        SyntaxNode nodeInList,
        IEnumerable<SyntaxNode> nodesToInsert,
        bool insertBefore) =>
        SyntaxReplacer.InsertNodeInList(
            this,
            (ThisSyntaxNode)nodeInList,
            nodesToInsert.Cast<ThisSyntaxNode>(),
            insertBefore)
        .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);

    protected internal override SyntaxNode ReplaceTokenInListCore(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens) =>
        SyntaxReplacer.ReplaceTokenInList(
            this,
            originalToken,
            newTokens)
        .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);

    protected internal override SyntaxNode InsertTokensInListCore(SyntaxToken originalToken, IEnumerable<SyntaxToken> newTokens, bool insertBefore) =>
        SyntaxReplacer.InsertTokenInList(
            this,
            originalToken,
            newTokens,
            insertBefore)
        .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);

    protected internal override SyntaxNode ReplaceTriviaInListCore(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia) =>
        SyntaxReplacer.ReplaceTriviaInList(
            this,
            originalTrivia,
            newTrivia)
        .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);

    protected internal override SyntaxNode InsertTriviaInListCore(SyntaxTrivia originalTrivia, IEnumerable<SyntaxTrivia> newTrivia, bool insertBefore) =>
        SyntaxReplacer.InsertTriviaInList(
            this,
            originalTrivia,
            newTrivia,
            insertBefore)
        .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);

    protected internal override SyntaxNode? RemoveNodesCore(IEnumerable<SyntaxNode> nodes, SyntaxRemoveOptions options) =>
        SyntaxNodeRemover.RemoveNodes(
            this,
            nodes.Cast<ThisSyntaxNode>(),
            options)
        .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);

    protected internal override SyntaxNode NormalizeWhitespaceCore(string indentation, string eol, bool elasticTrivia) =>
        SyntaxNormalizer.Normalize(
            this,
            indentation,
            eol,
            elasticTrivia)
        .AsRootOfNewTreeWithOptionsFrom(this.SyntaxTree);

    protected override bool IsEquivalentToCore(SyntaxNode node, bool topLevel = false) =>
        SyntaxFactory.AreEquivalent(this, (ThisSyntaxNode)node, topLevel);
    #endregion

    string IFormattable.ToString(string? format, IFormatProvider? formatProvider) => this.ToString();
}
