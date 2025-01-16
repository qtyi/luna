// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// Provides internal helper methods for <see cref="LanguageVersion"/>.
/// </summary>
internal static partial class LanguageVersionExtensionsInternal
{
    /// <summary>
    /// Checks if a <see cref="LanguageVersion"/> value is effective version.
    /// </summary>
    /// <param name="version">Specified enum value to be checked.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="version"/> is effective version; otherwise, <see langword="false"/>.</returns>
    internal static partial bool IsValid(this LanguageVersion version);

    /// <summary>
    /// Gets "FeatureNotAvailableInVersionN" <see cref="ErrorCode"/> value that specified <see cref="LanguageVersion"/> value corresponding to.
    /// </summary>
    /// <param name="version">Specified enum value.</param>
    /// <returns>"FeatureNotAvailableInVersionN" <see cref="ErrorCode"/> value that <paramref name="version"/> corresponding to.</returns>
    internal static partial ErrorCode GetErrorCode(this LanguageVersion version);
}

/// <inheritdoc/>
internal sealed partial class
#if LANG_LUA
    LuaRequiredLanguageVersion
#elif LANG_MOONSCRIPT
    MoonScriptRequiredLanguageVersion
#endif
    : RequiredLanguageVersion
{
    /// <summary>
    /// Gets underlying language version.
    /// </summary>
    /// <value>
    /// Language version wrapped.
    /// </value>
    internal LanguageVersion Version { get; }

    /// <summary>
    /// Create a new instance of <see cref="ThisRequiredLanguageVersion"/> type.
    /// </summary>
    /// <param name="version">Specified language version enum value to wrap.</param>
    internal
#if LANG_LUA
        LuaRequiredLanguageVersion
#elif LANG_MOONSCRIPT
        MoonScriptRequiredLanguageVersion
#endif
        (LanguageVersion version) => Version = version;

    /// <inheritdoc/>
    public override string ToString() => Version.ToDisplayString();
}

/// <summary>
/// Provides helper methods for <see cref="LanguageVersion"/>.
/// </summary>
public static partial class LanguageVersionFacts
{
    internal static partial LanguageVersion CurrentVersion { get; }

    /// <summary>
    /// Displays the version number in the format expected on the command-line (/langver flag).
    /// For instance, "5"、"5.4"、"latest".
    /// </summary>
    /// <param name="version">Language version enum value to display.</param>
    /// <returns>Formatted version number string.</returns>
    public static partial string ToDisplayString(this LanguageVersion version);

    /// <summary>
    /// Try parse a <see cref="LanguageVersion"/> from a string input, returning <see cref="LanguageVersion.Default"/> if input is <see langword="null"/> or unrecognized.
    /// </summary>
    /// <param name="version">String input.</param>
    /// <param name="result">Parsed <see cref="LanguageVersion"/> enum value from <paramref name="version"/>; <see cref="LanguageVersion.Default"/> if <paramref name="version"/> is unrecognized.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="version"/> is recognized; otherwise, <see langword="false"/>.</returns>
    public static partial bool TryParse(string? version, out LanguageVersion result);

    /// <summary>
    /// Map a language version (such as <see cref="LanguageVersion.Default"/>, <see cref="LanguageVersion.Latest"/>) to an effective version.
    /// </summary>
    /// <param name="version">Language version enum value to map.</param>
    /// <returns>An effective <see cref="LanguageVersion"/> enum value mapped from <paramref name="version"/>.</returns>

    public static LanguageVersion MapSpecifiedToEffectiveVersion(this LanguageVersion version)
    {
        var enumType = typeof(LanguageVersion);
        if (!Enum.IsDefined(enumType, version))
            throw new ArgumentException(string.Format(LunaResources.ArgMustBeDefinedInEnum, nameof(version), enumType), nameof(version));

        return version switch
        {
            LanguageVersion.Latest or
            LanguageVersion.Default => CurrentVersion,

            _ => version
        };
    }
}
