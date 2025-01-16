// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Globalization;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = MoonScript;
#endif

internal abstract partial class Symbol : ISymbol
{
    internal abstract InternalModel.Symbol UnderlyingSymbol { get; }

    #region Accept
    protected abstract void Accept(SymbolVisitor visitor);

    protected abstract TResult? Accept<TResult>(SymbolVisitor<TResult> visitor);

    protected abstract TResult Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument);
    #endregion

    #region Equality
    public sealed override bool Equals(object? obj) => Equals(obj as Symbol, SymbolEqualityComparer.Default);

    protected bool Equals(Symbol? other, SymbolEqualityComparer equalityComparer) => other is not null && UnderlyingSymbol.Equals(other.UnderlyingSymbol, equalityComparer.CompareKind);

    public sealed override int GetHashCode() => UnderlyingSymbol.GetHashCode();
    #endregion

    #region ISymbol
    SymbolKind ISymbol.Kind => (SymbolKind)UnderlyingSymbol.Kind;

    string ISymbol.Name => UnderlyingSymbol.Name;
    string ISymbol.MetadataName => UnderlyingSymbol.MetadataName;
    int ISymbol.MetadataToken => UnderlyingSymbol.MetadataToken;

    Accessibility ISymbol.DeclaredAccessibility => UnderlyingSymbol.DeclaredAccessibility;

    ISymbol? ISymbol.ContainingSymbol => UnderlyingSymbol.ContainingSymbol.GetPublicSymbol();
    IAssemblySymbol? ISymbol.ContainingAssembly => UnderlyingSymbol.ContainingAssembly.GetPublicSymbol();
    IModuleSymbol? ISymbol.ContainingModule => UnderlyingSymbol.ContainingModule.GetPublicSymbol();
    INamespaceSymbol? ISymbol.ContainingNamespace => UnderlyingSymbol.ContainingNamespace.GetPublicSymbol();
    INamedTypeSymbol? ISymbol.ContainingType => UnderlyingSymbol.ContainingType.GetPublicSymbol();

    ImmutableArray<SyntaxReference> ISymbol.DeclaringSyntaxReferences => UnderlyingSymbol.DeclaringSyntaxReferences;
    bool ISymbol.IsImplicitlyDeclared => UnderlyingSymbol.IsImplicitlyDeclared;

    ISymbol ISymbol.OriginalDefinition => UnderlyingSymbol.OriginalDefinition.GetPublicSymbol();
    bool ISymbol.IsDefinition => UnderlyingSymbol.IsDefinition;

    ImmutableArray<Location> ISymbol.Locations => UnderlyingSymbol.Locations;

    bool ISymbol.IsStatic => UnderlyingSymbol.IsStatic;
    bool ISymbol.IsVirtual => UnderlyingSymbol.IsVirtual;
    bool ISymbol.IsOverride => UnderlyingSymbol.IsOverride;
    bool ISymbol.IsAbstract => UnderlyingSymbol.IsAbstract;
    bool ISymbol.IsSealed => UnderlyingSymbol.IsSealed;
    bool ISymbol.IsExtern => UnderlyingSymbol.IsExtern;

    bool ISymbol.CanBeReferencedByName => UnderlyingSymbol.CanBeReferencedByName;

    bool ISymbol.HasUnsupportedMetadata => UnderlyingSymbol.HasUnsupportedMetadata;

    void ISymbol.Accept(Microsoft.CodeAnalysis.SymbolVisitor visitor) => Accept((SymbolVisitor)visitor);
    TResult? ISymbol.Accept<TResult>(Microsoft.CodeAnalysis.SymbolVisitor<TResult> visitor) where TResult : default => Accept((SymbolVisitor<TResult>)visitor);
    TResult ISymbol.Accept<TArgument, TResult>(Microsoft.CodeAnalysis.SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => Accept((SymbolVisitor<TArgument, TResult>)visitor, argument);

    bool ISymbol.Equals(ISymbol? other, SymbolEqualityComparer equalityComparer) => Equals(other as Symbol, equalityComparer);

    bool IEquatable<ISymbol?>.Equals(ISymbol? other) => Equals(other as Symbol, SymbolEqualityComparer.Default);

    ImmutableArray<AttributeData> ISymbol.GetAttributes() => ImmutableArray<AttributeData>.Empty;

    string? ISymbol.GetDocumentationCommentId() => null;
    string? ISymbol.GetDocumentationCommentXml(CultureInfo? preferredCulture, bool expandIncludes, CancellationToken cancellationToken) => null;

    ImmutableArray<SymbolDisplayPart> ISymbol.ToDisplayParts(SymbolDisplayFormat? format) => SymbolDisplay.ToDisplayParts(this, format);
    string ISymbol.ToDisplayString(SymbolDisplayFormat? format) => SymbolDisplay.ToDisplayString(this, format);
    #endregion
}
