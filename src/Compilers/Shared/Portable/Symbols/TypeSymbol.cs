// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.Cci;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Symbols;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class TypeSymbol : ITypeSymbolInternal
{
    /// <inheritdoc cref="ModuleSymbol()"/>
    internal TypeSymbol() { }

    public TypeKind TypeKind => this.TypeKindCore switch
    {
        Microsoft.CodeAnalysis.TypeKind.Unknown => TypeKind.Unknown,
        Microsoft.CodeAnalysis.TypeKind.Array => TypeKind.Array,
        Microsoft.CodeAnalysis.TypeKind.Dynamic => TypeKind.Dynamic,
        Microsoft.CodeAnalysis.TypeKind.Error => TypeKind.Error,

        Microsoft.CodeAnalysis.TypeKind.Class or
        Microsoft.CodeAnalysis.TypeKind.Delegate or
        Microsoft.CodeAnalysis.TypeKind.Enum or
        Microsoft.CodeAnalysis.TypeKind.Interface or
        Microsoft.CodeAnalysis.TypeKind.Struct or
        Microsoft.CodeAnalysis.TypeKind.Structure => TypeKind.NamedType,

        _ => TypeKind.Unknown
    };

    public SpecialType SpecialType => this.SpecialTypeCore;

    internal PrimitiveTypeCode PrimitiveTypeCode =>
        this.TypeKindCore switch
        {
            Microsoft.CodeAnalysis.TypeKind.Pointer => PrimitiveTypeCode.Pointer,
            Microsoft.CodeAnalysis.TypeKind.FunctionPointer => PrimitiveTypeCode.FunctionPointer,
            _ => SpecialTypes.GetTypeCode(SpecialType)
        };

    public bool IsReferenceType => this.IsReferenceTypeCore;

    public bool IsValueType => this.IsValueTypeCore;

    public sealed override bool IsGlobalModule => false;

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
    protected virtual TypeSymbol OriginalTypeSymbolDefinition => this;

    protected sealed override Symbol OriginalSymbolDefinition => this.OriginalTypeSymbolDefinition;
    #endregion

    #region 相等性
    internal virtual bool Equals(TypeSymbol? other, TypeCompareKind compareKind) => ReferenceEquals(this, other);

    public sealed override bool Equals(Symbol? other, TypeCompareKind compareKind) => other is TypeSymbol symbol && this.Equals(symbol, compareKind);

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

    #region GetMembers
    /// <inheritdoc/>
    internal sealed override ImmutableArray<ModuleSymbol> GetNamespaceMembers() => ImmutableArray<ModuleSymbol>.Empty;

    /// <inheritdoc/>
    internal sealed override ImmutableArray<ModuleSymbol> GetNamespaceMembers(string name) => ImmutableArray<ModuleSymbol>.Empty;
    #endregion

    #region Public Symbol
    protected sealed override Microsoft.CodeAnalysis.ITypeSymbol GetITypeSymbolCore() => this.GetITypeSymbol(this.DefaultNullableAnnotation);

    internal ITypeSymbol GetITypeSymbol(NullableAnnotation nullableAnnotation)
    {
        if (nullableAnnotation == this.DefaultNullableAnnotation)
            return (ITypeSymbol)this.ISymbol;

        return this.CreateITypeSymbol(nullableAnnotation);
    }

    internal NullableAnnotation DefaultNullableAnnotation => NullableAnnotation.None;

    protected abstract ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation);
    #endregion

    #region ModuleSymbol_Event
    #endregion

    #region ModuleSymbol_Field
    /// <inheritdoc/>
    protected sealed override bool IsVolatileCore => throw ExceptionUtilities.Unreachable();
    #endregion

    #region ModuleSymbol_Method
    /// <inheritdoc/>
    protected sealed override bool IsIteratorCore => throw ExceptionUtilities.Unreachable();

    /// <inheritdoc/>
    protected sealed override bool IsAsyncCore => throw ExceptionUtilities.Unreachable();

    /// <inheritdoc/>
    protected sealed override int CalculateLocalSyntaxOffsetCore(int declaratorPosition, SyntaxTree declaratorTree) => throw ExceptionUtilities.Unreachable();

    /// <inheritdoc/>
    protected sealed override Microsoft.CodeAnalysis.Symbols.IMethodSymbolInternal ConstructCore(params Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal[] typeArguments) => throw ExceptionUtilities.Unreachable();
    #endregion

    #region ModuleSymbol_Property
    #endregion

    #region ITypeSymbolInternal
    ITypeSymbol ITypeSymbolInternal.GetITypeSymbol() => this.GetITypeSymbol(this.DefaultNullableAnnotation);
    #endregion

    #region Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal
    Microsoft.CodeAnalysis.TypeKind Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.TypeKind => this.TypeKindCore;

    Microsoft.CodeAnalysis.ITypeSymbol Microsoft.CodeAnalysis.Symbols.ITypeSymbolInternal.GetITypeSymbol() => this.GetITypeSymbol(this.DefaultNullableAnnotation);
    #endregion
}
