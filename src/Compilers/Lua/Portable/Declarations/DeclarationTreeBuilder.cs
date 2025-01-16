// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua;

using Syntax;

partial class DeclarationTreeBuilder
{
    private partial ModuleDeclaration CreateRootDeclaration(ChunkSyntax chunk) => new(
        name: _scriptModuleName,
        syntaxReference: _syntaxTree.GetReference(chunk),
        diagnostics: ImmutableArray<Diagnostic>.Empty);
}
