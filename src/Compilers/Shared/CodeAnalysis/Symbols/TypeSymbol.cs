// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Cci;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class TypeSymbol : ITypeSymbolInternal
{
    /// <inheritdoc cref="ModuleSymbol()"/>
    internal TypeSymbol() { }

    public abstract TypeKind TypeKind { get; }

    public virtual SpecialType SpecialType => SpecialType.None;

    internal PrimitiveTypeCode PrimitiveTypeCode =>
        this.TypeKind switch
        {
            TypeKind.Pointer => PrimitiveTypeCode.Pointer,
            TypeKind.FunctionPointer => PrimitiveTypeCode.FunctionPointer,
            _ => SpecialTypes.GetTypeCode(SpecialType)
        };

    public abstract bool IsReferenceType { get; }

    public abstract bool IsValueType { get; }

    public abstract bool IsRefLikeType { get; }

    public abstract bool IsReadOnly { get; }

    #region 定义
    /// <summary>
    /// 获取此类型符号的原始定义。
    /// </summary>
    /// <value>
    /// 此类型符号的原始定义。
    /// 若此类型符号是由另一个类型符号通过类型置换创建的，则返回其最原始的在源代码或元数据中的定义。
    /// </value>
    public new TypeSymbol OriginalDefinition => this.OriginalTypeSymbolDefinition;

    /// <summary>
    /// 由子类重写，获取此类型符号的原始定义。
    /// </summary>
    /// <value>
    /// <see cref="OriginalDefinition"/>使用此返回值。
    /// </value>
    protected abstract TypeSymbol OriginalTypeSymbolDefinition { get; }

    protected sealed override Symbol OriginalSymbolDefinition => this.OriginalTypeSymbolDefinition;
    #endregion

    #region 相等性
    internal virtual bool Equals(TypeSymbol? other, TypeCompareKind compareKind) => object.ReferenceEquals(this, other);

    public sealed override bool Equals(Symbol? other, TypeCompareKind compareKind) => other is TypeSymbol symbol && this.Equals(symbol, compareKind);

    public static bool Equals(TypeSymbol? first, TypeSymbol? second, TypeCompareKind compareKind)
    {
        if (first is null)
            return second is null;
        else if (object.ReferenceEquals(first, second))
            return true;
        else
            return first.Equals(second, compareKind);
    }
    #endregion

    /// <summary>
    /// 与另一个类型符号合并产生新的类型符号，新类型符号具有两者的信息总和。
    /// </summary>
    /// <param name="other"></param>
    /// <param name="variance"></param>
    /// <returns></returns>
    internal abstract TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance);

    #region 公共符号
    internal ITypeSymbol GetITypeSymbol(NullableAnnotation nullableAnnotation)
    {
        if (nullableAnnotation == DefaultNullableAnnotation)
            return (ITypeSymbol)this.ISymbol;

        return this.CreateITypeSymbol(nullableAnnotation);
    }

    internal NullableAnnotation DefaultNullableAnnotation => NullableAnnotation.None;

    protected abstract ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation);

    ITypeSymbol ITypeSymbolInternal.GetITypeSymbol() => this.GetITypeSymbol(this.DefaultNullableAnnotation);
    #endregion
}
