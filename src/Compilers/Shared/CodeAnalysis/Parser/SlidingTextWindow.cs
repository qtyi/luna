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

/// <summary>
/// 为词法器分析的代码文本建立一个滑动的缓冲区域。通过设置记号及预先查看前方的字符，为词法器提供追踪当前“词素”的能力。词法器基于这些信息便可决定是移除记号来保留词素，或是回退偏移量到记号的位置来丢弃当前的词素。
/// </summary>
internal sealed class SlidingTextWindow : IDisposable
{
    /// <summary>
    /// 选取<see cref="char.MaxValue"/>作为代码中的非法字符，表示文件流已到达结尾或读取到无法识别的字符。
    /// </summary>
    public const char InvalidCharacter = char.MaxValue;
    /// <summary>
    /// 默认的缓冲区域的长度。
    /// </summary>
    private const int DefaultWindowLength = 2048;

    /// <summary>
    /// 词法器解析的代码文本。
    /// </summary>
    private readonly SourceText _text;
    /// <summary>
    /// 缓冲区域相对于代码文本的起始位置的偏移量。
    /// </summary>
    private int _basis;
    /// <summary>
    /// 当前处理的字符相对于缓冲区域的起始位置的偏移量。
    /// </summary>
    private int _offset;
    /// <summary>
    /// 代码文本的结束的绝对位置。
    /// </summary>
    private readonly int _textEnd;
    /// <summary>
    /// 储存缓冲区域范围内的代码文本中的字符的数组。
    /// </summary>
    private char[] _characterWindow;
    /// <summary>
    /// 字符数组中有效字符的数量。
    /// </summary>
    private int _characterWindowCount;
    /// <summary>
    /// 当前识别到的词素的起始位置相对于缓冲区域的起始位置的偏移量。
    /// </summary>
    private int _lexemeStart;

    /// <summary>
    /// 储存常用字符串的表。
    /// </summary>
    private readonly StringTable _strings;

    /// <summary>
    /// 循环利用的对象池。
    /// </summary>
    private static readonly ObjectPool<char[]> s_windowPool = new(() => new char[DefaultWindowLength]);

    /// <summary>
    /// 获取词法器解析的代码文本。
    /// </summary>
    public SourceText Text => this._text;

    /// <summary>
    /// 获取当前处理的字符相对于代码文本的起始位置的偏移量。
    /// </summary>
    public int Position => this._basis + this._offset;

    /// <summary>
    /// 获取当前处理的字符相对于缓冲区域的起始位置的偏移量。
    /// </summary>
    public int Offset => this._offset;

    /// <summary>
    /// 获取储存缓冲区域范围内的代码文本中的字符的数组。
    /// </summary>
    public char[] CharacterWindow => this._characterWindow;

    /// <summary>
    /// 获取字符数组中有效字符的数量。
    /// </summary>
    public int CharacterWindowCount => this._characterWindowCount;

    /// <summary>
    /// 获取当前识别到的标记的起始位置相对于缓冲区域的起始位置的偏移量。
    /// </summary>
    public int LexemeRelativeStart => this._lexemeStart;

    /// <summary>
    /// 获取当前识别到的标记的起始位置相对于代码文本的起始位置的偏移量。
    /// </summary>
    public int LexemeStartPosition => this._basis + this._lexemeStart;

    /// <summary>
    /// 获取当前识别到的标记的宽度。
    /// </summary>
    public int Width => this._offset - this._lexemeStart;

    /// <summary>
    /// 使用代码文本初始化<see cref="SlidingTextWindow"/>的新实例。
    /// </summary>
    /// <param name="text"></param>
    public SlidingTextWindow(SourceText text)
    {
        this._text = text;
        this._basis = 0;
        this._offset = 0;
        this._textEnd = text.Length;
        this._strings = StringTable.GetInstance();
        this._characterWindow = s_windowPool.Allocate();
        this._lexemeStart = 0;
    }

#pragma warning disable CS8625
    public void Dispose()
    {
        if (this._characterWindow is not null)
        {
            s_windowPool.Free(this._characterWindow);
            this._characterWindow = null;
            this._strings.Free();
        }
    }
#pragma warning restore CS8625

    /// <summary>
    /// 开始解析一个新标记。
    /// </summary>
    public void Start() => this._lexemeStart = this._offset;

    /// <summary>
    /// 重置当前识别的字符的偏移量到指定的位置。
    /// </summary>
    /// <param name="position">要重置到的位置。</param>
    public void Reset(int position)
    {
        // 获取当前的相对位置。
        var relative = position - this._basis;
        if (relative >= 0 && relative <= this._characterWindowCount)
            // 若当前位置在已读取的字符范围中，则使用已有的字符缓冲数组。
            this._offset = relative;
        else
        {
            // 需要重新读取文本缓冲数组。
            var amountToRead = Math.Max(
                0, // 读取字符数需大于0。
                Math.Min(
                    this._text.Length, // 不能超过代码文本的结尾。
                    position + this._characterWindow.Length
                ) - position
            );
            if (amountToRead > 0)
                // 填充字符缓冲数组。
                this._text.CopyTo(position, this._characterWindow, 0, amountToRead);

            this._lexemeStart = 0;
            this._offset = 0;
            this._basis = position;
            this._characterWindowCount = amountToRead;
        }
    }

    /// <summary>
    /// 移动或扩充字符缓冲数组以容纳更多的字符。
    /// </summary>
    /// <returns>若操作成功，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal bool MoreChars()
    {
        if (this._offset >= this._characterWindowCount)
        {
            if (this.Position >= this._textEnd) return false; // 已经处理到代码文本的结尾。

            // 若标记扫描已很大程度地深入了字符缓冲数组，则滑动字符缓冲范围，使其起始位置对准当前识别到的标记的起始位置。
            if (this._lexemeStart > (this._characterWindowCount / 4))
            {
                // 将从标记起始位置开始的字符数据复制到缓冲数组的开头。
                Array.Copy(
                    this._characterWindow, this._lexemeStart,
                    this._characterWindow, 0,
                    this._characterWindowCount - this._lexemeStart);

                this._characterWindowCount -= this._lexemeStart;
                this._offset -= this._lexemeStart;
                this._basis += this._lexemeStart;
                this._lexemeStart = 0;
            }

            if (this._characterWindowCount >= this._characterWindow.Length)
            {
                // 扩大字符缓冲数组的容量以容纳后续更多的字符。
                var oldWindow = this._characterWindow;
                var newWindow = new char[this._characterWindow.Length * 2]; // 扩大两倍。
                Array.Copy(oldWindow, 0, newWindow, 0, this._characterWindow.Length);
                s_windowPool.ForgetTrackedObject(oldWindow, newWindow);
                this._characterWindow = newWindow;
            }

            var amountToRead = Math.Min(
                this._textEnd - (this._basis + this.CharacterWindowCount), // 不能超过代码文本的结尾。
                this._characterWindow.Length - this._characterWindowCount // 把缓冲数组读满。
            );
            this._text.CopyTo(this._basis + this._characterWindowCount, this._characterWindow, this._characterWindowCount, amountToRead);
            this._characterWindowCount += amountToRead;

            return amountToRead > 0; // 读取到更多字符。
        }

        return true;
    }

    /// <summary>
    /// 当前是否读到代码文本的结尾。
    /// </summary>
    /// <returns>若为<see langword="true"/>时，表示已读到代码文本的结尾；为<see langword="false"/>时，表示未读到代码文本的结尾。</returns>
    internal bool IsReallyAtEnd() => this._offset >= this._characterWindowCount && this.Position >= this._textEnd;

    /// <summary>
    /// 将当前识别的字符偏移量向前推进<paramref name="n"/>个字符，不检查最终位置是否超出范围。
    /// </summary>
    /// <param name="n">将当前识别的字符偏移量向前推进的字符数。</param>
    [DebuggerStepThrough]
    public void AdvanceChar(int n = 1)
    {
        Debug.Assert(n >= 0);
        this._offset += n;
    }

    /// <summary>
    /// 将当前识别的字符偏移量向前推进，越过当前紧跟着的换行字符序列。若当前紧跟着的不是换行字符序列，则只推进1个字符。
    /// </summary>
    [DebuggerStepThrough]
    public void AdvancePastNewLine() => this.AdvanceChar(this.GetNewLineWidth());

    /// <summary>
    /// 抓取下一个字符并推进字符偏移量1个字符位置。
    /// </summary>
    /// <returns>下一个字符。若已到达结尾，则返回<see cref="SlidingTextWindow.InvalidCharacter"/>。</returns>
    [DebuggerStepThrough]
    public char NextChar()
    {
        var c = this.PeekChar();
        if (c != InvalidCharacter)
            this.AdvanceChar();
        return c;
    }

    /// <summary>
    /// 查看后方第<paramref name="delta"/>位上的字符。
    /// </summary>
    /// <param name="delta">相对于当前识别的字符位置的偏移量。</param>
    /// <returns>后方第<paramref name="delta"/>位上的字符。若已到达结尾，则返回<see cref="SlidingTextWindow.InvalidCharacter"/>。</returns>
    [DebuggerStepThrough]
    public char PeekChar(int delta = 0)
    {
        var position = this.Position;
        this.AdvanceChar(delta);

        char c;
        if (this._offset >= this._characterWindowCount && !this.MoreChars())
            c = InvalidCharacter;
        else
            c = this._characterWindow[this._offset];

        this.Reset(position);
        return c;
    }

    /// <summary>
    /// 查看后方共<paramref name="count"/>位字符。
    /// </summary>
    /// <param name="count">查看的字符位数。</param>
    /// <returns>后方共<paramref name="count"/>位字符。若已到达结尾，则该位上的字符为<see cref="SlidingTextWindow.InvalidCharacter"/>。</returns>
    [DebuggerStepThrough]
    public string PeekChars(int count)
    {
        if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));
        else if (count is 0) return string.Empty;

        for (var i = 0; i < count; i++)
        {
            var position = this._offset + i;
            if (position >= this._characterWindowCount && !this.MoreChars())
            {
                count = i + 1;
                break;
            }
        }

        return this.GetText(this._offset, count, intern: true);
    }

    /// <summary>
    /// 若紧跟着的字符序列匹配指定的字符串，则推进字符偏移量，越过匹配的字符串；否则不做任何变动。
    /// </summary>
    /// <param name="desired">作为匹配对象的字符串。</param>
    /// <returns>若为<see langword="true"/>时，表示已匹配并推进；为<see langword="false"/>时，表示未推进。</returns>
    private bool AdvanceIfMatches(string desired)
    {
        var length = desired.Length;

        for (var i = 0; i < length; i++)
        {
            if (this.PeekChar(i) != desired[i]) return false;
        }

        this.AdvanceChar(length);
        return true;
    }

    /// <summary>
    /// 搜索与<see cref="StringBuilder"/>匹配的字符串。
    /// </summary>
    /// <returns>搜索到的字符串。</returns>
    [DebuggerStepThrough]
    public string Intern(StringBuilder text) => this._strings.Add(text);

    /// <summary>
    /// 搜索与字符数组匹配的字符串。
    /// </summary>
    /// <returns>搜索到的字符串。</returns>
    [DebuggerStepThrough]
    public string Intern(char[] array, int start, int length) => this._strings.Add(array, start, length);

    /// <summary>
    /// 获取与当前识别到的标记匹配的字符串。
    /// </summary>
    /// <returns>与当前识别到的标记匹配的字符串。</returns>
    [DebuggerStepThrough]
    public string GetInternedText() => this.Intern(this._characterWindow, this._lexemeStart, this.Width);

    /// <summary>
    /// 获取表示当前识别到的标记的字符串。
    /// </summary>
    /// <param name="intern">是否搜索匹配的字符串。</param>
    /// <returns>表示当前识别到的标记的字符串。</returns>
    [DebuggerStepThrough]
    public string GetText(bool intern) => this.GetText(this.LexemeStartPosition, this.Width, intern);

    /// <summary>
    /// 获取一个范围内的字符串。
    /// </summary>
    /// <param name="position">相对于代码文本起始位置的偏移量。</param>
    /// <param name="length">字符范围的宽度。</param>
    /// <param name="intern">是否搜索匹配的字符串。</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    public string GetText(int position, int length, bool intern)
    {
        var offset = position - this._basis;

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
                var firstChar = this._characterWindow[offset];
                var nextChar = this._characterWindow[offset + 1];
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

    [DebuggerStepThrough]
    public int GetNewLineWidth() => GetNewLineWidth(this.PeekChar(0), this.PeekChar(1));

    /// <summary>
    /// 获取换行字符序列的宽度。
    /// </summary>
    /// <param name="currentChar">第一个字符。</param>
    /// <param name="nextChars">后续的字符序列。</param>
    /// <returns>换行字符序列的宽度。</returns>
    [DebuggerStepThrough]
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

    public byte NextByteEscape(out SyntaxDiagnosticInfo? info)
    {
        Debug.Assert(this.PeekChar(0) == '\\' && (this.PeekChar(1) == 'x' || SyntaxFacts.IsDecDigit(this.PeekChar(1))));

        byte byteValue = 0;
        info = null;

        var start = this.Position;

        this.AdvanceChar();
        if (this.PeekChar() == 'x')
        {
            this.AdvanceChar();

            // 识别2位十六进制数字。
            if (SyntaxFacts.IsHexDigit(this.PeekChar()))
            {
                byteValue = (byte)SyntaxFacts.HexValue(this.NextChar());

                if (SyntaxFacts.IsHexDigit(this.PeekChar()))
                {
                    byteValue = (byte)((byteValue << 4) + SyntaxFacts.HexValue(this.NextChar()));
                }
                else info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            }
            else info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
        }
        // 识别3位十进制数字。
        else if (SyntaxFacts.IsDecDigit(this.PeekChar()))
        {
            var uintValue = (uint)SyntaxFacts.HexValue(this.NextChar());

            if (SyntaxFacts.IsDecDigit(this.PeekChar()))
            {
                uintValue = (uint)(uintValue * 10 + SyntaxFacts.HexValue(this.NextChar()));

                if (SyntaxFacts.IsDecDigit(this.PeekChar()))
                {
                    uintValue = (uint)(uintValue * 10 + SyntaxFacts.HexValue(this.NextChar()));
                }
            }

            if (uintValue > byte.MaxValue)
                info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            else
                byteValue = (byte)uintValue;
        }

        return byteValue;
    }

    public byte[] NextUnicodeEscape(out SyntaxDiagnosticInfo? info)
    {
        info = null;

        var start = this.Position;

        var c = this.NextChar();
        Debug.Assert(c == '\\');

        c = this.NextChar();
        Debug.Assert(c == 'u');

        if (this.PeekChar() != '{') // 强制要求的左花括号。
        {
            info = this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return Array.Empty<byte>();
        }
        else
            this.AdvanceChar();

        if (!SyntaxFacts.IsHexDigit(this.PeekChar())) // 至少要有1位十六进制数字。
        {
            info = this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return Array.Empty<byte>();
        }
        else
            c = this.NextChar();

        // 最少识别1位十六进制数字，提前遇到非十六进制数字字符时中断。
        uint codepoint = 0;
        for (var i = 1; ; i++)
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
            return Array.Empty<byte>();
        }
        else
            this.AdvanceChar();

        if (codepoint == uint.MaxValue)
        {
            info ??= this.CreateIllegalEscapeDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return Array.Empty<byte>();
        }

        return GetUtf8BytesFromUnicode((int)codepoint);
    }

    internal static byte[] GetUtf8BytesFromUnicode(int codepoint) =>
        codepoint switch
        {
            <= 0x7F => new byte[] { (byte)codepoint },
            <= 0x7FF => new byte[] { (byte)(0xC0 | (0x1F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint)) },
            <= 0xFFFF => new byte[] { (byte)(0xE0 | (0xF & codepoint >> 12)), (byte)(0x80 | (0x3F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint)) },
            <= 0x1FFFFF => new byte[] { (byte)(0xF0 | (0x7 & codepoint >> 18)), (byte)(0x80 | (0x3F & codepoint >> 12)), (byte)(0x80 | (0x3F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint)) },
            <= 0x3FFFFFF => new byte[] { (byte)(0xF8 | (0x3 & codepoint >> 24)), (byte)(0x80 | (0x3F & codepoint >> 18)), (byte)(0x80 | (0x3F & codepoint >> 12)), (byte)(0x80 | (0x3F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint)) },
            _ => new byte[] { (byte)(0xFC | (0x1 & codepoint >> 30)), (byte)(0x80 | (0x3F & codepoint >> 24)), (byte)(0x80 | (0x3F & codepoint >> 18)), (byte)(0x80 | (0x3F & codepoint >> 12)), (byte)(0x80 | (0x3F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint)) }
        };

    private SyntaxDiagnosticInfo CreateIllegalEscapeDiagnostic(int start, ErrorCode code) =>
        new(
            start - this.LexemeStartPosition,
            this.Position - start,
            code);
}
