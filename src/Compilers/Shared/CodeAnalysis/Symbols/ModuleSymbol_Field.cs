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
    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.IFieldSymbolInternal.IsVolatile"/>.
    /// </summary>
    protected abstract bool IsVolatileCore { get; }

    #region Microsoft.CodeAnalysis.Symbols.IFieldSymbolInternal
    bool Microsoft.CodeAnalysis.Symbols.IFieldSymbolInternal.IsVolatile => this.IsVolatileCore;
    #endregion
}
