﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    private partial bool ScanMultiLineRawStringLiteral(ref TokenInfo info, int level)
    {
        if (this.ScanLongBrackets(out var isTerminal, level))
        {
            info.Kind = SyntaxKind.MultiLineRawStringLiteralToken;
            info.Text = this.TextWindow.GetText(intern: true);
            this.FlushToUtf8Builder();
            info.Utf8StringValue = this._utf8Builder.ToImmutableAndClear();

            if (!isTerminal)
                this.AddError(ErrorCode.ERR_UnterminatedStringLiteral);

            return true;
        }

        return true;
    }
}
