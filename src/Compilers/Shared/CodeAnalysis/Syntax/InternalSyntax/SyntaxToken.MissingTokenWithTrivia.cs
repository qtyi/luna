// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class SyntaxToken
{
    internal class MissingTokenWithTrivia : SyntaxTokenWithTrivia
    {
        static MissingTokenWithTrivia() => ObjectBinder.RegisterTypeReader(typeof(MissingTokenWithTrivia), r => new MissingTokenWithTrivia(r));

        public sealed override string Text => string.Empty;

        internal MissingTokenWithTrivia(SyntaxKind kind, GreenNode? leading, GreenNode? trailing) : base(kind, leading, trailing)
        {
            this.ClearIsNotMissingFlag();
        }

        internal MissingTokenWithTrivia(SyntaxKind kind, GreenNode? leading, GreenNode? trailing, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, leading, trailing, diagnostics, annotations)
        {
            this.ClearIsNotMissingFlag();
        }

        internal MissingTokenWithTrivia(ObjectReader reader) : base(reader)
        {
            this.ClearIsNotMissingFlag();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ClearIsNotMissingFlag() => this.ClearFlags(NodeFlags.IsNotMissing);

        internal override void WriteTo(ObjectWriter writer)
        {
            base.WriteTo(writer);
            writer.WriteValue(this._leading);
            writer.WriteValue(this._trailing);
        }

        public override SyntaxToken TokenWithLeadingTrivia(GreenNode? trivia) =>
            new MissingTokenWithTrivia(this.Kind, trivia, this._trailing, this.GetDiagnostics(), this.GetAnnotations());

        public override SyntaxToken TokenWithTrailingTrivia(GreenNode? trivia) =>
            new MissingTokenWithTrivia(this.Kind, this._leading, trivia, this.GetDiagnostics(), this.GetAnnotations());

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) =>
            new MissingTokenWithTrivia(this.Kind, this.GetLeadingTrivia(), this.GetTrailingTrivia(), diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) =>
            new MissingTokenWithTrivia(this.Kind, this.GetLeadingTrivia(), this.GetTrailingTrivia(), this.GetDiagnostics(), annotations);
    }
}
