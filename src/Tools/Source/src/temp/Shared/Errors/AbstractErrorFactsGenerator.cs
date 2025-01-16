// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Luna.Compilers.Generators;

public abstract class AbstractErrorFactsGenerator : AbstractSourceGenerator
{
    protected abstract string ThisLanguageName { get; }

    protected sealed override void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var input = GetErrorCodeType(context)
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
        context.RegisterSourceOutput(input, (context, fields) =>
        {
            if (fields.IsDefault)
                return;

            var dic = PooledDictionary<string, ArrayBuilder<string>>.GetInstance();
            foreach (var field in fields)
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

            GenerateOutputs(context, categorizedErrorCodeNames);
        });
    }

    /// <summary>
    /// Get a <see cref="INamedTypeSymbol"/> that represents the ErrorCode type in compilation.
    /// </summary>
    /// <param name="context">The <see cref="IncrementalGeneratorInitializationContext"/> to get compilation information from.</param>
    /// <returns>Returns a <see cref="INamedTypeSymbol"/> that represents the ErrorCode type, otherwise <see langword="null"/> if type not exists.</returns>
    protected virtual IncrementalValueProvider<INamedTypeSymbol?> GetErrorCodeType(IncrementalGeneratorInitializationContext context)
        => context.CompilationProvider.Select((compilation, _) =>
            compilation.GetTypeByMetadataName($"Qtyi.CodeAnalysis.{ThisLanguageName}.ErrorCode")
        );

    protected bool TryCategorizeErrorCodeField(SourceProductionContext context, IFieldSymbol field,
        [NotNullWhen(true)] out string? categoryName,
        [NotNullWhen(true)] out string? errorCodeName)
    {
        errorCodeName = field.Name;
        if (!TryCategorizeErrorCodeName(errorCodeName, out categoryName))
        {
            ReportUncategorizableErrorCodeField(context, field);
        }
        return false;
    }

    protected abstract void ReportUncategorizableErrorCodeField(SourceProductionContext context, IFieldSymbol field);

    protected virtual bool TryCategorizeErrorCodeName(string errorCodeName,
        [NotNullWhen(true)] out string? categoryName)
    {
        if (errorCodeName.StartsWith("WRN_"))
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

    protected abstract void GenerateOutputs(SourceProductionContext context, ImmutableDictionary<string, ImmutableArray<string>> categorizedErrorCodeNames);
}
