// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
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

internal class
#if LANG_LUA
    LuaDiagnostic
#elif LANG_MOONSCRIPT
    MoonScriptDiagnostic
#endif
    : DiagnosticWithInfo
{
    internal
#if LANG_LUA
        LuaDiagnostic
#elif LANG_MOONSCRIPT
        MoonScriptDiagnostic
#endif
        (DiagnosticInfo info, Location location, bool isSuppressed = false) : base(info, location, isSuppressed) { }

    public override string ToString() => ThisDiagnosticFormatter.Instance.Format(this);

    internal override Diagnostic WithLocation(Location location)
    {
        if (this.Location != location)
            return new ThisDiagnostic(this.Info, location, this.IsSuppressed);
        else return this;
    }

    internal override Diagnostic WithSeverity(DiagnosticSeverity severity)
    {
        if (this.Severity != severity)
            return new ThisDiagnostic(this.Info.GetInstanceWithSeverity(severity), this.Location, this.IsSuppressed);
        else return this;
    }

    internal override Diagnostic WithIsSuppressed(bool isSuppressed)
    {
        if (this.IsSuppressed != isSuppressed)
            return new ThisDiagnostic(this.Info, this.Location, isSuppressed);
        else return this;
    }
}
