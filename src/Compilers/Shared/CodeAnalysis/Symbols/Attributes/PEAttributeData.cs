// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;

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

    internal PEAttributeData(PENetmoduleSymbol moduleSymbol, CustomAttributeHandle handle)
    {
        this._decoder = new(moduleSymbol);
        this._handle = handle;
    }

    #region 未实现
#warning 未实现
    public override NamedTypeSymbol? AttributeClass => throw new NotImplementedException();

    public override ModuleSymbol? AttributeConstructor => throw new NotImplementedException();

    public override SyntaxReference? ApplicationSyntaxReference => throw new NotImplementedException();

    protected override Microsoft.CodeAnalysis.INamedTypeSymbol? CommonAttributeClass => throw new NotImplementedException();

    protected override IMethodSymbol? CommonAttributeConstructor => throw new NotImplementedException();

    protected override SyntaxReference? CommonApplicationSyntaxReference => throw new NotImplementedException();

    protected internal override ImmutableArray<TypedConstant> CommonConstructorArguments => throw new NotImplementedException();

    protected internal override ImmutableArray<KeyValuePair<string, TypedConstant>> CommonNamedArguments => throw new NotImplementedException();
    #endregion
}
