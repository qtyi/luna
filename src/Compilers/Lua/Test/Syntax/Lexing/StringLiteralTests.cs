// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Test.Utilities;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;
using Xunit;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;

using QuotedTextProvider = Func<string, string>;

public class StringLiteralTests : LexingTestBase
{
    public static readonly TheoryData<LanguageVersion> SupportMultiLineStringLiteralLanguageVersions = LanguageVersionTests.EffectiveLanguageVersions.Where(static version => version >= LanguageVersion.Lua2_2);

    public static readonly TheoryData<LanguageVersion> SupportLongBracketedForm = SupportMultiLineStringLiteralLanguageVersions.Where(static version => version >= LanguageVersion.Lua5_1);

    [InlineData("""   "'\"\'"   """, "'\"'")] // tests double quotes
    [InlineData("""   '"\'\"'   """, "\"'\"")] // tests single quotes
    [Theory]
    public void QuotedForm(string source, string value)
    {
        ValidateUtf8StringLiteral(source, SyntaxKind.StringLiteralToken, value);
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void BracketedForm(LanguageVersion version)
    {
        var options = TestOptions.Regular.WithLanguageVersion(version);

        // bracketed form
        if (version >= LanguageVersion.Lua2_2) // start from Lua 2.2
        {
            ValidateUtf8StringLiteral("[[]]", SyntaxKind.MultiLineRawStringLiteralToken, string.Empty);

            // ignore the new-line directly after [[
            if (version >= LanguageVersion.Lua5) // start from Lua 5
            {
                TestNewLineDirectlyAfterOpenBrackets(ignore: true, level: 0);
            }
            else
            {
                TestNewLineDirectlyAfterOpenBrackets(ignore: false, level: 0);
            }
        }
        else
        {
            var V = LexSource("[[]]", options: options);
            V(SyntaxKind.OpenBracketToken);
            V(SyntaxKind.OpenBracketToken);
            V(SyntaxKind.CloseBracketToken);
            V(SyntaxKind.CloseBracketToken);
            V(SyntaxKind.EndOfFileToken);
        }

        // long-bracketed form
        if (version >= LanguageVersion.Lua5_1) // start from Lua 5.1
        {
            ValidateUtf8StringLiteral("[=[]=]", SyntaxKind.MultiLineRawStringLiteralToken, string.Empty);

            // ignore the new-line directly after [=[
            if (true) // for all versions that supports long-bracketed form
            {
                TestNewLineDirectlyAfterOpenBrackets(ignore: true, level: 1);
            }
        }
        else
        {
            var V = LexSource("[=[]=]", options: options);
            V(SyntaxKind.OpenBracketToken);
            V(SyntaxKind.EqualsToken);
            V(SyntaxKind.OpenBracketToken);
            V(SyntaxKind.CloseBracketToken);
            V(SyntaxKind.EqualsToken);
            V(SyntaxKind.CloseBracketToken);
            V(SyntaxKind.EndOfFileToken);
        }

        void TestNewLineDirectlyAfterOpenBrackets(
            bool ignore,
            int level = 0)
        {
            (string? Text, byte[]? Value)[] trailings = [
                ("\r", [.. "\n"U8]),
                ("\n", [.. "\n"U8]),
                ("\r\n", [.. "\n"U8]),
                ("a", [.. "a"U8])
            ];

            TestValid(text: "\r", value: ignore ? [] : [.. "\n"U8], trailings: [trailings[0], trailings[2], trailings[3]], level: level, options: options); // [[^CR
            TestValid(text: "\n", value: ignore ? [] : [.. "\n"U8], trailings: trailings, level: level, options: options); // [[^LF
            TestValid(text: "\r\n", value: ignore ? [] : [.. "\n"U8], trailings: trailings, level: level, options: options); // [[^CRLF
        }

        static void TestValid(
            string text, byte[] value,
            (string? Text, byte[]? Value)[]? leadings = null,
            (string? Text, byte[]? Value)[]? trailings = null,
            int level = 0,
            LuaParseOptions? options = null)
        {
            var quotedTextProvider = Bracketed(level);

            TestWith(text, value, SyntaxKind.MultiLineRawStringLiteralToken, quotedTextProvider: quotedTextProvider, options: options);

            foreach (var (leading, leadingValue) in leadings ?? [(null, null)])
            {
                var hasLeading = leading is not null || leadingValue is not null;

                if (hasLeading)
                    TestWith(text, value, SyntaxKind.MultiLineRawStringLiteralToken, leading: leading, leadingValue: leadingValue, quotedTextProvider: quotedTextProvider, options: options);

                foreach (var (trailing, trailingValue) in trailings ?? [(null, null)])
                {
                    var hasTrailing = trailing is not null || trailingValue is not null;

                    if (hasTrailing)
                        TestWith(text, value, SyntaxKind.MultiLineRawStringLiteralToken, trailing: trailing, trailingValue: trailingValue, quotedTextProvider: quotedTextProvider, options: options);

                    if (hasLeading && hasTrailing)
                        TestWith(text, value, SyntaxKind.MultiLineRawStringLiteralToken, leading: leading, leadingValue: leadingValue, trailing: trailing, trailingValue: trailingValue, quotedTextProvider: quotedTextProvider, options: options);
                }
            }
        }

        static QuotedTextProvider Bracketed(int level = 0)
        {
            level = level < 0 ? 0 : level;
            return text =>
            {
                var equals = new string('=', level);
                return $"[{equals}[{text}]{equals}]";
            };
        }
    }

    [Theory]
    [MemberData(nameof(SupportMultiLineStringLiteralLanguageVersions))]
    public void Termination(LanguageVersion version)
    {
        var options = TestOptions.Regular.WithLanguageVersion(version);

        // long-bracketed form
        if (version >= LanguageVersion.Lua5_1) // start from Lua 5.1
        {
            // terminated string literal
            TestTermination(text: "[[]]", value: [], terminated: true);
            TestTermination(text: "[[[[]]", value: [.. "[["U8], terminated: true);
            TestTermination(text: "[=[]=]", value: [], terminated: true);
            TestTermination(text: "[==========[]==========]", value: [], terminated: true);
            TestTermination(text: "[=[]]=]", value: [.. "]"U8], terminated: true);
            TestTermination(text: "[=[[=[]=]", value: [.. "[=["U8], terminated: true);
            // unterminated string literal
            TestTermination(text: "[[", value: [], terminated: false);
            TestTermination(text: "[[]", value: [.. "]"U8], terminated: false);
            TestTermination(text: "[[[", value: [.. "["U8], terminated: false);
            TestTermination(text: "[[[]", value: [.. "[]"U8], terminated: false);
            TestTermination(text: "[=[]]", value: [.. "]]"U8], terminated: false);
            TestTermination(text: "[=[]==]", value: [.. "]==]"U8], terminated: false);
        }
        // bracketed form
        else
        {
            // terminated string literal
            TestTermination(text: "[[]]", value: [], terminated: true);
            TestTermination(text: "[[[[]]]]", value: [.. "[[]]"U8], terminated: true);
            // unterminated string literal
            TestTermination(text: "[[", value: [], terminated: false);
            TestTermination(text: "[[]", value: [.. "]"U8], terminated: false);
            TestTermination(text: "[[[", value: [.. "["U8], terminated: false);
            TestTermination(text: "[[[]", value: [.. "[]"U8], terminated: false);
            TestTermination(text: "[[[[]]", value: [.. "[[]]"U8], terminated: false);
            TestTermination(text: "[[[[]][[]]", value: [.. "[[]][[]]"U8], terminated: false);
        }

        void TestTermination(
            string text, byte[] value, bool terminated)
        {
            TestWith(text, value, SyntaxKind.MultiLineRawStringLiteralToken, options: options,
                diagnostics: terminated ? null : [Diagnostic(ErrorCode.ERR_UnterminatedStringLiteral, squiggledText: string.Empty).WithLocation(1, 1)]);
        }
    }

    [Theory]
    [MemberData(nameof(LanguageVersionTests.EffectiveLanguageVersions), MemberType = typeof(LanguageVersionTests))]
    public void SpecialEscapeSequence(LanguageVersion version)
    {
        var options = TestOptions.Regular.WithLanguageVersion(version);

        // \n \r \t
        if (true) // for all versions
        {
            TestValid(text: @"\n", value: [.. "\n"U8], options: options);
            TestValid(text: @"\r", value: [.. "\r"U8], options: options);
            TestValid(text: @"\t", value: [.. "\t"U8], options: options);
        }

        // \a \b \f \v \\ \" \'
        if (version >= LanguageVersion.Lua3_1) // start from Lua 3.1
        {
            TestValid(text: @"\a", value: [.. "\a"U8], options: options);
            TestValid(text: @"\b", value: [.. "\b"U8], options: options);
            TestValid(text: @"\f", value: [.. "\f"U8], options: options);
            TestValid(text: @"\v", value: [.. "\v"U8], options: options);
            TestValid(text: @"\\", value: [.. "\\"U8], options: options);
            TestValid(text: @"\""", value: [.. "\""U8], quote: '\'', options: options);
            TestValid(text: @"\'", value: [.. "'"U8], options: options);
            TestValid(text: @"\065", value: [.. "A"U8], options: options);
        }
        else
        {
            TestInvalid(text: @"\a", value: [.. "a"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: @"\b", value: [.. "b"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: @"\f", value: [.. "f"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: @"\v", value: [.. "v"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: @"\\", value: [.. ""U8], illegalEscapeOffsets: [0, 1], options: options);
            TestInvalid(text: @"\""", value: [.. "\""U8], quote: '\'', illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: @"\'", value: [.. "'"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: @"\065", value: [.. "065"U8], illegalEscapeOffsets: [0], options: options);
        }

        // \^CR \^LF \^CR\^LF
        if (version >= LanguageVersion.Lua4)
        {
            TestValid(text: "\\\r", value: [.. "\n"U8], options: options);
            TestValid(text: "\\\n", value: [.. "\n"U8], options: options);
            TestValid(text: "\\\r\n", value: [.. "\n"U8], options: options);
        }
        else
        {
            TestInvalid(text: "\\\r", value: [.. ""U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: "\\\n", value: [.. ""U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: "\\\r\n", value: [.. ""U8], illegalEscapeOffsets: [0], options: options);
        }

        // \[ \]
        if (version == LanguageVersion.Lua5)
        {
            TestValid(text: @"\[", value: [.. "["U8], options: options);
            TestValid(text: @"\]", value: [.. "]"U8], options: options);
        }
        else
        {
            TestInvalid(text: @"\[", value: [.. "["U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: @"\]", value: [.. "]"U8], illegalEscapeOffsets: [0], options: options);
        }

        // \xXX \z^WS \z^CR \z^LF \z^CR\^LF
        if (version >= LanguageVersion.Lua5_2)
        {
            TestValid(text: @"\x41", value: [.. "A"U8], options: options);
            TestValid(text: @"\z ", value: [.. ""U8], options: options);
            TestValid(text: "\\z\t", value: [.. ""U8], options: options);
            TestValid(text: "\\z\v", value: [.. ""U8], options: options);
            TestValid(text: "\\z\f", value: [.. ""U8], options: options);
            TestValid(text: "\\z\r", value: [.. ""U8], options: options);
            TestValid(text: "\\z\n", value: [.. ""U8], options: options);
            TestValid(text: "\\z\r\n", value: [.. ""U8], options: options);
        }
        else
        {
            TestInvalid(text: @"\x41", value: [.. "x41"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: @"\z ", value: [.. "z "U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: "\\z\t", value: [.. "z\t"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: "\\z\v", value: [.. "z\v"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: "\\z\f", value: [.. "z\f"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: "\\z\r", value: [.. "z"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: "\\z\n", value: [.. "z"U8], illegalEscapeOffsets: [0], options: options);
            TestInvalid(text: "\\z\r\n", value: [.. "z"U8], illegalEscapeOffsets: [0], options: options);
        }

        // \u{XXX}
        if (version >= LanguageVersion.Lua5_3)
        {
            TestValid(text: @"\u{137F}", value: [.. "\u137F"U8], options: options);
        }
        else
        {
            TestInvalid(text: @"\u{137F}", value: [.. "u{137F}"U8], illegalEscapeOffsets: [0], options: options);
        }

        static void TestValid(
            string text, byte[] value,
            char quote = '"',
            LuaParseOptions? options = null)
        {
            const string Leading = "c";
            const string Trailing = "d";

            var quotedTextProvider = GetQuotedTextProviderByQuote(quote);

            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: quotedTextProvider, options: options);
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: quotedTextProvider, leading: Leading, options: options);
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: quotedTextProvider, trailing: Trailing, options: options);
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: quotedTextProvider, leading: Leading, trailing: Trailing, options: options);
        }

        static void TestInvalid(
            string text, byte[] value,
            int[] illegalEscapeOffsets,
            char quote = '"',
            LuaParseOptions? options = null)
        {
            const string Leading = "c";
            const string Trailing = "d";

            void Validate(NodeValidator V, byte[] leadingValue, byte[] trailingValue)
            {
                var endsWithEOL = SyntaxFacts.IsNewLine(text[^1]);

                var builder = ArrayBuilder<DiagnosticDescription>.GetInstance();
                if (endsWithEOL)
                    builder.Add(Diagnostic(ErrorCode.ERR_NewlineInConst, squiggledText: string.Empty).WithLocation(1, 1));

                var buffer = trailingValue.Length > 0 ? text + Trailing + quote : text + quote;
                foreach (var offset in illegalEscapeOffsets)
                {
                    var squiggledText = buffer.Substring(offset, 2);
                    builder.Add(Diagnostic(ErrorCode.ERR_IllegalEscape, squiggledText).WithLocation(1, 1 + quote.ToString().Length + (leadingValue.Length > 0 ? Leading.Length : 0) + offset));
                }

                if (endsWithEOL)
                {
                    V(kind: SyntaxKind.StringLiteralToken, value: new Utf8String([.. leadingValue, .. value]), diagnostics: builder.ToArray());

                    string GetTrailingNewLine()
                    {
                        var newLine = string.Empty;
                        for (int length = text.Length, i = length - 1; i >= 0; i--)
                        {
                            var c = text[i];
                            if (SyntaxFacts.IsNewLine(c))
                                newLine = c + newLine;
                        }
                        return newLine;
                    }
                    V(kind: SyntaxKind.EndOfLineTrivia, text: GetTrailingNewLine(), location: TriviaLocation.Trailing);

                    if (trailingValue.Length > 0)
                        V(kind: SyntaxKind.IdentifierToken, text: Trailing);

                    V(kind: SyntaxKind.StringLiteralToken, value: Utf8String.Empty, diagnostics: [
                        Diagnostic(ErrorCode.ERR_NewlineInConst, squiggledText: string.Empty).WithLocation(1, 1)
                    ]);
                }
                else
                    V(kind: SyntaxKind.StringLiteralToken, value: new Utf8String([.. leadingValue, .. value, .. trailingValue]), diagnostics: builder.ToArray());

                V(SyntaxKind.EndOfFileToken);

                builder.Free();
            }

            var quotedTextProvider = GetQuotedTextProviderByQuote(quote);

            TestWith(text, validateAction: Validate, quotedTextProvider: quotedTextProvider, options: options);
            TestWith(text, validateAction: Validate, leading: Leading, quotedTextProvider: quotedTextProvider, options: options);
            TestWith(text, validateAction: Validate, trailing: Trailing, quotedTextProvider: quotedTextProvider, options: options);
            TestWith(text, validateAction: Validate, leading: Leading, trailing: Trailing, quotedTextProvider: quotedTextProvider, options: options);
        }
    }

    [Fact]
    public void DigitalEscapeSequence()
    {
        var sequences =
            from digit in Enumerable.Range(byte.MinValue, byte.MaxValue)
            let length = digit switch { < 10 => 1, < 100 => 2, _ => 3 }
            from count in Enumerable.Range(length, 4 - length)
            let format = "D" + count
            select ($"\\{digit.ToString(format)}", new byte[] { (byte)digit });

        foreach ((var text, var value) in sequences)
        {
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted);
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, leading: "a");
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, trailing: "b");
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, leading: "a", trailing: "b");
        }
    }

    [Fact]
    public void HexadecimalEscapeSequence()
    {
        var sequences =
            from digit in Enumerable.Range(byte.MinValue, byte.MaxValue)
            select ($"\\x{digit:X2}", new byte[] { (byte)digit });

        foreach ((var text, var value) in sequences)
        {
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted);
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, leading: "a");
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, trailing: "b");
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, leading: "a", trailing: "b");
        }
    }

    [Fact]
    public void UnicodeEscapeSequence()
    {
        var sequences =
            from digit in new int[] { 0x0, 0xF, 0x10, 0x7F, 0x80, 0xFF, 0x100, 0x7FF, 0x800, 0xFFF, 0x1000, 0xFFFF, 0x10000, 0xFFFFF, 0x100000, 0x1FFFFF, 0x200000, 0xFFFFFF, 0x1000000, 0x3FFFFFF, 0x4000000, 0xFFFFFFF, 0x10000000, 0x7FFFFFFF }
            let length = digit.ToString("X").Length
            from count in Enumerable.Range(length, 9 - length)
            let format = "X" + count
            select ($"\\u{{{digit.ToString(format)}}}", digit switch
            {
                <= 0x7F => new byte[] { (byte)digit },
                <= 0x7FF => [(byte)(0xC0 | (0x1F & digit >> 6)), (byte)(0x80 | (0x3F & digit))],
                <= 0xFFFF => [(byte)(0xE0 | (0xF & digit >> 12)), (byte)(0x80 | (0x3F & digit >> 6)), (byte)(0x80 | (0x3F & digit))],
                <= 0x1FFFFF => [(byte)(0xF0 | (0x7 & digit >> 18)), (byte)(0x80 | (0x3F & digit >> 12)), (byte)(0x80 | (0x3F & digit >> 6)), (byte)(0x80 | (0x3F & digit))],
                <= 0x3FFFFFF => [(byte)(0xF8 | (0x3 & digit >> 24)), (byte)(0x80 | (0x3F & digit >> 18)), (byte)(0x80 | (0x3F & digit >> 12)), (byte)(0x80 | (0x3F & digit >> 6)), (byte)(0x80 | (0x3F & digit))],
                _ => [(byte)(0xFC | (0x1 & digit >> 30)), (byte)(0x80 | (0x3F & digit >> 24)), (byte)(0x80 | (0x3F & digit >> 18)), (byte)(0x80 | (0x3F & digit >> 12)), (byte)(0x80 | (0x3F & digit >> 6)), (byte)(0x80 | (0x3F & digit))]
            });

        foreach ((var text, var value) in sequences)
        {
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted);
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, leading: " ");
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, trailing: " ");
            TestWith(text, value, kind: SyntaxKind.StringLiteralToken, quotedTextProvider: s_singleQuoted, leading: " ", trailing: " ");
        }
    }

    private static void TestWith(
        string text, byte[] value, SyntaxKind kind,
        string? leading = null, byte[]? leadingValue = null,
        string? trailing = null, byte[]? trailingValue = null,
        QuotedTextProvider? quotedTextProvider = null,
        DiagnosticDescription[]? diagnostics = null,
        LuaParseOptions? options = null)
        => TestWith(text, (V, leadingValue, trailingValue) =>
        {
            V = V.EndOfFile();
            V(kind, value: new Utf8String([.. leadingValue, .. value, .. trailingValue]), diagnostics: diagnostics);
        }, leading, leadingValue, trailing, trailingValue, quotedTextProvider, options);

    private static void TestWith(
        string text, Action<NodeValidator, byte[], byte[]> validateAction,
        string? leading = null, byte[]? leadingValue = null,
        string? trailing = null, byte[]? trailingValue = null,
        QuotedTextProvider? quotedTextProvider = null,
        LuaParseOptions? options = null)
    {
        leadingValue ??= leading is null ? [] : Encoding.UTF8.GetBytes(leading);
        trailingValue ??= trailing is null ? [] : Encoding.UTF8.GetBytes(trailing);

        var source = string.Concat(leading, text, trailing);
        if (quotedTextProvider is not null)
            source = quotedTextProvider(source);

        var V = LexSource(source, options, withTrivia: true);
        validateAction(V, leadingValue, trailingValue);
    }

    private static readonly QuotedTextProvider s_singleQuoted = GetQuotedTextProviderByQuote('\'');
    private static readonly QuotedTextProvider s_doubleQuoted = GetQuotedTextProviderByQuote('"');

    private static QuotedTextProvider GetQuotedTextProviderByQuote(char quote)
    {
        var quoteString = quote.ToString();
        return GetQuotedTextProviderByQuote(quoteString, quoteString);
    }

    private static QuotedTextProvider GetQuotedTextProviderByQuote(string openQuote, string closeQuote)
        => text => openQuote + text + closeQuote;
}
