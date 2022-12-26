// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

/// <summary>
/// 提供决定元数据标志和签名的方法的帮助类。
/// </summary>
internal partial class MetadataDecoder : Microsoft.CodeAnalysis.MetadataDecoder<PEModuleSymbol, TypeSymbol, PEMethodSymbol, PEFieldSymbol, Symbol>
{
    /// <summary>用于决定泛型类型参数的类型上下文。</summary>
    private readonly PENamedTypeSymbol? _typeContext;

    /// <summary>用于决定泛型类型参数的方法上下文。</summary>
    private readonly PEMethodSymbol? _methodContext;

    internal PEModuleSymbol ModuleSymbol => base.moduleSymbol;

    /// <summary>
    /// 使用类型上下文创建<see cref="MetadataDecoder"/>的新实例。
    /// </summary>
    /// <param name="moduleSymbol">元数据所处的PE模块符号。</param>
    /// <param name="context">元数据所处的类型上下文。</param>
    public MetadataDecoder(
        PEModuleSymbol moduleSymbol,
        PENamedTypeSymbol context)
        : this(moduleSymbol, context, null) { }

    /// <summary>
    /// 使用方法上下文创建<see cref="MetadataDecoder"/>的新实例。
    /// </summary>
    /// <param name="moduleSymbol">元数据所处的PE模块符号。</param>
    /// <param name="context">元数据所处的方法上下文。</param>
    public MetadataDecoder(
        PEModuleSymbol moduleSymbol,
        PEMethodSymbol context)
        : this(moduleSymbol, (PENamedTypeSymbol?)context.ContainingType, context) { }

    /// <summary>
    /// 创建<see cref="MetadataDecoder"/>的新实例。
    /// </summary>
    /// <param name="moduleSymbol">元数据所处的PE模块符号。</param>
    public MetadataDecoder(
        PEModuleSymbol moduleSymbol)
        : this(moduleSymbol, null, null) { }

    /// <summary>
    /// 通用构造函数。
    /// </summary>
    /// <param name="moduleSymbol">元数据所处的PE模块符号。</param>
    /// <param name="typeContext">元数据所处的类型上下文。</param>
    /// <param name="methodContext">元数据所处的方法上下文。</param>
    private MetadataDecoder(
        PEModuleSymbol moduleSymbol,
        PENamedTypeSymbol? typeContext,
        PEMethodSymbol? methodContext)
        : base(
            moduleSymbol.Module,
            moduleSymbol.ContainingAssembly.Identity,
            SymbolFactory.Instance,
            moduleSymbol)
    {
        this._typeContext = typeContext;
        this._methodContext = methodContext;
    }

    protected override TypeSymbol GetGenericMethodTypeParamSymbol(int position)
    {
        if (this._methodContext is null)
            return new UnsupportedMetadataTypeSymbol(); // 类型参数不与方法关联。

        Debug.Assert(position >= 0);

        var typeParameters = this._methodContext.TypeParameters;
        if (typeParameters.Length <= position)
            return new UnsupportedMetadataTypeSymbol(); // 类型参数的位置过大。

        return typeParameters[position];
    }

    protected override TypeSymbol GetGenericTypeParamSymbol(int position)
    {
        var type = this._typeContext;
        while (type is not null && (type.MetadataArity - type.Arity) > position)
            type = type.ContainingSymbol as PENamedTypeSymbol;

        if (type is null || type.MetadataArity <= position)
            return new UnsupportedMetadataTypeSymbol(); // 类型参数的位置过大。

        position -= type.MetadataArity - type.Arity;
        Debug.Assert(position >= 0 && position < type.Arity);

        return type.TypeParameters[position];
    }
}
