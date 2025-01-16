// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.


#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

/// <summary>
/// 一种特殊的<see cref="NetmoduleSymbol"/>，表示缺失的.NET模块符号（与<see cref="NonMissingNetmoduleSymbol"/>相对）。
/// </summary>
internal abstract partial class MissingNetmoduleSymbol : NetmoduleSymbol
{
}
