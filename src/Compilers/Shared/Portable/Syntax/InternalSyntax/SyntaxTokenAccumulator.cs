// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using System.Collections.Generic;
using ThisInternalSyntaxAccumulator = LuaSyntaxAccumulator<SyntaxToken>;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxAccumulator = MoonScriptSyntaxAccumulator<SyntaxToken>;
#endif

internal class SyntaxTokenAccumulator : ThisInternalSyntaxAccumulator
{
    public SyntaxTokenAccumulator(bool visitIntoStructuredTrivia = false) : base(visitIntoStructuredTrivia) { }

    public override IEnumerable<SyntaxToken> VisitToken(SyntaxToken token)
    {
        VisitList(token.LeadingTrivia);
        yield return token;
        VisitList(token.TrailingTrivia);
    }
}
