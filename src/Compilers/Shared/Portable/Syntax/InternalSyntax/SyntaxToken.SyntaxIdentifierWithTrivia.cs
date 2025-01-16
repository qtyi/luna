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
    internal class SyntaxIdentifierWithTrivia : SyntaxIdentifierExtended
    {
        private readonly GreenNode? _leading;
        private readonly GreenNode? _trailing;

        public sealed override GreenNode? GetLeadingTrivia() => _leading;
        public sealed override GreenNode? GetTrailingTrivia() => _trailing;

        internal SyntaxIdentifierWithTrivia(
            SyntaxKind contextualKind,
            string text,
            string valueText,
            GreenNode? leading,
            GreenNode? trailing
        ) : base(contextualKind, text, valueText)
        {
            if (leading is not null)
            {
                AdjustFlagsAndWidth(leading);
                _leading = leading;
            }
            if (trailing is not null)
            {
                AdjustFlagsAndWidth(trailing);
                _trailing = trailing;
            }
        }

        internal SyntaxIdentifierWithTrivia(
            SyntaxKind contextualKind,
            string text,
            string valueText,
            GreenNode? leading,
            GreenNode? trailing,
            DiagnosticInfo[]? diagnostics,
            SyntaxAnnotation[]? annotations
        ) : base(contextualKind, text, valueText, diagnostics, annotations)
        {
            if (leading is not null)
            {
                AdjustFlagsAndWidth(leading);
                _leading = leading;
            }
            if (trailing is not null)
            {
                AdjustFlagsAndWidth(trailing);
                _trailing = trailing;
            }
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new SyntaxIdentifierWithTrivia(ContextualKindField, TextField, ValueTextField, trivia, _trailing, GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new SyntaxIdentifierWithTrivia(ContextualKindField, TextField, ValueTextField, _leading, trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new SyntaxIdentifierWithTrivia(ContextualKindField, TextField, ValueTextField, _leading, _trailing, diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
            new SyntaxIdentifierWithTrivia(ContextualKindField, TextField, ValueTextField, _leading, _trailing, GetDiagnostics(), annotations);
    }
}
