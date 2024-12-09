// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#else
#error Language not supported.
#endif

internal enum LookupResultKind : byte
{
    Empty,

    Viable
}

/// <summary>
/// Provides helper methods for <see cref="LookupResultKind"/>.
/// </summary>
internal static partial class LookupResultKindExtensions
{
    /// <summary>
    /// Maps a <see cref="LookupResultKind"/> to a <see cref="CandidateReason"/>. Should not be called on <see cref="LookupResultKind.Viable"/>!
    /// </summary>
    public static CandidateReason ToCandidateReason(this LookupResultKind resultKind)
    {
        switch (resultKind)
        {
            case LookupResultKind.Empty: return CandidateReason.None;

            case LookupResultKind.Viable:
                Debug.Assert(false, "Should not call this on LookupResultKind.Viable");
                return CandidateReason.None;

            default:
                throw ExceptionUtilities.UnexpectedValue(resultKind);
        }
    }

    /// <summary>
    /// Return the lowest non-empty result kind.
    /// </summary>
    public static LookupResultKind WorseResultKind(this LookupResultKind resultKind1, LookupResultKind resultKind2)
    {
        if (resultKind1 == LookupResultKind.Empty)
            return resultKind2;
        if (resultKind2 == LookupResultKind.Empty)
            return resultKind1;
        if (resultKind1 < resultKind2)
            return resultKind1;
        else
            return resultKind2;
    }
}
