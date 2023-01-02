// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisDiagnostic = LuaDiagnostic;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnostic = MoonScriptDiagnostic;
#endif

/// <summary>
/// 此类型表示一个诊断格式化器，提供格式化<see cref="ThisDiagnostic"/>所需的方法。
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
    /// 仅编译器能创建实例。
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
    /// 获取诊断格式化器的新实例。
    /// </summary>
    /// <value>
    /// 一个诊断格式化器的新实例。
    /// </value>
    public static new
#if LANG_LUA
    LuaDiagnosticFormatter
#elif LANG_MOONSCRIPT
    MoonScriptDiagnosticFormatter
#endif
        Instance
    { get; } = new();

    /// <summary>
    /// 使用可选的<see cref="IFormatProvider"/>格式化<see cref="Diagnostic"/>信息。
    /// </summary>
    /// <inheritdoc cref="Format(ThisDiagnostic, IFormatProvider?)"/>
    public sealed override string Format(Diagnostic diagnostic, IFormatProvider? formatter = null)
    {
        if (diagnostic is not ThisDiagnostic d)
            return base.Format(diagnostic, formatter);

        return this.Format(d, formatter);
    }

    /// <summary>
    /// 使用可选的<see cref="IFormatProvider"/>格式化<see cref="ThisDiagnostic"/>信息。
    /// </summary>
    /// <param name="diagnostic">要格式化的诊断。</param>
    /// <param name="formatter">格式化使用的格式提供器。传入<see langword="null"/>时使用默认的格式提供器。</param>
    /// <returns>格式化后的信息。</returns>
    internal virtual string Format(ThisDiagnostic diagnostic, IFormatProvider? formatter = null) => base.Format(diagnostic, formatter);
}
