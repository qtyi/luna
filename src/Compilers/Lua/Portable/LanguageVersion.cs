// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua;

/// <summary>
/// Specifies the Lua language version.
/// </summary>
public enum LanguageVersion
{
    /// <summary>
    /// Lua language version 1.1.
    /// </summary>
    Lua1_1 = 1,
    /// <summary>
    /// Lua language version 2.1.
    /// </summary>
    Lua2_1,
    /// <summary>
    /// Lua language version 2.2.
    /// </summary>
    Lua2_2,
    /// <summary>
    /// Lua language version 2.4.
    /// </summary>
    Lua2_4,
    /// <summary>
    /// Lua language version 2.5.
    /// </summary>
    Lua2_5,
    /// <summary>
    /// Lua language version 3.1.
    /// </summary>
    Lua3_1,
    /// <summary>
    /// Lua language version 3.2.
    /// </summary>
    Lua3_2,
    /// <summary>
    /// Lua language version 4.0.
    /// </summary>
    Lua4,
    /// <summary>
    /// Lua language version 5.0.
    /// </summary>
    Lua5,
    /// <summary>
    /// Lua language version 5.1.
    /// </summary>
    Lua5_1,
    /// <summary>
    /// Lua language version 5.2.
    /// </summary>
    Lua5_2,
    /// <summary>
    /// Lua language version 5.3.
    /// </summary>
    Lua5_3,
    /// <summary>
    /// Lua language version 5.4.
    /// </summary>
    Lua5_4,

    /// <summary>
    /// Preview of the next language version.
    /// </summary>
    Preview = DotNet - 1,

    /// <summary>
    /// The .NET compatible version of the language.
    /// </summary>
    DotNet = Latest - 1,

    /// <summary>
    /// The latest supported version of the language.
    /// </summary>
    Latest = int.MaxValue,
    /// <summary>
    /// The default language version, which is the latest supported version.
    /// </summary>
    Default = 0
}

internal static partial class LanguageVersionExtensionsInternal
{
    internal static partial bool IsValid(this LanguageVersion version) => version switch
    {
        LanguageVersion.Lua1_1 or
        LanguageVersion.Lua2_1 or
        LanguageVersion.Lua2_2 or
        LanguageVersion.Lua2_4 or
        LanguageVersion.Lua2_5 or
        LanguageVersion.Lua3_1 or
        LanguageVersion.Lua3_2 or
        LanguageVersion.Lua4 or
        LanguageVersion.Lua5 or
        LanguageVersion.Lua5_1 or
        LanguageVersion.Lua5_2 or
        LanguageVersion.Lua5_3 or
        LanguageVersion.Lua5_4 or
        LanguageVersion.Preview or
        LanguageVersion.DotNet => true,

        _ => false
    };

    internal static partial ErrorCode GetErrorCode(this LanguageVersion version) => version switch
    {
        LanguageVersion.Lua1_1 => ErrorCode.ERR_FeatureNotAvailableInVersion1_1,
        LanguageVersion.Lua2_1 => ErrorCode.ERR_FeatureNotAvailableInVersion2_1,
        LanguageVersion.Lua2_2 => ErrorCode.ERR_FeatureNotAvailableInVersion2_2,
        LanguageVersion.Lua2_4 => ErrorCode.ERR_FeatureNotAvailableInVersion2_4,
        LanguageVersion.Lua2_5 => ErrorCode.ERR_FeatureNotAvailableInVersion2_5,
        LanguageVersion.Lua3_1 => ErrorCode.ERR_FeatureNotAvailableInVersion3_1,
        LanguageVersion.Lua3_2 => ErrorCode.ERR_FeatureNotAvailableInVersion3_2,
        LanguageVersion.Lua4 => ErrorCode.ERR_FeatureNotAvailableInVersion4,
        LanguageVersion.Lua5 => ErrorCode.ERR_FeatureNotAvailableInVersion5,
        LanguageVersion.Lua5_1 => ErrorCode.ERR_FeatureNotAvailableInVersion5_1,
        LanguageVersion.Lua5_2 => ErrorCode.ERR_FeatureNotAvailableInVersion5_2,
        LanguageVersion.Lua5_3 => ErrorCode.ERR_FeatureNotAvailableInVersion5_3,
        LanguageVersion.Lua5_4 => ErrorCode.ERR_FeatureNotAvailableInVersion5_4,
        LanguageVersion.Preview => ErrorCode.ERR_FeatureNotAvailableInPreview,
        LanguageVersion.DotNet => ErrorCode.ERR_FeatureNotAvailableInVersionDotNet,

        _ => throw ExceptionUtilities.UnexpectedValue(version)
    };
}

public static partial class LanguageVersionFacts
{
    /// <summary>
    /// Gets current language version of Lua.
    /// </summary>
    /// <value>
    /// Enum value that represents the current language version of Lua.
    /// </value>
    internal static partial LanguageVersion CurrentVersion => LanguageVersion.Lua5_4;

    /// <summary>
    /// Enum value that represents the next language version of Lua.
    /// </summary>
    internal const LanguageVersion LuaNext = LanguageVersion.Preview;

    public static partial string ToDisplayString(this LanguageVersion version) => version switch
    {
        LanguageVersion.Lua1_1 => "1.1",
        LanguageVersion.Lua2_1 => "2.1",
        LanguageVersion.Lua2_2 => "2.2",
        LanguageVersion.Lua2_4 => "2.4",
        LanguageVersion.Lua2_5 => "2.5",
        LanguageVersion.Lua3_1 => "3.1",
        LanguageVersion.Lua3_2 => "3.2",
        LanguageVersion.Lua4 => "4.0",
        LanguageVersion.Lua5 => "5.0",
        LanguageVersion.Lua5_1 => "5.1",
        LanguageVersion.Lua5_2 => "5.2",
        LanguageVersion.Lua5_3 => "5.3",
        LanguageVersion.Lua5_4 => "5.4",
        LanguageVersion.Preview => "preview",
        LanguageVersion.DotNet => "dotnet",
        LanguageVersion.Latest => "latest",
        LanguageVersion.Default => "default",

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

            case "1.1":
                result = LanguageVersion.Lua1_1;
                return true;

            case "2.1":
                result = LanguageVersion.Lua2_1;
                return true;

            case "2.2":
                result = LanguageVersion.Lua2_2;
                return true;

            case "2.4":
                result = LanguageVersion.Lua2_4;
                return true;

            case "2.5":
                result = LanguageVersion.Lua2_5;
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

            case "preview":
                result = LanguageVersion.Preview;
                return true;

            case "dotnet":
                result = LanguageVersion.DotNet;
                return true;

            default:
                result = LanguageVersion.Default;
                return false;
        }
    }
}
