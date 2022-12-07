// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
using System;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua;

using ThisMessageProvider = Lua.MessageProvider;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisMessageProvider = MoonScript.MessageProvider;
#endif

public partial class
#if LANG_LUA
    LuaCommandLineParser
#elif LANG_MOONSCRIPT
    MoonScriptCommandLineParser
#endif
    : CommandLineParser
{
    internal
#if LANG_LUA
        LuaCommandLineParser
#elif LANG_MOONSCRIPT
        MoonScriptCommandLineParser
#endif
        (bool isScriptCommandLineParser = false)
        : base(ThisMessageProvider.Instance, isScriptCommandLineParser) { }

}
