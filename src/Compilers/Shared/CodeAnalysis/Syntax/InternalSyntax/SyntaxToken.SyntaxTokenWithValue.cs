// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class SyntaxToken
{
    internal class SyntaxTokenWithValue<T> : SyntaxToken
    {
        static SyntaxTokenWithValue() => ObjectBinder.RegisterTypeReader(typeof(SyntaxTokenWithValue<T>), r => new SyntaxTokenWithValue<T>(r));

        protected readonly string _text;
        protected readonly T? _value;

        public override string Text => this._text;

        public override object? Value => this._value;

        public override string ValueText => Convert.ToString(this._value, CultureInfo.InvariantCulture) ?? string.Empty;

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T? value) : base(kind, text.Length)
        {
            this._text = text;
            this._value = value;
        }

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T? value, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, text.Length, diagnostics, annotations)
        {
            this._text = text;
            this._value = value;
        }

        internal SyntaxTokenWithValue(ObjectReader reader) : base(reader)
        {
            this._text = reader.ReadString();
            this.FullWidth = this._text.Length;
            this._value = (T?)reader.ReadValue();
        }

        internal override void WriteTo(ObjectWriter writer)
        {
            base.WriteTo(writer);
            writer.WriteString(this._text);
            writer.WriteValue(this._value);
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new SyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, trivia, null, this.GetDiagnostics(), this.GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new SyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, null, trivia, this.GetDiagnostics(), this.GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new SyntaxTokenWithValue<T>(this.Kind, this._text, this._value, diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
            new SyntaxTokenWithValue<T>(this.Kind, this._text, this._value, this.GetDiagnostics(), annotations);
    }
}
