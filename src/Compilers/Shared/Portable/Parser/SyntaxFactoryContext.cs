// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

/// <summary>
/// 为构建语法节点提供上下文信息，有助于决定节点是否能在增量分析中重用。
/// </summary>
/// <remarks>
/// 在<see cref="SyntaxParser"/>外部应为只读（但由于性能原因并不强制限制）。
/// </remarks>
internal partial class SyntaxFactoryContext
{
    /* 此类中存放用于构建语法节点的必要的上下文信息。
     * 
     * 基于Lua的所有支持语言通用的字段放置于此文件；
     * 各语言特定的字段放置于其对应项目的同名文件中。
     */

    private readonly Stack<SyntaxKind> _structureStack = new();

    internal SyntaxKind CurrentStructure => _structureStack.Count > 0 ? _structureStack.Peek() : SyntaxKind.None;

    internal void EnterStructure(SyntaxKind kind) => _structureStack.Push(kind);

    private void LeaveStructure()
    {
        Debug.Assert(_structureStack.Count != 0);
        _structureStack.Pop();
    }

    internal void LeaveStructure(SyntaxKind kind)
    {
        Debug.Assert(_structureStack.Count != 0);
        Debug.Assert(kind == _structureStack.Pop());
    }
}
