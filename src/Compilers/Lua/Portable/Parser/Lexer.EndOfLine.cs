// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = MoonScriptSyntaxNode;
#endif

partial class Lexer
{
    private partial ThisInternalSyntaxNode? ScanEndOfLine()
    {
        var c1 = this.TextWindow.PeekChar();
        var c2 = this.TextWindow.PeekChar(1);
        if (SyntaxFacts.IsNewLine(c1, c2))
        {
            this.TextWindow.AdvanceChar(2);
            if (c1 == '\r' && c2 == '\n')
                return SyntaxFactory.CarriageReturnLineFeed;
            else
                return SyntaxFactory.EndOfLine(new string(new[] { c1, c2 }));
        }
        else if (SyntaxFacts.IsNewLine(c1))
        {
            this.TextWindow.AdvanceChar();
            if (c1 == '\n')
                return SyntaxFactory.LineFeed;
            else if (c1 == '\r')
                return SyntaxFactory.CarriageReturn;
            else
                return SyntaxFactory.EndOfLine(new string(new[] { c1 }));
        }

        return null;
    }
}
