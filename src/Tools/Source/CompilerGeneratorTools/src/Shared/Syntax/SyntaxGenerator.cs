// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Luna.Compilers.Generators.Syntax;

namespace Luna.Compilers.Generators;

using Luna.Compilers.Generators.Syntax.Model;

[Generator]
public sealed class SyntaxGenerator : TreeSourceGenerator<SyntaxTree, SyntaxTreeType>
{
    private const string SyntaxXml = "Syntax.xml";

    private static readonly DiagnosticDescriptor s_MissingSyntaxXml = DiagnosticDescriptors.CreateMissingFile<SyntaxGenerator>(SyntaxXml);

    private static readonly DiagnosticDescriptor s_UnableToReadSyntaxXml = DiagnosticDescriptors.CreateUnableToReadFile<SyntaxGenerator>(SyntaxXml);

    private static readonly DiagnosticDescriptor s_SyntaxXmlSyntaxError = DiagnosticDescriptors.CreateFileSyntaxError<SyntaxGenerator>(SyntaxXml);

#if DEBUG
    protected override bool ShouldAttachDebugger => false;
#endif

    /// <inheritdoc/>
    protected override bool TryGetRelevantInputs(
        in IncrementalGeneratorInitializationContext context,
        out IncrementalValueProvider<ImmutableArray<AdditionalText>> inputs)
    {
        inputs = context.AdditionalTextsProvider.Where(text => Path.GetFileName(text.Path).Equals(SyntaxXml, StringComparison.OrdinalIgnoreCase)).Collect();
        return true;
    }

    /// <inheritdoc/>
    protected override void FlattenTree(SyntaxTree tree, CancellationToken cancellationToken) => SyntaxTreeFlattener.Instance.Flatten(tree, cancellationToken);

    /// <inheritdoc/>
    protected override void GenerateOutputs(
        in SourceProductionContext context,
        SyntaxTree tree,
        CancellationToken cancellationToken)
    {
        WriteAndAddSource(in context, writer => SyntaxSourceWriter.WriteMain(writer, tree, cancellationToken), SyntaxXml + ".Main.Generated.cs");
        WriteAndAddSource(in context, writer => SyntaxSourceWriter.WriteInternal(writer, tree, cancellationToken), SyntaxXml + ".Internal.Generated.cs");
        WriteAndAddSource(in context, writer => SyntaxSourceWriter.WriteSyntax(writer, tree, cancellationToken), SyntaxXml + ".Syntax.Generated.cs");
    }

    #region DiagnosticDescriptors
    /// <inheritdoc/>
    protected override DiagnosticDescriptor MissingInputDiagnosticDescriptor => s_MissingSyntaxXml;

    /// <inheritdoc/>
    protected override DiagnosticDescriptor UnableToReadInputDiagnosticDescriptor => s_UnableToReadSyntaxXml;

    /// <inheritdoc/>
    protected override DiagnosticDescriptor InputSyntaxErrorDiagnosticDescriptor => s_SyntaxXmlSyntaxError;
    #endregion
}
