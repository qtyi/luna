// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class ModuleSymbol
{
    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.TypeKind"/>.
    /// </summary>
    protected abstract Microsoft.CodeAnalysis.TypeKind TypeKindCore { get; }

    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.SpecialType"/>.
    /// </summary>
    protected virtual Microsoft.CodeAnalysis.SpecialType SpecialTypeCore => Microsoft.CodeAnalysis.SpecialType.None;

    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.IsReferenceType"/>.
    /// </summary>
    protected abstract bool IsReferenceTypeCore { get; }

    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.IsValueType"/>.
    /// </summary>
    protected abstract bool IsValueTypeCore { get; }

    /// <summary>
    /// Backing property for <see cref="Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.GetITypeSymbol()"/>.
    /// </summary>
    protected abstract Microsoft.CodeAnalysis.ITypeSymbol GetITypeSymbolCore();

    #region Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal
    Microsoft.CodeAnalysis.TypeKind Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.TypeKind => this.TypeKindCore;
    Microsoft.CodeAnalysis.SpecialType Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.SpecialType => this.SpecialTypeCore;
    bool Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.IsReferenceType => this.IsReferenceTypeCore;
    bool Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.IsValueType => this.IsValueTypeCore;
    Microsoft.CodeAnalysis.ITypeSymbol Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.GetITypeSymbol() => this.GetITypeSymbolCore();
    #endregion
}
