// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxTree = LuaSyntaxTree;
using ThisCompilation = LuaCompilation;
using ThisSemanticModel = LuaSemanticModel;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxTree = MoonScriptSyntaxTree;
using ThisCompilation = MoonScriptCompilation;
using ThisSemanticModel = MoonScriptSemanticModel;
#endif

using Symbols;

internal abstract partial class
#if LANG_LUA
    LuaSemanticModel
#elif LANG_MOONSCRIPT
    MoonScriptSemanticModel
#endif
    : SemanticModel
{
}
