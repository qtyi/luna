// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

abstract partial class NamedTypeSymbol : TypeSymbol, INamedTypeSymbolInternal
{
    /// <inheritdoc cref="TypeSymbol()"/>
    internal NamedTypeSymbol() { }

    public abstract int Arity { get; }

    public virtual NamedTypeSymbol? EnumUnderlyingType => null;

    #region Public Symbol
    protected override ISymbol CreateISymbol()
    {
#warning Not implemented.
        throw new NotImplementedException();
    }

    protected override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }
    #endregion

    #region INamedTypeSymbolInternal
#nullable disable
    INamedTypeSymbolInternal INamedTypeSymbolInternal.EnumUnderlyingType => EnumUnderlyingType;

    ImmutableArray<ISymbolInternal> INamedTypeSymbolInternal.GetMembers()
    {
#warning Not implemented.
        throw new NotImplementedException();
    }

    ImmutableArray<ISymbolInternal> INamedTypeSymbolInternal.GetMembers(string name)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }
#nullable enable
    #endregion
}
