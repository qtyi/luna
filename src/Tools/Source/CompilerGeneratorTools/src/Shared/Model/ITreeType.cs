// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Luna.Compilers.Generators.Model;

/// <summary>
/// Defines members to represent a tree type.
/// </summary>
public interface ITreeType
{
    /// <summary>
    /// Gets the name of this tree type.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Gets the base tree type name of this tree type.
    /// </summary>
    string? Base { get; }
}

/// <inheritdoc/>
/// <typeparam name="TTreeTypeChild">Type of tree type child.</typeparam>
public interface ITreeType<TTreeTypeChild> : ITreeType
    where TTreeTypeChild : ITreeTypeChild
{
    /// <summary>
    /// Gets a list of tree type children in this tree type.
    /// </summary>
    ImmutableList<TTreeTypeChild> Children { get; }
}
