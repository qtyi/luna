// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript.Test.Utilities;

partial class TestOptions
{
    public static readonly MoonScriptParseOptions Regular0_1;
    public static readonly MoonScriptParseOptions Regular0_2;
    public static readonly MoonScriptParseOptions Regular0_3;
    public static readonly MoonScriptParseOptions Regular0_4;
    public static readonly MoonScriptParseOptions Regular0_5;
    public static readonly MoonScriptParseOptions RegularNext;

    static TestOptions()
    {
        Regular0_1 = Regular.WithLanguageVersion(LanguageVersion.MoonScript0_1);
        Regular0_2 = Regular.WithLanguageVersion(LanguageVersion.MoonScript0_2);
        Regular0_3 = Regular.WithLanguageVersion(LanguageVersion.MoonScript0_3);
        Regular0_4 = Regular.WithLanguageVersion(LanguageVersion.MoonScript0_4);
        Regular0_5 = Regular.WithLanguageVersion(LanguageVersion.MoonScript0_5);
        RegularNext = Regular.WithLanguageVersion(LanguageVersionFacts.MoonScriptNext);
    }
}
