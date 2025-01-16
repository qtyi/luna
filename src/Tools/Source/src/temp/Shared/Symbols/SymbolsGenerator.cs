// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Luna.Compilers.Generators.Symbols;

namespace Luna.Compilers.Generators;

using Luna.Compilers.Generators.Symbols.Model;

[Generator]
public sealed class SymbolsGenerator : TreeSourceGenerator<SymbolTree, SymbolTreeType>
{
    private const string SymbolsXml = "Symbols.xml";

    private static readonly DiagnosticDescriptor s_MissingSymbolsXml = DiagnosticDescriptors.CreateMissingFile<SymbolsGenerator>(SymbolsXml);

    private static readonly DiagnosticDescriptor s_UnableToReadSymbolsXml = DiagnosticDescriptors.CreateUnableToReadFile<SymbolsGenerator>(SymbolsXml);

    private static readonly DiagnosticDescriptor s_SymbolsXmlSyntaxError = DiagnosticDescriptors.CreateFileSyntaxError<SymbolsGenerator>(SymbolsXml);

#if DEBUG
    protected override bool ShouldAttachDebugger => false;
#endif

    /// <inheritdoc/>
    protected override bool TryGetRelevantInputs(
        IncrementalGeneratorInitializationContext context,
        out IncrementalValueProvider<ImmutableArray<AdditionalText>> inputs)
    {
        inputs = context.AdditionalTextsProvider.Where(text => Path.GetFileName(text.Path).Equals(SymbolsXml, StringComparison.OrdinalIgnoreCase)).Collect();
        return true;
    }

    /// <inheritdoc/>
    protected override void GenerateOutputs(
        SourceProductionContext context,
        SymbolTree tree)
    {
        WriteAndAddSource(context, SymbolsSourceWriter.WriteInternal, tree, SymbolsXml + ".Internal.Generated.cs");
        WriteAndAddSource(context, SymbolsSourceWriter.WritePublic, tree, SymbolsXml + ".Public.Generated.cs");
    }

    #region DiagnosticDescriptors
    /// <inheritdoc/>
    protected override DiagnosticDescriptor MissingInputDiagnosticDescriptor => s_MissingSymbolsXml;

    /// <inheritdoc/>
    protected override DiagnosticDescriptor UnableToReadInputDiagnosticDescriptor => s_UnableToReadSymbolsXml;

    /// <inheritdoc/>
    protected override DiagnosticDescriptor InputSyntaxErrorDiagnosticDescriptor => s_SymbolsXmlSyntaxError;
    #endregion
}
