// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;
using Luna.Compilers.Generators.ErrorFacts;
using LanguageNames = Qtyi.CodeAnalysis.LanguageNames;

namespace Luna.Compilers.Generators;

[Generator]
public sealed class ErrorFactsGenerator : AbstractSourceGenerator
{
    protected override void InitializeCore(IncrementalGeneratorInitializationContext context)
    {
        var input = context.CompilationProvider.Select(static (compilation, cancellationToken) =>
        {
            var errorCodeType = compilation.GetTypeByMetadataName($"Qtyi.CodeAnalysis.{LanguageNames.This}.ErrorCode");
            if (errorCodeType is null)
                return ImmutableArray<string>.Empty;

            cancellationToken.ThrowIfCancellationRequested();

            return errorCodeType.GetMembers()
                .WhereAsArray(static (member, fieldType) => member is IFieldSymbol field && field.Type.Equals(fieldType), errorCodeType)
                .SelectAsArray(static field => field.Name);
        });
        context.RegisterSourceOutput(input, GenerateOutputs);
    }

    private static void GenerateOutputs(SourceProductionContext context, ImmutableArray<string> codeNames)
    {
        WriteAndAddSource(context, ErrorFactsSourceWriter.WriteMain, codeNames, "ErrorFacts.Generated.cs");
    }

    internal static void GetCodeNames(IEnumerable<string> codeNames,
        out ImmutableArray<string> warningCodeNames,
        out ImmutableArray<string> fatalCodeNames,
        out ImmutableArray<string> infoCodeNames,
        out ImmutableArray<string> hiddenCodeNames)
    {
        var wrn = ArrayBuilder<string>.GetInstance();
        var ftl = ArrayBuilder<string>.GetInstance();
        var inf = ArrayBuilder<string>.GetInstance();
        var hdn = ArrayBuilder<string>.GetInstance();
        foreach (var codeName in codeNames)
        {
            if (codeName.StartsWith("WRN_", StringComparison.OrdinalIgnoreCase))
                wrn.Add(codeName);
            else if (codeName.StartsWith("FTL_", StringComparison.OrdinalIgnoreCase))
                ftl.Add(codeName);
            else if (codeName.StartsWith("INF_", StringComparison.OrdinalIgnoreCase))
                inf.Add(codeName);
            else if (codeName.StartsWith("HDN_", StringComparison.OrdinalIgnoreCase))
                hdn.Add(codeName);
        }

        warningCodeNames = wrn.AsImmutableOrEmpty();
        fatalCodeNames = ftl.AsImmutableOrEmpty();
        infoCodeNames = inf.AsImmutableOrEmpty();
        hiddenCodeNames = hdn.AsImmutableOrEmpty();
    }
}
