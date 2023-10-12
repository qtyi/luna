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

public abstract class TreeSourceGenerator<TTree, TTreeType> : AbstractTreeSourceGenerator<TTree, TTreeType>
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    protected sealed override void InitializeCore(in IncrementalGeneratorInitializationContext context)
    {
        if (this.TryGetRelevantInputs(in context, out var inputs))
            context.RegisterSourceOutput(inputs, this.GenerateOutputs);
    }

    protected abstract bool TryGetRelevantInputs(
        in IncrementalGeneratorInitializationContext context,
        out IncrementalValueProvider<ImmutableArray<AdditionalText>> inputs);

    private void GenerateOutputs(SourceProductionContext context, ImmutableArray<AdditionalText> inputs)
    {
        foreach (var tree in this.SerializeOutputs(context, inputs))
            this.GenerateOutputs(in context, tree, context.CancellationToken);
    }

    protected abstract void GenerateOutputs(
        in SourceProductionContext context,
        TTree tree,
        CancellationToken cancellationToken);
}
