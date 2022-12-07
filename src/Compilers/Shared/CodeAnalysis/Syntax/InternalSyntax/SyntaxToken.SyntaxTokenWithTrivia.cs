// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class SyntaxToken
{
    internal class SyntaxTokenWithTrivia : SyntaxToken
    {
        static SyntaxTokenWithTrivia() => ObjectBinder.RegisterTypeReader(typeof(SyntaxTokenWithTrivia), r => new SyntaxTokenWithTrivia(r));

        protected readonly GreenNode? _leading;
        protected readonly GreenNode? _trailing;

        internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode? leading, GreenNode? trailing) : base(kind) =>
            SyntaxToken.InitializeWithTrivia(
                this, ref this._leading, ref this._trailing,
                leading, trailing
            );

        internal SyntaxTokenWithTrivia(SyntaxKind kind, GreenNode? leading, GreenNode? trailing, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations) =>
            SyntaxToken.InitializeWithTrivia(
                this, ref this._leading, ref this._trailing,
                leading, trailing
            );

        internal SyntaxTokenWithTrivia(ObjectReader reader) : base(reader) =>
            SyntaxToken.InitializeWithTrivia(
                this, ref this._leading, ref this._trailing,
                (GreenNode?)reader.ReadValue(),
                (GreenNode?)reader.ReadValue()
            );

        internal override void WriteTo(ObjectWriter writer)
        {
            base.WriteTo(writer);
            writer.WriteValue(this._leading);
            writer.WriteValue(this._trailing);
        }

        public sealed override GreenNode? GetLeadingTrivia() => this._leading;

        public sealed override GreenNode? GetTrailingTrivia() => this._trailing;

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new SyntaxTokenWithTrivia(this.Kind, this.GetLeadingTrivia(), this.GetTrailingTrivia(), diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
            new SyntaxTokenWithTrivia(this.Kind, this.GetLeadingTrivia(), this.GetTrailingTrivia(), this.GetDiagnostics(), annotations);
    }
}
