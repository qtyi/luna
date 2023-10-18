// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class AssemblySymbol : IAssemblySymbol
{
    internal abstract InternalModel.Symbols.AssemblySymbol UnderlyingAssemblySymbol { get; }

    private bool GivesAccessTo(IAssemblySymbol assemblyWantingAccess)
    {
        if (object.Equals(this, assemblyWantingAccess))
            return true;

        return false;
    }

    private ImmutableArray<INamedTypeSymbol> GetForwardedTypes() =>
        this.UnderlyingAssemblySymbol.GetAllTopLevelForwardedTypes()
            .Select(SymbolExtensions.GetPublicSymbol)
            .OrderBy(static t => t!.ToDisplayString(Microsoft.CodeAnalysis.SymbolDisplayFormat.QualifiedNameArityFormat))
            .AsImmutable()!;

    #region Symbol
    /// <inheritdoc/>
    internal sealed override InternalModel.Symbol UnderlyingSymbol => this.UnderlyingAssemblySymbol;

    /// <inheritdoc/>
    protected sealed override void Accept(SymbolVisitor visitor) => visitor.VisitAssembly(this);
    /// <inheritdoc/>
    protected sealed override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitAssembly(this);
    /// <inheritdoc/>
    protected sealed override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitAssembly(this, argument);
    #endregion

    #region Microsoft.CodeAnalysis.IAssemblySymbol
    Microsoft.CodeAnalysis.INamespaceSymbol Microsoft.CodeAnalysis.IAssemblySymbol.GlobalNamespace => this.UnderlyingAssemblySymbol.GlobalModule.GetPublicSymbol();
    IEnumerable<Microsoft.CodeAnalysis.IModuleSymbol> Microsoft.CodeAnalysis.IAssemblySymbol.Modules => this.UnderlyingAssemblySymbol.Netmodules.Select(SymbolExtensions.GetPublicSymbol)!;

    bool Microsoft.CodeAnalysis.IAssemblySymbol.IsInteractive => this.UnderlyingAssemblySymbol.IsInteractive;

    Microsoft.CodeAnalysis.AssemblyIdentity Microsoft.CodeAnalysis.IAssemblySymbol.Identity => this.UnderlyingAssemblySymbol.Identity;

    ICollection<string> Microsoft.CodeAnalysis.IAssemblySymbol.TypeNames => this.UnderlyingAssemblySymbol.TypeNames;
    ICollection<string> Microsoft.CodeAnalysis.IAssemblySymbol.NamespaceNames => this.UnderlyingAssemblySymbol.NamespaceNames;

    bool Microsoft.CodeAnalysis.IAssemblySymbol.MightContainExtensionMethods => this.UnderlyingAssemblySymbol.MightContainExtensionMethods;

    Microsoft.CodeAnalysis.AssemblyMetadata Microsoft.CodeAnalysis.IAssemblySymbol.GetMetadata() => this.UnderlyingAssemblySymbol.GetMetadata();

    Microsoft.CodeAnalysis.INamedTypeSymbol? Microsoft.CodeAnalysis.IAssemblySymbol.ResolveForwardedType(string fullyQualifiedMetadataName) => this.UnderlyingAssemblySymbol.ResolveForwardedType(fullyQualifiedMetadataName).GetPublicSymbol();
    ImmutableArray<Microsoft.CodeAnalysis.INamedTypeSymbol> Microsoft.CodeAnalysis.IAssemblySymbol.GetForwardedTypes() => StaticCast<Microsoft.CodeAnalysis.INamedTypeSymbol>.From(this.GetForwardedTypes());

    bool Microsoft.CodeAnalysis.IAssemblySymbol.GivesAccessTo(Microsoft.CodeAnalysis.IAssemblySymbol assemblyWantingAccess) => this.GivesAccessTo((IAssemblySymbol)assemblyWantingAccess);

    Microsoft.CodeAnalysis.INamedTypeSymbol? Microsoft.CodeAnalysis.IAssemblySymbol.GetTypeByMetadataName(string metadataName) => this.UnderlyingAssemblySymbol.GetTypeByMetadataName(metadataName).GetPublicSymbol();
    #endregion

    #region IAssemblySymbol
    IModuleSymbol IAssemblySymbol.GlobalModule => this.UnderlyingAssemblySymbol.GlobalModule.GetPublicSymbol();
    IEnumerable<INetmoduleSymbol> IAssemblySymbol.Netmodules => this.UnderlyingAssemblySymbol.Netmodules.Select(SymbolExtensions.GetPublicSymbol)!;

    ICollection<string> IAssemblySymbol.ModuleNames => this.UnderlyingAssemblySymbol.ModuleNames;

    INamedTypeSymbol? IAssemblySymbol.ResolveForwardedType(string fullyQualifiedMetadataName) => this.UnderlyingAssemblySymbol.ResolveForwardedType(fullyQualifiedMetadataName).GetPublicSymbol();
    ImmutableArray<INamedTypeSymbol> IAssemblySymbol.GetForwardedTypes() => this.GetForwardedTypes();

    bool IAssemblySymbol.GivesAccessTo(IAssemblySymbol assemblyWantingAccess) => this.GivesAccessTo(assemblyWantingAccess);

    INamedTypeSymbol? IAssemblySymbol.GetTypeByMetadataName(string metadataName) => this.UnderlyingAssemblySymbol.GetTypeByMetadataName(metadataName).GetPublicSymbol();
    #endregion
}
