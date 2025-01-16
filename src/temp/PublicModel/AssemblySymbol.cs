// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class AssemblySymbol : IAssemblySymbol
{
    internal abstract Symbols.AssemblySymbol UnderlyingAssemblySymbol { get; }

    private bool GivesAccessTo(IAssemblySymbol assemblyWantingAccess)
    {
        if (Equals(this, assemblyWantingAccess))
            return true;

        return false;
    }

    private ImmutableArray<INamedTypeSymbol> GetForwardedTypes() =>
        UnderlyingAssemblySymbol.GetAllTopLevelForwardedTypes()
            .Select(SymbolExtensions.GetPublicSymbol)
            .OrderBy(static t => t!.ToDisplayString(SymbolDisplayFormat.QualifiedNameArityFormat))
            .AsImmutable()!;

    #region Symbol
    /// <inheritdoc/>
    internal sealed override InternalModel.Symbol UnderlyingSymbol => UnderlyingAssemblySymbol;

    /// <inheritdoc/>
    protected sealed override void Accept(SymbolVisitor visitor) => visitor.VisitAssembly(this);
    /// <inheritdoc/>
    protected sealed override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitAssembly(this);
    /// <inheritdoc/>
    protected sealed override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitAssembly(this, argument);
    #endregion

    #region Microsoft.CodeAnalysis.IAssemblySymbol
    INamespaceSymbol IAssemblySymbol.GlobalNamespace => UnderlyingAssemblySymbol.GlobalModule.GetPublicSymbol();
    IEnumerable<IModuleSymbol> IAssemblySymbol.Modules => UnderlyingAssemblySymbol.Netmodules.Select(SymbolExtensions.GetPublicSymbol)!;

    bool IAssemblySymbol.IsInteractive => UnderlyingAssemblySymbol.IsInteractive;

    AssemblyIdentity IAssemblySymbol.Identity => UnderlyingAssemblySymbol.Identity;

    ICollection<string> IAssemblySymbol.TypeNames => UnderlyingAssemblySymbol.TypeNames;
    ICollection<string> IAssemblySymbol.NamespaceNames => UnderlyingAssemblySymbol.NamespaceNames;

    bool IAssemblySymbol.MightContainExtensionMethods => UnderlyingAssemblySymbol.MightContainExtensionMethods;

    AssemblyMetadata IAssemblySymbol.GetMetadata() => UnderlyingAssemblySymbol.GetMetadata();

    INamedTypeSymbol? IAssemblySymbol.ResolveForwardedType(string fullyQualifiedMetadataName) => UnderlyingAssemblySymbol.ResolveForwardedType(fullyQualifiedMetadataName).GetPublicSymbol();
    ImmutableArray<INamedTypeSymbol> IAssemblySymbol.GetForwardedTypes() => StaticCast<INamedTypeSymbol>.From(GetForwardedTypes());

    bool IAssemblySymbol.GivesAccessTo(IAssemblySymbol assemblyWantingAccess) => GivesAccessTo((IAssemblySymbol)assemblyWantingAccess);

    INamedTypeSymbol? IAssemblySymbol.GetTypeByMetadataName(string metadataName) => UnderlyingAssemblySymbol.GetTypeByMetadataName(metadataName).GetPublicSymbol();
    #endregion

    #region IAssemblySymbol
    IModuleSymbol IAssemblySymbol.GlobalModule => UnderlyingAssemblySymbol.GlobalModule.GetPublicSymbol();
    IEnumerable<IModuleSymbol> IAssemblySymbol.Netmodules => UnderlyingAssemblySymbol.Netmodules.Select(SymbolExtensions.GetPublicSymbol)!;

    ICollection<string> IAssemblySymbol.ModuleNames => UnderlyingAssemblySymbol.ModuleNames;

    INamedTypeSymbol? IAssemblySymbol.ResolveForwardedType(string fullyQualifiedMetadataName) => UnderlyingAssemblySymbol.ResolveForwardedType(fullyQualifiedMetadataName).GetPublicSymbol();
    ImmutableArray<INamedTypeSymbol> IAssemblySymbol.GetForwardedTypes() => GetForwardedTypes();

    bool IAssemblySymbol.GivesAccessTo(IAssemblySymbol assemblyWantingAccess) => GivesAccessTo(assemblyWantingAccess);

    INamedTypeSymbol? IAssemblySymbol.GetTypeByMetadataName(string metadataName) => UnderlyingAssemblySymbol.GetTypeByMetadataName(metadataName).GetPublicSymbol();
    #endregion
}
