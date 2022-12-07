// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Generators;

/// <summary>
/// 表示可以读取源代码文本的读取器。
/// </summary>
internal sealed class SourceTextReader : TextReader
{
    private readonly SourceText _sourceText;
    private int _position;

    /// <summary>
    /// 使用指定的源代码文本初始化<see cref="SourceTextReader"/>的新实例。
    /// </summary>
    /// <param name="sourceText">要读取的源代码文本。</param>
    public SourceTextReader(SourceText sourceText)
    {
        _sourceText = sourceText;
        _position = 0;
    }

    public override int Peek()
    {
        if (_position == _sourceText.Length)
            return -1;

        return _sourceText[_position];
    }

    public override int Read()
    {
        if (_position == _sourceText.Length)
        {
            return -1;
        }

        return _sourceText[_position++];
    }

    public override int Read(char[] buffer, int index, int count)
    {
        var charsToCopy = Math.Min(count, _sourceText.Length - _position);
        _sourceText.CopyTo(_position, buffer, index, charsToCopy);
        _position += charsToCopy;
        return charsToCopy;
    }
}
