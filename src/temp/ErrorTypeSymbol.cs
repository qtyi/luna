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

partial class ErrorTypeSymbol
{
    /// <inheritdoc cref="TypeSymbol()"/>
    internal ErrorTypeSymbol() : base() { }

    /// <summary>
    /// 表示未知的返回值的错误类型符号。
    /// </summary>
    internal static readonly ErrorTypeSymbol UnknownResultType = new UnsupportedMetadataTypeSymbol();

    /// <summary>
    /// Gets the underlying error this symbol of.
    /// </summary>
    /// <value>
    /// The underlying error.
    /// </value>
    internal abstract DiagnosticInfo? ErrorInfo { get; }

    /// <summary>
    /// Gets a summary of the reason why the type is bad.
    /// </summary>
    internal virtual LookupResultKind ResultKind => LookupResultKind.Empty;

    /// <summary>
    /// Gets a collection of symbols that seemed to be what the user intended.
    /// </summary>
    /// <remarks>
    /// When constructing this <see cref="ErrorTypeSymbol"/>, there may have been symbols that seemed to
    /// be what the user intended, but were unsuitable. For example, a type might have been
    /// inaccessible, or ambiguous. This property returns the possible symbols that the user
    /// might have intended. It will return no symbols if no possible symbols were found.
    /// See the <see cref="CandidateReason"/> property to understand why the symbols were unsuitable.
    /// </remarks>
    public virtual ImmutableArray<Symbol> CandidateSymbols
    {
        get
        {
            return ImmutableArray<Symbol>.Empty;
        }
    }

    ///<summary>
    /// If <see cref="CandidateSymbols"/> returns one or more symbols, returns the reason that those
    /// symbols were not chosen. Otherwise, returns None.
    /// </summary>
    public CandidateReason CandidateReason
    {
        get
        {
            if (!CandidateSymbols.IsEmpty)
            {
                Debug.Assert(ResultKind != LookupResultKind.Viable, "Shouldn't have viable result kind on error symbol");
                return ResultKind.ToCandidateReason();
            }
            else
            {
                return CandidateReason.None;
            }
        }
    }
}

partial class ErrorTypeSymbol
{
#warning 未完成
    public override string Name => base.Name;

    public override string MetadataName => base.MetadataName;

    public override int MetadataToken => base.MetadataToken;

    public override SymbolKind Kind => throw new NotImplementedException();

    public override Symbol? ContainingSymbol => throw new NotImplementedException();

    public override ModuleSymbol? ContainingModule => base.ContainingModule;

    public override AssemblySymbol? ContainingAssembly => base.ContainingAssembly;

    public override Accessibility DeclaredAccessibility => throw new NotImplementedException();

    public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences => throw new NotImplementedException();

    public override ThisCompilation? DeclaringCompilation => base.DeclaringCompilation;

    public override bool IsImplicitlyDeclared => base.IsImplicitlyDeclared;

    public override ImmutableArray<Location> Locations => throw new NotImplementedException();

    public override bool IsStatic => throw new NotImplementedException();

    public override bool IsVirtual => throw new NotImplementedException();

    public override bool IsOverride => throw new NotImplementedException();

    public override bool IsAbstract => throw new NotImplementedException();

    public override bool IsSealed => throw new NotImplementedException();

    public override bool IsExtern => throw new NotImplementedException();

    public override bool HasUnsupportedMetadata => base.HasUnsupportedMetadata;

    public override NamedTypeSymbol? ContainingType => base.ContainingType;

    public override NamedTypeSymbol? EnumUnderlyingType => base.EnumUnderlyingType;

    protected override TypeKind TypeKindCore => throw new NotImplementedException();

    protected override SpecialType SpecialTypeCore => base.SpecialTypeCore;

    protected override bool IsReferenceTypeCore => throw new NotImplementedException();

    protected override bool IsValueTypeCore => throw new NotImplementedException();

    protected override TypeSymbol OriginalTypeSymbolDefinition => base.OriginalTypeSymbolDefinition;

    internal override ModuleSymbol? ContainingNamespace => base.ContainingNamespace;

    internal override NetmoduleSymbol? ContainingNetmodule => base.ContainingNetmodule;

    internal override bool RequiresCompletion => base.RequiresCompletion;

    public override ImmutableArray<FieldSymbol> GetFieldMembers()
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<FieldSymbol> GetFieldMembers(string name)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<FieldSymbol> GetFieldMembers(string name, int arity)
    {
        throw new NotImplementedException();
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override ImmutableArray<ModuleSymbol> GetMembers()
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<ModuleSymbol> GetMembers(string name)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<NamedTypeSymbol> GetTypeMembers()
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
    {
        return base.GetTypeMembers(name, arity);
    }

    protected override ISymbol CreateISymbol()
    {
        return base.CreateISymbol();
    }

    protected override ITypeSymbol CreateITypeSymbol(NullableAnnotation nullableAnnotation)
    {
        return base.CreateITypeSymbol(nullableAnnotation);
    }

    protected override SymbolAdapter GetCciAdapterImpl()
    {
        return base.GetCciAdapterImpl();
    }

    protected override bool IsHighestPriorityUseSiteErrorCode(int code)
    {
        return base.IsHighestPriorityUseSiteErrorCode(code);
    }

    internal override bool Equals(TypeSymbol? other, TypeCompareKind compareKind)
    {
        return base.Equals(other, compareKind);
    }

    internal override void ForceComplete(SourceLocation? location, CancellationToken cancellationToken)
    {
        base.ForceComplete(location, cancellationToken);
    }

    internal override LexicalSortKey GetLexicalSortKey()
    {
        return base.GetLexicalSortKey();
    }

    internal override UseSiteInfo<AssemblySymbol> GetUseSiteInfo()
    {
        return base.GetUseSiteInfo();
    }

    internal override bool HasComplete(CompletionPart part)
    {
        return base.HasComplete(part);
    }

    internal override TypeSymbol MergeEquivalentTypes(TypeSymbol other, VarianceKind variance)
    {
        throw new NotImplementedException();
    }
}
