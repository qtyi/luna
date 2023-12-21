using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Xunit;

namespace Qtyi.CodeAnalysis.MoonScript.UnitTests.Lexing;

using Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

public partial class LexerTests
{
    internal static readonly MoonScriptParseOptions DefaultParseOptions = new(languageVersion: LanguageVersion.Preview);

    #region 正向测试
    public void NumericLiteralLexTests()
    {
        LiteralLexTest(long.MaxValue.ToString(), long.MaxValue);
        LiteralLexTest(long.MinValue.ToString(), 0x8000000000000000UL); // 由于long.MinValue取负后的数字超过long.MinValue，因此返回的类型是ulong。
        LiteralLexTest(double.MaxValue.ToString("G17"), double.MaxValue); // 由于精度影响，浮点数转换为字符串时会四舍五入，因此可能会导致从字符串转型回浮点数时不相等，甚至超出最大/最小值导致抛出错误。
        Assert.Equal(double.MinValue.ToString("G17").Substring(1), double.MaxValue.ToString("G17"));
        LiteralLexTest(double.MinValue.ToString("G17"), double.MaxValue);

        var value = 31.4568156151E-45;
        var hexValue = value.ToHexString();
        LiteralLexTest(hexValue, value); // 十六进制浮点数。

        LiteralLexTest("31415", 31415L); // 正常十进制整数，long类型。
        LiteralLexTest("0x31415ABCD", 0x31415ABCD); // 正常十六进制整数，long类型。
        LiteralLexTest("31.415", 31.415D); // 正常十进制浮点数，double类型。
        LiteralLexTest(".314", 0.314D); // 整数部分缺失。
        LiteralLexTest("314.", 314.0D); // 小数部分缺失。
    }

    public void StringLiteralLexTests()
    {
        LiteralLexTest("""   '\a\b\f\n\r\t\v\\\"\''   """, "\a\b\f\n\r\t\v\\\"\'"); // 基本转义字符。

        LiteralLexTest("""   "as',\",\'df"   """, "as',\",'df"); // 双引号内包含的单引号可以不转义，但双引号必须转义。
        LiteralLexTest("""   'as",\',\"df'   """, "as\",',\"df"); // 单引号内包含的双引号可以不转义，但单引号必须转义。

        LiteralLexTest("""
            'first line
            
            third line'
            """, "first line\n\nthird line"); // 字面换行。
        LiteralLexTest("""
            'first line
               second line
             third line'
            """, "first line\n  second line\nthird line"); // 换行字面量的缩进量以除第一行外缩进量最小的一行为准。
        LiteralLexTest("""
            '\97o\10\049t23\043456'
            """, "ao\n1t23+456"); // 转义十进制Ascii编码。
        LiteralLexTest("""
            '\x61\x6F\n\x312\xe5\xad\xa63'
            """, "ao\n12学3"); // 转义十六进制UTF-8编码序列。
        LiteralLexTest("""
            '\u{61}o\u{A}\u{0031}t23+\u{00000000000000000000000000005B57}456'
            """, "ao\n1t23+字456"); // 转义十进制Unicode码点。

        LiteralLexTest("""
            [===[a,[b],[[c]],[=[d]=],[==[e]==],[====[f]====],g]===]
            """, "a,[b],[[c]],[=[d]=],[==[e]==],[====[f]====],g"); // 多行原始字符串。
        LiteralLexTest("""
            [===[
             first line
              second line
               ]===]
            """, " first line\n  second line\n   "); // 字面多行，如果第一行没有字符则忽略这行。
    }

    private void InterpolatedStringLiteralLexTest(string source, Range[] interpolationRanges, string[] texts, MoonScriptParseOptions? options = null)
    {
        var lexer = LexerTests.CreateLexer(source, options);

        var token = lexer.Lex(LexerMode.Syntax);
         Assert.True(token.Kind == SyntaxKind.InterpolatedStringLiteralToken, "不是差值字符串标记。");

        Assert.IsNotNull(token.Value);
        var innerTokens = (ImmutableArray<SyntaxToken>)token.Value;

        for (int i = 0, n = innerTokens.Length; i < n; i++)
        {
            var innerToken = innerTokens[i];
            if (i % 2 == 0)
            {
                Assert.Equal(SyntaxKind.InterpolatedStringTextToken, innerToken.Kind);
                Assert.IsInstanceOfType(innerToken.Value, typeof(string));
                var index = i / 2;
                if (index < texts.Length) // 防止索引超出范围。
                    Assert.Equal(texts[index], innerToken.Value);
            }
            else
            {
                Assert.Equal(SyntaxKind.InterpolationToken, innerToken.Kind);
                Assert.IsNotNull(innerToken.Value);
                Assert.IsInstanceOfType(innerToken.Value, typeof(Lexer.Interpolation));
                var interpolation = (Lexer.Interpolation)innerToken.Value;
                var index = (i - 1) / 2;
                if (index < interpolationRanges.Length) // 防止索引超出范围。
                    Assert.Equal(interpolationRanges[index], interpolation.StartRange.Start..interpolation.EndRange.End);
            }
        }
    }

    public void InterpolatedStringTests()
    {
        // 普通插值。
        InterpolatedStringLiteralLexTest("""
            "This is #{"an"} apple and that is #{"a"} banana."
            """,
            new[] { 9..15, 35..40 },
            new[]
            {
                "This is ",
                " apple and that is ",
                " banana."
            });

        // 嵌套插值。
        InterpolatedStringLiteralLexTest("""
            "Outer string #{   "w#{  'r' .. 'a'  }p"   }s the inner string."
            """,
            new[] { 14..43 },
            new[]
            {
                "Outer string ",
                "s the inner string."
            });

        // 多行插值。
        InterpolatedStringLiteralLexTest("""
            "first #{
              if isline
                "line"
              else
                "time"
            } here!"
            """,
            new[] { 7..56 },
            new[]
            {
                "first ",
                " here!"
            });

        // 多行缩进插值。
        // 插值结束符号（右花括号）的缩进量可以为任意，不影响内容；
        // 其他内容除第一行外以缩进量最小值为准进行修剪。
        InterpolatedStringLiteralLexTest("""
            "first #{ "line"
                          } here!
                  second line here!
                third line here!"
            """,
            new[] { 7..32 },
            new[]
            {
                "first ",
                " here!\n  second line here!\nthird line here!"
            });
    }

    public void CommentLexTests()
    {
        var source = """
            --     a single line comment.       
            --[=a=[also a single line comment because of character 'a'.]=a=]
            -- [==[another single line comment because of the space before long bracket.]==]
            --[==[a multiline comment
            that,
            though contains other level of [=====[long brackets]=====], can
            cross
            many lines.
            ]==]     --last single line comment.
            """;
        Lexer lexer = LexerTests.CreateLexer(source);
        var mode = LexerMode.Syntax;
        SyntaxToken token;

        token = lexer.Lex(mode);
        var list = token.LeadingTrivia;
         Assert.True(list.Count == 9);

         Assert.True(list[0]!.Kind == SyntaxKind.SingleLineCommentTrivia);
        Assert.Equal(((SyntaxTrivia)list[0]!).Text, "--     a single line comment.       ");

         Assert.True(list[2]!.Kind == SyntaxKind.SingleLineCommentTrivia);
        Assert.Equal(((SyntaxTrivia)list[2]!).Text, "--[=a=[also a single line comment because of character 'a'.]=a=]");

         Assert.True(list[4]!.Kind == SyntaxKind.SingleLineCommentTrivia);
        Assert.Equal(((SyntaxTrivia)list[4]!).Text, "-- [==[another single line comment because of the space before long bracket.]==]");

         Assert.True(list[6]!.Kind == SyntaxKind.MultiLineCommentTrivia);
        Assert.Equal(((SyntaxTrivia)list[6]!).Text, """
            --[==[a multiline comment
            that,
            though contains other level of [=====[long brackets]=====], can
            cross
            many lines.
            ]==]
            """);

         Assert.True(list[8]!.Kind == SyntaxKind.SingleLineCommentTrivia);
        Assert.Equal(((SyntaxTrivia)list[8]!).Text, "--last single line comment.");
    }
    #endregion
}
