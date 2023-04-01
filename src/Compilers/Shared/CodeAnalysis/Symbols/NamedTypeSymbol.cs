// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class NamedTypeSymbol : INamedTypeSymbolInternal
{
    /// <inheritdoc cref="TypeSymbol()"/>
    internal NamedTypeSymbol() { }

    #region 包含关系
    public override NamedTypeSymbol? ContainingType => this.ContainingSymbol as NamedTypeSymbol;

    #endregion

    public virtual NamedTypeSymbol? EnumUnderlyingType => null;

    #region 公共符号
#warning 未完成
    protected override ISymbol CreateISymbol()
    {
        throw new NotImplementedException();
    }

    protected override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
    {
        throw new NotImplementedException();
    }
    #endregion

    #region INamedTypeSymbolInternal
#nullable disable
    INamedTypeSymbolInternal INamedTypeSymbolInternal.EnumUnderlyingType => this.EnumUnderlyingType;
#nullable enable
    #endregion
}
