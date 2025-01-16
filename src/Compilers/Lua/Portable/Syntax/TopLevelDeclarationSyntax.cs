// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax;

partial class TopLevelDeclarationSyntax
{
    public sealed override ModifierListSyntax? Modifiers => null;

    internal sealed override DeclarationSyntax AddModifiersModifiersCore(params ModifierSyntax[] items) => this;

    internal sealed override DeclarationSyntax WithModifiersCore(ModifierListSyntax? modifiers) => this;
}
