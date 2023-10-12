// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators;

internal abstract class TreeFileWriter<TTree, TTreeType, TTreeTypeChild> : TreeFileWriter<TTree, TTreeType>
    where TTree : ITree<TTreeType, TTreeTypeChild>
    where TTreeType : ITreeType<TTreeTypeChild>
    where TTreeTypeChild : ITreeTypeChild
{
    protected TreeFileWriter(TextWriter writer, TTree tree, CancellationToken cancellationToken) : base(writer, tree, cancellationToken) { }
}
