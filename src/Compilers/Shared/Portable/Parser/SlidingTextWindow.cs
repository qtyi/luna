// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;
using StringTable = Luna.Utilities.StringTable;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

/// <summary>
/// Keeps a sliding buffer over the <see cref="SourceText"/> of a file for the lexer. Also provides the lexer with the ability to keep track of a current "lexeme" by leaving a marker and advancing ahead the offset. The lexer can then decide to "keep" the lexeme by erasing the marker, or abandon the current lexeme by moving the offset back to the marker.
/// </summary>
#if DEBUG
[DebuggerStepThrough]
#endif
internal sealed class SlidingTextWindow : IDisposable
{
    /// <summary>
    /// A special <see cref="char"/> value indicate that there are no characters left and we have reached the end of the stream.
    /// </summary>
    /// <remarks>
    /// <para>In many cases, e.g. <see cref="PeekChar(int)"/>, we need the ability to indicate that there are no characters left and we have reached the end of the stream, or some other invalid or not present character was asked for. Due to perf concerns, things like nullable or out variables are not viable. Instead we need to choose a char value which can never be legal.</para>
    /// 
    /// <para>In .NET, all characters are represented in 16 bits using the UTF-16 encoding. Fortunately for us, there are a variety of different bit patterns which are *not* legal UTF-16 characters. 0xffff (<see cref="char.MaxValue"/>) is one of these characters -- a legal Unicode code point, but not a legal UTF-16 bit pattern.</para>
    /// </remarks>
    public const char InvalidCharacter = char.MaxValue;
    /// <summary>
    /// Default length of this buffer window.
    /// </summary>
    private const int DefaultWindowLength = 2048;

    /// <summary>
    /// Source of text to parse.
    /// </summary>
    private readonly SourceText _text;
    /// <summary>
    /// Offset of the window relative to the <see cref="_text"/> start.
    /// </summary>
    private int _basis;
    /// <summary>
    /// Offset from the start of the window.
    /// </summary>
    private int _offset;
    /// <summary>
    /// Absolute end position of <see cref="_text"/>.
    /// </summary>
    private readonly int _textEnd;
    /// <summary>
    /// Moveable window of characters from <see cref="_text"/>.
    /// </summary>
    private char[] _characterWindow;
    /// <summary>
    /// Number of valid characters in <see cref="_characterWindow"/>.
    /// </summary>
    private int _characterWindowCount;
    /// <summary>
    /// Start of current lexeme relative to the <see cref="_basis"/>.
    /// </summary>
    private int _lexemeStart;

    // Example for the above variables:
    // The text starts at 0.
    // The window onto the text starts at basis.
    // The current character is at (basis + offset), AKA the current "Position".
    // The current lexeme started at (basis + lexemeStart), which is <= (basis + offset)
    // The current lexeme is the characters between the lexemeStart and the offset.

    /// <summary>
    /// Table that stores string values we lexed.
    /// </summary>
    private readonly StringTable _strings;

    /// <summary>
    /// Object pool that creates and caches character window.
    /// </summary>
    private static readonly ObjectPool<char[]> s_windowPool = new(static () => new char[DefaultWindowLength]);

    /// <summary>
    /// Gets source of text to parse.
    /// </summary>
    public SourceText Text => _text;

    /// <summary>
    /// Gets the current absolute position in the text file.
    /// </summary>
    public int Position => _basis + _offset;

    /// <summary>
    /// Gets the current offset inside the window (relative to the window start).
    /// </summary>
    public int Offset => _offset;

    /// <summary>
    /// Gets the buffer backing the current window.
    /// </summary>
    public char[] CharacterWindow => _characterWindow;

    /// <summary>
    /// Number of characters in the character window.
    /// </summary>
    public int CharacterWindowCount => _characterWindowCount;

    /// <summary>
    /// Gets the start of the current lexeme relative to the window start.
    /// </summary>
    public int LexemeRelativeStart => _lexemeStart;

    /// <summary>
    /// Gets the absolute position of the start of the current lexeme in <see cref="Text"/>.
    /// </summary>
    public int LexemeStartPosition => _basis + _lexemeStart;

    /// <summary>
    /// Gets the number of characters in the current lexeme.
    /// </summary>
    public int Width => _offset - _lexemeStart;

    /// <summary>
    /// Create a new instance of <see cref="SlidingTextWindow"/> type with specified source text.
    /// </summary>
    /// <param name="text">Source text to read.</param>
    public SlidingTextWindow(SourceText text)
    {
        _text = text;
        _basis = 0;
        _offset = 0;
        _textEnd = text.Length;
        _strings = StringTable.GetInstance();
        _characterWindow = s_windowPool.Allocate();
        _lexemeStart = 0;
    }

#pragma warning disable CS8625
    public void Dispose()
    {
        if (_characterWindow is not null)
        {
            s_windowPool.Free(_characterWindow);
            _characterWindow = null;
            _strings.Free();
        }
    }
#pragma warning restore CS8625

    /// <summary>
    /// Start parsing a new lexeme.
    /// </summary>
    public void Start()
    {
        _lexemeStart = _offset;
    }

    /// <summary>
    /// 重置当前识别的字符的偏移量到指定的位置。
    /// </summary>
    /// <param name="position">要重置到的位置。</param>
    public void Reset(int position)
    {
        // 获取当前的相对位置。
        var relative = position - _basis;
        if (relative >= 0 && relative <= _characterWindowCount)
            // 若当前位置在已读取的字符范围中，则使用已有的字符缓冲数组。
            _offset = relative;
        else
        {
            // 需要重新读取文本缓冲数组。
            var amountToRead = Math.Max(
                0, // 读取字符数需大于0。
                Math.Min(
                    _text.Length, // 不能超过代码文本的结尾。
                    position + _characterWindow.Length
                ) - position
            );
            if (amountToRead > 0)
                // 填充字符缓冲数组。
                _text.CopyTo(position, _characterWindow, 0, amountToRead);

            _lexemeStart = 0;
            _offset = 0;
            _basis = position;
            _characterWindowCount = amountToRead;
        }
    }

    /// <summary>
    /// 移动或扩充字符缓冲数组以容纳更多的字符。
    /// </summary>
    /// <returns>若操作成功，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal bool MoreChars()
    {
        if (_offset >= _characterWindowCount)
        {
            if (Position >= _textEnd) return false; // 已经处理到代码文本的结尾。

            // 若标记扫描已很大程度地深入了字符缓冲数组，则滑动字符缓冲范围，使其起始位置对准当前识别到的标记的起始位置。
            if (_lexemeStart > (_characterWindowCount / 4))
            {
                // 将从标记起始位置开始的字符数据复制到缓冲数组的开头。
                Array.Copy(
                    _characterWindow, _lexemeStart,
                    _characterWindow, 0,
                    _characterWindowCount - _lexemeStart);

                _characterWindowCount -= _lexemeStart;
                _offset -= _lexemeStart;
                _basis += _lexemeStart;
                _lexemeStart = 0;
            }

            if (_characterWindowCount >= _characterWindow.Length)
            {
                // 扩大字符缓冲数组的容量以容纳后续更多的字符。
                var oldWindow = _characterWindow;
                var newWindow = new char[_characterWindow.Length * 2]; // 扩大两倍。
                Array.Copy(oldWindow, 0, newWindow, 0, _characterWindow.Length);
                s_windowPool.ForgetTrackedObject(oldWindow, newWindow);
                _characterWindow = newWindow;
            }

            var amountToRead = Math.Min(
                _textEnd - (_basis + CharacterWindowCount), // 不能超过代码文本的结尾。
                _characterWindow.Length - _characterWindowCount // 把缓冲数组读满。
            );
            _text.CopyTo(_basis + _characterWindowCount, _characterWindow, _characterWindowCount, amountToRead);
            _characterWindowCount += amountToRead;

            return amountToRead > 0; // 读取到更多字符。
        }

        return true;
    }

    /// <summary>
    /// 当前是否读到代码文本的结尾。
    /// </summary>
    /// <returns>若为<see langword="true"/>时，表示已读到代码文本的结尾；为<see langword="false"/>时，表示未读到代码文本的结尾。</returns>
    internal bool IsReallyAtEnd() => _offset >= _characterWindowCount && Position >= _textEnd;

    /// <summary>
    /// 将当前识别的字符偏移量向前推进<paramref name="n"/>个字符，不检查最终位置是否超出范围。
    /// </summary>
    /// <param name="n">将当前识别的字符偏移量向前推进的字符数。</param>
    public void AdvanceChar(int n = 1)
    {
        Debug.Assert(n >= 0);
        _offset += n;
    }

    /// <summary>
    /// 将当前识别的字符偏移量向前推进，越过当前紧跟着的换行字符序列。若当前紧跟着的不是换行字符序列，则只推进1个字符。
    /// </summary>
    public void AdvancePastNewLine() => AdvanceChar(GetNewLineWidth());

    /// <summary>
    /// 抓取下一个字符并推进字符偏移量1个字符位置。
    /// </summary>
    /// <returns>下一个字符。若已到达结尾，则返回<see cref="InvalidCharacter"/>。</returns>
    public char NextChar()
    {
        var c = PeekChar();
        if (c != InvalidCharacter)
            AdvanceChar();
        return c;
    }

    /// <summary>
    /// 查看后方第<paramref name="delta"/>位上的字符。
    /// </summary>
    /// <param name="delta">相对于当前识别的字符位置的偏移量。</param>
    /// <returns>后方第<paramref name="delta"/>位上的字符。若已到达结尾，则返回<see cref="InvalidCharacter"/>。</returns>
    public char PeekChar(int delta = 0)
    {
        var position = Position;
        AdvanceChar(delta);

        char c;
        if (_offset >= _characterWindowCount && !MoreChars())
            c = InvalidCharacter;
        else
            c = _characterWindow[_offset];

        Reset(position);
        return c;
    }

    /// <summary>
    /// 查看后方共<paramref name="count"/>位字符。
    /// </summary>
    /// <param name="count">查看的字符位数。</param>
    /// <returns>后方共<paramref name="count"/>位字符。若已到达结尾，则该位上的字符为<see cref="InvalidCharacter"/>。</returns>
    public string PeekChars(int count)
    {
        if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));
        else if (count is 0) return string.Empty;

        for (var i = 0; i < count; i++)
        {
            var position = _offset + i;
            if (position >= _characterWindowCount && !MoreChars())
            {
                count = i + 1;
                break;
            }
        }

        return GetText(_offset, count, intern: true);
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
            if (PeekChar(i) != desired[i]) return false;
        }

        AdvanceChar(length);
        return true;
    }

    /// <summary>
    /// 搜索与<see cref="StringBuilder"/>匹配的字符串。
    /// </summary>
    /// <returns>搜索到的字符串。</returns>
    public string Intern(StringBuilder text) => _strings.Add(text);

    /// <summary>
    /// 搜索与字符数组匹配的字符串。
    /// </summary>
    /// <returns>搜索到的字符串。</returns>
    public string Intern(char[] array, int start, int length) => _strings.Add(array, start, length);

    public Utf8String Intern(ArrayBuilder<byte> text)
    {
        var array = text.ToArray();
        return Intern(array, 0, array.Length);
    }

    public Utf8String Intern(byte[] array, int start, int length) => _strings.Add(array, start, length);

    /// <summary>
    /// 获取与当前识别到的标记匹配的字符串。
    /// </summary>
    /// <returns>与当前识别到的标记匹配的字符串。</returns>
    public string GetInternedText() => Intern(_characterWindow, _lexemeStart, Width);

    /// <summary>
    /// 获取表示当前识别到的标记的字符串。
    /// </summary>
    /// <param name="intern">是否搜索匹配的字符串。</param>
    /// <returns>表示当前识别到的标记的字符串。</returns>
    public string GetText(bool intern) => GetText(LexemeStartPosition, Width, intern);

    /// <summary>
    /// 获取一个范围内的字符串。
    /// </summary>
    /// <param name="position">相对于代码文本起始位置的偏移量。</param>
    /// <param name="length">字符范围的宽度。</param>
    /// <param name="intern">是否搜索匹配的字符串。</param>
    /// <returns></returns>
    public string GetText(int position, int length, bool intern)
    {
        var offset = position - _basis;

        switch (length)
        {
            case 0: return string.Empty;
            case 1:
                if (_characterWindow[offset] == ' ')
                    return " ";
                else if (_characterWindow[offset] == '\n')
                    return "\n";
                break;
            case 2:
                var firstChar = _characterWindow[offset];
                var nextChar = _characterWindow[offset + 1];
                if (firstChar == '\r' && nextChar == '\n')
                    return "\r\n";
                else if (firstChar == '-' && nextChar == '-')
                    return "--";
                break;
            case 3:
                if (_characterWindow[offset] == '-' &&
                    _characterWindow[offset + 1] == '-' &&
                    _characterWindow[offset + 2] == ' ')
                    return "-- ";
                break;
        }

        if (intern) return Intern(_characterWindow, offset, length);
        else return new string(_characterWindow, offset, length);
    }

    public int GetNewLineWidth() => GetNewLineWidth(PeekChar(0), PeekChar(1));

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

    public byte NextByteEscape(out SyntaxDiagnosticInfo? info)
    {
        Debug.Assert(PeekChar(0) == '\\' && (PeekChar(1) == 'x' || SyntaxFacts.IsDecDigit(PeekChar(1))));

        byte byteValue = 0;
        info = null;

        var start = Position;

        AdvanceChar();
        if (PeekChar() == 'x')
        {
            AdvanceChar();

            // 识别2位十六进制数字。
            if (SyntaxFacts.IsHexDigit(PeekChar()))
            {
                byteValue = (byte)SyntaxFacts.HexValue(NextChar());

                if (SyntaxFacts.IsHexDigit(PeekChar()))
                {
                    byteValue = (byte)((byteValue << 4) + SyntaxFacts.HexValue(NextChar()));
                }
                else info ??= CreateDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            }
            else info ??= CreateDiagnostic(start, ErrorCode.ERR_IllegalEscape);
        }
        // 识别3位十进制数字。
        else if (SyntaxFacts.IsDecDigit(PeekChar()))
        {
            var uintValue = (uint)SyntaxFacts.HexValue(NextChar());

            if (SyntaxFacts.IsDecDigit(PeekChar()))
            {
                uintValue = (uint)(uintValue * 10 + SyntaxFacts.HexValue(NextChar()));

                if (SyntaxFacts.IsDecDigit(PeekChar()))
                {
                    uintValue = (uint)(uintValue * 10 + SyntaxFacts.HexValue(NextChar()));
                }
            }

            if (uintValue > byte.MaxValue)
                info ??= CreateDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            else
                byteValue = (byte)uintValue;
        }

        return byteValue;
    }

    public byte[] NextUnicodeEscape(out SyntaxDiagnosticInfo? info)
    {
        info = null;

        var start = Position;

        var c = NextChar();
        Debug.Assert(c == '\\');

        c = NextChar();
        Debug.Assert(c == 'u');

        if (PeekChar() != '{') // 强制要求的左花括号。
        {
            info = CreateDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return [];
        }
        else
            AdvanceChar();

        if (!SyntaxFacts.IsHexDigit(PeekChar())) // 至少要有1位十六进制数字。
        {
            info = CreateDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return [];
        }
        else
            c = NextChar();

        // 最少识别1位十六进制数字，提前遇到非十六进制数字字符时中断。
        uint codepoint = 0;
        for (var i = 1; ; i++)
        {
            if (codepoint <= 0x7FFFFFFF)
                codepoint = (codepoint << 4) + (uint)SyntaxFacts.HexValue(c);
            if (codepoint > 0x7FFFFFFF)
                codepoint = uint.MaxValue;

            if (SyntaxFacts.IsHexDigit(PeekChar()))
                c = NextChar();
            else
                break;
        }

        if (PeekChar() != '}') // 强制要求的右花括号。
        {
            info ??= CreateDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return [];
        }
        else
            AdvanceChar();

        if (codepoint == uint.MaxValue)
        {
            info ??= CreateDiagnostic(start, ErrorCode.ERR_IllegalEscape);
            return [];
        }

        return GetUtf8BytesFromUnicode((int)codepoint);
    }

    internal static byte[] GetUtf8BytesFromUnicode(int codepoint) =>
        codepoint switch
        {
            <= 0x7F => [(byte)codepoint],
            <= 0x7FF => [(byte)(0xC0 | (0x1F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint))],
            <= 0xFFFF => [(byte)(0xE0 | (0xF & codepoint >> 12)), (byte)(0x80 | (0x3F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint))],
            <= 0x1FFFFF => [(byte)(0xF0 | (0x7 & codepoint >> 18)), (byte)(0x80 | (0x3F & codepoint >> 12)), (byte)(0x80 | (0x3F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint))],
            <= 0x3FFFFFF => [(byte)(0xF8 | (0x3 & codepoint >> 24)), (byte)(0x80 | (0x3F & codepoint >> 18)), (byte)(0x80 | (0x3F & codepoint >> 12)), (byte)(0x80 | (0x3F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint))],
            _ => [(byte)(0xFC | (0x1 & codepoint >> 30)), (byte)(0x80 | (0x3F & codepoint >> 24)), (byte)(0x80 | (0x3F & codepoint >> 18)), (byte)(0x80 | (0x3F & codepoint >> 12)), (byte)(0x80 | (0x3F & codepoint >> 6)), (byte)(0x80 | (0x3F & codepoint))]
        };

    internal char[] SingleOrSurrogatePair(out SyntaxDiagnosticInfo? info)
    {
        info = null;

        var start = Position;

        var high = PeekChar();
        if (high == InvalidCharacter && IsReallyAtEnd())
            return [];

        AdvanceChar();
        if (char.IsSurrogate(high))
        {
            var low = PeekChar();
            if ((low == InvalidCharacter && IsReallyAtEnd()) ||
                !char.IsSurrogatePair(high, low))
            {
                info ??= CreateDiagnostic(start, ErrorCode.ERR_UnpairedSurrogates);
                return [];
            }

            AdvanceChar();
            return [high, low];
        }
        else
            return [high];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private SyntaxDiagnosticInfo CreateDiagnostic(int start, ErrorCode code)
        => new(offset: start - LexemeStartPosition, width: Position - start, code);
}
