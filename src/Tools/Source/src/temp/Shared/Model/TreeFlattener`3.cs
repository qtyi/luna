// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Model;

/// <inheritdoc/>
/// <typeparam name="TTree">Type of tree.</typeparam>
/// <typeparam name="TTreeType">Type of tree type.</typeparam>
/// <typeparam name="TTreeTypeChild">Type of tree type child.</typeparam>
public abstract class TreeFlattener<TTree, TTreeType, TTreeTypeChild> : TreeFlattener<TTree, TTreeType>
    where TTree : ITree<TTreeType, TTreeTypeChild>
    where TTreeType : ITreeType<TTreeTypeChild>
    where TTreeTypeChild : ITreeTypeChild
{
    /// <inheritdoc/>
    protected sealed override void FlattenType(TTreeType type, TTree containingTree, CancellationToken cancellationToken)
    {
        foreach (var child in type.Children)
        {
            if (this.ShouldFlattenChild(child, containingTree, type, cancellationToken))
                this.FlattenChild(child, containingTree, type, cancellationToken);
        }
    }

    /// <summary>
    /// Gets a value indicate if particular tree type child should be flattened.
    /// </summary>
    /// <param name="child">Tree type child to flattened.</param>
    /// <param name="containingTree">The tree which contains <paramref name="containingTreeType"/>.</param>
    /// <param name="containingTreeType">The tree type which contains <paramref name="child"/>.</param>
    /// <param name="cancellationToken">Token that propagates notifications that this operation should be cancelled.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="child"/> should be flattened; otherwise, <see langword="false"/>.</returns>
    protected virtual bool ShouldFlattenChild(TTreeTypeChild child, TTree containingTree, TTreeType containingTreeType, CancellationToken cancellationToken) => true;

    /// <summary>
    /// Start flattening a tree type.
    /// </summary>
    /// <param name="child">Tree type child to flattened.</param>
    /// <param name="containingTree">The tree which contains <paramref name="containingTreeType"/>.</param>
    /// <param name="containingTreeType">The tree type which contains <paramref name="child"/>.</param>
    /// <param name="cancellationToken">Token that propagates notifications that this operation should be cancelled.</param>
    protected abstract void FlattenChild(TTreeTypeChild child, TTree containingTree, TTreeType containingTreeType, CancellationToken cancellationToken);
}
