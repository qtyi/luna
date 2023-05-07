// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = LuaSyntaxNode;
using SyntaxTriviaList = Microsoft.CodeAnalysis.Syntax.InternalSyntax.SyntaxList<LuaSyntaxNode>;
#elif LANG_MOONSCRIPT

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
using SyntaxTriviaList = Microsoft.CodeAnalysis.Syntax.InternalSyntax.SyntaxList<MoonScriptSyntaxNode>;
#endif

internal partial class SyntaxToken : ThisInternalSyntaxNode
{
    static SyntaxToken()
    {
        ObjectBinder.RegisterTypeReader(typeof(SyntaxToken), r => new SyntaxToken(r));

        SyntaxToken.InitializeTokensWithWellKnownText();
    }

    public virtual SyntaxKind ContextualKind => this.Kind;
    public sealed override int RawContextualKind => (int)this.ContextualKind;

    public virtual string Text => SyntaxFacts.GetText(this.Kind);
    public virtual string ValueText => this.Text;

    public override int Width => this.Text.Length;

    internal SyntaxTriviaList LeadingTrivia => new(this.GetLeadingTrivia());

    internal SyntaxTriviaList TrailingTrivia => new(this.GetTrailingTrivia());

    public sealed override bool IsToken => true;

    #region 构造函数
    internal SyntaxToken(SyntaxKind kind) : base(kind)
    {
        this.SetFullWidth();
        this.SetIsNotMissingFlag();
    }

    internal SyntaxToken(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        this.SetFullWidth();
        this.SetIsNotMissingFlag();
    }

    internal SyntaxToken(SyntaxKind kind, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        this.SetFullWidth();
        this.SetIsNotMissingFlag();
    }

    internal SyntaxToken(SyntaxKind kind, int fullWidth) : base(kind, fullWidth)
    {
        this.SetIsNotMissingFlag();
    }

    internal SyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics, fullWidth)
    {
        this.SetIsNotMissingFlag();
    }

    internal SyntaxToken(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations, fullWidth)
    {
        this.SetIsNotMissingFlag();
    }

    internal SyntaxToken(ObjectReader reader) : base(reader)
    {
        var text = this.Text;
        if (text is not null) this.FullWidth = text.Length;

        this.SetIsNotMissingFlag();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetFullWidth() => this.FullWidth = this.Text.Length;

    /// <summary>
    /// 在此基类上添加<see cref="Microsoft.CodeAnalysis.GreenNode.NodeFlags.IsNotMissing"/>标记。若子类要表示缺失的语法标记，则在子类中移除这个标记。
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private void SetIsNotMissingFlag() => this.SetFlags(NodeFlags.IsNotMissing);
    #endregion

    #region 这些成员在各语言的独立项目中定义
    // FirstTokenWithWellKnownText常量
    // LastTokenWithWellKnownText常量
    // s_tokensWithNoTrivia字段
    // s_tokensWithElasticTrivia字段
    // s_tokensWithSingleTrailingSpace字段
    // s_tokensWithSingleTrailingCRLF字段

    /// <summary>
    /// 在<see cref="SyntaxToken"/>的静态构造函数中初始化所有已知文本的标记。
    /// </summary>
    protected static partial void InitializeTokensWithWellKnownText();
    #endregion

    internal static SyntaxToken Create(SyntaxKind kind)
    {
        if (kind > SyntaxToken.LastTokenWithWellKnownText)
        {
            if (!SyntaxFacts.IsAnyToken(kind))
                throw new ArgumentException(string.Format(LunaResources.ThisMethodCanOnlyBeUsedToCreateTokens, kind), nameof(kind));
            else
                return SyntaxToken.CreateMissing(kind, null, null);
        }

        return SyntaxToken.s_tokensWithNoTrivia[(int)kind].Value;
    }

    internal static SyntaxToken Create(SyntaxKind kind, GreenNode? leading, GreenNode? trailing)
    {
        if (kind > SyntaxToken.LastTokenWithWellKnownText)
        {
            if (!SyntaxFacts.IsAnyToken(kind))
                throw new ArgumentException(string.Format(LunaResources.ThisMethodCanOnlyBeUsedToCreateTokens, kind), nameof(kind));
            else
                return SyntaxToken.CreateMissing(kind, null, null);
        }

        if (leading is null)
        {
            if (trailing is null)
                return SyntaxToken.s_tokensWithNoTrivia[(int)kind].Value;
            else if (trailing == SyntaxFactory.Space)
                return SyntaxToken.s_tokensWithSingleTrailingSpace[(int)kind].Value;
            else if (trailing == SyntaxFactory.CarriageReturnLineFeed)
                return SyntaxToken.s_tokensWithSingleTrailingCRLF[(int)kind].Value;
        }
        else if (leading == SyntaxFactory.ElasticZeroSpace && trailing == SyntaxFactory.ElasticZeroSpace)
            return SyntaxToken.s_tokensWithElasticTrivia[(int)kind].Value;

        return new SyntaxTokenWithTrivia(kind, leading, trailing);
    }

    internal static SyntaxToken CreateMissing(SyntaxKind kind, GreenNode? leading, GreenNode? trailing) => new MissingTokenWithTrivia(kind, leading, trailing);

    internal static SyntaxToken Identifier(string text) => new SyntaxIdentifier(text);

    internal static SyntaxToken Identifier(GreenNode? leading, string text, GreenNode? trailing)
    {
        if (leading is null && trailing is null)
            return SyntaxToken.Identifier(text);
        else
            return new SyntaxIdentifierWithTrivia(SyntaxKind.IdentifierToken, text, text, leading, trailing);
    }

    internal static SyntaxToken Identifier(SyntaxKind contextualKind, GreenNode? leading, string text, string valueText, GreenNode? trailing)
    {
        if (contextualKind == SyntaxKind.IdentifierName && valueText == text)
            return SyntaxToken.Identifier(leading, text, trailing);
        else
            return new SyntaxIdentifierWithTrivia(contextualKind, text, valueText, leading, trailing);
    }

    internal static SyntaxToken WithValue<T>(SyntaxKind kind, string text, T? value) => new SyntaxTokenWithValue<T>(kind, text, value);

    internal static SyntaxToken WithValue<T>(SyntaxKind kind, GreenNode? leading, string text, T? value, GreenNode? trailing) => new SyntaxTokenWithValueAndTrivia<T>(kind, text, value, leading, trailing);

    /// <summary>
    /// 表示语法标记在序列化过程中是否应被重用。
    /// </summary>
    internal override bool ShouldReuseInSerialization => base.ShouldReuseInSerialization &&
        // 同时不应超过词法器的最大缓存标记空间。
        this.FullWidth < Lexer.MaxCachedTokenSize;

    /// <exception cref="InvalidOperationException">此方法永远不会被调用。</exception>
    internal sealed override GreenNode? GetSlot(int index) => throw ExceptionUtilities.Unreachable;

    #region 常见标记
    private static readonly ArrayElement<SyntaxToken>[] s_tokensWithNoTrivia = new ArrayElement<SyntaxToken>[(int)SyntaxToken.LastTokenWithWellKnownText + 1];
    private static readonly ArrayElement<SyntaxToken>[] s_tokensWithElasticTrivia = new ArrayElement<SyntaxToken>[(int)SyntaxToken.LastTokenWithWellKnownText + 1];
    private static readonly ArrayElement<SyntaxToken>[] s_tokensWithSingleTrailingSpace = new ArrayElement<SyntaxToken>[(int)SyntaxToken.LastTokenWithWellKnownText + 1];
    private static readonly ArrayElement<SyntaxToken>[] s_tokensWithSingleTrailingCRLF = new ArrayElement<SyntaxToken>[(int)SyntaxToken.LastTokenWithWellKnownText + 1];

    protected static partial void InitializeTokensWithWellKnownText()
    {
        for (var kind = SyntaxToken.FirstTokenWithWellKnownText; kind <= SyntaxToken.LastTokenWithWellKnownText; kind++)
        {
            SyntaxToken.s_tokensWithNoTrivia[(int)kind].Value = new SyntaxToken(kind);
            SyntaxToken.s_tokensWithElasticTrivia[(int)kind].Value = new SyntaxTokenWithTrivia(kind, SyntaxFactory.ElasticZeroSpace, SyntaxFactory.ElasticZeroSpace);
            SyntaxToken.s_tokensWithSingleTrailingSpace[(int)kind].Value = new SyntaxTokenWithTrivia(kind, null, SyntaxFactory.Space);
            SyntaxToken.s_tokensWithSingleTrailingCRLF[(int)kind].Value = new SyntaxTokenWithTrivia(kind, null, SyntaxFactory.CarriageReturnLineFeed);
        }
    }

    internal static IEnumerable<SyntaxToken> GetWellKnownTokens()
    {
        foreach (var token in SyntaxToken.s_tokensWithNoTrivia)
        {
            if (token.Value is not null) yield return token.Value;
        }

        foreach (var token in SyntaxToken.s_tokensWithElasticTrivia)
        {
            if (token.Value is not null) yield return token.Value;
        }

        foreach (var token in SyntaxToken.s_tokensWithSingleTrailingSpace)
        {
            if (token.Value is not null) yield return token.Value;
        }

        foreach (var token in SyntaxToken.s_tokensWithSingleTrailingCRLF)
        {
            if (token.Value is not null) yield return token.Value;
        }
    }
    #endregion

    public override object? GetValue() => this.Value;

    public override string GetValueText() => this.ValueText;

    public override int GetLeadingTriviaWidth()
    {
        var leading = this.GetLeadingTrivia();
        return leading is null ? 0 : leading.FullWidth;
    }

    public override int GetTrailingTriviaWidth()
    {
        var trailing = this.GetTrailingTrivia();
        return trailing is null ? 0 : trailing.FullWidth;
    }

    public override GreenNode WithLeadingTrivia(GreenNode? trivia) => this.TokenWithLeadingTrivia(trivia);

    public virtual SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
        new SyntaxTokenWithTrivia(this.Kind, trivia, null, this.GetDiagnostics(), this.GetAnnotations());

    public override GreenNode WithTrailingTrivia(GreenNode? trivia) => this.TokenWithTrailingTrivia(trivia);

    public virtual SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
        new SyntaxTokenWithTrivia(this.Kind, null, trivia, this.GetDiagnostics(), this.GetAnnotations());

    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        Debug.Assert(this.GetType() == typeof(SyntaxToken));
        return new SyntaxToken(this.Kind, this.FullWidth, diagnostics, this.GetAnnotations());
    }

    internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        Debug.Assert(this.GetType() == typeof(SyntaxToken));
        return new SyntaxToken(this.Kind, this.FullWidth, this.GetDiagnostics(), annotations);
    }

    #region 访问方法
    public override TResult? Accept<TResult>(
#if LANG_LUA
        LuaSyntaxVisitor<TResult>
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxVisitor<TResult>
#endif
        visitor) where TResult : default => visitor.VisitToken(this);

    public override void Accept(
#if LANG_LUA
        LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxVisitor
#endif
        visitor) => visitor.VisitToken(this);
    #endregion

    protected override void WriteTokenTo(TextWriter writer, bool leading, bool trailing)
    {
        if (leading) this.GetLeadingTrivia()?.WriteTo(writer, true, true);

        writer.Write(this.Text);

        if (trailing) this.GetTrailingTrivia()?.WriteTo(writer, true, true);
    }

    public override bool IsEquivalentTo([NotNullWhen(true)] GreenNode? other)
    {
        if (!base.IsEquivalentTo(other)) return false;

        var otherToken = (SyntaxToken)other;
        if (this.Text != otherToken.Text) return false;

        var thisLeading = this.GetLeadingTrivia();
        var otherLeading = otherToken.GetLeadingTrivia();
        if (thisLeading != otherLeading)
        {
            if (thisLeading is null || otherLeading is null) return false;
            else if (!thisLeading.IsEquivalentTo(otherLeading)) return false;
        }

        var thisTrailing = this.GetTrailingTrivia();
        var otherTrailing = otherToken.GetTrailingTrivia();
        if (thisTrailing != otherTrailing)
        {
            if (thisTrailing is null || otherTrailing is null) return false;
            else if (!thisTrailing.IsEquivalentTo(otherTrailing)) return false;
        }

        return true;
    }

    /// <exception cref="InvalidOperationException">此方法永远不会被调用。</exception>
    internal sealed override SyntaxNode CreateRed(SyntaxNode? parent, int position) => throw ExceptionUtilities.Unreachable;

    public override string ToString() => this.Text;
}
