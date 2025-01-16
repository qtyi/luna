// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Syntax.InternalSyntax;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

/// <summary>
/// Represents one or a sequence of minimal units in the internal syntax tree. It can be a token, a trivia or a list of other nodes.
/// </summary>
[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal abstract partial class
#if LANG_LUA
    LuaSyntaxNode
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxNode
#endif
    : GreenNode
{
    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind) : base((ushort)kind) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, int fullWidth) : base((ushort)kind, fullWidth) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base((ushort)kind, diagnostics) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, DiagnosticInfo[]? diagnostics, int fullWidth) : base((ushort)kind, diagnostics, fullWidth) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base((ushort)kind, diagnostics, annotations) => GreenStats.NoteGreen(this);

    internal
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
        (SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations, int fullWidth) : base((ushort)kind, diagnostics, annotations, fullWidth) => GreenStats.NoteGreen(this);

    /// <summary>
    /// Gets the source language.
    /// </summary>
    public sealed override string Language =>
#if LANG_LUA
        LanguageNames.Lua;
#elif LANG_MOONSCRIPT
        LanguageNames.MoonScript;
#endif

    public SyntaxKind Kind => (SyntaxKind)RawKind;

    public override string KindText => Kind.ToString();

    public override int RawContextualKind => RawKind;

    public override bool IsStructuredTrivia => this is StructuredTriviaSyntax;

    public override bool IsDirective => false;

    public sealed override bool IsDocumentationCommentTrivia => false;

#if DEBUG
    public override int GetSlotOffset(int index)
    {
        // This implementation should not support arbitrary
        // length lists since the implementation is O(n).
        Debug.Assert(index is >= 0 and < MaxSlotCount);

        return base.GetSlotOffset(index);
    }
#endif

    public SyntaxToken? GetFirstToken() => (SyntaxToken?)GetFirstTerminal();

    public SyntaxToken? GetLastToken() => (SyntaxToken?)GetLastTerminal();

    public SyntaxToken? GetLastNonmissingToken() => (SyntaxToken?)GetLastNonmissingTerminal();

    public virtual GreenNode? GetLeadingTrivia() => null;
    public sealed override GreenNode? GetLeadingTriviaCore() => GetLeadingTrivia();

    public virtual GreenNode? GetTrailingTrivia() => null;
    public sealed override GreenNode? GetTrailingTriviaCore() => GetTrailingTrivia();

    public abstract TResult? Accept<TResult>(ThisInternalSyntaxVisitor<TResult> visitor);

    public abstract void Accept(ThisInternalSyntaxVisitor visitor);

    internal virtual DirectiveStack ApplyDirectives(DirectiveStack stack) => ApplyDirectives(this, stack);

    internal static DirectiveStack ApplyDirectives(GreenNode node, DirectiveStack stack)
    {
        if (node.ContainsDirectives)
        {
            for (int i = 0, n = node.SlotCount; i < n; i++)
            {
                var child = node.GetSlot(i);
                if (child is not null)
                    stack = ApplyDirectivesToListOrNode(child, stack);
            }
        }

        return stack;
    }

    internal static DirectiveStack ApplyDirectivesToListOrNode(GreenNode listOrNode, DirectiveStack stack)
    {
        // If we have a list of trivia, then that node is not actually a CSharpSyntaxNode.
        // Just defer to our standard ApplyDirectives helper as it will do the appropriate
        // walking of this list to ApplyDirectives to the children.
        if (listOrNode.RawKind == ListKind)
            return ApplyDirectives(listOrNode, stack);
        else
            // Otherwise, we must have an actual piece of C# trivia.  Just apply the stack
            // to that node directly.
            return ((ThisInternalSyntaxNode)listOrNode).ApplyDirectives(stack);
    }

    internal virtual IList<DirectiveTriviaSyntax> GetDirectives()
    {
        if (ContainsDirectives)
        {
            var list = new List<DirectiveTriviaSyntax>(32);
            GetDirectives(this, list);
            return list;
        }

        return SpecializedCollections.EmptyList<DirectiveTriviaSyntax>();
    }

    private static void GetDirectives(GreenNode? node, List<DirectiveTriviaSyntax> directives)
    {
        if (node is not null && node.ContainsDirectives)
        {
            if (node is DirectiveTriviaSyntax d)
                directives.Add(d);
            else
            {
                if (node is SyntaxToken t)
                {
                    GetDirectives(t.GetLeadingTrivia(), directives);
                    GetDirectives(t.GetTrailingTrivia(), directives);
                }
                else
                {
                    for (int i = 0, n = node.SlotCount; i < n; i++)
                        GetDirectives(node.GetSlot(i), directives);
                }
            }
        }
    }

    /// <summary>
    /// Set syntax factory context to this node.
    /// </summary>
    /// <param name="context">Context containing syntax factory information.</param>
    /// <remarks>
    /// This should probably be an extra constructor parameter, but we don't need more constructor overloads.
    /// </remarks>
    protected partial void SetFactoryContext(SyntaxFactoryContext context);

    /// <inheritdoc/>
    /// <summary>
    /// Create a separator token for <see cref="Microsoft.CodeAnalysis.SeparatedSyntaxList{TNode}"/> according to the list element.
    /// </summary>
    /// <param name="element">The element in / to be added to the list.</param>
    /// <returns>A syntax token, which is the trailing token of <paramref name="element"/> or the common separator token, in <see cref="Microsoft.CodeAnalysis.SeparatedSyntaxList{TNode}"/>.</returns>
    public sealed override partial Microsoft.CodeAnalysis.SyntaxToken CreateSeparator(SyntaxNode element);

    public virtual bool IsTokenAtEndOfLine() =>
        this is SyntaxToken token &&
            token.TrailingTrivia.Count > 0 &&
                token.TrailingTrivia.Last?.IsTriviaWithEndOfLine() == true;

    /// <inheritdoc/>
    /// <summary>
    /// Checks if this node is a trivia with EOF.
    /// </summary>
    public override partial bool IsTriviaWithEndOfLine();

    // Use conditional weak table so we always return same identity for structured trivia
    private static readonly ConditionalWeakTable<SyntaxNode, Dictionary<Microsoft.CodeAnalysis.SyntaxTrivia, WeakReference<SyntaxNode>>> s_structuresTable = new();

    /// <inheritdoc/>
    /// <summary>
    /// Gets the syntax node represented the structure of this trivia, if any. The HasStructure property can be used to 
    /// determine if this trivia has structure.
    /// </summary>
    /// <exception cref="ArgumentException">If this trivia node does not have structure.</exception>
    /// <returns>
    /// A <see cref="ThisSyntaxNode"/> derived from <see cref="Syntax.StructuredTriviaSyntax"/>, with the structured view of this trivia node.
    /// </returns>
    /// <remarks>
    /// Some types of trivia have structure that can be accessed as additional syntax nodes.
    /// These forms of trivia include: 
    ///   directives, where the structure describes the structure of the directive.
    ///   documentation comments, where the structure describes the XML structure of the comment.
    ///   skipped tokens, where the structure describes the tokens that were skipped by the parser.
    /// </remarks>
    public override SyntaxNode GetStructure(Microsoft.CodeAnalysis.SyntaxTrivia trivia)
    {
        if (!trivia.HasStructure) throw new ArgumentException(string.Format(LunaResources.ArgIsNotStructuredTrivia), nameof(trivia));

        var parent = trivia.Token.Parent;
        if (parent is null)
            return Syntax.StructuredTriviaSyntax.Create(trivia);
        else
        {
            SyntaxNode? structure;
            var structsInParent = s_structuresTable.GetOrCreateValue(parent);
            lock (structsInParent)
            {
                if (!structsInParent.TryGetValue(trivia, out var weekStructure))
                {
                    structure = Syntax.StructuredTriviaSyntax.Create(trivia);
                    structsInParent.Add(trivia, new(structure));
                }
                else if (!weekStructure.TryGetTarget(out structure))
                {
                    structure = Syntax.StructuredTriviaSyntax.Create(trivia);
                    structsInParent.Add(trivia, new(structure));
                }
            }

            return structure;
        }
    }
}
