// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Qtyi.CodeAnalysis.UnitTests;

[TestClass]
public class IntegerParserTests
{
    private static readonly Random s_random = new();

    protected internal static BigInteger NextRandomBigInteger()
    {
        int sign = s_random.Next(2) * 2 - 1;
        int bits = s_random.Next(2, 310); // 稍微比double.MaxValue大一些。
        char[] chars = new char[bits];
        for (int i = 0; i < chars.Length; i++)
            chars[i] = (char)(s_random.Next(10) + '0');
        var result = BigInteger.Parse(new string(chars));

        if (sign == -1)
            return -result;
        else
            return result;
    }

    protected internal static long NextRandomInt64()
    {
#if NET6_0
        return s_random.NextInt64();
#else
        return (s_random.Next() << sizeof(uint)) + unchecked((uint)s_random.Next());
#endif
    }

    private protected virtual BigInteger GetRandomUnsignedBigInteger() => BigInteger.Abs(IntegerParserTests.NextRandomBigInteger());

    protected internal static ulong NextRandomUInt64() =>
        Math.Min(unchecked((ulong)IntegerParserTests.NextRandomInt64()), 0x8000000000000000);

    private protected virtual long GetRandomInt64() => IntegerParserTests.NextRandomInt64();

    private protected virtual ulong GetRandomUInt64() => IntegerParserTests.NextRandomUInt64();

    [TestMethod]
    public void TryParseDecimalInt64Tests()
    {
        const int SampleCount = 31000;
        Parallel.For(0, SampleCount, body =>
        {
            var source = this.GetRandomUnsignedBigInteger();
            var decimalStr = source.ToString();

            var success = IntegerParser.TryParseDecimalInt64(decimalStr, out var result);

            if (success)
                Assert.AreEqual(source, (BigInteger)result, "十进制数字解析错误！");

            if (source > 0x8000000000000000)
                Assert.IsFalse(success, "大数字超出范围，应当数字溢出导致失败！");
            else
                Assert.IsTrue(success, "大数字在范围内，应当返回解析结果！");
        });
    }

    [TestMethod]
    public void TryParseHexadecimalInt64Tests()
    {
        const int SampleCount = 31000;
        Parallel.For(0, SampleCount, body =>
        {
            var source = this.GetRandomInt64();
            var hexadecimalStr = Convert.ToString(source, 16);

            var success = IntegerParser.TryParseHexadecimalInt64(hexadecimalStr, out var result);

            Assert.AreEqual(source, result, "十六进制数字解析错误！");
        });
    }
}
