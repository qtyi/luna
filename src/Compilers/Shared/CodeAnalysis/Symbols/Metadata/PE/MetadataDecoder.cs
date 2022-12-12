// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

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

    public MetadataDecoder(
        PEModuleSymbol moduleSymbol,
        PENamedTypeSymbol context)
        : this(moduleSymbol, context, null) { }

    public MetadataDecoder(
        PEModuleSymbol moduleSymbol,
        PEMethodSymbol context)
        : this(moduleSymbol, (PENamedTypeSymbol?)context.ContainingType, context) { }

    public MetadataDecoder(
        PEModuleSymbol moduleSymbol)
        : this(moduleSymbol, null, null) { }

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
            return new 
    }
}
