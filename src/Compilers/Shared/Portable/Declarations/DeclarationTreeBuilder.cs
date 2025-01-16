// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

using Syntax;

/// <summary>
/// A builder that helps build the tree structure of declarations.
/// </summary>
internal sealed partial class DeclarationTreeBuilder : ThisSyntaxVisitor<Declaration>
{
    private readonly SyntaxTree _syntaxTree;
    private readonly string _scriptModuleName;
    private readonly bool _isSubmission;

    private DeclarationTreeBuilder(
        SyntaxTree syntaxTree,
        string scriptModuleName,
        bool isSubmission)
    {
        _syntaxTree = syntaxTree;
        _scriptModuleName = scriptModuleName;
        _isSubmission = isSubmission;
    }

    /// <summary>
    /// Build a root declaration for a syntax tree.
    /// </summary>
    /// <param name="syntaxTree">The syntax tree which declaration is build for.</param>
    /// <param name="scriptModuleName">The name of the script class.</param>
    /// <param name="isSubmission">Whether it is a script submission.</param>
    /// <returns>The root declaration.</returns>
    public static ModuleDeclaration ForTree(
        SyntaxTree syntaxTree,
        string scriptModuleName,
        bool isSubmission)
    {
        var builder = new DeclarationTreeBuilder(syntaxTree, scriptModuleName, isSubmission);
        return (ModuleDeclaration)builder.Visit(syntaxTree.GetRoot())!;
    }

    public override Declaration VisitChunk(ChunkSyntax node) => CreateRootDeclaration(node);

    /// <summary>
    /// Create a root declaration for a Chunk syntax.
    /// </summary>
    private partial ModuleDeclaration CreateRootDeclaration(ChunkSyntax chunk);
}
