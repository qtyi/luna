// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class DirectiveTriviaSyntax
{
    public sealed override bool IsDirective => true;

    internal override DirectiveStack ApplyDirectives(DirectiveStack stack) => stack.Add(new(this));
}
