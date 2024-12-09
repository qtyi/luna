// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class DynamicTypeSymbol : TypeSymbol
{
    /// <value>
    /// Returns empty string as dynamic type is the default type.
    /// </value>
    public override string Name => base.Name;

    public override SymbolKind Kind => SymbolKind.DynamicType;

    /// <inheritdoc/>
    protected override Microsoft.CodeAnalysis.TypeKind TypeKindCore => Microsoft.CodeAnalysis.TypeKind.Dynamic;

    #region Containing
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    public override Symbol? ContainingSymbol => null;
    #endregion

    #region Declaring
    /// <value>Returns <see cref="Accessibility.NotApplicable"/>.</value>
    /// <inheritdoc/>
    public override Accessibility DeclaredAccessibility => Accessibility.NotApplicable;

    /// <inheritdoc/>
    public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences => ImmutableArray<SyntaxReference>.Empty;
    #endregion

    /// <summary>
    /// Gets the unique <see cref="DynamicTypeSymbol"/>.
    /// </summary>
    internal static readonly DynamicTypeSymbol Instance = new();

    /// <inheritdoc/>
    public override ImmutableArray<Location> Locations => ImmutableArray<Location>.Empty;

    /// <inheritdoc/>
    public override bool IsStatic => false;

    /// <inheritdoc/>
    public override bool IsVirtual => false;

    /// <inheritdoc/>
    public override bool IsOverride => false;

    /// <inheritdoc/>
    public override bool IsAbstract => false;

    /// <inheritdoc/>
    public override bool IsSealed => false;

    /// <inheritdoc/>
    public override bool IsExtern => false;

    protected override bool IsReferenceTypeCore => true;

    protected override bool IsValueTypeCore => false;

    #region GetMembers
    /// <inheritdoc/>
    public override ImmutableArray<ModuleSymbol> GetMembers() => ImmutableArray<ModuleSymbol>.Empty;

    /// <inheritdoc/>
    public override ImmutableArray<ModuleSymbol> GetMembers(string name) => ImmutableArray<ModuleSymbol>.Empty;

    /// <inheritdoc/>
    public override ImmutableArray<NamedTypeSymbol> GetTypeMembers() => ImmutableArray<NamedTypeSymbol>.Empty;

    /// <inheritdoc/>
    public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name) => ImmutableArray<NamedTypeSymbol>.Empty;

    /// <inheritdoc/>
    public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity) => ImmutableArray<NamedTypeSymbol>.Empty;

    /// <inheritdoc/>
    public override ImmutableArray<FieldSymbol> GetFieldMembers() => ImmutableArray<FieldSymbol>.Empty;

    /// <inheritdoc/>
    public override ImmutableArray<FieldSymbol> GetFieldMembers(string name) => ImmutableArray<FieldSymbol>.Empty;

    /// <inheritdoc/>
    public override ImmutableArray<FieldSymbol> GetFieldMembers(string name, int arity) => ImmutableArray<FieldSymbol>.Empty;
    #endregion

    internal override TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance)
    {
        Debug.Assert(this.Equals(other, TypeCompareKind.IgnoreDynamicAndTupleNames | TypeCompareKind.IgnoreNullableModifiersForReferenceTypes));
        return this;
    }

    #region Public Symbol
    protected override ISymbol CreateISymbol() => new PublicModel.DynamicTypeSymbol(this, this.DefaultNullableAnnotation);

    protected override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
    {
        Debug.Assert(nullableAnnotation != this.DefaultNullableAnnotation);
        return new PublicModel.DynamicTypeSymbol(this, nullableAnnotation);
    }
    #endregion

    /// <inheritdoc/>
    private DynamicTypeSymbol() { }

}
