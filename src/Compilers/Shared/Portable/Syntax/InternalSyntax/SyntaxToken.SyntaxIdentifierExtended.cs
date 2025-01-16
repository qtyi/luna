// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class SyntaxToken
{
    internal class SyntaxIdentifierExtended : SyntaxIdentifier
    {
        protected readonly SyntaxKind ContextualKindField;
        protected readonly string ValueTextField;

        /// <inheritdoc/>
        public override SyntaxKind ContextualKind => ContextualKindField;

        /// <inheritdoc/>
        public override object? Value => ValueTextField;

        /// <inheritdoc/>
        public override string ValueText => ValueTextField;

        internal SyntaxIdentifierExtended(SyntaxKind contextualKind, string text, string valueText) : base(text)
        {
            ContextualKindField = contextualKind;
            ValueTextField = valueText;
        }

        internal SyntaxIdentifierExtended(SyntaxKind contextualKind, string text, string valueText, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(text, diagnostics, annotations)
        {
            ContextualKindField = contextualKind;
            ValueTextField = valueText;
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia)
            => new SyntaxIdentifierWithTrivia(ContextualKindField, TextField, ValueTextField, trivia, trailing: null, GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia)
            => new SyntaxIdentifierWithTrivia(ContextualKindField, TextField, ValueTextField, leading: null, trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
            => new SyntaxIdentifierExtended(ContextualKindField, TextField, ValueTextField, diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
            => new SyntaxIdentifierExtended(ContextualKindField, TextField, ValueTextField, GetDiagnostics(), annotations);
    }
}
