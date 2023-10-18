// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

internal sealed class SymbolFactory : SymbolFactory<PENetmoduleSymbol, TypeSymbol>
{
    /// <summary>
    /// 符号工厂的实例。
    /// </summary>
    internal static readonly SymbolFactory Instance = new();

    #region 未完成
#warning 未完成
    internal override TypeSymbol GetEnumUnderlyingType(PENetmoduleSymbol moduleSymbol, TypeSymbol type)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol GetMDArrayTypeSymbol(PENetmoduleSymbol moduleSymbol, int rank, TypeSymbol elementType, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers, ImmutableArray<int> sizes, ImmutableArray<int> lowerBounds)
    {
        throw new NotImplementedException();
    }

    internal override PrimitiveTypeCode GetPrimitiveTypeCode(PENetmoduleSymbol moduleSymbol, TypeSymbol type)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol GetSpecialType(PENetmoduleSymbol moduleSymbol, SpecialType specialType)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol GetSystemTypeSymbol(PENetmoduleSymbol moduleSymbol)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol GetSZArrayTypeSymbol(PENetmoduleSymbol moduleSymbol, TypeSymbol elementType, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol GetUnsupportedMetadataTypeSymbol(PENetmoduleSymbol moduleSymbol, BadImageFormatException exception)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol MakeFunctionPointerTypeSymbol(PENetmoduleSymbol moduleSymbol, CallingConvention callingConvention, ImmutableArray<ParamInfo<TypeSymbol>> returnAndParamTypes)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol MakePointerTypeSymbol(PENetmoduleSymbol moduleSymbol, TypeSymbol type, ImmutableArray<ModifierInfo<TypeSymbol>> customModifiers)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol MakeUnboundIfGeneric(PENetmoduleSymbol moduleSymbol, TypeSymbol type)
    {
        throw new NotImplementedException();
    }

    internal override TypeSymbol SubstituteTypeParameters(PENetmoduleSymbol moduleSymbol, TypeSymbol generic, ImmutableArray<KeyValuePair<TypeSymbol, ImmutableArray<ModifierInfo<TypeSymbol>>>> arguments, ImmutableArray<bool> refersToNoPiaLocalType)
    {
        throw new NotImplementedException();
    }
    #endregion
}
