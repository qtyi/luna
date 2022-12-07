// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    private partial SyntaxTrivia ScanComment()
    {
        if (this.ScanLongBrackets(out bool isTerminal))
        {
            if (!isTerminal)
                this.AddError(ErrorCode.ERR_OpenEndedComment);
        }
        else
            this.ScanToEndOfLine(isTrim: true);

        var text = this.TextWindow.GetText(intern: false);
        return SyntaxFactory.Comment(text);
    }
}
