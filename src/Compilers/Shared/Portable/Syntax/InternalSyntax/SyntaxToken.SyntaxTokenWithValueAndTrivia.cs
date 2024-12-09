// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class SyntaxToken
{
    internal class SyntaxTokenWithValueAndTrivia<T> : SyntaxTokenWithValue<T>
    {
        static SyntaxTokenWithValueAndTrivia() => ObjectBinder.RegisterTypeReader(typeof(SyntaxTokenWithValueAndTrivia<T>), r => new SyntaxTokenWithValueAndTrivia<T>(r));

        protected readonly GreenNode? _leading;
        protected readonly GreenNode? _trailing;

        internal SyntaxTokenWithValueAndTrivia(
            SyntaxKind kind,
            string text,
            T? value,
            GreenNode? leading,
            GreenNode? trailing
        ) : base(kind, text, value)
        {
            if (leading is not null)
            {
                this.AdjustFlagsAndWidth(leading);
                this._leading = leading;
            }
            if (trailing is not null)
            {
                this.AdjustFlagsAndWidth(trailing);
                _trailing = trailing;
            }
        }

        internal SyntaxTokenWithValueAndTrivia(
            SyntaxKind kind,
            string text,
            T? value,
            GreenNode? leading,
            GreenNode? trailing,
            DiagnosticInfo[]? diagnostics,
            SyntaxAnnotation[]? annotations
        ) : base(kind, text, value, diagnostics, annotations)
        {
            if (leading is not null)
            {
                this.AdjustFlagsAndWidth(leading);
                this._leading = leading;
            }
            if (trailing is not null)
            {
                this.AdjustFlagsAndWidth(trailing);
                this._trailing = trailing;
            }
        }

        internal SyntaxTokenWithValueAndTrivia(ObjectReader reader) : base(reader)
        {
            var leading = (GreenNode?)reader.ReadValue();
            if (leading is not null)
            {
                this.AdjustFlagsAndWidth(leading);
                this._leading = leading;
            }
            var trailing = (GreenNode?)reader.ReadValue();
            if (trailing is not null)
            {
                this.AdjustFlagsAndWidth(trailing);
                this._trailing = trailing;
            }
        }

        internal override void WriteTo(ObjectWriter writer)
        {
            base.WriteTo(writer);
            writer.WriteValue(this._leading);
            writer.WriteValue(this._trailing);
        }

        public override GreenNode? GetLeadingTrivia() => this._leading;

        public override GreenNode? GetTrailingTrivia() => this._trailing;

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new SyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, this.GetLeadingTrivia(), this.GetTrailingTrivia(), diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
             new SyntaxTokenWithValueAndTrivia<T>(this.Kind, this._text, this._value, this.GetLeadingTrivia(), this.GetTrailingTrivia(), this.GetDiagnostics(), annotations);
    }
}
