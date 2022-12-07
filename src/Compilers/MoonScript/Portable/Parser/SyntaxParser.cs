// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class SyntaxParser
{
    protected virtual partial SyntaxDiagnosticInfo GetExpectedTokenError(SyntaxKind expected, SyntaxKind actual, int offset, int width)
    {
        var code = SyntaxParser.GetExpectedTokenErrorCode(expected, actual);
        return code switch
        {
            ErrorCode.ERR_SyntaxError =>
                new(offset, width, code, SyntaxFacts.GetText(expected)),
#warning 需完善错误码到语法诊断信息实例的映射。
            _ =>
            new(offset, width, code)
        };
    }

    /// <summary>
    /// 获取一个错误码，对应未得到期望的标志。
    /// </summary>
    /// <returns>对应未得到期望的标志的错误码。</returns>
    /// <inheritdoc cref="SyntaxParser.GetExpectedTokenError(SyntaxKind, SyntaxKind)"/>
    private static ErrorCode GetExpectedTokenErrorCode(SyntaxKind expected, SyntaxKind actual) =>
        expected switch
        {
#warning 需完善未得到期望的标志对应的错误码。
            _ => ErrorCode.ERR_SyntaxError
        };

#warning Need code review.
    protected partial TNode CheckFeatureAvailability<TNode>(TNode node, MessageID feature, bool forceWarning = false)
        where TNode : GreenNode
    {
        LanguageVersion avaliableVersion = this.Options.LanguageVersion;

        switch (feature)
        {
            default:
                break;
        };

        var info = feature.GetFeatureAvailabilityDiagnosticInfo(this.Options);
        if (info is null)
            return node;

        if (forceWarning)
            return this.AddError(node, ErrorCode.WRN_ErrorOverride, info, (int)info.Code);
        else
            return this.AddError(node, info.Code, info.Arguments);
    }
}
