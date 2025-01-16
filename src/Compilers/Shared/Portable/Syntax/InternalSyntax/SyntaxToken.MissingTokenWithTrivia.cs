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
    internal class MissingTokenWithTrivia : SyntaxTokenWithTrivia
    {
        /// <inheritdoc/>
        public override string Text => string.Empty;

        /// <inheritdoc/>
        public override object? Value => Kind switch
        {
            SyntaxKind.IdentifierToken => string.Empty,
            _ => null
        };

        internal MissingTokenWithTrivia(SyntaxKind kind, GreenNode? leading, GreenNode? trailing) : base(kind, leading, trailing)
        {
            ClearFlags(NodeFlags.IsNotMissing);
        }

        internal MissingTokenWithTrivia(SyntaxKind kind, GreenNode? leading, GreenNode? trailing, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, leading, trailing, diagnostics, annotations)
        {
            ClearFlags(NodeFlags.IsNotMissing);
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia)
            => new MissingTokenWithTrivia(Kind, trivia, TrailingField, GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia)
            => new MissingTokenWithTrivia(Kind, LeadingField, trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
            => new MissingTokenWithTrivia(Kind, GetLeadingTrivia(), GetTrailingTrivia(), diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
            => new MissingTokenWithTrivia(Kind, GetLeadingTrivia(), GetTrailingTrivia(), GetDiagnostics(), annotations);
    }
}
