// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Generators;

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
        this._sourceText = sourceText;
        this._position = 0;
    }

    /// <inheritdoc/>
    public override int Peek()
    {
        if (this._position == this._sourceText.Length)
            return -1;

        return this._sourceText[this._position];
    }

    /// <inheritdoc/>
    public override int Read()
    {
        if (this._position == this._sourceText.Length)
            return -1;

        return this._sourceText[this._position++];
    }

    /// <inheritdoc/>
    public override int Read(char[] buffer, int index, int count)
    {
        var charsToCopy = Math.Min(count, this._sourceText.Length - this._position);
        this._sourceText.CopyTo(this._position, buffer, index, charsToCopy);
        this._position += charsToCopy;
        return charsToCopy;
    }
}
