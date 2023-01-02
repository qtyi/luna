// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

public static partial class SymbolDisplay
{
    public static partial string? FormatPrimitive(object obj, bool quoteStrings, bool useHexadecimalNumbers)
    {
        var options = ObjectDisplayOptions.EscapeNonPrintableCharacters;

        if (quoteStrings)
            options |= ObjectDisplayOptions.UseQuotes;

        if (useHexadecimalNumbers)
            options |= ObjectDisplayOptions.UseHexadecimalNumbers;

        return ObjectDisplay.FormatPrimitive(obj, options);
    }

    public static partial string FormatLiteral(string value, bool quoteStrings)
    {
        var options = ObjectDisplayOptions.EscapeNonPrintableCharacters;
        if (quoteStrings) options |= ObjectDisplayOptions.UseQuotes;

        return ObjectDisplay.FormatLiteral(value, options);
    }
}
