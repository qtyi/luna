// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Numerics;
using Xunit;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;

public class NumericLiteralTests : LexingTestBase
{
    [Fact]
    public void TestInt64MaxValue()
    {
        const long num = long.MaxValue;
        ValidateLiteral(num.ToString(), SyntaxKind.NumericLiteralToken, num);
    }

    [Fact]
    public void TestInt64MaxValuePlusOne()
    {
        const ulong num = 0x8000000000000000UL;
        ValidateLiteral(num.ToString(), SyntaxKind.NumericLiteralToken, num);
    }

    [Fact]
    public void TestDoubleMaxValue()
    {
        const double num = double.MaxValue;
        ValidateLiteral(num.ToString("G17"), SyntaxKind.NumericLiteralToken, num);
    }

    [Fact]
    public void TestHexDoubleValue()
    {
        const double value = 31.4568156151E-45;
        var hexValue = value.ToHexString();
        ValidateLiteral(hexValue, SyntaxKind.NumericLiteralToken, value);
    }

    [Fact]
    public void TestHexInt64Value()
    {
        ValidateLiteral("0x31415ABCD", SyntaxKind.NumericLiteralToken, 0x31415ABCD);
    }

    [Fact]
    public void TestDecDoubleValue1()
    {
        ValidateLiteral("31.415", SyntaxKind.NumericLiteralToken, 31.415D);
    }

    [Fact]
    public void TestDecDoubleValue2()
    {
        ValidateLiteral(".314", SyntaxKind.NumericLiteralToken, 0.314D);
    }

    [Fact]
    public void TestDecDoubleValue3()
    {
        ValidateLiteral("314.", SyntaxKind.NumericLiteralToken, 314.0D);
    }

    [Fact]
    public void TestDecInt64Value()
    {
        ValidateLiteral("31415", SyntaxKind.NumericLiteralToken, 31415L);
    }

}
