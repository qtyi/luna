// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisParseOptions = LuaParseOptions;
using ThisCompilation = LuaCompilation;
using ThisDiagnosticInfo = LuaDiagnosticInfo;
using ThisRequiredLanguageVersion = LuaRequiredLanguageVersion;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisParseOptions = MoonScriptParseOptions;
using ThisCompilation = MoonScriptCompilation;
using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
using ThisRequiredLanguageVersion = MoonScriptRequiredLanguageVersion;
#endif

/// <summary>
/// 为便于本地化文本，此类将<see cref="MessageID"/>包装为实现<see cref="IFormattable"/>的对象。
/// </summary>
internal partial struct LocalizableErrorArgument : IFormattable
{
    private readonly MessageID _id;

    internal LocalizableErrorArgument(MessageID id) => this._id = id;

    public override string ToString() => this.ToString(format: null, formatProvider: null);

    public string ToString(string? format, IFormatProvider? formatProvider) =>
        ErrorFacts.GetMessage(this._id, formatProvider as System.Globalization.CultureInfo);
}

/// <summary>
/// 为便于本地化<see cref="MessageID"/>的文本，在此类定义一系列的扩展方法。
/// </summary>
internal static partial class MessageIDExtensions
{
    /// <summary>
    /// 获取实现<see cref="IFormattable"/>的包装。
    /// </summary>
    /// <param name="id">要本地化的消息编号。</param>
    /// <returns>消息编号的一个实现<see cref="IFormattable"/>的包装</returns>
    public static LocalizableErrorArgument Localize(this MessageID id) => new(id);

    /// <summary>
    /// 返回通过/features开关开启相应<see cref="MessageID"/>特性的字符串表示。
    /// </summary>
    /// <remarks>
    /// <para>你应当先调用此方法，然后再调用<see cref="RequiredVersion(MessageID)"/>：</para>
    /// <para>    若此方法返回值为<see langword="null"/>时，调用<see cref="RequiredVersion(MessageID)"/>并使用其返回值。</para>
    /// <para>    若此方法返回值不为<see langword="null"/>时，使用返回值。</para>
    /// <para><see cref="RequiredFeature(MessageID)"/>和<see cref="RequiredVersion(MessageID)"/>之间应是互斥的。</para>
    /// </remarks>
    /// <param name="feature"></param>
    /// <returns><see cref="MessageID"/>特性的字符串表示。</returns>
    internal static partial string? RequiredFeature(this MessageID feature);

    internal static partial LanguageVersion RequiredVersion(this MessageID feature);

    internal static bool CheckFeatureAvailability(
        this MessageID feature,
        BindingDiagnosticBag diagnostics,
        SyntaxNode syntax,
        Location? location = null)
    {
        var diagnostic = GetFeatureAvailabilityDiagnosticInfo(feature, (ThisParseOptions)syntax.SyntaxTree.Options);
        if (diagnostic is null) return true;

        diagnostics.Add(diagnostic, location ?? syntax.GetLocation());
        return false;
    }

    internal static bool CheckFeatureAvailability(
        this MessageID feature,
        BindingDiagnosticBag diagnostics,
        Compilation compilation,
        Location location)
    {
        var diagnostic = GetFeatureAvailabilityDiagnosticInfo(feature, (ThisCompilation)compilation);
        if (diagnostic is null) return true;

        diagnostics.Add(diagnostic, location);
        return false;
    }

    internal static ThisDiagnosticInfo? GetFeatureAvailabilityDiagnosticInfo(this MessageID feature, ThisParseOptions options) =>
        options.IsFeatureEnabled(feature) ? null :
            GetDisabledFeatureDiagnosticInfo(feature, options.LanguageVersion);

    internal static ThisDiagnosticInfo? GetFeatureAvailabilityDiagnosticInfo(this MessageID feature, ThisCompilation compilation) =>
        compilation.IsFeatureEnabled(feature) ? null :
            GetDisabledFeatureDiagnosticInfo(feature, compilation.LanguageVersion);

    internal static ThisDiagnosticInfo GetDisabledFeatureDiagnosticInfo(MessageID feature, LanguageVersion availableVersion)
    {
        var requiredFeature = feature.RequiredFeature();
        if (requiredFeature is not null)
            return new(ErrorCode.ERR_FeatureIsExperimental, feature.Localize(), requiredFeature);

        var requiredVersion = feature.RequiredVersion();
        if (requiredVersion == LanguageVersion.Preview.MapSpecifiedToEffectiveVersion())
            return new(ErrorCode.ERR_FeatureInPreview, feature.Localize());
        else
            return new(availableVersion.GetErrorCode(), feature.Localize(), new ThisRequiredLanguageVersion(requiredVersion));
    }
}
