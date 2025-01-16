// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using System.Collections.Immutable;

namespace Luna.Tools;

using Registration = (Func<SyntaxTree, CancellationToken, bool> predicate, Action<SourceProductionContext, string?, ImmutableArray<SyntaxTree>> action);

/// <summary>
/// Represents an incremental source generator that is the base class of all syntax-modificative generators for Luna.
/// This class is abstract.
/// </summary>
public abstract class AbstractModificativeSourceGenerator : AbstractSourceGenerator<ImmutableArray<SyntaxTree>>
{
    private ImmutableArray<Registration> _registrations;

    /// <inheritdoc/>
    protected override void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var registrationContext = new ModificativeSourceGeneratorRegistrationContext();
        Register(registrationContext);
        _registrations = registrationContext.Registrations!.ToImmutableAndFree();

        base.Initialize(context);
    }

    protected abstract void Register(ModificativeSourceGeneratorRegistrationContext context);

    /// <inheritdoc/>
    protected override IncrementalValueProvider<ImmutableArray<SyntaxTree>> GetRelevantInputs(IncrementalGeneratorInitializationContext context)
        => context.SyntaxTreesProvider
        .Where((tree, cancellationToken) =>
            _registrations.Any(reg => reg.predicate(tree, cancellationToken)))
        .Collect();

    /// <inheritdoc/>
    protected override void ProduceSource(SourceProductionContext context, string? thisLanguageName, ImmutableArray<SyntaxTree> inputs)
    {
        foreach (var (predicate, action) in _registrations)
        {
            var trees = inputs.WhereAsArray(predicate, context.CancellationToken);
            if (trees.IsEmpty)
                continue;
            action(context, thisLanguageName, trees);
        }
    }
}

public readonly struct ModificativeSourceGeneratorRegistrationContext()
{
    internal readonly ArrayBuilder<Registration> Registrations = ArrayBuilder<Registration>.GetInstance();

    public void RegisterFileName(string fileName, Action<SourceProductionContext, string?, ImmutableArray<SyntaxTree>> action)
        => RegisterPredicate((tree, _) => string.Equals(Path.GetFileName(tree.FilePath), fileName), action);

    public void RegisterPredicate(Func<SyntaxTree, CancellationToken, bool> predicate, Action<SourceProductionContext, string?, ImmutableArray<SyntaxTree>> action)
        => Registrations.Add((predicate, action));
}
