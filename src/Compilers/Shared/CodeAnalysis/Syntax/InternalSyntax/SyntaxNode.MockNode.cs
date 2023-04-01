// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if DEBUG

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisSyntaxNode = Qtyi.CodeAnalysis.Lua.LuaSyntaxNode;
using ThisMockNode = Qtyi.CodeAnalysis.Lua.LuaSyntaxNode.MockNode;
using ThisInternalSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisSyntaxNode = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxNode;
using ThisMockNode = Qtyi.CodeAnalysis.MoonScript.MoonScriptSyntaxNode.MockNode;
using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
#endif

partial class
#if LANG_LUA
    LuaSyntaxNode
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxNode
#endif
{
    internal class MockNode : ThisInternalSyntaxNode
    {
        internal MockNode() : base(SyntaxKind.None) { }
        internal MockNode(int fullWidth) : base(SyntaxKind.None, fullWidth) { }
        internal MockNode(DiagnosticInfo[]? diagnostics) : base(SyntaxKind.None, diagnostics) { }
        internal MockNode(DiagnosticInfo[]? diagnostics, int fullWidth) : base(SyntaxKind.None, diagnostics, fullWidth) { }
        internal MockNode(DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(SyntaxKind.None, diagnostics, annotations) { }
        internal MockNode(DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations, int fullWidth) : base(SyntaxKind.None, diagnostics, annotations, fullWidth) { }

        public override TResult? Accept<TResult>(LuaSyntaxVisitor<TResult> visitor) where TResult : default => default;

        public override void Accept(LuaSyntaxVisitor visitor) { }

        internal override SyntaxNode CreateRed(SyntaxNode? parent, int position) => new ThisMockNode(this, parent as ThisSyntaxNode, position);

        internal override GreenNode? GetSlot(int index) => null;

        internal override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics) => new MockNode(diagnostics, this.GetAnnotations());

        internal override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations) => new MockNode(this.GetDiagnostics(), annotations);
    }
}

#endif
