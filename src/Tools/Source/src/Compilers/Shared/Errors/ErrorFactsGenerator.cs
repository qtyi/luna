// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Luna.Compilers.Generators.ErrorFacts;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Luna.Compilers.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class ErrorFactsGenerator : AbstractSourceGenerator<ImmutableArray<IFieldSymbol>>
{
    protected sealed override IncrementalValueProvider<ImmutableArray<IFieldSymbol>> GetRelevantInputs(IncrementalGeneratorInitializationContext context)
    {
        return GetErrorCodeType(context)
            // Check if `errorCodeType` is valid.
            .Select(static (errorCodeType, cancellationToken) =>
            {
                if (errorCodeType is null)
                    return null;

                // Enum type should be value type.
                if (!errorCodeType.IsValueType)
                    return null;

                // Enum type should directly derived from `System.Enum`.
                if (errorCodeType.BaseType is not { SpecialType: SpecialType.System_Enum })
                    return null;

                return errorCodeType;
            })
            // Get enum fields.
            .SelectMany(static (errorCodeType, cancellationToken) =>
            {
                if (errorCodeType is null)
                    return [];

                cancellationToken.ThrowIfCancellationRequested();

                return errorCodeType.GetMembers()
                    .WhereAsArray(
                        static (member, fieldType) =>
                            member is IFieldSymbol field &&
                                field.DeclaredAccessibility >= Accessibility.Internal &&
                                field.Type.Equals(fieldType),
                        errorCodeType)
                    .SelectAsArray(static member => (IFieldSymbol)member);
            })
            .Collect();
    }

    /// <summary>
    /// Get a <see cref="INamedTypeSymbol"/> that represents the ErrorCode type in compilation.
    /// </summary>
    /// <param name="context">The <see cref="IncrementalGeneratorInitializationContext"/> to get compilation information from.</param>
    /// <returns>Returns a <see cref="INamedTypeSymbol"/> that represents the ErrorCode type, otherwise <see langword="null"/> if type not exists.</returns>
    private IncrementalValueProvider<INamedTypeSymbol?> GetErrorCodeType(IncrementalGeneratorInitializationContext context)
        => ThisLanguageNameProvider.Combine(context.CompilationProvider).Select((tuple, _) =>
            tuple.Right.GetTypeByMetadataName($"Qtyi.CodeAnalysis.{tuple.Left}.ErrorCode")
        );

    protected sealed override void ProduceSource(SourceProductionContext context, string? thisLanguageName, ImmutableArray<IFieldSymbol> inputs)
    {
        if (string.IsNullOrWhiteSpace(thisLanguageName))
            return;

        if (inputs.IsDefault)
            return;

        var dic = PooledDictionary<string, ArrayBuilder<string>>.GetInstance();
        foreach (var field in inputs)
        {
            if (TryCategorizeErrorCodeField(context, field, out var categoryName, out var errorCodeName))
            {
                var builder = dic.GetOrAdd(categoryName, ArrayBuilder<string>.GetInstance());
                builder.Add(errorCodeName);
            }
        }

        ImmutableDictionary<string, ImmutableArray<string>> categorizedErrorCodeNames;

        if (dic.Count == 0)
        {
            categorizedErrorCodeNames = ImmutableDictionary<string, ImmutableArray<string>>.Empty;
        }
        else
        {
            var dicBuilder = ImmutableDictionary.CreateBuilder<string, ImmutableArray<string>>();
            foreach ((var categoryName, var errorCodeNames) in dic)
                dicBuilder.Add(categoryName, errorCodeNames.ToImmutableAndFree());

            categorizedErrorCodeNames = dicBuilder.ToImmutable();
        }
        dic.Free();

        GenerateOutputs(context, thisLanguageName!, categorizedErrorCodeNames);
    }

    private bool TryCategorizeErrorCodeField(SourceProductionContext context, IFieldSymbol field,
        [NotNullWhen(true)] out string? categoryName,
        [NotNullWhen(true)] out string? errorCodeName)
    {
        errorCodeName = field.Name;
        if (!TryCategorizeErrorCodeName(errorCodeName, out categoryName))
        {
            ReportUncategorizableErrorCodeField(context, field);
            return false;
        }
        return true;
    }

    private void ReportUncategorizableErrorCodeField(SourceProductionContext context, IFieldSymbol field)
    {
        context.ReportDiagnostic(Diagnostic.Create(DiagnosticDescriptors.UncategorizableErrorCodeField, field.Locations.FirstOrDefault(), field));
    }

    private bool TryCategorizeErrorCodeName(string errorCodeName,
        [NotNullWhen(true)] out string? categoryName)
    {
        if (errorCodeName.StartsWith("ERR"))
            categoryName = "Error";
        else if (errorCodeName.StartsWith("WRN_"))
            categoryName = "Warning";
        else if (errorCodeName.StartsWith("FTL_"))
            categoryName = "Fatal";
        else if (errorCodeName.StartsWith("INF_"))
            categoryName = "Info";
        else if (errorCodeName.StartsWith("HDN_"))
            categoryName = "Hidden";
        else
            categoryName = null;

        return categoryName is not null;
    }

    private void GenerateOutputs(SourceProductionContext context, string thisLanguageName, ImmutableDictionary<string, ImmutableArray<string>> categorizedErrorCodeNames)
    {
        WriteAndAddSource(context, ErrorFactsSourceWriter.WriteMain, new ErrorFactsSourceProductionContext(thisLanguageName, categorizedErrorCodeNames), "ErrorFacts.Generated.cs");
    }
}

/// <summary>
/// Context passed to an <see cref="ErrorFactsSourceWriter"/> to start source production.
/// </summary>
internal readonly struct ErrorFactsSourceProductionContext
{
    public readonly string ThisLanguageName;
    public readonly ImmutableDictionary<string, ImmutableArray<string>> CategorizedErrorCodeNames;

    public ErrorFactsSourceProductionContext(string thisLanguageName, ImmutableDictionary<string, ImmutableArray<string>> categorizedErrorCodeNames)
    {
        ThisLanguageName = thisLanguageName;
        CategorizedErrorCodeNames = categorizedErrorCodeNames;
    }
}
