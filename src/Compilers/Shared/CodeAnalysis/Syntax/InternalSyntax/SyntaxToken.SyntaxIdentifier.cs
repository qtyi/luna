// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class SyntaxToken
{
    internal class SyntaxIdentifier : SyntaxToken
    {
        static SyntaxIdentifier() => ObjectBinder.RegisterTypeReader(typeof(SyntaxIdentifier), r => new SyntaxIdentifier(r));

        protected readonly string _text;

        public override string Text => this._text;

        public override object? Value => this._text;

        public override string ValueText => this._text;

        internal SyntaxIdentifier(string text) : base(SyntaxKind.IdentifierToken, text.Length) => this._text = text;

        internal SyntaxIdentifier(string text, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(SyntaxKind.IdentifierToken, text.Length, diagnostics) => this._text = text;

        internal SyntaxIdentifier(ObjectReader reader) : base(reader)
        {
            this._text = reader.ReadString();
            this.FullWidth = this._text.Length;
        }

        internal override void WriteTo(ObjectWriter writer)
        {
            base.WriteTo(writer);
            writer.WriteString(this._text);
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new SyntaxIdentifierWithTrivia(this.Kind, this._text, this._text, trivia, null, this.GetDiagnostics(), this.GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new SyntaxIdentifierWithTrivia(this.Kind, this._text, this._text, null, trivia, this.GetDiagnostics(), this.GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new SyntaxIdentifier(this.Text, diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
            new SyntaxIdentifier(this.Text, this.GetDiagnostics(), annotations);
    }
}
