// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Generators.ErrorFacts;

/// <summary>
/// Context passed to an <see cref="ErrorFactsSourceWriter"/> to start source production.
/// </summary>
internal readonly struct SourceFormatOptions
{
    public readonly string LanguageName;
    public readonly int IndentSize;
    public readonly char IndentChar;

    public SourceFormatOptions(string languageName, int indentSize, char indentChar)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(languageName));
        Debug.Assert(indentSize > 0);
        Debug.Assert(char.IsWhiteSpace(indentChar));

        LanguageName = languageName;
        IndentSize = indentSize;
        IndentChar = indentChar;
    }
}
