// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Text;
using MSCA::System.Diagnostics.CodeAnalysis;
using Luna.Compilers.Generators.Syntax;
using Luna.Compilers.Generators.Syntax.Model;

namespace Luna.Compilers.Generators;

[Generator]
public sealed class SyntaxGenerator : CachingSourceGenerator
{
    private static readonly DiagnosticDescriptor s_MissingSyntaxXml = new(
        "CSSG1001",
        title: "Syntax.xml is missing",
        messageFormat: "The Syntax.xml file was not included in the project, so we are not generating source.",
        category: "SyntaxGenerator",
        defaultSeverity: DiagnosticSeverity.Warning,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor s_UnableToReadSyntaxXml = new(
        "CSSG1002",
        title: "Syntax.xml could not be read",
        messageFormat: "The Syntax.xml file could not even be read. Does it exist?",
        category: "SyntaxGenerator",
        defaultSeverity: DiagnosticSeverity.Error,
        isEnabledByDefault: true);

    private static readonly DiagnosticDescriptor s_SyntaxXmlError = new(
        "CSSG1003",
        title: "Syntax.xml has a syntax error",
        messageFormat: "{0}",
        category: "SyntaxGenerator",
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
        var input = context.AdditionalFiles.SingleOrDefault(a => Path.GetFileName(a.Path) == "Syntax.xml"); // 限定定义语法树节点的附加文件名为“Syntax.xml”。
        if (input is null)
        {
            context.ReportDiagnostic(Diagnostic.Create(s_MissingSyntaxXml, location: null));
            inputPath = null;
            inputText = null;
            return false;
        }

        inputText = input.GetText();
        if (inputText is null)
        {
            context.ReportDiagnostic(Diagnostic.Create(s_UnableToReadSyntaxXml, location: null));
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
        Tree tree;
        var reader = XmlReader.Create(new SourceTextReader(inputText), new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit });

        try
        {
            var serializer = new XmlSerializer(typeof(Tree));
            tree = (Tree)serializer.Deserialize(reader);
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
                    s_SyntaxXmlError,
                    location: Location.Create(inputPath!, span, lineSpan),
                    xmlException.Message));

            return false;
        }

        TreeFlattening.FlattenChildren(tree);

        var sourcesBuilder = ImmutableArray.CreateBuilder<(string hintName, SourceText sourceText)>();
        addResult(writer => SyntaxSourceWriter.WriteMain(writer, tree, cancellationToken), "Syntax.xml.Main.Generated.cs");
        addResult(writer => SyntaxSourceWriter.WriteInternal(writer, tree, cancellationToken), "Syntax.xml.Internal.Generated.cs");
        addResult(writer => SyntaxSourceWriter.WriteSyntax(writer, tree, cancellationToken), "Syntax.xml.Syntax.Generated.cs");

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
