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
    internal static readonly LanguageVersion[] DefinedLanguageVersions =
#if NETFRAMEWORK
        (LanguageVersion[])Enum.GetValues(typeof(LanguageVersion));
#else
        Enum.GetValues<LanguageVersion>();
#endif

    /// <summary>
    /// Theory data of effective <see cref="LanguageVersion"/> enum values.
    /// </summary>
    public static readonly TheoryData<LanguageVersion> EffectiveLanguageVersions = new(
        from version in DefinedLanguageVersions
        where version.IsValid()
        orderby version
        select version);

    [Fact]
    public void NoDuplication()
    {
        Assert.Distinct(DefinedLanguageVersions);
    }

    [Fact]
    public void WellKnownEnumValuesOrder()
    {
        var orderedValues = (from version in DefinedLanguageVersions
                             orderby version
                             select version).ToArray();

        // 0: Default
        Assert.Equal(LanguageVersion.Default, orderedValues[0]);
        // ^3: Preview
        Assert.Equal(LanguageVersion.Preview, orderedValues[^3]);
        // ^2: DotNet
        Assert.Equal(LanguageVersion.DotNet, orderedValues[^2]);
        // ^1: Latest
        Assert.Equal(LanguageVersion.Latest, orderedValues[^1]);
    }

    [Fact]
    public void NumericVersionOrder()
    {
        var orderedValues = (from version in DefinedLanguageVersions
                             where version is not LanguageVersion.Default and
                                              not LanguageVersion.Preview and
                                              not LanguageVersion.DotNet and
                                              not LanguageVersion.Latest
                             orderby version
                             select version);

        var expectedOrderedValues = orderedValues
            .Select(value =>
            {
                var valueString = Enum.GetName(typeof(LanguageVersion), value)!;
                Assert.StartsWith(ThisLanguageName, valueString);

                var versionPart = valueString[ThisLanguageName.Length..];
                Assert.Matches(@"\d+(_\d+)?", versionPart);

                var underscoreIndex = versionPart.IndexOf('_');
                int major, minor;
                string wellFormatted;
                if (underscoreIndex < 0) // major only
                {
                    var majorPart = versionPart;
                    major = int.Parse(majorPart);
                    minor = 0;
                    wellFormatted = $"{ThisLanguageName}{major}";
                }
                else
                {
                    var majorPart = versionPart[..underscoreIndex];
                    var minorPart = versionPart[(underscoreIndex + 1)..];
                    major = int.Parse(majorPart);
                    minor = int.Parse(minorPart);
                    wellFormatted = $"{ThisLanguageName}{major}_{minor}";
                }
                Assert.True(wellFormatted == valueString, $"Bad format for language version name '{valueString}', should use '{wellFormatted}' instead.");

                return (Major: major, Minor: minor, Value: value);
            })
            .OrderBy(static version => version.Major)
            .ThenBy(static version => version.Minor)
            .Select(static version => version.Value);

        Assert.Equal(expectedOrderedValues, orderedValues);
    }

    [Fact]
    public void CurrentVersionOrder()
    {
        var orderedValues = (from version in DefinedLanguageVersions
                             where version is not LanguageVersion.Default and
                                              not LanguageVersion.Preview and
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
        foreach (var version in DefinedLanguageVersions)
        {
            Assert.False(string.IsNullOrWhiteSpace(version.ToDisplayString()));
        }
    }

    [Fact]
    public void ParseAllEnumValueDisplayStrings()
    {
        foreach (var version in DefinedLanguageVersions)
        {
            Assert.True(LanguageVersionFacts.TryParse(version.ToDisplayString(), out var result));
            Assert.Equal(version, result);
        }
    }

    [Fact]
    public void MappedEnumValuesAreDefinedAndValid()
    {
        foreach (var version in DefinedLanguageVersions)
        {
            var mappedVersion = version.MapSpecifiedToEffectiveVersion();
            Assert.True(Enum.IsDefined(typeof(LanguageVersion), mappedVersion));
            Assert.True(mappedVersion.IsValid());
        }
    }
}
