// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using NetmoduleMetadata = Microsoft.CodeAnalysis.ModuleMetadata;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class NetmoduleSymbol : INetmoduleSymbol
{
    private readonly InternalModel.Symbols.NetmoduleSymbol _underlying;

    internal override InternalModel.Symbol UnderlyingSymbol => this._underlying;

    #region Accept
    protected override void Accept(SymbolVisitor visitor) => visitor.VisitNetmodule(this);

    protected override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitNetmodule(this);

    protected override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitNetmodule(this, argument);
    #endregion

    public NetmoduleSymbol(InternalModel.Symbols.NetmoduleSymbol underlying)
    {
        Debug.Assert(underlying is not null);
        this._underlying = underlying;
    }

    #region Microsoft.CodeAnalysis.IModuleSymbol
    Microsoft.CodeAnalysis.INamespaceSymbol Microsoft.CodeAnalysis.IModuleSymbol.GlobalNamespace => this._underlying.GlobalNamespace.GetPublicSymbol();

    Microsoft.CodeAnalysis.INamespaceSymbol? Microsoft.CodeAnalysis.IModuleSymbol.GetModuleNamespace(Microsoft.CodeAnalysis.INamespaceSymbol namespaceSymbol) => this._underlying.GetNetmoduleNamespace((IModuleSymbol)namespaceSymbol).GetPublicSymbol();

    NetmoduleMetadata? Microsoft.CodeAnalysis.IModuleSymbol.GetMetadata() => this._underlying.GetMetadata();

    ImmutableArray<Microsoft.CodeAnalysis.AssemblyIdentity> Microsoft.CodeAnalysis.IModuleSymbol.ReferencedAssemblies => this._underlying.ReferencedAssemblies;

    ImmutableArray<Microsoft.CodeAnalysis.IAssemblySymbol> Microsoft.CodeAnalysis.IModuleSymbol.ReferencedAssemblySymbols => this._underlying.ReferencedAssemblySymbols.GetPublicSymbols().Cast<IAssemblySymbol, Microsoft.CodeAnalysis.IAssemblySymbol>();
    #endregion

    #region INetmoduleSymbol
    IModuleSymbol INetmoduleSymbol.GlobalModule => this._underlying.GlobalModule.GetPublicSymbol();

    ImmutableArray<IAssemblySymbol> INetmoduleSymbol.ReferencedAssemblySymbols => this._underlying.ReferencedAssemblySymbols.GetPublicSymbols();

    IModuleSymbol? INetmoduleSymbol.GetNetmoduleModule(IModuleSymbol moduleSymbol) => this._underlying.GetNetmoduleModule(moduleSymbol).GetPublicSymbol();
    #endregion
}
