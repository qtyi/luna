// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Luna.Compilers.Generators.Model;

public abstract class TreeFlattener<TTree, TTreeType>
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    public void Flatten(TTree tree, CancellationToken cancellationToken = default)
    {
        foreach (var treeType in tree.Types)
        {
            if (this.ShouldFlattenType(treeType, tree, cancellationToken))
                this.FlattenType(treeType, tree, cancellationToken);
        }
    }

    protected virtual bool ShouldFlattenType(TTreeType treeType, TTree containingTree, CancellationToken cancellationToken) => true;

    protected abstract void FlattenType(TTreeType treeType, TTree containingTree, CancellationToken cancellationToken);
}
