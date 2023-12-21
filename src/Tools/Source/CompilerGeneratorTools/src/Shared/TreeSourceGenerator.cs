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
/// Represents an incremental source generator that driven by a tree deserialized from XML.
/// </summary>
/// <inheritdoc/>
public abstract class TreeSourceGenerator<TTree, TTreeType> : AbstractTreeSourceGenerator<TTree, TTreeType>
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    /// <inheritdoc/>
    protected sealed override void InitializeCore(IncrementalGeneratorInitializationContext context)
    {
        if (this.TryGetRelevantInputs(context, out var inputs))
            context.RegisterSourceOutput(inputs, this.GenerateOutputs);
    }

    /// <summary>
    /// Generate source outputs from all input XML files.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="inputs">A collection of input XML files.</param>
    private void GenerateOutputs(SourceProductionContext context, ImmutableArray<AdditionalText> inputs)
    {
        foreach (var tree in this.SerializeInputs(context, inputs))
            this.GenerateOutputs(context, tree);
    }

    /// <summary>
    /// Generate source output from single tree.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="tree">Tree to generate source output.</param>
    protected abstract void GenerateOutputs(
        SourceProductionContext context,
        TTree tree);

    /// <inheritdoc cref="AbstractSourceGenerator.WriteAndAddSource{T}(SourceProductionContext, Action{TextWriter, T, CancellationToken}, T, string)"/>
    /// <param name="tree">The parameter of the method that <paramref name="writeAction"/> encapsulates.</param>
    protected static void WriteAndAddSource(SourceProductionContext context, Action<TextWriter, TTree, CancellationToken> writeAction, TTree tree, string hintName) => AbstractSourceGenerator.WriteAndAddSource(context, writeAction, tree, hintName);
}
