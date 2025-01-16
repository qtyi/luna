// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Collections;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript;

internal sealed class ClassDeclaration : Declaration
{
    /// <summary>
    /// The syntax referenced by the declaration.
    /// </summary>
    private readonly SyntaxReference _syntaxReference;
    /// <summary>
    /// The location of the name syntax of the declaration in source code, or <see langword="null"/> if none.
    /// </summary>
    private readonly SourceLocation? _nameLocation;

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

    /// <summary>
    /// Gets the location of the name syntax of the declaration in source code.
    /// </summary>
    /// <value>
    /// The location of the name syntax of the declaration in source code, or <see langword="null"/> if none.
    /// </value>
    public SourceLocation? NameLocation => _nameLocation;

    /// <value>
    /// Always returns <see cref="DeclarationKind.Class"/>.
    /// </value>
    /// <inheritdoc/>
    public override DeclarationKind Kind => DeclarationKind.Class;

    /// <summary>
    /// Gets the names of all members of the declaration.
    /// </summary>
    /// <value>
    /// Names of all members.
    /// </value>
    public ImmutableSegmentedDictionary<string, VoidResult> MemberNames { get; }

    /// <summary>
    /// Create a new instance of <see cref="ModuleDeclaration"/> type.
    /// </summary>
    /// <param name="name">The name of the module.</param>
    /// <param name="syntaxReference">The reference to the chunk syntax.</param>
    /// <param name="nameLocation">The location of the name syntax.</param>
    /// <param name="diagnostics">The diagnostics reported.</param>
    internal ClassDeclaration(
        string name,
        SyntaxReference syntaxReference,
        SourceLocation? nameLocation,
        ImmutableSegmentedDictionary<string, VoidResult> memberNames,
        ImmutableArray<Diagnostic> diagnostics)
        : base(name)
    {
        _syntaxReference = syntaxReference;
        _nameLocation = nameLocation;
        MemberNames = memberNames;
        Diagnostics = diagnostics;
    }

    /// <returns>Always returns an empty array as a Lua module declaration does not contains any child declaration.</returns>
    /// <inheritdoc/>
    protected override ImmutableArray<Declaration> GetDeclarationChildren() => ImmutableArray<Declaration>.Empty;
}
