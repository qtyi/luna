// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class SyntaxToken
{
    internal class IndentedSyntaxTokenWithValueAndTrivia<T> : IndentedSyntaxTokenWithValue<T>
        where T : notnull
    {
        private readonly GreenNode? _leading;
        private readonly GreenNode? _trailing;

        internal IndentedSyntaxTokenWithValueAndTrivia(
            SyntaxKind kind,
            string text,
            T value,
            int innerIndent,
            GreenNode? leading,
            GreenNode? trailing
        ) : base(kind, text, value, innerIndent)
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

        internal IndentedSyntaxTokenWithValueAndTrivia(
            SyntaxKind kind,
            string text,
            T value,
            int innerIndent,
            GreenNode? leading,
            GreenNode? trailing,
            DiagnosticInfo[]? diagnostics,
            SyntaxAnnotation[]? annotations
        ) : base(kind, text, value, innerIndent, diagnostics, annotations)
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

        public override GreenNode? GetLeadingTrivia() => _leading;

        public override GreenNode? GetTrailingTrivia() => _trailing;

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia)
            => new IndentedSyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, InnerIndent, trivia, _trailing, GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia)
            => new IndentedSyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, InnerIndent, _leading, trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
            => new IndentedSyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, InnerIndent, _leading, _trailing, diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
            => new IndentedSyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, InnerIndent, _leading, _trailing, GetDiagnostics(), annotations);
    }
}
