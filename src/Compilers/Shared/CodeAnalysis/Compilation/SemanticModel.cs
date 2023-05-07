// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxTree = LuaSyntaxTree;
using ThisCompilation = LuaCompilation;
using ThisSemanticModel = LuaSemanticModel;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxTree = MoonScriptSyntaxTree;
using ThisCompilation = MoonScriptCompilation;
using ThisSemanticModel = MoonScriptSemanticModel;
#endif

using Symbols;

internal abstract partial class
#if LANG_LUA
    LuaSemanticModel
#elif LANG_MOONSCRIPT
    MoonScriptSemanticModel
#endif
    : SemanticModel
{
    /// <summary>
    /// 获取获得此语义模型的来源编译内容。
    /// </summary>
    /// <value>
    /// 编译内容的实例，其<see cref="ThisCompilation.GetSemanticModel(SyntaxTree, bool)"/>返回此语义模型。
    /// </value>
    public new abstract ThisCompilation Compilation { get; }

    /// <summary>
    /// 获取此语义模型的父语义模型。
    /// </summary>
    /// <value>
    /// 若此语义模型是推测式的，则返回其父语义模型；否则返回<see langword="null"/>。
    /// </value>
    public new abstract ThisSemanticModel? ParentModel { get; }

    /// <summary>
    /// 获取与此语义模型相关联的语法树。
    /// </summary>
    /// <value>
    /// 与此语义模型相关联的语法树。
    /// </value>
    public new abstract ThisSyntaxTree SyntaxTree { get; }

    /// <summary>
    /// 绑定基于的语法树的根节点。
    /// </summary>
    internal new abstract ThisSyntaxNode Root { get; }

    public partial ISymbol GetDeclaredSymbol(ThisSyntaxNode node, CancellationToken cancellationToken = default);

    #region 帮助方法
    /// <summary>
    /// 获取一个值，指示指定语法节点是否能被查询函数查询。
    /// </summary>
    /// <param name="node">要被查询的语法节点。</param>
    /// <param name="allowNamedArgumentName">是否允许查询有名实参的名称。</param>
    /// <param name="isSpeculative">是否是推测式查询。若传入<see langword="true"/>，则不检查<paramref name="node"/>的<see cref="ThisSyntaxNode.Parent"/>。</param>
    /// <returns>若指定语法节点能被查询函数查询，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal static partial bool CanGetSemanticInfo(ThisSyntaxNode node, bool allowNamedArgumentName = false, bool isSpeculative = false);

    /// <summary>
    /// 获取一个值，指示指定语法节点是否位于与此语义模型相关联的语法树中。
    /// </summary>
    /// <param name="node">要检查的语法节点。</param>
    /// <returns>若<paramref name="node"/>位于<see cref="SyntaxTree"/>中，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal bool IsInTree(ThisSyntaxNode node) => node.SyntaxTree == this.SyntaxTree;

    /// <summary>
    /// A convenience method that determines a position from a node.  If the node is missing,
    /// then its position will be adjusted using CheckAndAdjustPosition.
    /// </summary>
    protected int GetAdjustedNodePosition(ThisSyntaxNode node)
    {
        Debug.Assert(this.IsInTree(node));

        var fullSpan = this.Root.FullSpan;
        var position = node.SpanStart;

        // 跳过零宽语法标记，但不跳过语法节点末尾。
        var firstToken = node.GetFirstToken(includeZeroWidth: false);
        if (firstToken.Node is not null)
        {
            var betterPosition = firstToken.SpanStart;
            if (betterPosition < node.Span.End) // 防止跳出语法节点范围。
                position = betterPosition;
        }

        if (fullSpan.IsEmpty)
        {
            Debug.Assert(position == fullSpan.Start);
            return position;
        }
        else if (position == fullSpan.End)
        {
            Debug.Assert(node.Width == 0);
            // 若要检查的语法节点位于总范围的末尾，且宽度为零，则检查并调整为前方一个位置。
            return this.CheckAndAdjustPosition(position - 1);
        }
        else if (node.IsMissing || node.HasErrors || node.Width == 0 || node.IsPartOfStructuredTrivia())
        {
            // 若要检查的语法节点缺失、含有错误、宽度为零或属于语法琐碎内容，则检查并调整为当前位置。
            return this.CheckAndAdjustPosition(position);
        }
        else
        {
            // 不用检查和调整位置。
            return position;
        }
    }

    /// <summary>
    /// 断言调整后的位置并未发生变化。
    /// </summary>
    /// <param name="position">要使用<see cref="CheckAndAdjustPosition(int)"/>检查和调整的位置。</param>
    [Conditional("DEBUG")]
    protected void AssertPositionAdjusted(int position)
    {
        Debug.Assert(position == this.CheckAndAdjustPosition(position), "Expected adjusted position");
    }

    /// <summary>
    /// 检查指定位置是否是某一个语法标记的起始位置，若不是则调整到这个起始位置。
    /// </summary>
    /// <returns><see cref="Root"/>中的某一个语法标记的起始位置，与<paramref name="position"/>可能相等。</returns>
    /// <inheritdoc cref="CheckAndAdjustPosition(int, out SyntaxToken)"/>
    protected int CheckAndAdjustPosition(int position) => this.CheckAndAdjustPosition(position, out _);

    /// <summary>
    /// 检查指定位置是否是某一个语法标记的起始位置并返回后者，若不是则调整到这个起始位置。
    /// </summary>
    /// <param name="position">要检查的位置。</param>
    /// <param name="token">传出<paramref name="position"/>落在其范围中的语法标记。</param>
    /// <returns><paramref name="token"/>的起始位置，与<paramref name="position"/>可能相等。</returns>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="position"/>落在<see cref="Root"/>根语法节点的范围之外。</exception>
    protected int CheckAndAdjustPosition(int position, out SyntaxToken token)
    {
        var fullStart = this.Root.Position;
        var fullEnd = this.Root.FullSpan.End;
        var atEOF = position == fullEnd && position == this.SyntaxTree.GetRoot().FullSpan.End;

        if ((fullStart <= position && position < fullEnd) || atEOF)
        {
            token = this.FindTokenAtPosition(
                atEOF ? this.SyntaxTree.GetRoot() : this.Root,
                position,
                atEOF);

            if (position < token.SpanStart)
            {
                // 若此时已经是第一个语法标记，则返回SyntaxToken的默认值。
                token = token.GetPreviousToken();
            }

            // 根节点可能缺失第一个语法标记，因此需防止越界。
            return Math.Max(token.SpanStart, fullStart);
        }
        else if (fullStart == fullEnd && position == fullEnd)
        {
            // 根节点的文本区域为空且不是完整的编译单元。
            token = default;
            return fullStart;
        }

        throw new ArgumentOutOfRangeException(nameof(position), position,
            string.Format(LunaResources.PositionIsNotWithinSyntax, Root.FullSpan));
    }

    /// <summary>
    /// 返回指定根节点中的某个语法标记，指定位置落在其范围中。
    /// </summary>
    /// <param name="root">要查找的语法标记所在的根语法节点。</param>
    /// <param name="position">落于要查找的语法标记的范围内的位置。</param>
    /// <param name="atEOF">要查找的语法标记是否位于<paramref name="root"/>的结尾。</param>
    /// <returns><paramref name="root"/>中的某个语法标记，<paramref name="position"/>落在其范围中。</returns>
    private partial SyntaxToken FindTokenAtPosition(ThisSyntaxNode root, int position, bool atEOF);

    /// <summary>
    /// 检查指定语法节点是否位于此语义模型相关联的语法树中。
    /// </summary>
    /// <param name="node">要检查的语法节点。</param>
    /// <exception cref="ArgumentException"><paramref name="node"/>不位于<see cref="SyntaxTree"/>中。</exception>
    /// <see cref="IsInTree(ThisSyntaxNode)"/>
    protected void CheckSyntaxNode(ThisSyntaxNode node)
    {
        if (!this.IsInTree(node))
            throw new ArgumentException(LunaResources.SyntaxNodeIsNotWithinSyntaxTree);
    }

    // This method ensures that the given syntax node to speculate is non-null and doesn't belong to a SyntaxTree of any model in the chain.
    /// <summary>
    /// 检查指定语法节点是否不属于一系列语义模型相关联的语法树中的任何一颗。
    /// </summary>
    /// <param name="node">要检查的语法节点。</param>
    /// <exception cref="InvalidOperationException">此语义模型是推测式的，不支持串联推测式语义模型。</exception>
    /// <exception cref="ArgumentException">推测式语法节点不应属于当前的编译内容。</exception>
    private void CheckModelAndSyntaxNodeToSpeculate(ThisSyntaxNode node)
    {
        if (this.IsSpeculativeSemanticModel)
            throw new InvalidOperationException(LunaResources.ChainingSpeculativeModelIsNotSupported);

        if (this.Compilation.ContainsSyntaxTree(node.SyntaxTree))
            throw new ArgumentException(LunaResources.SpeculatedSyntaxNodeCannotBelongToCurrentCompilation);
    }
    #endregion

    #region 查询符号
    /// <summary>
    /// 供其他查找符号方法调用以获得位于指定位置及可选父符号的上下文中的符合条件的有名符号，方法仅返回在指定位置可操作及可见的符号。
    /// </summary>
    /// <param name="position">决定封闭声明作用域和可见性的字符位置。</param>
    /// <param name="container">在其范围内进行查找的父符号。若传入<see langword="null"/>，则使用<paramref name="position"/>所在位置的封闭声明作用域。</param>
    /// <param name="name">要获取的符号的指定名称。若传入<see langword="null"/>，则返回任何名称的符号。</param>
    /// <param name="options">影响查找操作的设置。</param>
    /// <returns>查找到的不可变符号数组。若未查找到结果，则返回空数组。</returns>
    private ImmutableArray<ISymbol> LookupSymbolsInternal(
        int position,
        ModuleSymbol? container,
        string? name,
        LookupOptions options);
    #endregion

    private partial SymbolInfo GetSymbolInfoFromNode(ThisSyntaxNode node, CancellationToken cancellationToken);

    private partial TypeInfo GetTypeInfoFromNode(ThisSyntaxNode node, CancellationToken cancellationToken);

    #region SemanticModel
    protected sealed override Compilation CompilationCore => this.Compilation;

    protected sealed override SemanticModel? ParentModelCore => this.ParentModel;

    protected sealed override SyntaxTree SyntaxTreeCore => this.SyntaxTree;

    protected sealed override SyntaxNode RootCore => this.Root;
    #endregion
}
