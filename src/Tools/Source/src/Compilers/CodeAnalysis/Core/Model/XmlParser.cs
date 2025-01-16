// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;
using XmlFileWithSchema = (Microsoft.CodeAnalysis.AdditionalText source, Microsoft.CodeAnalysis.AdditionalText? schema);
using XmlObjectWithParseContext<TXmlObject, TOutput> = (TXmlObject xmlObject, Luna.Tools.Model.XmlParser<TOutput>.ParseContext context) where TXmlObject : System.Xml.Linq.XObject;

namespace Luna.Tools.Model;

internal abstract class XmlParser<TOutput>
{
    public TOutput? Parse(ImmutableArray<XmlFileWithSchema> files, DiagnosticBag diagnosticBag)
    {
        if (files.IsDefaultOrEmpty)
            return ParseDocuments([], diagnosticBag);

        var builder = ImmutableArray.CreateBuilder<XmlObjectWithParseContext<XDocument, TOutput>>();
        foreach (var sourceWithSchema in files)
        {
            var source = sourceWithSchema.source.GetTextOrReportDiagnostic(diagnosticBag);
            if (source is null)
                continue;

            // Create parse context.
            var context = new ParseContext(sourceWithSchema.source, source);

            var schema = sourceWithSchema.schema?.GetTextOrReportDiagnostic(diagnosticBag);

            var settings = new XmlReaderSettings();
            if (schema is null)
            {
                settings.DtdProcessing = DtdProcessing.Prohibit;
            }
            else
            {
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas.Add(targetNamespace: null, schemaDocument: XmlReader.Create(new SourceTextReader(schema)));
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationEventHandler += (object sender, ValidationEventArgs e) =>
                    ReportXmlExceptionDiagnostic(
                        diagnosticBag,
                        context,
                        e.Message,
                        e.Exception.LineNumber, e.Exception.LinePosition,
                        e.Severity switch
                        {
                            XmlSeverityType.Error => DiagnosticSeverity.Error,
                            XmlSeverityType.Warning => DiagnosticSeverity.Warning,
                            _ => throw ExceptionUtilities.UnexpectedValue(e.Severity)
                        }
                    );
            }

            var document = XDocument.Load(XmlReader.Create(new SourceTextReader(source), settings));
            builder.Add((document, context));
        }
        return ParseDocuments(builder.ToImmutable(), diagnosticBag);
    }

    protected abstract TOutput? ParseDocuments(ImmutableArray<XmlObjectWithParseContext<XDocument, TOutput>> documents, DiagnosticBag diagnosticBag);

    protected static void ReportXmlExceptionDiagnostic(
        DiagnosticBag diagnosticBag,
        ParseContext context,
        string message,
        int lineNumber, int linePosition,
        DiagnosticSeverity severity = DiagnosticSeverity.Error)
    {
        diagnosticBag.Add(
            Diagnostic.Create(
                DiagnosticDescriptors.XmlException(severity),
                GetLocation(context, lineNumber, linePosition),
                message
            )
        );
    }

    protected static void ReportXmlExceptionDiagnostic<TXmlLineInfo>(
        DiagnosticBag diagnosticBag,
        ParseContext context,
        string message,
        ImmutableArray<TXmlLineInfo> xmlLineInfos,
        DiagnosticSeverity severity = DiagnosticSeverity.Error)
        where TXmlLineInfo : IXmlLineInfo
        => ReportXmlExceptionDiagnostic(
            diagnosticBag,
            message,
            xmlLineInfos.SelectAsArray(info => GetLocation(context, info)),
            severity
        );

    protected static void ReportXmlExceptionDiagnostic(
        DiagnosticBag diagnosticBag,
        string message,
        ImmutableArray<Location> locations,
        DiagnosticSeverity severity = DiagnosticSeverity.Error)
    {
        Location? location = null;
        IEnumerable<Location>? additionalLocations = null;
        if (!locations.IsDefaultOrEmpty)
        {
            location = locations[0];
            if (locations.Length > 1)
            {
                var length = locations.Length - 1;
                var array = new Location[length];
                for (var i = 0; i < length; i++)
                {
                    array[i] = locations[i + 1];
                }
                additionalLocations = array;
            }
        }

        diagnosticBag.Add(
            Diagnostic.Create(
                DiagnosticDescriptors.XmlException(severity),
                location,
                additionalLocations,
                message
            )
        );
    }

    protected static Location GetLocation(ParseContext context, int lineNumber, int linePosition)
    {
        var line = context.SourceText.Lines[lineNumber - 1];    // LineNumber is one-based.
        int offset = linePosition - 1;                          // LinePosition is one-based
        var position = line.Start + offset;
        var span = new TextSpan(position, 0);
        var lineSpan = context.SourceText.Lines.GetLinePositionSpan(span);
        return Location.Create(context.AdditionalText.Path, span, lineSpan);
    }

    protected static Location GetLocation<TXmlLineInfo>(ParseContext context, TXmlLineInfo lineInfo)
        where TXmlLineInfo : IXmlLineInfo
        => GetLocation(context, lineInfo.LineNumber, lineInfo.LinePosition);

    internal readonly struct ParseContext
    {
        public readonly AdditionalText AdditionalText;
        public readonly SourceText SourceText;

        public ParseContext(AdditionalText additionalText, SourceText sourceText)
        {
            AdditionalText = additionalText;
            SourceText = sourceText;
        }
    }
}
