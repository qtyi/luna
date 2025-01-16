// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis.Lua;

internal partial class ModuleDeclaration
{
    /// <inheritdoc/>
    public sealed override DeclarationKind Kind => DeclarationKind.Module;

    /// <summary>
    /// Create a new instance of <see cref="ModuleDeclaration"/> type.
    /// </summary>
    /// <param name="name">The name of the module.</param>
    /// <param name="syntaxReference">The reference to the chunk syntax.</param>
    /// <param name="diagnostics">The diagnostics reported.</param>
    internal ModuleDeclaration(
        string name,
        SyntaxReference syntaxReference,
        ImmutableArray<Diagnostic> diagnostics)
        : base(name)
    {
        _syntaxReference = syntaxReference;
        Diagnostics = diagnostics;
    }

    #region Declaration
    /// <returns>Always returns an empty array as a Lua module declaration does not contains any child declaration.</returns>
    /// <inheritdoc/>
    protected sealed override ImmutableArray<Declaration> GetDeclarationChildren() => ImmutableArray<Declaration>.Empty;
    #endregion
}
