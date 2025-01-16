// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Model;

/// <summary>
/// Represents a flattener that flatten a tree with trimming and adjusting unexpected values.
/// </summary>
/// <typeparam name="TTree">Type of tree.</typeparam>
/// <typeparam name="TTreeType">Type of tree type.</typeparam>
public abstract class TreeFlattener<TTree, TTreeType>
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    /// <summary>
    /// Start flattening a tree.
    /// </summary>
    /// <param name="tree">Tree to flatten.</param>
    /// <param name="cancellationToken">Token that propagates notifications that this operation should be cancelled.</param>
    /// <remarks>This should be the only entry of tree flattener.</remarks>
    public void Flatten(TTree tree, CancellationToken cancellationToken = default)
    {
        foreach (var treeType in tree.Types)
        {
            if (this.ShouldFlattenType(treeType, tree, cancellationToken))
                this.FlattenType(treeType, tree, cancellationToken);
        }
    }

    /// <summary>
    /// Gets a value indicate if particular tree type should be flattened.
    /// </summary>
    /// <param name="type">Tree type to flattened.</param>
    /// <param name="containingTree">The tree which contains <paramref name="type"/>.</param>
    /// <param name="cancellationToken">Token that propagates notifications that this operation should be cancelled.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="type"/> should be flattened; otherwise, <see langword="false"/>.</returns>
    protected virtual bool ShouldFlattenType(TTreeType type, TTree containingTree, CancellationToken cancellationToken) => true;

    /// <summary>
    /// Start flattening a tree type.
    /// </summary>
    /// <param name="type">Tree type to flatten.</param>
    /// <param name="containingTree">The tree which contains <paramref name="type"/>.</param>
    /// <param name="cancellationToken">Token that propagates notifications that this operation should be cancelled.</param>
    protected abstract void FlattenType(TTreeType type, TTree containingTree, CancellationToken cancellationToken);
}
