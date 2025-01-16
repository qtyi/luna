// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Luna.Compilers.Generators;

[Generator(LanguageNames.CSharp)]
public sealed class LexerCacheGenerator : AbstractSourceGenerator<ImmutableArray<string>>
{
    protected override IncrementalValueProvider<ImmutableArray<string>> GetRelevantInputs(IncrementalGeneratorInitializationContext context)
    {
        return GetSyntaxKindType(context)
            // Check if `syntaxKindType` is valid.
            .Select(static (syntaxKindType, cancellationToken) =>
            {
                if (syntaxKindType is null)
                    return null;

                // Enum type should be value type.
                if (!syntaxKindType.IsValueType)
                    return null;

                // Enum type should directly derived from `System.Enum`.
                if (syntaxKindType.BaseType is not { SpecialType: SpecialType.System_Enum })
                    return null;

                return syntaxKindType;
            })
            // Get enum fields.
            .SelectMany(static (syntaxKindType, cancellationToken) =>
            {
                if (syntaxKindType is null)
                    return [];

                cancellationToken.ThrowIfCancellationRequested();

                return syntaxKindType.GetMembers()
                    .WhereAsArray(
                        static (member, fieldType) =>
                            member is IFieldSymbol field &&
                                field.DeclaredAccessibility >= Accessibility.Internal &&
                                field.Type.Equals(fieldType) &&
                                // Keyword kinds are supposed to end with `Keyword`.
                                field.Name.EndsWith("Keyword"),
                        syntaxKindType)
                    .SelectAsArray(static member => member.Name);
            })
            .Collect();
    }

    /// <summary>
    /// Get a <see cref="INamedTypeSymbol"/> that represents the SyntaxKind type in compilation.
    /// </summary>
    /// <param name="context">The <see cref="IncrementalGeneratorInitializationContext"/> to get compilation information from.</param>
    /// <returns>Returns a <see cref="INamedTypeSymbol"/> that represents the SyntaxKind type, otherwise <see langword="null"/> if type not exists.</returns>
    private IncrementalValueProvider<INamedTypeSymbol?> GetSyntaxKindType(IncrementalGeneratorInitializationContext context)
        => ThisLanguageNameProvider.Combine(context.CompilationProvider).Select((tuple, _) =>
            tuple.Right.GetTypeByMetadataName($"Qtyi.CodeAnalysis.{tuple.Left}.SyntaxKind")
        );

    protected override void ProduceSource(SourceProductionContext context, string? thisLanguageName, ImmutableArray<string> inputs)
    {
        if (string.IsNullOrWhiteSpace(thisLanguageName))
            return;

        if (inputs.IsDefault)
            return;

        var lexerCacheSourceProductionContext = new LexerCacheSourceProductionContext(thisLanguageName!, inputs);
        var hintName = "LexerCache.g.cs";
        AddSource(context, new LexerCacheSourceProvider(lexerCacheSourceProductionContext).Produce(), hintName);
    }
}

/// <summary>
/// Context passed to an <see cref="LexerCacheSourceProvider"/> to start source production.
/// </summary>
internal readonly struct LexerCacheSourceProductionContext
{
    public readonly string ThisLanguageName;
    public readonly ImmutableArray<string> KeywordKindNames;

    public LexerCacheSourceProductionContext(string thisLanguageName, ImmutableArray<string> keywordKindNames)
    {
        ThisLanguageName = thisLanguageName;
        KeywordKindNames = keywordKindNames;
    }
}
