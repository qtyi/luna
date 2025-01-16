// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class Lexer
{
    private partial SyntaxTrivia? LexEndOfLine()
    {
        var c1 = TextWindow.PeekChar();
        var c2 = TextWindow.PeekChar(1);
        if (SyntaxFacts.IsNewLine(c1, c2))
        {
            TextWindow.AdvanceChar(2);
            if (c1 == '\r' && c2 == '\n')
                return ThisInternalSyntaxFactory.CarriageReturnLineFeed;
            else
                return ThisInternalSyntaxFactory.EndOfLine(new string(new[] { c1, c2 }));
        }
        else if (SyntaxFacts.IsNewLine(c1))
        {
            TextWindow.AdvanceChar();
            if (c1 == '\n')
                return ThisInternalSyntaxFactory.LineFeed;
            else if (c1 == '\r')
                return ThisInternalSyntaxFactory.CarriageReturn;
            else
                return ThisInternalSyntaxFactory.EndOfLine(new string(new[] { c1 }));
        }

        return null;
    }
}
