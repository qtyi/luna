// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;
using Luna.Tools.ErrorFacts;

namespace Luna.Tools;

public abstract class AbstractErrorFactsGenerator<TProducer> : AbstractSourceGenerator<ImmutableArray<IFieldSymbol>>
    where TProducer : AbstractErrorFactsSourceSyntaxProducer, new()
{
    public const string CategoryName_Fatal = "Fatal";
    public const string CategoryName_Error = nameof(DiagnosticSeverity.Error);
    public const string CategoryName_Warning = nameof(DiagnosticSeverity.Warning);
    public const string CategoryName_Info = nameof(DiagnosticSeverity.Info);
    public const string CategoryName_Hidden = nameof(DiagnosticSeverity.Hidden);

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

        var dicBuilder = ImmutableDictionary.CreateBuilder<string, ImmutableArray<string>>();
        string[] orderedCategoryNames = [CategoryName_Fatal, CategoryName_Error, CategoryName_Warning, CategoryName_Info, CategoryName_Hidden];
        foreach (var categoryName in orderedCategoryNames)
        {
            var errorCodeNames = dic.TryGetValue(categoryName, out var builder) ? builder.ToImmutableAndFree() : [];
            dicBuilder.Add(categoryName, errorCodeNames);
        }
        dic.Free();

        GenerateOutputs(context, thisLanguageName!, dicBuilder.ToImmutable());
    }

    private bool TryCategorizeErrorCodeField(SourceProductionContext context, IFieldSymbol field,
        [NotNullWhen(true)] out string? categoryName,
        [NotNullWhen(true)] out string? errorCodeName)
    {
        errorCodeName = field.Name;
        if (!TryCategorizeErrorCodeName(errorCodeName, out categoryName))
        {
            Debug.Assert(field.HasConstantValue, "Public enum field should has constant value.");

            // Do not report diagnostic if const value of field is internal error code.
            if (field.ConstantValue is not InternalErrorCode.Void and not InternalErrorCode.Unknown)
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
            categoryName = CategoryName_Error;
        else if (errorCodeName.StartsWith("WRN_"))
            categoryName = CategoryName_Warning;
        else if (errorCodeName.StartsWith("FTL_"))
            categoryName = CategoryName_Fatal;
        else if (errorCodeName.StartsWith("INF_"))
            categoryName = CategoryName_Info;
        else if (errorCodeName.StartsWith("HDN_"))
            categoryName = CategoryName_Hidden;
        else
            categoryName = null;

        return categoryName is not null;
    }

    private void GenerateOutputs(SourceProductionContext context, string thisLanguageName, ImmutableDictionary<string, ImmutableArray<string>> categorizedErrorCodeNames)
    {
        var hintName = "ErrorFacts.Generated.cs";
        ProduceAndAddSource<TProducer>(context, hintName, thisLanguageName, categorizedErrorCodeNames);
    }
}
