// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxTree = LuaSyntaxTree;
using ThisParseOptions = LuaParseOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxTree = MoonScriptSyntaxTree;
using ThisParseOptions = MoonScriptParseOptions;
#endif

using Syntax.InternalSyntax;
using SyntaxToken = Microsoft.CodeAnalysis.SyntaxToken;

static partial class SyntaxFactory
{
    private static SourceText MakeSourceText(string text, int offset) => SourceText.From(text).GetSubText(offset);

    private static Lexer MakeLexer(string text, int offset, ThisParseOptions? options = null) => new(SyntaxFactory.MakeSourceText(text, offset), options ?? ThisParseOptions.Default);

    private static LanguageParser MakeParser(Lexer lexer) => new(lexer, oldTree: null, changes: null);

    public static SyntaxTree ParseSyntaxTree(
        SourceText text,
        ParseOptions? options,
        string path,
        CancellationToken cancellationToken) =>
        SyntaxFactory.ParseSyntaxTree(text, (ThisParseOptions?)options, path, cancellationToken);

    public static SyntaxTree ParseSyntaxTree(
        SourceText text,
        ThisParseOptions? options,
        string path,
        CancellationToken cancellationToken) =>
        ThisSyntaxTree.ParseText(text, options, path, cancellationToken);

    public static SyntaxTriviaList ParseLeadingTrivia(string text, ThisParseOptions? options = null, int offset = 0)
    {
        using var lexer = new Lexer(SyntaxFactory.MakeSourceText(text, offset), options ?? ThisParseOptions.Default);
        return lexer.LexSyntaxLeadingTrivia();
    }

    public static SyntaxTriviaList ParseTrailingTrivia(string text, ThisParseOptions? options = null, int offset = 0)
    {
        using var lexer = new Lexer(SyntaxFactory.MakeSourceText(text, offset), options ?? ThisParseOptions.Default);
        return lexer.LexSyntaxTrailingTrivia();
    }

    public static SyntaxToken ParseToken(string text, ThisParseOptions? options = null, int offset = 0)
    {
        using var lexer = new Lexer(SyntaxFactory.MakeSourceText(text, offset), options ?? ThisParseOptions.Default);
        return new SyntaxToken(lexer.Lex(LexerMode.Syntax));
    }

    public static IEnumerable<SyntaxToken> ParseTokens(string text, ThisParseOptions? options = null, int offset = 0, int initialTokenPosition = 0)
    {
        using var lexer = new Lexer(SyntaxFactory.MakeSourceText(text, offset), options ?? ThisParseOptions.Default);
        var position = initialTokenPosition;
        while (true)
        {
            var green = lexer.Lex(LexerMode.Syntax);
            // 创建红树标志并枚举。
            foreach (var token in SyntaxFactory.ParseTokens(green, position))
            {
                yield return token;
                position += token.FullWidth;
            }

            if (green.Kind == SyntaxKind.EndOfFileToken) yield break;
        }
    }

    /// <summary>
    /// 从表示标志的绿树节点中创建一个或多个红树标志。
    /// </summary>
    /// <param name="green">要处理的表示标志的绿树节点。</param>
    /// <param name="position"><paramref name="green"/>的起始位置。</param>
    /// <returns>从<paramref name="green"/>中创建一个或多个红树标志。</returns>
    internal static partial IEnumerable<SyntaxToken> ParseTokens(Syntax.InternalSyntax.SyntaxToken green, int position);
}
