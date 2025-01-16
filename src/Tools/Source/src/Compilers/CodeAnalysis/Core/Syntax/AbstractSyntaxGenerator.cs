// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml.Serialization;
using System.Xml;
using Luna.Tools.Syntax.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Tools;

public abstract class AbstractSyntaxGenerator : AbstractSourceGenerator<ImmutableArray<AdditionalText>>
{
    protected const string Syntax_xml = "Syntax.xml";

    protected override IncrementalValueProvider<ImmutableArray<AdditionalText>> GetRelevantInputs(IncrementalGeneratorInitializationContext context)
    {
        return context.AdditionalTextsProvider.Where(text => Path.GetFileName(text.Path).Equals(Syntax_xml, StringComparison.OrdinalIgnoreCase)).Collect();
    }

    /// <summary>
    /// Serialize trees in all input XML files, flatten them and report diagnostics.
    /// </summary>
    /// <param name="context">Incremental source generator context during source production.</param>
    /// <param name="inputs">A collection of input XML files.</param>
    /// <returns>All trees serialized from <paramref name="inputs"/>.</returns>
    protected Syntax.Model.SyntaxTree? SerializeInputs(SourceProductionContext context, ImmutableArray<AdditionalText> inputs)
    {
        if (inputs.Length == 0)
        {
            //context.ReportDiagnostic(Diagnostic.Create(this.MissingInputDiagnosticDescriptor, location: null));
            return null;
        }

        var input = inputs[0];
        var inputText = input.GetText();
        if (inputText == null)
        {
            context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.FileCannotRead, location: null, input.Path));
            return null;
        }

        Syntax.Model.SyntaxTree tree;
        try
        {
            var reader = XmlReader.Create(new SourceTextReader(inputText), new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit });
            var serializer = new XmlSerializer(typeof(Syntax.Model.SyntaxTree));
            tree = (Syntax.Model.SyntaxTree)serializer.Deserialize(reader);
        }
        catch (InvalidOperationException ex) when (ex.InnerException is XmlException xmlException)
        {
            var line = inputText.Lines[xmlException.LineNumber - 1]; // LineNumber is one-based.
            int offset = xmlException.LinePosition - 1; // LinePosition is one-based
            var position = line.Start + offset;
            var span = new TextSpan(position, 0);
            var lineSpan = inputText.Lines.GetLinePositionSpan(span);

            context.ReportDiagnostic(Diagnostic.Create(
                DiagnosticDescriptors.XmlException(DiagnosticSeverity.Error),
                location: Location.Create(input.Path, span, lineSpan),
                xmlException.Message));
            return null;
        }

        SyntaxTreeFlattener.Instance.Flatten(tree, context.CancellationToken);
        return tree;

    }
}
