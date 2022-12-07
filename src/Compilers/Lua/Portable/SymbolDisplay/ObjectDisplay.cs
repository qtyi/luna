// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

internal static class ObjectDisplay
{
    /// <summary>
    /// 获取<see langword="nil"/>的字符串表示。
    /// </summary>
    internal static string NilLiteral => "nil";

    /// <summary>
    /// 格式化预定义类型的值。
    /// </summary>
    /// <param name="obj">要格式化的预定义类型的值。</param>
    /// <returns><paramref name="obj"/>的字符串表示。</returns>
    /// <remarks>
    /// 处理类型有<see cref="bool"/>、<see cref="string"/>、<see cref="sbyte"/>、<see cref="byte"/>、<see cref="short"/>、<see cref="ushort"/>、<see cref="int"/>、<see cref="uint"/>、<see cref="long"/>、<see cref="ulong"/>、<see cref="double"/>、<see cref="float"/>、<see cref="decimal"/>和<see langword="null"/>。
    /// </remarks>
    internal static string? FormatPrimitive(object? obj, ObjectDisplayOptions options) =>
        obj switch
        {
            null => ObjectDisplay.NilLiteral,
            bool => ObjectDisplay.FormatLiteral((bool)obj),
            sbyte or byte or short or ushort or int or uint or long or ulong => ObjectDisplay.FormatLiteral((long)Convert.ChangeType(obj, typeof(long)), options),
            float or double or decimal => ObjectDisplay.FormatLiteral((double)Convert.ChangeType(obj, typeof(double)), options),
            string => ObjectDisplay.FormatLiteral((string)obj, options),
            _ => null
        };

    /// <summary>
    /// 格式化布尔值字面量。
    /// </summary>
    /// <param name="value">要格式化的字面量。</param>
    /// <returns><paramref name="value"/>的字符串表示。</returns>
    internal static string FormatLiteral(bool value) =>
        value ? "true" : "false";

    /// <summary>
    /// 格式化64位有符号整数字面量。
    /// </summary>
    /// <param name="value">要格式化的字面量。</param>
    /// <returns><paramref name="value"/>的字符串表示。</returns>
    /// <remarks>
    /// 当<paramref name="options"/>包含<see cref="ObjectDisplayOptions.UseHexadecimalNumbers"/>时，返回十六进制格式。
    /// </remarks>
    internal static string FormatLiteral(long value, ObjectDisplayOptions options, CultureInfo? cultureInfo = null)
    {
        var pooledBuilder = PooledStringBuilder.GetInstance();
        var sb = pooledBuilder.Builder;

        if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
        {
            sb.Append("0x");
            sb.Append(value.ToHexString());
        }
        else
        {
            sb.Append(value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo)));
        }

        return pooledBuilder.ToStringAndFree();
    }

    /// <summary>
    /// 格式化64位无符号整数字面量。
    /// </summary>
    /// <param name="value">要格式化的字面量。</param>
    /// <returns><paramref name="value"/>的字符串表示。</returns>
    /// <remarks>
    /// 当<paramref name="options"/>包含<see cref="ObjectDisplayOptions.UseHexadecimalNumbers"/>时，返回十六进制格式。
    /// </remarks>
    internal static string FormatLiteral(ulong value, ObjectDisplayOptions options, CultureInfo? cultureInfo = null)
    {
        var pooledBuilder = PooledStringBuilder.GetInstance();
        var sb = pooledBuilder.Builder;

        if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
        {
            sb.Append("0x");
            sb.Append(value.ToHexString());
        }
        else
        {
            sb.Append(value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo)));
        }

        return pooledBuilder.ToStringAndFree();
    }

    /// <summary>
    /// 格式化浮点数字面量。
    /// </summary>
    /// <param name="value">要格式化的字面量。</param>
    /// <returns><paramref name="value"/>的字符串表示。</returns>
    /// <remarks>
    /// 当<paramref name="options"/>包含<see cref="ObjectDisplayOptions.UseHexadecimalNumbers"/>时，返回十六进制格式。
    /// </remarks>
    internal static string FormatLiteral(double value, ObjectDisplayOptions options, CultureInfo? cultureInfo = null)
    {
        var pooledBuilder = PooledStringBuilder.GetInstance();
        var sb = pooledBuilder.Builder;

        if (options.IncludesOption(ObjectDisplayOptions.UseHexadecimalNumbers))
        {
            sb.Append("0x");
            sb.Append(value.ToHexString());
        }
        else
        {
            sb.Append(value.ToString(ObjectDisplay.GetFormatCulture(cultureInfo)));
        }

        return pooledBuilder.ToStringAndFree();
    }

    /// <summary>
    /// 将64位有符号整数转化为十六进制字符串格式。
    /// </summary>
    public static string ToHexString(this long value) => value.ToString("X");

    /// <summary>
    /// 将64位无符号整数转化为十六进制字符串格式。
    /// </summary>
    public static string ToHexString(this ulong value) => value.ToString("X");

    /// <summary>
    /// 将双精度浮点数转化为十六进制字符串格式。
    /// </summary>
    public static string ToHexString(this double value)
    {
        var pooledBuilder = PooledStringBuilder.GetInstance();
        var sb = pooledBuilder.Builder;

        if (double.IsNaN(value))
        {
            sb.Append("nan");
        }
        else
        {
            if (value < 0)
            {
                sb.Append('-');
                value = -value;
            }

            if (double.IsInfinity(value))
            {
                sb.Append("inf");
            }
            else
            {
                sb.Append("0x1");

                long longValue = BitConverter.DoubleToInt64Bits(value);
                long mantissa = longValue & 0xFFFFFFFFFFFFF;
                long exponent = (longValue >> 52) - 0x3FF;

                string mantissaStr = mantissa.ToString("X");
                mantissaStr = mantissaStr.TrimEnd('0');
                if (mantissaStr.Length != 0)
                {
                    sb.Append('.');
                    sb.Append(mantissaStr);
                }

                if (exponent != 0)
                {
                    sb.Append('P');
                    sb.Append(exponent);
                }
            }
        }

        return pooledBuilder.ToStringAndFree();
    }

    /// <summary>
    /// 格式化字符串字面量。
    /// </summary>
    /// <param name="value">要格式化的字面量。</param>
    /// <returns><paramref name="value"/>的字符串表示。</returns>
    /// <remarks>
    /// <para>当<paramref name="options"/>包含<see cref="ObjectDisplayOptions.UseQuotes"/>时，使用引用符号包裹。</para>
    /// <para>当<paramref name="options"/>包含<see cref="ObjectDisplayOptions.EscapeNonPrintableCharacters"/>时，转义无法打印的字符字符串。</para>
    /// <para>当<paramref name="options"/>包含<see cref="ObjectDisplayOptions.UseQuotes"/>但不包含<see cref="ObjectDisplayOptions.EscapeNonPrintableCharacters"/>且<paramref name="value"/>中含有换行时，使用长方括号包裹。</para>
    /// </remarks>
    internal static string FormatLiteral(string value, ObjectDisplayOptions options)
    {
        const char quote = '"';

        var pooledBuilder = PooledStringBuilder.GetInstance();
        var sb = pooledBuilder.Builder;

        var useQuote = options.IncludesOption(ObjectDisplayOptions.UseQuotes);
        var escapeNonPrintable = options.IncludesOption(ObjectDisplayOptions.EscapeNonPrintableCharacters);
        var isVerbatim = useQuote && !escapeNonPrintable && ObjectDisplay.ContainsNewLine(value);

        int longBracketLevel = -1; // 逐字字符串两端的长方括号级数。
        var disabledLevels = PooledHashSet<int>.GetInstance(); // 不能使用的长方括号级数。

        for (int i = 0; i < value.Length; i++)
        {
            char c = value[i];

            // 检查长方括号级数。
            switch (c)
            {
                // 字符“]”只能出现在头部和尾部。
                case ']':
                    if (longBracketLevel < 0) // 初始状态
                        longBracketLevel = 0;
                    else // 结束状态
                    {
                        disabledLevels.Add(longBracketLevel);
                        longBracketLevel = -1; // 设置为初始状态。
                    }
                    break;
                case '=': // 字符“=”只能出现在中间，且必须是连续的至少零个字符。
                    if (longBracketLevel >= 0) // 符合格式，累加级数。
                        longBracketLevel++;
                    else
                        longBracketLevel = -1; // 不符合格式，设置为初始状态。
                    break;
            }

            // 处理字符转义。
            if (escapeNonPrintable && CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.Surrogate)
            {
                var category = CharUnicodeInfo.GetUnicodeCategory(value, i);
                if (category == UnicodeCategory.Surrogate)
                { // 未配对的代理对。
                    sb.Append("\\u{");
                    sb.Append(((int)c).ToString("X"));
                    sb.Append('}');
                }
                else if (ObjectDisplay.NeedsEscaping(category))
                { // 要被转义的已配对的代理对。
                    var unicode = char.ConvertToUtf32(value, i);
                    sb.Append("\\u{");
                    sb.Append(unicode.ToString("X"));

                    i++; // 跳过代理对的第二个字符。
                }
                else
                { // 直接输出不需转义的已配对的代理对。
                    sb.Append(c);
                    sb.Append(value[++i]);
                }
            }
            else if (escapeNonPrintable && ObjectDisplay.TryReplaceChar(c, out var replaceWith))
            {
                sb.Append(replaceWith);
            }
            else if (useQuote && c == quote)
            {
                if (isVerbatim)
                    sb.Append(c);
                else
                {
                    sb.Append('\\');
                    sb.Append(quote);
                }
            }
            else
            {
                sb.Append(c);
            }
        }
        disabledLevels.Free();

        if (useQuote)
        {
            if (isVerbatim)
            {
                // 找到可用的长方括号级数。
                int avaliableLevel = 0;
                while (disabledLevels.Contains(avaliableLevel)) avaliableLevel++;

                char[] chars = new char[avaliableLevel + 2];
                for (int i = 1; i <= avaliableLevel; i++)
                    chars[i] = '=';

                chars[0] = chars[^1] = '[';
                sb.Insert(0, chars);

                chars[0] = chars[^1] = ']';
                sb.Append(chars);
            }
            else
            {
                sb.Insert(0, quote);
                sb.Append(quote);
            }
        }

        return pooledBuilder.ToStringAndFree();
    }

    /// <summary>
    /// 字符串<paramref name="s"/>中是否包含换行。
    /// </summary>
    private static bool ContainsNewLine(string s)
    {
        foreach (char c in s)
        {
            if (SyntaxFacts.IsNewLine(c))
                return true;
        }

        return false;
    }

    /// <summary>
    /// 是否应该转义指定的Unicode类别。
    /// </summary>
    private static bool NeedsEscaping(UnicodeCategory category) =>
        category switch
        {
            UnicodeCategory.Control or
            UnicodeCategory.OtherNotAssigned or
            UnicodeCategory.ParagraphSeparator or
            UnicodeCategory.LineSeparator or
            UnicodeCategory.Surrogate => true,

            _ => false,
        };

    /// <summary>
    /// 尝试替换字符为转义字符串。
    /// </summary>
    /// <returns>若替换成功，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    private static bool TryReplaceChar(char c, [NotNullWhen(true)] out string? replaceWith)
    {
        replaceWith = c switch
        {
            '\\' => "\\\\",
            '\0' => "\\0",
            '\a' => "\\a",
            '\b' => "\\b",
            '\f' => "\\f",
            '\n' => "\\n",
            '\r' => "\\r",
            '\t' => "\\t",
            '\v' => "\\v",
            _ => null
        };

        if (replaceWith is not null) return true;

        if (ObjectDisplay.NeedsEscaping(CharUnicodeInfo.GetUnicodeCategory(c)))
        {
            replaceWith = $"\\u{{{(int)c:X}}}";
            return true;
        }

        return false;
    }

    /// <summary>
    /// 获取格式化的文化信息。
    /// </summary>
    /// <remarks>
    /// 默认使用<see cref="CultureInfo.InvariantCulture"/>。
    /// </remarks>
    private static CultureInfo GetFormatCulture(CultureInfo? cultureInfo) => cultureInfo ?? CultureInfo.InvariantCulture;
}
