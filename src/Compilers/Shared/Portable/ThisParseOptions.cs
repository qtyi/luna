﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// Presents parse options.
/// </summary>
public sealed partial class
#if LANG_LUA
    LuaParseOptions
#elif LANG_MOONSCRIPT
    MoonScriptParseOptions
#endif
    : ParseOptions, IEquatable<ThisParseOptions>
{
    /// <summary>
    /// 默认解析选项。
    /// </summary>
    public static ThisParseOptions Default { get; } = new();

    private ImmutableDictionary<string, string> _features;

    public override IReadOnlyDictionary<string, string> Features => _features;

    /// <summary>
    /// 获取源代码的语言名称。
    /// </summary>
    public override string Language =>
#if LANG_LUA
        LanguageNames.Lua
#elif LANG_MOONSCRIPT
        LanguageNames.MoonScript
#endif
        ;

    /// <summary>
    /// 获取有效的语言版本，编译器将依据版本选择应用程序的语言规范。
    /// </summary>
    public LanguageVersion LanguageVersion { get; init; }

    /// <summary>
    /// 获取特定的语言版本，此属性的值在创建<see cref="ThisParseOptions"/>的新实例时传入构造函数，或使用<see cref="WithLanguageVersion"/>方法设置。
    /// </summary>
    public LanguageVersion SpecifiedLanguageVersion { get; init; }

    public
#if LANG_LUA
        LuaParseOptions
#elif LANG_MOONSCRIPT
        MoonScriptParseOptions
#endif
        (
        LanguageVersion languageVersion = LanguageVersion.Default,
        DocumentationMode documentationMode = DocumentationMode.Parse,
        SourceCodeKind kind = SourceCodeKind.Regular)
            : this(languageVersion, documentationMode, kind, ImmutableDictionary<string, string>.Empty)
    { }

    internal
#if LANG_LUA
        LuaParseOptions
#elif LANG_MOONSCRIPT
        MoonScriptParseOptions
#endif
        (
        LanguageVersion languageVersion,
        DocumentationMode documentationMode,
        SourceCodeKind kind,
        IReadOnlyDictionary<string, string>? features) : base(kind, documentationMode)
    {
        SpecifiedLanguageVersion = languageVersion;
        LanguageVersion = languageVersion.MapSpecifiedToEffectiveVersion();
        _features = features?.ToImmutableDictionary() ?? ImmutableDictionary<string, string>.Empty;
    }

    private
#if LANG_LUA
        LuaParseOptions
#elif LANG_MOONSCRIPT
        MoonScriptParseOptions
#endif
        (ThisParseOptions other)
        : this(
        languageVersion: other.SpecifiedLanguageVersion,
        documentationMode: other.DocumentationMode,
        kind: other.Kind,
        features: other.Features)
    { }

    public new ThisParseOptions WithKind(SourceCodeKind kind)
    {
        if (kind == SpecifiedKind) return this;

        var effectiveKind = kind.MapSpecifiedToEffectiveKind();
        return new(this)
        {
            SpecifiedKind = kind,
            Kind = effectiveKind
        };
    }

    public ThisParseOptions WithLanguageVersion(LanguageVersion version)
    {
        if (version == SpecifiedLanguageVersion) return this;

        var effectiveLanguageVersion = version.MapSpecifiedToEffectiveVersion();
        return new(this)
        {
            SpecifiedLanguageVersion = version,
            LanguageVersion = effectiveLanguageVersion
        };
    }

    public new ThisParseOptions WithDocumentationMode(DocumentationMode documentationMode)
    {
        if (documentationMode == DocumentationMode) return this;

        return new(this)
        {
            DocumentationMode = documentationMode
        };
    }

    public new ThisParseOptions WithFeatures(IEnumerable<KeyValuePair<string, string>>? features)
    {
        var dictionary = features?.ToImmutableDictionary(StringComparer.OrdinalIgnoreCase) ?? ImmutableDictionary<string, string>.Empty;

        return new(this)
        {
            _features = dictionary
        };
    }

    internal override void ValidateOptions(ArrayBuilder<Diagnostic> builder)
    {
        ValidateOptions(builder, ThisMessageProvider.Instance);

        // 验证当Latest/Default被转换后，LanguageVersion不是SpecifiedLanguageVersion。
        if (!LanguageVersion.IsValid())
            builder.Add(Diagnostic.Create(ThisMessageProvider.Instance, (int)ErrorCode.ERR_BadLanguageVersion, LanguageVersion.ToString()));
    }

    internal bool IsFeatureEnabled(MessageID feature)
    {
        var featureFlag = feature.RequiredFeature();
        if (featureFlag is not null) return Features.ContainsKey(featureFlag);

        var avaliableVersion = LanguageVersion;
        var requiredVersion = feature.RequiredVersion();
        return avaliableVersion >= requiredVersion;
    }

    #region ParseOptions
    public sealed override Microsoft.CodeAnalysis.ParseOptions CommonWithKind(SourceCodeKind kind) => WithKind(kind);

    protected sealed override Microsoft.CodeAnalysis.ParseOptions CommonWithDocumentationMode(DocumentationMode documentationMode) => WithDocumentationMode(documentationMode);

    protected sealed override Microsoft.CodeAnalysis.ParseOptions CommonWithFeatures(IEnumerable<KeyValuePair<string, string>> features) => WithFeatures(features);
    #endregion

    public sealed override bool Equals(object? obj) => Equals(obj as ThisParseOptions);
    public bool Equals(ThisParseOptions? other)
    {
        if (ReferenceEquals(this, other)) return true;
        else if (!EqualsHelper(other)) return false;
        else return SpecifiedLanguageVersion == other.SpecifiedLanguageVersion;
    }

    public override int GetHashCode() =>
        Hash.Combine(GetHashCodeHelper(),
            Hash.Combine((int)SpecifiedLanguageVersion, 0));
}
