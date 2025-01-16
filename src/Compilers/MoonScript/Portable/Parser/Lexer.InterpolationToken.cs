// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    private sealed class InterpolationToken : SyntaxToken.SyntaxTokenWithValueAndTrivia<Interpolation>
    {
        internal InterpolationToken(string text, in Interpolation value, GreenNode? leading, GreenNode? trailing) : base(SyntaxKind.InterpolationToken, text, value, leading, trailing) { }

        internal InterpolationToken(string text, in Interpolation value, GreenNode? leading, GreenNode? trailing, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(SyntaxKind.InterpolationToken, text, value, leading, trailing, diagnostics, annotations) { }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new InterpolationToken(TextField, ValueField, trivia, GetTrailingTrivia(), GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new InterpolationToken(TextField, ValueField, GetLeadingTrivia(), trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new InterpolationToken(TextField, ValueField, GetLeadingTrivia(), GetTrailingTrivia(), diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
             new InterpolationToken(TextField, ValueField, GetLeadingTrivia(), GetTrailingTrivia(), GetDiagnostics(), annotations);
    }
}
