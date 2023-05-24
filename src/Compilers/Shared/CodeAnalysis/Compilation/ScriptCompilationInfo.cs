// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilation = LuaCompilation;
using ThisScriptCompilationInfo = LuaScriptCompilationInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilation = MoonScriptCompilation;
using ThisScriptCompilationInfo = MoonScriptScriptCompilationInfo;
#endif

/// <summary>
/// Collects information about a script compilation.
/// </summary>
public sealed class
#if LANG_LUA
    LuaScriptCompilationInfo
#elif LANG_MOONSCRIPT
    MoonScriptScriptCompilationInfo
#endif
    : ScriptCompilationInfo
{
    /// <summary>
    /// Gets a compilation that represent previous script.
    /// </summary>
    /// <value>
    /// An object that represent previous script.
    /// </value>
    public new ThisCompilation? PreviousScriptCompilation { get; }

    /// <summary>
    /// Create a new instance of <see cref="ThisScriptCompilationInfo"/> class.
    /// </summary>
    /// <param name="previousCompilation">A compilation that represent previous script.</param>
    /// <param name="returnType">The return type of script.</param>
    /// <param name="globalsType">The globals type.</param>
    internal
#if LANG_LUA
        LuaScriptCompilationInfo
#elif LANG_MOONSCRIPT
        MoonScriptScriptCompilationInfo
#endif
    (
        ThisCompilation? previousCompilation,
        Type? returnType,
        Type? globalsType) :
        base(returnType, globalsType)
    {
        Debug.Assert(previousCompilation is null || previousCompilation.HostObjectType == globalsType);

        this.PreviousScriptCompilation = previousCompilation;
    }

    /// <summary>
    /// Create a new instance of <see cref="ThisScriptCompilationInfo"/> class with another previous script compilation.
    /// </summary>
    /// <param name="compilation">A compilation that represent previous script.</param>
    /// <returns>A new instance of <see cref="ThisScriptCompilationInfo"/> class with <paramref name="compilation"/> as previous script compilation.</returns>
    public ThisScriptCompilationInfo WithPreviousScriptCompilation(ThisCompilation? compilation) =>
        compilation == this.PreviousScriptCompilation ?
            this :
            new(compilation, this.ReturnTypeOpt, this.GlobalsType);

    #region ScriptCompilationInfo
    internal override Compilation? CommonPreviousScriptCompilation => this.PreviousScriptCompilation;

    internal override ScriptCompilationInfo CommonWithPreviousScriptCompilation(Compilation? compilation) => this.WithPreviousScriptCompilation((ThisCompilation?)compilation);
    #endregion
}
