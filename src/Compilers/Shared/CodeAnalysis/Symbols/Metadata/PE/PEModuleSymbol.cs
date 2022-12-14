// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using MSCA::Microsoft.Cci;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.PooledObjects;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;

using ThisAttributeData = LuaAttributeData;
using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;

using ThisAttributeData = MoonScriptAttributeData;
using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

internal sealed partial class PEModuleSymbol : NonMissingNetModuleSymbol
{
    /// <summary>包含此PE模块的程序集的符号。可能是一个<see cref="PEAssemblySymbol"/>或<see cref="SourceAssemblySymbol"/>。</summary>
    private readonly AssemblySymbol _assemblySymbol;
    private readonly int _ordinal;

    /// <summary>为此符号提供元数据的PE模块对象。</summary>
    private readonly PEModule _module;

    /// <summary>表示全局命名空间的符号。</summary>
    private readonly PENamespaceSymbol _globalNamespace;

    internal readonly ImmutableArray<MetadataLocation> MetadataLocations;

    internal readonly MetadataImportOptions ImportOptions;

    /// <summary>此PE模块中的自定义特性集合。</summary>
    private ImmutableArray<ThisAttributeData> _lazyCustomAttributes;

    /// <summary>此PE模块中的程序集特性集合。</summary>
    private ImmutableArray<ThisAttributeData> _lazyAssemblyAttributes;

    /// <summary>此PE模块中的类型名称集合。</summary>
    private ICollection<string> _lazyTypeNames;

    /// <summary>此PE模块中的命名空间名称集合。</summary>
    private ICollection<string> _lazyNamespaceNames;

    private enum NullableMemberMetadata
    {
        Unknown = 0,
        Public,
        Internal,
        All,
    }

    private NullableMemberMetadata _lazyNullableMemberMetadata;

    private ThreeState _lazyUseUpdatedEscapeRules;

    private DiagnosticInfo? _lazyCachedCompilerFeatureRequireDiagnosticInfo = ThisDiagnosticInfo.EmptyErrorInfo;

    internal override int Ordinal => this._ordinal;

    internal override Machine Machine => this._module.Machine;

    internal override bool Bit32Required => this._module.Bit32Required;

    /// <summary>
    /// 获取此模块符号内部提供元数据的PE模块对象。
    /// </summary>
    /// <value>
    /// 此模块符号内部提供元数据的PE模块对象。
    /// </value>
    internal PEModule Module => this._module;

    public override NamespaceSymbol GlobalNamespace => this._globalNamespace;

    public override string Name => this._module.Name;

    private static EntityHandle Token => EntityHandle.ModuleDefinition;

    public override Symbol? ContainingSymbol => this._assemblySymbol;

    public override AssemblySymbol ContainingAssembly => this._assemblySymbol;

    public override ImmutableArray<Location> Locations => this.MetadataLocations.Cast<MetadataLocation, Location>();

    internal bool HasAnyCustomAttributes(EntityHandle token)
    {
        try
        {
            return this._module.GetCustomAttributesOrThrow(token).Count > 0;
        }
        catch (BadImageFormatException) { }

        return false;
    }

    public ImmutableArray<ThisAttributeData> GetAttributes()
    {
        if (this._lazyCustomAttributes.IsDefault)
            this.LoadCustomAttributes(PEModuleSymbol.Token, ref this._lazyCustomAttributes);

        return this._lazyCustomAttributes;
    }

    internal ImmutableArray<ThisAttributeData> GetAssemblyAttributeData()
    {
        if (this._lazyAssemblyAttributes.IsDefault)
        {
            ArrayBuilder<ThisAttributeData>? moduleAssemblyAttributesBuilder = null;

            var corlibName = this.ContainingAssembly.CorLibrary.Name;
            var assemblyMSCorLib = this.Module.GetAssemblyRef(corlibName);
            if (!assemblyMSCorLib.IsNil)
            {
                foreach (var qualifier in MetadataWriter.dummyAssemblyAttributeParentQualifier)
                {
                    var typerefAssemblyAttributeGoHere =
                        this.Module.GetTypeRef(
                            assemblyMSCorLib,
                            MetadataWriter.dummyAssemblyAttributeParentNamespace,
                            MetadataWriter.dummyAssemblyAttributeParentName + qualifier);

                    if (!typerefAssemblyAttributeGoHere.IsNil)
                    {
                        try
                        {
                            foreach (var customAttributeHandle in this.Module.GetCustomAttributesOrThrow(typerefAssemblyAttributeGoHere))
                            {
                                moduleAssemblyAttributesBuilder ??= new();
                                moduleAssemblyAttributesBuilder.Add(new PEAttributeData(this, customAttributeHandle));
                            }
                        }
                        catch (BadImageFormatException) { }
                    }
                }
            }

            ImmutableInterlocked.InterlockedCompareExchange(
                ref this._lazyAssemblyAttributes,
                moduleAssemblyAttributesBuilder?.ToImmutableAndFree() ?? ImmutableArray<ThisAttributeData>.Empty,
                default);
        }

        return this._lazyAssemblyAttributes;
    }

    internal void LoadCustomAttributes(EntityHandle token, ref ImmutableArray<ThisAttributeData> customAttributes)
    {
        var loaded = this.GetCustomAttributesForToken(token);
        ImmutableInterlocked.InterlockedInitialize(ref customAttributes, loaded);
    }

    internal void LoadCustomAttributesFilterExtensions(EntityHandle token, ref ImmutableArray<ThisAttributeData> customAttributes)
    {
        var loadedCustomAttributes = this.GetCustomAttributesFilterCompilerAttributes(token, out _, out _);
        ImmutableInterlocked.InterlockedInitialize(ref customAttributes, loadedCustomAttributes);
    }

    internal PEModuleSymbol(
        PEAssemblySymbol assemblySymbol,
        PEModule module,
        MetadataImportOptions importOptions,
        int ordinal)
        : this((AssemblySymbol)assemblySymbol, module, importOptions, ordinal) =>
        Debug.Assert(ordinal >= 0);

    internal PEModuleSymbol(
        SourceAssemblySymbol assemblySymbol,
        PEModule module,
        MetadataImportOptions importOptions,
        int ordinal)
        : this((AssemblySymbol)assemblySymbol, module, importOptions, ordinal) =>
        Debug.Assert(ordinal >= 0);

    private PEModuleSymbol(
        AssemblySymbol assemblySymbol,
        PEModule module,
        MetadataImportOptions importOptions,
        int ordinal)
    {
        this._assemblySymbol = assemblySymbol;
        this._ordinal = ordinal;
        this._module = module;
        this.ImportOptions = importOptions;
        this._globalNamespace = new(this);
        this.MetadataLocations = ImmutableArray.Create(new MetadataLocation(this));
    }
}
