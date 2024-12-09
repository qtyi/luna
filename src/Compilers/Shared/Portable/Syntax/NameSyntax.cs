// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax;
#endif

partial class NameSyntax
{
    /// <summary>
    /// 返回限定名称的非限定的（最右侧的）部分，若名称已经是非限定的，则返回自身。
    /// </summary>
    /// <returns>限定名称的非限定的（最右侧的）部分，若名称已经是非限定的，则返回自身。</returns>
    internal abstract IdentifierNameSyntax GetUnqualifiedName();
}
