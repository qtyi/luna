// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    private partial void ScanMultiLineRawStringLiteral(ref TokenInfo info, int level)
    {
        ScanLongBrackets(out var isTerminal, level);

        info.Kind = SyntaxKind.MultiLineRawStringLiteralToken;
        info.ValueKind = TokenValueType.String;
        info.Text = TextWindow.GetText(intern: true);
        info.StringValue = TextWindow.Intern(_builder);

        CheckFeatureAvaliability(MessageID.IDS_FeatureMultiLineRawStringLiteral);
        if (!isTerminal)
            AddError(ErrorCode.ERR_UnterminatedStringLiteral);
    }

    private partial bool IgnoreNewLineDirectlyAfterOpenBrackets() => true;
}
