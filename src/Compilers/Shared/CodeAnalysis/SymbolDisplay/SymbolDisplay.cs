// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.
#if LANG_LUA
    Lua
#elif LANG_MOONSCRIPT
    MoonScript
#endif
    ;

public static partial class SymbolDisplay
{
    /// <summary>
    /// 返回语言预定义类型的字符串表示。
    /// </summary>
    /// <param name="obj">要表示成字符串的值。</param>
    /// <param name="quoteStrings">是否使用引用符号包裹。</param>
    /// <param name="useHexadecimalNumbers">是否使用十六进制的数字表示。</param>
    /// <returns>语言预定义类型的字符串表示。</returns>
    public static partial string? FormatPrimitive(object obj, bool quoteStrings, bool useHexadecimalNumbers);

    /// <summary>
    /// 返回字符串的字符串表示。
    /// </summary>
    /// <param name="value">要表示成字符串的值。</param>
    /// <param name="quoteStrings">是否使用引用符号包裹。</param>
    /// <returns>字符串的字符串表示。</returns>
    public static partial string FormatLiteral(string value, bool quoteStrings);
}
