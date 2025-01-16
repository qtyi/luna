// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class TypeSymbol : ITypeSymbol
{
    internal abstract Symbols.TypeSymbol UnderlyingTypeSymbol { get; }
    internal sealed override Symbols.ModuleSymbol UnderlyingModuleSymbol => throw new NotImplementedException();

    #region 未实现
#warning 未实现
    TypeKind ITypeSymbol.TypeKind => throw new NotImplementedException();

    INamedTypeSymbol? ITypeSymbol.BaseType => throw new NotImplementedException();

    ImmutableArray<INamedTypeSymbol> ITypeSymbol.Interfaces => throw new NotImplementedException();

    ImmutableArray<INamedTypeSymbol> ITypeSymbol.AllInterfaces => throw new NotImplementedException();

    ITypeSymbol ITypeSymbol.OriginalDefinition => throw new NotImplementedException();

    Microsoft.CodeAnalysis.NullableAnnotation ITypeSymbol.NullableAnnotation => throw new NotImplementedException();

    ISymbol? ITypeSymbol.FindImplementationForInterfaceMember(ISymbol interfaceMember)
    {
        throw new NotImplementedException();
    }

    ITypeSymbol ITypeSymbol.WithNullableAnnotation(Microsoft.CodeAnalysis.NullableAnnotation nullableAnnotation)
    {
        throw new NotImplementedException();
    }
    #endregion
}
