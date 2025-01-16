// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Test.Utilities;

partial class TestOptions
{
    public static readonly LuaParseOptions Regular1_1;
    public static readonly LuaParseOptions Regular2_1;
    public static readonly LuaParseOptions Regular2_2;
    public static readonly LuaParseOptions Regular2_4;
    public static readonly LuaParseOptions Regular2_5;
    public static readonly LuaParseOptions Regular3_1;
    public static readonly LuaParseOptions Regular3_2;
    public static readonly LuaParseOptions Regular4;
    public static readonly LuaParseOptions Regular5;
    public static readonly LuaParseOptions Regular5_1;
    public static readonly LuaParseOptions Regular5_2;
    public static readonly LuaParseOptions Regular5_3;
    public static readonly LuaParseOptions Regular5_4;
    public static readonly LuaParseOptions RegularNext;

    static TestOptions()
    {
        Regular1_1 = Regular.WithLanguageVersion(LanguageVersion.Lua1_1);
        Regular2_1 = Regular.WithLanguageVersion(LanguageVersion.Lua2_1);
        Regular2_2 = Regular.WithLanguageVersion(LanguageVersion.Lua2_2);
        Regular2_4 = Regular.WithLanguageVersion(LanguageVersion.Lua2_4);
        Regular2_5 = Regular.WithLanguageVersion(LanguageVersion.Lua2_5);
        Regular3_1 = Regular.WithLanguageVersion(LanguageVersion.Lua3_1);
        Regular3_2 = Regular.WithLanguageVersion(LanguageVersion.Lua3_2);
        Regular4 = Regular.WithLanguageVersion(LanguageVersion.Lua4);
        Regular5 = Regular.WithLanguageVersion(LanguageVersion.Lua5);
        Regular5_1 = Regular.WithLanguageVersion(LanguageVersion.Lua5_1);
        Regular5_2 = Regular.WithLanguageVersion(LanguageVersion.Lua5_2);
        Regular5_3 = Regular.WithLanguageVersion(LanguageVersion.Lua5_3);
        Regular5_4 = Regular.WithLanguageVersion(LanguageVersion.Lua5_4);
        RegularNext = Regular.WithLanguageVersion(LanguageVersionFacts.LuaNext);
    }
}
