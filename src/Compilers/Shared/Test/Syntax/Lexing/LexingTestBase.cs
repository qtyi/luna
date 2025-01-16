// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Xunit;
using Xunit.Abstractions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Luna.Test.Utilities;

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
using Microsoft.CodeAnalysis.Test.Utilities;

public abstract partial class LexingTestBase : ThisTestBase
{
    public static readonly TheoryData<LanguageVersion, SourceCodeKind> EffectiveLanguageVersionsAndSourceCodeKind =
        LanguageVersionTests.EffectiveLanguageVersions.Combine(new EnumTheoryData<SourceCodeKind>());

    protected virtual ITestOutputHelper? Output => null;

    protected static NodeValidator LexSource(string source, ThisParseOptions? options = null, bool withTrivia = false) => LexSource(SourceText.From(source), options, withTrivia);

    private static NodeValidator LexSource(SourceText sourceText, ThisParseOptions? options = null, bool withTrivia = false) => CreateValidator(sourceText, options, withTrivia).NextNode;

    private static ValidatorImpl CreateValidator(SourceText sourceText, ThisParseOptions? options = null, bool withTrivia = false, ITestOutputHelper? output = null)
    {
        var lexer = new Lexer(sourceText, options ?? TestOptions.RegularDefault);
        return new ValidatorImpl(lexer, withTrivia: withTrivia, intoStructuredTrivia: true, output: output);
    }

    protected static void ValidateLiteral<T>(string source, SyntaxKind kind, T? value, DiagnosticDescription[]? diagnostics = null, ThisParseOptions? options = null)
    {
        var V = LexSource(source, options).EndOfFile();
        V(kind, value: value, diagnostics: diagnostics ?? []);
    }

    protected static void ValidateUtf8StringLiteral(string source, SyntaxKind kind, string? value, DiagnosticDescription[]? diagnostics = null, ThisParseOptions? options = null)
        => ValidateUtf8StringLiteral(source, kind, value is null ? null : new Utf8String(Encoding.UTF8.GetBytes(value)), diagnostics, options);

    protected static void ValidateUtf8StringLiteral(string source, SyntaxKind kind, Utf8String? value, DiagnosticDescription[]? diagnostics = null, ThisParseOptions? options = null)
        => ValidateLiteral(source, kind, value, diagnostics, options);

    protected void Print(string source, ThisParseOptions? options = null, bool withTrivia = false) => Print(SourceText.From(source), options, withTrivia);

    protected void Print(SourceText sourceText, ThisParseOptions? options = null, bool withTrivia = false)
    {
        var output = Output;
        if (output is null) return;

        var validator = CreateValidator(sourceText, options, withTrivia, output: output);
        while (validator.TryWriteNextValidation()) { }
    }
}
