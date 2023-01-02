// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

internal sealed class SymbolFactory : SymbolFactory<PEModuleSymbol, TypeSymbol>
{
    /// <summary>
    /// 符号工厂的实例。
    /// </summary>
    internal static readonly SymbolFactory Instance = new();
}
