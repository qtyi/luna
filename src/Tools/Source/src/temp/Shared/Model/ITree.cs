// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Luna.Compilers.Generators.Model;

/// <summary>
/// Defines members to represent a tree.
/// </summary>
/// <typeparam name="TTreeType">Type of tree type.</typeparam>
public interface ITree<TTreeType>
    where TTreeType : ITreeType
{
    /// <summary>
    /// Gets the root tree type name of this tree.
    /// </summary>
    string Root { get; }

    /// <summary>
    /// Gets a list of tree types in this tree.
    /// </summary>
    ImmutableList<TTreeType> Types { get; }
}

/// <inheritdoc/>
/// <typeparam name="TTreeType">Type of tree type.</typeparam>
/// <typeparam name="TTreeTypeChild">Type of tree type child.</typeparam>
public interface ITree<TTreeType, TTreeTypeChild> : ITree<TTreeType>
    where TTreeType : ITreeType<TTreeTypeChild>
    where TTreeTypeChild : ITreeTypeChild
{
}
