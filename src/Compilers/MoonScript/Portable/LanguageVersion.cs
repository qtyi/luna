// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript;

/// <summary>
/// 枚举MoonScript语言的所有版本。
/// </summary>
public enum LanguageVersion
{
    /* 由于MoonScript尚未有正式发行版本，因此没有版本号。 */
    [Obsolete("未正式发行版本")]
    MoonScript0_5 = 1,

    /// <summary>
    /// 支持的最新的主要版本。
    /// </summary>
    LatestMajor = int.MaxValue - 2,
    /// <summary>
    /// 下一个预览版本。
    /// </summary>
    Preview = int.MaxValue - 1,
    /// <summary>
    /// 支持的最新的版本。
    /// </summary>
    Latest = int.MaxValue,
    /// <summary>
    /// 默认的语言版本，也就是支持的最新的版本。
    /// </summary>
    Default = 0
}

internal static partial class LanguageVersionExtensionsInternal
{
    internal static partial ErrorCode GetErrorCode(this LanguageVersion version) =>
        version switch
        {
            LanguageVersion.MoonScript0_5 => ErrorCode.ERR_FeatureNotAvailableInVersion0_5,
            _ => throw ExceptionUtilities.UnexpectedValue(version)
        };
}

public static partial class LanguageVersionFacts
{
    /// <summary>
    /// 获取MoonScript的下一个版本的<see cref="LanguageVersion"/>常量。
    /// </summary>
    internal const LanguageVersion MoonScriptNext = LanguageVersion.Preview;

    public static partial string ToDisplayString(this LanguageVersion version) =>
        version switch
        {
            LanguageVersion.MoonScript0_5 => "0.5",
            LanguageVersion.Default => "default",
            LanguageVersion.Latest => "latest",
            LanguageVersion.LatestMajor => "latestmajor",
            LanguageVersion.Preview => "preview",
            _ => throw ExceptionUtilities.UnexpectedValue(version)
        };

    public static partial bool TryParse(string? version, out LanguageVersion result)
    {
        if (version is null)
        {
            result = LanguageVersion.Default;
            return true;
        }

        switch (CaseInsensitiveComparison.ToLower(version))
        {
            case "default":
                result = LanguageVersion.Default;
                return true;
            case "latest":
                result = LanguageVersion.Latest;
                return true;
            case "latestmajor":
                result = LanguageVersion.LatestMajor;
                return true;
            case "preview":
                result = LanguageVersion.Preview;
                return true;

            case "0.5":
                result = LanguageVersion.MoonScript0_5;
                return true;

            default:
                result = LanguageVersion.Default;
                return false;
        }
    }

    public static partial LanguageVersion MapSpecifiedToEffectiveVersion(this LanguageVersion version) =>
        version switch
        {
            LanguageVersion.Latest or
            LanguageVersion.Default => LanguageVersion.MoonScript0_5,
            LanguageVersion.LatestMajor => LanguageVersion.Preview,
            _ => version
        };

    /// <summary>
    /// 获取MoonScript语言的当前版本。
    /// </summary>
    internal static LanguageVersion CurrentVersion => LanguageVersion.MoonScript0_5;
}
