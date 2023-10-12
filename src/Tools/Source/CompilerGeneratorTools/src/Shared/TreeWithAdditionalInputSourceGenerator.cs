// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using System.Xml;
using Luna.Compilers.Generators.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;
using System.Diagnostics;

namespace Luna.Compilers.Generators;

public abstract class TreeWithAdditionalInputSourceGenerator<TTree, TTreeType, TAdditionalInput> : AbstractTreeSourceGenerator<TTree, TTreeType>
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    protected sealed override void InitializeCore(in IncrementalGeneratorInitializationContext context)
    {
        if (this.TryGetRelevantInputs(in context, out var inputs) &&
            this.TryGetAdditionalInputs(in context, out var additionalInputs))
            context.RegisterSourceOutput(inputs.Combine(additionalInputs), this.GenerateOutputs);
    }

    protected abstract bool TryGetRelevantInputs(
        in IncrementalGeneratorInitializationContext context,
        out IncrementalValueProvider<ImmutableArray<AdditionalText>> inputs);

    protected abstract bool TryGetAdditionalInputs(
        in IncrementalGeneratorInitializationContext context,
        out IncrementalValueProvider<TAdditionalInput> additionalInputs);

    private void GenerateOutputs(SourceProductionContext context, (ImmutableArray<AdditionalText> Inputs, TAdditionalInput AdditionalInputs) combinedInputs)
    {
        foreach (var tree in this.SerializeOutputs(context, combinedInputs.Inputs))
            this.GenerateOutputs(in context, tree, combinedInputs.AdditionalInputs, context.CancellationToken);
    }

    protected abstract void GenerateOutputs(
        in SourceProductionContext context,
        TTree tree,
        TAdditionalInput additionalInput,
        CancellationToken cancellationToken);
}
