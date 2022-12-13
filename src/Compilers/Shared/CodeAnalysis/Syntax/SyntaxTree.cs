// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/*
 * Latest code review on 2022.7.3.
 */

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;
using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using Qtyi.CodeAnalysis.Lua.Syntax;
using ThisSyntaxTree = LuaSyntaxTree;
using ThisSyntaxNode = LuaSyntaxNode;
using ThisParseOptions = LuaParseOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using Qtyi.CodeAnalysis.MoonScript.Syntax;
using ThisSyntaxTree = MoonScriptSyntaxTree;
using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisParseOptions = MoonScriptParseOptions;
#endif

#if LANG_LUA
/// <summary>一份Lua源代码文件的解析后表示。</summary>
#elif LANG_MOONSCRIPT
/// <summary>一份MoonScript源代码文件的解析后表示。</summary>
#endif
public abstract partial class
#if LANG_LUA
    LuaSyntaxTree
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxTree
#endif
    : SyntaxTree
{
#warning 需要文档注释。
    internal static readonly SyntaxTree Dummy = new DummySyntaxTree();

    /// <summary>
    /// 获取导出语法树的解析器使用的选项集。
    /// </summary>
    /// <value>
    /// 导出语法树的解析器使用的选项集，包含改变解析器各项行为的选项。
    /// </value>
    public new abstract ThisParseOptions Options { get; }

    /// <summary>
    /// 获取代码文件的路径。
    /// </summary>
    /// <value>
    /// 代码文件的路径。
    /// </value>
    /// <inheritdoc/>
    public abstract override string FilePath { get; }

    /// <summary>
    /// 获取一个值，指示此语法树是否含有表示编译单元的根语法节点。
    /// </summary>
    /// <value>
    /// 若此语法树是否含有表示编译单元的根语法节点，则返回<see langword="true"/>；否则返回<see langword="false"/>。
    /// </value>
    public abstract override bool HasCompilationUnitRoot { get; }

    /// <summary>
    /// 获取代码文件的编码。
    /// </summary>
    /// <value>
    /// 代码文件的编码。
    /// </value>
    public abstract override Encoding? Encoding { get; }

    /// <summary>
    /// 获取此语法树包含的文本的长度。
    /// </summary>
    /// <value>
    /// 此语法树包含的文本的长度。
    /// </value>
    public abstract override int Length { get; }

    /// <inheritdoc cref="ThisSyntaxNode.CloneNodeAsRoot{T}(T, SyntaxTree)"/>
    /// <seealso cref="ThisSyntaxNode.CloneNodeAsRoot{T}(T, SyntaxTree)"/>
    protected T CloneNodeAsRoot<T>(T node)
        where T : ThisSyntaxNode =>
        ThisSyntaxNode.CloneNodeAsRoot(node, this);

    /// <summary>
    /// 获取语法树的根节点。
    /// </summary>
    /// <param name="cancellationToken">获取过程的取消标记。</param>
    /// <returns>语法树的根节点。</returns>
    public new abstract ThisSyntaxNode GetRoot(CancellationToken cancellationToken = default);

    /// <summary>
    /// 尝试获取语法树的根节点，并返回一个值指示其是否存在。
    /// </summary>
    /// <param name="root">语法树的根节点</param>
    /// <returns>一个值，指示语法树的根节点是否存在。</returns>
    public abstract bool TryGetRoot([NotNullWhen(true)] out ThisSyntaxNode? root);

    /// <summary>
    /// 异步获取语法树的根节点。
    /// </summary>
    /// <param name="cancellationToken">获取过程的取消标记。</param>
    /// <returns>获取过程的任务。</returns>
    /// <remarks>
    /// 默认情况下获取工作将在当前线程中立即执行。
    /// 若希望进行其他安排的实现，则应该重写 <see cref="GetRootAsync(CancellationToken)"/>。
    /// </remarks>
    public new virtual Task<ThisSyntaxNode> GetRootAsync(CancellationToken cancellationToken = default) =>
        Task.FromResult(this.TryGetRoot(out ThisSyntaxNode? node) ? node : this.GetRoot(cancellationToken));

    /* GetCompilationUnitRoot方法在各语言特定的类库中定义。 */

    /// <summary>
    /// 判断两棵语法树是否相等，忽略可能不同的语法琐碎内容。
    /// </summary>
    /// <param name="tree">与此语法树进行相等比较的语法树。</param>
    /// <param name="topLevel">
    /// <para>设置为<see langword="true"/>时，仅要求语法树内部的定义了元数据可见符号信息的节点和标志相等，忽略位于方法体和初始化表达式内部的节点差异。</para>
    /// <para>设置为<see langword="false"/>时，要求语法树内部的所有节点和标志必须全部相等。</para>
    /// </param>
    /// <returns>若此语法树与<paramref name="tree"/>相等时返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public override bool IsEquivalentTo(SyntaxTree tree, bool topLevel = false) =>
        SyntaxFactory.AreEquivalent(this, tree, topLevel);

    #region 工厂方法
    /// <summary>
    /// 创建一个新语法树对象，此对象使用指定的根语法节点、可选的解析器选项集、路径信息和文本编码。
    /// </summary>
    /// <param name="root">要设置为新语法树对象根节点的语法节点。</param>
    /// <param name="options">新语法树对象使用的解析器导出选项。</param>
    /// <param name="path">新语法树对象使用的代码文本的路径。</param>
    /// <param name="encoding">新语法树对象使用的代码文本的编码。</param>
    /// <returns>以<paramref name="root"/>为根语法节点创建的新语法树对象。</returns>
    /// <exception cref="ArgumentNullException"><paramref name="root"/>的值为<see langword="null"/>。</exception>
    public static SyntaxTree Create(
        ThisSyntaxNode root,
        ThisParseOptions? options = null,
        string? path = "",
        Encoding? encoding = null
    ) =>
        new ParsedSyntaxTree(
            text: null,
            encoding: encoding,
            checksumAlgorithm: SourceHashAlgorithm.Sha1,
            path: path,
            options: options ?? ThisParseOptions.Default,
            root: root ?? throw new ArgumentNullException(nameof(root)),
            cloneRoot: true);

    /// <summary>
    /// 创建一个用于调试的新语法树对象，此对象使用指定的根语法节点、代码文本和解析器选项集。
    /// </summary>
    /// <param name="root">要设置为新语法树对象根节点的语法节点。</param>
    /// <param name="text">新语法树对象使用的代码文本。此代码文本应与<paramref name="root"/>包含的内容相等。</param>
    /// <param name="options">新语法树对象使用的解析器导出选项。</param>
    /// <returns>以<paramref name="root"/>为根语法节点创建的用于调试的新语法树对象。</returns>
    internal static SyntaxTree CreateForDebugger(
        ThisSyntaxNode root,
        SourceText text,
        ThisParseOptions options
    ) => new DebuggerSyntaxTree(root, text, options);

    /// <summary>
    /// 创建一个使用指定的根语法节点的新语法树对象，但不复制这个根节点。
    /// </summary>
    /// <param name="root">要设置为新语法树对象根节点的语法节点。</param>
    /// <returns>以<paramref name="root"/>为根语法节点创建的新语法树对象。</returns>
    /// <remarks>
    /// <para>
    /// 此方法用于<see cref="ThisSyntaxNode"/>创建以<paramref name="root"/>为根节点的新语法树对象。此方法不会复制这个节点，反而保留其引用信息。
    /// </para>
    /// <para>
    /// 注意：此方法应仅由<see cref="ThisSyntaxNode.SyntaxTree"/>属性调用。
    /// </para>
    /// <para>
    /// 注意：不应在其他位置调用此方法，若要创建新语法树对象，应使用<see cref="ThisSyntaxTree.Create(ThisSyntaxNode, ThisParseOptions?, string?, Encoding?)"/>。
    /// </para>
    /// </remarks>
    internal static SyntaxTree CreateWithoutClone(ThisSyntaxNode root) =>
        new ParsedSyntaxTree(
            text: null,
            encoding: null,
            checksumAlgorithm: SourceHashAlgorithm.Sha1,
            path: "",
            options: ThisParseOptions.Default,
            root: root,
            cloneRoot: false);

    /// <summary>
    /// 创建一个延迟解析代码文本的新语法树对象。
    /// </summary>
    /// <remarks>
    /// 语法树只有在调用<see cref="ThisSyntaxTree.GetRoot(CancellationToken)"/>时才解析。
    /// </remarks>
    /// <param name="text">新语法树对象使用的代码文本。</param>
    /// <param name="options">新语法树对象使用的解析器导出选项。</param>
    /// <param name="path">新语法树对象使用的代码文本的路径。</param>
    /// <returns>延迟解析代码文本的新语法树对象。</returns>
    internal static SyntaxTree ParseTextLazy(
        SourceText text,
        ThisParseOptions? options = null,
        string path = "") =>
        new LazySyntaxTree(text, options ?? ThisParseOptions.Default, path);

    /// <inheritdoc cref="ThisSyntaxTree.ParseText(SourceText, ThisParseOptions?, string, CancellationToken)"/>
    public static SyntaxTree ParseText(
        string text,
        ThisParseOptions? options = null,
        string path = "",
        Encoding? encoding = null,
        CancellationToken cancellationToken = default) => ThisSyntaxTree.ParseText(
            text: SourceText.From(text, encoding),
            options: options,
            path: path,
            cancellationToken: cancellationToken);

    /// <summary>
    /// 解析代码文本并产生语法树。
    /// </summary>
    /// <param name="text">要解析的代码文本。</param>
    /// <param name="options">解析选项。</param>
    /// <param name="path">代码文本的路径。</param>
    /// <param name="cancellationToken">解析操作的取消标志。</param>
    /// <returns>表示<paramref name="text"/>的所有信息的语法树。</returns>
    public static ThisSyntaxTree ParseText(
        SourceText text,
        ThisParseOptions? options = null,
        string path = "",
        CancellationToken cancellationToken = default)
    {
        if (text is null) throw new ArgumentNullException(nameof(text));

        options ??= ThisParseOptions.Default;

        // 创建词法分析器。
        using var lexer = new Syntax.InternalSyntax.Lexer(text, options);
        // 创建语言解析器。
        using var parser = new Syntax.InternalSyntax.LanguageParser(lexer, oldTree: null, changes: null, cancellationToken: cancellationToken);
        // 解析并生成编译单元节点（绿树节点）对应的红树节点。
        var chunk = (ChunkSyntax)parser.ParseCompilationUnit().CreateRed();

        // 创建已解析的语法树，使用编译单元节点作为根节点。
        var tree = new ParsedSyntaxTree(
            text,
            text.Encoding,
            text.ChecksumAlgorithm,
            path,
            options,
            chunk,
            cloneRoot: true);
        // 验证语法树的所有节点均匹配代码文本。
        tree.VerifySource();

        return tree;
    }
    #endregion

    #region 更改
    public override SyntaxTree WithChangedText(SourceText newText)
    {
        if (this.TryGetText(out SourceText? oldText))
        {
            var changes = newText.GetChangeRanges(oldText);

            if (changes.Count == 0 && newText == oldText) return this;

            return this.WithChanges(newText, changes);
        }

        return this.WithChanges(newText, new[] { new TextChangeRange(new TextSpan(0, this.Length), newText.Length) });
    }

    private SyntaxTree WithChanges(SourceText newText, IReadOnlyList<TextChangeRange> changes)
    {
        if (changes is null) throw new ArgumentNullException(nameof(changes));

        var workingChanges = changes;
        var oldTree = this;

        // 如果全文都发生更改，则重新进行全文解析。
        if (workingChanges.Count == 1 && workingChanges[0].Span == new TextSpan(0, this.Length) && workingChanges[0].NewLength == newText.Length)
        {
            workingChanges = null;
            oldTree = null;
        }

        // 创建词法分析器。
        using var lexer = new Syntax.InternalSyntax.Lexer(newText, this.Options);
        // 创建语言解析器。
        using var parser = new Syntax.InternalSyntax.LanguageParser(lexer, oldTree?.GetRoot(), workingChanges);
        // 解析并生成编译单元节点（绿树节点）对应的红树节点。
        var chunk = (ChunkSyntax)parser.ParseCompilationUnit().CreateRed();

        // 创建已解析的语法树，使用编译单元节点作为根节点。
        return new ParsedSyntaxTree(
            newText,
            newText.Encoding,
            newText.ChecksumAlgorithm,
            this.FilePath,
            this.Options,
            chunk,
            cloneRoot: true
        );
    }

    public override IList<TextSpan> GetChangedSpans(SyntaxTree oldTree) => SyntaxDiffer.GetPossiblyDifferentTextSpans(oldTree, this);

    public override IList<TextChange> GetChanges(SyntaxTree oldTree) => SyntaxDiffer.GetTextChanges(oldTree, this);
    #endregion

    #region 行位置和定位
    /// <summary>
    /// 获取指定的字符位置对应的行位置。
    /// </summary>
    /// <param name="position">指定的字符位置。</param>
    /// <param name="cancellationToken">取消操作的标识符。</param>
    /// <returns>指定的字符位置对应的行位置。</returns>
    private LinePosition GetLinePosition(int position, CancellationToken cancellationToken) => this.GetText(cancellationToken).Lines.GetLinePosition(position);

    /// <summary>
    /// 获取指定的文本范围所在的位置信息。
    /// </summary>
    /// <param name="span">一段文本的范围。</param>
    /// <returns>这段文本所在的位置信息。</returns>
    public override Location GetLocation(TextSpan span) => new SourceLocation(this, span);

    public override FileLinePositionSpan GetLineSpan(TextSpan span, CancellationToken cancellationToken = default) =>
        new(this.FilePath, this.GetLinePosition(span.Start, cancellationToken), this.GetLinePosition(span.End, cancellationToken));

    /// <summary>
    /// 获取重新映射后的行位置范围信息。因为基于Lua的语言不支持重新映射行位置，所以此方法等同于<see cref="GetLineSpan(TextSpan, CancellationToken)"/>。
    /// </summary>
    /// <returns>重新映射后的行位置范围信息。</returns>
    /// <inheritdoc cref="GetLineSpan(TextSpan, CancellationToken)"/>
    public sealed override FileLinePositionSpan GetMappedLineSpan(TextSpan span, CancellationToken cancellationToken = default) => this.GetLineSpan(span, cancellationToken);

    internal sealed override FileLinePositionSpan GetMappedLineSpanAndVisibility(TextSpan span, out bool isHiddenPosition) => base.GetMappedLineSpanAndVisibility(span, out isHiddenPosition);

    public sealed override LineVisibility GetLineVisibility(int position, CancellationToken cancellationToken = default) => LineVisibility.Visible;

    public sealed override IEnumerable<LineMapping> GetLineMappings(CancellationToken cancellationToken = default) => Array.Empty<LineMapping>();

    public sealed override bool HasHiddenRegions() => false;
    #endregion

    #region 诊断
    public override IEnumerable<Diagnostic> GetDiagnostics(SyntaxNode node) => this.GetDiagnostics(node.Green, node.Position);

    private IEnumerable<Diagnostic> GetDiagnostics(GreenNode node, int position)
    {
        if (node.ContainsDiagnostics)
            return this.EnumerateDiagnostics(node, position);
        else
            return SpecializedCollections.EmptyEnumerable<Diagnostic>();
    }

    private IEnumerable<Diagnostic> EnumerateDiagnostics(GreenNode node, int position) =>
        new SyntaxTreeDiagnosticEnumerator(this, node, position).GetEnumerable();

    public override IEnumerable<Diagnostic> GetDiagnostics(SyntaxToken token)
    {
        Debug.Assert(token.Node is not null);
        return this.GetDiagnostics(token.Node, token.Position);
    }

    public override IEnumerable<Diagnostic> GetDiagnostics(SyntaxTrivia trivia)
    {
        Debug.Assert(trivia.UnderlyingNode is not null);
        return this.GetDiagnostics(trivia.UnderlyingNode, trivia.Position);
    }

    public override IEnumerable<Diagnostic> GetDiagnostics(SyntaxNodeOrToken nodeOrToken)
    {
        Debug.Assert(nodeOrToken.UnderlyingNode is not null);
        return this.GetDiagnostics(nodeOrToken.UnderlyingNode, nodeOrToken.Position);
    }

    public override IEnumerable<Diagnostic> GetDiagnostics(CancellationToken cancellationToken = default) => this.GetDiagnostics(this.GetRoot(cancellationToken));
    #endregion

    #region SyntaxTree
    /// <inheritdoc/>
    protected sealed override ParseOptions OptionsCore => this.Options;

    /// <inheritdoc/>
    protected sealed override SyntaxNode GetRootCore(CancellationToken cancellationToken) => this.GetRoot(cancellationToken);

    /// <inheritdoc/>
    protected sealed override async Task<SyntaxNode> GetRootAsyncCore(CancellationToken cancellationToken) => await this.GetRootAsync(cancellationToken).ConfigureAwait(false);

    /// <inheritdoc/>
    protected sealed override bool TryGetRootCore([NotNullWhen(true)] out SyntaxNode? root)
    {
        if (this.TryGetRoot(out ThisSyntaxNode? node))
        {
            root = node;
            return true;
        }
        else
        {
            root = null;
            return false;
        }
    }
    #endregion
}
