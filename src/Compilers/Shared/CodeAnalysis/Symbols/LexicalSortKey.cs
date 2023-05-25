// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisCompilation = LuaCompilation;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisCompilation = MoonScriptCompilation;
#endif

internal struct LexicalSortKey : IComparable<LexicalSortKey>, IEquatable<LexicalSortKey>
{
    private int _treeOrdinal;
    private int _position;

    public int TreeOrdinal => this._treeOrdinal;
    public int Position => this._position;

    public bool IsInSource => Volatile.Read(ref this._treeOrdinal) >= 0;
    public bool IsInMetadata => Volatile.Read(ref this._treeOrdinal) < 0;
    public bool IsInitialized => Volatile.Read(ref this._position) >= 0;

    public static readonly LexicalSortKey NotInSource = new() { _treeOrdinal = -1, _position = 0 };
    public static readonly LexicalSortKey NotInitialized = new() { _treeOrdinal = -1, _position = -1 };

    private LexicalSortKey(int treeOrdinal, int position)
    {
        Debug.Assert(treeOrdinal >= 0);
        Debug.Assert(position >= 0);
        this._treeOrdinal = treeOrdinal;
        this._position = position;
    }

    private LexicalSortKey(
        SyntaxTree tree,
        int position,
        ThisCompilation compilation)
        : this(compilation.GetSyntaxTreeOrdinal(tree), position)
    { }

    public LexicalSortKey(
        SyntaxReference syntaxRef,
        ThisCompilation compilation)
        : this(syntaxRef.SyntaxTree, syntaxRef.Span.Start, compilation)
    { }

    public LexicalSortKey(
        Location location,
        ThisCompilation compilation)
        : this(
            location.SourceTree ?? throw new ArgumentException(LunaResources.LocationIsNotInSyntaxTree, nameof(location)),
            location.SourceSpan.Start,
            compilation)
    { }

    public LexicalSortKey(
        ThisSyntaxNode node,
        ThisCompilation compilation)
        : this(node.SyntaxTree, node.SpanStart, compilation)
    { }

    public LexicalSortKey(
        SyntaxToken token,
        ThisCompilation compilation)
        : this(
            token.SyntaxTree ?? throw new ArgumentException(LunaResources.SyntaxTokenIsNotInSyntaxTree, nameof(token)),
            token.SpanStart,
            compilation)
    { }

    int IComparable<LexicalSortKey>.CompareTo(LexicalSortKey other) => LexicalSortKey.Compare(this, other);

    public static int Compare(LexicalSortKey x, LexicalSortKey y)
    {
        int comparison;

        if (x.TreeOrdinal != y.TreeOrdinal)
        {
            if (x.TreeOrdinal < 0)
                return 1;
            else if (y.TreeOrdinal < 0)
                return -1;

            comparison = x.TreeOrdinal - y.TreeOrdinal;
            Debug.Assert(comparison != 0);
            return comparison;
        }

        return x.Position - y.Position;
    }

    public override bool Equals(object? obj) => obj is LexicalSortKey sortKey && this.Equals(sortKey);

    bool IEquatable<LexicalSortKey>.Equals(LexicalSortKey other) => LexicalSortKey.Compare(this, other) == 0;

    public override int GetHashCode() => this._treeOrdinal.GetHashCode() ^ this._position.GetHashCode();

    public static LexicalSortKey First(LexicalSortKey xSortKey, LexicalSortKey ySortKey)
    {
        var comparison = LexicalSortKey.Compare(xSortKey, ySortKey);
        return comparison > 0 ? ySortKey : xSortKey;
    }

    public void SetFrom(LexicalSortKey other)
    {
        Debug.Assert(other.IsInitialized);
        Volatile.Write(ref this._treeOrdinal, other._treeOrdinal);
        Volatile.Write(ref this._position, other._position);
    }
}
