// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using NetmoduleMetadata = Microsoft.CodeAnalysis.ModuleMetadata;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class NetmoduleSymbol : IModuleSymbol
{
    private readonly Symbols.NetmoduleSymbol _underlying;

    internal override InternalModel.Symbol UnderlyingSymbol => _underlying;

    #region Accept
    protected override void Accept(SymbolVisitor visitor) => visitor.VisitModule(this);

    protected override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitNetmodule(this);

    protected override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitNetmodule(this, argument);
    #endregion

    public NetmoduleSymbol(Symbols.NetmoduleSymbol underlying)
    {
        Debug.Assert(underlying is not null);
        _underlying = underlying;
    }

    #region Microsoft.CodeAnalysis.IModuleSymbol
    INamespaceSymbol IModuleSymbol.GlobalNamespace => _underlying.GlobalNamespace.GetPublicSymbol();

    INamespaceSymbol? IModuleSymbol.GetModuleNamespace(INamespaceSymbol namespaceSymbol) => _underlying.GetNetmoduleNamespace((IModuleSymbol)namespaceSymbol).GetPublicSymbol();

    NetmoduleMetadata? IModuleSymbol.GetMetadata() => _underlying.GetMetadata();

    ImmutableArray<AssemblyIdentity> IModuleSymbol.ReferencedAssemblies => _underlying.ReferencedAssemblies;

    ImmutableArray<IAssemblySymbol> IModuleSymbol.ReferencedAssemblySymbols => _underlying.ReferencedAssemblySymbols.GetPublicSymbols().Cast<IAssemblySymbol, IAssemblySymbol>();
    #endregion

    #region INetmoduleSymbol
    IModuleSymbol IModuleSymbol.GlobalModule => _underlying.GlobalModule.GetPublicSymbol();

    ImmutableArray<IAssemblySymbol> IModuleSymbol.ReferencedAssemblySymbols => _underlying.ReferencedAssemblySymbols.GetPublicSymbols();

    IModuleSymbol? IModuleSymbol.GetNetmoduleModule(IModuleSymbol moduleSymbol) => _underlying.GetNetmoduleModule(moduleSymbol).GetPublicSymbol();
    #endregion
}
