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

internal partial class SyntaxTrivia :
#if LANG_LUA
    LuaSyntaxNode
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxNode
#endif
{
    public readonly string Text;

    static SyntaxTrivia()
    {
        ObjectBinder.RegisterTypeReader(typeof(SyntaxTrivia), r => new SyntaxTrivia(r));
    }

    internal override void WriteTo(ObjectWriter writer)
    {
        base.WriteTo(writer);
        writer.WriteString(this.Text);
    }

    internal SyntaxTrivia(
        SyntaxKind kind,
        string text,
        DiagnosticInfo[]? diagnostics = null,
        SyntaxAnnotation[]? annotations = null) : base(kind, diagnostics, annotations, text.Length)
    {
        this.Text = text;
    }

    internal SyntaxTrivia(ObjectReader reader) : base(reader)
    {
        this.Text = reader.ReadString();
        this.FullWidth = this.Text.Length;
    }

    public override int GetLeadingTriviaWidth() => 0;

    public override int GetTrailingTriviaWidth() => 0;

    public override int Width
    {
        get
        {
            Debug.Assert(this.FullWidth == this.Text.Length);
            return this.FullWidth;
        }
    }

    /// <summary>此语法节点是否为指令。</summary>
    /// <remarks>此属性的值永远为<see langword="false"/>。</remarks>
    public sealed override bool IsDirective => false;

    /// <summary>此语法节点是否为标志。</summary>
    /// <remarks>此属性的值永远为<see langword="false"/>。</remarks>
    public sealed override bool IsToken => false;

    /// <summary>此语法节点是否为琐碎内容。</summary>
    /// <remarks>此属性的值永远为<see langword="true"/>。</remarks>
    public sealed override bool IsTrivia => true;

    public virtual bool IsWhiteSpace => this.Kind == SyntaxKind.WhiteSpaceTrivia;

    public virtual bool IsEndOfLine => this.Kind == SyntaxKind.EndOfLineTrivia;

    internal override bool ShouldReuseInSerialization => this.Kind == SyntaxKind.WhiteSpaceTrivia && this.FullWidth < Lexer.MaxCachedTokenSize;

    public override void Accept(
#if LANG_LUA
        LuaSyntaxVisitor
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxVisitor
#endif
        visitor) => visitor.VisitTrivia(this);

    public override TResult? Accept<TResult>(
#if LANG_LUA
        LuaSyntaxVisitor<TResult>
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxVisitor<TResult>
#endif
        visitor) where TResult : default => visitor.VisitTrivia(this);

    internal static SyntaxTrivia Create(SyntaxKind kind, string text) => new(kind, text);

    internal override SyntaxNode CreateRed(SyntaxNode? parent, int position) => throw ExceptionUtilities.Unreachable;

    internal override GreenNode? GetSlot(int index) => throw ExceptionUtilities.Unreachable;

    public override bool IsEquivalentTo([NotNullWhen(true)] GreenNode? other) =>
        base.IsEquivalentTo(other) && this.Text == ((SyntaxTrivia)other).Text;

    public override string ToFullString() => this.Text;

    public override string ToString() => this.Text;

    internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
        new SyntaxTrivia(this.Kind, this.Text, diagnostics, this.GetAnnotations());

    internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
        new SyntaxTrivia(this.Kind, this.Text, this.GetDiagnostics(), annotations);

    protected override void WriteTriviaTo(TextWriter writer) => writer.Write(this.Text);

    public static implicit operator Microsoft.CodeAnalysis.SyntaxTrivia(SyntaxTrivia trivia) =>
        new(token: default, trivia, position: 0, index: 0);
}
