// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua;

internal enum MessageID
{
    None = 0,
    MessageBase = 1200,

    IDS_Text,
    IDS_Number,
    IDS_PathList,
    IDS_LIB_ENV,
    IDS_LIB_OPTION,

    IDS_LangVersions,
    IDS_ToolName,
    IDS_LogoLine1,
    IDS_LogoLine2,
    IDS_LUACHelp,

    IDS_DirectoryDoesNotExist,
    IDS_DirectoryHasInvalidPath,

    IDS_FeatureHexadecimalFloatConstant,
    IDS_FeatureBinaryExponent
}

internal static partial class MessageIDExtensions
{
    internal static partial string? RequiredFeature(this MessageID feature) =>
        feature switch
        {
            _ => null
        };

    internal static partial LanguageVersion RequiredVersion(this MessageID feature)
    {
        Debug.Assert(RequiredFeature(feature) is null);

        // 在语言分析器中检查特性的支持版本。
        return feature switch
        {
            // Lua 5.2的特性
            MessageID.IDS_FeatureHexadecimalFloatConstant or
            MessageID.IDS_FeatureBinaryExponent
                => LanguageVersion.Lua5_2,

            _ => throw ExceptionUtilities.UnexpectedValue(feature)
        };
    }
}
