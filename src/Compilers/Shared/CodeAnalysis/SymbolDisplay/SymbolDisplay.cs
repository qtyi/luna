// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSemanticModel = LuaSemanticModel;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSemanticModel = MoonScriptSemanticModel;
#endif

public static partial class SymbolDisplay
{
    /// <summary>
    /// 返回语言预定义类型的字符串表示。
    /// </summary>
    /// <param name="obj">要表示成字符串的值。</param>
    /// <param name="quoteStrings">是否使用引用符号包裹。</param>
    /// <param name="useHexadecimalNumbers">是否使用十六进制的数字表示。</param>
    /// <returns>语言预定义类型的字符串表示。</returns>
    public static string? FormatPrimitive(object obj, bool quoteStrings, bool useHexadecimalNumbers)
    {
        var options = ObjectDisplayOptions.EscapeNonPrintableCharacters;

        if (quoteStrings)
            options |= ObjectDisplayOptions.UseQuotes;

        if (useHexadecimalNumbers)
            options |= ObjectDisplayOptions.UseHexadecimalNumbers;

        return ObjectDisplay.FormatPrimitive(obj, options);
    }

    /// <summary>
    /// 返回字符串的字符串表示。
    /// </summary>
    /// <param name="value">要表示成字符串的值。</param>
    /// <param name="quoteStrings">是否使用引用符号包裹。</param>
    /// <returns>字符串的字符串表示。</returns>
    public static string FormatLiteral(string value, bool quoteStrings)
    {
        var options = ObjectDisplayOptions.EscapeNonPrintableCharacters;
        if (quoteStrings) options |= ObjectDisplayOptions.UseQuotes;

        return ObjectDisplay.FormatLiteral(value, options);
    }

#warning 未完成
    internal static ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(Symbols.PublicModel.Symbol symbol, ThisSemanticModel semanticModel, int position, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    internal static string ToMinimalDisplayString(Symbols.PublicModel.Symbol symbol, ThisSemanticModel semanticModel, int position, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    internal static ImmutableArray<SymbolDisplayPart> ToDisplayParts(Symbols.PublicModel.Symbol symbol, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    internal static string ToDisplayString(Symbols.PublicModel.Symbol symbol, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }
}
