// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.Collections.Immutable;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class NamedTypeSymbol : INamedTypeSymbol
{
    internal abstract InternalModel.Symbols.NamedTypeSymbol UnderlyingNamedTypeSymbol { get; }
    internal sealed override InternalModel.Symbols.TypeSymbol UnderlyingTypeSymbol => this.UnderlyingNamedTypeSymbol;

    #region Accept
    protected sealed override void Accept(SymbolVisitor visitor) => visitor.VisitNamedType(this);

    protected sealed override TResult? Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => visitor.VisitNamedType(this);

    protected sealed override TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => visitor.VisitNamedType(this, argument);
    #endregion

    #region 未完成
#warning 未完成
    ImmutableArray<ITypeSymbol> INamedTypeSymbol.TypeArguments => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.ITypeSymbol> Microsoft.CodeAnalysis.INamedTypeSymbol.TypeArguments => throw new NotImplementedException();

    INamedTypeSymbol INamedTypeSymbol.OriginalDefinition => throw new NotImplementedException();

    Microsoft.CodeAnalysis.INamedTypeSymbol Microsoft.CodeAnalysis.INamedTypeSymbol.OriginalDefinition => throw new NotImplementedException();

    IModuleSymbol? INamedTypeSymbol.DelegateInvokeMethod => throw new NotImplementedException();

    Microsoft.CodeAnalysis.IMethodSymbol? Microsoft.CodeAnalysis.INamedTypeSymbol.DelegateInvokeMethod => throw new NotImplementedException();

    INamedTypeSymbol? INamedTypeSymbol.EnumUnderlyingType => throw new NotImplementedException();

    Microsoft.CodeAnalysis.INamedTypeSymbol? Microsoft.CodeAnalysis.INamedTypeSymbol.EnumUnderlyingType => throw new NotImplementedException();

    INamedTypeSymbol INamedTypeSymbol.ConstructedFrom => throw new NotImplementedException();

    Microsoft.CodeAnalysis.INamedTypeSymbol Microsoft.CodeAnalysis.INamedTypeSymbol.ConstructedFrom => throw new NotImplementedException();

    ImmutableArray<IModuleSymbol> INamedTypeSymbol.InstanceConstructors => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.IMethodSymbol> Microsoft.CodeAnalysis.INamedTypeSymbol.InstanceConstructors => throw new NotImplementedException();

    ImmutableArray<IModuleSymbol> INamedTypeSymbol.StaticConstructors => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.IMethodSymbol> Microsoft.CodeAnalysis.INamedTypeSymbol.StaticConstructors => throw new NotImplementedException();

    ImmutableArray<IModuleSymbol> INamedTypeSymbol.Constructors => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.IMethodSymbol> Microsoft.CodeAnalysis.INamedTypeSymbol.Constructors => throw new NotImplementedException();

    ISymbol? INamedTypeSymbol.AssociatedSymbol => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ISymbol? Microsoft.CodeAnalysis.INamedTypeSymbol.AssociatedSymbol => throw new NotImplementedException();

    INamedTypeSymbol? INamedTypeSymbol.TupleUnderlyingType => throw new NotImplementedException();

    Microsoft.CodeAnalysis.INamedTypeSymbol? Microsoft.CodeAnalysis.INamedTypeSymbol.TupleUnderlyingType => throw new NotImplementedException();

    ImmutableArray<IModuleSymbol> INamedTypeSymbol.TupleElements => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.IFieldSymbol> Microsoft.CodeAnalysis.INamedTypeSymbol.TupleElements => throw new NotImplementedException();

    INamedTypeSymbol? INamedTypeSymbol.NativeIntegerUnderlyingType => throw new NotImplementedException();

    Microsoft.CodeAnalysis.INamedTypeSymbol? Microsoft.CodeAnalysis.INamedTypeSymbol.NativeIntegerUnderlyingType => throw new NotImplementedException();

    int Microsoft.CodeAnalysis.INamedTypeSymbol.Arity => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.INamedTypeSymbol.IsGenericType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.INamedTypeSymbol.IsUnboundGenericType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.INamedTypeSymbol.IsScriptClass => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.INamedTypeSymbol.IsImplicitClass => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.INamedTypeSymbol.IsComImport => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.INamedTypeSymbol.IsFileLocal => throw new NotImplementedException();

    IEnumerable<string> Microsoft.CodeAnalysis.INamedTypeSymbol.MemberNames => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.ITypeParameterSymbol> Microsoft.CodeAnalysis.INamedTypeSymbol.TypeParameters => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.NullableAnnotation> Microsoft.CodeAnalysis.INamedTypeSymbol.TypeArgumentNullableAnnotations => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.INamedTypeSymbol.MightContainExtensionMethods => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.INamedTypeSymbol.IsSerializable => throw new NotImplementedException();

    INamedTypeSymbol INamedTypeSymbol.Construct(params ITypeSymbol[] typeArguments)
    {
        throw new NotImplementedException();
    }

    INamedTypeSymbol INamedTypeSymbol.Construct(ImmutableArray<ITypeSymbol> typeArguments, ImmutableArray<Microsoft.CodeAnalysis.NullableAnnotation> typeArgumentNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    Microsoft.CodeAnalysis.INamedTypeSymbol Microsoft.CodeAnalysis.INamedTypeSymbol.Construct(params Microsoft.CodeAnalysis.ITypeSymbol[] typeArguments)
    {
        throw new NotImplementedException();
    }

    Microsoft.CodeAnalysis.INamedTypeSymbol Microsoft.CodeAnalysis.INamedTypeSymbol.Construct(ImmutableArray<Microsoft.CodeAnalysis.ITypeSymbol> typeArguments, ImmutableArray<Microsoft.CodeAnalysis.NullableAnnotation> typeArgumentNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    INamedTypeSymbol INamedTypeSymbol.ConstructUnboundGenericType()
    {
        throw new NotImplementedException();
    }

    Microsoft.CodeAnalysis.INamedTypeSymbol Microsoft.CodeAnalysis.INamedTypeSymbol.ConstructUnboundGenericType()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<Microsoft.CodeAnalysis.CustomModifier> Microsoft.CodeAnalysis.INamedTypeSymbol.GetTypeArgumentCustomModifiers(int ordinal)
    {
        throw new NotImplementedException();
    }
    #endregion
}
