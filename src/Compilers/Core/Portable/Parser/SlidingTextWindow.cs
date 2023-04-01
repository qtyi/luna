// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis.PooledObjects;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Syntax.InternalSyntax;

/// <summary>
/// 为词法器分析的代码文本建立一个滑动的缓冲区域。通过设置标记及预先查看前方的字符，为词法器提供追踪当前“词素”的能力。词法器基于这些信息便可决定是移除标记保留词素，或是回退偏移量到标记的位置丢弃当前的词素。
/// </summary>
internal abstract class SlidingTextWindow : IDisposable
{
    /// <summary>
    /// 选取<see cref="char.MaxValue"/>作为代码中的非法字符，表示文件流已到达结尾或读取到无法识别的字符。
    /// </summary>
    public const char InvalidCharacter = char.MaxValue;
    /// <summary>
    /// 默认的缓冲区域的长度。
    /// </summary>
    protected const int DefaultWindowLength = 2048;

    /// <summary>
    /// 词法器解析的代码文本。
    /// </summary>
    protected readonly SourceText _text;
    /// <summary>
    /// 缓冲区域相对于代码文本的起始位置的偏移量。
    /// </summary>
    protected int _basis;
    /// <summary>
    /// 当前处理的字符相对于缓冲区域的起始位置的偏移量。
    /// </summary>
    protected int _offset;
    /// <summary>
    /// 代码文本的绝对的结束位置。
    /// </summary>
    protected readonly int _textEnd;
    /// <summary>
    /// 储存缓冲区域范围内的代码文本中的字符的数组。
    /// </summary>
    protected char[] _characterWindow;
    /// <summary>
    /// 字符数组中有效字符的数量。
    /// </summary>
    protected int _characterWindowCount;
    /// <summary>
    /// 当前识别到的词素的起始位置相对于缓冲区域的起始位置的偏移量。
    /// </summary>
    protected int _lexemeStart;

    /// <summary>
    /// 储存常用字符串的表。
    /// </summary>
    protected readonly StringTable _strings;

    /// <summary>
    /// 循环利用的对象池。
    /// </summary>
    protected static readonly ObjectPool<char[]> s_windowPool = new(() => new char[SlidingTextWindow.DefaultWindowLength]);

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
    /// 获取当前识别到的词素的起始位置相对于缓冲区域的起始位置的偏移量。
    /// </summary>
    public int LexemeRelativeStart => this._lexemeStart;

    /// <summary>
    /// 获取当前识别到的词素的起始位置相对于代码文本的起始位置的偏移量。
    /// </summary>
    public int LexemeStartPosition => this._basis + this._lexemeStart;

    /// <summary>
    /// 获取当前识别到的词素的宽度。
    /// </summary>
    public int Width => this._offset - this._lexemeStart;

    /// <summary>
    /// 使用代码文本初始化<see cref="SlidingTextWindow"/>的新实例。
    /// </summary>
    /// <param name="text"></param>
    protected SlidingTextWindow(SourceText text)
    {
        this._text = text;
        this._basis = 0;
        this._offset = 0;
        this._textEnd = text.Length;
        this._strings = StringTable.GetInstance();
        this._characterWindow = SlidingTextWindow.s_windowPool.Allocate();
        this._lexemeStart = 0;
    }

#pragma warning disable CS8625
    public void Dispose()
    {
        if (this._characterWindow is not null)
        {
            SlidingTextWindow.s_windowPool.Free(this._characterWindow);
            this._characterWindow = null;
            this._strings.Free();
        }
    }
#pragma warning restore CS8625

    /// <summary>
    /// 开始解析一个新词素。
    /// </summary>
    public virtual void Start() => this._lexemeStart = this._offset;

    /// <summary>
    /// 重置当前识别的字符的偏移量到指定的位置。
    /// </summary>
    /// <param name="position">要重置到的位置。</param>
    public virtual void Reset(int position)
    {
        // 获取当前的相对位置。
        int relative = position - this._basis;
        if (relative >= 0 && relative <= this._characterWindowCount)
            // 若当前位置在已读取的字符范围中，则使用已有的字符缓冲数组。
            this._offset = relative;
        else
        {
            // 需要重新读取文本缓冲数组。
            int amountToRead = Math.Max(
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
    protected internal bool MoreChars()
    {
        if (this._offset >= this._characterWindowCount)
        {
            if (this.Position >= this._textEnd) return false; // 已经处理到代码文本的结尾。

            // 若词素扫描已很大程度地深入了字符缓冲数组，则滑动字符缓冲范围，使其起始位置对准当前识别到的词素的起始位置。
            if (this._lexemeStart > (this._characterWindowCount / 4))
            {
                // 将从词素起始位置开始的字符数据复制到缓冲数组的开头。
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
                char[] oldWindow = this._characterWindow;
                char[] newWindow = new char[this._characterWindow.Length * 2]; // 扩大两倍。
                Array.Copy(oldWindow, 0, newWindow, 0, this._characterWindow.Length);
                SlidingTextWindow.s_windowPool.ForgetTrackedObject(oldWindow, newWindow);
                this._characterWindow = newWindow;
            }

            int amountToRead = Math.Min(
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
    internal virtual bool IsReallyAtEnd() => this._offset >= this._characterWindowCount && this.Position >= this._textEnd;

    /// <summary>
    /// 将当前识别的字符偏移量向前推进<paramref name="n"/>个字符，不检查最终位置是否超出范围。
    /// </summary>
    /// <param name="n"></param>
    [DebuggerStepThrough]
    public virtual void AdvanceChar(int n = 1) => this._offset += n;

    /// <summary>
    /// 将当前识别的字符偏移量向前推进，越过当前紧跟着的换行字符序列。若当前紧跟着的不是换行字符序列，则只推进1个字符。
    /// </summary>
    public virtual void AdvancePastNewLine() => this.AdvanceChar(this.GetNewLineWidth());

    /// <summary>
    /// 获取换行字符序列的宽度。
    /// </summary>
    /// <returns>换行字符序列的宽度。</returns>
    public abstract int GetNewLineWidth();

    /// <summary>
    /// 抓取下一个字符并推进字符偏移量1个字符位置。
    /// </summary>
    /// <returns>下一个字符。若已到达结尾，则返回<see cref="SlidingTextWindow.InvalidCharacter"/>。</returns>
    [DebuggerStepThrough]
    public virtual char NextChar()
    {
        char c = this.PeekChar();
        if (c != SlidingTextWindow.InvalidCharacter)
            this.AdvanceChar();
        return c;
    }

    /// <summary>
    /// 查看后方第<paramref name="delta"/>位上的字符。
    /// </summary>
    /// <param name="delta">相对于当前识别的字符位置的偏移量。</param>
    /// <returns>后方第<paramref name="delta"/>位上的字符。若已到达结尾，则返回<see cref="SlidingTextWindow.InvalidCharacter"/>。</returns>
    [DebuggerStepThrough]
    public virtual char PeekChar(int delta = 0)
    {
        int position = this.Position;
        this.AdvanceChar(delta);

        char c;
        if (this._offset >= this._characterWindowCount && !this.MoreChars())
            c = SlidingTextWindow.InvalidCharacter;
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
    public virtual string PeekChars(int count)
    {
        if (count is < 0) throw new ArgumentOutOfRangeException(nameof(count));
        else if (count is 0) return string.Empty;

        for (int i = 0; i < count; i++)
        {
            int position = this._offset + i;
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
    protected virtual bool AdvanceIfMatches(string desired)
    {
        int length = desired.Length;

        for (int i = 0; i < length; i++)
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
    public virtual string Intern(StringBuilder text) => this._strings.Add(text);

    /// <summary>
    /// 搜索与字符数组匹配的字符串。
    /// </summary>
    /// <returns>搜索到的字符串。</returns>
    [DebuggerStepThrough]
    public virtual string Intern(char[] array, int start, int length) => this._strings.Add(array, start, length);

    /// <summary>
    /// 获取与当前识别到的词素匹配的字符串。
    /// </summary>
    /// <returns>与当前识别到的词素匹配的字符串。</returns>
    [DebuggerStepThrough]
    public virtual string GetInternedText() => this.Intern(this._characterWindow, this._lexemeStart, this.Width);

    /// <summary>
    /// 获取表示当前识别到的词素的字符串。
    /// </summary>
    /// <param name="intern">是否搜索匹配的字符串。</param>
    /// <returns>表示当前识别到的词素的字符串。</returns>
    [DebuggerStepThrough]
    public virtual string GetText(bool intern) => this.GetText(this.LexemeStartPosition, this.Width, intern);

    /// <summary>
    /// 获取一个范围内的字符串。
    /// </summary>
    /// <param name="position">相对于代码文本起始位置的偏移量。</param>
    /// <param name="length">字符范围的宽度。</param>
    /// <param name="intern">是否搜索匹配的字符串。</param>
    /// <returns></returns>
    [DebuggerStepThrough]
    public abstract string GetText(int position, int length, bool intern);
}
