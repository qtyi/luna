// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.MoonScript;

/// <summary>
/// Specifies the MoonScript language version.
/// </summary>
public enum LanguageVersion
{
    /// <summary>
    /// MoonScript language version 0.1.
    /// </summary>
    MoonScript0_1 = 1,
    /// <summary>
    /// MoonScript language version 0.2.
    /// </summary>
    MoonScript0_2,
    /// <summary>
    /// MoonScript language version 0.3.
    /// </summary>
    MoonScript0_3,
    /// <summary>
    /// MoonScript language version 0.4.
    /// </summary>
    MoonScript0_4,
    /// <summary>
    /// MoonScript language version 0.5.
    /// </summary>
    MoonScript0_5,

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
        LanguageVersion.MoonScript0_1 or
        LanguageVersion.MoonScript0_2 or
        LanguageVersion.MoonScript0_3 or
        LanguageVersion.MoonScript0_4 or
        LanguageVersion.MoonScript0_5 or
        LanguageVersion.Preview or
        LanguageVersion.DotNet => true,

        _ => false
    };

    internal static partial ErrorCode GetErrorCode(this LanguageVersion version) => version switch
    {
        LanguageVersion.MoonScript0_1 => ErrorCode.ERR_FeatureNotAvailableInVersion0_1,
        LanguageVersion.MoonScript0_2 => ErrorCode.ERR_FeatureNotAvailableInVersion0_2,
        LanguageVersion.MoonScript0_3 => ErrorCode.ERR_FeatureNotAvailableInVersion0_3,
        LanguageVersion.MoonScript0_4 => ErrorCode.ERR_FeatureNotAvailableInVersion0_4,
        LanguageVersion.MoonScript0_5 => ErrorCode.ERR_FeatureNotAvailableInVersion0_5,
        LanguageVersion.Preview => ErrorCode.ERR_FeatureNotAvailableInPreview,
        LanguageVersion.DotNet => ErrorCode.ERR_FeatureNotAvailableInVersionDotNet,

        _ => throw ExceptionUtilities.UnexpectedValue(version)
    };
}

public static partial class LanguageVersionFacts
{
    /// <summary>
    /// Gets current language version of MoonScript.
    /// </summary>
    /// <value>
    /// Enum value that represents the current language version of MoonScript.
    /// </value>
    internal static partial LanguageVersion CurrentVersion => LanguageVersion.MoonScript0_5;

    /// <summary>
    /// Enum value that represents the next language version of MoonScript.
    /// </summary>
    internal const LanguageVersion MoonScriptNext = LanguageVersion.Preview;

    public static partial string ToDisplayString(this LanguageVersion version) => version switch
    {
        LanguageVersion.MoonScript0_1 => "0.1",
        LanguageVersion.MoonScript0_2 => "0.2",
        LanguageVersion.MoonScript0_3 => "0.3",
        LanguageVersion.MoonScript0_4 => "0.4",
        LanguageVersion.MoonScript0_5 => "0.5",
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

            case "0.1":
                result = LanguageVersion.MoonScript0_1;
                return true;

            case "0.2":
                result = LanguageVersion.MoonScript0_2;
                return true;

            case "0.3":
                result = LanguageVersion.MoonScript0_3;
                return true;

            case "0.4":
                result = LanguageVersion.MoonScript0_4;
                return true;

            case "0.5":
                result = LanguageVersion.MoonScript0_5;
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
