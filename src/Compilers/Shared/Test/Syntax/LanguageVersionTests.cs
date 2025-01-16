// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Xunit;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests;

using ThisTestBase = Test.Utilities.LuaTestBase;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests;

using ThisTestBase = Test.Utilities.MoonScriptTestBase;
#endif

public partial class LanguageVersionTests : ThisTestBase
{
    [Fact]
    public void NoDuplication()
    {
        Assert.Distinct(Enum.GetValues(typeof(LanguageVersion)).Cast<LanguageVersion>());
    }

    [Fact]
    public void WellKnownEnumValuesOrder()
    {
        var orderedValues = (from LanguageVersion version in Enum.GetValues(typeof(LanguageVersion))
                             orderby version
                             select version).ToArray();

        // 0: Default
        Assert.Equal(LanguageVersion.Default, orderedValues[0]);
        // ^2: DotNet
        Assert.Equal(LanguageVersion.DotNet, orderedValues[^2]);
        // ^1: Latest
        Assert.Equal(LanguageVersion.Latest, orderedValues[^1]);
    }

    [Fact]
    public void CurrentVersionOrder()
    {
        var orderedValues = (from LanguageVersion version in Enum.GetValues(typeof(LanguageVersion))
                             where version is not LanguageVersion.Default and
                                              not LanguageVersion.DotNet and
                                              not LanguageVersion.Latest
                             orderby version
                             select version).ToArray();

        // ^1: CurrentVersion
        Assert.Equal(LanguageVersionFacts.CurrentVersion, orderedValues[^1]);
    }

    [Fact]
    public void AllEnumValuesHaveDisplayString()
    {
        foreach (LanguageVersion version in Enum.GetValues(typeof(LanguageVersion)))
        {
            Assert.False(string.IsNullOrWhiteSpace(version.ToDisplayString()));
        }
    }

    [Fact]
    public void ParseAllEnumValueDisplayStrings()
    {
        foreach (LanguageVersion version in Enum.GetValues(typeof(LanguageVersion)))
        {
            Assert.True(LanguageVersionFacts.TryParse(version.ToDisplayString(), out var result));
            Assert.Equal(version, result);
        }
    }

    [Fact]
    public void MappedEnumValuesAreDefinedAndValid()
    {
        foreach (LanguageVersion version in Enum.GetValues(typeof(LanguageVersion)))
        {
            var mappedVersion = version.MapSpecifiedToEffectiveVersion();
            Assert.True(Enum.IsDefined(typeof(LanguageVersion), mappedVersion));
            Assert.True(mappedVersion.IsValid());
        }
    }
}
