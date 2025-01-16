// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA

global using Thisc = Qtyi.CodeAnalysis.Lua.CommandLine.Luac;
global using ThisCompiler = Qtyi.CodeAnalysis.Lua.LuaCompiler;
global using ThisCommandLineParser = Qtyi.CodeAnalysis.Lua.LuaCommandLineParser;

#elif LANG_MOONSCRIPT

global using Thisc = Qtyi.CodeAnalysis.MoonScript.CommandLine.Moonc;
global using ThisCompiler = Qtyi.CodeAnalysis.MoonScript.MoonScriptCompiler;
global using ThisCommandLineParser = Qtyi.CodeAnalysis.MoonScript.MoonScriptCommandLineParser;

#else
#error Language not supported.
#endif
