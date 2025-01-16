// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

internal partial class ModuleDeclaration : Declaration
{
    /// <summary>
    /// The syntax referenced by the declaration.
    /// </summary>
    private readonly SyntaxReference _syntaxReference;

    /// <summary>
    /// Any diagnostics reported while converting the Chunk syntax into the Declaration
    /// instance.  Generally, we determine and store some diagnostics here because we don't want 
    /// to have to go back to Syntax when we have our ModuleSymbol.
    /// </summary>
    public readonly ImmutableArray<Diagnostic> Diagnostics;

    /// <summary>
    /// Gets the location of the syntax referenced by the declaration in source code.
    /// </summary>
    /// <value>
    /// The location of the syntax referenced by the declaration in source code
    /// </value>
    public SourceLocation Location => new(_syntaxReference);

    /// <summary>
    /// Gets the syntax referenced by the declaration.
    /// </summary>
    /// <value>
    /// The syntax referenced by the declaration.
    /// </value>
    public SyntaxReference SyntaxReference => _syntaxReference;
}
