// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.PooledObjects;
using MSCA::Roslyn.Utilities;
#if !NETCOREAPP
using AllowNullAttribute = MSCA::System.Diagnostics.CodeAnalysis.AllowNullAttribute;
#endif

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilationOptions = LuaCompilationOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilationOptions = MoonScriptCompilationOptions;
#endif

#warning 未实现。
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
    public
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
        ImmutableDictionary<string, ReportDiagnostic>? specificDiagnosticOptions = null,
        bool concurrentBuild = true,
        bool deterministic = false,
        XmlReferenceResolver? xmlReferenceResolver = null,
        SourceReferenceResolver? sourceReferenceResolver = null,
        MetadataReferenceResolver? metadataReferenceResolver = null,
        AssemblyIdentityComparer? assemblyIdentityComparer = null,
        StrongNameProvider? strongNameProvider = null,
        bool publicSign = false,
        MetadataImportOptions metadataImportOptions = MetadataImportOptions.Public) :
        this(
            outputKind,
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
            xmlReferenceResolver,
            sourceReferenceResolver,
            syntaxTreeOptionsProvider: null,
            metadataReferenceResolver,
            assemblyIdentityComparer,
            strongNameProvider,
            metadataImportOptions,
            referencesSupersedeLowerVersions: false,
            publicSign)
    { }

    /// <remarks>正确参数的构造器。</remarks>
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
        ImmutableDictionary<string, ReportDiagnostic>? specificDiagnosticOptions,
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
        bool publicSign) :
        base(
            outputKind,
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
            referencesSupersedeLowerVersions)
    {
    }

    private
#if LANG_LUA
        LuaCompilationOptions
#elif LANG_MOONSCRIPT
        MoonScriptCompilationOptions
#endif
    (ThisCompilationOptions other) : this(
        other.OutputKind,
        other.ReportSuppressedDiagnostics,
        other.ModuleName,
        other.MainTypeName,
        other.ScriptClassName,
        other.OptimizationLevel,
        other.CheckOverflow,
        other.CryptoKeyContainer,
        other.CryptoKeyFile,
        other.CryptoPublicKey,
        other.DelaySign,
        other.Platform,
        other.GeneralDiagnosticOption,
        other.WarningLevel,
        other.SpecificDiagnosticOptions,
        other.ConcurrentBuild,
        other.Deterministic,
        other.CurrentLocalTime,
        other.DebugPlusMode,
        other.XmlReferenceResolver,
        other.SourceReferenceResolver,
        other.SyntaxTreeOptionsProvider,
        other.MetadataReferenceResolver,
        other.AssemblyIdentityComparer,
        other.StrongNameProvider,
        other.MetadataImportOptions,
        other.ReferencesSupersedeLowerVersions,
        other.PublicSign)
    { }

    #region With修改
    internal ThisCompilationOptions WithReferencesSupersedeLowerVersions(bool value)
    {
        if (value == this.ReferencesSupersedeLowerVersions) return this;

        return new(this) { ReferencesSupersedeLowerVersions = value };
    }
    #endregion

    public override NullableContextOptions NullableContextOptions { get => throw new NotImplementedException(); protected set => throw new NotImplementedException(); }

    public override string Language => throw new NotImplementedException();

    public override bool Equals(object? obj)
    {
        throw new NotImplementedException();
    }

    public bool Equals([AllowNull] ThisCompilationOptions other)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithAssemblyIdentityComparer(AssemblyIdentityComparer? comparer)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithCheckOverflow(bool checkOverflow)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithConcurrentBuild(bool concurrent)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithCryptoKeyContainer(string? cryptoKeyContainer)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithCryptoKeyFile(string? cryptoKeyFile)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithCryptoPublicKey(ImmutableArray<byte> cryptoPublicKey)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithDelaySign(bool? delaySign)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithDeterministic(bool deterministic)
    {
        throw new NotImplementedException();
    }

    [Obsolete]
    protected override CompilationOptions CommonWithFeatures(ImmutableArray<string> features)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithGeneralDiagnosticOption(ReportDiagnostic generalDiagnosticOption)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithMainTypeName(string? mainTypeName)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithMetadataImportOptions(MetadataImportOptions value)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithMetadataReferenceResolver(MetadataReferenceResolver? resolver)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithModuleName(string? moduleName)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithOptimizationLevel(OptimizationLevel value)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithOutputKind(OutputKind kind)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithPlatform(Platform platform)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithPublicSign(bool publicSign)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithReportSuppressedDiagnostics(bool reportSuppressedDiagnostics)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithScriptClassName(string scriptClassName)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithSourceReferenceResolver(SourceReferenceResolver? resolver)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithSpecificDiagnosticOptions(ImmutableDictionary<string, ReportDiagnostic>? specificDiagnosticOptions)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithSpecificDiagnosticOptions(IEnumerable<KeyValuePair<string, ReportDiagnostic>> specificDiagnosticOptions)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithStrongNameProvider(StrongNameProvider? provider)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithSyntaxTreeOptionsProvider(SyntaxTreeOptionsProvider? resolver)
    {
        throw new NotImplementedException();
    }

    protected override CompilationOptions CommonWithXmlReferenceResolver(XmlReferenceResolver? resolver)
    {
        throw new NotImplementedException();
    }

    protected override int ComputeHashCode()
    {
        throw new NotImplementedException();
    }

    internal override DeterministicKeyBuilder CreateDeterministicKeyBuilder()
    {
        throw new NotImplementedException();
    }

    internal override Diagnostic? FilterDiagnostic(Diagnostic diagnostic, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal override ImmutableArray<string> GetImports()
    {
        throw new NotImplementedException();
    }

    internal override void ValidateOptions(ArrayBuilder<Diagnostic> builder)
    {
        throw new NotImplementedException();
    }
}
