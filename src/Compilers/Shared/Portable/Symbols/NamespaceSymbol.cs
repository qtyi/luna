// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

/// <summary>
/// A TypeSymbol is a base class for all the symbols that represent a type.
/// </summary>
abstract partial class NamespaceSymbol : NamespaceOrTypeSymbol, INamespaceSymbolInternal
{
    /// <inheritdoc cref="NamespaceOrTypeSymbol()"/>
    internal NamespaceSymbol() { }

    public virtual bool IsGlobalNamespace
    {
        get
        {
#warning Not implemented.
            throw new NotImplementedException();
        }
    }
}
