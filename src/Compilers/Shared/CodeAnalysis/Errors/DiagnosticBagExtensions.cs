// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisDiagnostic = LuaDiagnostic;
using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnostic = MoonScriptDiagnostic;
using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

internal static class DiagnosticBagExtensions
{
    internal static ThisDiagnosticInfo Add(this DiagnosticBag diagnostics, ErrorCode code, Location location)
    {
        var info = new ThisDiagnosticInfo(code);
        var diag = new ThisDiagnostic(info, location);
        diagnostics.Add(diag);
        return info;
    }

    internal static ThisDiagnosticInfo Add(this DiagnosticBag diagnostics, ErrorCode code, Location location, params object[] args)
    {
        var info = new ThisDiagnosticInfo(code, args);
        var diag = new ThisDiagnostic(info, location);
        diagnostics.Add(diag);
        return info;
    }

    internal static ThisDiagnosticInfo Add(this DiagnosticBag diagnostics, ErrorCode code, Location location, ImmutableArray<Symbol> symbols, params object[] args)
    {
        var info = new ThisDiagnosticInfo(code, args, symbols, ImmutableArray<Location>.Empty);
        var diag = new ThisDiagnostic(info, location);
        diagnostics.Add(diag);
        return info;
    }

    internal static void Add(this DiagnosticBag diagnostics, DiagnosticInfo info, Location location)
    {
        var diag = new ThisDiagnostic(info, location);
        diagnostics.Add(diag);
    }

    internal static bool Add(this DiagnosticBag diagnostic, SyntaxNode node, HashSet<DiagnosticInfo> useSiteDiagnostics) =>
        !useSiteDiagnostics.IsNullOrEmpty() && diagnostic.Add(node.Location, useSiteDiagnostics);

    internal static bool Add(this DiagnosticBag diagnostic, SyntaxToken token, HashSet<DiagnosticInfo> useSiteDiagnostics) =>
        !useSiteDiagnostics.IsNullOrEmpty() && diagnostic.Add(token.GetLocation(), useSiteDiagnostics);

    internal static bool Add(this DiagnosticBag diagnostic, Location location, IReadOnlyCollection<DiagnosticInfo> useSiteDiagnostics)
    {
        if (useSiteDiagnostics.IsNullOrEmpty()) return false;

        var haveErrors = false;

        foreach (var info in useSiteDiagnostics)
        {
            if (info.Severity == DiagnosticSeverity.Error)
                haveErrors = true;

            diagnostic.Add(new ThisDiagnostic(info, location));
        }

        return haveErrors;
    }
}
