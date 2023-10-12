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

public abstract class AbstractTreeSourceGenerator<TTree, TTreeType> : IIncrementalGenerator
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    #region DiagnosticDescriptors
    protected abstract DiagnosticDescriptor MissingInputDiagnosticDescriptor { get; }

    protected abstract DiagnosticDescriptor UnableToReadInputDiagnosticDescriptor { get; }

    protected abstract DiagnosticDescriptor InputSyntaxErrorDiagnosticDescriptor { get; }
    #endregion

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
#if DEBUG
        if (this.ShouldAttachDebugger && !Debugger.IsAttached)
        {
            Debugger.Launch();
        }
#endif

        this.InitializeCore(in context);
    }

    protected abstract void InitializeCore(in IncrementalGeneratorInitializationContext context);

#if DEBUG
    protected virtual bool ShouldAttachDebugger => false;
#endif

    protected IEnumerable<TTree> SerializeOutputs(SourceProductionContext context, ImmutableArray<AdditionalText> inputs)
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

    protected virtual void FlattenTree(TTree tree, CancellationToken cancellationToken) { }

    protected static void WriteAndAddSource(in SourceProductionContext context, Action<TextWriter> writeAction, string hintName)
    {
        // Write out the contents to a StringBuilder to avoid creating a single large string
        // in memory
        var stringBuilder = new StringBuilder();
        using (var textWriter = new StringWriter(stringBuilder))
        {
            writeAction(textWriter);
        }

        // And create a SourceText from the StringBuilder, once again avoiding allocating a single massive string
        var sourceText = SourceText.From(new StringBuilderReader(stringBuilder), stringBuilder.Length, encoding: Encoding.UTF8);
        context.AddSource(hintName, sourceText);
    }
}
