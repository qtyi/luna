// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents common parse options.
/// </summary>
/// <inheritdoc/>
public abstract class ParseOptions : Microsoft.CodeAnalysis.ParseOptions
{
    /// <summary>
    /// Gets the source language.
    /// </summary>
    /// <inheritdoc/>
    public abstract override string Language { get; }

    internal ParseOptions(SourceCodeKind kind, DocumentationMode documentationMode) : base(kind, documentationMode) { }
}
