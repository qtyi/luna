// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Qtyi.CodeAnalysis.Lua;

partial class SyntaxAndDeclarationManager
{
    private static partial State CreateState(
        ImmutableArray<SyntaxTree> externalSyntaxTrees,
        string scriptClassName,
        SourceReferenceResolver resolver,
        CommonMessageProvider messageProvider,
        bool isSubmission)
    {
        var treesBuilder = ArrayBuilder<SyntaxTree>.GetInstance();
        var ordinalMapBuilder = PooledDictionary<SyntaxTree, int>.GetInstance();
        var loadedSyntaxTreeMapBuilder = PooledDictionary<string, SyntaxTree>.GetInstance();
        var declMapBuilder = PooledDictionary<SyntaxTree, Lazy<ModuleDeclaration>>.GetInstance();
        var declTable = DeclarationTable.Empty;

        foreach (var tree in externalSyntaxTrees)
        {
            SyntaxAndDeclarationManager.AppendAllSyntaxTrees(
                treesBuilder,
                tree,
                scriptClassName,
                resolver,
                messageProvider,
                isSubmission,
                ordinalMapBuilder,
                loadedSyntaxTreeMapBuilder,
                declMapBuilder,
                ref declTable);
        }

        return new(
            treesBuilder.ToImmutableAndFree(),
            ordinalMapBuilder.ToImmutableDictionaryAndFree(),
            loadedSyntaxTreeMapBuilder.ToImmutableDictionaryAndFree(),
            declMapBuilder.ToImmutableDictionaryAndFree(),
            declTable);
    }

    public partial SyntaxAndDeclarationManager AddSyntaxTrees(IEnumerable<SyntaxTree> trees)
    {
        var scriptClassName = this.ScriptClassName;
        var resolver = this.Resolver;
        var messageProvider = this.MessageProvider;
        var isSubmission = this.IsSubmission;

        var state = this._lazyState;
        var newExternalSyntaxTrees = this.ExternalSyntaxTrees.AddRange(trees);
        if (state is null)
            return this.WithExternalSyntaxTrees(newExternalSyntaxTrees);

        var ordinalMapBuilder = state.OrdinalMap.ToBuilder();
        var loadedSyntaxTreeMapBuilder = state.LoadedSyntaxTreeMap.ToBuilder();
        var declMapBuilder = state.Modules.ToBuilder();
        var declTable = state.DeclarationTable;

        var treesBuilder = ArrayBuilder<SyntaxTree>.GetInstance();
        treesBuilder.AddRange(state.SyntaxTrees);

        foreach (var tree in trees)
        {
            SyntaxAndDeclarationManager.AppendAllSyntaxTrees(
                    treesBuilder,
                    tree,
                    scriptClassName,
                    resolver,
                    messageProvider,
                    isSubmission,
                    ordinalMapBuilder,
                    loadedSyntaxTreeMapBuilder,
                    declMapBuilder,
                    ref declTable);
        }

        state = new(
            treesBuilder.ToImmutableAndFree(),
            ordinalMapBuilder.ToImmutableDictionary(),
            loadedSyntaxTreeMapBuilder.ToImmutableDictionary(),
            declMapBuilder.ToImmutableDictionary(),
            declTable);

        return new(
            newExternalSyntaxTrees,
            scriptClassName,
            resolver,
            messageProvider,
            isSubmission,
            state);
    }

    /// <summary>
    /// Appends all trees.
    /// </summary>
    /// <remarks>
    /// Only append <paramref name="tree"/> itself, as Lua load other source files during runtime instead of build-time.
    /// </remarks>
    private static void AppendAllSyntaxTrees(
        ArrayBuilder<SyntaxTree> treesBuilder,
        SyntaxTree tree,
        string scriptClassName,
        SourceReferenceResolver resolver,
        CommonMessageProvider messageProvider,
        bool isSubmission,
        IDictionary<SyntaxTree, int> ordinalMapBuilder,
        IDictionary<string, SyntaxTree> loadedSyntaxTreeMapBuilder,
        IDictionary<SyntaxTree, Lazy<ModuleDeclaration>> declMapBuilder,
        ref DeclarationTable declTable)
    {
        SyntaxAndDeclarationManager.AddSyntaxTreeToDeclarationMapAndTable(tree, scriptClassName, isSubmission, declMapBuilder, ref declTable);

        treesBuilder.Add(tree);

        ordinalMapBuilder.Add(tree, ordinalMapBuilder.Count);
    }

    /// <summary>
    /// Add syntax tree to declaration map and declaration table.
    /// </summary>
    private static void AddSyntaxTreeToDeclarationMapAndTable(
        SyntaxTree tree,
        string scriptClassName,
        bool isSubmission,
        IDictionary<SyntaxTree, Lazy<ModuleDeclaration>> declMapBuilder,
        ref DeclarationTable declTable)
    {
        var lazyRoot = new Lazy<ModuleDeclaration>(() => DeclarationTreeBuilder.ForTree(tree, scriptClassName, isSubmission));
        declMapBuilder.Add(tree, lazyRoot); // Callers are responsible for checking for existing entries.
        declTable = declTable.AddRootDeclaration(lazyRoot);
    }

    public partial SyntaxAndDeclarationManager RemoveSyntaxTrees(HashSet<SyntaxTree> trees)
    {
        var state = _lazyState;
        var newExternalSyntaxTrees = this.ExternalSyntaxTrees.RemoveAll(t => trees.Contains(t));
        if (state == null)
        {
            return this.WithExternalSyntaxTrees(newExternalSyntaxTrees);
        }

    }
}
