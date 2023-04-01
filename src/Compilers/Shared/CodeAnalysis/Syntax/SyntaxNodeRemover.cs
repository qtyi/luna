// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax;

using ThisSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax;

using ThisSyntaxNode = MoonScriptSyntaxNode;
#endif

internal static class SyntaxNodeRemover
{
    internal static TRoot? RemoveNodes<TRoot>(
        TRoot root,
        IEnumerable<ThisSyntaxNode>? nodes,
        SyntaxRemoveOptions options)
        where TRoot : ThisSyntaxNode
    {
#warning 未实现。
        throw new NotImplementedException();
    }
}
