// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Reflection;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
#endif

partial class ModuleSymbol : IModuleSymbol
{
    internal abstract InternalModel.Symbols.ModuleSymbol UnderlyingModuleSymbol { get; }
    internal sealed override InternalModel.Symbol UnderlyingSymbol => this.UnderlyingModuleSymbol;

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

    Microsoft.CodeAnalysis.TypeKind Microsoft.CodeAnalysis.ITypeSymbol.TypeKind => throw new NotImplementedException();

    Microsoft.CodeAnalysis.INamedTypeSymbol? Microsoft.CodeAnalysis.ITypeSymbol.BaseType => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.INamedTypeSymbol> Microsoft.CodeAnalysis.ITypeSymbol.Interfaces => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.INamedTypeSymbol> Microsoft.CodeAnalysis.ITypeSymbol.AllInterfaces => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsReferenceType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsValueType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsAnonymousType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsTupleType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsNativeIntegerType => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ITypeSymbol Microsoft.CodeAnalysis.ITypeSymbol.OriginalDefinition => throw new NotImplementedException();

    IEventSymbol IEventSymbol.OriginalDefinition => throw new NotImplementedException();

    Microsoft.CodeAnalysis.IFieldSymbol Microsoft.CodeAnalysis.IFieldSymbol.OriginalDefinition => throw new NotImplementedException();

    IMethodSymbol IMethodSymbol.OriginalDefinition => throw new NotImplementedException();

    IPropertySymbol IPropertySymbol.OriginalDefinition => throw new NotImplementedException();

    SpecialType Microsoft.CodeAnalysis.ITypeSymbol.SpecialType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsRefLikeType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsUnmanagedType => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsReadOnly => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.IFieldSymbol.IsReadOnly => throw new NotImplementedException();

    bool IMethodSymbol.IsReadOnly => throw new NotImplementedException();

    bool IPropertySymbol.IsReadOnly => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.ITypeSymbol.IsRecord => throw new NotImplementedException();

    NullableAnnotation Microsoft.CodeAnalysis.ITypeSymbol.NullableAnnotation => throw new NotImplementedException();

    NullableAnnotation IEventSymbol.NullableAnnotation => throw new NotImplementedException();

    NullableAnnotation Microsoft.CodeAnalysis.IFieldSymbol.NullableAnnotation => throw new NotImplementedException();

    NullableAnnotation IPropertySymbol.NullableAnnotation => throw new NotImplementedException();

    bool INamespaceOrTypeSymbol.IsNamespace => throw new NotImplementedException();

    bool INamespaceOrTypeSymbol.IsType => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ITypeSymbol IEventSymbol.Type => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ITypeSymbol Microsoft.CodeAnalysis.IFieldSymbol.Type => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ITypeSymbol IPropertySymbol.Type => throw new NotImplementedException();

    bool IEventSymbol.IsWindowsRuntimeEvent => throw new NotImplementedException();

    IMethodSymbol? IEventSymbol.AddMethod => throw new NotImplementedException();

    IMethodSymbol? IEventSymbol.RemoveMethod => throw new NotImplementedException();

    IMethodSymbol? IEventSymbol.RaiseMethod => throw new NotImplementedException();

    IEventSymbol? IEventSymbol.OverriddenEvent => throw new NotImplementedException();

    ImmutableArray<IEventSymbol> IEventSymbol.ExplicitInterfaceImplementations => throw new NotImplementedException();

    ImmutableArray<IMethodSymbol> IMethodSymbol.ExplicitInterfaceImplementations => throw new NotImplementedException();

    ImmutableArray<IPropertySymbol> IPropertySymbol.ExplicitInterfaceImplementations => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ISymbol? Microsoft.CodeAnalysis.IFieldSymbol.AssociatedSymbol => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ISymbol? IMethodSymbol.AssociatedSymbol => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.IFieldSymbol.IsConst => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.IFieldSymbol.IsVolatile => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.IFieldSymbol.IsRequired => throw new NotImplementedException();

    bool IPropertySymbol.IsRequired => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.IFieldSymbol.IsFixedSizeBuffer => throw new NotImplementedException();

    int Microsoft.CodeAnalysis.IFieldSymbol.FixedSize => throw new NotImplementedException();

    RefKind Microsoft.CodeAnalysis.IFieldSymbol.RefKind => throw new NotImplementedException();

    RefKind IMethodSymbol.RefKind => throw new NotImplementedException();

    RefKind IPropertySymbol.RefKind => throw new NotImplementedException();

    ImmutableArray<CustomModifier> Microsoft.CodeAnalysis.IFieldSymbol.RefCustomModifiers => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IMethodSymbol.RefCustomModifiers => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IPropertySymbol.RefCustomModifiers => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.IFieldSymbol.HasConstantValue => throw new NotImplementedException();

    object? Microsoft.CodeAnalysis.IFieldSymbol.ConstantValue => throw new NotImplementedException();

    ImmutableArray<CustomModifier> Microsoft.CodeAnalysis.IFieldSymbol.CustomModifiers => throw new NotImplementedException();

    Microsoft.CodeAnalysis.IFieldSymbol? Microsoft.CodeAnalysis.IFieldSymbol.CorrespondingTupleField => throw new NotImplementedException();

    bool Microsoft.CodeAnalysis.IFieldSymbol.IsExplicitlyNamedTupleElement => throw new NotImplementedException();

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

    Microsoft.CodeAnalysis.ITypeSymbol IMethodSymbol.ReturnType => throw new NotImplementedException();

    NullableAnnotation IMethodSymbol.ReturnNullableAnnotation => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.ITypeSymbol> IMethodSymbol.TypeArguments => throw new NotImplementedException();

    ImmutableArray<NullableAnnotation> IMethodSymbol.TypeArgumentNullableAnnotations => throw new NotImplementedException();

    ImmutableArray<ITypeParameterSymbol> IMethodSymbol.TypeParameters => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.IParameterSymbol> IMethodSymbol.Parameters => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.IParameterSymbol> IPropertySymbol.Parameters => throw new NotImplementedException();

    IMethodSymbol IMethodSymbol.ConstructedFrom => throw new NotImplementedException();

    bool IMethodSymbol.IsInitOnly => throw new NotImplementedException();

    IMethodSymbol? IMethodSymbol.OverriddenMethod => throw new NotImplementedException();

    Microsoft.CodeAnalysis.ITypeSymbol? IMethodSymbol.ReceiverType => throw new NotImplementedException();

    NullableAnnotation IMethodSymbol.ReceiverNullableAnnotation => throw new NotImplementedException();

    IMethodSymbol? IMethodSymbol.ReducedFrom => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IMethodSymbol.ReturnTypeCustomModifiers => throw new NotImplementedException();

    SignatureCallingConvention IMethodSymbol.CallingConvention => throw new NotImplementedException();

    ImmutableArray<Microsoft.CodeAnalysis.INamedTypeSymbol> IMethodSymbol.UnmanagedCallingConventionTypes => throw new NotImplementedException();

    IMethodSymbol? IMethodSymbol.PartialDefinitionPart => throw new NotImplementedException();

    IMethodSymbol? IMethodSymbol.PartialImplementationPart => throw new NotImplementedException();

    MethodImplAttributes IMethodSymbol.MethodImplementationFlags => throw new NotImplementedException();

    bool IMethodSymbol.IsPartialDefinition => throw new NotImplementedException();

    Microsoft.CodeAnalysis.INamedTypeSymbol? IMethodSymbol.AssociatedAnonymousDelegate => throw new NotImplementedException();

    bool IMethodSymbol.IsConditional => throw new NotImplementedException();

    bool IPropertySymbol.IsIndexer => throw new NotImplementedException();

    bool IPropertySymbol.IsWriteOnly => throw new NotImplementedException();

    bool IPropertySymbol.IsWithEvents => throw new NotImplementedException();

    IMethodSymbol? IPropertySymbol.GetMethod => throw new NotImplementedException();

    IMethodSymbol? IPropertySymbol.SetMethod => throw new NotImplementedException();

    IPropertySymbol? IPropertySymbol.OverriddenProperty => throw new NotImplementedException();

    ImmutableArray<CustomModifier> IPropertySymbol.TypeCustomModifiers => throw new NotImplementedException();

    IMethodSymbol IMethodSymbol.Construct(params Microsoft.CodeAnalysis.ITypeSymbol[] typeArguments)
    {
        throw new NotImplementedException();
    }

    IMethodSymbol IMethodSymbol.Construct(ImmutableArray<Microsoft.CodeAnalysis.ITypeSymbol> typeArguments, ImmutableArray<NullableAnnotation> typeArgumentNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    Microsoft.CodeAnalysis.ISymbol? Microsoft.CodeAnalysis.ITypeSymbol.FindImplementationForInterfaceMember(Microsoft.CodeAnalysis.ISymbol interfaceMember)
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

    ImmutableArray<Microsoft.CodeAnalysis.ISymbol> INamespaceOrTypeSymbol.GetMembers()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<Microsoft.CodeAnalysis.ISymbol> INamespaceOrTypeSymbol.GetMembers(string name)
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

    Microsoft.CodeAnalysis.ITypeSymbol? IMethodSymbol.GetTypeInferredDuringReduction(ITypeParameterSymbol reducedFromTypeParameter)
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

    ImmutableArray<Microsoft.CodeAnalysis.INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers()
    {
        throw new NotImplementedException();
    }

    ImmutableArray<Microsoft.CodeAnalysis.INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers(string name)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<Microsoft.CodeAnalysis.INamedTypeSymbol> INamespaceOrTypeSymbol.GetTypeMembers(string name, int arity)
    {
        throw new NotImplementedException();
    }

    IMethodSymbol? IMethodSymbol.ReduceExtensionMethod(Microsoft.CodeAnalysis.ITypeSymbol receiverType)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<SymbolDisplayPart> Microsoft.CodeAnalysis.ITypeSymbol.ToDisplayParts(NullableFlowState topLevelNullability, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    string Microsoft.CodeAnalysis.ITypeSymbol.ToDisplayString(NullableFlowState topLevelNullability, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    ImmutableArray<SymbolDisplayPart> Microsoft.CodeAnalysis.ITypeSymbol.ToMinimalDisplayParts(Microsoft.CodeAnalysis.SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    string Microsoft.CodeAnalysis.ITypeSymbol.ToMinimalDisplayString(Microsoft.CodeAnalysis.SemanticModel semanticModel, NullableFlowState topLevelNullability, int position, SymbolDisplayFormat? format)
    {
        throw new NotImplementedException();
    }

    Microsoft.CodeAnalysis.ITypeSymbol Microsoft.CodeAnalysis.ITypeSymbol.WithNullableAnnotation(NullableAnnotation nullableAnnotation)
    {
        throw new NotImplementedException();
    }
    #endregion
}
