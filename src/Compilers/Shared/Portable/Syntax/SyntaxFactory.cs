﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

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
    public static SyntaxTrivia CarriageReturnLineFeed { get; } = ThisInternalSyntaxFactory.CarriageReturnLineFeed;
    /// <summary>
    /// 获取包含回车符和换行符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的可变的语法琐碎内容，其包含回车符和换行符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticCarriageReturnLineFeed { get; } = ThisInternalSyntaxFactory.ElasticCarriageReturnLineFeed;

    /// <summary>
    /// 获取包含换行符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的语法琐碎内容，其包含单个换行符。
    /// </value>
    public static SyntaxTrivia LineFeed { get; } = ThisInternalSyntaxFactory.LineFeed;
    /// <summary>
    /// 获取包含换行符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的可变的语法琐碎内容，其包含单个换行符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticLineFeed { get; } = ThisInternalSyntaxFactory.ElasticLineFeed;

    /// <summary>
    /// 获取包含回车符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的语法琐碎内容，其包含单个回车符。
    /// </value>
    public static SyntaxTrivia CarriageReturn { get; } = ThisInternalSyntaxFactory.CarriageReturn;
    /// <summary>
    /// 获取包含回车符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.EndOfLineTrivia"/>的可变的语法琐碎内容，其包含单个回车符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticCarriageReturn { get; } = ThisInternalSyntaxFactory.ElasticCarriageReturn;

    /// <summary>
    /// 获取包含空格符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhitespaceTrivia"/>的语法琐碎内容，其包含单个空格符。
    /// </value>
    public static SyntaxTrivia Space { get; } = ThisInternalSyntaxFactory.Space;
    /// <summary>
    /// 获取包含空格符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhitespaceTrivia"/>的可变的语法琐碎内容，其包含单个空格符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticSpace { get; } = ThisInternalSyntaxFactory.ElasticSpace;

    /// <summary>
    /// 获取包含制表符的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhitespaceTrivia"/>的语法琐碎内容，其包含单个制表符。
    /// </value>
    public static SyntaxTrivia Tab { get; } = ThisInternalSyntaxFactory.Tab;
    /// <summary>
    /// 获取包含制表符的可变的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhitespaceTrivia"/>的可变的语法琐碎内容，其包含单个制表符。
    /// </value>
    /// <remarks>
    /// 可变的语法琐碎内容用于表示那些不是从解析代码文本过程中产生的琐碎内容，它们一般在格式化时不会被保留。
    /// </remarks>
    public static SyntaxTrivia ElasticTab { get; } = ThisInternalSyntaxFactory.ElasticTab;

    /// <summary>
    /// 获取表示可变记号的语法琐碎内容。
    /// </summary>
    /// <value>
    /// 一个语法类型为<see cref="SyntaxKind.WhitespaceTrivia"/>的可变的语法琐碎内容，其不包含任何字符。
    /// </value>
    /// <remarks>
    /// 当语法琐碎内容没有明确时，工厂方法将自动置入可变记号。在语法格式化阶段，可变记号将会被替换为合适的语法琐碎内容。
    /// </remarks>
    public static SyntaxTrivia ElasticMarker { get; } = ThisInternalSyntaxFactory.ElasticZeroSpace;

    #region 琐碎内容
    public static SyntaxTrivia EndOfLine(string text) => ThisInternalSyntaxFactory.EndOfLine(text, elastic: false);

    public static SyntaxTrivia ElasticEndOfLine(string text) => ThisInternalSyntaxFactory.EndOfLine(text, elastic: true);

    public static SyntaxTrivia Whitespace(string text) => ThisInternalSyntaxFactory.Whitespace(text, elastic: false);

    public static SyntaxTrivia ElasticWhitespace(string text) => ThisInternalSyntaxFactory.Whitespace(text, elastic: true);

    public static SyntaxTrivia Comment(string text) => ThisInternalSyntaxFactory.Comment(text);

    public static SyntaxTrivia PreprocessingMessage(string text) => ThisInternalSyntaxFactory.PreprocessingMessage(text);

    public static SyntaxTrivia Trivia(Syntax.StructuredTriviaSyntax node) => new(default, node.Green, position: 0, index: 0);

    /// <summary>
    /// Trivia nodes represent parts of the program text that are not parts of the
    /// syntactic grammar, such as spaces, newlines, comments, preprocessor
    /// directives, and disabled code.
    /// </summary>
    /// <param name="kind">
    /// A <see cref="SyntaxKind"/> representing the specific kind of <see cref="SyntaxTrivia"/>. One of
    /// <see cref="SyntaxKind.WhitespaceTrivia"/>, <see cref="SyntaxKind.EndOfLineTrivia"/>,
    /// <see cref="SyntaxKind.SingleLineCommentTrivia"/>, <see cref="SyntaxKind.MultiLineCommentTrivia"/>,
    /// <see cref="SyntaxKind.DocumentationCommentExteriorTrivia"/>, <see cref="SyntaxKind.DisabledTextTrivia"/>
    /// </param>
    /// <param name="text">
    /// The actual text of this token.
    /// </param>
    public static SyntaxTrivia SyntaxTrivia(SyntaxKind kind, string text)
    {
        if (text is null)
            throw new ArgumentNullException(nameof(text));

        switch (kind)
        {
            case SyntaxKind.DisabledTextTrivia:
            case SyntaxKind.EndOfLineTrivia:
            case SyntaxKind.MultiLineCommentTrivia:
            case SyntaxKind.SingleLineCommentTrivia:
            case SyntaxKind.WhitespaceTrivia:
                return new SyntaxTrivia(default, new Syntax.InternalSyntax.SyntaxTrivia(kind, text, null, null), 0, 0);
            default:
                throw new ArgumentException("kind");
        }
    }
    #endregion

    #region 标记
    public static SyntaxToken Token(SyntaxKind kind) =>
        new(ThisInternalSyntaxFactory.Token(
            ElasticMarker.UnderlyingNode,
            kind,
            ElasticMarker.UnderlyingNode));

    public static SyntaxToken Token(
        SyntaxTriviaList leading,
        SyntaxKind kind,
        SyntaxTriviaList trailing) =>
        new(ThisInternalSyntaxFactory.Token(
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

        return new(ThisInternalSyntaxFactory.Token(
            leading.Node,
            kind,
            text,
            valueText,
            trailing.Node));
    }

    private static partial void ValidateTokenKind(SyntaxKind kind);

    public static SyntaxToken MissingToken(SyntaxKind kind) =>
        new(ThisInternalSyntaxFactory.MissingToken(
            ElasticMarker.UnderlyingNode,
            kind,
            ElasticMarker.UnderlyingNode));

    public static SyntaxToken MissingToken(
        SyntaxTriviaList leading,
        SyntaxKind kind,
        SyntaxTriviaList trailing) =>
        new(ThisInternalSyntaxFactory.MissingToken(
            leading.Node,
            kind,
            trailing.Node));

    public static SyntaxToken BadToken(
        SyntaxTriviaList leading,
        string text,
        SyntaxTriviaList trailing) =>
        new(ThisInternalSyntaxFactory.BadToken(
            leading.Node,
            text,
            trailing.Node));

    #region 标识符
    public static SyntaxToken Identifier(string text) =>
        new(ThisInternalSyntaxFactory.Identifier(
            ElasticMarker.UnderlyingNode,
            text,
            ElasticMarker.UnderlyingNode));

    public static SyntaxToken Identifier(
        SyntaxTriviaList leading,
        string text,
        SyntaxTriviaList trailing) =>
        new(ThisInternalSyntaxFactory.Identifier(
            leading.Node,
            text,
            trailing.Node));

    public static SyntaxToken Identifier(
        SyntaxTriviaList leading,
        SyntaxKind contextualKind,
        string text,
        string valueText,
        SyntaxTriviaList trailing) =>
        new(ThisInternalSyntaxFactory.Identifier(
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
    /// <inheritdoc cref="AreEquivalent(ThisSyntaxTree?, ThisSyntaxTree?, bool)"/>
    public static bool AreEquivalent(SyntaxTree? oldTree, SyntaxTree? newTree, bool topLevel) => AreEquivalent((ThisSyntaxTree?)oldTree, (ThisSyntaxTree?)newTree, topLevel);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent(ThisSyntaxTree?, ThisSyntaxTree?, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent(ThisSyntaxTree? oldTree, ThisSyntaxTree? newTree, bool topLevel) =>
        SyntaxEquivalence.AreEquivalent(oldTree, newTree, ignoreChildNode: null, topLevel: topLevel);

    /// <inheritdoc cref="AreEquivalent(ThisSyntaxNode?, ThisSyntaxNode?, bool)"/>
    public static bool AreEquivalent(SyntaxNode? oldNode, SyntaxNode? newNode, bool topLevel) => AreEquivalent((ThisSyntaxNode?)oldNode, (ThisSyntaxNode?)newNode, topLevel);

    /// <inheritdoc cref="SyntaxEquivalence.AreEquivalent(ThisSyntaxNode?, ThisSyntaxNode?, Func{SyntaxKind, bool}?, bool)"/>
    public static bool AreEquivalent(ThisSyntaxNode? oldNode, ThisSyntaxNode? newNode, bool topLevel) =>
        SyntaxEquivalence.AreEquivalent(oldNode, newNode, ignoreChildNode: null, topLevel: topLevel);

    /// <inheritdoc cref="AreEquivalent(ThisSyntaxNode?, ThisSyntaxNode?, Func{SyntaxKind, bool}?)"/>
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
