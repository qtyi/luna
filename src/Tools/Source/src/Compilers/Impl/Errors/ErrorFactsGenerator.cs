// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Luna.Compilers.Generators.ErrorFacts;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Generators;

partial class ErrorFactsGenerator : AbstractErrorFactsGenerator
{
    protected override void GenerateOutputs(SourceProductionContext context, ImmutableDictionary<string, ImmutableArray<string>> categorizedErrorCodeNames)
    {
        WriteAndAddSource(context, ErrorFactsSourceWriter.WriteMain, new ErrorFactsSourceProductionContext(ThisLanguageName, categorizedErrorCodeNames), "ErrorFacts.Generated.cs");
    }
}

/// <summary>
/// Context passed to an <see cref="ErrorFactsSourceWriter"/> to start source production.
/// </summary>
internal readonly struct ErrorFactsSourceProductionContext
{
    public readonly string ThisLanguageName;
    public readonly ImmutableDictionary<string, ImmutableArray<string>> CategorizedErrorCodeNames;

    public ErrorFactsSourceProductionContext(string thisLanguageName, ImmutableDictionary<string, ImmutableArray<string>> categorizedErrorCodeNames)
    {
        ThisLanguageName = thisLanguageName;
        CategorizedErrorCodeNames = categorizedErrorCodeNames;
    }
}
