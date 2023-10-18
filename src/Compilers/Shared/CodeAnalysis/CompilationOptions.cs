// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilationOptions = LuaCompilationOptions;
using ThisDeterministicKeyBuilder = LuaDeterministicKeyBuilder;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilationOptions = MoonScriptCompilationOptions;
using ThisDeterministicKeyBuilder = MoonScriptDeterministicKeyBuilder;
#else
#error Not implemented
#endif

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
        Platform platform = Platform.AnyCpu,
        string? netmoduleName = null,
        string? mainModuleName = null,
        string? scriptModuleName = null,
        OptimizationLevel optimizationLevel = OptimizationLevel.Debug,
        int warningLevel = Diagnostic.DefaultWarningLevel,
        bool concurrentBuild = true,
        bool deterministic = false,
        string? cryptoKeyContainer = null,
        string? cryptoKeyFile = null,
        ImmutableArray<byte> cryptoPublicKey = default,
        bool? delaySign = null,
        bool publicSign = false,
        ReportDiagnostic generalDiagnosticOption = ReportDiagnostic.Default,
        IEnumerable<KeyValuePair<string, ReportDiagnostic>>? specificDiagnosticOptions = null,
        XmlReferenceResolver? xmlReferenceResolver = null,
        SourceReferenceResolver? sourceReferenceResolver = null,
        MetadataReferenceResolver? metadataReferenceResolver = null,
        AssemblyIdentityComparer? assemblyIdentityComparer = null,
        StrongNameProvider? strongNameProvider = null,
        MetadataImportOptions metadataImportOptions = MetadataImportOptions.Public) :
        this(
            outputKind,
            platform,
            netmoduleName,
            mainModuleName,
            scriptModuleName,
            optimizationLevel,
            warningLevel,
            concurrentBuild,
            deterministic,
            currentLocalTime: default,
            cryptoKeyContainer,
            cryptoKeyFile,
            cryptoPublicKey,
            delaySign,
            publicSign,
            referencesSupersedeLowerVersions: true,
            generalDiagnosticOption,
            specificDiagnosticOptions.ToImmutableDictionaryOrEmpty(),
            reportSuppressedDiagnostics: false,
            xmlReferenceResolver,
            sourceReferenceResolver,
            syntaxTreeOptionsProvider: null,
            metadataReferenceResolver,
            assemblyIdentityComparer,
            strongNameProvider,
            metadataImportOptions,
            debugPlusMode: false)
    { }

    /// <inheritdoc/>
    public override NullableContextOptions NullableContextOptions { get => NullableContextOptions.Enable; protected set { } }

    /// <inheritdoc/>
    public override bool Equals(object? obj) => this.Equals(obj as ThisCompilationOptions);

    /// <inheritdoc/>
    public partial bool Equals(ThisCompilationOptions? other);

    /// <inheritdoc/>
    protected override partial int ComputeHashCode();

    /// <inheritdoc/>
    internal override DeterministicKeyBuilder CreateDeterministicKeyBuilder() => ThisDeterministicKeyBuilder.Instance;

    /// <inheritdoc/>
    internal override partial Diagnostic? FilterDiagnostic(Diagnostic diagnostic, CancellationToken cancellationToken);

    /// <inheritdoc/>
    internal override partial ImmutableArray<string> GetImports();

    /// <inheritdoc/>
    internal override partial void ValidateOptions(ArrayBuilder<Diagnostic> builder);

    #region Options
    internal ThisCompilationOptions WithReferencesSupersedeLowerVersions(bool value)
    {
        if (value == this.ReferencesSupersedeLowerVersions)
            return this;

        return new(this) { ReferencesSupersedeLowerVersions = value };
    }

    /// <inheritdoc/>
    [Obsolete]
    protected override CompilationOptions CommonWithFeatures(ImmutableArray<string> features) => throw new NotImplementedException();
    #endregion
}
