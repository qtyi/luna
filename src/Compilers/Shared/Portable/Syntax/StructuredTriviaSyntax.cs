// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax;
#endif

public abstract partial class StructuredTriviaSyntax : ThisSyntaxNode, IStructuredTriviaSyntax
{
    private SyntaxTrivia _parent;

    /// <summary>
    /// 获取上级语法琐碎内容。
    /// </summary>
    public override SyntaxTrivia ParentTrivia => _parent;

    internal StructuredTriviaSyntax(
        ThisInternalSyntaxNode green,
        SyntaxNode? parent,
        int position)
        : base(
            green,
            position,
            parent?.SyntaxTree) =>
        Debug.Assert(parent is null || position >= 0);

    /// <summary>
    /// 从语法琐碎内容中创建一个结构化语法节点的实例。
    /// </summary>
    /// <param name="trivia">提供必要信息的语法琐碎内容。</param>
    /// <returns>包含信息的结构化语法节点。</returns>
    internal static StructuredTriviaSyntax Create(SyntaxTrivia trivia)
    {
        var node = trivia.RequiredUnderlyingNode;
        var parent = trivia.Token.Parent;
        var position = trivia.Position;
        var red = (StructuredTriviaSyntax)node.CreateRed(parent, position);
        red._parent = trivia;
        return red;
    }
}
