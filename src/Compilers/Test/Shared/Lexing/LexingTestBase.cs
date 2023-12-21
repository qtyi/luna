// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis.Text;
using Xunit.Abstractions;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;

using ThisParseOptions = LuaParseOptions;
using ThisTestBase = Test.Utilities.LuaTestBase;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests.Lexing;

using ThisParseOptions = MoonScriptParseOptions;
using ThisTestBase = Test.Utilities.MoonScriptTestBase;
#endif

using Syntax.InternalSyntax;
using Test.Utilities;

public abstract partial class LexingTestBase : ThisTestBase
{
    protected virtual ITestOutputHelper? Output => null;

    protected static NodeValidator LexSource(string source, ThisParseOptions? options = null, bool withTrivia = false) => LexSource(SourceText.From(source), options, withTrivia);

    private static NodeValidator LexSource(SourceText sourceText, ThisParseOptions? options = null, bool withTrivia = false)
    {
        var lexer = new Lexer(sourceText, options ?? TestOptions.RegularDefault);
        var validator = new ValidatorImpl(lexer, withTrivia: withTrivia, intoStructuredTrivia: true);
        return validator.NextNode;
    }

    protected static void ValidateLiteral<T>(string source, SyntaxKind kind, T? value, ThisParseOptions? options = null)
    {
        var V = LexSource(source, options).EndOfFile();
        V(kind, value: value);
    }

    protected static void ValidateUtf8StringLiteral(string source, SyntaxKind kind, string? value, ThisParseOptions? options = null)
        => ValidateLiteral<object>(source, kind, value is null ? null : Encoding.UTF8.GetBytes(value).ToImmutableArray(), options);

    protected static void ValidateUtf8StringLiteral(string source, SyntaxKind kind, byte[]? value, ThisParseOptions? options = null)
        => ValidateLiteral<object>(source, kind, value is null ? null : value.ToImmutableArray(), options);

    protected void Print(string source, ThisParseOptions? options = null, bool withTrivia = false)
    {
        var output = this.Output;
        if (output is null) return;

        var lexer = new Lexer(SourceText.From(source), options ?? TestOptions.RegularDefault);
        var validator = new ValidatorImpl(lexer, withTrivia: withTrivia, intoStructuredTrivia: true, output: output);
        while (validator.TryWriteNextValidation()) { }
    }
}
