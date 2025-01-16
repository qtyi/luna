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
    internal class SyntaxIdentifier : SyntaxToken
    {
        protected readonly string TextField;

        /// <inheritdoc/>
        public override string Text => TextField;

        /// <inheritdoc/>
        public override object? Value => TextField;

        /// <inheritdoc/>
        public override string ValueText => TextField;

        internal SyntaxIdentifier(string text) : base(SyntaxKind.IdentifierToken, text.Length)
        {
            TextField = text;
        }

        internal SyntaxIdentifier(string text, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(SyntaxKind.IdentifierToken, text.Length, diagnostics, annotations)
        {
            TextField = text;
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia)
            => new SyntaxIdentifierWithTrivia(Kind, TextField, TextField, trivia, trailing: null, GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia)
            => new SyntaxIdentifierWithTrivia(Kind, TextField, TextField, leading: null, trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
            => new SyntaxIdentifier(Text, diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
            => new SyntaxIdentifier(Text, GetDiagnostics(), annotations);
    }
}
