// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    private partial bool ScanMultiLineRawStringLiteral(ref TokenInfo info, int level)
    {
        if (ScanLongBrackets(out var isTerminal, level))
        {
            info.Kind = SyntaxKind.MultiLineRawStringLiteralToken;
            info.Text = TextWindow.GetText(intern: true);
            FlushToUtf8Builder();
            info.Utf8StringValue = new(_utf8Builder.ToArrayAndFree());

            if (!isTerminal)
                AddError(ErrorCode.ERR_UnterminatedStringLiteral);

            return true;
        }

        return true;
    }
}
