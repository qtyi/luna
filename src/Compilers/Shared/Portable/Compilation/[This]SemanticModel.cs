// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#else
#error Language not supported.
#endif

internal abstract partial class
#if LANG_LUA
    LuaSemanticModel
#elif LANG_MOONSCRIPT
    MoonScriptSemanticModel
#endif
    : SemanticModel
{
}
