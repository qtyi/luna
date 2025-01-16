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
    internal class SyntaxTokenWithValueAndTrivia<T> : SyntaxTokenWithValue<T>
        where T : notnull
    {
        private readonly GreenNode? _leading;
        private readonly GreenNode? _trailing;

        internal SyntaxTokenWithValueAndTrivia(
            SyntaxKind kind,
            string text,
            T value,
            GreenNode? leading,
            GreenNode? trailing
        ) : base(kind, text, value)
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

        internal SyntaxTokenWithValueAndTrivia(
            SyntaxKind kind,
            string text,
            T value,
            GreenNode? leading,
            GreenNode? trailing,
            DiagnosticInfo[]? diagnostics,
            SyntaxAnnotation[]? annotations
        ) : base(kind, text, value, diagnostics, annotations)
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
            => new SyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, trivia, _trailing, GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia)
            => new SyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, _leading, trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
            => new SyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, _leading, _trailing, diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
            => new SyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, _leading, _trailing, GetDiagnostics(), annotations);
    }
}
