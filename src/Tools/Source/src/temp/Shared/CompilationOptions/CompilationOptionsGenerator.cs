// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Luna.Compilers.Generators.CompilationOptions;

namespace Luna.Compilers.Generators;

using Luna.Compilers.Generators.CompilationOptions.Model;

[Generator]
public sealed class CompilationOptionsGenerator : TreeSourceGenerator<OptionList, Option>
{
    private const string CompilationOptionsXml = "CompilationOptions.xml";

    private static readonly DiagnosticDescriptor s_MissingCompilationOptionsXml = DiagnosticDescriptors.CreateMissingFile<CompilationOptionsGenerator>(CompilationOptionsXml);

    private static readonly DiagnosticDescriptor s_UnableToReadCompilationOptionsXml = DiagnosticDescriptors.CreateUnableToReadFile<CompilationOptionsGenerator>(CompilationOptionsXml);

    private static readonly DiagnosticDescriptor s_CompilationOptionsXmlSyntaxError = DiagnosticDescriptors.CreateFileSyntaxError<CompilationOptionsGenerator>(CompilationOptionsXml);

#if DEBUG
    protected override bool ShouldAttachDebugger => false;
#endif

    /// <inheritdoc/>
    protected override bool TryGetRelevantInputs(
        IncrementalGeneratorInitializationContext context,
        out IncrementalValueProvider<ImmutableArray<AdditionalText>> inputs)
    {
        inputs = context.AdditionalTextsProvider.Where(text => Path.GetFileName(text.Path).Equals(CompilationOptionsXml, StringComparison.OrdinalIgnoreCase)).Collect();
        return true;
    }

    /// <inheritdoc/>
    protected override void GenerateOutputs(
        SourceProductionContext context,
        OptionList tree)
    {
        WriteAndAddSource(context, CompilationOptionsSourceWriter.WriteMain, tree, CompilationOptionsXml + ".Main.Generated.cs");
    }

    #region DiagnosticDescriptors
    /// <inheritdoc/>
    protected override DiagnosticDescriptor MissingInputDiagnosticDescriptor => s_MissingCompilationOptionsXml;

    /// <inheritdoc/>
    protected override DiagnosticDescriptor UnableToReadInputDiagnosticDescriptor => s_UnableToReadCompilationOptionsXml;

    /// <inheritdoc/>
    protected override DiagnosticDescriptor InputSyntaxErrorDiagnosticDescriptor => s_CompilationOptionsXmlSyntaxError;
    #endregion
}
