// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisParseOptions = LuaParseOptions;
using ThisCompilation = LuaCompilation;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisParseOptions = MoonScriptParseOptions;
using ThisCompilation = MoonScriptCompilation;
#endif

internal static class
#if LANG_LUA
    LuaCompilationExtensions
#elif LANG_MOONSCRIPT
    MoonScriptCompilationExtensions
#endif
{
    internal static bool IsFeatureEnabled(this ThisCompilation compilation, MessageID feature)
    {
        return ((ThisParseOptions?)compilation.SyntaxTrees.FirstOrDefault()?.Options)?.IsFeatureEnabled(feature) == true;
    }

    internal static bool IsFeatureEnabled(this SyntaxNode? syntax, MessageID feature)
    {
        return ((ThisParseOptions?)syntax?.SyntaxTree.Options)?.IsFeatureEnabled(feature) == true;
    }

#warning 未完成
}
