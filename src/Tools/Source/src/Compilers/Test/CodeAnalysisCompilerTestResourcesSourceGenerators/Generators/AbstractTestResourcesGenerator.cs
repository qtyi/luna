// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Luna.Tools;

public abstract class AbstractTestResourcesGenerator : AbstractSourceGenerator<ImmutableArray<AdditionalText>>
{
    /// <summary>
    /// Gets this generator name.
    /// </summary>
    protected abstract string GeneratorName { get; }

    protected override IncrementalValueProvider<ImmutableArray<AdditionalText>> GetRelevantInputs(IncrementalGeneratorInitializationContext context)
    {
        return context.AnalyzerConfigOptionsProvider.Combine(context.AdditionalTextsProvider.Collect())
            .Select((combined, cancellationToken) =>
                combined.Right
                    .WhereAsArray(text => combined.Left.GetOptions(text).TryGetValue("build_metadata.AdditionalFiles.GeneratorName", out var name) && GeneratorName == name)
                    .Sort(static (l, r) => string.CompareOrdinal(Path.GetFileName(l.Path), Path.GetFileName(r.Path)))
            );
    }
}
