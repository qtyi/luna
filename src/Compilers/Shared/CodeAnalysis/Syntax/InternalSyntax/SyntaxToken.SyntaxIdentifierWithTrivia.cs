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
    internal class SyntaxIdentifierWithTrivia : SyntaxIdentifier
    {
        static SyntaxIdentifierWithTrivia() => ObjectBinder.RegisterTypeReader(typeof(SyntaxIdentifierWithTrivia), r => new SyntaxIdentifierWithTrivia(r));

        protected readonly SyntaxKind _contextualKind;
        protected readonly string _valueText;

        private readonly GreenNode? _leading;
        private readonly GreenNode? _trailing;

        public sealed override GreenNode? GetLeadingTrivia() => this._leading;
        public sealed override GreenNode? GetTrailingTrivia() => this._trailing;

        public override SyntaxKind ContextualKind => this._contextualKind;

        public override string ValueText => this._valueText;

        public override object? Value => this._valueText;

        internal SyntaxIdentifierWithTrivia(
            SyntaxKind contextualKind,
            string text,
            string valueText,
            GreenNode? leading,
            GreenNode? trailing
        ) : base(text)
        {
            this._contextualKind = contextualKind;
            this._valueText = valueText;

            SyntaxToken.InitializeWithTrivia(this, ref this._leading, ref this._trailing,
                leading, trailing
            );
        }

        internal SyntaxIdentifierWithTrivia(
            SyntaxKind contextualKind,
            string text,
            string valueText,
            GreenNode? leading,
            GreenNode? trailing,
            DiagnosticInfo[]? diagnostics,
            SyntaxAnnotation[]? annotations
        ) : base(text, diagnostics, annotations)
        {
            this._contextualKind = contextualKind;
            this._valueText = valueText;

            SyntaxToken.InitializeWithTrivia(this, ref this._leading, ref this._trailing,
                leading, trailing
            );
        }

        internal SyntaxIdentifierWithTrivia(ObjectReader reader) : base(reader)
        {
            this._contextualKind = (SyntaxKind)reader.ReadInt16();
            this._valueText = reader.ReadString();

            SyntaxToken.InitializeWithTrivia(this, ref this._leading, ref this._trailing,
                (GreenNode?)reader.ReadValue(),
                (GreenNode?)reader.ReadValue()
            );
        }

        internal override void WriteTo(ObjectWriter writer)
        {
            base.WriteTo(writer);
            writer.WriteValue(this._leading);
            writer.WriteValue(this._trailing);
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new SyntaxIdentifierWithTrivia(this._contextualKind, this._text, this._valueText, trivia, this.GetTrailingTrivia(), this.GetDiagnostics(), this.GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new SyntaxIdentifierWithTrivia(this._contextualKind, this._text, this._valueText, this.GetLeadingTrivia(), trivia, this.GetDiagnostics(), this.GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new SyntaxIdentifierWithTrivia(this._contextualKind, this._text, this._valueText, this.GetLeadingTrivia(), this.GetTrailingTrivia(), diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
            new SyntaxIdentifierWithTrivia(this._contextualKind, this._text, this._valueText, this.GetLeadingTrivia(), this.GetTrailingTrivia(), this.GetDiagnostics(), annotations);
    }
}
