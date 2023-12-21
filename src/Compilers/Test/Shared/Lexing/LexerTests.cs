// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;
using Xunit;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parser;

using ThisParseOptions = LuaParseOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests.Parser;

using ThisParseOptions = MoonScriptParseOptions;
#endif

using Syntax.InternalSyntax;

partial class LexerTests
{
    protected delegate void TokenValidator(SyntaxKind kind, string? text = null, object? value = null);

    internal static Lexer CreateLexer(string source, ThisParseOptions? options = null) => new(SourceText.From(source), options ?? LexerTests.DefaultParseOptions);

    protected static TokenValidator LexSource(string source, ThisParseOptions? options = null) => LexSource(SourceText.From(source), options);

    private static TokenValidator LexSource(SourceText sourceText, ThisParseOptions? options = null)
    {
        var lexer = new Lexer(sourceText, options ?? LexerTests.DefaultParseOptions);

        return (kind, text, value) =>
        {
            var token = lexer.Lex(LexerMode.Syntax);

            Assert.Equal(kind, token.Kind);
            if (text is not null)
                Assert.Equal(text, token.Text);
            if (value is not null)
                Assert.Equal(value, token.Value);
        };
    }

    protected static void LiteralLexTest<T>(string source, SyntaxKind kind, T? value, ThisParseOptions? options = null)
    {
        var TK = LexSource(source, options);

        TK(kind, value: value);
        TK(SyntaxKind.EndOfFileToken);
    }
}
