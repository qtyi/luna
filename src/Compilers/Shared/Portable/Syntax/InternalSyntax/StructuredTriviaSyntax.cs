// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
#endif

internal abstract partial class StructuredTriviaSyntax : ThisInternalSyntaxNode
{
    internal StructuredTriviaSyntax(SyntaxKind kind, DiagnosticInfo[]? diagnostics = null, SyntaxAnnotation[]? annotations = null) : base(kind, diagnostics, annotations) => this.Initialize();

    internal StructuredTriviaSyntax(ObjectReader reader) : base(reader) => this.Initialize();

    private void Initialize()
    {
        this.SetFlags(NodeFlags.ContainsStructuredTrivia);
    }
}
