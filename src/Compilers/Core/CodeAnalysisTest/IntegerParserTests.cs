// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Numerics;
using Xunit;

namespace Qtyi.CodeAnalysis.UnitTests;

public class IntegerParserTests
{
    private static readonly Random s_random = new();

    protected internal static BigInteger NextRandomBigInteger()
    {
        var sign = s_random.Next(2) * 2 - 1;
        var bits = s_random.Next(2, 310); // A bit bigger than double.MaxValue.
        var chars = new char[bits];
        for (var i = 0; i < chars.Length; i++)
            chars[i] = (char)(s_random.Next(10) + '0');
        var result = BigInteger.Parse(new string(chars));

        if (sign == -1)
            return -result;
        else
            return result;
    }

    protected internal static long NextRandomInt64()
    {
#if NET6_0_OR_GREATER
        return s_random.NextInt64();
#else
        return (s_random.Next() << sizeof(uint)) + unchecked((uint)s_random.Next());
#endif
    }

    private protected virtual BigInteger GetRandomUnsignedBigInteger() => BigInteger.Abs(NextRandomBigInteger());

    protected internal static ulong NextRandomUInt64() =>
        Math.Min(unchecked((ulong)NextRandomInt64()), 0x8000000000000000);

    private protected virtual long GetRandomInt64() => NextRandomInt64();

    private protected virtual ulong GetRandomUInt64() => NextRandomUInt64();

    [Fact]
    public void TryParseDecimalInt64Tests()
    {
        const int SampleCount = 31000;
        Parallel.For(0, SampleCount, body =>
        {
            var source = this.GetRandomUnsignedBigInteger();
            var decimalStr = source.ToString();

            var success = IntegerParser.TryParseDecimalInt64(decimalStr, out var result);

            if (success)
                Assert.Equal(source, (BigInteger)result);

            if (source > 0x8000000000000000)
                Assert.False(success, "BigInteger overflow, should failed.");
            else
                Assert.True(success, "BigInteger in range, should success.");
        });
    }

    [Fact]
    public void TryParseHexadecimalInt64Tests()
    {
        const int SampleCount = 31000;
        Parallel.For(0, SampleCount, body =>
        {
            var source = this.GetRandomInt64();
            var hexadecimalStr = Convert.ToString(source, 16);

            var success = IntegerParser.TryParseHexadecimalInt64(hexadecimalStr, out var result);

            Assert.True(success, "Failed parse hexadecimal double.");
            Assert.Equal(source, result);
        });
    }
}
