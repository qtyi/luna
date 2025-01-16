// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

internal sealed partial class PEAssemblySymbol : MetadataOrSourceAssemblySymbol
{
    #region 未实现
#warning 未实现
    public override AssemblyIdentity Identity => throw new NotImplementedException();

    public override Version? AssemblyVersionPattern => throw new NotImplementedException();

    internal override ModuleSymbol GlobalNamespace => throw new NotImplementedException();

    public override ModuleSymbol GlobalModule => throw new NotImplementedException();

    public override ImmutableArray<NetmoduleSymbol> Netmodules => throw new NotImplementedException();

    public override ICollection<string> TypeNames => throw new NotImplementedException();

    public override ICollection<string> ModuleNames => throw new NotImplementedException();

    public override bool MightContainExtensionMethods => throw new NotImplementedException();

    public override ImmutableArray<Location> Locations => throw new NotImplementedException();

    internal override bool IsMissing => throw new NotImplementedException();

    internal override ImmutableArray<byte> PublicKey => throw new NotImplementedException();

    internal override ICollection<string> NamespaceNames => throw new NotImplementedException();

    public override void Accept(ThisSymbolVisitor visitor)
    {
        throw new NotImplementedException();
    }

    public override TResult? Accept<TResult>(ThisSymbolVisitor<TResult> visitor) where TResult : default
    {
        throw new NotImplementedException();
    }

    protected override ISymbol CreateISymbol()
    {
        throw new NotImplementedException();
    }

    internal override TResult? Accept<TArgument, TResult>(ThisSymbolVisitor<TArgument, TResult> visitor, TArgument argument) where TResult : default
    {
        throw new NotImplementedException();
    }

    internal override IEnumerable<NamedTypeSymbol> GetAllTopLevelForwardedTypes()
    {
        throw new NotImplementedException();
    }

    internal override NamedTypeSymbol LookupDeclaredOrForwardedTopLevelMetadataType(ref MetadataTypeName emittedName, ConsList<AssemblySymbol>? visitedAssemblies)
    {
        throw new NotImplementedException();
    }

    internal override NamedTypeSymbol? LookupDeclaredTopLevelMetadataType(ref MetadataTypeName emittedName)
    {
        throw new NotImplementedException();
    }

    public override AssemblyMetadata GetMetadata()
    {
        throw new NotImplementedException();
    }
    #endregion
}
