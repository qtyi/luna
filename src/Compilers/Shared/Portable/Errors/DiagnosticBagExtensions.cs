// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

internal static class DiagnosticBagExtensions
{
    /// <summary>
    /// Add a diagnostic to the bag.
    /// </summary>
    /// <param name="diagnostics">The diagnostic bag to add to.</param>
    /// <param name="code">Error code of a diagnostic.</param>
    /// <param name="location">Location of a diagnostic.</param>
    /// <returns>The info of the diagnostic being added.</returns>
    internal static ThisDiagnosticInfo Add(this DiagnosticBag diagnostics,
        ErrorCode code,
        Location location)
    {
        var info = new ThisDiagnosticInfo(code);
        var diag = new ThisDiagnostic(info, location);
        diagnostics.Add(diag);
        return info;
    }

    /// <inheritdoc cref="Add(DiagnosticBag, ErrorCode, Location)"/>
    /// <param name="args">Arguments for formatting a text.</param>
    internal static ThisDiagnosticInfo Add(this DiagnosticBag diagnostics,
        ErrorCode code,
        Location location,
        params object[] args)
    {
        var info = new ThisDiagnosticInfo(code, args);
        var diag = new ThisDiagnostic(info, location);
        diagnostics.Add(diag);
        return info;
    }

    /// <inheritdoc cref="Add(DiagnosticBag, ErrorCode, Location, object[])"/>
    /// <param name="symbols">A collection of symbols a diagnostic is in.</param>
    internal static ThisDiagnosticInfo Add(this DiagnosticBag diagnostics,
        ErrorCode code,
        Location location,
        ImmutableArray<Symbol> symbols,
        params object[] args)
    {
        var info = new ThisDiagnosticInfo(code, args, symbols, ImmutableArray<Location>.Empty);
        var diag = new ThisDiagnostic(info, location);
        diagnostics.Add(diag);
        return info;
    }

    /// <summary>
    /// Add a diagnostic to the bag.
    /// </summary>
    /// <param name="diagnostics">The diagnostic bag to add to.</param>
    /// <param name="info">Information about a diagnostic.</param>
    /// <param name="location">Location of a diagnostic.</param>
    internal static void Add(this DiagnosticBag diagnostics,
        DiagnosticInfo info,
        Location location)
    {
        var diag = new ThisDiagnostic(info, location);
        diagnostics.Add(diag);
    }

    /// <summary>
    /// Adds diagnostics from useSiteDiagnostics into diagnostics and returns <see langword="true"/> if there were any errors.
    /// </summary>
    /// <param name="diagnostic">The diagnostic bag to add to.</param>
    /// <param name="node">The syntax node which <paramref name="useSiteDiagnostics"/> are on its location.</param>
    /// <param name="useSiteDiagnostics">A collection of use-site diagnostics to be added.</param>
    /// <returns><see langword="true"/> if there were any errors; otherwise, <see langword="false"/>.</returns>
    internal static bool Add(this DiagnosticBag diagnostic,
        SyntaxNode node,
        HashSet<DiagnosticInfo> useSiteDiagnostics) =>
        !useSiteDiagnostics.IsNullOrEmpty() && diagnostic.Add(node.Location, useSiteDiagnostics);

    /// <summary>
    /// Adds diagnostics from useSiteDiagnostics into diagnostics and returns <see langword="true"/> if there were any errors.
    /// </summary>
    /// <param name="diagnostic">The diagnostic bag to add to.</param>
    /// <param name="token">The syntax token which <paramref name="useSiteDiagnostics"/> are on its location.</param>
    /// <param name="useSiteDiagnostics">A collection of use-site diagnostics to be added.</param>
    /// <returns><see langword="true"/> if there were any errors; otherwise, <see langword="false"/>.</returns>
    internal static bool Add(this DiagnosticBag diagnostic,
        SyntaxToken token,
        HashSet<DiagnosticInfo> useSiteDiagnostics) =>
        !useSiteDiagnostics.IsNullOrEmpty() && diagnostic.Add(token.GetLocation(), useSiteDiagnostics);

    /// <summary>
    /// Adds diagnostics from useSiteDiagnostics into diagnostics and returns <see langword="true"/> if there were any errors.
    /// </summary>
    /// <param name="diagnostic">The diagnostic bag to add to.</param>
    /// <param name="location">The location where <paramref name="useSiteDiagnostics"/> are added on.</param>
    /// <param name="useSiteDiagnostics">A collection of use-site diagnostics to be added.</param>
    /// <returns><see langword="true"/> if there were any errors; otherwise, <see langword="false"/>.</returns>
    internal static bool Add(this DiagnosticBag diagnostic,
        Location location,
        IReadOnlyCollection<DiagnosticInfo> useSiteDiagnostics)
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
