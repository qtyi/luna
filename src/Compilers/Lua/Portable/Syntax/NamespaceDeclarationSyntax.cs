// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax;

partial class NamespaceDeclarationSyntax
{
    public override AnnotatedWithClauseSyntax? AnnotatedWithClause => null;

    internal override DeclarationSyntax AddAnnotatedWithClauseAttributeListCore(params ObjectCreationExpressionSyntax[] items) => this;

    internal override DeclarationSyntax WithAnnotatedWithClauseCore(AnnotatedWithClauseSyntax? annotatedWithClause) => this;
}
