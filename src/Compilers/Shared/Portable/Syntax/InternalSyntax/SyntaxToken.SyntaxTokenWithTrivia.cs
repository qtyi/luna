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
    internal class SyntaxTokenWithTrivia : SyntaxToken
    {
        protected readonly GreenNode? LeadingField;
        protected readonly GreenNode? TrailingField;

        internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode? leading, GreenNode? trailing) : base(kind)
        {
            if (leading is not null)
            {
                AdjustFlagsAndWidth(leading);
                LeadingField = leading;
            }
            if (trailing is not null)
            {
                AdjustFlagsAndWidth(trailing);
                TrailingField = trailing;
            }
        }

        internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode? leading, GreenNode? trailing, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
        {
            if (leading is not null)
            {
                AdjustFlagsAndWidth(leading);
                LeadingField = leading;
            }
            if (trailing is not null)
            {
                AdjustFlagsAndWidth(trailing);
                TrailingField = trailing;
            }
        }

        public sealed override GreenNode? GetLeadingTrivia() => LeadingField;

        public sealed override GreenNode? GetTrailingTrivia() => TrailingField;

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia)
            => new SyntaxTokenWithTrivia(Kind, trivia, TrailingField, GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia)
            => new SyntaxTokenWithTrivia(Kind, LeadingField, trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
            => new SyntaxTokenWithTrivia(Kind, GetLeadingTrivia(), GetTrailingTrivia(), diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
            => new SyntaxTokenWithTrivia(Kind, GetLeadingTrivia(), GetTrailingTrivia(), GetDiagnostics(), annotations);
    }
}
