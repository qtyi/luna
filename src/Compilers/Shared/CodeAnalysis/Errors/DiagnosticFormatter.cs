// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

public class
#if LANG_LUA
    LuaDiagnosticFormatter
#elif LANG_MOONSCRIPT
    MoonScriptDiagnosticFormatter
#endif
    : Microsoft.CodeAnalysis.DiagnosticFormatter
{
    internal
#if LANG_LUA
    LuaDiagnosticFormatter
#elif LANG_MOONSCRIPT
    MoonScriptDiagnosticFormatter
#endif
        ()
    { }

    public static new
#if LANG_LUA
    LuaDiagnosticFormatter
#elif LANG_MOONSCRIPT
    MoonScriptDiagnosticFormatter
#endif
        Instance
    { get; } = new();
}
