// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
#endif

using Microsoft.CodeAnalysis.Syntax.InternalSyntax;

/// <summary>
/// 此类型提供构造各种内部的语法节点、标记和琐碎内容的工厂方法。
/// </summary>
internal static partial class SyntaxFactory
{
    /// <summary>表示回车符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia CarriageReturn = SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\r");
    /// <summary>表示可变的回车符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia ElasticCarriageReturn = CarriageReturn.AsElastic();

    /// <summary>表示换行符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia LineFeed = SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\n");
    /// <summary>表示可变的换行符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia ElasticLineFeed = LineFeed.AsElastic();

    /// <summary>表示回车换行符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia CarriageReturnLineFeed = SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, "\r\n");
    /// <summary>表示可变的回车换行符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia ElasticCarriageReturnLineFeed = CarriageReturnLineFeed.AsElastic();

    /// <summary>表示垂直制表符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia VerticalTab = SyntaxTrivia.Create(SyntaxKind.WhiteSpaceTrivia, "\v");
    /// <summary>表示可变的垂直制表符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia ElasticVerticalTab = VerticalTab.AsElastic();

    /// <summary>表示换页符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia FormFeed = SyntaxTrivia.Create(SyntaxKind.WhiteSpaceTrivia, "\f");
    /// <summary>表示可变的换页符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia ElasticFormFeed = FormFeed.AsElastic();

    /// <summary>表示空格符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia Space = SyntaxTrivia.Create(SyntaxKind.WhiteSpaceTrivia, " ");
    /// <summary>表示可变的空格符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia ElasticSpace = Space.AsElastic();

    /// <summary>表示制表符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia Tab = SyntaxTrivia.Create(SyntaxKind.WhiteSpaceTrivia, "\t");
    /// <summary>表示可变的制表符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia ElasticTab = Tab.AsElastic();

    /// <summary>表示可变的零空格符的语法琐碎内容。</summary>
    internal static readonly SyntaxTrivia ElasticZeroSpace = SyntaxTrivia.Create(SyntaxKind.WhiteSpaceTrivia, string.Empty).AsElastic();

    /// <summary>
    /// 将语法琐碎内容转换为可变的。
    /// </summary>
    /// <param name="trivia">要转换的语法琐碎内容。</param>
    /// <returns>可变的语法琐碎内容。</returns>
    internal static SyntaxTrivia AsElastic(this SyntaxTrivia trivia) => trivia.WithAnnotationsGreen(new[] { SyntaxAnnotation.ElasticAnnotation });

    /// <summary>
    /// 构造表示行末的内部语法琐碎内容。
    /// </summary>
    /// <param name="text">表示行末的字符串。</param>
    /// <param name="elastic">生成的语法琐碎内容是否为可变的。</param>
    /// <returns>表示行末的内部语法琐碎内容。</returns>
    internal static SyntaxTrivia EndOfLine(string text, bool elastic = false) =>
        text switch
        {
            "\r" => elastic ? ElasticCarriageReturn : CarriageReturn,
            "\n" => elastic ? ElasticLineFeed : LineFeed,
            "\r\n" => elastic ? ElasticCarriageReturnLineFeed : CarriageReturnLineFeed,
            _ => elastic switch
            {
                false => SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, text),
                true => SyntaxTrivia.Create(SyntaxKind.EndOfLineTrivia, text).AsElastic()
            }
        };

    /// <summary>
    /// 构造表示空白内容的内部语法琐碎内容。
    /// </summary>
    /// <param name="text">表示空白内容的字符串。</param>
    /// <param name="elastic">生成的语法琐碎内容是否为可变的。</param>
    /// <returns>表示空白内容的内部语法琐碎内容。</returns>
    internal static SyntaxTrivia WhiteSpace(string text, bool elastic = false) =>
        text switch
        {
            " " => elastic ? ElasticSpace : Space,
            "\t" => elastic ? ElasticTab : Tab,
            "\v" => elastic ? ElasticVerticalTab : Tab,
            "\f" => elastic ? ElasticFormFeed : FormFeed,
            "" => elastic ? ElasticZeroSpace : SyntaxTrivia.Create(SyntaxKind.WhiteSpaceTrivia, text),
            _ => elastic switch
            {
                false => SyntaxTrivia.Create(SyntaxKind.WhiteSpaceTrivia, text),
                true => SyntaxTrivia.Create(SyntaxKind.WhiteSpaceTrivia, text).AsElastic()
            }
        };

    /// <summary>
    /// 构造表示注释的内部语法琐碎内容。
    /// </summary>
    /// <param name="text">表示注释的字符串。</param>
    /// <returns>表示注释的内部语法琐碎内容。</returns>
    internal static SyntaxTrivia Comment(string text)
    {
        // 检测text是否为多行注释的格式（“--[”与“[”之间间隔零个或复数个“=”）。
        if (text.Length > 2 && text[2] == '[')
        {
            for (var i = 3; i < text.Length; i++)
            {
                if (text[i] == '[')
                    return SyntaxTrivia.Create(SyntaxKind.MultiLineCommentTrivia, text);
                else if (text[i] == '=')
                    continue;
                else // 不符合左长方括号的结构，判定为非多行注释。
                    break;
            }
        }

        // 否则text为单行注释。
        return SyntaxTrivia.Create(SyntaxKind.SingleLineCommentTrivia, text);
    }

    internal static SyntaxTrivia PreprocessingMessage(string text) => SyntaxTrivia.Create(SyntaxKind.PreprocessingMessageTrivia, text);

#if DEBUG
    internal static ThisInternalSyntaxNode Mock() => new ThisInternalSyntaxNode.MockNode();
#endif

    public static SyntaxToken Token(SyntaxKind kind) => SyntaxToken.Create(kind);

    internal static SyntaxToken Token(GreenNode? leading, SyntaxKind kind, GreenNode? trailing) => SyntaxToken.Create(kind, leading, trailing);

    internal static SyntaxToken Token(GreenNode? leading, SyntaxKind kind, string text, string valueText, GreenNode? trailing)
    {
        ValidateTokenKind(kind); // 检查不接受的语法分类。

        var defaultText = SyntaxFacts.GetText(kind);
        return kind >= SyntaxToken.FirstTokenWithWellKnownText && kind <= SyntaxToken.LastTokenWithWellKnownText && text == defaultText && valueText == defaultText
            ? Token(leading, kind, trailing)
            : SyntaxToken.WithValue(kind, leading, text, valueText, trailing);
    }

    [Conditional("DEBUG")]
    private static partial void ValidateTokenKind(SyntaxKind kind);

    internal static SyntaxToken MissingToken(SyntaxKind kind) => SyntaxToken.CreateMissing(kind, null, null);

    internal static SyntaxToken MissingToken(GreenNode? leading, SyntaxKind kind, GreenNode? trailing) => SyntaxToken.CreateMissing(kind, leading, trailing);

    internal static SyntaxToken Identifier(string text) => Identifier(SyntaxKind.IdentifierToken, null, text, text, null);

    internal static SyntaxToken Identifier(GreenNode? leading, string text, GreenNode? trailing) => Identifier(SyntaxKind.IdentifierToken, leading, text, text, trailing);

    internal static SyntaxToken Identifier(SyntaxKind contextualKind, GreenNode? leading, string text, string valueText, GreenNode? trailing) => SyntaxToken.Identifier(contextualKind, leading, text, valueText, trailing);

    internal static SyntaxToken Literal(GreenNode? leading, string text, long value, GreenNode? trailing) => SyntaxToken.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);

    internal static SyntaxToken Literal(GreenNode? leading, string text, ulong value, GreenNode? trailing) => SyntaxToken.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);

    internal static SyntaxToken Literal(GreenNode? leading, string text, double value, GreenNode? trailing) => SyntaxToken.WithValue(SyntaxKind.NumericLiteralToken, leading, text, value, trailing);

    internal static SyntaxToken BadToken(GreenNode? leading, string text, GreenNode? trailing) => SyntaxToken.WithValue(SyntaxKind.BadToken, leading, text, text, trailing);

    internal static SyntaxToken Literal(GreenNode? leading, string text, string value, GreenNode? trailing) => SyntaxToken.WithValue(SyntaxKind.StringLiteralToken, leading, text, value, trailing);

    internal static SyntaxToken Literal(GreenNode? leading, string text, SyntaxKind kind, ImmutableArray<byte> value, GreenNode? trailing) => SyntaxToken.WithValue(kind, leading, text, value, trailing);

    #region SyntaxKind到SyntaxToken的转换方法
    // 各种语法部分的转换方法在各语言的独立项目中定义
    #endregion

    #region 节点列表
    public static SyntaxList<TNode> List<TNode>() where TNode : ThisInternalSyntaxNode => default;

    public static SyntaxList<TNode> List<TNode>(TNode node) where TNode : ThisInternalSyntaxNode => new(SyntaxList.List(node));

    public static SyntaxList<TNode> List<TNode>(TNode node0, TNode node1) where TNode : ThisInternalSyntaxNode => new(SyntaxList.List(node0, node1));

    internal static GreenNode ListNode(ThisInternalSyntaxNode node0, ThisInternalSyntaxNode node1) => SyntaxList.List(node0, node1);

    public static SyntaxList<TNode> List<TNode>(TNode node0, TNode node1, TNode node2) where TNode : ThisInternalSyntaxNode => new(SyntaxList.List(node0, node1, node2));

    internal static GreenNode ListNode(ThisInternalSyntaxNode node0, ThisInternalSyntaxNode node1, ThisInternalSyntaxNode node2) => SyntaxList.List(node0, node1, node2);

    public static SyntaxList<TNode> List<TNode>(params TNode[] nodes) where TNode : ThisInternalSyntaxNode => nodes is null ? default : new(SyntaxList.List(nodes));

    internal static GreenNode ListNode(params ArrayElement<GreenNode>[] nodes) => SyntaxList.List(nodes);
    #endregion

    #region 间隔的节点列表
    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(TNode node) where TNode : ThisInternalSyntaxNode => new(new SyntaxList<ThisInternalSyntaxNode>(node));

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(SyntaxToken token) where TNode : ThisInternalSyntaxNode => new(new SyntaxList<ThisInternalSyntaxNode>(token));

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(TNode node1, SyntaxToken token, TNode node2) where TNode : ThisInternalSyntaxNode => new(new SyntaxList<ThisInternalSyntaxNode>(SyntaxList.List(node1, token, node2)));

    public static SeparatedSyntaxList<TNode> SeparatedList<TNode>(params ThisInternalSyntaxNode[] nodes) where TNode : ThisInternalSyntaxNode => nodes is null ? default : new(SyntaxList.List(nodes));

    internal static partial IEnumerable<SyntaxTrivia> GetWellKnownTrivia();

    internal static IEnumerable<SyntaxToken> GetWellKnownTokens() => SyntaxToken.GetWellKnownTokens();
    #endregion
}
