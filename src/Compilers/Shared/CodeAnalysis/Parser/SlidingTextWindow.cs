// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

/// <inheritdoc/>
internal sealed class SlidingTextWindow : Qtyi.CodeAnalysis.Syntax.InternalSyntax.SlidingTextWindow
{
    public SlidingTextWindow(SourceText text) : base(text) { }

    public override int GetNewLineWidth() => SlidingTextWindow.GetNewLineWidth(this.PeekChar(0), this.PeekChar(1));

    /// <summary>
    /// 获取换行字符序列的宽度。
    /// </summary>
    /// <param name="currentChar">第一个字符。</param>
    /// <param name="nextChars">后续的字符序列。</param>
    /// <returns>换行字符序列的宽度。</returns>
    public static int GetNewLineWidth(char currentChar, params char[] nextChars)
    {
        Debug.Assert(SyntaxFacts.IsNewLine(currentChar));

        if (nextChars.Length >= 1 && SyntaxFacts.IsNewLine(currentChar, nextChars[0]))
            // "\r\n"
            return 2;
        else
            // 其他1个字符宽度的换行字符序列。
            return 1;
    }

    public override string GetText(int position, int length, bool intern)
    {
        int offset = position - this._basis;

        switch (length)
        {
            case 0: return string.Empty;
            case 1:
                if (this._characterWindow[offset] == ' ')
                    return " ";
                else if (this._characterWindow[offset] == '\n')
                    return "\n";
                break;
            case 2:
                char firstChar = this._characterWindow[offset];
                char nextChar = this._characterWindow[offset + 1];
                if (firstChar == '\r' && nextChar == '\n')
                    return "\r\n";
                else if (firstChar == '-' && nextChar == '-')
                    return "--";
                break;
            case 3:
                if (this._characterWindow[offset] == '-' &&
                    this._characterWindow[offset + 1] == '-' &&
                    this._characterWindow[offset + 2] == ' ')
                    return "-- ";
                break;
        }

        if (intern) return this.Intern(this._characterWindow, offset, length);
        else return new string(this._characterWindow, offset, length);
    }

    public char NextByteEscape(out SyntaxDiagnosticInfo? info, out char surrogate)
    {
        Debug.Assert(this.PeekChar(0) == '\\' || this.PeekChar(1) == 'x' || SyntaxFacts.IsDecDigit(this.PeekChar(1)));

        int start = this.Position;

        /* 对于每个字节，都先检查NextByteEscapeCore的传出参数byteError，看是否在处理转义期间就产生了错误。
         * 若转义期间产生了错误，就直接终止后续扫描。
         * 再检查字节是否在指定位置应有的范围内，若不在则错误的范围应划定到此字节转义的前方（除了第一个字节）。
         * 第一个字节就不在范围内，则错误的范围应划定到此字节转义的后方（至少要吃掉一个字节转义）。
         */

        bool success = this.NextByteEscapeCore(out var firstByte, out var firstByteError);
        Debug.Assert(success); // 第一个byte必定能获取到。

        if (firstByteError is not null) // 此字节转义产生错误，立即中断后续扫描并返回。
        {
            info = firstByteError;
            surrogate = SlidingTextWindow.InvalidCharacter;
            return SlidingTextWindow.InvalidCharacter;
        }
        else if (!IsUtf8ByteSequenceValidAt(0, firstByte)) // 此字节不在指定位置应有的范围内，立即中断后续扫描并返回。
        {
            info = this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalUtf8ByteSequence);
            surrogate = SlidingTextWindow.InvalidCharacter;
            return SlidingTextWindow.InvalidCharacter;
        }

        // 获取第一个转义后字节指示的UTF-8字节序列总长度。
        int count = firstByte switch
        {
            <= 0b01111111 => 1,
            >= 0b11000000 and <= 0b11011111 => 2,
            >= 0b11100000 and <= 0b11101111 => 3,
            >= 0b11110000 and <= 0b11110111 => 4,
            _ => throw ExceptionUtilities.Unreachable // 前面已经检查过了。
        };
        var utf8Bytes = new byte[count];
        utf8Bytes[0] = firstByte;

        bool recovering = false; // 是否处于错误恢复状态（查找下一个起始字节）。
        for (int index = 1; index < count;)
        {
            int byteStart = this.Position;

            success = this.NextByteEscapeCore(out var byteValue, out var byteError);
            if (!success) // 后方不是字节转义，UTF-8字节序列未完成。
            {
                info = this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalUtf8ByteSequence);
                surrogate = SlidingTextWindow.InvalidCharacter;
                return SlidingTextWindow.InvalidCharacter;
            }
            else if (byteError is not null) // 此字节转义产生错误，立即中断后续扫描并返回。
            {
                info = byteError;
                surrogate = SlidingTextWindow.InvalidCharacter;
                return SlidingTextWindow.InvalidCharacter;
            }
            else if (IsUtf8ByteSequenceValidAt(0, byteValue)) // 此字节是下一个起始字节，则将前方的序列都划入错误范围，立即中断后续扫描并返回。
            {
                this.Reset(byteStart);

                info = this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalUtf8ByteSequence);
                surrogate = SlidingTextWindow.InvalidCharacter;
                return SlidingTextWindow.InvalidCharacter;
            }
            else if (recovering || !IsUtf8ByteSequenceValidAt(index, byteValue)) // 此字节不在指定位置应有的范围内。
            {
                // UTF-8字节序列出现错误字节，需要向后查找，直到找到另一个起始字节或到达结尾。
                // 由于我们限制了index的变化，因此循环将一直持续下去，必然会符合上方的某项失败条件。
                recovering = true; // 进入错误恢复状态。
                continue;
            }
            else // 终于找到正确的字节。
            {
                utf8Bytes[index] = byteValue;
                index++; // 处理下一个字节转义。
            }
        }

        // 不再会有错误了。
        info = null;
        uint codepoint = firstByte switch
        {
            <= 0b01111111 => firstByte,
            >= 0b11000000 and <= 0b11011111 => firstByte & (uint)0b11111,
            >= 0b11100000 and <= 0b11101111 => firstByte & (uint)0b1111,
            >= 0b11110000 and <= 0b11110111 => firstByte & (uint)0b111,
            _ => throw ExceptionUtilities.Unreachable // 前面已经检查过了。
        };
        for (int index = 1; index < utf8Bytes.Length; index++)
            codepoint = (codepoint << 6) + (utf8Bytes[index] & (uint)0b111111);
        return SlidingTextWindow.GetCharsFromUtf32(codepoint, out surrogate);
    }

    private bool NextByteEscapeCore(out byte byteValue, out SyntaxDiagnosticInfo? info)
    {
        byteValue = 0;
        info = null;

        char c;
        int start = this.Position;

        if (this.PeekChar(0) != '\\')
            return false;
        else if (this.PeekChar(1) == 'x')
        {
            this.AdvanceChar(2);

            // 识别2位十六进制数字。
            if (SyntaxFacts.IsHexDigit(this.PeekChar()))
            {
                c = this.NextChar();
                byteValue = (byte)SyntaxFacts.HexValue(c);

                if (SyntaxFacts.IsHexDigit(this.PeekChar()))
                {
                    c = this.NextChar();
                    byteValue = (byte)((byteValue << 4) + SyntaxFacts.HexValue(c));
                }
                else info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            }
            else info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);

            return true;
        }
        // 识别3位十进制数字。
        else if (SyntaxFacts.IsDecDigit(this.PeekChar(1)))
        {
            this.AdvanceChar(1);

            uint uintValue = 0;
            c = this.NextChar();
            uintValue = (uint)SyntaxFacts.HexValue(c);

            if (SyntaxFacts.IsDecDigit(this.PeekChar()))
            {
                c = this.NextChar();
                uintValue = (byte)(uintValue * 10 + SyntaxFacts.HexValue(c));

                if (SyntaxFacts.IsDecDigit(this.PeekChar()))
                {
                    c = this.NextChar();
                    uintValue = (byte)(uintValue * 10 + SyntaxFacts.HexValue(c));
                }
            }

            if (uintValue > byte.MaxValue)
                info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            else
                byteValue = (byte)uintValue;

            return true;
        }
        else
            return false;
    }

    private static bool IsUtf8ByteSequenceValidAt(int index, byte byteValue)
    {
        if (index == 0)
            return byteValue switch
            {
                <= 0b01111111 => true,
                >= 0b11000000 and <= 0b11011111 => true,
                >= 0b11100000 and <= 0b11101111 => true,
                >= 0b11110000 and <= 0b11110111 => true,
                _ => false
            };
        else
            return byteValue is >= 0b10000000 and <= 0b10111111;
    }

    public char NextUnicodeEscape(out SyntaxDiagnosticInfo? info, out char surrogate)
    {
        info = null;

        int start = this.Position;

        char c = this.NextChar();
        surrogate = SlidingTextWindow.InvalidCharacter;
        Debug.Assert(c == '\\');

        c = this.NextChar();
        Debug.Assert(c == 'u');

        if (this.PeekChar() != '{') // 强制要求的左花括号。
        {
            info = this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return SlidingTextWindow.InvalidCharacter;
        }
        else
            this.AdvanceChar();

        if (!SyntaxFacts.IsHexDigit(this.PeekChar())) // 至少要有1位十六进制数字。
        {
            info = this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return SlidingTextWindow.InvalidCharacter;
        }
        else
            c = this.NextChar();

        // 最少识别1位十六进制数字，提前遇到非十六进制数字字符时中断。
        uint codepoint = 0;
        for (int i = 1; ; i++)
        {
            if (codepoint <= 0x7FFFFFFF)
                codepoint = (codepoint << 4) + (uint)SyntaxFacts.HexValue(c);
            if (codepoint > 0x7FFFFFFF)
                codepoint = uint.MaxValue;

            if (SyntaxFacts.IsHexDigit(this.PeekChar()))
                c = this.NextChar();
            else
                break;
        }

        if (this.PeekChar() != '}') // 强制要求的右花括号。
        {
            info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return SlidingTextWindow.InvalidCharacter;
        }
        else
            this.AdvanceChar();

        if (codepoint == uint.MaxValue)
        {
            info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return SlidingTextWindow.InvalidCharacter;
        }

        return SlidingTextWindow.GetCharsFromUtf32(codepoint, out surrogate);
    }

    internal static char GetCharsFromUtf32(uint codepoint, out char lowSurrogate)
    {
        if (codepoint < 0x00010000)
        {
            lowSurrogate = SlidingTextWindow.InvalidCharacter;
            return (char)codepoint;
        }
        else if (codepoint > 0x0010FFFF)
        {
            lowSurrogate = SlidingTextWindow.InvalidCharacter;
            return SlidingTextWindow.InvalidCharacter;
        }
        else
        {
            lowSurrogate = (char)((codepoint - 0x00010000) % 0x0400 + 0xDC00);
            return (char)((codepoint - 0x00010000) / 0x0400 + 0xD800);
        }
    }

    private SyntaxDiagnosticInfo CreateIllegalEscapeDiagnostic(int start, ErrorCode code) =>
        new(
            start - this.LexemeStartPosition,
            this.Position - start,
            code);
}
