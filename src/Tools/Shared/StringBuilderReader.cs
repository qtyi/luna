// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;

namespace Luna.Tools;

/// <summary>
/// 表示可以读取字符串构造器的读取器。
/// </summary>
internal sealed class StringBuilderReader : TextReader
{
    private readonly StringBuilder _stringBuilder;
    private int _position;

    /// <summary>
    /// 使用指定的字符串构造器初始化<see cref="StringBuilderReader"/>的新实例。
    /// </summary>
    /// <param name="stringBuilder">要读取的字符串构造器。</param>
    public StringBuilderReader(StringBuilder stringBuilder)
    {
        _stringBuilder = stringBuilder;
        _position = 0;
    }

    public override int Peek()
    {
        if (_position == _stringBuilder.Length)
        {
            return -1;
        }

        return _stringBuilder[_position];
    }

    public override int Read()
    {
        if (_position == _stringBuilder.Length)
        {
            return -1;
        }

        return _stringBuilder[_position++];
    }

    public override int Read(char[] buffer, int index, int count)
    {
        var charsToCopy = Math.Min(count, _stringBuilder.Length - _position);
        _stringBuilder.CopyTo(_position, buffer, index, charsToCopy);
        _position += charsToCopy;
        return charsToCopy;
    }
}
