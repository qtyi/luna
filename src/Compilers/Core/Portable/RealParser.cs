// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;

namespace Qtyi.CodeAnalysis;

internal static class RealParser
{
    public static bool TryParseHexadecimalDouble(string s, out double d)
    {
        bool isHex = s.StartsWith("0x", StringComparison.OrdinalIgnoreCase);

        int exponentIndex = s.Length; // 默认指数前缀的位置超出字符串末尾（即不存在指数）。
        int decimalSeparaterIndex = exponentIndex; // 默认小数点的位置为指数前缀的位置（即不含小数点和小数部分）
        ulong mantissa = 0;
        int shiftedDigits = 0;
        for (int i = isHex ? 2 : 0; i < exponentIndex; i++)
        {
            char c = s[i];
            switch (c)
            {
                case '.':
                    decimalSeparaterIndex = i; // 查找小数点的位置。
                    continue;
                case 'p':
                case 'P':
                    exponentIndex = i; // 查找指数前缀的位置。
                    /* 小数点的位置（如果有）必定在指数前缀的位置前。
                     * 如果字符串中包含小数点，则decimalSeparaterIndex在循环前期就已经被发现并设置。此时exponentIndex还是默认值，必定比decimalSeparaterIndex大，条件不成立；
                     * 如果到此时仍未找到小数点，则decimalSeparaterIndex还是默认值，必定比exponentIndex大，条件成立。
                     */
                    if (decimalSeparaterIndex > exponentIndex)
                        decimalSeparaterIndex = exponentIndex; // 设置为指数前缀的位置（即不含小数点和小数部分）
                    continue;
            }

            if (shiftedDigits < 14) // 尾数部分最多只有52+1位（需要14个十六进制数位）。
            {
                if (c == '0' && mantissa == 0) continue; // 跳过所有的先导0。

                mantissa <<= 4;
                shiftedDigits++;
                mantissa += (byte)c.HexValue();
            }
        }
        // 1234.56789 => 123456789.0, decimalSeparaterOffset = decimalSeparaterIndex - exponentIndex. (< 0 because of left shift)
        int offset = (decimalSeparaterIndex - exponentIndex + shiftedDigits) * 4 - 1;
        // 若第54-56位上不全为0，则右移尾数部分，直到高位上只有第53位为1，计算偏移量。
        while (mantissa > 0x1FFFFFFFFFFFFF)
        {
            mantissa >>= 1;
            offset++;
        }
        // 若第53位为0，则左移尾数部分，直到第53位为1，计算偏移量。
        while (mantissa != 0 && mantissa <= 0xFFFFFFFFFFFFF)
        {
            mantissa <<= 1;
            offset--;
        }

        // 计算指数。
        int exponent;
        if (exponentIndex < s.Length)
        {
            if (exponentIndex == s.Length - 1)
                exponent = 0;
            else if (!short.TryParse(
                s.Substring(exponentIndex + 1, s.Length - exponentIndex - 1),
                NumberStyles.Number,
                CultureInfo.InvariantCulture,
                out var result
            )) // 用Int16承接指数的情况下仍然溢出。
            {
                d = default;
                return false;
            }
            else
                exponent = result;
        }
        else
            exponent = 0;
        exponent += offset; // 加上偏移量。
        if (exponent <= -1022 || exponent >= 1023) // 指数溢出。
        {
            d = default;
            return false;
        }

        d = BitConverter.Int64BitsToDouble(unchecked((long)(mantissa + ((ulong)(exponent + 0x3FF) << 52))));
        return true;
    }

    public static bool TryParseDecimalDouble(string s, out double d) =>
        double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out d);
}
