// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Diagnostics;
using MSCA::Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    private partial bool ScanMultiLineRawStringLiteral(ref TokenInfo info, int level)
    {
        if (this.ScanLongBrackets(out var isTerminal, level))
        {
            info.Kind = SyntaxKind.MultiLineRawStringLiteralToken;
            info.ValueKind = SpecialType.System_String;
            info.Text = this.TextWindow.GetText(intern: true);
            info.StringValue = this.TextWindow.Intern(this._builder);

            this.CheckFeatureAvaliability(MessageID.IDS_FeatureMultiLineRawStringLiteral);
            if (!isTerminal)
                this.AddError(ErrorCode.ERR_UnterminatedStringLiteral);

            return true;
        }

        return false;
    }
}
