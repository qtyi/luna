// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;
using CoreInternalSyntax = Microsoft.CodeAnalysis.Syntax.InternalSyntax;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxVisitor = LuaSyntaxVisitor<Declaration>;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxVisitor = MoonScriptSyntaxVisitor<Declaration>;
#endif

using Syntax;

/// <summary>
/// A builder that helps build the tree structure of declarations.
/// </summary>
internal sealed partial class DeclarationTreeBuilder : ThisSyntaxVisitor
{
    private readonly SyntaxTree _syntaxTree;
    private readonly string _scriptClassName;
    private readonly bool _isSubmission;

    private DeclarationTreeBuilder(
        SyntaxTree syntaxTree,
        string scriptClassName,
        bool isSubmission)
    {
        this._syntaxTree = syntaxTree;
        this._scriptClassName = scriptClassName;
        this._isSubmission = isSubmission;
    }

    /// <summary>
    /// Build a root declaration for a syntax tree.
    /// </summary>
    /// <param name="syntaxTree">The syntax tree which declaration is build for.</param>
    /// <param name="scriptClassName">The name of the script class.</param>
    /// <param name="isSubmission">Whether it is a script submission.</param>
    /// <returns>The root declaration.</returns>
    public static ModuleDeclaration ForTree(
        SyntaxTree syntaxTree,
        string scriptClassName,
        bool isSubmission)
    {
        var builder = new DeclarationTreeBuilder(syntaxTree, scriptClassName, isSubmission);
        return (ModuleDeclaration)builder.Visit(syntaxTree.GetRoot())!;
    }

    public override Declaration VisitChunk(ChunkSyntax node) => this.CreateRootDeclaration(node);

    /// <summary>
    /// Create a root declaration for a Chunk syntax.
    /// </summary>
    private partial ModuleDeclaration CreateRootDeclaration(ChunkSyntax chunk);
}
