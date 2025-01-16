// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.MoonScript.Syntax;

namespace Qtyi.CodeAnalysis.MoonScript;

partial class MoonScriptSyntaxTree
{
    /// <summary>
    /// 获取语法树的编译单元根节点，这个根节点必须为<see cref="ChunkSyntax"/>类型。
    /// </summary>
    /// <remarks>
    /// 调用此方法前应确认此语法树的<see cref="SyntaxTree.HasCompilationUnitRoot"/>是否为<see langword="true"/>。
    /// </remarks>
    /// <returns>语法树的编译单元根节点。</returns>
    /// <exception cref="InvalidCastException">当<see cref="SyntaxTree.HasCompilationUnitRoot"/>为<see langword="false"/>抛出。</exception>
    /// <inheritdoc cref="MoonScriptSyntaxTree.GetRoot(CancellationToken)"/>
    public ChunkSyntax GetCompilationUnitRoot(CancellationToken cancellationToken = default) =>
        (ChunkSyntax)GetRoot(cancellationToken);
}
