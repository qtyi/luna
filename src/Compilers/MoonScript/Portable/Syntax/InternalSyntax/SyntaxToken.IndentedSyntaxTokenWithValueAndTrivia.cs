// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;
using MSCA::Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class SyntaxToken
{
    internal class IndentedSyntaxTokenWithValueAndTrivia<T> : IndentedSyntaxTokenWithValue<T>
    {
        static IndentedSyntaxTokenWithValueAndTrivia() => ObjectBinder.RegisterTypeReader(typeof(IndentedSyntaxTokenWithValueAndTrivia<T>), r => new IndentedSyntaxTokenWithValueAndTrivia<T>(r));

        private readonly GreenNode? _leading;
        private readonly GreenNode? _trailing;

        internal IndentedSyntaxTokenWithValueAndTrivia(
            SyntaxKind kind,
            string text,
            T? value,
            int innerIndent,
            GreenNode? leading,
            GreenNode? trailing
        ) : base(kind, text, value, innerIndent)
        {
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

        internal IndentedSyntaxTokenWithValueAndTrivia(
            SyntaxKind kind,
            string text,
            T? value,
            int innerIndent,
            GreenNode? leading,
            GreenNode? trailing,
            DiagnosticInfo[]? diagnostics,
            SyntaxAnnotation[]? annotations
        ) : base(kind, text, value, innerIndent, diagnostics, annotations)
        {
            if (leading is not null)
            {
                this.AdjustFlagsAndWidth(leading);
                this._leading = leading;
            }
            if (trailing is not null)
            {
                this.AdjustFlagsAndWidth(trailing);
                this._trailing = trailing;
            }
        }

        internal IndentedSyntaxTokenWithValueAndTrivia(ObjectReader reader) : base(reader)
        {
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
            writer.WriteValue(this._leading);
            writer.WriteValue(this._trailing);
        }

        public override GreenNode? GetLeadingTrivia() => this._leading;

        public override GreenNode? GetTrailingTrivia() => this._trailing;

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new IndentedSyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, this._innerIndent, trivia, this.GetTrailingTrivia(), this.GetDiagnostics(), this.GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new IndentedSyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, this._innerIndent, this.GetLeadingTrivia(), trivia, this.GetDiagnostics(), this.GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new IndentedSyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, this._innerIndent, this.GetLeadingTrivia(), this.GetTrailingTrivia(), diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
             new IndentedSyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, this._innerIndent, this.GetLeadingTrivia(), this.GetTrailingTrivia(), this.GetDiagnostics(), annotations);
    }
}
