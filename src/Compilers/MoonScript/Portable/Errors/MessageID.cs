﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript;

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
    IDS_MOONCHelp,

    IDS_DirectoryDoesNotExist,
    IDS_DirectoryHasInvalidPath,

    IDS_FeatureHexadecimalFloatConstant,
    IDS_FeatureBinaryExponent,

    // 预览功能
    IDS_FeatureMultiLineRawStringLiteral,
    IDS_FeatureMultiLineComment,
    IDS_FeatureFloorDivisionAssignmentOperator,
    IDS_FeatureBitwiseAndAssignmentOperator,
    IDS_FeatureBitwiseOrAssignmentOperator,
    IDS_FeatureExponentiationAssignmentOperator,
    IDS_FeatureBitwiseLeftShiftAssignmentOperator,
    IDS_FeatureBitwiseRightShiftAssignmentOperator,
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

        return feature switch
        {
            // MoonScript预览版的特性
            MessageID.IDS_FeatureMultiLineRawStringLiteral or
            MessageID.IDS_FeatureMultiLineComment or
            MessageID.IDS_FeatureFloorDivisionAssignmentOperator or
            MessageID.IDS_FeatureBitwiseAndAssignmentOperator or
            MessageID.IDS_FeatureBitwiseOrAssignmentOperator or
            MessageID.IDS_FeatureExponentiationAssignmentOperator or
            MessageID.IDS_FeatureBitwiseLeftShiftAssignmentOperator or
            MessageID.IDS_FeatureBitwiseRightShiftAssignmentOperator => LanguageVersion.DotNet,

            // MoonScript 0.5的特性
            MessageID.IDS_FeatureHexadecimalFloatConstant or
            MessageID.IDS_FeatureBinaryExponent
                => LanguageVersion.MoonScript0_5,

            _ => throw ExceptionUtilities.UnexpectedValue(feature)
        };
    }
}
