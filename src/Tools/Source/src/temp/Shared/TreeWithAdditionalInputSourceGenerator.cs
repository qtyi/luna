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

/// <summary>
/// Represents an incremental source generator that driven by not only a tree deserialized from XML, but also additional input.
/// </summary>
/// <inheritdoc/>
public abstract class TreeWithAdditionalInputSourceGenerator<TTree, TTreeType, TAdditionalInput> : AbstractTreeSourceGenerator<TTree, TTreeType>
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    /// <inheritdoc/>
    protected sealed override void Initialize(IncrementalGeneratorInitializationContext context)
    {
        if (this.TryGetRelevantInputs(context, out var inputs) &&
            this.TryGetAdditionalInputs(context, out var additionalInputs))
            context.RegisterSourceOutput(inputs.Combine(additionalInputs), this.GenerateOutputs);
    }

    /// <summary>
    /// Try to get additional inputs from <see cref="IncrementalGeneratorInitializationContext"/>.
    /// </summary>
    /// <param name="context">Incremental source generator context during initialization.</param>
    /// <param name="additionalInputs">Additional inputs found.</param>
    /// <returns>Returns <see langword="true"/> if we find additional inputs; otherwise, <see langword="false"/>.</returns>
    protected abstract bool TryGetAdditionalInputs(
        IncrementalGeneratorInitializationContext context,
        out IncrementalValueProvider<TAdditionalInput> additionalInputs);

    /// <summary>
    /// Generate source outputs from all input XML files.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="combinedInputs">A tuple of a collection of input XML files and additional inputs.</param>
    private void GenerateOutputs(SourceProductionContext context, (ImmutableArray<AdditionalText> Inputs, TAdditionalInput AdditionalInputs) combinedInputs)
    {
        foreach (var tree in this.SerializeInputs(context, combinedInputs.Inputs))
            this.GenerateOutputs(context, tree, combinedInputs.AdditionalInputs);
    }

    /// <summary>
    /// Generate source output from single tree.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="tree">Tree to generate source output.</param>
    /// <param name="additionalInput">Additional inputs to generate source output.</param>
    protected abstract void GenerateOutputs(
        SourceProductionContext context,
        TTree tree,
        TAdditionalInput additionalInput);

    /// <inheritdoc cref="AbstractSourceGenerator.WriteAndAddSource{T}(SourceProductionContext, Action{TextWriter, T, Compilation, CancellationToken}, T, string)"/>
    /// <param name="tree">The first parameter of the method that <paramref name="writeAction"/> encapsulates.</param>
    /// <param name="additionalInput">The second parameter of the method that <paramref name="writeAction"/> encapsulates.</param>
    protected static void WriteAndAddSource(SourceProductionContext context, Action<TextWriter, TTree, TAdditionalInput, Compilation, CancellationToken> writeAction, TTree tree, TAdditionalInput additionalInput, string hintName) => AbstractSourceGenerator.WriteAndAddSource(context, writeAction, tree, additionalInput, hintName);
}
