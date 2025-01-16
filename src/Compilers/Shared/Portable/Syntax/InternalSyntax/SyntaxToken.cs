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

using SyntaxTriviaList = Microsoft.CodeAnalysis.Syntax.InternalSyntax.SyntaxList<ThisInternalSyntaxNode>;

/// <summary>
/// A terminal node, that consists of one or many characters, representing identifier, keyword, punctuation, const value or other mininal unit that affect the meaning of an internal syntax tree.
/// </summary>
internal partial class SyntaxToken : ThisInternalSyntaxNode
{
    /// <summary>
    /// Gets contextual syntax kind of this token.
    /// </summary>
    public virtual SyntaxKind ContextualKind => Kind;

    public sealed override int RawContextualKind => (int)ContextualKind;

    /// <summary>
    /// Gets text representation of this token.
    /// </summary>
    public virtual string Text => SyntaxFacts.GetText(Kind);

    public virtual partial object? Value { get; }

    /// <summary>
    /// Gets text representation of the value this token represents.
    /// </summary>
    public virtual string ValueText => Text;

    public override int Width => Text.Length;

    internal SyntaxTriviaList LeadingTrivia => new(GetLeadingTrivia());

    internal SyntaxTriviaList TrailingTrivia => new(GetTrailingTrivia());

    public sealed override bool IsToken => true;

    #region Constructors
    //====================
    // Optimization: Normally, we wouldn't accept this much duplicate code, but these constructors
    // are called A LOT and we want to keep them as short and simple as possible and increase the
    // likelihood that they will be inlined.

    internal SyntaxToken(SyntaxKind kind) : base(kind)
    {
        FullWidth = Text.Length;
        SetFlags(NodeFlags.IsNotMissing);
    }

    internal SyntaxToken(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        FullWidth = Text.Length;
        SetFlags(NodeFlags.IsNotMissing);
    }

    internal SyntaxToken(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        FullWidth = Text.Length;
        SetFlags(NodeFlags.IsNotMissing);
    }

    internal SyntaxToken(SyntaxKind kind, int fullWidth) : base(kind, fullWidth)
    {
        SetFlags(NodeFlags.IsNotMissing);
    }

    internal SyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics, fullWidth)
    {
        SetFlags(NodeFlags.IsNotMissing);
    }

    internal SyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations, fullWidth)
    {
        SetFlags(NodeFlags.IsNotMissing);
    }
    #endregion

    #region Create
    internal static SyntaxToken Create(SyntaxKind kind)
    {
        if (kind > LastTokenWithWellKnownText)
        {
            if (!SyntaxFacts.IsAnyToken(kind))
                throw new ArgumentException(string.Format(LunaResources.ThisMethodCanOnlyBeUsedToCreateTokens, kind), nameof(kind));

            return CreateMissing(kind, null, null);
        }

        return s_tokensWithNoTrivia[(int)kind].Value;
    }

    internal static SyntaxToken Create(SyntaxKind kind, GreenNode? leading, GreenNode? trailing)
    {
        if (kind > LastTokenWithWellKnownText)
        {
            if (!SyntaxFacts.IsAnyToken(kind))
                throw new ArgumentException(string.Format(LunaResources.ThisMethodCanOnlyBeUsedToCreateTokens, kind), nameof(kind));

            return CreateMissing(kind, leading, trailing);
        }

        if (leading is null)
        {
            if (trailing is null)
                return s_tokensWithNoTrivia[(int)kind].Value;
            else if (trailing == ThisInternalSyntaxFactory.Space)
                return s_tokensWithSingleTrailingSpace[(int)kind].Value;
            else if (trailing == ThisInternalSyntaxFactory.CarriageReturnLineFeed)
                return s_tokensWithSingleTrailingCRLF[(int)kind].Value;
        }
        else if (leading == ThisInternalSyntaxFactory.ElasticZeroSpace && trailing == ThisInternalSyntaxFactory.ElasticZeroSpace)
            return s_tokensWithElasticTrivia[(int)kind].Value;

        return new SyntaxTokenWithTrivia(kind, leading, trailing);
    }

    internal static SyntaxToken CreateMissing(SyntaxKind kind, GreenNode? leading, GreenNode? trailing)
        => new MissingTokenWithTrivia(kind, leading, trailing);
    #endregion

    #region Identifier
    internal static SyntaxToken Identifier(string text) => new SyntaxIdentifier(text);

    internal static SyntaxToken Identifier(GreenNode? leading, string text, GreenNode? trailing)
    {
        if (leading is null && trailing is null)
            return Identifier(text);
        else
            return new SyntaxIdentifierWithTrivia(SyntaxKind.IdentifierToken, text, text, leading, trailing);
    }

    internal static SyntaxToken Identifier(SyntaxKind contextualKind, GreenNode? leading, string text, string valueText, GreenNode? trailing)
    {
        if (contextualKind == SyntaxKind.IdentifierName && valueText == text)
            return Identifier(leading, text, trailing);
        else
            return new SyntaxIdentifierWithTrivia(contextualKind, text, valueText, leading, trailing);
    }
    #endregion

    #region WithValue
    internal static SyntaxToken WithValue<T>(SyntaxKind kind, string text, T value) where T : notnull
        => new SyntaxTokenWithValue<T>(kind, text, value);

    internal static SyntaxToken WithValue<T>(SyntaxKind kind, GreenNode? leading, string text, T value, GreenNode? trailing) where T : notnull
        => new SyntaxTokenWithValueAndTrivia<T>(kind, text, value, leading, trailing);
    #endregion

    /// <exception cref="InvalidOperationException">This override method is though to be unreachable.</exception>
    [DoesNotReturn]
    internal sealed override GreenNode? GetSlot(int index) => throw ExceptionUtilities.Unreachable();

    #region Well known tokens
    private static readonly ArrayElement<SyntaxToken>[] s_tokensWithNoTrivia = new ArrayElement<SyntaxToken>[(int)LastTokenWithWellKnownText + 1];
    private static readonly ArrayElement<SyntaxToken>[] s_tokensWithElasticTrivia = new ArrayElement<SyntaxToken>[(int)LastTokenWithWellKnownText + 1];
    private static readonly ArrayElement<SyntaxToken>[] s_tokensWithSingleTrailingSpace = new ArrayElement<SyntaxToken>[(int)LastTokenWithWellKnownText + 1];
    private static readonly ArrayElement<SyntaxToken>[] s_tokensWithSingleTrailingCRLF = new ArrayElement<SyntaxToken>[(int)LastTokenWithWellKnownText + 1];

    static SyntaxToken()
    {
        for (var kind = FirstTokenWithWellKnownText; kind <= LastTokenWithWellKnownText; kind++)
        {
            s_tokensWithNoTrivia[(int)kind].Value = new SyntaxToken(kind);
            s_tokensWithElasticTrivia[(int)kind].Value = new SyntaxTokenWithTrivia(kind, ThisInternalSyntaxFactory.ElasticZeroSpace, ThisInternalSyntaxFactory.ElasticZeroSpace);
            s_tokensWithSingleTrailingSpace[(int)kind].Value = new SyntaxTokenWithTrivia(kind, null, ThisInternalSyntaxFactory.Space);
            s_tokensWithSingleTrailingCRLF[(int)kind].Value = new SyntaxTokenWithTrivia(kind, null, ThisInternalSyntaxFactory.CarriageReturnLineFeed);
        }
    }

    internal static IEnumerable<SyntaxToken> GetWellKnownTokens()
    {
        foreach (var token in s_tokensWithNoTrivia)
        {
            if (token.Value is not null) yield return token.Value;
        }

        foreach (var token in s_tokensWithElasticTrivia)
        {
            if (token.Value is not null) yield return token.Value;
        }

        foreach (var token in s_tokensWithSingleTrailingSpace)
        {
            if (token.Value is not null) yield return token.Value;
        }

        foreach (var token in s_tokensWithSingleTrailingCRLF)
        {
            if (token.Value is not null) yield return token.Value;
        }
    }
    #endregion

    public override object? GetValue() => Value;

    public override string GetValueText() => ValueText;

    public override int GetLeadingTriviaWidth()
    {
        var leading = GetLeadingTrivia();
        return leading is null ? 0 : leading.FullWidth;
    }

    public override int GetTrailingTriviaWidth()
    {
        var trailing = GetTrailingTrivia();
        return trailing is null ? 0 : trailing.FullWidth;
    }

    public sealed override GreenNode WithLeadingTrivia(GreenNode? trivia) => TokenWithLeadingTrivia(trivia);

    public virtual SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
        new SyntaxTokenWithTrivia(Kind, trivia, GetTrailingTrivia(), GetDiagnostics(), GetAnnotations());

    public sealed override GreenNode WithTrailingTrivia(GreenNode? trivia) => TokenWithTrailingTrivia(trivia);

    public virtual SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
        new SyntaxTokenWithTrivia(Kind, GetLeadingTrivia(), trivia, GetDiagnostics(), GetAnnotations());

    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        Debug.Assert(GetType() == typeof(SyntaxToken));
        return new SyntaxToken(Kind, FullWidth, diagnostics, GetAnnotations());
    }

    internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        Debug.Assert(GetType() == typeof(SyntaxToken));
        return new SyntaxToken(Kind, FullWidth, GetDiagnostics(), annotations);
    }

    internal override DirectiveStack ApplyDirectives(DirectiveStack stack)
    {
        if (ContainsDirectives)
        {
            stack = ApplyDirectivesToTrivia(GetLeadingTrivia(), stack);
            stack = ApplyDirectivesToTrivia(GetTrailingTrivia(), stack);
        }

        return stack;
    }

    private static DirectiveStack ApplyDirectivesToTrivia(GreenNode? triviaList, DirectiveStack stack)
    {
        if (triviaList is not null && triviaList.ContainsDirectives)
            return ApplyDirectivesToListOrNode(triviaList, stack);

        return stack;
    }

    #region Accept
    public override TResult? Accept<TResult>(ThisInternalSyntaxVisitor<TResult> visitor) where TResult : default => visitor.VisitToken(this);

    public override void Accept(ThisInternalSyntaxVisitor visitor) => visitor.VisitToken(this);
    #endregion

    protected override void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
    {
        if (leading)
            GetLeadingTrivia()?.WriteTo(writer, true, true);

        writer.Write(Text);

        if (trailing)
            GetTrailingTrivia()?.WriteTo(writer, true, true);
    }

    public override bool IsEquivalentTo([NotNullWhen(true)] GreenNode? other)
    {
        if (!base.IsEquivalentTo(other))
            return false;

        var otherToken = (SyntaxToken)other;
        if (Text != otherToken.Text)
            return false;

        var thisLeading = GetLeadingTrivia();
        var otherLeading = otherToken.GetLeadingTrivia();
        if (thisLeading != otherLeading)
        {
            if (thisLeading is null || otherLeading is null)
                return false;
            else if (!thisLeading.IsEquivalentTo(otherLeading)) 
                return false;
        }

        var thisTrailing = GetTrailingTrivia();
        var otherTrailing = otherToken.GetTrailingTrivia();
        if (thisTrailing != otherTrailing)
        {
            if (thisTrailing is null || otherTrailing is null)
                return false;
            else if (!thisTrailing.IsEquivalentTo(otherTrailing))
                return false;
        }

        return true;
    }

    /// <exception cref="InvalidOperationException">此方法永远不会被调用。</exception>
    /// <inheritdoc/>
    [DoesNotReturn]
    internal sealed override SyntaxNode CreateRed(SyntaxNode? parent, int position) => throw ExceptionUtilities.Unreachable();

    /// <inheritdoc/>
    public override string ToString() => Text;
}
