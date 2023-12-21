// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using ICSharpCode.Decompiler.Metadata;
using ICSharpCode.Decompiler.TypeSystem;
using Internal.TypeSystem;
using Newtonsoft.Json.Linq;
using Xunit;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;

public class StringLiteralTests : LexingTestBase
{
    [InlineData("""   "'\"\'"   """, false, "'\"'")] // tests double quotes
    [InlineData("""   '"\'\"'   """, false, "\"'\"")] // tests single quotes
    [InlineData("[[[[][]=]]", true, "[[][]=")] // tests long brackets
    [InlineData("[=[]\r]\r\n]=\n=]]=]", true, "]\n]\n]=\n=]")] // tests different new lines
    [InlineData("[==[\r\nabc\r\n]==]", true, "abc\n")] // tests new lines on first line and last line
    [Theory]
    public void TestQuotes(string source, bool isMultiLine, string value)
    {
        ValidateUtf8StringLiteral(source, isMultiLine ? SyntaxKind.MultiLineRawStringLiteralToken : SyntaxKind.StringLiteralToken, value);
    }

    public static IEnumerable<(string text, byte[] value)> SpecialEscapeSequences { get; } = new[]
    {
        ("\\a", "\a"U8.ToArray()),
        ("\\b", "\b"U8.ToArray()),
        ("\\f", "\f"U8.ToArray()),
        ("\\n", "\n"U8.ToArray()),
        ("\\r", "\r"U8.ToArray()),
        ("\\t", "\t"U8.ToArray()),
        ("\\v", "\v"U8.ToArray()),
        ("\\\\", "\\"U8.ToArray()),
        ("\\'", "'"U8.ToArray()),
        ("\\\"", "\""U8.ToArray()),
        ("\\\n", "\n"U8.ToArray()),
        ("\\\r", "\n"U8.ToArray()),
        ("\\\r\n", "\n"U8.ToArray()),
        ("\\z ", ""U8.ToArray()),
        ("\\z\f", ""U8.ToArray()),
        ("\\z\n", ""U8.ToArray()),
        ("\\z\r", ""U8.ToArray()),
        ("\\z\t", ""U8.ToArray()),
        ("\\z\v", ""U8.ToArray()),
        ("\\z\r\n", ""U8.ToArray())
    };

    [Fact]
    public void TestSpecialEscapeSequence()
    {
        foreach ((var text, var value) in SpecialEscapeSequences)
        {
            TestWith(text, value);
            TestWith(text, value, leading: "a");
            TestWith(text, value, trailing: "b");
            TestWith(text, value, leading: "a", trailing: "b");
        }
    }

    public static IEnumerable<(string text, byte[] value)> DigitalEscapeSequences { get; } =
        from digit in Enumerable.Range(byte.MinValue, byte.MaxValue)
        let length = digit switch { < 10 => 1, < 100 => 2, _ => 3 }
        from count in Enumerable.Range(length, 4 - length)
        let format = "D" + count
        select ($"\\{digit.ToString(format)}", new byte[] { (byte)digit });

    [Fact]
    public void TestDigitalEscapeSequence()
    {
        foreach ((var text, var value) in DigitalEscapeSequences)
        {
            TestWith(text, value);
            TestWith(text, value, leading: "a");
            TestWith(text, value, trailing: "b");
            TestWith(text, value, leading: "a", trailing: "b");
        }
    }

    public static IEnumerable<(string text, byte[] value)> HexadecimalEscapeSequences { get; } =
        from digit in Enumerable.Range(byte.MinValue, byte.MaxValue)
        select ($"\\x{digit:X2}", new byte[] { (byte)digit });

    [Fact]
    public void TestHexadecimalEscapeSequence()
    {
        foreach ((var text, var value) in HexadecimalEscapeSequences)
        {
            TestWith(text, value);
            TestWith(text, value, leading: "a");
            TestWith(text, value, trailing: "b");
            TestWith(text, value, leading: "a", trailing: "b");
        }
    }

    public static IEnumerable<(string text, byte[] value)> UnicodeEscapeSequenceTestSources { get; } =
        from digit in new int[] { 0x0, 0xF, 0x10, 0x7F, 0x80, 0xFF, 0x100, 0x7FF, 0x800, 0xFFF, 0x1000, 0xFFFF, 0x10000, 0xFFFFF, 0x100000, 0x1FFFFF, 0x200000, 0xFFFFFF, 0x1000000, 0x3FFFFFF, 0x4000000, 0xFFFFFFF, 0x10000000, 0x7FFFFFFF }
        let length = digit.ToString("X").Length
        from count in Enumerable.Range(length, 9 - length)
        let format = "X" + count
        select ($"\\u{{{digit.ToString(format)}}}", digit switch
        {
            <= 0x7F => new byte[] { (byte)digit },
            <= 0x7FF => new byte[] { (byte)(0xC0 | (0x1F & digit >> 6)), (byte)(0x80 | (0x3F & digit)) },
            <= 0xFFFF => new byte[] { (byte)(0xE0 | (0xF & digit >> 12)), (byte)(0x80 | (0x3F & digit >> 6)), (byte)(0x80 | (0x3F & digit)) },
            <= 0x1FFFFF => new byte[] { (byte)(0xF0 | (0x7 & digit >> 18)), (byte)(0x80 | (0x3F & digit >> 12)), (byte)(0x80 | (0x3F & digit >> 6)), (byte)(0x80 | (0x3F & digit)) },
            <= 0x3FFFFFF => new byte[] { (byte)(0xF8 | (0x3 & digit >> 24)), (byte)(0x80 | (0x3F & digit >> 18)), (byte)(0x80 | (0x3F & digit >> 12)), (byte)(0x80 | (0x3F & digit >> 6)), (byte)(0x80 | (0x3F & digit)) },
            _ => new byte[] { (byte)(0xFC | (0x1 & digit >> 30)), (byte)(0x80 | (0x3F & digit >> 24)), (byte)(0x80 | (0x3F & digit >> 18)), (byte)(0x80 | (0x3F & digit >> 12)), (byte)(0x80 | (0x3F & digit >> 6)), (byte)(0x80 | (0x3F & digit)) }
        });

    [Fact]
    public void TestUnicodeEscapeSequence()
    {
        foreach ((var text, var value) in UnicodeEscapeSequenceTestSources)
        {
            TestWith(text, value);
            TestWith(text, value, leading: " ");
            TestWith(text, value, trailing: " ");
            TestWith(text, value, leading: " ", trailing: " ");
        }
    }

    private void TestWith(
        string text, byte[] value,
        string? leading = null, byte[]? leadingValue = null,
        string? trailing = null, byte[]? trailingValue = null)
    {
        if (leading is not null)
            leadingValue ??= Encoding.UTF8.GetBytes(leading);

        if (trailing is not null)
            trailingValue ??= Encoding.UTF8.GetBytes(trailing);

        if (leading is not null || trailing is not null)
        {
            text = string.Concat(leading, text, trailing);
            value = ConcatValues(leadingValue, value, trailingValue);
        }

        text = Quote(text, out var isMultiLine);
        ValidateUtf8StringLiteral(text, isMultiLine ? SyntaxKind.MultiLineRawStringLiteralToken : SyntaxKind.StringLiteralToken, value);

        static byte[] ConcatValues(params byte[]?[] values) => values.Where(static v => v is not null).SelectMany(static v => v!).ToArray();
    }

    private static string Quote(string text, out bool isMultiLine)
    {
        if (TrySingleLineQuote(text, out var quoted))
            isMultiLine = false;
        else
        {
            quoted = MultiLineQuote(text);
            isMultiLine = true;
        }

        return quoted;
    }

    private static bool TrySingleLineQuote(string text, [NotNullWhen(true)] out string? quoted)
    {
        char quote;
        if (text.Length == 0)
            quote = '\'';
        else
        {
            bool firstIsSingle, firstIsDouble;
            bool lastIsSingle, lastIsDouble;
            switch (text[0])
            {
                case '\'':
                    firstIsSingle = true;
                    firstIsDouble = false;
                    break;

                case '"':
                    firstIsSingle = false;
                    firstIsDouble = true;
                    break;

                default:
                    firstIsSingle = false;
                    firstIsDouble = false;
                    break;
            }
            switch (text[^1])
            {
                case '\'':
                    if (text.EndsWith("\\'"))
                    {
                        lastIsSingle = false;
                        lastIsDouble = false;
                    }
                    else
                    {
                        lastIsSingle = true;
                        lastIsDouble = false;
                    }
                    break;

                case '"':
                    if (text.EndsWith("\\\""))
                    {
                        lastIsSingle = false;
                        lastIsDouble = false;
                    }
                    else
                    {
                        lastIsSingle = false;
                        lastIsDouble = true;
                    }
                    break;

                default:
                    lastIsSingle = false;
                    lastIsDouble = false;
                    break;
            }

            if (firstIsSingle)
            {
                if (lastIsDouble)
                {
                    quoted = null;
                    return false;
                }
                quote = '"';
            }
            else if (firstIsDouble)
            {
                if (lastIsSingle)
                {
                    quoted = null;
                    return false;
                }
                quote = '\'';
            }
            else if (lastIsSingle)
            {
                if (firstIsDouble)
                {
                    quoted = null;
                    return false;
                }
                quote = '"';
            }
            else if (lastIsDouble)
            {
                if (firstIsSingle)
                {
                    quoted = null;
                    return false;
                }
                quote = '\'';
            }
            else
                quote = '\'';
        }

        quoted = string.Concat(quote, text, quote);
        return true;
    }

    private static string MultiLineQuote(string text)
    {
        var level = 0;
        if (text.Length >= 2)
        {
            while (true)
            {
                var endBracket = string.Concat(']', new string('=', level), ']');
                if (!text.Contains(endBracket))
                    break;

                level++;
            }
        }

        var equals = new string('=', level);
        return string.Concat('[', equals, '[', text, ']', equals, ']');
    }
}
