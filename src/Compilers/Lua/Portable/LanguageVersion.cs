// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;
using MSCA::Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua;

/// <summary>
/// 枚举Lua语言的所有版本。
/// </summary>
public enum LanguageVersion
{
    /// <summary>
    /// Lua语言版本1.0。
    /// </summary>
    Lua1 = 1,
    /// <summary>
    /// Lua语言版本1.1。
    /// </summary>
    Lua1_1,
    /// <summary>
    /// Lua语言版本2.1。
    /// </summary>
    Lua2_1,
    /// <summary>
    /// Lua语言版本2.2。
    /// </summary>
    Lua2_2,
    /// <summary>
    /// Lua语言版本2.3。
    /// </summary>
    Lua2_3,
    /// <summary>
    /// Lua语言版本2.4。
    /// </summary>
    Lua2_4,
    /// <summary>
    /// Lua语言版本2.5。
    /// </summary>
    Lua2_5,
    /// <summary>
    /// Lua语言版本3.0。
    /// </summary>
    Lua3,
    /// <summary>
    /// Lua语言版本3.1。
    /// </summary>
    Lua3_1,
    /// <summary>
    /// Lua语言版本3.2。
    /// </summary>
    Lua3_2,
    /// <summary>
    /// Lua语言版本4.0。
    /// </summary>
    Lua4,
    /// <summary>
    /// Lua语言版本5.0。
    /// </summary>
    Lua5,
    /// <summary>
    /// Lua语言版本5.1。
    /// </summary>
    Lua5_1,
    /// <summary>
    /// Lua语言版本5.2。
    /// </summary>
    Lua5_2,
    /// <summary>
    /// Lua语言版本5.3。
    /// </summary>
    Lua5_3,
    /// <summary>
    /// Lua语言版本5.4。
    /// </summary>
    Lua5_4,

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
            LanguageVersion.Lua1 => ErrorCode.ERR_FeatureNotAvailableInVersion1,
            LanguageVersion.Lua1_1 => ErrorCode.ERR_FeatureNotAvailableInVersion1_1,
            LanguageVersion.Lua2_1 => ErrorCode.ERR_FeatureNotAvailableInVersion2_1,
            LanguageVersion.Lua2_2 => ErrorCode.ERR_FeatureNotAvailableInVersion2_2,
            LanguageVersion.Lua2_3 => ErrorCode.ERR_FeatureNotAvailableInVersion2_3,
            LanguageVersion.Lua2_4 => ErrorCode.ERR_FeatureNotAvailableInVersion2_4,
            LanguageVersion.Lua2_5 => ErrorCode.ERR_FeatureNotAvailableInVersion2_5,
            LanguageVersion.Lua3 => ErrorCode.ERR_FeatureNotAvailableInVersion3,
            LanguageVersion.Lua3_1 => ErrorCode.ERR_FeatureNotAvailableInVersion3_1,
            LanguageVersion.Lua3_2 => ErrorCode.ERR_FeatureNotAvailableInVersion3_2,
            LanguageVersion.Lua4 => ErrorCode.ERR_FeatureNotAvailableInVersion4,
            LanguageVersion.Lua5 => ErrorCode.ERR_FeatureNotAvailableInVersion5,
            LanguageVersion.Lua5_1 => ErrorCode.ERR_FeatureNotAvailableInVersion5_1,
            LanguageVersion.Lua5_2 => ErrorCode.ERR_FeatureNotAvailableInVersion5_2,
            LanguageVersion.Lua5_3 => ErrorCode.ERR_FeatureNotAvailableInVersion5_3,
            LanguageVersion.Lua5_4 => ErrorCode.ERR_FeatureNotAvailableInVersion5_4,
            _ => throw ExceptionUtilities.UnexpectedValue(version)
        };
}

public static partial class LanguageVersionFacts
{
    /// <summary>
    /// 获取Lua的下一个版本的<see cref="LanguageVersion"/>常量。
    /// </summary>
    internal const LanguageVersion LuaNext = LanguageVersion.Preview;

    public static partial string ToDisplayString(this LanguageVersion version) =>
        version switch
        {
            LanguageVersion.Lua1 => "1.0",
            LanguageVersion.Lua1_1 => "1.1",
            LanguageVersion.Lua2_1 => "2.1",
            LanguageVersion.Lua2_2 => "2.2",
            LanguageVersion.Lua2_3 => "2.3",
            LanguageVersion.Lua2_4 => "2.4",
            LanguageVersion.Lua2_5 => "2.5",
            LanguageVersion.Lua3 => "3.0",
            LanguageVersion.Lua3_1 => "3.1",
            LanguageVersion.Lua3_2 => "3.2",
            LanguageVersion.Lua4 => "4.0",
            LanguageVersion.Lua5 => "5.0",
            LanguageVersion.Lua5_1 => "5.1",
            LanguageVersion.Lua5_2 => "5.2",
            LanguageVersion.Lua5_3 => "5.3",
            LanguageVersion.Lua5_4 => "5.4",
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

            case "1":
            case "1.0":
                result = LanguageVersion.Lua1;
                return true;

            case "1.1":
                result = LanguageVersion.Lua1_1;
                return true;

            case "2.1":
                result = LanguageVersion.Lua2_1;
                return true;

            case "2.2":
                result = LanguageVersion.Lua2_2;
                return true;

            case "2.3":
                result = LanguageVersion.Lua2_3;
                return true;

            case "2.4":
                result = LanguageVersion.Lua2_4;
                return true;

            case "2.5":
                result = LanguageVersion.Lua2_5;
                return true;

            case "3":
            case "3.0":
                result = LanguageVersion.Lua3;
                return true;

            case "3.1":
                result = LanguageVersion.Lua3_1;
                return true;

            case "3.2":
                result = LanguageVersion.Lua3_2;
                return true;

            case "4":
            case "4.0":
                result = LanguageVersion.Lua4;
                return true;

            case "5":
            case "5.0":
                result = LanguageVersion.Lua5;
                return true;

            case "5.1":
                result = LanguageVersion.Lua5_1;
                return true;

            case "5.2":
                result = LanguageVersion.Lua5_2;
                return true;

            case "5.3":
                result = LanguageVersion.Lua5_3;
                return true;

            case "5.4":
                result = LanguageVersion.Lua5_4;
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
            LanguageVersion.Default => LanguageVersion.Lua5_4,
            LanguageVersion.LatestMajor => LanguageVersion.Lua5,
            _ => version
        };

    /// <summary>
    /// 获取Lua语言的当前版本。
    /// </summary>
    internal static LanguageVersion CurrentVersion => LanguageVersion.Lua5_4;
}
