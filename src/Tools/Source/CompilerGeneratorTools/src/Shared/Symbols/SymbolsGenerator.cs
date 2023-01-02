// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Text;
using MSCA::System.Diagnostics.CodeAnalysis;
using Luna.Compilers.Generators.Symbols.Model;
using Luna.Compilers.Generators.Symbols;

namespace Luna.Compilers.Generators;

[Generator]
public sealed class SymbolsGenerator : CachingSourceGenerator
{
    private static readonly DiagnosticDescriptor s_MissingSymbolsXml = new(
        "CSSG1001",
        title: "Symbols.xml is missing",
        messageFormat: "The Symbols.xml file was not included in the project, so we are not generating source.",
        category: "SymbolsGenerator",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor s_UnableToReadSymbolsXml = new(
        "CSSG1002",
        title: "Symbols.xml could not be read",
        messageFormat: "The Symbols.xml file could not even be read. Does it exist?",
        category: "SymbolsGenerator",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor s_SymbolsXmlError = new(
        "CSSG1003",
        title: "Symbols.xml has a syntax error",
        messageFormat: "{0}",
        category: "SymbolsGenerator",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

#if false && DEBUG
    public override void Initialize(GeneratorInitializationContext context)
    {
        if (!Debugger.IsAttached)
            Debugger.Launch();
    }
#endif

    /// <inheritdoc/>
    protected override bool TryGetRelevantInput(
        in GeneratorExecutionContext context,
        out string? inputPath,
        [NotNullWhen(true)] out SourceText? inputText)
    {
        var input = context.AdditionalFiles.SingleOrDefault(a => Path.GetFileName(a.Path) == "Symbols.xml"); // 限定定义符号树节点的附加文件名为“Symbols.xml”。
        if (input is null)
        {
            context.ReportDiagnostic(Diagnostic.Create(s_MissingSymbolsXml, location: null));
            inputPath = null;
            inputText = null;
            return false;
        }

        inputText = input.GetText();
        if (inputText is null)
        {
            context.ReportDiagnostic(Diagnostic.Create(s_UnableToReadSymbolsXml, location: null));
            inputPath = null;
            return false;
        }

        inputPath = input.Path;
        return true;
    }

    protected override bool TryGenerateSources(
        string? inputPath,
        SourceText inputText,
        out ImmutableArray<(string hintName, SourceText sourceText)> sources,
        out ImmutableArray<Diagnostic> diagnostics,
        CancellationToken cancellationToken)
    {
        SymbolTree tree;
        var reader = XmlReader.Create(new SourceTextReader(inputText), new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit });

        try
        {
            var serializer = new XmlSerializer(typeof(SymbolTree));
            tree = (SymbolTree)serializer.Deserialize(reader);
        }
        catch (InvalidOperationException ex) when (ex.InnerException is XmlException xmlException)
        {
            var line = inputText.Lines[xmlException.LineNumber - 1]; // LineNumber is one-based.
            int offset = xmlException.LinePosition - 1; // LinePosition is one-based
            var position = line.Start + offset;
            var span = new TextSpan(position, 0);
            var lineSpan = inputText.Lines.GetLinePositionSpan(span);

            sources = default;
            diagnostics = ImmutableArray.Create(
                Diagnostic.Create(
                    s_SymbolsXmlError,
                    location: Location.Create(inputPath!, span, lineSpan),
                    xmlException.Message));

            return false;
        }

        var sourcesBuilder = ImmutableArray.CreateBuilder<(string hintName, SourceText sourceText)>();
        addResult(writer => SymbolsSourceWriter.WriteInternal(writer, tree, cancellationToken), "Symbols.xml.Internal.Generated.cs");
        addResult(writer => SymbolsSourceWriter.WritePublic(writer, tree, cancellationToken), "Symbols.xml.Public.Generated.cs");

        sources = sourcesBuilder.ToImmutable();
        diagnostics = ImmutableArray<Diagnostic>.Empty;
        return true;

        void addResult(Action<TextWriter> writeFunction, string hintName)
        {
            // 将内容写入一个StringBuilder以避免在内存中创建一个庞大字符串。
            var stringBuilder = new StringBuilder();
            using (var textWriter = new StringWriter(stringBuilder))
            {
                writeFunction(textWriter);
            }

            // 从StringBuilder创建一个SourceText，再次避免申请一个庞大字符串的空间。
            var sourceText = SourceText.From(new StringBuilderReader(stringBuilder), stringBuilder.Length, encoding: Encoding.UTF8);
            sourcesBuilder.Add((hintName, sourceText));
        }
    }
}
