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

using ThisCompilation = LuaCompilation;
using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;

using ThisCompilation = MoonScriptCompilation;
using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

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
    /// 其第一项为一个私有的<see cref="SourceNetModuleSymbol"/>，其他的均为.NET模块。
    /// </summary>
    private readonly ImmutableArray<NetModuleSymbol> _modules;

    /// <summary>许可此程序集操作内部元数据的程序集符号集。其中的程序集符号应符合特定的名称以及公钥。</summary>
    private ConcurrentSet<AssemblySymbol> _optimisticallyGrantedInternalsAccess;

    //EDMAURER please don't use thread local storage widely. This is hoped to be a one-off usage.
    [ThreadStatic]
    private static AssemblySymbol t_assemblyForWhichCurrentThreadIsComputingKeys;

    public override string Name => this._assemblySimpleName;

    public override ThisCompilation DeclaringCompilation => this._compilation;

    public override bool IsInteractive => this._compilation.IsSubmission;

    public override ImmutableArray<NetModuleSymbol> NetModules => this._modules;

    public override ImmutableArray<Location> Locations => this._modules.SelectMany(m => m.Locations).AsImmutable();

    public override AssemblyIdentity Identity
    {
        get
        {
            if (lazyAssemblyIdentity is null)
                Interlocked.CompareExchange(ref this.lazyAssemblyIdentity, this.ComputeIdentity(), null);

            return this.lazyAssemblyIdentity;
        }
    }

    internal StrongNameKeys StrongNameKeys => this._assemblyStrongNameKeys;

    internal override ImmutableArray<byte> PublicKey => this.StrongNameKeys.PublicKey;

    private Version AssemblyVersion
    {
        get
        {

        }
    }

    public override Version? AssemblyVersionPattern
    {
        get
        {
            var version = this.AssemblyVersion;
            return (version is null || (version.Build != ushort.MaxValue && version.Revision != ushort.MaxValue)) ? null : version;
        }
    }

    internal string FileVersion { get { } }

    internal string InformationVersion { get { } }

    internal string Title { get { } }

    internal string Description { get { } }

    internal string Company { get { } }

    internal string Product { get { } }

    internal string Copyright { get { } }

    internal string Trademark { get { } }

    private ThreeState AssemblyDelaySign { get { } }

    private string AssemblyKeyContainer { get { } }

    private string AssemblyKeyFile { get { } }

    private string AssemblyCulture { get { } }

    public string SignatureKey { get { } }

    internal AssemblyHashAlgorithm? AssemblyAlgorithmId { get { } }

    public AssemblyHashAlgorithm HashAlgorithm => this.AssemblyAlgorithmId ?? AssemblyHashAlgorithm.Sha1;

    public AssemblyFlags AssemblyFlags { get { } }

    public bool InternalsAreVisible { get { } }

    internal bool IsDelaySigned
    {
        get
        {
            if (this._compilation.Options.DelaySign.HasValue)
                return this._compilation.Options.DelaySign.Value;

            return this._compilation.Options.PublicSign;
        }
    }

    internal SourceNetModuleSymbol SourceNetModule => (SourceNetModuleSymbol)this._modules[0];

    internal SourceAssemblySymbol(
        ThisCompilation compilation,
        string assemblySimpleName,
        string moduleName,
        ImmutableArray<PEModule> netModules)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(moduleName));
        Debug.Assert(!netModules.IsDefault);

        this._compilation = compilation;
        this._assemblySimpleName = assemblySimpleName;

        var moduleBuilder = new ArrayBuilder<NetModuleSymbol>(netModules.Length + 1);
        moduleBuilder.Add(new SourceNetModuleSymbol(this, compilation.Declarations, moduleName));

        var importOptions = (compilation.Options.MetadataImportOptions == MetadataImportOptions.All) ?
            MetadataImportOptions.All : MetadataImportOptions.Internal;

        foreach (var netModule in netModules)
            moduleBuilder.Add(new PEModuleSymbol(this, netModule, importOptions, moduleBuilder.Count));

        this._modules = moduleBuilder.ToImmutableAndFree();

        if (!compilation.Options.CryptoPublicKey.IsEmpty)
            this._assemblyStrongNameKeys = StrongNameKeys.Create(compilation.Options.CryptoPublicKey, privateKey: null, hasCounterSignature: false, messageProvider: MessageProvider.Instance);
        else
            this._assemblyStrongNameKeys = this.ComputeStrongNameKeys();
    }

    private AssemblyIdentity ComputeIdentity()
    {
        return new AssemblyIdentity(
            this._assemblySimpleName,
            version: VersionHelper.GenerateVersionFromPatternAndCurrentTime(this._compilation.Options.CurrentLocalTime, this.AssemblyVersion),
            cultureName: this.AssemblyCulture,
            publicKeyOrToken: this.StrongNameKeys.PublicKey,
            hasPublicKey: !this.StrongNameKeys.PublicKey.IsDefault,
            isRetargetable: (this.AssemblyFlags & AssemblyFlags.Retargetable) == AssemblyFlags.Retargetable);
    }

    private StrongNameKeys ComputeStrongNameKeys()
    {
        var keyFile = this._compilation.Options.CryptoKeyFile;

        if (this._compilation.Options.PublicSign)
        {
            if (!string.IsNullOrEmpty(keyFile) && !PathUtilities.IsAbsolute(keyFile))
            {
                Debug.Assert(!this._compilation.Options.Errors.IsEmpty);
                return StrongNameKeys.None;
            }

            return StrongNameKeys.Create(keyFile, messageProvider: MessageProvider.Instance);
        }
        if (string.IsNullOrEmpty(keyFile)) keyFile = this.AssemblyKeyFile;

        var keyContainer = this._compilation.Options.CryptoKeyContainer;
        if (string.IsNullOrEmpty(keyContainer)) keyContainer = this.AssemblyKeyContainer;

        var hasCounterSignature = !string.IsNullOrEmpty(this.SignatureKey);
        return StrongNameKeys.Create(this._compilation.Options.StrongNameProvider, keyFile, keyContainer, hasCounterSignature, MessageProvider.Instance);
    }

    internal bool MightContainNoPiaLocalTypes()
    {
        foreach (Metadata.PE.PEModuleSymbol peModuleSymbol in this._modules)
        {
            if (peModuleSymbol.Module.ContainsNoPiaLocalTypes())
                return true;
        }

        return false;
    }

    #region 符号完成
    private SymbolCompletionState _state;

    /// <value>返回<see langword="true"/>。</value>
    /// <inheritdoc/>
    internal override bool RequiresCompletion => true;

    internal override bool HasComplete(CompletionPart part) => this._state.HasComplete(part);

    internal override partial void ForceComplete(SourceLocation? location, CancellationToken cancellationToken);
    #endregion

    private void ReportDiagnosticsForAddedNetModules()
    {
        var diagnostics = BindingDiagnosticBag.GetInstance();

        foreach (var (reference, index) in this._compilation.GetBoundReferenceManager().ReferencedModuleIndexMap as IDictionary<MetadataReference, int>)
        {
            if (reference is PortableExecutableReference fileRef && fileRef.FilePath is not null)
            {
                var fileName = FileNameUtilities.GetFileName(fileRef.FilePath);
                var moduleName = this._modules[index].Name;

                if (!string.Equals(fileName, moduleName, StringComparison.OrdinalIgnoreCase))
                    diagnostics.Add(ErrorCode.ERR_NetModuleNameMismatch, NoLocation.Singleton, moduleName, fileName);
            }
        }

        // 仅在发射程序集时检查。
        if (this._modules.Length > 1 && !this._compilation.Options.OutputKind.IsNetModule())
        {
            var assemblyMachine = this.Machine;
            var isPlatformAgnostic = (assemblyMachine == Machine.I386 && !this.Bit32Required);
            var knownModuleNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var m in this._modules)
            {
                if (!knownModuleNames.Add(m.Name))
                    diagnostics.Add(ErrorCode.ERR_NetModuleNameMustBeUnique, NoLocation.Singleton, m.Name);

                if (!((PEModuleSymbol)m).Module.IsCOFFOnly)
                {
                    var moduleMachine = m.Machine;

                    if (moduleMachine == Machine.I386 && !m.Bit32Required)
                    {
                        // 这种情况是合理的，其他情况不合理。
                        ;
                    }
                    else if (isPlatformAgnostic)
                        diagnostics.Add(ErrorCode.ERR_AgnosticToMachineModule, NoLocation.Singleton, m);
                    else if (assemblyMachine != moduleMachine)
                        diagnostics.Add(ErrorCode.ERR_ConflictingMachineModule, NoLocation.Singleton, m);
                }
            }

            // 程序集的主模块必须显式地引用被其他程序集模块引用的所有模块，例如传递闭包中的所有模块都必须在此显式地引用。
            foreach (PEModuleSymbol m in this._modules)
            {
                try
                {
                    foreach (var referencedModuleName in m.Module.GetReferencedManagedModulesOrThrow())
                    {
                        if (knownModuleNames.Add(referencedModuleName))
                            diagnostics.Add(ErrorCode.ERR_MissingNetModuleReference, NoLocation.Singleton, referencedModuleName);
                    }
                }
                catch (BadImageFormatException)
                {
                    diagnostics.Add(new ThisDiagnosticInfo(ErrorCode.ERR_BindToBogus, m), NoLocation.Singleton);
                }
            }
        }

        this.ReportNameCollisionDiagnosticsForAddedModules(this.GlobalNamespace, diagnostics);
        this.AddDeclarationDiagnostics(diagnostics);
        diagnostics.Free();
    }
    private void ReportNameCollisionDiagnosticsForAddedModules(NamespaceSymbol ns, BindingDiagnosticBag diagnostics)
    {
        if (ns is not MergedNamespaceSymbol mergedNs) return;

        ImmutableArray<NamespaceSymbol> constituent = mergedNs.ConstituentNamespaces;

        if (constituent.Length > 2 || (constituent.Length == 2 && constituent[0].ContainingModule.Ordinal != 0 && constituent[1].ContainingModule.Ordinal != 0))
        {
            var topLevelTypesFromModules = ArrayBuilder<NamedTypeSymbol>.GetInstance();

            foreach (var moduleNs in constituent)
            {
                Debug.Assert(moduleNs.Extent.Kind == NamespaceKind.Module);

                if (moduleNs.ContainingModule.Ordinal != 0)
                    topLevelTypesFromModules.AddRange(moduleNs.GetTypeMembers());
            }

            topLevelTypesFromModules.Sort(NameCollisionForAddedModulesTypeComparer.Singleton);

            var reportedAnError = false;
            for (var i = 0; i < topLevelTypesFromModules.Count - 1; i++)
            {
                var x = topLevelTypesFromModules[i];
                var y = topLevelTypesFromModules[i + 1];

                if (x.Arity == y.Arity && x.Name == y.Name)
                {
                    if (!reportedAnError)
                    {
                        // Skip synthetic <Module> type which every .NET module has.
                        if (x.Arity != 0 || !x.ContainingNamespace.IsGlobalNamespace || x.Name != "<Module>")
                            diagnostics.Add(ErrorCode.ERR_DuplicateNameInNS, y.Locations.FirstOrNone(),
                                            y.ToDisplayString(SymbolDisplayFormat.ShortFormat),
                                            y.ContainingNamespace);

                        reportedAnError = true;
                    }
                }
                else reportedAnError = false;
            }

            topLevelTypesFromModules.Free();

            // 递归处理子命名空间。
            foreach (Symbol member in mergedNs.GetMembers())
            {
                if (member.Kind == SymbolKind.Namespace)
                    this.ReportNameCollisionDiagnosticsForAddedModules((NamespaceSymbol)member, diagnostics);
            }
        }
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
                    result = x.ContainingModule.Ordinal - y.ContainingModule.Ordinal;
            }

            return result;
        }
    }

}
