// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Generators;

public abstract class TestResourcesGenerator : AbstractTestResourcesGenerator
{
    /// <inheritdoc/>
    protected override void InitializeCore(IncrementalGeneratorInitializationContext context)
    {
        var inputs = context.AnalyzerConfigOptionsProvider.Combine(context.AdditionalTextsProvider.Collect())
            .SelectMany((combined, _) =>
                this.GetRelevantTexts(combined.Left, combined.Right)
            );
        context.RegisterSourceOutput(inputs.Collect(), this.GenerateOutput);
    }

    /// <summary>
    /// Generate output from a collection of additional text files.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="texts">A collection of additional text files to generate source output.</param>
    protected abstract void GenerateOutput(
        SourceProductionContext context,
        ImmutableArray<AdditionalText> texts);
}
