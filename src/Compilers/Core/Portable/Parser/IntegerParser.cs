// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace Qtyi.CodeAnalysis;

internal static class IntegerParser
{
    /// <summary>
    /// 尝试解析表示十六进制整型数字的字符串。
    /// </summary>
    /// <param name="s">表示十六进制整型数字的字符串。</param>
    /// <param name="l"><paramref name="s"/>表示的整型数字。</param>
    /// <returns>若解析成功，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    /// <remarks><paramref name="s"/>中不正确的格式或数字溢出都将导致解析失败。</remarks>
    public static bool TryParseHexadecimalInt64(string s, out long l)
    {
        if (s.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
            s = s.Substring(2);
        return long.TryParse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out l);
    }

    /// <summary>
    /// 尝试解析表示十进制整型数字的字符串。
    /// </summary>
    /// <param name="s">表示十进制整型数字的字符串。</param>
    /// <param name="ul"><paramref name="s"/>表示的整型数字。</param>
    /// <returns>若解析成功，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    /// <remarks>
    /// <para><paramref name="s"/>中不能包含负号（<c>-</c>），且表示的整型数字不能超过<c>0x8000000000000000</c>。</para>
    /// <para><paramref name="s"/>中不正确的格式或数字溢出都将导致解析失败。</para>
    /// </remarks>
    public static bool TryParseDecimalInt64(string s, out ulong ul)
    {
        if (ulong.TryParse(s, NumberStyles.Integer, CultureInfo.InvariantCulture, out ul))
            return ul <= 0x8000000000000000;
        else
            return false;
    }
}
