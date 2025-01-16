// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Luna.Tools;

/// <summary>
/// Represents a helper that can write a sequential series of characters with indents into a <see cref="TextWriter"/> instance.
/// </summary>
internal partial class IndentWriter
{
    /// <summary>
    /// Inner text writer that do the actual write job.
    /// </summary>
    protected readonly TextWriter Writer;

    /// <summary>
    /// Count of whitespace characters of every indent.
    /// </summary>
    protected readonly int IndentSize;
    /// <summary>
    /// Whitespace character of every indent.
    /// </summary>
    protected readonly char IndentChar;
    private int _indentLevel;
    private bool _needIndent = true;

    /// <summary>
    /// Initialize an instance of <see cref="IndentWriter"/>.
    /// </summary>
    /// <param name="writer">Output text writer.</param>
    public IndentWriter(TextWriter writer) : this(writer, 4, ' ') { }

    /// <inheritdoc cref="IndentWriter(TextWriter)"/>
    /// <param name="indentSize">Count of whitespace characters of every indent.</param>
    /// <param name="indentChar">Whitespace character of every indent.</param>
    public IndentWriter(TextWriter writer, int indentSize, char indentChar)
    {
        Debug.Assert(char.IsWhiteSpace(indentChar));

        Writer = writer;
        IndentSize = indentSize;
        IndentChar = indentChar;
    }

    /// <summary>
    /// Increase indent level.
    /// </summary>
    protected internal void Indent() => _indentLevel++;

    /// <summary>
    /// Decrease indent level.
    /// </summary>
    /// <exception cref="InvalidOperationException">Cannot unindent from base level.</exception>
    protected internal void Unindent()
    {
        if (_indentLevel <= 0) throw new InvalidOperationException("Cannot unindent from base level");

        _indentLevel--;
    }

    /// <inheritdoc cref="TextWriter.Write(char)"/>
    public void Write(char value)
    {
        WriteIndentIfNeeded();
        Writer.Write(value);
    }

    /// <inheritdoc cref="TextWriter.Write(string)"/>
    public void Write(string? value)
    {
        WriteIndentIfNeeded();
        Writer.Write(value);
    }

    /// <inheritdoc cref="TextWriter.Write(string, object)"/>
    public void Write(string format, object arg0) => Write(string.Format(format, arg0));

    /// <inheritdoc cref="TextWriter.Write(string, object, object)"/>
    public void Write(string format, object arg0, object arg1) => Write(string.Format(format, arg0, arg1));

    /// <inheritdoc cref="TextWriter.Write(string, object, object, object)"/>
    public void Write(string format, object arg0, object arg1, object arg2) => Write(string.Format(format, arg0, arg1, arg2));

    /// <inheritdoc cref="TextWriter.Write(string, object[])"/>
    public void Write(string format, params object[] args) => Write(string.Format(format, args));

    /// <inheritdoc cref="TextWriter.WriteLine()"/>
    public void WriteLine()
    {
        Writer.WriteLine();

        _needIndent = true; // Need an indent after each line break
    }

    /// <inheritdoc cref="TextWriter.WriteLine(char)"/>
    public void WriteLine(char value)
    {
        WriteIndentIfNeeded();
        Writer.WriteLine(value);

        _needIndent = true; // Need an indent after each line break
    }

    /// <inheritdoc cref="TextWriter.WriteLine(string)"/>
    public void WriteLine(string? value)
    {
        if (!string.IsNullOrEmpty(value))
            WriteIndentIfNeeded(); // Do not write indent if empty line.
        Writer.WriteLine(value);

        _needIndent = true; // Need an indent after each line break
    }

    /// <inheritdoc cref="TextWriter.WriteLine(string, object)"/>
    public void WriteLine(string format, object arg0) => WriteLine(string.Format(format, arg0));

    /// <inheritdoc cref="TextWriter.WriteLine(string, object, object)"/>
    public void WriteLine(string format, object arg0, object arg1) => WriteLine(string.Format(format, arg0, arg1));

    /// <inheritdoc cref="TextWriter.WriteLine(string, object, object, object)"/>
    public void WriteLine(string format, object arg0, object arg1, object arg2) => WriteLine(string.Format(format, arg0, arg1, arg2));

    /// <inheritdoc cref="TextWriter.WriteLine(string, object[])"/>
    public void WriteLine(string format, params object[] args) => WriteLine(string.Format(format, args));

    /// <summary>
    /// Writes a string followed by a line terminator to the text string or stream, but without leading indent.
    /// </summary>
    /// <inheritdoc cref="TextWriter.WriteLine(string)"/>
    public void WriteLineWithoutIndent(string? value)
    {
        Writer.WriteLine(value);
        _needIndent = true; // Need an indent after each line break
    }

    /// <summary>
    /// Writes out a formatted string and a new line, but without leading indent, using the same semantics as <see cref="string.Format(IFormatProvider, string, object[])"/>.
    /// </summary>
    /// <inheritdoc cref="TextWriter.WriteLine(string, object)"/>
    public void WriteLineWithoutIndent(string format, object arg0) => WriteLineWithoutIndent(string.Format(format, arg0));

    /// <summary>
    /// Writes out a formatted string and a new line, but without leading indent, using the same semantics as <see cref="string.Format(IFormatProvider, string, object[])"/>.
    /// </summary>
    /// <inheritdoc cref="TextWriter.WriteLine(string, object, object)"/>
    public void WriteLineWithoutIndent(string format, object arg0, object arg1) => WriteLineWithoutIndent(string.Format(format, arg0, arg1));

    /// <summary>
    /// Writes out a formatted string and a new line, but without leading indent, using the same semantics as <see cref="string.Format(IFormatProvider, string, object[])"/>.
    /// </summary>
    /// <inheritdoc cref="TextWriter.WriteLine(string, object, object, object)"/>
    public void WriteLineWithoutIndent(string format, object arg0, object arg1, object arg2) => WriteLineWithoutIndent(string.Format(format, arg0, arg1, arg2));

    /// <summary>
    /// Writes out a formatted string and a new line, but without leading indent, using the same semantics as <see cref="string.Format(IFormatProvider, string, object[])"/>.
    /// </summary>
    /// <inheritdoc cref="TextWriter.WriteLine(string, object[])"/>
    public void WriteLineWithoutIndent(string format, params object[] args) => WriteLineWithoutIndent(string.Format(format, args));

    /// <summary>
    /// Writes indent if <see cref="_needIndent"/> is <see langword="true"/>.
    /// </summary>
    private void WriteIndentIfNeeded()
    {
        if (_needIndent)
        {
            Writer.Write(new string(IndentChar, _indentLevel * IndentSize));
            _needIndent = false;
        }
    }

    #region Output helpers
    /// <summary>
    /// Joins all the values together in <paramref name="values"/> into one string with each
    /// value separated by a comma.
    /// </summary>
    /// <remarks>Empty strings are ignored.</remarks>
    /// <inheritdoc cref="Join(string, bool, object[])"/>
    protected static string CommaJoin(params object[] values)
        => Join(", ", ignoreEmptyEntries: true, values);

    /// <summary>
    /// Joins all the values together in <paramref name="values"/> into one string with each value separated by a particular separator.
    /// </summary>
    /// <param name="separator">Separate each value while joining.</param>
    /// <param name="ignoreEmptyEntries">A value that indicate if empty strings should be ignored.</param>
    /// <param name="values">Values to be joined together, can be either <see cref="string"/>s or <see cref="IEnumerable"/>s (of <see cref="string"/>s or <see cref="IEnumerable"/>s of ...).  All of these are flattened into a
    /// single sequence that is joined.</param>
    /// <returns>A string that is joined by <paramref name="values"/> with <paramref name="separator"/>.</returns>
    protected static string Join(string separator, bool ignoreEmptyEntries, params object[] values)
    {
        var flattenValues = FlattenValues(values);
        if (ignoreEmptyEntries)
            flattenValues = flattenValues.Where(static value => value != string.Empty);
        return string.Join(separator, flattenValues);

        static IEnumerable<string> FlattenValues(IEnumerable values)
        {
            foreach (var value in values)
            {
                switch (value)
                {
                    case string sValue:
                        yield return sValue;
                        break;

                    case IEnumerable<string> isValue:
                        foreach (var sValue in isValue)
                            yield return sValue;
                        break;

                    case IEnumerable iValue:
                        foreach (var sValue in FlattenValues(iValue))
                            yield return sValue;
                        break;

                    default:
                        throw new InvalidOperationException("Join must be passed strings or collections of strings");
                }
            }
        }
    }

    /// <summary>
    /// Strips a name with a particular heading.
    /// </summary>
    /// <param name="name">Name to strip.</param>
    /// <param name="pre">Heading of <paramref name="name"/> that is match.</param>
    /// <returns>Returns <paramref name="name"/> without heading <paramref name="pre"/>, or <paramref name="name"/> itself.</returns>
    protected static string StripPre(string name, string pre)
    {
        if (name.StartsWith(pre, StringComparison.Ordinal))
            return name[pre.Length..];
        else
            return name;
    }

    /// <summary>
    /// Strips a name with a particular ending.
    /// </summary>
    /// <param name="name">Name to strip.</param>
    /// <param name="post">Ending of <paramref name="name"/> that is match.</param>
    /// <returns>Returns <paramref name="name"/> without ending <paramref name="post"/>, or <paramref name="name"/> itself.</returns>
    protected static string StripPost(string name, string post)
    {
        if (name.EndsWith(post, StringComparison.Ordinal))
            return name[..^post.Length];
        else
            return name;
    }

    /// <summary>
    /// Converts a span of text to camel-case.
    /// </summary>
    /// <param name="name">Text to convert.</param>
    /// <returns><paramref name="name"/> in camel-case.</returns>
    protected static string CamelCase(string name)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(name));

        return string.Concat(
            SplitName(name).Select(static (name, index) =>
            {
                if (index == 0)
                    return name.ToLowerInvariant();
                else
                    return char.ToUpperInvariant(name[0]) + name[1..].ToLowerInvariant();
            })
        );
    }

    /// <summary>
    /// Converts a span of text to pascal-case.
    /// </summary>
    /// <param name="name">Text to convert.</param>
    /// <returns><paramref name="name"/> in pascal-case.</returns>
    protected static string PascalCase(string name)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(name));

        return string.Concat(
            from split in SplitName(name)
            select char.ToUpperInvariant(split[0]) + split[1..].ToLowerInvariant()
        );
    }

    /// <summary>
    /// Converts a span of text to snake-case.
    /// </summary>
    /// <param name="name">Text to convert.</param>
    /// <returns><paramref name="name"/> in snake-case.</returns>
    protected static string SnakeCase(string name)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(name));

        return string.Join("_",
            from split in SplitName(name)
            select split.ToLowerInvariant()
        );
    }

    /// <summary>
    /// Converts a span of text to spinal-case.
    /// </summary>
    /// <param name="name">Text to convert.</param>
    /// <returns><paramref name="name"/> in spinal-case.</returns>
    protected static string SpinalCase(string name)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(name));

        return string.Join("-",
            from split in SplitName(name)
            select split.ToLowerInvariant()
        );
    }

    private static IEnumerable<string> SplitName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            yield break;

        foreach (var split in SplitByKind(name))
        {
            if (char.IsDigit(split[0])) yield return split;

            var pos = 0;
            var len = 0;
            var isUpper = false;
            foreach (var c in split)
            {
                var newIsUpper = char.IsUpper(c);
                if (isUpper == newIsUpper)
                    len++;
                else
                {
                    if (len != 0)
                    {
                        if (isUpper && !newIsUpper)
                        {
                            // Like 'File', single upper-case letter trailing with a lower-case letter, we meat 'i' and continue to complete the word.
                            if (len == 1)
                            {
                                len++;
                                isUpper = newIsUpper;
                                continue;
                            }
                            // Like 'PEFile', multiple upper-case letters trailing with a lower-case letter, we want 'PE' and 'File' instead of 'PEFile'
                            else
                                len--;
                        }
                        yield return split.Substring(pos, len);
                    }
                    pos += len;
                    len = 1;
                    isUpper = newIsUpper;
                }
            }

            if (len != 0)
            {
                yield return split.Substring(pos, len);
            }
        }

        static IEnumerable<string> SplitByKind(string name)
        {
            var pos = 0;
            var len = 0;
            // 0 - whitespace
            // 1 - letter
            // 2 - digit
            // 3 - underscore or dash
            // others are not allowed
            var kind = 0;
            foreach (var c in name)
            {
                int newKind;
                if (char.IsWhiteSpace(c))
                    newKind = 0;
                else if (char.IsLetter(c))
                    newKind = 1;
                else if (char.IsDigit(c))
                    newKind = 2;
                else if (c is '-' or '_')
                    newKind = 3;
                else
                    throw ExceptionUtilities.UnexpectedValue(c);

                if (kind == newKind)
                    len++;
                else
                {
                    // We only return letters and digits.
                    if (len != 0 && kind is 1 or 2)
                        yield return name.Substring(pos, len);
                    pos += len;
                    len = 1;
                    kind = newKind;
                }
            }

            // We only return letters and digits.
            if (len != 0 && kind is 1 or 2)
                yield return name.Substring(pos, len);
        }
    }
    #endregion
}
