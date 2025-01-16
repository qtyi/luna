// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Luna.Compilers.Generators.Model;

namespace Luna.Compilers.Generators;

/// <summary>
/// Represents a writer that work for a tree, it collects information about parent and child relationships between tree types.
/// </summary>
/// <typeparam name="TTree"></typeparam>
/// <typeparam name="TTreeType"></typeparam>
internal abstract class TreeFileWriter<TTree, TTreeType> : IndentWriter
    where TTree : ITree<TTreeType>
    where TTreeType : ITreeType
{
    private readonly TTree _tree;
    private readonly Dictionary<string, string?> _parentMap;
    private readonly Lookup<string, string> _childMap;

    private readonly Dictionary<string, TTreeType> _typeMap;

    /// <summary>
    /// The tree this file writer work for.
    /// </summary>
    protected TTree Tree => this._tree;
    /// <summary>
    /// Used to get parent tree type of every tree types in <see cref="Tree"/>.
    /// </summary>
    protected IDictionary<string, string?> ParentMap => this._parentMap;
    protected ILookup<string, string> ChildMap => this._childMap;

    /// <inheritdoc cref="TreeFileWriter{TTree, TTreeType}.TreeFileWriter(TextWriter, int, char, TTree)"/>
    protected TreeFileWriter(TextWriter writer, TTree tree) : this(writer, 4, ' ', tree) { }

    /// <summary>
    /// Initialize an instance of <see cref="TreeFileWriter{TTree, TTreeType}"/>.
    /// </summary>
    /// <param name="tree">The tree to work for.</param>
    /// <inheritdoc cref="IndentWriter.IndentWriter(TextWriter, int, char)"/>
    protected TreeFileWriter(TextWriter writer, int indentSize, char indentChar, TTree tree) : base(writer, indentSize, indentChar)
    {
        _tree = tree;
        _typeMap = tree.Types.ToDictionary(static n => n.Name);
        _parentMap = tree.Types.ToDictionary(static n => n.Name, static n => n.Base);
        _parentMap.Add(tree.Root, null);
        _childMap = (Lookup<string, string>)tree.Types.Where(static n => n.Base is not null).ToLookup(static n => n.Base!, static n => n.Name);
    }

    #region Helper Methods
    /// <summary>
    /// Gets a value indicate that a type is derived from another type.
    /// </summary>
    /// <param name="typeName">Name of the type should be derived from <paramref name="derivedTypeName"/>.</param>
    /// <param name="derivedTypeName">Name of the type should be the base type of <paramref name="typeName"/>.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="typeName"/> is derived from <paramref name="derivedTypeName"/>; otherwise, <see langword="false"/>.</returns>
    protected bool IsDerivedType(string typeName, string? derivedTypeName)
    {
        if (typeName == derivedTypeName)
            return true;
        else if (derivedTypeName is not null && this._parentMap.TryGetValue(derivedTypeName, out var baseType))
            return this.IsDerivedType(typeName, baseType);
        else
            return false;
    }

    /// <summary>
    /// Gets a type with a particular name.
    /// </summary>
    /// <param name="typeName">Name of the type that is match.</param>
    /// <returns>A type with name match <paramref name="typeName"/>.</returns>
    protected TTreeType? GetTreeType(string? typeName)
        => typeName is not null && this._typeMap.TryGetValue(typeName, out var type) ? type : default;
    #endregion
}
