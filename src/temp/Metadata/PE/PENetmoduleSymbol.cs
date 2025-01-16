// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Reflection.PortableExecutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

internal sealed partial class PENetmoduleSymbol : NonMissingNetmoduleSymbol
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

    internal override int Ordinal => _ordinal;

    internal override Machine Machine => _module.Machine;

    internal override bool Bit32Required => _module.Bit32Required;

    /// <summary>
    /// 获取此模块符号内部提供元数据的PE模块对象。
    /// </summary>
    /// <value>
    /// 此模块符号内部提供元数据的PE模块对象。
    /// </value>
    internal PEModule Module => _module;

    internal override ModuleSymbol GlobalNamespace => _globalNamespace;

    public override ModuleSymbol GlobalModule => _globalNamespace;

    public override string Name => _module.Name;

    private static EntityHandle Token => EntityHandle.ModuleDefinition;

    public override Symbol? ContainingSymbol => _assemblySymbol;

    public override AssemblySymbol ContainingAssembly => _assemblySymbol;

    public override ImmutableArray<Location> Locations => MetadataLocations.Cast<MetadataLocation, Location>();

    internal bool HasAnyCustomAttributes(EntityHandle token)
    {
        try
        {
            return _module.GetCustomAttributesOrThrow(token).Count > 0;
        }
        catch (BadImageFormatException) { }

        return false;
    }

    public ImmutableArray<ThisAttributeData> GetAttributes()
    {
        if (_lazyCustomAttributes.IsDefault)
            LoadCustomAttributes(Token, ref _lazyCustomAttributes);

        return _lazyCustomAttributes;
    }

    internal ImmutableArray<ThisAttributeData> GetAssemblyAttributeData()
    {
        if (_lazyAssemblyAttributes.IsDefault)
        {
            ArrayBuilder<ThisAttributeData>? moduleAssemblyAttributesBuilder = null;

            var corlibName = ContainingAssembly.CorLibrary.Name;
            var assemblyMSCorLib = Module.GetAssemblyRef(corlibName);
            if (!assemblyMSCorLib.IsNil)
            {
                foreach (var qualifier in MetadataWriter.dummyAssemblyAttributeParentQualifier)
                {
                    var typerefAssemblyAttributeGoHere =
                        Module.GetTypeRef(
                            assemblyMSCorLib,
                            MetadataWriter.dummyAssemblyAttributeParentNamespace,
                            MetadataWriter.dummyAssemblyAttributeParentName + qualifier);

                    if (!typerefAssemblyAttributeGoHere.IsNil)
                    {
                        try
                        {
                            foreach (var customAttributeHandle in Module.GetCustomAttributesOrThrow(typerefAssemblyAttributeGoHere))
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
                ref _lazyAssemblyAttributes,
                moduleAssemblyAttributesBuilder?.ToImmutableAndFree() ?? ImmutableArray<ThisAttributeData>.Empty,
                default);
        }

        return _lazyAssemblyAttributes;
    }

    internal void LoadCustomAttributes(EntityHandle token, ref ImmutableArray<ThisAttributeData> customAttributes)
    {
        var loaded = GetCustomAttributesForToken(token);
        ImmutableInterlocked.InterlockedInitialize(ref customAttributes, loaded);
    }

    internal void LoadCustomAttributesFilterExtensions(EntityHandle token, ref ImmutableArray<ThisAttributeData> customAttributes)
    {
        var loadedCustomAttributes = GetCustomAttributesFilterCompilerAttributes(token, out _, out _);
        ImmutableInterlocked.InterlockedInitialize(ref customAttributes, loadedCustomAttributes);
    }

    private ImmutableArray<ThisAttributeData> GetCustomAttributesForToken(EntityHandle token)
    {
#warning 未完成
        throw new NotImplementedException();
    }

    private ImmutableArray<ThisAttributeData> GetCustomAttributesFilterCompilerAttributes(EntityHandle token, out object o1, out object o2)
    {
#warning 未完成
        throw new NotImplementedException();
    }

    internal PENetmoduleSymbol(
        PEAssemblySymbol assemblySymbol,
        PEModule module,
        MetadataImportOptions importOptions,
        int ordinal)
        : this((AssemblySymbol)assemblySymbol, module, importOptions, ordinal) =>
        Debug.Assert(ordinal >= 0);

    internal PENetmoduleSymbol(
        SourceAssemblySymbol assemblySymbol,
        PEModule module,
        MetadataImportOptions importOptions,
        int ordinal)
        : this((AssemblySymbol)assemblySymbol, module, importOptions, ordinal) =>
        Debug.Assert(ordinal >= 0);

    private PENetmoduleSymbol(
        AssemblySymbol assemblySymbol,
        PEModule module,
        MetadataImportOptions importOptions,
        int ordinal)
    {
        _assemblySymbol = assemblySymbol;
        _ordinal = ordinal;
        _module = module;
        ImportOptions = importOptions;
        _globalNamespace = new(this);
        MetadataLocations = ImmutableArray.Create(new MetadataLocation(this));
    }
}

partial class PENetmoduleSymbol
{
#warning 未完成
    public override string MetadataName => base.MetadataName;

    public override int MetadataToken => base.MetadataToken;

    public override ThisCompilation? DeclaringCompilation => base.DeclaringCompilation;

    public override bool HasUnsupportedMetadata => base.HasUnsupportedMetadata;

    public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences => base.DeclaringSyntaxReferences;

    public override bool AreLocalsZeroed => throw new NotImplementedException();

    protected override Symbol OriginalSymbolDefinition => base.OriginalSymbolDefinition;

    internal override ModuleSymbol? ContainingNamespace => base.ContainingNamespace;

    internal override bool RequiresCompletion => base.RequiresCompletion;

    internal override bool HasUnifiedReferences => base.HasUnifiedReferences;

    public override void Accept(ThisSymbolVisitor visitor)
    {
        base.Accept(visitor);
    }

    public override TResult? Accept<TResult>(ThisSymbolVisitor<TResult> visitor) where TResult : default
    {
        return base.Accept(visitor);
    }

    public override bool Equals(Symbol? other, TypeCompareKind compareKind)
    {
        return base.Equals(other, compareKind);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override ModuleMetadata GetMetadata()
    {
        throw new NotImplementedException();
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
        return base.Accept(visitor, argument);
    }

    internal override void ForceComplete(SourceLocation? location, CancellationToken cancellationToken)
    {
        base.ForceComplete(location, cancellationToken);
    }

    internal override LexicalSortKey GetLexicalSortKey()
    {
        return base.GetLexicalSortKey();
    }

    internal override bool GetUnificationUseSiteDiagnostic(ref DiagnosticInfo result, TypeSymbol dependentType)
    {
        return base.GetUnificationUseSiteDiagnostic(ref result, dependentType);
    }

    internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
    {
        return base.GetUseSiteInfo();
    }

    internal override bool HasComplete(CompletionPart part)
    {
        return base.HasComplete(part);
    }

    internal override NamedTypeSymbol? LookupTopLevelMetadataType(ref MetadataTypeName emittedName)
    {
        throw new NotImplementedException();
    }

    internal override void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol? originatingSourceAssemblyDebugOnly = null)
    {
        base.SetReferences(moduleReferences, originatingSourceAssemblyDebugOnly);
    }
}
