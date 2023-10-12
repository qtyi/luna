// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Model;

public abstract class TreeFlattener<TTree, TTreeType, TTreeTypeChild> : TreeFlattener<TTree, TTreeType>
    where TTree : ITree<TTreeType, TTreeTypeChild>
    where TTreeType : ITreeType<TTreeTypeChild>
    where TTreeTypeChild : ITreeTypeChild
{
    protected sealed override void FlattenType(TTreeType treeType, TTree containingTree, CancellationToken cancellationToken)
    {
        foreach (var child in treeType.Children)
        {
            if (this.ShouldFlattenChild(child, containingTree, treeType, cancellationToken))
                this.FlattenChild(child, containingTree, treeType, cancellationToken);
        }
    }

    protected virtual bool ShouldFlattenChild(TTreeTypeChild child, TTree containingTree, TTreeType containingTreeType, CancellationToken cancellationToken) => true;

    protected abstract void FlattenChild(TTreeTypeChild child, TTree containingTree, TTreeType containingTreeType, CancellationToken cancellationToken);
}
