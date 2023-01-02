// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;
using MSCA::Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class SyntaxToken
{
    internal class IndentedSyntaxTokenWithValue<T> : SyntaxTokenWithValue<T>
    {
        protected readonly int _innerIndent;

        static IndentedSyntaxTokenWithValue() => ObjectBinder.RegisterTypeReader(typeof(IndentedSyntaxTokenWithValue<T>), r => new IndentedSyntaxTokenWithValue<T>(r));

        internal IndentedSyntaxTokenWithValue(SyntaxKind kind, string text, T? value, int innerIndent) : base(kind, text, value)
        {
            this._innerIndent = innerIndent;
        }

        internal IndentedSyntaxTokenWithValue(SyntaxKind kind, string text, T? value, int innerIndent, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, text, value, diagnostics, annotations)
        {
            this._innerIndent = innerIndent;
        }

        internal IndentedSyntaxTokenWithValue(ObjectReader reader) : base(reader)
        {
            this._innerIndent = reader.ReadInt32();
        }

        internal override void WriteTo(ObjectWriter writer)
        {
            base.WriteTo(writer);
            writer.WriteInt32(this._innerIndent);
        }

        public override bool TryGetInnerWhiteSpaceIndent(out int indent)
        {
            indent = this._innerIndent;
            return true;
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new IndentedSyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, this._innerIndent, trivia, null, this.GetDiagnostics(), this.GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new IndentedSyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, this._innerIndent, null, trivia, this.GetDiagnostics(), this.GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new IndentedSyntaxTokenWithValue<T>(this.Kind, this._text, this._value, this._innerIndent, diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
            new IndentedSyntaxTokenWithValue<T>(this.Kind, this._text, this._value, this._innerIndent, this.GetDiagnostics(), annotations);
    }
}
