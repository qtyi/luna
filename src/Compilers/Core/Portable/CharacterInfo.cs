// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;

namespace Qtyi.CodeAnalysis;

internal static class CharacterInfo
{
    /// <summary>
    /// 指定的Unicode字符是否表示空白。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns>若<paramref name="c"/>的值表示空白则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static bool IsWhiteSpace(char c) =>
        c switch
        {
            ' ' or
            '\t' or
            '\v' or
            '\f' or
            '\u00A0' or // 无中断空格符（U+00A0）
            '\uFEFF' or // 零宽无中断空格符（U+FEFF）
            '\u001A'    // 替换符（U+001A）
                => true,
            > (char)255 =>
                CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.SpaceSeparator,
            _ => false
        };

    /// <summary>
    /// 指定的Unicode字符是否表示新行。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns>若<paramref name="c"/>的值表示新行则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static bool IsNewLine(char c) =>
        c switch
        {
            '\n' or           // 换行符（U+000A）
            '\r' or           // 回车符（U+000D）
            '\u0085' or       // 新行符（U+0085）
            '\u2028' or       // 分行负（U+2028）
            '\u2029' => true, // 分段符（U+2029）
            _ => false
        };

    /// <summary>
    /// 指定的两个连续的Unicode字符是否表示新行。
    /// </summary>
    /// <param name="firstChar">第一个Unicode字符。</param>
    /// <param name="secondChar">第二个Unicode字符。</param>
    /// <returns>若<paramref name="firstChar"/>和<paramref name="secondChar"/>组成的字符序列的值表示新行则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static bool IsNewLine(char firstChar, char secondChar) =>
        firstChar == '\r' && secondChar == '\n';

    /// <summary>
    /// 指定的Unicode字符是否是十六进制数字的数位。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns>若<paramref name="c"/>的值是十六进制数字的数位（0-9、A-F、a-f）则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static bool IsHexDigit(this char c) =>
        c switch
        {
            >= '0' and <= '9' => true,
            >= 'A' and <= 'F' => true,
            >= 'a' and <= 'f' => true,
            _ => false
        };

    /// <summary>
    /// 指定的Unicode字符是否是二进制数字的数位。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns>若<paramref name="c"/>的值是二进制数字的数位（0或1）则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static bool IsBinaryDigit(this char c) =>
        c == '0' || c == '1';

    /// <summary>
    /// 指定的Unicode字符是否是十进制数字的数位。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns>若<paramref name="c"/>的值是十进制数字的数位（0-9）则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static bool IsDecDigit(this char c) =>
        c >= '0' && c <= '9';

    /// <summary>
    /// 获取指定的Unicode字符表示的十六进制数字的数位的值。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns><paramref name="c"/>表示的十六进制数字的数位的值。</returns>
    public static int HexValue(this char c)
    {
        Debug.Assert(c.IsHexDigit());
        return c switch
        {
            >= '0' and <= '9' => c - '0',
            _ => (c & 0xdf) - 'A' + 10
        };
    }

    /// <summary>
    /// 获取指定的Unicode字符表示的二进制数字的数位的值。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns><paramref name="c"/>表示的二进制数字的数位的值。</returns>
    internal static int BinaryValue(this char c)
    {
        Debug.Assert(c.IsBinaryDigit());
        return c - '0';
    }

    /// <summary>
    /// 获取指定的Unicode字符表示的十进制数字的数位的值。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns><paramref name="c"/>表示的十进制数字的数位的值。</returns>
    internal static int DecValue(this char c)
    {
        Debug.Assert(c.IsDecDigit());
        return c - '0';
    }
}
