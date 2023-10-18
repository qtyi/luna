// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Globalization;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols.PublicModel;

using InternalModel = Qtyi.CodeAnalysis.MoonScript;
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
    public sealed override bool Equals(object? obj) => this.Equals(obj as Symbol, Microsoft.CodeAnalysis.SymbolEqualityComparer.Default);

    protected bool Equals(Symbol? other, Microsoft.CodeAnalysis.SymbolEqualityComparer equalityComparer) => other is not null && this.UnderlyingSymbol.Equals(other.UnderlyingSymbol, equalityComparer.CompareKind);

    public sealed override int GetHashCode() => this.UnderlyingSymbol.GetHashCode();
    #endregion

    #region Microsoft.CodeAnalysis.ISymbol
    Microsoft.CodeAnalysis.SymbolKind Microsoft.CodeAnalysis.ISymbol.Kind => (Microsoft.CodeAnalysis.SymbolKind)this.UnderlyingSymbol.Kind;

    string Microsoft.CodeAnalysis.ISymbol.Name => this.UnderlyingSymbol.Name;
    string Microsoft.CodeAnalysis.ISymbol.MetadataName => this.UnderlyingSymbol.MetadataName;
    int Microsoft.CodeAnalysis.ISymbol.MetadataToken => this.UnderlyingSymbol.MetadataToken;

    Microsoft.CodeAnalysis.Accessibility Microsoft.CodeAnalysis.ISymbol.DeclaredAccessibility => this.UnderlyingSymbol.DeclaredAccessibility;

    Microsoft.CodeAnalysis.ISymbol? Microsoft.CodeAnalysis.ISymbol.ContainingSymbol => this.UnderlyingSymbol.ContainingSymbol.GetPublicSymbol();
    Microsoft.CodeAnalysis.IAssemblySymbol? Microsoft.CodeAnalysis.ISymbol.ContainingAssembly => this.UnderlyingSymbol.ContainingAssembly.GetPublicSymbol();
    Microsoft.CodeAnalysis.IModuleSymbol? Microsoft.CodeAnalysis.ISymbol.ContainingModule => this.UnderlyingSymbol.ContainingNetmodule.GetPublicSymbol();
    Microsoft.CodeAnalysis.INamespaceSymbol? Microsoft.CodeAnalysis.ISymbol.ContainingNamespace => this.UnderlyingSymbol.ContainingNamespace.GetPublicSymbol();
    Microsoft.CodeAnalysis.INamedTypeSymbol? Microsoft.CodeAnalysis.ISymbol.ContainingType => this.UnderlyingSymbol.ContainingType.GetPublicSymbol();

    ImmutableArray<Microsoft.CodeAnalysis.SyntaxReference> Microsoft.CodeAnalysis.ISymbol.DeclaringSyntaxReferences => this.UnderlyingSymbol.DeclaringSyntaxReferences;
    bool Microsoft.CodeAnalysis.ISymbol.IsImplicitlyDeclared => this.UnderlyingSymbol.IsImplicitlyDeclared;

    Microsoft.CodeAnalysis.ISymbol Microsoft.CodeAnalysis.ISymbol.OriginalDefinition => this.UnderlyingSymbol.OriginalDefinition.GetPublicSymbol();
    bool Microsoft.CodeAnalysis.ISymbol.IsDefinition => this.UnderlyingSymbol.IsDefinition;

    ImmutableArray<Microsoft.CodeAnalysis.Location> Microsoft.CodeAnalysis.ISymbol.Locations => this.UnderlyingSymbol.Locations;

    bool Microsoft.CodeAnalysis.ISymbol.IsStatic => this.UnderlyingSymbol.IsStatic;
    bool Microsoft.CodeAnalysis.ISymbol.IsVirtual => this.UnderlyingSymbol.IsVirtual;
    bool Microsoft.CodeAnalysis.ISymbol.IsOverride => this.UnderlyingSymbol.IsOverride;
    bool Microsoft.CodeAnalysis.ISymbol.IsAbstract => this.UnderlyingSymbol.IsAbstract;
    bool Microsoft.CodeAnalysis.ISymbol.IsSealed => this.UnderlyingSymbol.IsSealed;
    bool Microsoft.CodeAnalysis.ISymbol.IsExtern => this.UnderlyingSymbol.IsExtern;

    bool Microsoft.CodeAnalysis.ISymbol.CanBeReferencedByName => this.UnderlyingSymbol.CanBeReferencedByName;

    bool Microsoft.CodeAnalysis.ISymbol.HasUnsupportedMetadata => this.UnderlyingSymbol.HasUnsupportedMetadata;

    void Microsoft.CodeAnalysis.ISymbol.Accept(Microsoft.CodeAnalysis.SymbolVisitor visitor) => this.Accept((SymbolVisitor)visitor);
    TResult? Microsoft.CodeAnalysis.ISymbol.Accept<TResult>(Microsoft.CodeAnalysis.SymbolVisitor<TResult> visitor) where TResult : default => this.Accept((SymbolVisitor<TResult>)visitor);
    TResult Microsoft.CodeAnalysis.ISymbol.Accept<TArgument, TResult>(Microsoft.CodeAnalysis.SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => this.Accept((SymbolVisitor<TArgument, TResult>)visitor, argument);

    bool Microsoft.CodeAnalysis.ISymbol.Equals(Microsoft.CodeAnalysis.ISymbol? other, Microsoft.CodeAnalysis.SymbolEqualityComparer equalityComparer) => this.Equals(other as Symbol, equalityComparer);

    bool IEquatable<Microsoft.CodeAnalysis.ISymbol?>.Equals(Microsoft.CodeAnalysis.ISymbol? other) => this.Equals(other as Symbol, Microsoft.CodeAnalysis.SymbolEqualityComparer.Default);

    ImmutableArray<Microsoft.CodeAnalysis.AttributeData> Microsoft.CodeAnalysis.ISymbol.GetAttributes() => ImmutableArray<Microsoft.CodeAnalysis.AttributeData>.Empty;

    string? Microsoft.CodeAnalysis.ISymbol.GetDocumentationCommentId() => null;
    string? Microsoft.CodeAnalysis.ISymbol.GetDocumentationCommentXml(CultureInfo? preferredCulture, bool expandIncludes, CancellationToken cancellationToken) => null;

    ImmutableArray<Microsoft.CodeAnalysis.SymbolDisplayPart> Microsoft.CodeAnalysis.ISymbol.ToDisplayParts(Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToDisplayParts(this, format);
    string Microsoft.CodeAnalysis.ISymbol.ToDisplayString(Microsoft.CodeAnalysis.SymbolDisplayFormat? format) => SymbolDisplay.ToDisplayString(this, format);
    #endregion

    #region ISymbol
    SymbolKind ISymbol.Kind => this.UnderlyingSymbol.Kind;
    ISymbol? ISymbol.ContainingSymbol => this.UnderlyingSymbol.ContainingSymbol.GetPublicSymbol();
    IAssemblySymbol? ISymbol.ContainingAssembly => this.UnderlyingSymbol.ContainingAssembly.GetPublicSymbol();
    INetmoduleSymbol? ISymbol.ContainingNetModule => this.UnderlyingSymbol.ContainingNetmodule.GetPublicSymbol();
    IModuleSymbol? ISymbol.ContainingModule => this.UnderlyingSymbol.ContainingModule.GetPublicSymbol();
    INamedTypeSymbol? ISymbol.ContainingType => this.UnderlyingSymbol.ContainingType.GetPublicSymbol();

    ISymbol ISymbol.OriginalDefinition => this.UnderlyingSymbol.OriginalDefinition.GetPublicSymbol();

    bool ISymbol.Equals(ISymbol? other, Microsoft.CodeAnalysis.SymbolEqualityComparer equalityComparer) => this.Equals(other as Symbol, equalityComparer);
    bool IEquatable<ISymbol?>.Equals(ISymbol? other) => this.Equals(other as Symbol, Microsoft.CodeAnalysis.SymbolEqualityComparer.Default);

    void ISymbol.Accept(SymbolVisitor visitor) => this.Accept(visitor);
    TResult? ISymbol.Accept<TResult>(SymbolVisitor<TResult> visitor) where TResult : default => this.Accept(visitor);
    TResult ISymbol.Accept<TArgument, TResult>(SymbolVisitor<TArgument, TResult> visitor, TArgument argument) => this.Accept(visitor, argument);
    #endregion
}
