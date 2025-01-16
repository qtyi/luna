// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;
#endif

internal abstract partial class AssemblySymbol : Symbol, IAssemblySymbol
{
    internal abstract Symbols.AssemblySymbol UnderlyingAssemblySymbol { get; }

#warning Not implemented.
    bool IAssemblySymbol.IsInteractive => throw new NotImplementedException();

    AssemblyIdentity IAssemblySymbol.Identity => throw new NotImplementedException();

    INamespaceSymbol IAssemblySymbol.GlobalNamespace => throw new NotImplementedException();

    IEnumerable<IModuleSymbol> IAssemblySymbol.Modules => throw new NotImplementedException();

    ICollection<string> IAssemblySymbol.TypeNames => throw new NotImplementedException();

    ICollection<string> IAssemblySymbol.NamespaceNames => throw new NotImplementedException();

    bool IAssemblySymbol.MightContainExtensionMethods => throw new NotImplementedException();

    ImmutableArray<INamedTypeSymbol> IAssemblySymbol.GetForwardedTypes()
    {
        throw new NotImplementedException();
    }

    AssemblyMetadata? IAssemblySymbol.GetMetadata()
    {
        throw new NotImplementedException();
    }

    INamedTypeSymbol? IAssemblySymbol.GetTypeByMetadataName(string fullyQualifiedMetadataName)
    {
        throw new NotImplementedException();
    }

    bool IAssemblySymbol.GivesAccessTo(IAssemblySymbol toAssembly)
    {
        throw new NotImplementedException();
    }

    INamedTypeSymbol? IAssemblySymbol.ResolveForwardedType(string fullyQualifiedMetadataName)
    {
        throw new NotImplementedException();
    }
}
