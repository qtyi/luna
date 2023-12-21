// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisSyntaxNode = Qtyi.CodeAnalysis.Lua.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisSyntaxNode = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxNode;
#endif

internal readonly partial struct Blender
{
    private readonly Lexer _lexer;
    private readonly Cursor _oldTreeCursor;
    private readonly ImmutableStack<TextChangeRange> _changes;

    private readonly int _newPosition;
    private readonly int _changeDelta;
    private readonly LexerMode _newLexerDrivenMode;

    public Blender(
        Lexer lexer,
        ThisSyntaxNode? oldTree,
        IEnumerable<TextChangeRange>? changes)
    {
        this._lexer = lexer;
        this._changes = ImmutableStack.Create<TextChangeRange>();

        if (changes is not null)
        {
            var collapsed = TextChangeRange.Collapse(changes);

            var affectedRange = ExtendToAffectedRange(oldTree, collapsed);
            this._changes = this._changes.Push(affectedRange);
        }

        if (oldTree is null)
        {
            this._oldTreeCursor = new();
            this._newPosition = lexer.TextWindow.Position;
        }
        else
        {
            this._oldTreeCursor = Cursor.FromRoot(oldTree).MoveToFirstChild();
            this._newPosition = 0;
        }

        this._changeDelta = 0;
        this._newLexerDrivenMode = LexerMode.None;
    }

    private Blender(
        Lexer lexer,
        Cursor oldTreeCursor,
        ImmutableStack<TextChangeRange> changes,
        int newPosition,
        int changeDelta,
        LexerMode newLexerDrivenMode
    )
    {
        this._lexer = lexer;
        this._oldTreeCursor = oldTreeCursor;
        this._changes = changes;
        this._newPosition = newPosition;
        this._changeDelta = changeDelta;
        this._newLexerDrivenMode = newLexerDrivenMode;
    }

    private static TextChangeRange ExtendToAffectedRange(ThisSyntaxNode oldTree, TextChangeRange changeRange)
    {
        const int maxLookahead = 1;

        var lastCharIndex = oldTree.FullWidth - 1;

        var start = Math.Max(Math.Min(changeRange.Span.Start, lastCharIndex), 0);

        for (var i = 0; start > 0 && i <= maxLookahead;)
        {
            var token = oldTree.FindToken(start, findInsideTrivia: false);
            Debug.Assert(token.Kind() != SyntaxKind.None);

            start = Math.Max(0, token.Position - 1);

            if (token.FullWidth > 0) i++;
        }

        var finalSpan = TextSpan.FromBounds(start, changeRange.Span.End);
        var finalLength = changeRange.NewLength + (changeRange.Span.Start - start);
        return new TextChangeRange(finalSpan, finalLength);
    }

    public BlendedNode ReadNode(LexerMode mode) => this.ReadNodeOrToken(mode, asToken: false);

    public BlendedNode ReadToken(LexerMode mode) => this.ReadNodeOrToken(mode, asToken: true);

    private BlendedNode ReadNodeOrToken(LexerMode mode, bool asToken)
    {
        var reader = new Reader(this);
        return reader.ReadNodeOrToken(mode, asToken);
    }
}
