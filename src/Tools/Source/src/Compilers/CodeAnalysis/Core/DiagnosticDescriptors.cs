// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Utilities;

namespace Luna.Tools;

internal static partial class DiagnosticDescriptors
{
    private enum CodeAnalysisSourceGeneratorsErrorCode
    {
        FileCannotRead,
        XmlException,
        UncategorizableErrorCodeField
    }

    private static string GetDiagnosticId(this CodeAnalysisSourceGeneratorsErrorCode errorCode)
    {
        switch (errorCode)
        {
            case CodeAnalysisSourceGeneratorsErrorCode.FileCannotRead:
            case CodeAnalysisSourceGeneratorsErrorCode.XmlException:
            case CodeAnalysisSourceGeneratorsErrorCode.UncategorizableErrorCodeField:
                return "CASG" + ((int)errorCode).ToString("D2");

            default:
                throw ExceptionUtilities.UnexpectedValue(errorCode);
        }
    }
}
