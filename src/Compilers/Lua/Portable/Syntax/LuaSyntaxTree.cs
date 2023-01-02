// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Syntax;

namespace Qtyi.CodeAnalysis.Lua;

partial class LuaSyntaxTree
{
    /// <summary>
    /// 获取语法树的编译单元根节点，这个根节点必须为<see cref="ChunkSyntax"/>类型。
    /// </summary>
    /// <remarks>
    /// 调用此方法前应确认此语法树的<see cref="SyntaxTree.HasCompilationUnitRoot"/>是否为<see langword="true"/>。
    /// </remarks>
    /// <returns>语法树的编译单元根节点。</returns>
    /// <exception cref="InvalidCastException">当<see cref="SyntaxTree.HasCompilationUnitRoot"/>为<see langword="false"/>抛出。</exception>
    /// <inheritdoc cref="LuaSyntaxTree.GetRoot(CancellationToken)"/>
    public ChunkSyntax GetCompilationUnitRoot(CancellationToken cancellationToken = default) =>
        (ChunkSyntax)this.GetRoot(cancellationToken);
}
