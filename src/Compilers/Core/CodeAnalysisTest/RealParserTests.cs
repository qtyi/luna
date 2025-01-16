// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Test.Utilities;
using Xunit;

namespace Qtyi.CodeAnalysis.UnitTests;

public class RealParserTests
{
    protected internal static double NextRandomDouble() => BitConverter.Int64BitsToDouble(IntegerParserTests.NextRandomInt64());

    private protected virtual double GetRandomDouble() => NextRandomDouble();

    [Fact]
    public void TryParseE()
    {
#if NETFRAMEWORK
        const double E = 2.7182818284590451;
#else
        const double E = double.E;
#endif
        TestFromToString(E, "2.7182818284590451");
    }

    [Fact]
    public void TryParseEpsilon()
    {
        TestFromToString(double.Epsilon, "4.94065645841247E-324");
    }

    [Fact]
    public void TryParsePi()
    {
#if NETFRAMEWORK
        const double Pi = 3.1415926535897931;
#else
        const double Pi = double.Pi;
#endif
        TestFromToString(Pi, "3.1415926535897931");
    }

    [Fact]
    public void TryParseTau()
    {
#if NETFRAMEWORK
        const double Tau = 6.2831853071795862;
#else
        const double Tau = double.Tau;
#endif
        TestFromToString(Tau, "6.2831853071795862");
    }

    [Fact]
    public void TryParseMaxValue()
    {
        TestFromToString(double.MaxValue, "1.7976931348623157E+308");
    }

    [Fact]
    public void TryParseMinValue()
    {
        TestFromToString(double.MinValue, "-1.7976931348623157E+308");
    }

    /// <remarks>
    /// Test only runs on CoreClr due to different behavior of <see cref="double.TryParse(string?, System.Globalization.NumberStyles, IFormatProvider?, out double)"/> between .NET Framework and .NET Core.
    /// See also <a href="https://devblogs.microsoft.com/dotnet/floating-point-parsing-and-formatting-improvements-in-net-core-3-0/">Floating-Point Parsing and Formatting improvements in .NET Core 3.0</a>.
    /// </remarks>
    [ConditionalFact(typeof(CoreClrOnly))]
    public void TryParseDecimalDouble()
    {
        const int SampleCount = 31000;
        Parallel.For(0, SampleCount, _ =>
        {
            var source = GetRandomDouble();
            TestFromToString(source);
        });
    }

    private static void TestFromToString(double source, string? text = null)
    {
        text ??= source.ToString();

        var success = RealParser.TryParseDecimalDouble(text, out var result);

        Assert.True(success, "Failed parse decimal double.");
        Assert.Equal(source, result);
    }
}
