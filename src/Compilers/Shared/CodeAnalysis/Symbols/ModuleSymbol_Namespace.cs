// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class ModuleSymbol
{
    #region Microsoft.CodeAnalysis.Symbols.INamespaceSymbolInternal
    bool Microsoft.CodeAnalysis.Symbols.INamespaceSymbolInternal.IsGlobalNamespace => this.IsGlobalModule && this.IsNamespace;
    #endregion
}
