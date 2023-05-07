// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Luna.Compilers.Generators.Model;

public interface ITreeType<TTreeTypeChild>
    where TTreeTypeChild : ITreeTypeChild
{
    string Name { get; }

    string? Base { get; }

    ImmutableList<TTreeTypeChild> Children { get; }
}
