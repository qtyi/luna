// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

internal sealed class SourceNetmoduleSymbol : NetmoduleSymbol
{
    /// <summary>
    /// The name (contains extension)
    /// </summary>
    private readonly string _name;

    /// <summary>
    /// Owning assembly.
    /// </summary>
    private readonly SourceAssemblySymbol _assemblySymbol;

    private ImmutableArray<AssemblySymbol> _lazyAssembliesToEmbedTypesFrom;

    private ThreeState _lazyContainsExplicitDefinitionOfNoPiaLocalTypes = ThreeState.Unknown;

    /// <summary>
    /// The declarations corresponding to the source files of this module.
    /// </summary>
    private readonly DeclarationTable _sources;

    private SymbolCompletionState _state;
    private ImmutableArray<Location> _locations;
    private ModuleSymbol? _lazyGlobalModule;

    private bool _hasBadAttributes;

    private ThreeState _lazyUseUpdatedEscapeRules;
    private ThreeState _lazyRequiresRefSafetyRulesAttribute;

    internal SourceNetmoduleSymbol(
        SourceAssemblySymbol assemblySymbol,
        DeclarationTable declarations,
        string netmoduleName)
    {
        Debug.Assert((object)assemblySymbol != null);

        _assemblySymbol = assemblySymbol;
        _sources = declarations;
        _name = netmoduleName;
    }

    /// <inheritdoc/>
    public override string Name => _name;

    #region Containing
    /// <inheritdoc/>
    public override Symbol? ContainingSymbol => _assemblySymbol;

    /// <inheritdoc/>
    public override AssemblySymbol ContainingAssembly => _assemblySymbol;

    internal SourceAssemblySymbol ContainingSourceAssembly => _assemblySymbol;
    #endregion

    #region Declaring
    /// <inheritdoc/>
    public override ThisCompilation? DeclaringCompilation => _assemblySymbol.DeclaringCompilation;
    #endregion

    internal override int Ordinal => 0;

    internal override Machine Machine => DeclaringCompilation.Options.Platform switch
    {
        Platform.Arm => Machine.ArmThumb2,
        Platform.X64 => Machine.Amd64,
        Platform.Arm64 => Machine.Arm64,
        Platform.Itanium => Machine.IA64,
        _ => Machine.I386,
    };

    internal override bool Bit32Required => DeclaringCompilation.Options.Platform == Platform.X86;

    #region
#warning 未实现
    internal override ModuleSymbol GlobalNamespace => throw new NotImplementedException();

    public override ModuleSymbol GlobalModule => throw new NotImplementedException();

    internal override bool IsMissing => throw new NotImplementedException();

    public override ImmutableArray<AssemblyIdentity> ReferencedAssemblies => throw new NotImplementedException();

    public override ImmutableArray<AssemblySymbol> ReferencedAssemblySymbols => throw new NotImplementedException();

    internal override bool HasUnifiedReferences => throw new NotImplementedException();

    public override ImmutableArray<Location> Locations => throw new NotImplementedException();

    public override bool AreLocalsZeroed => throw new NotImplementedException();

    internal override void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol? originatingSourceAssemblyDebugOnly = null)
    {
        throw new NotImplementedException();
    }

    internal override bool GetUnificationUseSiteDiagnostic(ref DiagnosticInfo result, TypeSymbol dependentType)
    {
        throw new NotImplementedException();
    }

    internal override NamedTypeSymbol? LookupTopLevelMetadataType(ref MetadataTypeName emittedName)
    {
        throw new NotImplementedException();
    }

    protected override ISymbol CreateISymbol()
    {
        throw new NotImplementedException();
    }

    public override ModuleMetadata GetMetadata()
    {
        throw new NotImplementedException();
    }
    #endregion
}
