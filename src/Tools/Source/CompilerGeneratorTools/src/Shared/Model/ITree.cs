// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Luna.Compilers.Generators.Model;

public interface ITree<TTreeType>
    where TTreeType : ITreeType
{
    string Root { get; }

    ImmutableList<TTreeType> Types { get; }
}

public interface ITree<TTreeType, TTreeTypeChild> : ITree<TTreeType>
    where TTreeType : ITreeType<TTreeTypeChild>
    where TTreeTypeChild : ITreeTypeChild
{
}
