// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript;

internal partial class ModuleDeclaration
{
    /// <summary>
    /// Child class declarations of the module declaration.
    /// </summary>
    private readonly ImmutableArray<ClassDeclaration> _children;

    /// <inheritdoc/>
    public override DeclarationKind Kind => DeclarationKind.Module;

    /// <inheritdoc cref="Declaration.Children"/>
    public new ImmutableArray<ClassDeclaration> Children => _children;

    /// <summary>
    /// Create a new instance of <see cref="ModuleDeclaration"/> type.
    /// </summary>
    /// <param name="name">The name of the module.</param>
    /// <param name="syntaxReference">The reference to the chunk syntax.</param>
    /// <param name="children">A collection of the child class declarations.</param>
    /// <param name="diagnostics">The diagnostics reported.</param>
    internal ModuleDeclaration(
        string name,
        SyntaxReference syntaxReference,
        ImmutableArray<ClassDeclaration> children,
        ImmutableArray<Diagnostic> diagnostics)
        : base(name)
    {
        _syntaxReference = syntaxReference;
        _children = children;
        Diagnostics = diagnostics;
    }

    #region Declaration
    /// <inheritdoc/>
    protected sealed override ImmutableArray<Declaration> GetDeclarationChildren() => StaticCast<Declaration>.From(_children);
    #endregion
}
