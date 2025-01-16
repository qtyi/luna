// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class SyntaxParser
{
    protected virtual partial SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual, int offset, int width)
    {
        var code = GetExpectedTokenErrorCode(expected, actual, Options);
        return code switch
        {
            ErrorCode.ERR_IdentifierExpectedKW =>
                new(offset, width, code, SyntaxFacts.GetText(actual)),
            ErrorCode.ERR_SyntaxError =>
                new(offset, width, code, SyntaxFacts.GetText(expected)),

            _ => new(offset, width, code)
        };
    }

    protected static partial ErrorCode GetExpectedTokenErrorCode(SyntaxKind expected, SyntaxKind actual, ThisParseOptions options) =>
        expected switch
        {
            SyntaxKind.IdentifierToken =>
                SyntaxFacts.IsReservedKeyword(expected, options.LanguageVersion) ? ErrorCode.ERR_IdentifierExpectedKW : ErrorCode.ERR_IdentifierExpected,
            SyntaxKind.CommaToken => ErrorCode.ERR_CommaExpected,
            SyntaxKind.SemicolonToken => ErrorCode.ERR_SemicolonExpected,
            SyntaxKind.IfKeyword =>
                actual == SyntaxKind.ElseIfKeyword ? ErrorCode.ERR_ElseIfCannotStartStatement : ErrorCode.ERR_SyntaxError,

            _ => ErrorCode.ERR_SyntaxError
        };

#warning Need code review.
    protected partial TNode CheckFeatureAvailability<TNode>(TNode node, MessageID feature, bool forceWarning)
        where TNode : GreenNode
    {
        var avaliableVersion = Options.LanguageVersion;

        switch (feature)
        {
            default:
                break;
        };

        var info = feature.GetFeatureAvailabilityDiagnosticInfo(Options);
        if (info is null)
            return node;

        if (forceWarning)
            return AddError(node, ErrorCode.WRN_ErrorOverride, info, (int)info.Code);
        else
            return AddError(node, info.Code, info.Arguments);
    }
}
