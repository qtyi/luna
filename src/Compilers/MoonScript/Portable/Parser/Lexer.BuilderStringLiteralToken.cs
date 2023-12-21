// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    private sealed class BuilderStringLiteralToken : SyntaxToken
    {
        static BuilderStringLiteralToken() => ObjectBinder.RegisterTypeReader(typeof(BuilderStringLiteralToken), r => new BuilderStringLiteralToken(r));

        private readonly string _text;
        private readonly ArrayBuilder<string?>? _builder;
        private string? _value;
        private int _innerIndent;

        private readonly GreenNode? _leading;
        private readonly GreenNode? _trailing;

        public override string Text => this._text;

        [MemberNotNull(nameof(_value))]
        public override object? Value
        {
            get
            {
                this.Build();
                return this._value;
            }
        }

        [MemberNotNull(nameof(_value))]
        public override string ValueText => Convert.ToString(this.Value, CultureInfo.InvariantCulture) ?? string.Empty;

        internal ArrayBuilder<string?>? Builder => this._builder;

        internal int InnerIndent { get => this._innerIndent; set => this._innerIndent = value; }

        internal BuilderStringLiteralToken(
            SyntaxKind kind,
            string text,
            ArrayBuilder<string?> builder,
            int innerIndent,
            GreenNode? leading,
            GreenNode? trailing) : base(kind, text.Length)
        {
            this._text = text;
            this._builder = builder;
            this._innerIndent = innerIndent;

            if (leading is not null)
            {
                this.AdjustFlagsAndWidth(leading);
                this._leading = leading;
            }
            if (trailing is not null)
            {
                this.AdjustFlagsAndWidth(trailing);
                _trailing = trailing;
            }
        }

        internal BuilderStringLiteralToken(
            SyntaxKind kind,
            string text,
            ArrayBuilder<string?> builder,
            int innerIndent,
            GreenNode? leading,
            GreenNode? trailing,
            DiagnosticInfo[]? diagnostics,
            SyntaxAnnotation[]? annotations) : base(kind, text.Length, diagnostics, annotations)
        {
            this._text = text;
            this._builder = builder;
            this._innerIndent = innerIndent;

            if (leading is not null)
            {
                this.AdjustFlagsAndWidth(leading);
                this._leading = leading;
            }
            if (trailing is not null)
            {
                this.AdjustFlagsAndWidth(trailing);
                _trailing = trailing;
            }
        }

        internal BuilderStringLiteralToken(ObjectReader reader) : base(reader)
        {
            this._text = reader.ReadString();
            this.FullWidth = this._text.Length;
            this._builder = null;
            this._value = reader.ReadString();
            this._innerIndent = reader.ReadInt32();

            var leading = (GreenNode?)reader.ReadValue();
            if (leading is not null)
            {
                this.AdjustFlagsAndWidth(leading);
                this._leading = leading;
            }
            var trailing = (GreenNode?)reader.ReadValue();
            if (trailing is not null)
            {
                this.AdjustFlagsAndWidth(trailing);
                this._trailing = trailing;
            }
        }

        internal override void WriteTo(ObjectWriter writer)
        {
            base.WriteTo(writer);
            writer.WriteString(this._text);
            writer.WriteString(this._value);
            writer.WriteInt32(this._innerIndent);

            writer.WriteValue(this._leading);
            writer.WriteValue(this._trailing);
        }

        public override GreenNode? GetLeadingTrivia() => this._leading;

        public override GreenNode? GetTrailingTrivia() => this._trailing;

        public override bool TryGetInnerWhiteSpaceIndent(out int indent)
        {
            indent = this._innerIndent;
            return true;
        }

        [MemberNotNull(nameof(_value))]
        internal void Build()
        {
            if (this._value is not null) return;
            if (this._builder is null)
            {
                this._value ??= string.Empty;
                return;
            }

            TrimIndent(this._builder, this._innerIndent);

            this._value = string.Concat(this._builder.ToImmutableOrEmptyAndFree());
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new IndentedSyntaxTokenWithValueAndTrivia<string>(this.Kind, this._text, this._value, this._innerIndent, trivia, this.GetTrailingTrivia(), this.GetDiagnostics(), this.GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new IndentedSyntaxTokenWithValueAndTrivia<string>(this.Kind, this._text, this._value, this._innerIndent, this.GetLeadingTrivia(), trivia, this.GetDiagnostics(), this.GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new IndentedSyntaxTokenWithValueAndTrivia<string>(this.Kind, this._text, this._value, this._innerIndent, this.GetLeadingTrivia(), this.GetTrailingTrivia(), diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
             new IndentedSyntaxTokenWithValueAndTrivia<string>(this.Kind, this._text, this._value, this._innerIndent, this.GetLeadingTrivia(), this.GetTrailingTrivia(), this.GetDiagnostics(), annotations);
    }
}
