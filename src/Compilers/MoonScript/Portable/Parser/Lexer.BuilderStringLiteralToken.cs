// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    private sealed class BuilderStringLiteralToken : SyntaxToken
    {
        private readonly string _text;
        private readonly ArrayBuilder<string?>? _builder;
        private string? _value;
        private int _innerIndent;

        private readonly GreenNode? _leading;
        private readonly GreenNode? _trailing;

        public override string Text => _text;

        [MemberNotNull(nameof(_value))]
        public override object? Value
        {
            get
            {
                Build();
                return _value;
            }
        }

        [MemberNotNull(nameof(_value))]
        public override string ValueText => Convert.ToString(Value, CultureInfo.InvariantCulture) ?? string.Empty;

        internal ArrayBuilder<string?>? Builder => _builder;

        internal int InnerIndent { get => _innerIndent; set => _innerIndent = value; }

        internal BuilderStringLiteralToken(
            SyntaxKind kind,
            string text,
            ArrayBuilder<string?> builder,
            int innerIndent,
            GreenNode? leading,
            GreenNode? trailing) : base(kind, text.Length)
        {
            _text = text;
            _builder = builder;
            _innerIndent = innerIndent;

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

        internal BuilderStringLiteralToken(
            SyntaxKind kind,
            string text,
            ArrayBuilder<string?> builder,
            int innerIndent,
            GreenNode? leading,
            GreenNode? trailing,
            DiagnosticInfo[]? diagnostics,
            SyntaxAnnotation[]? annotations) : base(kind, text.Length, diagnostics, annotations)
        {
            _text = text;
            _builder = builder;
            _innerIndent = innerIndent;

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

        public override bool TryGetInnerWhiteSpaceIndent(out int indent)
        {
            indent = _innerIndent;
            return true;
        }

        [MemberNotNull(nameof(_value))]
        internal void Build()
        {
            if (_value is not null) return;
            if (_builder is null)
            {
                _value ??= string.Empty;
                return;
            }

            TrimIndent(_builder, _innerIndent);

            _value = string.Concat(_builder.ToImmutableOrEmptyAndFree());
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new IndentedSyntaxTokenWithValueAndTrivia<string>(Kind, _text, _value, _innerIndent, trivia, GetTrailingTrivia(), GetDiagnostics(), GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new IndentedSyntaxTokenWithValueAndTrivia<string>(Kind, _text, _value, _innerIndent, GetLeadingTrivia(), trivia, GetDiagnostics(), GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new IndentedSyntaxTokenWithValueAndTrivia<string>(Kind, _text, _value, _innerIndent, GetLeadingTrivia(), GetTrailingTrivia(), diagnostics, GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
             new IndentedSyntaxTokenWithValueAndTrivia<string>(Kind, _text, _value, _innerIndent, GetLeadingTrivia(), GetTrailingTrivia(), GetDiagnostics(), annotations);
    }
}
