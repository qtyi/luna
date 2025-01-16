// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Symbols;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class FieldSymbol : ModuleSymbol, IFieldSymbolInternal
{
    public sealed override bool IsGlobalModule => false;

    public abstract TypeWithAnnotations TypeWithAnnotations { get; }

    public TypeSymbol Type => TypeWithAnnotations.Type;

    #region 定义
    /// <summary>
    /// 获取此类型符号的原始定义。
    /// </summary>
    /// <value>
    /// 此类型符号的原始定义。
    /// 若此类型符号是由另一个类型符号通过类型置换创建的，则返回其最原始的在源代码或元数据中的定义。
    /// </value>
    public new FieldSymbol OriginalDefinition => OriginalFieldSymbolDefinition;

    /// <summary>
    /// 由子类重写，获取此类型符号的原始定义。
    /// </summary>
    /// <value>
    /// <see cref="OriginalDefinition"/>使用此返回值。
    /// </value>
    protected abstract FieldSymbol OriginalFieldSymbolDefinition { get; }

    protected sealed override Symbol OriginalSymbolDefinition => OriginalFieldSymbolDefinition;
    #endregion

    #region GetMembers
    /// <inheritdoc/>
    public override ImmutableArray<ModuleSymbol> GetMembers() => GetFieldMembers().Cast<FieldSymbol, ModuleSymbol>();

    /// <inheritdoc/>
    public override ImmutableArray<ModuleSymbol> GetMembers(string name) => GetFieldMembers(name).Cast<FieldSymbol, ModuleSymbol>();

    /// <inheritdoc/>
    internal sealed override ImmutableArray<ModuleSymbol> GetNamespaceMembers() => ImmutableArray<ModuleSymbol>.Empty;

    /// <inheritdoc/>
    internal sealed override ImmutableArray<ModuleSymbol> GetNamespaceMembers(string name) => ImmutableArray<ModuleSymbol>.Empty;

    /// <inheritdoc/>
    public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers() => ImmutableArray<NamedTypeSymbol>.Empty;

    /// <inheritdoc/>
    public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name) => ImmutableArray<NamedTypeSymbol>.Empty;

    /// <inheritdoc/>
    public sealed override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity) => ImmutableArray<NamedTypeSymbol>.Empty;
    #endregion

    #region ModuleSymbol_Event
    #endregion

    #region ModuleSymbol_Type
    /// <inheritdoc/>
    protected sealed override TypeKind TypeKindCore => TypeKind.Unknown;

    /// <inheritdoc/>
    protected sealed override SpecialType SpecialTypeCore => throw ExceptionUtilities.Unreachable();

    /// <inheritdoc/>
    protected sealed override bool IsReferenceTypeCore => throw ExceptionUtilities.Unreachable();

    /// <inheritdoc/>
    protected sealed override bool IsValueTypeCore => throw ExceptionUtilities.Unreachable();

    /// <inheritdoc/>
    protected sealed override ITypeSymbol GetITypeSymbolCore() => throw ExceptionUtilities.Unreachable();
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

}
