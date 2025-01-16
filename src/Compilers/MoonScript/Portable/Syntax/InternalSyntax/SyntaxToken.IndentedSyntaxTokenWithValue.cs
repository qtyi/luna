// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class SyntaxToken
{
    internal class IndentedSyntaxTokenWithValue<T> : SyntaxTokenWithValue<T>
        where T : notnull
    {
        protected readonly int InnerIndent;

        internal IndentedSyntaxTokenWithValue(SyntaxKind kind, string text, T value, int innerIndent) : base(kind, text, value)
        {
            InnerIndent = innerIndent;
        }

        internal IndentedSyntaxTokenWithValue(SyntaxKind kind, string text, T value, int innerIndent, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, text, value, diagnostics, annotations)
        {
            InnerIndent = innerIndent;
        }

        public override bool TryGetInnerWhiteSpaceIndent(out int indent)
        {
            indent = InnerIndent;
            return true;
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia)
            => new IndentedSyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, InnerIndent, trivia, trailing: null, GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia)
            => new IndentedSyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, InnerIndent, leading: null, trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
            => new IndentedSyntaxTokenWithValue<T>(Kind, TextField, ValueField, InnerIndent, diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
            => new IndentedSyntaxTokenWithValue<T>(Kind, TextField, ValueField, InnerIndent, GetDiagnostics(), annotations);
    }
}
