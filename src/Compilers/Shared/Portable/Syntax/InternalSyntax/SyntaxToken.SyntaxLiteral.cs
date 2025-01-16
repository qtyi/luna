// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class SyntaxToken
{
    internal class SyntaxTokenWithValue<T> : SyntaxToken
        where T : notnull
    {
        protected readonly string TextField;
        protected readonly T ValueField;

        public override string Text => TextField;

        public override object? Value => ValueField;

        public override string ValueText => Convert.ToString(ValueField, CultureInfo.InvariantCulture)!;

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value) : base(kind, text.Length)
        {
            TextField = text;
            ValueField = value;
        }

        internal SyntaxTokenWithValue(SyntaxKind kind, string text, T value, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, text.Length, diagnostics, annotations)
        {
            TextField = text;
            ValueField = value;
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new SyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, trivia, GetTrailingTrivia(), GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new SyntaxTokenWithValueAndTrivia<T>(Kind, TextField, ValueField, GetLeadingTrivia(), trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new SyntaxTokenWithValue<T>(Kind, TextField, ValueField, diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
            new SyntaxTokenWithValue<T>(Kind, TextField, ValueField, GetDiagnostics(), annotations);
    }
}
