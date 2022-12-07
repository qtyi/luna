// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Numerics;

namespace Qtyi.CodeAnalysis.UnitTests;

[TestClass]
public class RealParserTests
{
    private static readonly Random s_random = new();

    protected internal static double NextRandomDouble() => BitConverter.Int64BitsToDouble(IntegerParserTests.NextRandomInt64());

    private protected virtual double GetRandomDouble() => RealParserTests.NextRandomDouble();

    [TestMethod]
    public void TryParserDecimalInt64Tests()
    {
        const int SampleCount = 31000;
        Parallel.For(0, SampleCount, body =>
        {
            var source = this.GetRandomDouble();
            var decimalStr = source.ToString();

            var success = RealParser.TryParseDecimalDouble(decimalStr, out var result);

            Assert.AreEqual(source, result, "十进制双精度浮点数解析错误！");
        });
    }
}
