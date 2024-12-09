// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#else
#error Language not supported.
#endif

/// <summary>
/// The formatter to format a <see cref="ThisDiagnostic"/>.
/// </summary>
public class
#if LANG_LUA
    LuaDiagnosticFormatter
#elif LANG_MOONSCRIPT
    MoonScriptDiagnosticFormatter
#endif
    : DiagnosticFormatter
{
    /// <remarks>
    /// Prevent anyone else from deriving from this class.
    /// </remarks>
    internal
#if LANG_LUA
        LuaDiagnosticFormatter
#elif LANG_MOONSCRIPT
        MoonScriptDiagnosticFormatter
#endif
        ()
    { }

    /// <summary>
    /// Get the unique instance of <see cref="ThisDiagnosticFormatter"/>.
    /// </summary>
    /// <value>
    /// The unique instance of <see cref="ThisDiagnosticFormatter"/>.
    /// </value>
    public static new ThisDiagnosticFormatter Instance { get; } = new();
}
