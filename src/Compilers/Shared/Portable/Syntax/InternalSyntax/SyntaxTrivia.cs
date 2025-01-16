// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal partial class SyntaxTrivia : ThisInternalSyntaxNode
{
    public readonly string Text;

    internal SyntaxTrivia(
        SyntaxKind kind,
        string text,
        DiagnosticInfo[]? diagnostics = null,
        SyntaxAnnotation[]? annotations = null
    ) : base(kind, diagnostics, annotations, text.Length)
    {
        Text = text;

        if (kind == SyntaxKind.PreprocessingMessageTrivia)
        {
            SetFlags(NodeFlags.ContainsSkippedText);
        }
    }

    public override int Width
    {
        get
        {
            Debug.Assert(FullWidth == Text.Length);
            return FullWidth;
        }
    }

    /// <inheritdoc/>
    public sealed override bool IsDirective => false;

    /// <inheritdoc/>
    public sealed override bool IsToken => false;

    /// <inheritdoc/>
    public sealed override bool IsTrivia => true;

    public virtual bool IsWhitespace => Kind == SyntaxKind.WhitespaceTrivia;

    public virtual bool IsEndOfLine => Kind == SyntaxKind.EndOfLineTrivia;

    #region Accept
    public override void Accept(ThisInternalSyntaxVisitor visitor) => visitor.VisitTrivia(this);

    public override TResult? Accept<TResult>(ThisInternalSyntaxVisitor<TResult> visitor) where TResult : default => visitor.VisitTrivia(this);
    #endregion

    internal static SyntaxTrivia Create(SyntaxKind kind, string text) => new(kind, text);

    [DoesNotReturn]
    internal override SyntaxNode CreateRed(SyntaxNode? parent, int position) => throw ExceptionUtilities.Unreachable();

    [DoesNotReturn]
    internal override GreenNode? GetSlot(int index) => throw ExceptionUtilities.Unreachable();

    public override bool IsEquivalentTo([NotNullWhen(true)] GreenNode? other) =>
        base.IsEquivalentTo(other) && Text == ((SyntaxTrivia)other).Text;

    public override int GetLeadingTriviaWidth() => 0;

    public override int GetTrailingTriviaWidth() => 0;

    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        => new SyntaxTrivia(Kind, Text, diagnostics, GetAnnotations());

    internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
        => new SyntaxTrivia(Kind, Text, GetDiagnostics(), annotations);

    protected override void WriteTriviaTo(TextWriter writer) => writer.Write(Text);

    public override string ToFullString() => Text;

    public override string ToString() => Text;

    public static implicit operator Microsoft.CodeAnalysis.SyntaxTrivia(SyntaxTrivia trivia) =>
        new(token: default, trivia, position: 0, index: 0);
}
