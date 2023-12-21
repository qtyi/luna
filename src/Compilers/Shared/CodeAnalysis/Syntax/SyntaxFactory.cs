// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisParseOptions = LuaParseOptions;
using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxTree = LuaSyntaxTree;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisParseOptions = MoonScriptParseOptions;
using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxTree = MoonScriptSyntaxTree;
#endif

using InternalSyntaxFactory = Syntax.InternalSyntax.SyntaxFactory;
using InternalSyntaxToken = Syntax.InternalSyntax.SyntaxToken;

/// <summary>
/// 此类型提供构造各种语法节点、标记和琐碎内容的工厂方法。
/// </summary>
public static partial class SyntaxFactory
{
    /// <summary>
    /// 获取包含回车符和换行符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的语法琐碎内容，其包含回车符和换行符。
    /// </value>
    public static SyntaxTrivia CarriageReturnLineFeed { get; } = InternalSyntaxFactory.CarriageReturnLineFeed;
    /// <summary>
    /// 获取包含回车符和换行符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的可变的语法琐碎内容，其包含回车符和换行符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticCarriageReturnLineFeed { get; } = InternalSyntaxFactory.ElasticCarriageReturnLineFeed;

    /// <summary>
    /// 获取包含换行符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的语法琐碎内容，其包含单个换行符。
    /// </value>
    public static SyntaxTrivia LineFeed { get; } = InternalSyntaxFactory.LineFeed;
    /// <summary>
    /// 获取包含换行符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的可变的语法琐碎内容，其包含单个换行符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticLineFeed { get; } = InternalSyntaxFactory.ElasticLineFeed;

    /// <summary>
    /// 获取包含回车符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的语法琐碎内容，其包含单个回车符。
    /// </value>
    public static SyntaxTrivia CarriageReturn { get; } = InternalSyntaxFactory.CarriageReturn;
    /// <summary>
    /// 获取包含回车符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的可变的语法琐碎内容，其包含单个回车符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticCarriageReturn { get; } = InternalSyntaxFactory.ElasticCarriageReturn;

    /// <summary>
    /// 获取包含空格符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhiteSpaceTrivia"/>的语法琐碎内容，其包含单个空格符。
    /// </value>
    public static SyntaxTrivia Space { get; } = InternalSyntaxFactory.Space;
    /// <summary>
    /// 获取包含空格符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhiteSpaceTrivia"/>的可变的语法琐碎内容，其包含单个空格符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticSpace { get; } = InternalSyntaxFactory.ElasticSpace;

    /// <summary>
    /// 获取包含制表符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhiteSpaceTrivia"/>的语法琐碎内容，其包含单个制表符。
    /// </value>
    public static SyntaxTrivia Tab { get; } = InternalSyntaxFactory.Tab;
    /// <summary>
    /// 获取包含制表符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhiteSpaceTrivia"/>的可变的语法琐碎内容，其包含单个制表符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticTab { get; } = InternalSyntaxFactory.ElasticTab;

    /// <summary>
    /// 获取表示可变记号的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhiteSpaceTrivia"/>的可变的语法琐碎内容，其不包含任何字符。
    /// </value>
    /// <remarks>
    /// 当语法琐碎内容没有明确时，工厂方法将自动置入可变记号。在语法格式化阶段，可变记号将会被替换为合适的语法琐碎内容。
    /// </remarks>
    public static SyntaxTrivia ElasticMarker { get; } = InternalSyntaxFactory.ElasticZeroSpace;

    #region 琐碎内容
    public static SyntaxTrivia EndOfLine(string text) => InternalSyntaxFactory.EndOfLine(text, elastic: false);

    public static SyntaxTrivia ElasticEndOfLine(string text) => InternalSyntaxFactory.EndOfLine(text, elastic: true);

    public static SyntaxTrivia WhiteSpace(string text) => InternalSyntaxFactory.WhiteSpace(text, elastic: false);

    public static SyntaxTrivia ElasticWhiteSpace(string text) => InternalSyntaxFactory.WhiteSpace(text, elastic: true);

    public static SyntaxTrivia Comment(string text) => InternalSyntaxFactory.Comment(text);

    public static SyntaxTrivia PreprocessingMessage(string text) => InternalSyntaxFactory.PreprocessingMessage(text);

    public static SyntaxTrivia Trivia(Syntax.StructuredTriviaSyntax node) => new(default, node.Green, position: 0, index: 0);
    #endregion

    #region 标记
    public static SyntaxToken Token(SyntaxKind kind) =>
        new(InternalSyntaxFactory.Token(
            ElasticMarker.UnderlyingNode,
            kind,
            ElasticMarker.UnderlyingNode));

    public static SyntaxToken Token(
        SyntaxTriviaList leading,
        SyntaxKind kind,
        SyntaxTriviaList trailing) =>
        new(InternalSyntaxFactory.Token(
            leading.Node,
            kind,
            trailing.Node));

    public static SyntaxToken Token(
        SyntaxTriviaList leading,
        SyntaxKind kind,
        string text,
        string valueText,
        SyntaxTriviaList trailing)
    {
        ValidateTokenKind(kind);

        return new(InternalSyntaxFactory.Token(
            leading.Node,
            kind,
            text,
            valueText,
            trailing.Node));
    }

    private static partial void ValidateTokenKind(SyntaxKind kind);

#if DEBUG
    internal static ThisSyntaxNode Mock() => new ThisSyntaxNode.MockNode(InternalSyntaxFactory.Mock());

    internal static SyntaxToken Token(SyntaxNode parent, InternalSyntaxToken token, int position, int index) => Token((ThisSyntaxNode)parent, token, position, index);

    internal static SyntaxToken Token(ThisSyntaxNode parent, InternalSyntaxToken token, int position, int index) => new(parent, token, position, index);
#endif

    public static SyntaxToken MissingToken(SyntaxKind kind) =>
        new(InternalSyntaxFactory.MissingToken(
            ElasticMarker.UnderlyingNode,
            kind,
            ElasticMarker.UnderlyingNode));

    public static SyntaxToken MissingToken(
        SyntaxTriviaList leading,
        SyntaxKind kind,
        SyntaxTriviaList trailing) =>
        new(InternalSyntaxFactory.MissingToken(
            leading.Node,
            kind,
            trailing.Node));

    public static SyntaxToken BadToken(
        SyntaxTriviaList leading,
        string text,
        SyntaxTriviaList trailing) =>
        new(InternalSyntaxFactory.BadToken(
            leading.Node,
            text,
            trailing.Node));

    #region 标识符
    public static SyntaxToken Identifier(string text) =>
        new(InternalSyntaxFactory.Identifier(
            ElasticMarker.UnderlyingNode,
            text,
            ElasticMarker.UnderlyingNode));

    public static SyntaxToken Identifier(
        SyntaxTriviaList leading,
        string text,
        SyntaxTriviaList trailing) =>
        new(InternalSyntaxFactory.Identifier(
            leading.Node,
            text,
            trailing.Node));

    public static SyntaxToken Identifier(
        SyntaxTriviaList leading,
        SyntaxKind contextualKind,
        string text,
        string valueText,
        SyntaxTriviaList trailing) =>
        new(InternalSyntaxFactory.Identifier(
            contextualKind,
            leading.Node,
            text,
            valueText,
            trailing.Node));
    #endregion

    #region 字面量
    /// <summary>
    /// 构造表示64位有符号整数的语法标记。
    /// </summary>
    /// <param name="value">表示的64位有符号整数。</param>
    /// <returns>表示64位有符号整数的语法标记。</returns>
    public static partial SyntaxToken Literal(long value);

    /// <summary>
    /// 构造表示64位有符号整数的语法标记，使用指定的字符串表示。
    /// </summary>
    /// <param name="text">指定的<paramref name="value"/>的字符串表示。</param>
    /// <param name="value">表示的64位有符号整数。</param>
    /// <returns>表示64位有符号整数的语法标记。</returns>
    public static partial SyntaxToken Literal(string text, long value);

    /// <summary>
    /// 构造表示64位有符号整数的语法标记，使用指定的字符串表示以及前后方语法琐碎内容。
    /// </summary>
    /// <param name="leading">指定的前方语法琐碎内容。</param>
    /// <param name="text">指定的<paramref name="value"/>的字符串表示。</param>
    /// <param name="value">表示的64位有符号整数。</param>
    /// <param name="trailing">指定的后方语法琐碎内容。</param>
    /// <returns>表示64位有符号整数的语法标记。</returns>
    public static partial SyntaxToken Literal(
        SyntaxTriviaList leading,
        string text,
        long value,
        SyntaxTriviaList trailing);

    /// <summary>
    /// 构造表示64位无符号整数的语法标记。
    /// </summary>
    /// <param name="value">表示的64位无符号整数。</param>
    /// <returns>表示64位无符号整数的语法标记。</returns>
    public static partial SyntaxToken Literal(ulong value);

    /// <summary>
    /// 构造表示64位无符号整数的语法标记，使用指定的字符串表示。
    /// </summary>
    /// <param name="text">指定的<paramref name="value"/>的字符串表示。</param>
    /// <param name="value">表示的64位无符号整数。</param>
    /// <returns>表示64位无符号整数的语法标记。</returns>
    public static partial SyntaxToken Literal(string text, ulong value);

    /// <summary>
    /// 构造表示64位无符号整数的语法标记，使用指定的字符串表示以及前后方语法琐碎内容。
    /// </summary>
    /// <param name="leading">指定的前方语法琐碎内容。</param>
    /// <param name="text">指定的<paramref name="value"/>的字符串表示。</param>
    /// <param name="value">表示的64位无符号整数。</param>
    /// <param name="trailing">指定的后方语法琐碎内容。</param>
    /// <returns>表示64位无符号整数的语法标记。</returns>
    public static partial SyntaxToken Literal(
        SyntaxTriviaList leading,
        string text,
        ulong value,
        SyntaxTriviaList trailing);

    /// <summary>
    /// 构造表示双精度浮点数的语法标记。
    /// </summary>
    /// <param name="value">表示的双精度浮点数。</param>
    /// <returns>表示双精度浮点数的语法标记。</returns>
    public static partial SyntaxToken Literal(double value);

    /// <summary>
    /// 构造表示双精度浮点数的语法标记，使用指定的字符串表示。
    /// </summary>
    /// <param name="text">指定的<paramref name="value"/>的字符串表示。</param>
    /// <param name="value">表示的双精度浮点数。</param>
    /// <returns>表示双精度浮点数的语法标记。</returns>
    public static partial SyntaxToken Literal(string text, double value);

    /// <summary>
    /// 构造表示双精度浮点数的语法标记，使用指定的字符串表示以及前后方语法琐碎内容。
    /// </summary>
    /// <param name="leading">指定的前方语法琐碎内容。</param>
    /// <param name="text">指定的<paramref name="value"/>的字符串表示。</param>
    /// <param name="value">表示的双精度浮点数。</param>
    /// <param name="trailing">指定的后方语法琐碎内容。</param>
    /// <returns>表示双精度浮点数的语法标记。</returns>
    public static partial SyntaxToken Literal(
        SyntaxTriviaList leading,
        string text,
        double value,
        SyntaxTriviaList trailing);

    /// <summary>
    /// 构造表示字符串的语法标记。
    /// </summary>
    /// <param name="value">表示的字符串。</param>
    /// <returns>表示字符串的语法标记。</returns>
    public static partial SyntaxToken Literal(string value);

    /// <summary>
    /// 构造表示字符串的语法标记，使用指定的字符串表示。
    /// </summary>
    /// <param name="text">指定的<paramref name="value"/>的字符串表示。</param>
    /// <param name="value">表示的字符串。</param>
    /// <returns>表示字符串的语法标记。</returns>
    public static partial SyntaxToken Literal(string text, string value);

    /// <summary>
    /// 构造表示字符串的语法标记，使用指定的字符串表示以及前后方语法琐碎内容。
    /// </summary>
    /// <param name="leading">指定的前方语法琐碎内容。</param>
    /// <param name="text">指定的<paramref name="value"/>的字符串表示。</param>
    /// <param name="value">表示的字符串。</param>
    /// <param name="trailing">指定的后方语法琐碎内容。</param>
    /// <returns>表示字符串的语法标记。</returns>
    public static partial SyntaxToken Literal(
        SyntaxTriviaList leading,
        string text,
        string value,
        SyntaxTriviaList trailing);
    #endregion
    #endregion

    #region 列表
    public static SyntaxList<TNode> List<TNode>() where TNode : ThisSyntaxNode => default;

    public static SyntaxList<TNode> SingletonList<TNode>(TNode node) where TNode : ThisSyntaxNode => new(node);

    public static SyntaxList<TNode> List<TNode>(IEnumerable<TNode> nodes) where TNode : ThisSyntaxNode => new(nodes);
    #endregion

    #region 标记列表
    public static SyntaxTokenList TokenList() => default;

    public static SyntaxTokenList TokenList(SyntaxToken token) => new(token);

    public static SyntaxTokenList TokenList(params SyntaxToken[] tokens) => new(tokens);

    public static SyntaxTokenList TokenList(IEnumerable<SyntaxToken> tokens) => new(tokens);
    #endregion

    #region 琐碎内容列表
    public static SyntaxTriviaList TriviaList() => default;

    public static SyntaxTriviaList TriviaList(SyntaxTrivia trivia) => new(trivia);

    public static SyntaxTriviaList TriviaList(params SyntaxTrivia[] trivia) => new(trivia);

    public static SyntaxTriviaList TriviaList(IEnumerable<SyntaxTrivia> trivia) => new(trivia);
    #endregion

    #region 带分隔符的列表
    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>() where TNode : ThisSyntaxNode => default;

    public static SeparatedSyntaxList<TNode> SingletonSeparatedList<TNode>(TNode node) where TNode : ThisSyntaxNode => new(new SyntaxNodeOrTokenList(node, index: 0));

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode>? nodes) where TNode : ThisSyntaxNode
    {
        if (nodes is null) return SeparatedList<TNode>();

        var collection = nodes as ICollection<TNode>;
        if (collection is not null && collection.Count == 0) return default;

        using var enumerator = nodes.GetEnumerator();
        if (!enumerator.MoveNext()) return default;

        var firstNode = enumerator.Current;
        if (!enumerator.MoveNext())
            return SingletonSeparatedList(firstNode);

        var builder = new SeparatedSyntaxListBuilder<TNode>(collection?.Count ?? 3);
        builder.Add(firstNode);
        var commaToken = Token(SyntaxKind.CommaToken);
        do
        {
            builder.AddSeparator(commaToken);
            builder.Add(enumerator.Current);
        }
        while (enumerator.MoveNext());

        return builder.ToList();
    }

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<TNode>? nodes, IEnumerable<SyntaxToken>? separators) where TNode : ThisSyntaxNode
    {
        if (nodes is null)
        {
            if (separators is null)
                return SeparatedList<TNode>();
            else
                throw new ArgumentException(string.Format(LunaResources.ArgMustBeNullWhenArgIsNull, nameof(nodes), nameof(separators)), nameof(separators));
        }

        var enumerator = nodes.GetEnumerator();
        var builder = SeparatedSyntaxListBuilder<TNode>.Create();
        if (separators is not null)
        {
            foreach (var token in separators)
            {
                if (!enumerator.MoveNext())
                    throw new ArgumentException(string.Format(LunaResources.ArgMustNotBeEmpty, nameof(nodes)), nameof(nodes));

                builder.Add(enumerator.Current);
                builder.AddSeparator(token);
            }
        }

        if (enumerator.MoveNext())
        {
            builder.Add(enumerator.Current);
            if (enumerator.MoveNext())
                throw new ArgumentException(string.Format(LunaResources.ArgMustHaveOneFewerElementThanArg, nameof(separators), nameof(nodes)), nameof(separators));
        }

        return builder.ToList();
    }

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(IEnumerable<SyntaxNodeOrToken> nodesAndTokens) where TNode : ThisSyntaxNode =>
        SeparatedList<TNode>(NodeOrTokenList(nodesAndTokens));

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxNodeOrTokenList nodesAndTokens) where TNode : ThisSyntaxNode
    {
        if (!HasSeparatedNodeTokenPattern())
            throw new ArgumentException(CodeAnalysisResources.NodeOrTokenOutOfSequence);

        if (!NodesAreCorrectType())
            throw new ArgumentException(CodeAnalysisResources.UnexpectedTypeOfNodeInList);

        return new SeparatedSyntaxList<TNode>(nodesAndTokens);

        bool HasSeparatedNodeTokenPattern()
        {
            for (int i = 0, n = nodesAndTokens.Count; i < n; i++)
            {
                var element = nodesAndTokens[i];
                if (element.IsToken == ((i % 2) == 0))
                    return false;
            }

            return true;
        }

        bool NodesAreCorrectType()
        {
            for (int i = 0, n = nodesAndTokens.Count; i < n; i++)
            {
                var element = nodesAndTokens[i];
                if (element.IsNode && element.AsNode() is not TNode)
                    return false;
            }

            return true;
        }
    }
    #endregion

    #region 节点或标记列表
    public static SyntaxNodeOrTokenList NodeOrTokenList() => default;

    public static SyntaxNodeOrTokenList NodeOrTokenList(IEnumerable<SyntaxNodeOrToken> nodesAndTokens) => new(nodesAndTokens);

    public static SyntaxNodeOrTokenList NodeOrTokenList(params SyntaxNodeOrToken[] nodesAndTokens) => new(nodesAndTokens);
    #endregion

    #region 语法树
    public static SyntaxTree SyntaxTree(
        SyntaxNode root,
        ParseOptions? options = null,
        string path = "",
        Encoding? encoding = null) =>
        SyntaxTree((ThisSyntaxNode)root, (ThisParseOptions?)options, path, encoding);

    public static SyntaxTree SyntaxTree(
        ThisSyntaxNode root,
        ThisParseOptions? options = null,
        string path = "",
        Encoding? encoding = null) =>
        ThisSyntaxTree.Create(
            root,
            options,
            path,
            encoding);
    #endregion

    #region 相等判断
    /// <inheritdoc cref="SyntaxFactory.AreEquivalent(ThisSyntaxTree?, ThisSyntaxTree?, bool)"/>
    public static bool AreEquivalent(SyntaxTree? oldTree, SyntaxTree? newTree, bool topLevel) => AreEquivalent((ThisSyntaxTree?)oldTree, (ThisSyntaxTree?)newTree, topLevel);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent(ThisSyntaxTree?, ThisSyntaxTree?, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent(ThisSyntaxTree? oldTree, ThisSyntaxTree? newTree, bool topLevel) =>
        SyntaxEquivalence.AreEquivalent(oldTree, newTree, ignoreChildNode: null, topLevel: topLevel);

    /// <inheritdoc cref="SyntaxFactory.AreEquivalent(ThisSyntaxNode?, ThisSyntaxNode?, bool)"/>
    public static bool AreEquivalent(SyntaxNode? oldNode, SyntaxNode? newNode, bool topLevel) => AreEquivalent((ThisSyntaxNode?)oldNode, (ThisSyntaxNode?)newNode, topLevel);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent(ThisSyntaxNode?, ThisSyntaxNode?, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent(ThisSyntaxNode? oldNode, ThisSyntaxNode? newNode, bool topLevel) =>
        SyntaxEquivalence.AreEquivalent(oldNode, newNode, ignoreChildNode: null, topLevel: topLevel);

    /// <inheritdoc cref="SyntaxFactory.AreEquivalent(ThisSyntaxNode?, ThisSyntaxNode?, Func{SyntaxKind, bool}?)"/>
    public static bool AreEquivalent(SyntaxNode? oldNode, SyntaxNode? newNode, Func<SyntaxKind, bool>? ignoreChildNode = null) => AreEquivalent((ThisSyntaxNode?)oldNode, (ThisSyntaxNode?)newNode, ignoreChildNode);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent(ThisSyntaxNode?, ThisSyntaxNode?, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent(ThisSyntaxNode? oldNode, ThisSyntaxNode? newNode, Func<SyntaxKind, bool>? ignoreChildNode = null) =>
        SyntaxEquivalence.AreEquivalent(oldNode, newNode, ignoreChildNode: ignoreChildNode, topLevel: false);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent(SyntaxToken, SyntaxToken)"/>
    public static bool AreEquivalent(SyntaxToken oldToken, SyntaxToken newToken) =>
        SyntaxEquivalence.AreEquivalent(oldToken, newToken);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent(SyntaxTokenList, SyntaxTokenList)"/>
    public static bool AreEquivalent(SyntaxTokenList oldList, SyntaxTokenList newList) =>
        SyntaxEquivalence.AreEquivalent(oldList, newList);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent{TNode}(SyntaxList{TNode}, SyntaxList{TNode}, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent<TNode>(SyntaxList<TNode> oldList, SyntaxList<TNode> newList, bool topLevel)
        where TNode : ThisSyntaxNode =>
        SyntaxEquivalence.AreEquivalent(oldList, newList, ignoreChildNode: null, topLevel: topLevel);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent{TNode}(SyntaxList{TNode}, SyntaxList{TNode}, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent<TNode>(SyntaxList<TNode> oldList, SyntaxList<TNode> newList, Func<SyntaxKind, bool>? ignoreChildNode = null)
        where TNode : ThisSyntaxNode =>
        SyntaxEquivalence.AreEquivalent(oldList, newList, ignoreChildNode: ignoreChildNode, topLevel: false);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent{TNode}(SeparatedSyntaxList{TNode}, SeparatedSyntaxList{TNode}, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent<TNode>(SeparatedSyntaxList<TNode> oldList, SeparatedSyntaxList<TNode> newList, bool topLevel)
        where TNode : ThisSyntaxNode =>
        SyntaxEquivalence.AreEquivalent(oldList, newList, ignoreChildNode: null, topLevel: topLevel);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent{TNode}(SeparatedSyntaxList{TNode}, SeparatedSyntaxList{TNode}, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent<TNode>(SeparatedSyntaxList<TNode> oldList, SeparatedSyntaxList<TNode> newList, Func<SyntaxKind, bool>? ignoreChildNode = null)
        where TNode : ThisSyntaxNode =>
        SyntaxEquivalence.AreEquivalent(oldList, newList, ignoreChildNode: ignoreChildNode, topLevel: false);
    #endregion
}
