// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Luna.Compilers.Generators;

public abstract class AbstractTestResourcesGenerator : AbstractSourceGenerator
{
    /// <summary>
    /// Gets this generator name.
    /// </summary>
    protected abstract string GeneratorName { get; }

    /// <summary>
    /// Gets an ordered collection of <see cref="AdditionalText"/> that this generator accepts.
    /// </summary>
    protected ImmutableArray<AdditionalText> GetRelevantTexts(AnalyzerConfigOptionsProvider provider, ImmutableArray<AdditionalText> texts) =>
        texts.Where(text => provider.GetOptions(text).TryGetValue("build_metadata.AdditionalFiles.GeneratorName", out var name) && this.GeneratorName == name)
            .OrderBy(static text => Path.GetFileName(text.Path))
            .ToImmutableArray();
}
