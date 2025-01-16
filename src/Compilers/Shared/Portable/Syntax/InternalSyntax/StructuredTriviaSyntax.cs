// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal abstract partial class StructuredTriviaSyntax : ThisInternalSyntaxNode
{
    internal StructuredTriviaSyntax(SyntaxKind kind, DiagnosticInfo[]? diagnostics = null, SyntaxAnnotation[]? annotations = null) : base(kind, diagnostics, annotations) => Initialize();

    private void Initialize()
    {
        SetFlags(NodeFlags.ContainsStructuredTrivia);
    }
}
