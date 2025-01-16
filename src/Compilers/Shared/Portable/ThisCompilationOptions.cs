// Licensed to the Qtyi under one or more agreements.
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
#else
#error Not implemented
#endif

/// <summary>
/// Represents various options that affect compilation, such as whether to emit an executable or a library, whether to optimize generated code, and so on.
/// </summary>
public sealed partial class
#pragma warning disable CS0659
#if LANG_LUA
    LuaCompilationOptions
#elif LANG_MOONSCRIPT
    MoonScriptCompilationOptions
#endif
#pragma warning restore CS0659
    : CompilationOptions, IEquatable<ThisCompilationOptions>
{
    // Defaults correspond to the compiler's defaults or indicate that the user did not specify when that is significant.
    // That's significant when one option depends on another's setting.
    internal
#if LANG_LUA
    LuaCompilationOptions
#elif LANG_MOONSCRIPT
    MoonScriptCompilationOptions
#endif
    (
        OutputKind outputKind,
        bool reportSuppressedDiagnostics = false,
        string? moduleName = null,
        string? mainTypeName = null,
        string? scriptClassName = null,
        OptimizationLevel optimizationLevel = OptimizationLevel.Debug,
        bool checkOverflow = false,
        string? cryptoKeyContainer = null,
        string? cryptoKeyFile = null,
        ImmutableArray<byte> cryptoPublicKey = default,
        bool? delaySign = null,
        Platform platform = Platform.AnyCpu,
        ReportDiagnostic generalDiagnosticOption = ReportDiagnostic.Default,
        int warningLevel = Diagnostic.DefaultWarningLevel,
        IEnumerable<KeyValuePair<string, ReportDiagnostic>>? specificDiagnosticOptions = null,
        bool concurrentBuild = true,
        bool deterministic = false,
        XmlReferenceResolver? xmlReferenceResolver = null,
        SourceReferenceResolver? sourceReferenceResolver = null,
        MetadataReferenceResolver? metadataReferenceResolver = null,
        AssemblyIdentityComparer? assemblyIdentityComparer = null,
        StrongNameProvider? strongNameProvider = null,
        bool publicSign = false,
        MetadataImportOptions metadataImportOptions = MetadataImportOptions.Public,
        NullableContextOptions nullableContextOptions = NullableContextOptions.Disable
    ) : this(outputKind,
             reportSuppressedDiagnostics,
             moduleName,
             mainTypeName,
             scriptClassName,
             optimizationLevel,
             checkOverflow,
             cryptoKeyContainer,
             cryptoKeyFile,
             cryptoPublicKey,
             delaySign,
             platform,
             generalDiagnosticOption,
             warningLevel,
             specificDiagnosticOptions,
             concurrentBuild,
             deterministic,
             currentLocalTime: default,
             debugPlusMode: false,
             xmlReferenceResolver: xmlReferenceResolver,
             sourceReferenceResolver: sourceReferenceResolver,
             syntaxTreeOptionsProvider: null,
             metadataReferenceResolver: metadataReferenceResolver,
             assemblyIdentityComparer: assemblyIdentityComparer,
             strongNameProvider: strongNameProvider,
             metadataImportOptions: metadataImportOptions,
             referencesSupersedeLowerVersions: false,
             publicSign: publicSign,
             nullableContextOptions: nullableContextOptions
    )
    { }

    // Expects correct arguments.
    internal
#if LANG_LUA
    LuaCompilationOptions
#elif LANG_MOONSCRIPT
    MoonScriptCompilationOptions
#endif
    (
        OutputKind outputKind,
        bool reportSuppressedDiagnostics,
        string? moduleName,
        string? mainTypeName,
        string? scriptClassName,
        OptimizationLevel optimizationLevel,
        bool checkOverflow,
        string? cryptoKeyContainer,
        string? cryptoKeyFile,
        ImmutableArray<byte> cryptoPublicKey,
        bool? delaySign,
        Platform platform,
        ReportDiagnostic generalDiagnosticOption,
        int warningLevel,
        IEnumerable<KeyValuePair<string, ReportDiagnostic>>? specificDiagnosticOptions,
        bool concurrentBuild,
        bool deterministic,
        DateTime currentLocalTime,
        bool debugPlusMode,
        XmlReferenceResolver? xmlReferenceResolver,
        SourceReferenceResolver? sourceReferenceResolver,
        SyntaxTreeOptionsProvider? syntaxTreeOptionsProvider,
        MetadataReferenceResolver? metadataReferenceResolver,
        AssemblyIdentityComparer? assemblyIdentityComparer,
        StrongNameProvider? strongNameProvider,
        MetadataImportOptions metadataImportOptions,
        bool referencesSupersedeLowerVersions,
        bool publicSign,
        NullableContextOptions nullableContextOptions
    ) : base(outputKind,
             reportSuppressedDiagnostics,
             moduleName,
             mainTypeName,
             scriptClassName,
             cryptoKeyContainer,
             cryptoKeyFile,
             cryptoPublicKey,
             delaySign,
             publicSign,
             optimizationLevel,
             checkOverflow,
             platform,
             generalDiagnosticOption,
             warningLevel,
             specificDiagnosticOptions.ToImmutableDictionaryOrEmpty(),
             concurrentBuild,
             deterministic,
             currentLocalTime,
             debugPlusMode,
             xmlReferenceResolver,
             sourceReferenceResolver,
             syntaxTreeOptionsProvider,
             metadataReferenceResolver,
             assemblyIdentityComparer,
             strongNameProvider,
             metadataImportOptions,
             referencesSupersedeLowerVersions
    )
    {
        NullableContextOptions = nullableContextOptions;
    }

    private
#if LANG_LUA
    LuaCompilationOptions
#elif LANG_MOONSCRIPT
    MoonScriptCompilationOptions
#endif
    (ThisCompilationOptions other) : this(
        outputKind: other.OutputKind,
        moduleName: other.ModuleName,
        mainTypeName: other.MainTypeName,
        scriptClassName: other.ScriptClassName,
        optimizationLevel: other.OptimizationLevel,
        checkOverflow: other.CheckOverflow,
        cryptoKeyContainer: other.CryptoKeyContainer,
        cryptoKeyFile: other.CryptoKeyFile,
        cryptoPublicKey: other.CryptoPublicKey,
        delaySign: other.DelaySign,
        platform: other.Platform,
        generalDiagnosticOption: other.GeneralDiagnosticOption,
        warningLevel: other.WarningLevel,
        specificDiagnosticOptions: other.SpecificDiagnosticOptions,
        concurrentBuild: other.ConcurrentBuild,
        deterministic: other.Deterministic,
        currentLocalTime: other.CurrentLocalTime,
        debugPlusMode: other.DebugPlusMode,
        xmlReferenceResolver: other.XmlReferenceResolver,
        sourceReferenceResolver: other.SourceReferenceResolver,
        syntaxTreeOptionsProvider: other.SyntaxTreeOptionsProvider,
        metadataReferenceResolver: other.MetadataReferenceResolver,
        assemblyIdentityComparer: other.AssemblyIdentityComparer,
        strongNameProvider: other.StrongNameProvider,
        metadataImportOptions: other.MetadataImportOptions,
        referencesSupersedeLowerVersions: other.ReferencesSupersedeLowerVersions,
        reportSuppressedDiagnostics: other.ReportSuppressedDiagnostics,
        publicSign: other.PublicSign,
        nullableContextOptions: other.NullableContextOptions
    )
    { }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => Equals(obj as ThisCompilationOptions);

    /// <inheritdoc/>
    public partial bool Equals(ThisCompilationOptions? other);

    /// <inheritdoc/>
    protected override partial int ComputeHashCode();

    /// <inheritdoc/>
    internal override partial Diagnostic? FilterDiagnostic(Diagnostic diagnostic, CancellationToken cancellationToken);

    /// <inheritdoc/>
    internal override partial ImmutableArray<string> GetImports();

    /// <inheritdoc/>
    internal override partial void ValidateOptions(ArrayBuilder<Diagnostic> builder);

    /// <inheritdoc/>
    public override NullableContextOptions NullableContextOptions { get => NullableContextOptions.Enable; protected set { } }

    /// <inheritdoc/>
    internal override DeterministicKeyBuilder CreateDeterministicKeyBuilder() => ThisDeterministicKeyBuilder.Instance;

    #region Options
    public new ThisCompilationOptions WithOutputKind(OutputKind kind)
        => kind == OutputKind ? this : new(this) { OutputKind = kind };

    public new ThisCompilationOptions WithModuleName(string? moduleName)
        => moduleName == ModuleName ? this : new(this) { ModuleName = moduleName };

    public new ThisCompilationOptions WithMainTypeName(string? mainTypeName)
        => mainTypeName == MainTypeName ? this : new(this) { MainTypeName = mainTypeName };

    public new ThisCompilationOptions WithScriptClassName(string scriptClassName)
        => scriptClassName == ScriptClassName ? this : new(this) { ScriptClassName = scriptClassName };

    public new ThisCompilationOptions WithOptimizationLevel(OptimizationLevel value)
        => value == OptimizationLevel ? this : new(this) { OptimizationLevel = value };

    public new ThisCompilationOptions WithOverflowChecks(bool checkOverflow)
        => checkOverflow == CheckOverflow ? this : new(this) { CheckOverflow = checkOverflow };

    public new ThisCompilationOptions WithCryptoKeyContainer(string? cryptoKeyContainer)
        => cryptoKeyContainer == CryptoKeyContainer ? this : new(this) { CryptoKeyContainer = cryptoKeyContainer };

    public new ThisCompilationOptions WithCryptoKeyFile(string? cryptoKeyFile)
    {
        if (string.IsNullOrEmpty(cryptoKeyFile))
            cryptoKeyFile = null;

        return cryptoKeyFile == CryptoKeyFile ? this : new(this) { CryptoKeyFile = cryptoKeyFile };
    }

    public new ThisCompilationOptions WithCryptoPublicKey(ImmutableArray<byte> cryptoPublicKey)
    {
        if (cryptoPublicKey.IsDefault)
            cryptoPublicKey = [];

        return cryptoPublicKey == CryptoPublicKey ? this : new(this) { CryptoPublicKey = cryptoPublicKey };
    }

    public new ThisCompilationOptions WithDelaySign(bool? delaySign)
        => delaySign == DelaySign ? this : new(this) { DelaySign = delaySign };

    public new ThisCompilationOptions WithPlatform(Platform platform)
        => platform == Platform ? this : new(this) { Platform = platform };

    public new ThisCompilationOptions WithGeneralDiagnosticOption(ReportDiagnostic generalDiagnosticOption)
        => generalDiagnosticOption == GeneralDiagnosticOption ? this : new(this) { GeneralDiagnosticOption = generalDiagnosticOption };

    public ThisCompilationOptions WithWarningLevel(int warningLevel)
        => warningLevel == WarningLevel ? this : new(this) { WarningLevel = warningLevel };

    public new ThisCompilationOptions WithSpecificDiagnosticOptions(ImmutableDictionary<string, ReportDiagnostic>? specificDiagnosticOptions)
    {
        if (specificDiagnosticOptions is null)
            specificDiagnosticOptions = ImmutableDictionary<string, ReportDiagnostic>.Empty;

        return specificDiagnosticOptions == SpecificDiagnosticOptions ? this : new(this) { SpecificDiagnosticOptions = specificDiagnosticOptions };
    }

    public new ThisCompilationOptions WithSpecificDiagnosticOptions(IEnumerable<KeyValuePair<string, ReportDiagnostic>>? specificDiagnosticOptions)
        => new(this) { SpecificDiagnosticOptions = specificDiagnosticOptions.ToImmutableDictionaryOrEmpty() };

    public new ThisCompilationOptions WithConcurrentBuild(bool concurrent)
        => concurrent == ConcurrentBuild ? this : new(this) { ConcurrentBuild = concurrent };

    public new ThisCompilationOptions WithDeterministic(bool deterministic)
        => deterministic == Deterministic ? this : new(this) { Deterministic = deterministic };

    internal ThisCompilationOptions WithCurrentLocalTime(DateTime value)
        => value == CurrentLocalTime ? this : new(this) { CurrentLocalTime = value };

    internal ThisCompilationOptions WithDebugPlusMode(bool value)
        => value == DebugPlusMode ? this : new(this) { DebugPlusMode = value};

    public new ThisCompilationOptions WithXmlReferenceResolver(XmlReferenceResolver? resolver)
        => ReferenceEquals(resolver, XmlReferenceResolver) ? this : new(this) { XmlReferenceResolver = resolver };

    public new ThisCompilationOptions WithSourceReferenceResolver(SourceReferenceResolver? resolver)
        => ReferenceEquals(resolver, SourceReferenceResolver) ? this : new(this) { SourceReferenceResolver = resolver };

    public new ThisCompilationOptions WithSyntaxTreeOptionsProvider(SyntaxTreeOptionsProvider? resolver)
        => ReferenceEquals(resolver, SyntaxTreeOptionsProvider) ? this : new(this) { SyntaxTreeOptionsProvider = resolver };

    public new ThisCompilationOptions WithMetadataReferenceResolver(MetadataReferenceResolver? resolver)
        => ReferenceEquals(resolver, MetadataReferenceResolver) ? this : new(this) { MetadataReferenceResolver = resolver };

    public new ThisCompilationOptions WithAssemblyIdentityComparer(AssemblyIdentityComparer? comparer)
    {
        comparer ??= AssemblyIdentityComparer.Default;

        return ReferenceEquals(comparer, AssemblyIdentityComparer) ? this : new(this) { AssemblyIdentityComparer = comparer };
    }

    public new ThisCompilationOptions WithStrongNameProvider(StrongNameProvider? provider)
        => ReferenceEquals(provider, StrongNameProvider) ? this : new(this) { StrongNameProvider = provider };

    public new ThisCompilationOptions WithMetadataImportOptions(MetadataImportOptions value)
        => value == MetadataImportOptions ? this : new(this) { MetadataImportOptions = value };

    internal ThisCompilationOptions WithReferencesSupersedeLowerVersions(bool value)
        => value == ReferencesSupersedeLowerVersions ? this : new(this) { ReferencesSupersedeLowerVersions = value };

    public new ThisCompilationOptions WithReportSuppressedDiagnostics(bool reportSuppressedDiagnostics)
        => reportSuppressedDiagnostics == ReportSuppressedDiagnostics ? this : new(this) { ReportSuppressedDiagnostics = reportSuppressedDiagnostics };

    public new ThisCompilationOptions WithPublicSign(bool publicSign)
        => publicSign == PublicSign ? this : new(this) { PublicSign = publicSign };

    protected override CompilationOptions CommonWithOutputKind(OutputKind kind) => WithOutputKind(kind);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithModuleName(string? moduleName) => WithModuleName(moduleName);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithMainTypeName(string? mainTypeName) => WithMainTypeName(mainTypeName);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithScriptClassName(string scriptClassName) => WithScriptClassName(scriptClassName);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithOptimizationLevel(OptimizationLevel value) => WithOptimizationLevel(value);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithCheckOverflow(bool checkOverflow) => WithOverflowChecks(checkOverflow);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithCryptoKeyContainer(string? cryptoKeyContainer) => WithCryptoKeyContainer(cryptoKeyContainer);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithCryptoKeyFile(string? cryptoKeyFile) => WithCryptoKeyFile(cryptoKeyFile);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithCryptoPublicKey(ImmutableArray<byte> cryptoPublicKey) => WithCryptoPublicKey(cryptoPublicKey);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithDelaySign(bool? delaySign) => WithDelaySign(delaySign);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithPlatform(Platform platform) => WithPlatform(platform);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithGeneralDiagnosticOption(ReportDiagnostic generalDiagnosticOption) => WithGeneralDiagnosticOption(generalDiagnosticOption);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithSpecificDiagnosticOptions(ImmutableDictionary<string, ReportDiagnostic>? specificDiagnosticOptions) => WithSpecificDiagnosticOptions(specificDiagnosticOptions);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithSpecificDiagnosticOptions(IEnumerable<KeyValuePair<string, ReportDiagnostic>>? specificDiagnosticOptions) => WithSpecificDiagnosticOptions(specificDiagnosticOptions);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithConcurrentBuild(bool concurrent) => WithConcurrentBuild(concurrent);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithDeterministic(bool deterministic) => WithDeterministic(deterministic);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithXmlReferenceResolver(XmlReferenceResolver? resolver) => WithXmlReferenceResolver(resolver);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithSourceReferenceResolver(SourceReferenceResolver? resolver) => WithSourceReferenceResolver(resolver);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithSyntaxTreeOptionsProvider(SyntaxTreeOptionsProvider? resolver) => WithSyntaxTreeOptionsProvider(resolver);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithMetadataReferenceResolver(MetadataReferenceResolver? resolver) => WithMetadataReferenceResolver(resolver);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithAssemblyIdentityComparer(AssemblyIdentityComparer? comparer) => WithAssemblyIdentityComparer(comparer);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithStrongNameProvider(StrongNameProvider? provider) => WithStrongNameProvider(provider);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithMetadataImportOptions(MetadataImportOptions value) => WithMetadataImportOptions(value);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithReportSuppressedDiagnostics(bool reportSuppressedDiagnostics) => WithReportSuppressedDiagnostics(reportSuppressedDiagnostics);

    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithPublicSign(bool publicSign) => WithPublicSign(publicSign);

    [Obsolete]
    protected override Microsoft.CodeAnalysis.CompilationOptions CommonWithFeatures(ImmutableArray<string> features) => throw new NotImplementedException();
    #endregion
}
