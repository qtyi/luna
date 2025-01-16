// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal readonly partial struct Directive
{
    private readonly DirectiveTriviaSyntax _node;

    internal Directive(DirectiveTriviaSyntax node)
    {
        _node = node;
    }

    public SyntaxKind Kind => _node.Kind;

    internal bool IsActive => _node.IsActive;

    internal partial bool BranchTaken { get; }

    internal partial string? GetIdentifier();

    public bool IncrementallyEquivalent(Directive other)
    {
        if (Kind != other.Kind)
            return false;

        var isActive = IsActive;
        var otherIsActive = other.IsActive;

        // states of inactive directives don't matter
        if (!isActive && !otherIsActive)
            return true;

        if (isActive != otherIsActive)
            return false;

        return IncrementallyEquivalentWithoutActiveCheck(other);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private partial bool IncrementallyEquivalentWithoutActiveCheck(Directive other);

    // Can't be private as it's called by DirectiveStack in its GetDebuggerDisplay()
    internal string GetDebuggerDisplay()
    {
        using var writer = new StringWriter(System.Globalization.CultureInfo.InvariantCulture);
        _node.WriteTo(writer, leading: false, trailing: false);
        return writer.ToString();
    }
}
