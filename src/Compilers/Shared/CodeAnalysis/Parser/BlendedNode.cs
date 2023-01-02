// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisSyntaxNode = Lua.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisSyntaxNode = MoonScript.MoonScriptSyntaxNode;
#endif

/// <summary>
/// 表示已协调的节点。
/// </summary>
internal readonly struct BlendedNode
{
    internal readonly ThisSyntaxNode? Node;
    internal readonly SyntaxToken Token;
    internal readonly Blender Blender;

    internal BlendedNode(ThisSyntaxNode? node, SyntaxToken token, Blender blender)
    {
        this.Node = node;
        this.Token = token;
        this.Blender = blender;
    }
}
