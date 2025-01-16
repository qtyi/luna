// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using Luna.Test.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Test.Utilities;
using Xunit;
using Xunit.Abstractions;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;

using ICSharpCode.Decompiler.Metadata;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests.Lexing;
#endif

using Test.Utilities;

public partial class PunctuationTests(ITestOutputHelper output) : LexingTestBase
{
    [property: NotNull]
    protected override ITestOutputHelper? Output { get; } = output;

    public static readonly TheoryData<LanguageVersion, SourceCodeKind> ShebangSupportiveLanguageVersionsAndSourceCodeKind =
        LanguageVersionTests.EffectiveLanguageVersions
        .Where(static version => SyntaxFactsTests.GetExpectedSyntaxKindInfos_Punctuation(version).Any(static info => info.Kind == SyntaxKind.HashExclamationToken))
        .Combine(new EnumTheoryData<SourceCodeKind>());

    [Theory]
    [MemberData(nameof(EffectiveLanguageVersionsAndSourceCodeKind))]
    public void LexPunctuation(
        LanguageVersion version,
        SourceCodeKind kind)
    {
        var parseOptions = TestOptions.Regular.WithLanguageVersion(version)
                                              .WithKind(kind);
        foreach (var info in SyntaxFactsTests.GetExpectedSyntaxKindInfos_Punctuation(version))
        {
            var V = LexSource(info.Text, options: parseOptions, withTrivia: true);
            if (info.Kind == SyntaxKind.HashExclamationToken)
            {
                DiagnosticDescription[] diagnostics = kind == SourceCodeKind.Script ? [] :
                [
                    Diagnostic(ErrorCode.WRN_ShebangOnlySupportedInScript, squiggledText: info.Text).WithLocation(1, 1)
                ];
                V(kind: SyntaxKind.ShebangDirectiveTrivia, text: info.Text, location: TriviaLocation.Leading, diagnostics: diagnostics);
                {
                    V(SyntaxKind.HashExclamationToken);
                    V(SyntaxKind.EndOfDirectiveToken);
                }
            }
            else
                V(kind: info.Kind, text: info.Text);
            V(SyntaxKind.EndOfFileToken);
        }
    }

    [Theory]
    [MemberData(nameof(ShebangSupportiveLanguageVersionsAndSourceCodeKind))]
    public void LexShebangAtFileStart(
        LanguageVersion version,
        SourceCodeKind kind)
    {
        var parseOptions = TestOptions.Regular.WithLanguageVersion(version)
                                              .WithKind(kind);

        // Test `#!` at the beginning of the file.
        var source = "#!";
        var V = LexSource(source, options: parseOptions, withTrivia: true);
        DiagnosticDescription[] diagnostics = kind == SourceCodeKind.Script ? [] :
        [
            Diagnostic(ErrorCode.WRN_ShebangOnlySupportedInScript, squiggledText: source).WithLocation(1, 1)
        ];
        V(kind: SyntaxKind.ShebangDirectiveTrivia, text: source, location: TriviaLocation.Leading, diagnostics: diagnostics);
        {
            V(SyntaxKind.HashExclamationToken);
            V(SyntaxKind.EndOfDirectiveToken);
        }
        V(SyntaxKind.EndOfFileToken);
    }

    [Theory]
    [MemberData(nameof(ShebangSupportiveLanguageVersionsAndSourceCodeKind))]
    public void LexShebangAtFirstLineBeforeFirstToken(
        LanguageVersion version,
        SourceCodeKind kind)
    {
        var parseOptions = TestOptions.Regular.WithLanguageVersion(version)
                                              .WithKind(kind);

        // Test `#!` at the first line with leading non-EOL whitespaces.
        var source = " #!";
        var V = LexSource(source, options: parseOptions, withTrivia: true);
        V(kind: SyntaxKind.WhitespaceTrivia, location: TriviaLocation.Leading);
        LexBrokenShebang(version, kind, V);
        V(SyntaxKind.EndOfFileToken);
    }

    [Theory]
    [MemberData(nameof(ShebangSupportiveLanguageVersionsAndSourceCodeKind))]
    public void LexShebangNotAtFirstLineBeforeFirstToken(
        LanguageVersion version,
        SourceCodeKind kind)
    {
        var parseOptions = TestOptions.Regular.WithLanguageVersion(version)
                                              .WithKind(kind);
        var shebang = SyntaxFactsTests.GetExpectedSyntaxKindInfos_Punctuation(version).First(static info => info.Kind == SyntaxKind.HashExclamationToken).Text;

        // Test `#!` not at the first line.
        var source = "\r\n" + shebang;
        var V = LexSource(source, options: parseOptions, withTrivia: true);
        V(kind: SyntaxKind.EndOfLineTrivia, location: TriviaLocation.Leading);
        LexBrokenShebang(version, kind, V);
        V(SyntaxKind.EndOfFileToken);
    }

    [Theory]
    [MemberData(nameof(ShebangSupportiveLanguageVersionsAndSourceCodeKind))]
    public void LexShebangAfterFirstToken(
        LanguageVersion version,
        SourceCodeKind kind)
    {
        var parseOptions = TestOptions.Regular.WithLanguageVersion(version)
                                              .WithKind(kind);
        var shebang = SyntaxFactsTests.GetExpectedSyntaxKindInfos_Punctuation(version).First(static info => info.Kind == SyntaxKind.HashExclamationToken).Text;

        // Test `#!` at the first line after first token.
        var source = "a" + shebang;
        var V = LexSource(source, options: parseOptions, withTrivia: true);
        V(kind: SyntaxKind.IdentifierToken, text: "a", location: TriviaLocation.Leading);
        LexBrokenShebang(version, kind, V);
        V(SyntaxKind.EndOfFileToken);
    }

    private void LexBrokenShebang(
        LanguageVersion version,
        SourceCodeKind kind,
        NodeValidator V)
    {
        const string Hash = "#";
        var hashKindInfo = SyntaxFactsTests.GetExpectedSyntaxKindInfos_Punctuation(version).FirstOrDefault(static version => version.Text == Hash);
        if (hashKindInfo.Kind == SyntaxKind.None)
            V(SyntaxKind.BadToken, text: Hash);
        else
            V(hashKindInfo.Kind, text: Hash);

        const string Exclamation = "!";
        var exclamationKindInfo = SyntaxFactsTests.GetExpectedSyntaxKindInfos_Punctuation(version).FirstOrDefault(static version => version.Text == Exclamation);
        if (exclamationKindInfo.Kind == SyntaxKind.None)
            V(SyntaxKind.BadToken, text: Exclamation);
        else
            V(exclamationKindInfo.Kind, text: Exclamation);
    }
}
