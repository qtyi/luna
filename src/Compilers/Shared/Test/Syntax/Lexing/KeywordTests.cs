// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Xunit;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests.Lexing;
#endif

using Syntax.InternalSyntax;
using Test.Utilities;

public partial class IdentifierTests : LexingTestBase
{
    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void LexReservedKeyword(LanguageVersion version)
    {
        var parseOptions = TestOptions.Regular.WithLanguageVersion(version);
        foreach (var info in SyntaxFactsTests.GetExpectedSyntaxKindInfos_ReservedKeyword(version))
        {
            var V = LexSource(info.Text, options: parseOptions).EndOfFile();
            V(kind: info.Kind, text: info.Text);
        }
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void LexContextualKeyword(LanguageVersion version)
    {
        var parseOptions = TestOptions.Regular.WithLanguageVersion(version);
        foreach (var info in SyntaxFactsTests.GetExpectedSyntaxKindInfos_ContextualKeyword(version))
        {
            var V = LexSource(info.Text, options: parseOptions).EndOfFile();
            V(kind: SyntaxKind.IdentifierToken, contextualKind: info.Kind, text: info.Text);
        }
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void MaxKeywordLength(LanguageVersion version)
    {
        foreach (var info in SyntaxFactsTests.GetExpectedSyntaxKindInfos_Keyword(version))
        {
            Assert.True(info.Text.Length <= LexerCache.MaxKeywordLength);
#if DEBUG
            if (info.Text.Length == LexerCache.MaxKeywordLength)
                Assert.Contains(info.Kind, LexerCache.MaxKeywordKinds);
#endif
        }
    }
}
