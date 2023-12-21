// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Simulators;

public struct SyntaxParserExecutionContext
{
    public ImmutableDictionary<string, object?> ParseOptions { get; }
    public SourceText SourceText { get; }
    public string FilePath { get; }
    public CancellationToken CancellationToken { get; }

    internal SyntaxParserExecutionContext(IEnumerable<KeyValuePair<string, object?>> options, SourceText sourceText, string filePath, CancellationToken cancellationToken)
    {
        this.ParseOptions = options.ToImmutableDictionary();
        this.SourceText = sourceText;
        this.FilePath = filePath;
        this.CancellationToken = cancellationToken;
    }
}
