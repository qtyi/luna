// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Generators.ErrorFacts;

/// <summary>
/// Context passed to an <see cref="ErrorFactsSourceWriter"/> to start source production.
/// </summary>
internal readonly struct ErrorFactsSourceProductionContext
{
    public readonly SourceFormatOptions FormatOptions;
    public readonly DiagnosticBag Diagnostics;
    public readonly Compilation Compilation;
    public readonly ImmutableDictionary<string, ImmutableArray<string>> ErrorCodeNames;

    public ErrorFactsSourceProductionContext(SourceProductionContext generatorContext, SourceFormatOptions formatOptions, ImmutableDictionary<string, ImmutableArray<string>> categorizedErrorCodeNames)
    {
        FormatOptions = formatOptions;
        Diagnostics = generatorContext.Diagnostics;
        Compilation = generatorContext.Compilation;
        ErrorCodeNames = categorizedErrorCodeNames;
    }
}
