// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// A Declaration summarizes the declaration structure of a source file. Each entity declaration
/// in the program that is a container, that is represented by a node in this tree.  At the top level, the
/// compilation unit is treated as a declaration of the Module named by source file name.
/// </summary>
internal abstract class Declaration
{
    /// <summary>
    /// Name of the declaration.
    /// </summary>
    protected readonly string name;

    protected Declaration(string name) => this.name = name;

    /// <summary>
    /// Gets the name of the declaration.
    /// </summary>
    /// <value>
    /// Name of the declaration.
    /// </value>
    public string Name => name;

    /// <summary>
    /// Gets the kind of the declaration.
    /// </summary>
    /// <value>
    /// Kind of the declaration.
    /// </value>
    public abstract DeclarationKind Kind { get; }

    /// <summary>
    /// Gets the child declarations of this declaration.
    /// </summary>
    /// <returns>
    /// A collection of child declarations of this declaration.
    /// </returns>
    protected abstract ImmutableArray<Declaration> GetDeclarationChildren();

    /// <summary>
    /// Gets the child declarations of the declaration.
    /// </summary>
    /// <value>
    /// Child declarations of the declaration.
    /// </value>
    public ImmutableArray<Declaration> Children => GetDeclarationChildren();
}
