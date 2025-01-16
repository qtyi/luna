// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal enum DefineState
{
    Defined,
    Undefined,
    Unspecified
}

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal readonly partial struct DirectiveStack
{
    public static readonly DirectiveStack Empty = new(ConsList<Directive>.Empty);

    private readonly ConsList<Directive>? _directives;

    private DirectiveStack(ConsList<Directive>? directives)
    {
        _directives = directives;
    }

    public static void InterlockedInitialize(ref DirectiveStack location, DirectiveStack value)
        => Interlocked.CompareExchange(ref Unsafe.AsRef(in location._directives), value._directives, null);

    public bool IsNull => _directives is null;

    public bool IsEmpty => _directives == ConsList<Directive>.Empty;

    public partial DefineState IsDefined(string id);

    public partial DirectiveStack Add(Directive directive);

    internal string GetDebuggerDisplay()
    {
        if (IsNull) return "<null>";

        if (IsEmpty) return "[]";

        var sb = new StringBuilder();
        for (var current = _directives; current != null && current.Any(); current = current.Tail)
        {
            if (sb.Length > 0)
                sb.Insert(0, " | ");

            sb.Insert(0, current.Head.GetDebuggerDisplay());
        }

        return sb.ToString();
    }

    public bool IncrementallyEquivalent(DirectiveStack other)
    {
        var mine = SkipInsignificantDirectives(_directives);
        var theirs = SkipInsignificantDirectives(other._directives);
        var mineHasAny = mine != null && mine.Any();
        var theirsHasAny = theirs != null && theirs.Any();
        while (mineHasAny && theirsHasAny)
        {
            if (!mine!.Head.IncrementallyEquivalent(theirs!.Head))
                return false;

            mine = SkipInsignificantDirectives(mine.Tail);
            theirs = SkipInsignificantDirectives(theirs.Tail);
            mineHasAny = mine != null && mine.Any();
            theirsHasAny = theirs != null && theirs.Any();
        }

        return mineHasAny == theirsHasAny;
    }

    private static partial ConsList<Directive>? SkipInsignificantDirectives(ConsList<Directive>? directives);
}
