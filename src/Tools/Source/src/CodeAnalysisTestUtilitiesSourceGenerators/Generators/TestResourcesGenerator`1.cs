// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Luna.Compilers.Generators;

public abstract class TestResourcesGenerator<TInput> : AbstractTestResourcesGenerator
{
    /// <inheritdoc/>
    protected override void InitializeCore(IncrementalGeneratorInitializationContext context)
    {
        var inputs = context.AnalyzerConfigOptionsProvider.Combine(context.AdditionalTextsProvider.Collect()).Combine(this.GetRelevantInput(context))
            .Select((combined, _) =>
                (texts: this.GetRelevantTexts(combined.Left.Left, combined.Left.Right),
                 input: combined.Right)
            );
        context.RegisterSourceOutput(inputs, (context, combined) => this.GenerateOutput(context, combined.texts, combined.input));
    }

    protected abstract IncrementalValueProvider<TInput> GetRelevantInput(IncrementalGeneratorInitializationContext context);

    /// <summary>
    /// Generate output from a collection of additional text files.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="texts">A collection of additional text files to generate source output.</param>
    protected abstract void GenerateOutput(
        SourceProductionContext context,
        ImmutableArray<AdditionalText> texts,
        TInput input);
}
