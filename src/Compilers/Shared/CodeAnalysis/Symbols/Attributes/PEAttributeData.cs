// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.Metadata.PE;

using ThisAttributeData = LuaAttributeData;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.Metadata.PE;

using ThisAttributeData = MoonScriptAttributeData;
#endif

internal sealed partial class PEAttributeData : ThisAttributeData
{
    private readonly MetadataDecoder _decoder;
    private readonly CustomAttributeHandle _handle;
    private NamedTypeSymbol? _lazyAttributeClass = ErrorTypeSymbol.UnknownResultType;
    private PEMethodSymbol? _lazyAttributeConstructor;
    private ImmutableArray<TypedConstant> _lazyConstructorArguments;
    private ImmutableArray<KeyValuePair<string, TypedConstant>> _lazyNamedArguments;
    private ThreeState _lazyHasErrors = ThreeState.Unknown;

    internal PEAttributeData(PEModuleSymbol moduleSymbol, CustomAttributeHandle handle)
    {
        this._decoder = new(moduleSymbol);
        this._handle = handle;
    }
}
