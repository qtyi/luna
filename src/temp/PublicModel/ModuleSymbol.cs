// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class ModuleSymbol : IModuleSymbol
{
    internal abstract Symbols.ModuleSymbol UnderlyingModuleSymbol { get; }
    internal sealed override InternalModel.Symbol UnderlyingSymbol => UnderlyingModuleSymbol;

    #region 未实现
#warning 未实现
    ModuleKind IModuleSymbol.ModuleKind => throw new NotImplementedException();

    bool IModuleSymbol.IsField => throw new NotImplementedException();

    bool IModuleSymbol.IsGlobalModule => throw new NotImplementedException();

    ImmutableArray<IModuleSymbol> IModuleSymbol.ConstituentModules => throw new NotImplementedException();

    bool INamespaceSymbol.IsGlobalNamespace => throw new NotImplementedException();

    NamespaceKind INamespaceSymbol.NamespaceKind => throw new NotImplementedException();

    Compilation? INamespaceSymbol.ContainingCompilation => throw new NotImplementedException();

    ImmutableArray<INamespaceSymbol> INamespaceSymbol.ConstituentNamespaces => throw new NotImplementedException();

    TypeKind ITypeSymbol.TypeKind => throw new NotImplementedException();

    INamedTypeSymbol? ITypeSymbol.BaseType => throw new NotImplementedException();

    ImmutableArray<INamedTypeSymbol> ITypeSymbol.Interfaces => throw new NotImplementedException();

    ImmutableArray<INamedTypeSymbol> ITypeSymbol.AllInterfaces => throw new NotImplementedException();

    bool ITypeSymbol.IsReferenceType => throw new NotImplementedException();

    bool ITypeSymbol.IsValueType => throw new NotImplementedException();

    bool ITypeSymbol.IsAnonymousType => throw new NotImplementedException();

    bool ITypeSymbol.IsTupleType => throw new NotImplementedException();

    bool ITypeSymbol.IsNativeIntegerType => throw new NotImplementedException();

    ITypeSymbol ITypeSymbol.OriginalDefinition => throw new NotImplementedException();

    IEventSymbol IEventSymbol.OriginalDefinition => throw new NotImplementedException();

    IFieldSymbol IFieldSymbol.OriginalDefinition => throw new NotImplementedException();

    IMethodSymbol IMethodSymbol.OriginalDefinition => throw new NotImplementedException();

    IPropertySymbol IPropertySymbol.OriginalDefinition => throw new NotImplementedException();

    SpecialType ITypeSymbol.SpecialType => throw new NotImplementedException();

    bool ITypeSymbol.IsRefLikeType => throw new NotImplementedException();

    bool ITypeSymbol.IsUnmanagedType => throw new NotImplementedException();

    bool ITypeSymbol.IsReadOnly => throw new NotImplementedException();

    bool IFieldSymbol.IsReadOnly => throw new NotImplementedException();

    bool IMethodSymbol.IsReadOnly => throw new NotImplementedException();

    bool IPropertySymbol.IsReadOnly => throw new NotImplementedException();

    bool ITypeSymbol.IsRecord => throw new NotImplementedException();

    NullableAnnotation ITypeSymbol.NullableAnnotation => throw new NotImplementedException();

    NullableAnnotation IEventSymbol.NullableAnnotation => throw new NotImplementedException();

    NullableAnnotation IFieldSymbol.NullableAnnotation => throw new NotImplementedException();

    NullableAnnotation IPropertySymbol.NullableAnnotation => throw new NotImplementedException();

    bool INamespaceOrTypeSymbol.IsNamespace => throw new NotImplementedException();

    bool INamespaceOrTypeSymbol.IsType => throw new NotImplementedException();

    ITypeSymbol IEventSymbol.Type => throw new NotImplementedException();

    ITypeSymbol IFieldSymbol.Type => throw new NotImplementedException();

    ITypeSymbol IPropertySymbol.Type => throw new NotImplementedException();

    bool IEventSymbol.IsWindowsRuntimeEvent => throw new NotImplementedException();

    IMethodSymbol? IEventSymbol.AddMethod => throw new NotImplementedException();

    IMethodSymbol? IEventSymbol.RemoveMethod => throw new NotImplementedException();

    IMethodSymbol? IEventSymbol.RaiseMethod => throw new NotImplementedException();

    IEventSymbol? IEventSymbol.OverriddenEvent => throw new NotImplementedException();

    ImmutableArray<IEventSymbol> IEventSymbol.ExplicitInterfaceImplementations => throw new NotImplementedException();

    ImmutableArray<IMethodSymbol> IMethodSymbol.ExplicitInterfaceImplementations => throw new NotImplementedException();

    ImmutableArray<IPropertySymbol> IPropertySymbol.ExplicitInterfaceImplementations => throw new NotImplementedException();

    ISymbol? IFieldSymbol.AssociatedSymbol => throw new NotImplementedException();

    ISymbol? IMethodSymbol.AssociatedSymbol => throw new NotImplementedException();

    bool IFieldSymbol.IsConst => throw new NotImplementedException();

    bool IFieldSymbol.IsVolatile => throw new NotImplementedException();

    bool IFieldSymbol.IsRequired => throw new NotImplementedException();

    bool IPropertySymbol.IsRequired => throw new NotImplementedException();

    bool IFieldSymbol.IsFixedSizeBuffer => throw new NotImplementedException();

    int IFieldSymbol.FixedSize => throw new NotImplementedException();

    RefKind IFieldSymbol.RefKind => throw new NotImplementedException();

    RefKind IMethodSymbol.RefKind => throw new NotImplementedException();

    RefKind IPropertySymbol.RefKind => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IFieldSymbol.RefCustomModifiers => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IMethodSymbol.RefCustomModifiers => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IPropertySymbol.RefCustomModifiers => throw new NotImplementedException();

    bool IFieldSymbol.HasConstantValue => throw new NotImplementedException();

    object? IFieldSymbol.ConstantValue => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IFieldSymbol.CustomModifiers => throw new NotImplementedException();

    IFieldSymbol? IFieldSymbol.CorrespondingTupleField => throw new NotImplementedException();

    bool IFieldSymbol.IsExplicitlyNamedTupleElement => throw new NotImplementedException();

    MethodKind IMethodSymbol.MethodKind => throw new NotImplementedException();

    int IMethodSymbol.Arity => throw new NotImplementedException();

    bool IMethodSymbol.IsGenericMethod => throw new NotImplementedException();

    bool IMethodSymbol.IsExtensionMethod => throw new NotImplementedException();

    bool IMethodSymbol.IsAsync => throw new NotImplementedException();

    bool IMethodSymbol.IsVararg => throw new NotImplementedException();

    bool IMethodSymbol.IsCheckedBuiltin => throw new NotImplementedException();

    bool IMethodSymbol.HidesBaseMethodsByName => throw new NotImplementedException();

    bool IMethodSymbol.ReturnsVoid => throw new NotImplementedException();

    bool IMethodSymbol.ReturnsByRef => throw new NotImplementedException();

    bool IPropertySymbol.ReturnsByRef => throw new NotImplementedException();

    bool IMethodSymbol.ReturnsByRefReadonly => throw new NotImplementedException();

    bool IPropertySymbol.ReturnsByRefReadonly => throw new NotImplementedException();

    ITypeSymbol IMethodSymbol.ReturnType => throw new NotImplementedException();

    NullableAnnotation IMethodSymbol.ReturnNullableAnnotation => throw new NotImplementedException();

    ImmutableArray<ITypeSymbol> IMethodSymbol.TypeArguments => throw new NotImplementedException();

    ImmutableArray<NullableAnnotation> IMethodSymbol.TypeArgumentNullableAnnotations => throw new NotImplementedException();

    ImmutableArray<ITypeParameterSymbol> IMethodSymbol.TypeParameters => throw new NotImplementedException();

    ImmutableArray<IParameterSymbol> IMethodSymbol.Parameters => throw new NotImplementedException();

    ImmutableArray<IParameterSymbol> IPropertySymbol.Parameters => throw new NotImplementedException();

    IMethodSymbol IMethodSymbol.ConstructedFrom => throw new NotImplementedException();

    bool IMethodSymbol.IsInitOnly => throw new NotImplementedException();

    IMethodSymbol? IMethodSymbol.OverriddenMethod => throw new NotImplementedException();

    ITypeSymbol? IMethodSymbol.ReceiverType => throw new NotImplementedException();

    NullableAnnotation IMethodSymbol.ReceiverNullableAnnotation => throw new NotImplementedException();

    IMethodSymbol? IMethodSymbol.ReducedFrom => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IMethodSymbol.ReturnTypeCustomModifiers => throw new NotImplementedException();

    SignatureCallingConvention IMethodSymbol.CallingConvention => throw new NotImplementedException();

    ImmutableArray<INamedTypeSymbol> IMethodSymbol.UnmanagedCallingConventionTypes => throw new NotImplementedException();

    IMethodSymbol? IMethodSymbol.PartialDefinitionPart => throw new NotImplementedException();

    IMethodSymbol? IMethodSymbol.PartialImplementationPart => throw new NotImplementedException();

    MethodImplAttributes IMethodSymbol.MethodImplementationFlags => throw new NotImplementedException();

    bool IMethodSymbol.IsPartialDefinition => throw new NotImplementedException();

    INamedTypeSymbol? IMethodSymbol.AssociatedAnonymousDelegate => throw new NotImplementedException();

    bool IMethodSymbol.IsConditional => throw new NotImplementedException();

    bool IPropertySymbol.IsIndexer => throw new NotImplementedException();

    bool IPropertySymbol.IsWriteOnly => throw new NotImplementedException();

    bool IPropertySymbol.IsWithEvents => throw new NotImplementedException();

    IMethodSymbol? IPropertySymbol.GetMethod => throw new NotImplementedException();

    IMethodSymbol? IPropertySymbol.SetMethod => throw new NotImplementedException();

    IPropertySymbol? IPropertySymbol.OverriddenProperty => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IPropertySymbol.TypeCustomModifiers => throw new NotImplementedException();

    IMethodSymbol IMethodSymbol.Construct(params ITypeSymbol[] typeArguments)
    {
        throw new NotImplementedException();
    }

    IMethodSymbol IMethodSymbol.Construct(ImmutableArray<ITypeSymbol> typeArguments, ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    ISymbol? ITypeSymbol.FindImplementationForInterfaceMember(ISymbol interfaceMember)
    {
        throw new NotImplementedException();
    }

    DllImportData? IMethodSymbol.GetDllImportData()
    {
        throw new NotImplementedException();
    }

    IEnumerable<IFieldSymbol> IModuleSymbol.GetFieldSymbols()
    {
        throw new NotImplementedException();
    }

    IEnumerable<IFieldSymbol> IModuleSymbol.GetFieldSymbols(string name)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<IModuleSymbol> IModuleSymbol.GetMembers()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<IModuleSymbol> IModuleSymbol.GetMembers(string name)
    {
        throw new NotImplementedException();
    }

    IEnumerable<INamespaceOrTypeSymbol> INamespaceSymbol.GetMembers()
    {
        throw new NotImplementedException();
    }

    IEnumerable<INamespaceOrTypeSymbol> INamespaceSymbol.GetMembers(string name)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<ISymbol> INamespaceOrTypeSymbol.GetMembers()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<ISymbol> INamespaceOrTypeSymbol.GetMembers(string name)
    {
        throw new NotImplementedException();
    }

    IEnumerable<IModuleSymbol> IModuleSymbol.GetNamespaceMembers()
    {
        throw new NotImplementedException();
    }

    IEnumerable<INamespaceSymbol> INamespaceSymbol.GetNamespaceMembers()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<AttributeData> IMethodSymbol.GetReturnTypeAttributes()
    {
        throw new NotImplementedException();
    }

    ITypeSymbol? IMethodSymbol.GetTypeInferredDuringReduction(ITypeParameterSymbol reducedFromTypeParameter)
    {
        throw new NotImplementedException();
    }

    IEnumerable<INamedTypeSymbol> IModuleSymbol.GetTypeMembers()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<INamedTypeSymbol> IModuleSymbol.GetTypeMembers(string name)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<INamedTypeSymbol> IModuleSymbol.GetTypeMembers(string name, int arity)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers(string name)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers(string name, int arity)
    {
        throw new NotImplementedException();
    }

    IMethodSymbol? IMethodSymbol.ReduceExtensionMethod(ITypeSymbol receiverType)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<SymbolDisplayPart> ITypeSymbol.ToDisplayParts(NullableFlowState topLevelNullability, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    string ITypeSymbol.ToDisplayString(NullableFlowState topLevelNullability, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<SymbolDisplayPart> ITypeSymbol.ToMinimalDisplayParts(Microsoft.CodeAnalysis.SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    string ITypeSymbol.ToMinimalDisplayString(Microsoft.CodeAnalysis.SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    ITypeSymbol ITypeSymbol.WithNullableAnnotation(NullableAnnotation nullableAnnotation)
    {
        throw new NotImplementedException();
    }
    #endregion
}
