// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Qtyi.CodeAnalysis.MoonScript;

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
                isSubmission,
                ordinalMapBuilder,
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
                    isSubmission,
                    ordinalMapBuilder,
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
        bool isSubmission,
        IDictionary<SyntaxTree, int> ordinalMapBuilder,
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
        var newExternalSyntaxTrees = this.ExternalSyntaxTrees.RemoveAll(trees.Contains);
        if (state is null)
            return this.WithExternalSyntaxTrees(newExternalSyntaxTrees);

        var syntaxTrees = state.SyntaxTrees;
        var loadedSyntaxTreeMap = state.LoadedSyntaxTreeMap;
        var removeSet = PooledHashSet<SyntaxTree>.GetInstance();
        foreach (var tree in trees)
        {
            SyntaxAndDeclarationManager.GetRemoveSet(
                tree,
                removeSet,
                out var _);
        }

        var treesBuilder = ArrayBuilder<SyntaxTree>.GetInstance();
        var ordinalMapBuilder = PooledDictionary<SyntaxTree, int>.GetInstance();
        var declMapBuilder = state.Modules.ToBuilder();
        var declTable = state.DeclarationTable;
        foreach (var tree in syntaxTrees)
        {
            if (removeSet.Contains(tree))
            {
                loadedSyntaxTreeMap.Remove(tree.FilePath);
                SyntaxAndDeclarationManager.RemoveSyntaxTreeFromDeclarationMapAndTable(tree, declMapBuilder, ref declTable);
            }
            else if (!SyntaxAndDeclarationManager.IsLoadedSyntaxTree(tree, loadedSyntaxTreeMap))
            {
                SyntaxAndDeclarationManager.UpdateSyntaxTreesAndOrdinalMapOnly(
                    treesBuilder,
                    tree,
                    ordinalMapBuilder,
                    loadedSyntaxTreeMap);
            }
        }
        removeSet.Free();

        state = new(
            treesBuilder.ToImmutableAndFree(),
            ordinalMapBuilder.ToImmutableDictionaryAndFree(),
            loadedSyntaxTreeMap,
            declMapBuilder.ToImmutableDictionary(),
            declTable);

        return new(
            newExternalSyntaxTrees,
            this.ScriptClassName,
            this.Resolver,
            this.MessageProvider,
            this.IsSubmission,
            state);
    }

    // TODO: Remove this method.
    /// <summary>
    /// Collects all the trees loaded by <paramref name="oldTree"/> (as well as
    /// <paramref name="oldTree"/> itself) and populates <paramref name="removeSet"/>
    /// with all the trees that are safe to remove (not loaded by any other tree).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void GetRemoveSet(
        SyntaxTree oldTree,
        HashSet<SyntaxTree> removeSet,
        out int totalReferencedTreeCount)
    {
        removeSet.Add(oldTree);
        totalReferencedTreeCount = removeSet.Count;
    }

    /// <summary>
    /// Remove syntax tree from declaration map and declaration table.
    /// </summary>
    private static void RemoveSyntaxTreeFromDeclarationMapAndTable(
        SyntaxTree tree,
        IDictionary<SyntaxTree, Lazy<ModuleDeclaration>> declMap,
        ref DeclarationTable declTable)
    {
        var lazyRoot = declMap[tree];
        declTable = declTable.RemoveRootDeclaration(lazyRoot);
        declMap.Remove(tree);
    }

    public partial SyntaxAndDeclarationManager ReplaceSyntaxTree(SyntaxTree oldTree, SyntaxTree newTree)
    {
        var state = this._lazyState;
        var newExternalSyntaxTrees = this.ExternalSyntaxTrees.Replace(oldTree, newTree);
        if (state is null)
            return this.WithExternalSyntaxTrees(newExternalSyntaxTrees);

        var syntaxTrees = state.SyntaxTrees;
        var ordinalMap = state.OrdinalMap;
        var loadedSyntaxTreeMap = state.LoadedSyntaxTreeMap;
        var removeSet = PooledHashSet<SyntaxTree>.GetInstance();
        SyntaxAndDeclarationManager.GetRemoveSet(
            oldTree,
            removeSet,
            out var _);

        var loadedSyntaxTreeMapBuilder = loadedSyntaxTreeMap.ToBuilder();
        var declMapBuilder = state.Modules.ToBuilder();
        var declTable = state.DeclarationTable;
        foreach (var tree in removeSet)
        {
            loadedSyntaxTreeMapBuilder.Remove(tree.FilePath);
            SyntaxAndDeclarationManager.RemoveSyntaxTreeFromDeclarationMapAndTable(tree, declMapBuilder, ref declTable);
        }
        removeSet.Free();

        var oldOrdinal = ordinalMap[oldTree];
        ImmutableArray<SyntaxTree> newTrees;
        SyntaxAndDeclarationManager.AddSyntaxTreeToDeclarationMapAndTable(newTree, this.ScriptClassName, this.IsSubmission, declMapBuilder, ref declTable);

        Debug.Assert(ordinalMap.ContainsKey(oldTree)); // Checked by RemoveSyntaxTreeFromDeclarationMapAndTable

        newTrees = syntaxTrees.SetItem(oldOrdinal, newTree);

        ordinalMap = ordinalMap.Remove(oldTree);
        ordinalMap = ordinalMap.SetItem(newTree, oldOrdinal);

        state = new(
            newTrees,
            ordinalMap,
            loadedSyntaxTreeMapBuilder.ToImmutable(),
            declMapBuilder.ToImmutable(),
            declTable);

        return new(
            newExternalSyntaxTrees,
            this.ScriptClassName,
            this.Resolver,
            this.MessageProvider,
            this.IsSubmission,
            state);
    }

    /// <summary>
    /// Check if a syntax tree is loaded.
    /// </summary>
    /// <param name="tree">The syntax tree to be checked.</param>
    /// <param name="loadedSyntaxTreeMap">A map contains all loaded syntax trees.</param>
    /// <returns><see langword="true"/> if <paramref name="tree"/> is loaded; otherwise, <see langword="false"/>.</returns>
    internal static bool IsLoadedSyntaxTree(SyntaxTree tree, ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap) =>
        loadedSyntaxTreeMap.TryGetValue(tree.FilePath, out var loadedTree) && (tree == loadedTree);

    /// <summary>
    /// Only update syntax trees and ordinal map.
    /// </summary>
    private static void UpdateSyntaxTreesAndOrdinalMapOnly(
        ArrayBuilder<SyntaxTree> treesBuilder,
        SyntaxTree tree,
        IDictionary<SyntaxTree, int> ordinalMapBuilder,
        ImmutableDictionary<string, SyntaxTree> loadedSyntaxTreeMap)
    {
        treesBuilder.Add(tree);

        ordinalMapBuilder.Add(tree, ordinalMapBuilder.Count);
    }
}
