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
/// Represents an incremental source generator that driven by a tree deserialized from XML.  This class is abstract.
/// </summary>
/// <typeparam name="TTree">Type of tree.</typeparam>
/// <typeparam name="TTreeType">Type of tree type.</typeparam>
public abstract class AbstractTreeSourceGenerator<TTree, TTreeType> : AbstractSourceGenerator
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    #region DiagnosticDescriptors
    /// <summary>
    /// Gets a description about missing-input-XML-file diagnostic.
    /// </summary>
    protected abstract DiagnosticDescriptor MissingInputDiagnosticDescriptor { get; }

    /// <summary>
    /// Gets a description about unable-to-read-input-XML-file diagnostic.
    /// </summary>
    protected abstract DiagnosticDescriptor UnableToReadInputDiagnosticDescriptor { get; }

    /// <summary>
    /// Gets a description about input-XML-file-has-syntax-error diagnostic.
    /// </summary>
    protected abstract DiagnosticDescriptor InputSyntaxErrorDiagnosticDescriptor { get; }
    #endregion

    /// <summary>
    /// Try to get relevant XML files from <see cref="IncrementalGeneratorInitializationContext.AdditionalTextsProvider"/>.
    /// </summary>
    /// <param name="context">Incremental source generator context during initialization.</param>
    /// <param name="inputs">Relevant XML files found.</param>
    /// <returns>Returns <see langword="true"/> if we find relevant XML files; otherwise, <see langword="false"/>.</returns>
    protected abstract bool TryGetRelevantInputs(
        IncrementalGeneratorInitializationContext context,
        out IncrementalValueProvider<ImmutableArray<AdditionalText>> inputs);

    /// <summary>
    /// Serialize trees in all input XML files, flatten them and report diagnostics.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="inputs">A collection of input XML files.</param>
    /// <returns>All trees serialized from <paramref name="inputs"/>.</returns>
    protected IEnumerable<TTree> SerializeInputs(SourceProductionContext context, ImmutableArray<AdditionalText> inputs)
    {
        if (inputs.Length == 0)
        {
            context.ReportDiagnostic(Diagnostic.Create(this.MissingInputDiagnosticDescriptor, location: null));
            yield break;
        }

        foreach (var input in inputs)
        {
            var inputText = input.GetText();
            if (inputText == null)
            {
                context.ReportDiagnostic(Diagnostic.Create(this.UnableToReadInputDiagnosticDescriptor, location: null));
                continue;
            }

            TTree tree;
            try
            {
                var reader = XmlReader.Create(new SourceTextReader(inputText), new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit });
                var serializer = new XmlSerializer(typeof(TTree));
                tree = (TTree)serializer.Deserialize(reader);
            }
            catch (InvalidOperationException ex) when (ex.InnerException is XmlException xmlException)
            {
                var line = inputText.Lines[xmlException.LineNumber - 1]; // LineNumber is one-based.
                int offset = xmlException.LinePosition - 1; // LinePosition is one-based
                var position = line.Start + offset;
                var span = new TextSpan(position, 0);
                var lineSpan = inputText.Lines.GetLinePositionSpan(span);

                context.ReportDiagnostic(Diagnostic.Create(
                    this.InputSyntaxErrorDiagnosticDescriptor,
                    location: Location.Create(input.Path, span, lineSpan),
                    xmlException.Message));
                continue;
            }

            this.FlattenTree(tree, context.CancellationToken);
            yield return tree;
        }
    }

    /// <summary>
    /// Flatten a tree.
    /// </summary>
    /// <param name="tree">The tree to flatten.</param>
    /// <param name="cancellationToken">Token that propagates notifications that this operation should be cancelled.</param>
    protected virtual void FlattenTree(TTree tree, CancellationToken cancellationToken) { }
}
