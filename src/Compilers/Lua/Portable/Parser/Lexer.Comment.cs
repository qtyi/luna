// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    private partial SyntaxTrivia LexComment()
    {
        if (ScanLongBrackets(out var isTerminal))
        {
            if (!isTerminal)
                AddError(ErrorCode.ERR_OpenEndedComment);
        }
        else
            ScanToEndOfLine(_builder, trimEnd: true);

        var text = TextWindow.GetText(intern: false);
        return ThisInternalSyntaxFactory.Comment(text);
    }
}
