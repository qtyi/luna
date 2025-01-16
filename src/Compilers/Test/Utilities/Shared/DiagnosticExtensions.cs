// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Test.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisDiagnostic = LuaDiagnostic;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnostic = MoonScriptDiagnostic;
#endif

internal static class DiagnosticExtensions
{
    public static void Verify(this IEnumerable<DiagnosticInfo> actual, params DiagnosticDescription[] expected)
        => actual.Select(static info => new ThisDiagnostic(info, NoLocation.Singleton)).Verify(expected);

    public static void Verify(this ImmutableArray<DiagnosticInfo> actual, params DiagnosticDescription[] expected)
        => actual.Select(static info => new ThisDiagnostic(info, NoLocation.Singleton)).Verify(expected);

    public static string ToLocalizedString(this MessageID id)
        => new LocalizableErrorArgument(id).ToString(format: null, formatProvider: null);
}
