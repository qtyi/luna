// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Text;

namespace Luna.Tools;

/// <summary>
/// Represents a reader that can read source text.
/// </summary>
internal sealed class SourceTextReader : TextReader
{
    private readonly SourceText _sourceText;
    private int _position;

    /// <summary>
    /// Initialize an instance of type <see cref="SourceTextReader"/>.
    /// </summary>
    /// <param name="sourceText">Source text to read.</param>
    public SourceTextReader(SourceText sourceText)
    {
        _sourceText = sourceText;
        _position = 0;
    }

    /// <inheritdoc/>
    public override int Peek()
    {
        if (_position == _sourceText.Length)
            return -1;

        return _sourceText[_position];
    }

    /// <inheritdoc/>
    public override int Read()
    {
        if (_position == _sourceText.Length)
            return -1;

        return _sourceText[_position++];
    }

    /// <inheritdoc/>
    public override int Read(char[] buffer, int index, int count)
    {
        var charsToCopy = Math.Min(count, _sourceText.Length - _position);
        _sourceText.CopyTo(_position, buffer, index, charsToCopy);
        _position += charsToCopy;
        return charsToCopy;
    }
}
