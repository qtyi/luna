// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.VisualBasic.Syntax;
using Roslyn.Test.Utilities;
using Xunit;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests;

using ThisTestBase = Test.Utilities.LuaTestBase;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests;

using ThisTestBase = Test.Utilities.MoonScriptTestBase;
#endif

public partial class SyntaxFactsTests : ThisTestBase
{
    internal readonly struct SyntaxKindInfo(SyntaxKind kind, string text)
    {
        public readonly SyntaxKind Kind = kind;
        public readonly string Text = text;

        public void Deconstruct(out SyntaxKind kind, out string text)
        {
            kind = Kind;
            text = Text;
        }

        public static implicit operator (SyntaxKind Kind, string Text)(SyntaxKindInfo info) => (info.Kind, info.Text);
        public static implicit operator SyntaxKindInfo((SyntaxKind Kind, string Text) tuple) => new(tuple.Kind, tuple.Text);
    }

    /// <summary>
    /// Tests syntax kinds provided by <paramref name="actualProvider"/>:
    ///   1) contains each object once;
    ///   2) are all defined in <see cref="SyntaxKind"/> enum;
    ///   3) set equals to syntax kinds provided by <paramref name="expectedProvider"/>.
    /// </summary>
    private static void TestGetSyntaxKinds(
        LanguageVersion version,
        Func<LanguageVersion, IEnumerable<SyntaxKindInfo>> expectedProvider,
        Func<LanguageVersion, IEnumerable<SyntaxKind>> actualProvider)
    {
        var actual = actualProvider(version);
        Assert.Distinct(actual);
        Assert.All(actual, static kind => Assert.True(Enum.IsDefined(typeof(SyntaxKind), kind)));
        var expected = expectedProvider(version);
        AssertEx.SetEqual(expected.Select(static info => info.Kind), actual);
    }

    /// <summary>
    /// Tests syntax kinds, that are all defined in <see cref="SyntaxKind"/> enum and selected by <paramref name="actualSelector"/>, set equals to syntax kinds provider by <paramref name="expectedProvider"/>.
    /// </summary>
    private static void TestIsSyntaxKind(
        LanguageVersion version,
        Func<LanguageVersion, IEnumerable<SyntaxKindInfo>> expectedProvider,
        Func<SyntaxKind, LanguageVersion, bool> actualSelector)
    {
        var expected = expectedProvider(version);
        var actual = from SyntaxKind kind in Enum.GetValues(typeof(SyntaxKind))
                     where actualSelector(kind, version)
                     select kind;
        AssertEx.SetEqual(expected.Select(static info => info.Kind), actual);
    }

    /// <summary>
    /// Tests syntax kinds provided by <paramref name="expectedProvider"/> are all have textual representation and can be parsed back via <paramref name="getKindFunc"/> to itself.
    /// </summary>
    private static void TestGetTextGetKind(
        LanguageVersion version,
        Func<LanguageVersion, IEnumerable<SyntaxKindInfo>> expectedProvider,
        Func<string, SyntaxKind>? getKindFunc = null)
    {
        foreach (var info in expectedProvider(version))
        {
            var text = SyntaxFacts.GetText(info.Kind);
            Assert.Equal(info.Text, text);

            if (getKindFunc is not null)
            {
                var parsedKind = getKindFunc(text);
                Assert.Equal(info.Kind, parsedKind);
            }
        }
    }

    #region Keyword
    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void GetSyntaxKinds_Keyword(LanguageVersion version)
    {
        TestGetSyntaxKinds(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_Keyword,
            actualProvider: SyntaxFacts.GetKeywordKinds);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void IsSyntaxKind_Keyword(LanguageVersion version)
    {
        TestIsSyntaxKind(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_Keyword,
            actualSelector: SyntaxFacts.IsKeywordKind);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void GetTextGetKind_Keyword(LanguageVersion version)
    {
        TestGetTextGetKind(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_ReservedKeyword,
            getKindFunc: SyntaxFacts.GetKeywordKind);
    }

    internal static IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_Keyword(LanguageVersion version) => [
        .. GetExpectedSyntaxKindInfos_ReservedKeyword(version),
        .. GetExpectedSyntaxKindInfos_ContextualKeyword(version)
    ];

    #region Reserved Keyword
    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void GetSyntaxKinds_ReservedKeyword(LanguageVersion version)
    {
        TestGetSyntaxKinds(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_ReservedKeyword,
            actualProvider: SyntaxFacts.GetReservedKeywordKinds);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void IsSyntaxKind_ReservedKeyword(LanguageVersion version)
    {
        TestIsSyntaxKind(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_ReservedKeyword,
            actualSelector: SyntaxFacts.IsReservedKeyword);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void GetTextGetKind_ReservedKeyword(LanguageVersion version)
    {
        TestGetTextGetKind(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_ReservedKeyword,
            getKindFunc: SyntaxFacts.GetReservedKeywordKind);
    }

    internal static partial IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_ReservedKeyword(LanguageVersion version);
    #endregion

    #region Contextual Keyword
    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void GetSyntaxKinds_ContextualKeyword(LanguageVersion version)
    {
        TestGetSyntaxKinds(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_ContextualKeyword,
            actualProvider: SyntaxFacts.GetContextualKeywordKinds);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void IsSyntaxKind_ContextualKeyword(LanguageVersion version)
    {
        TestIsSyntaxKind(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_ContextualKeyword,
            actualSelector: SyntaxFacts.IsContextualKeyword);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void GetTextGetKind_ContextualKeyword(LanguageVersion version)
    {
        TestGetTextGetKind(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_ContextualKeyword,
            getKindFunc: SyntaxFacts.GetContextualKeywordKind);
    }

    internal static partial IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_ContextualKeyword(LanguageVersion version);
    #endregion
    #endregion

    #region Punctuation
    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void GetSyntaxKinds_Punctuation(LanguageVersion version)
    {
        TestGetSyntaxKinds(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_Punctuation,
            actualProvider: SyntaxFacts.GetPunctuationKinds);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void IsSyntaxKind_Punctuation(LanguageVersion version)
    {
        TestIsSyntaxKind(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_Punctuation,
            actualSelector: SyntaxFacts.IsPunctuation);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void GetTextGetKind_Punctuation(LanguageVersion version)
    {
        TestGetTextGetKind(
            version,
            expectedProvider: GetExpectedSyntaxKindInfos_Punctuation,
            getKindFunc: SyntaxFacts.GetPunctuationKind);
    }

    internal static partial IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_Punctuation(LanguageVersion version);
    #endregion
}
