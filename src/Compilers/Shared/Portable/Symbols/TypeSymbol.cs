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

/// <summary>
/// A TypeSymbol is a base class for all the symbols that represent a type.
/// </summary>
abstract partial class TypeSymbol : NamespaceOrTypeSymbol, ITypeSymbolInternal
{
    /// <inheritdoc cref="NamespaceOrTypeSymbol()"/>
    internal TypeSymbol() { }

    public virtual TypeKind TypeKind
    {
        get
        {
#warning Not implemented.
            throw new NotImplementedException();
        }
    }

    public virtual SpecialType SpecialType
    {
        get
        {
#warning Not implemented.
            throw new NotImplementedException();
        }
    }

    internal virtual PrimitiveTypeCode PrimitiveTypeCode
    {
        get
        {
#warning Not implemented.
            throw new NotImplementedException();
        }
    }

    public virtual bool IsReferenceType
    {
        get
        {
#warning Not implemented.
            throw new NotImplementedException();
        }
    }

    public virtual bool IsValueType
    {
        get
        {
#warning Not implemented.
            throw new NotImplementedException();
        }
    }

    #region OriginalDefinition
    /// <summary>
    /// 获取此类型符号的原始定义。
    /// </summary>
    /// <value>
    /// 此类型符号的原始定义。
    /// 若此类型符号是由另一个类型符号通过类型置换创建的，则返回其最原始的在源代码或元数据中的定义。
    /// </value>
    public new TypeSymbol OriginalDefinition => OriginalTypeSymbolDefinition;

    /// <summary>
    /// 由子类重写，获取此类型符号的原始定义。
    /// </summary>
    /// <value>
    /// <see cref="OriginalDefinition"/>使用此返回值。
    /// </value>
    protected virtual TypeSymbol OriginalTypeSymbolDefinition => this;

    protected sealed override Symbol OriginalSymbolDefinition => OriginalTypeSymbolDefinition;
    #endregion

    #region Equals
    internal virtual bool Equals(TypeSymbol? other, TypeCompareKind compareKind) => ReferenceEquals(this, other);

    public sealed override bool Equals(Symbol? other, TypeCompareKind compareKind) => other is TypeSymbol symbol && Equals(symbol, compareKind);

    public static bool Equals(TypeSymbol? first, TypeSymbol? second, TypeCompareKind compareKind)
    {
        if (first is null)
            return second is null;
        else if (ReferenceEquals(first, second))
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

    #region Public Symbol
    internal ITypeSymbol GetITypeSymbol(NullableAnnotation nullableAnnotation)
    {
        if (nullableAnnotation == DefaultNullableAnnotation)
            return (ITypeSymbol)this.ISymbol;

        return CreateITypeSymbol(nullableAnnotation);
    }

    internal NullableAnnotation DefaultNullableAnnotation => NullableAnnotation.None;

    protected abstract ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation);
    #endregion

    #region ITypeSymbolInternal
    ITypeSymbol ITypeSymbolInternal.GetITypeSymbol() => GetITypeSymbol(DefaultNullableAnnotation);
    #endregion
}
