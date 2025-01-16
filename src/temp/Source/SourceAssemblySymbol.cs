// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

using Machine = System.Reflection.PortableExecutable.Machine;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

using Metadata.PE;

internal sealed partial class SourceAssemblySymbol : AssemblySymbol
{
    /// <summary>用于创建此程序集符号的编译内容。</summary>
    private readonly ThisCompilation _compilation;

    /// <summary>此程序集的身份。</summary>
    internal AssemblyIdentity? lazyAssemblyIdentity;
    private readonly string _assemblySimpleName;

    private readonly StrongNameKeys _assemblyStrongNameKeys;

    /// <summary>
    /// 组成此程序集的模块列表。
    /// 其第一项为一个私有的<see cref="SourceNetmoduleSymbol"/>，其他的均为.NET模块。
    /// </summary>
    private readonly ImmutableArray<NetmoduleSymbol> _modules;

    /// <summary>许可此程序集操作内部元数据的程序集符号集。其中的程序集符号应符合特定的名称以及公钥。</summary>
    private ConcurrentSet<AssemblySymbol> _optimisticallyGrantedInternalsAccess;

    //EDMAURER please don't use thread local storage widely. This is hoped to be a one-off usage.
    [ThreadStatic]
    private static AssemblySymbol t_assemblyForWhichCurrentThreadIsComputingKeys;

    public override string Name => _assemblySimpleName;

    public override ThisCompilation DeclaringCompilation => _compilation;

    public override bool IsInteractive => _compilation.IsSubmission;

    public override ImmutableArray<NetmoduleSymbol> Netmodules => _modules;

    public override ImmutableArray<Location> Locations => _modules.SelectMany(m => m.Locations).AsImmutable();

    public override AssemblyIdentity Identity
    {
        get
        {
            if (lazyAssemblyIdentity is null)
                Interlocked.CompareExchange(ref lazyAssemblyIdentity, ComputeIdentity(), null);

            return lazyAssemblyIdentity;
        }
    }

    internal StrongNameKeys StrongNameKeys => _assemblyStrongNameKeys;

    internal override ImmutableArray<byte> PublicKey => StrongNameKeys.PublicKey;

    private Version AssemblyVersion => throw new NotImplementedException();

    public override Version? AssemblyVersionPattern
    {
        get
        {
            var version = AssemblyVersion;
            return (version is null || (version.Build != ushort.MaxValue && version.Revision != ushort.MaxValue)) ? null : version;
        }
    }

    internal string FileVersion => throw new NotImplementedException();

    internal string InformationVersion => throw new NotImplementedException();

    internal string Title => throw new NotImplementedException();

    internal string Description => throw new NotImplementedException();

    internal string Company => throw new NotImplementedException();

    internal string Product => throw new NotImplementedException();

    internal string Copyright => throw new NotImplementedException();

    internal string Trademark => throw new NotImplementedException();

    private ThreeState AssemblyDelaySign => throw new NotImplementedException();

    private string AssemblyKeyContainer => throw new NotImplementedException();

    private string AssemblyKeyFile => throw new NotImplementedException();

    private string AssemblyCulture => throw new NotImplementedException();

    public string SignatureKey => throw new NotImplementedException();

    internal AssemblyHashAlgorithm? AssemblyAlgorithmId => throw new NotImplementedException();

    public AssemblyHashAlgorithm HashAlgorithm => AssemblyAlgorithmId ?? AssemblyHashAlgorithm.Sha1;

    public AssemblyFlags AssemblyFlags => throw new NotImplementedException();

    public bool InternalsAreVisible => throw new NotImplementedException();

    internal bool IsDelaySigned
    {
        get
        {
            if (_compilation.Options.DelaySign.HasValue)
                return _compilation.Options.DelaySign.Value;

            return _compilation.Options.PublicSign;
        }
    }

    internal SourceNetmoduleSymbol SourceNetmodule => (SourceNetmoduleSymbol)_modules[0];

    internal SourceAssemblySymbol(
        ThisCompilation compilation,
        string assemblySimpleName,
        string moduleName,
        ImmutableArray<PEModule> netmodules)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(moduleName));
        Debug.Assert(!netmodules.IsDefault);

        _compilation = compilation;
        _assemblySimpleName = assemblySimpleName;

        var moduleBuilder = new ArrayBuilder<NetmoduleSymbol>(netmodules.Length + 1);
        moduleBuilder.Add(new SourceNetmoduleSymbol(this, compilation.Declarations, moduleName));

        var importOptions = (compilation.Options.MetadataImportOptions == MetadataImportOptions.All) ?
            MetadataImportOptions.All : MetadataImportOptions.Internal;

        foreach (var netmodule in netmodules)
            moduleBuilder.Add(new PENetmoduleSymbol(this, netmodule, importOptions, moduleBuilder.Count));

        _modules = moduleBuilder.ToImmutableAndFree();

        if (!compilation.Options.CryptoPublicKey.IsEmpty)
            _assemblyStrongNameKeys = StrongNameKeys.Create(compilation.Options.CryptoPublicKey, privateKey: null, hasCounterSignature: false, messageProvider: ThisMessageProvider.Instance);
        else
            _assemblyStrongNameKeys = ComputeStrongNameKeys();
    }

    private AssemblyIdentity ComputeIdentity()
    {
        return new AssemblyIdentity(
            _assemblySimpleName,
            version: VersionHelper.GenerateVersionFromPatternAndCurrentTime(_compilation.Options.CurrentLocalTime, AssemblyVersion),
            cultureName: AssemblyCulture,
            publicKeyOrToken: StrongNameKeys.PublicKey,
            hasPublicKey: !StrongNameKeys.PublicKey.IsDefault,
            isRetargetable: (AssemblyFlags & AssemblyFlags.Retargetable) == AssemblyFlags.Retargetable);
    }

    private StrongNameKeys ComputeStrongNameKeys()
    {
        var keyFile = _compilation.Options.CryptoKeyFile;

        if (_compilation.Options.PublicSign)
        {
            if (!string.IsNullOrEmpty(keyFile) && !PathUtilities.IsAbsolute(keyFile))
            {
                Debug.Assert(!_compilation.Options.Errors.IsEmpty);
                return StrongNameKeys.None;
            }

            return StrongNameKeys.Create(keyFile, messageProvider: ThisMessageProvider.Instance);
        }
        if (string.IsNullOrEmpty(keyFile)) keyFile = AssemblyKeyFile;

        var keyContainer = _compilation.Options.CryptoKeyContainer;
        if (string.IsNullOrEmpty(keyContainer)) keyContainer = AssemblyKeyContainer;

        var hasCounterSignature = !string.IsNullOrEmpty(SignatureKey);
        return StrongNameKeys.Create(_compilation.Options.StrongNameProvider, keyFile, keyContainer, hasCounterSignature, ThisMessageProvider.Instance);
    }

    internal bool MightContainNoPiaLocalTypes()
    {
        foreach (PENetmoduleSymbol peNetmoduleSymbol in _modules)
        {
            if (peNetmoduleSymbol.Module.ContainsNoPiaLocalTypes())
                return true;
        }

        return false;
    }

    #region 符号完成
    private SymbolCompletionState _state;

    /// <value>返回<see langword="true"/>。</value>
    /// <inheritdoc/>
    internal override bool RequiresCompletion => true;

    internal override bool HasComplete(CompletionPart part) => _state.HasComplete(part);

    internal override partial void ForceComplete(SourceLocation? location, CancellationToken cancellationToken);
    #endregion

    private void ReportDiagnosticsForAddedNetmodules()
    {
        throw new NotImplementedException();
    }

    private void ReportNameCollisionDiagnosticsForAddedModules(ModuleSymbol module, BindingDiagnosticBag diagnostics)
    {
        throw new NotImplementedException();
    }

    private class NameCollisionForAddedModulesTypeComparer : IComparer<NamedTypeSymbol>
    {
        public static readonly NameCollisionForAddedModulesTypeComparer Singleton = new();

        private NameCollisionForAddedModulesTypeComparer() { }

        public int Compare(NamedTypeSymbol? x, NamedTypeSymbol? y)
        {
            Debug.Assert(x is not null);
            Debug.Assert(y is not null);

            var result = string.CompareOrdinal(x.Name, y.Name);

            if (result == 0)
            {
                result = x.Arity - y.Arity;

                if (result == 0)
                    result = x.ContainingNetmodule.Ordinal - y.ContainingNetmodule.Ordinal;
            }

            return result;
        }
    }
}

partial class SourceAssemblySymbol
{
#warning 未完成
    public override string MetadataName => base.MetadataName;

    public override int MetadataToken => base.MetadataToken;

    public override bool IsImplicitlyDeclared => base.IsImplicitlyDeclared;

    public override bool HasUnsupportedMetadata => base.HasUnsupportedMetadata;

    internal override ModuleSymbol GlobalNamespace => throw new NotImplementedException();

    public override ModuleSymbol GlobalModule => throw new NotImplementedException();

    public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences => base.DeclaringSyntaxReferences;

    public override ICollection<string> TypeNames => throw new NotImplementedException();

    public override ICollection<string> ModuleNames => throw new NotImplementedException();

    public override bool MightContainExtensionMethods => throw new NotImplementedException();

    public override AssemblyMetadata GetMetadata()
    {
        throw new NotImplementedException();
    }

    protected override Symbol OriginalSymbolDefinition => base.OriginalSymbolDefinition;

    internal override ModuleSymbol? ContainingNamespace => base.ContainingNamespace;

    internal override bool IsMissing => throw new NotImplementedException();

    internal override bool KeepLookingForDeclaredSpecialTypes => base.KeepLookingForDeclaredSpecialTypes;

    internal override ICollection<string> NamespaceNames => throw new NotImplementedException();

    public override void Accept(ThisSymbolVisitor visitor)
    {
        throw new NotImplementedException();
    }

    public override TResult? Accept<TResult>(ThisSymbolVisitor<TResult> visitor) where TResult : default
    {
        throw new NotImplementedException();
    }

    public override bool Equals(Symbol? other, TypeCompareKind compareKind)
    {
        return base.Equals(other, compareKind);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    protected override ISymbol CreateISymbol()
    {
        throw new NotImplementedException();
    }

    protected override SymbolAdapter GetCciAdapterImpl()
    {
        return base.GetCciAdapterImpl();
    }

    protected override bool IsHighestPriorityUseSiteErrorCode(int code)
    {
        return base.IsHighestPriorityUseSiteErrorCode(code);
    }

    internal override TResult? Accept<TArgument, TResult>(ThisSymbolVisitor<TArgument, TResult> visitor, TArgument argument) where TResult : default
    {
        throw new NotImplementedException();
    }

    internal override IEnumerable<NamedTypeSymbol> GetAllTopLevelForwardedTypes()
    {
        throw new NotImplementedException();
    }

    internal override NamedTypeSymbol GetDeclaredSpecialType(SpecialType type)
    {
        throw new NotImplementedException();
    }

    internal override LexicalSortKey GetLexicalSortKey()
    {
        return base.GetLexicalSortKey();
    }

    internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
    {
        return base.GetUseSiteInfo();
    }

    internal override NamedTypeSymbol LookupDeclaredOrForwardedTopLevelMetadataType(ref MetadataTypeName emittedName, ConsList<AssemblySymbol>? visitedAssemblies)
    {
        throw new NotImplementedException();
    }

    internal override NamedTypeSymbol? LookupDeclaredTopLevelMetadataType(ref MetadataTypeName emittedName)
    {
        throw new NotImplementedException();
    }

    internal override void RegisterDeclaredSpecialType(NamedTypeSymbol corType)
    {
        base.RegisterDeclaredSpecialType(corType);
    }

    internal override NamedTypeSymbol? TryLookupForwardedMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol>? visitedAssemblies)
    {
        return base.TryLookupForwardedMetadataTypeWithCycleDetection(ref emittedName, visitedAssemblies);
    }
}
