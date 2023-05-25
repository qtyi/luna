﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
using ThisDiagnostic = LuaDiagnostic;
using ThisDiagnosticFormatter = LuaDiagnosticFormatter;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnostic = MoonScriptDiagnostic;
using ThisDiagnosticFormatter = MoonScriptDiagnosticFormatter;
#endif

/// <summary>
/// A diagnostic, along with the location where it occurred.
/// </summary>
internal sealed class
#if LANG_LUA
    LuaDiagnostic
#elif LANG_MOONSCRIPT
    MoonScriptDiagnostic
#endif
    : DiagnosticWithInfo
{
    /// <summary>
    /// Initialize an instance of <see cref="ThisDiagnostic"/> class with specific information and location.
    /// </summary>
    /// <param name="info">Information about the diagnostic.</param>
    /// <param name="location">A program location in source code where the diagnostic is reported.</param>
    /// <param name="isSuppressed">A value indicate whether the diagnostic is suppressed.</param>
    internal
#if LANG_LUA
        LuaDiagnostic
#elif LANG_MOONSCRIPT
        MoonScriptDiagnostic
#endif
        (DiagnosticInfo info, Location location, bool isSuppressed = false) : base(info, location, isSuppressed) { }

    /// <inheritdoc/>
    public override string ToString() => ThisDiagnosticFormatter.Instance.Format(this);

    /// <inheritdoc/>
    /// <returns>A new diagnostic.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="location"/> is <see langword="null"/>.</exception>
    internal override Diagnostic WithLocation(Location location)
    {
        if (location is null) throw new ArgumentNullException(nameof(location));

        if (this.Location != location)
            return new ThisDiagnostic(this.Info, location, this.IsSuppressed);

        return this;
    }

    /// <inheritdoc/>
    /// <returns>A new diagnostic.</returns>
    internal override Diagnostic WithSeverity(DiagnosticSeverity severity)
    {
        if (this.Severity != severity)
            return new ThisDiagnostic(this.Info.GetInstanceWithSeverity(severity), this.Location, this.IsSuppressed);

        return this;
    }

    /// <inheritdoc/>
    /// <returns>A new diagnostic.</returns>
    internal override Diagnostic WithIsSuppressed(bool isSuppressed)
    {
        if (this.IsSuppressed != isSuppressed)
            return new ThisDiagnostic(this.Info, this.Location, isSuppressed);

        return this;
    }
}
