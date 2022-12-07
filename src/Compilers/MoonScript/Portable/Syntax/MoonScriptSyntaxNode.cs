// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript;

public abstract partial class MoonScriptSyntaxNode
{
    /// <summary>
    /// 获取内部绿树节点。
    /// </summary>
    internal Syntax.InternalSyntax.MoonScriptSyntaxNode MoonScriptGreen => (Syntax.InternalSyntax.MoonScriptSyntaxNode)this.Green;
}
