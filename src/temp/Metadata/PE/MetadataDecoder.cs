// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

/// <summary>
/// 提供决定元数据标记和签名的方法的帮助类。
/// </summary>
internal partial class MetadataDecoder : MetadataDecoder<PENetmoduleSymbol, TypeSymbol, PEMethodSymbol, PEFieldSymbol, Symbol>
{
    /// <summary>用于决定泛型类型参数的类型上下文。</summary>
    private readonly PENamedTypeSymbol? _typeContext;

    /// <summary>用于决定泛型类型参数的方法上下文。</summary>
    private readonly PEMethodSymbol? _methodContext;

    internal PENetmoduleSymbol ModuleSymbol => moduleSymbol;

    /// <summary>
    /// 使用类型上下文创建<see cref="MetadataDecoder"/>的新实例。
    /// </summary>
    /// <param name="moduleSymbol">元数据所处的PE模块符号。</param>
    /// <param name="context">元数据所处的类型上下文。</param>
    public MetadataDecoder(
        PENetmoduleSymbol moduleSymbol,
        PENamedTypeSymbol context)
        : this(moduleSymbol, context, null) { }

    /// <summary>
    /// 使用方法上下文创建<see cref="MetadataDecoder"/>的新实例。
    /// </summary>
    /// <param name="moduleSymbol">元数据所处的PE模块符号。</param>
    /// <param name="context">元数据所处的方法上下文。</param>
    public MetadataDecoder(
        PENetmoduleSymbol moduleSymbol,
        PEMethodSymbol context)
        : this(moduleSymbol, (PENamedTypeSymbol?)context.ContainingType, context) { }

    /// <summary>
    /// 创建<see cref="MetadataDecoder"/>的新实例。
    /// </summary>
    /// <param name="moduleSymbol">元数据所处的PE模块符号。</param>
    public MetadataDecoder(
        PENetmoduleSymbol moduleSymbol)
        : this(moduleSymbol, null, null) { }

    /// <summary>
    /// 通用构造函数。
    /// </summary>
    /// <param name="moduleSymbol">元数据所处的PE模块符号。</param>
    /// <param name="typeContext">元数据所处的类型上下文。</param>
    /// <param name="methodContext">元数据所处的方法上下文。</param>
    private MetadataDecoder(
        PENetmoduleSymbol moduleSymbol,
        PENamedTypeSymbol? typeContext,
        PEMethodSymbol? methodContext)
        : base(
            moduleSymbol.Module,
            moduleSymbol.ContainingAssembly.Identity,
            SymbolFactory.Instance,
            moduleSymbol)
    {
        _typeContext = typeContext;
        _methodContext = methodContext;
    }

    protected override TypeSymbol GetGenericMethodTypeParamSymbol(int position)
    {
        if (_methodContext is null)
            return new UnsupportedMetadataTypeSymbol(); // 类型参数不与方法关联。

        Debug.Assert(position >= 0);

        var typeParameters = _methodContext.TypeParameters;
        if (typeParameters.Length <= position)
            return new UnsupportedMetadataTypeSymbol(); // 类型参数的位置过大。

        return typeParameters[position];
    }

    protected override TypeSymbol GetGenericTypeParamSymbol(int position)
    {
        var type = _typeContext;
        while (type is not null && (type.MetadataArity - type.Arity) > position)
            type = type.ContainingSymbol as PENamedTypeSymbol;

        if (type is null || type.MetadataArity <= position)
            return new UnsupportedMetadataTypeSymbol(); // 类型参数的位置过大。

        position -= type.MetadataArity - type.Arity;
        Debug.Assert(position >= 0 && position < type.Arity);

        return type.TypeParameters[position];
    }
}

partial class MetadataDecoder
{
#warning 未实现
    protected override void EnqueueTypeSymbol(Queue<TypeDefinitionHandle> typeDefsToSearch, Queue<TypeSymbol> typeSymbolsToSearch, TypeSymbol typeSymbol)
    {
        throw new NotImplementedException();
    }

    protected override void EnqueueTypeSymbolInterfacesAndBaseTypes(Queue<TypeDefinitionHandle> typeDefsToSearch, Queue<TypeSymbol> typeSymbolsToSearch, TypeSymbol typeSymbol)
    {
        throw new NotImplementedException();
    }

    protected override PEFieldSymbol FindFieldSymbolInType(TypeSymbol type, FieldDefinitionHandle fieldDef)
    {
        throw new NotImplementedException();
    }

    protected override PEMethodSymbol FindMethodSymbolInType(TypeSymbol type, MethodDefinitionHandle methodDef)
    {
        throw new NotImplementedException();
    }

    protected override int GetIndexOfReferencedAssembly(AssemblyIdentity identity)
    {
        throw new NotImplementedException();
    }

    protected override MethodDefinitionHandle GetMethodHandle(PEMethodSymbol method)
    {
        throw new NotImplementedException();
    }

    protected override ConcurrentDictionary<TypeDefinitionHandle, TypeSymbol> GetTypeHandleToTypeMap()
    {
        throw new NotImplementedException();
    }

    protected override ConcurrentDictionary<TypeReferenceHandle, TypeSymbol> GetTypeRefHandleToTypeMap()
    {
        throw new NotImplementedException();
    }

    protected override bool IsContainingAssembly(AssemblyIdentity identity)
    {
        return base.IsContainingAssembly(identity);
    }

    protected override TypeSymbol LookupNestedTypeDefSymbol(TypeSymbol container, ref MetadataTypeName emittedName)
    {
        throw new NotImplementedException();
    }

    protected override TypeSymbol LookupTopLevelTypeDefSymbol(ref MetadataTypeName emittedName, out bool isNoPiaLocalType)
    {
        throw new NotImplementedException();
    }

    protected override TypeSymbol LookupTopLevelTypeDefSymbol(int referencedAssemblyIndex, ref MetadataTypeName emittedName)
    {
        throw new NotImplementedException();
    }

    protected override TypeSymbol LookupTopLevelTypeDefSymbol(string moduleName, ref MetadataTypeName emittedName, out bool isNoPiaLocalType)
    {
        throw new NotImplementedException();
    }

    protected override TypeSymbol SubstituteNoPiaLocalType(TypeDefinitionHandle typeDef, ref MetadataTypeName name, string interfaceGuid, string scope, string identifier)
    {
        throw new NotImplementedException();
    }

    internal override Symbol GetSymbolForMemberRef(MemberReferenceHandle memberRef, TypeSymbol implementingTypeSymbol = null, bool methodsOnly = false)
    {
        throw new NotImplementedException();
    }
}
