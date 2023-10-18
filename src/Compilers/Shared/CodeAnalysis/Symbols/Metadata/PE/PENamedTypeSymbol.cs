// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;
#endif

internal abstract partial class PENamedTypeSymbol : NamedTypeSymbol
{
    private readonly ModuleSymbol _container;
    private readonly TypeDefinitionHandle _handle;
    private readonly string _name;
    private readonly TypeAttributes _flags;
    private readonly SpecialType _corTypeId;

    /// <summary>
    /// 获取逻辑上包含了此符号的PE文件的.NET模块符号。
    /// </summary>
    /// <value>
    /// 逻辑上包含了此符号的PE文件的.NET模块符号。
    /// 若此符号不属于任何.NET模块，或在多个.NET模块间共享，则返回<see langword="null"/>。
    /// </value>
    internal PENetmoduleSymbol ContainingPEModule
    {
        get
        {
            Symbol s = this._container;

            while (s.Kind != SymbolKind.Namespace)
                s = s.ContainingSymbol;

            return ((PENamespaceSymbol)s).ContainingPEModule;
        }
    }

    /// <inheritdoc/>
    internal override NetmoduleSymbol? ContainingNetmodule => this.ContainingPEModule;

    public abstract override int Arity { get; }

    internal abstract int MetadataArity { get; }

    internal TypeDefinitionHandle Handle => this._handle;

    public override int MetadataToken => MetadataTokens.GetToken(_handle);

    public abstract ImmutableArray<PETypeParameterSymbol> TypeParameters { get; }

}
