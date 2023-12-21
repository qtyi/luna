// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Qtyi.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilation = LuaCompilation;
using ThisDiagnosticInfo = LuaDiagnosticInfo;
using ThisSemanticModel = LuaSemanticModel;
using ThisSymbolVisitor = LuaSymbolVisitor;
using ThisSymbolVisitor<TResult> = LuaSymbolVisitor<TResult>;
using ThisSymbolVisitor<TResult, TArgument> = LuaSymbolVisitor<TResult, TArgument>;
using ThisSyntaxNode = LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilation = MoonScriptCompilation;
using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
using ThisSemanticModel = MoonScriptSemanticModel;
using ThisSymbolVisitor = MoonScriptSymbolVisitor;
using ThisSymbolVisitor<TResult> = MoonScriptSymbolVisitor<TResult>;
using ThisSymbolVisitor<TResult, TArgument> = MoonScriptSymbolVisitor<TResult, TArgument>;
using ThisSyntaxNode = MoonScriptSyntaxNode;
#else
#error Not implemented
#endif

using Symbols;

/// <summary>
/// The base class for all symbols (module, type, field, parameter, etc.) that are 
/// exposed by the compiler.
/// </summary>
[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal abstract partial class Symbol : ISymbolInternal, IFormattable
{
    private ISymbol? _lazyISymbol;

    /// <summary>
    /// Gets the name of this symbol.
    /// </summary>
    /// <value>
    /// Symbols without a name return the empty string; <see langword="null"/> is
    /// never returned.
    /// </value>
    public virtual string Name => string.Empty;

    /// <summary>
    /// Gets the name of a symbol as it appears in metadata.
    /// </summary>
    /// <value>
    /// Most of the time, this is the same as the Name property, with the following exceptions:
    /// 1) The metadata name of generic types includes the "`1", "`2" etc. suffix that
    /// indicates the number of type parameters (it does not include, however, names of
    /// containing types or namespaces).
    /// 2) The metadata name of explicit interface names have spaces removed, compared to
    /// the name property.
    /// </value>
    public virtual string MetadataName => this.Name;

    /// <summary>
    /// Gets the token for this symbol as it appears in metadata.
    /// </summary>
    /// <value>
    /// Most of the time this is 0, as it is when the symbol is not loaded from metadata.
    /// </value>
    public virtual int MetadataToken => 0;

    /// <summary>
    /// Gets the kind of this symbol.
    /// </summary>
    /// <value>
    /// A <see cref="SymbolKind"/> value represent the kind of this symbol.
    /// </value>
    public abstract SymbolKind Kind { get; }

    #region Containing
    /// <summary>
    /// Get the symbol that logically contains this symbol. 
    /// </summary>
    /// <value>
    /// Returns the symbol containing this symbol.  If this symbol doesn't belong to any
    /// other symbol, returns <see langword="null"/>.
    /// </value>
    public abstract Symbol? ContainingSymbol { get; }

    /// <summary>
    /// Get the named type symbol that logically contains this symbol. 
    /// </summary>
    /// <value>
    /// Returns the named type symbol containing this symbol.  If this symbol doesn't
    /// belong to any named type symbol, returns <see langword="null"/>.
    /// </value>
    public virtual NamedTypeSymbol? ContainingType =>
        // PERF: Derived class should provider more efficient implementation.
        this.GetContainingSymbolHelper<NamedTypeSymbol>();

    /// <summary>
    /// Get the module symbol that logically contains this symbol.
    /// </summary>
    /// <value>
    /// Returns the module symbol containing this symbol.  If this symbol doesn't belong
    /// to any module symbol, returns <see langword="null"/>.
    /// </value>
    public virtual ModuleSymbol? ContainingModule =>
        // PERF: Derived class should provider more efficient implementation.
        this.GetContainingSymbolHelper<ModuleSymbol>();

    /// <summary>
    /// Get the module symbol that represent the .NET namespace that logically contains
    /// this symbol.
    /// </summary>
    /// <value>
    /// Returns the module symbol that represent the .NET namespace that containing this
    /// symbol.  If this symbol doesn't belong to any .NET namespace, returns <see langword="null"/>.
    /// </value>
    internal virtual ModuleSymbol? ContainingNamespace =>
        // PERF: Derived class should provider more efficient implementation.
        this.GetContainingSymbolHelper<ModuleSymbol>(static module => module.IsNamespace);

    /// <summary>
    /// Get the module symbol that represent the .NET module that logically contains
    /// this symbol.
    /// </summary>
    /// <value>
    /// Returns the .NET module symbol that containing this symbol.  If this symbol
    /// doesn't belong to any .NET module, returns <see langword="null"/>.
    /// </value>
    internal virtual NetmoduleSymbol? ContainingNetmodule =>
        // PERF: Derived class should provider more efficient implementation.
        this.ContainingSymbol?.ContainingNetmodule;

    /// <summary>
    /// Get the assembly symbol that logically contains this symbol.
    /// </summary>
    /// <value>
    /// Returns the assembly symbol containing this symbol.  If this symbol doesn't
    /// belong to any assembly symbol, returns <see langword="null"/>.
    /// </value>
    public virtual AssemblySymbol? ContainingAssembly =>
        // PERF: Derived class should provider more efficient implementation.
        this.ContainingSymbol?.ContainingAssembly;

    /// <summary>
    /// Helper method to get the first symbol containing this symbol.
    /// </summary>
    /// <typeparam name="TSymbol">Type of symbol to get.</typeparam>
    /// <param name="predicate">Check if symbol is match.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private TSymbol? GetContainingSymbolHelper<TSymbol>(Func<TSymbol, bool>? predicate = null) where TSymbol : Symbol
    {
        for (var container = this.ContainingSymbol; container is not null; container = container.ContainingSymbol)
        {
            if (container is TSymbol symbol &&
                (predicate is null || predicate(symbol)))
                return symbol;
        }
        return null;
    }
    #endregion

    #region Declaring
    /// <summary>
    /// Gets this accessibility that was declared on this symbol.
    /// </summary>
    /// <value>
    /// For symbols that do not have accessibility declared on them, returns <see cref="Accessibility.NotApplicable"/>.
    /// </value>
    public abstract Accessibility DeclaredAccessibility { get; }

    /// <summary>
    /// Gets the syntax node(s) where this symbol was declared in source.
    /// </summary>
    /// <value>
    /// The syntax node(s) that declared the symbol. If the symbol was declared in metadata or
    /// was implicitly declared, returns an empty read-only array.
    /// </value>
    /// <remarks>
    /// <para>
    /// Some symbols may be defined in more than one location. This property should return
    /// one or more syntax nodes only if the symbol was declared in source code and also
    /// was not implicitly declared (see the <see cref="IsImplicitlyDeclared"/> property). 
    /// </para>
    /// <para>
    /// Note that for the global module, the declaring syntax will be the <see cref="Syntax.ChunkSyntax"/>.
    /// </para>
    /// <para>
    /// To go the opposite direction (from syntax node to symbol), see <see
    /// cref="ThisSemanticModel.GetDeclaredSymbol(ThisSyntaxNode, CancellationToken)"/>.
    /// </para>
    /// </remarks>
    public abstract ImmutableArray<SyntaxReference> DeclaringSyntaxReferences { get; }

    /// <summary>
    /// Helper for implementing <see cref="DeclaringSyntaxReferences"/> for derived classes that store a location but not a 
    /// <see cref="ThisSyntaxNode"/> or <see cref="SyntaxReference"/>.
    /// </summary>
    /// <param name="locations">A collection of locations where symbol declaring syntaxes at.</param>
    internal static ImmutableArray<SyntaxReference> GetDeclaringSyntaxReferenceHelper<TNode>(ImmutableArray<Location> locations)
        where TNode : ThisSyntaxNode
    {
        if (locations.IsEmpty)
            return ImmutableArray<SyntaxReference>.Empty;

        var builder = ArrayBuilder<SyntaxReference>.GetInstance();
        foreach (var location in locations)
        {
            if (!location.IsInSource) continue;

            if (location.SourceSpan.Length != 0)
            {
                var token = location.SourceTree.GetRoot().FindToken(location.SourceSpan.Start);
                if (token.Kind() != SyntaxKind.None)
                {
                    var node = token.Parent?.FirstAncestorOrSelf<TNode>();
                    if (node is not null)
                        builder.Add(node.GetReference());
                }
            }
            else
            {
                // Since the location we're interested in can't contain a token, we'll inspect the whole tree,
                // pruning away branches that don't contain that location. We'll pick the narrowest node of the type
                // we're looking for.
                var parent = location.SourceTree.GetRoot();
                SyntaxNode? found = null;
                foreach (var descendant in parent.DescendantNodesAndSelf(c => c.Location.SourceSpan.Contains(location.SourceSpan)))
                {
                    if (descendant is TNode && descendant.Location.SourceSpan.Contains(location.SourceSpan))
                        found = descendant;
                }

                if (found is not null)
                    builder.Add(found.GetReference());
            }
        }

        return builder.ToImmutableAndFree();
    }

    /// <summary>
    /// Gets the compilation this symbol declaring in.
    /// </summary>
    /// <value>
    /// For a source assembly, the associated compilation.
    /// For any other assembly, null.
    /// For a source .NET module, the <see cref="SourceAssemblySymbol.DeclaringCompilation"/> of the associated source assembly.
    /// For any other .NET module, null.
    /// For any other symbol, the <see cref="SourceNetmoduleSymbol.DeclaringCompilation"/> of the associated .NET module.
    /// </value>
    /// <remarks>
    /// We're going through the containing .NET module, rather than the containing assembly,
    /// because of /addmodule (symbols in such .NET modules should return null).
    /// 
    /// Remarks, not "ContainingCompilation" because it isn't transitive.
    /// </remarks>
    public virtual ThisCompilation? DeclaringCompilation
    {
        get
        {
            if (!this.IsDefinition)
                return this.OriginalDefinition.DeclaringCompilation;

            switch (this.Kind)
            {
                case SymbolKind.ErrorType:
                    return null;
                case SymbolKind.Assembly:
                    Debug.Assert(this is not SourceAssemblySymbol, $"{nameof(SourceAssemblySymbol)} must override {nameof(DeclaringCompilation)}");
                    return null;
                case SymbolKind.Netmodule:
                    Debug.Assert(this is not SourceNetmoduleSymbol, $"{nameof(SourceNetmoduleSymbol)} must override {nameof(DeclaringCompilation)}");
                    return null;
            }

            switch (this.ContainingNetmodule)
            {
                case SourceNetmoduleSymbol sourceModuleSymbol:
                    return sourceModuleSymbol.DeclaringCompilation;
            }

            return null;
        }
    }

    /// <summary>
    /// Gets a value indicate if this symbol is implicitly declared.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol was automatically created by the
    /// compiler, and does not have an explicit corresponding source code declaration;
    /// otherwise, <see langword="false"/>.
    /// </value>
    /// <remarks>
    /// This is intended for symbols that are ordinary symbols in the language sense,
    /// and may be used by code, but that are simply declared implicitly rather than
    /// with explicit language syntax.
    /// </remarks>
    public virtual bool IsImplicitlyDeclared => false;
    #endregion

    #region Definition
    /// <summary>
    /// Gets the original definition of this symbol.
    /// </summary>
    /// <value>
    /// If this symbol is constructed from another symbol by type substitution then returns
    /// the original symbol as it was defined in source or metadata.
    /// </value>
    public Symbol OriginalDefinition => this.OriginalSymbolDefinition;

    /// <summary>
    /// Gets the original definition of this symbol.
    /// </summary>
    /// <remarks>
    /// Backing property of <see cref="OriginalDefinition"/>.
    /// </remarks>
    protected virtual Symbol OriginalSymbolDefinition => this;

    /// <summary>
    /// Gets a value that indicate if this symbol is the original definition of itself.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this is the original definition of this symbol;
    /// otherwise, <see langword="false"/>.
    /// </value>
    public bool IsDefinition => ReferenceEquals(this, this.OriginalDefinition);
    #endregion

    /// <summary>
    /// Gets the locations where this symbol was originally defined.
    /// </summary>
    /// <value>
    /// The locations where this symbol was originally defined, either in source or
    /// metadata.
    /// </value>
    public abstract ImmutableArray<Location> Locations { get; }

    /// <summary>
    /// Gets a source location key for sorting.
    /// </summary>
    /// <remarks>
    /// <para>
    /// For performance, it's important that this
    /// be able to be returned from a symbol without doing any additional allocations (even
    /// if nothing is cached yet.)
    /// </para>
    /// <para>
    /// Only (original) source symbols and namespaces that can be merged
    /// need implement this function if they want to do so for efficiency.
    /// </para>
    /// </remarks>
    internal virtual LexicalSortKey GetLexicalSortKey()
    {
        var locations = this.Locations;
        var declaringCompilation = this.DeclaringCompilation;
        Debug.Assert(declaringCompilation != null); // require that it is a source symbol
        return (locations.Length > 0) ? new LexicalSortKey(locations[0], declaringCompilation) : LexicalSortKey.NotInSource;
    }

    #region Completion
    /// <summary>
    /// Gets a value indicate if this symbol should be completed by calling <see cref="ForceComplete(SourceLocation?, CancellationToken)"/>.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol should be completed; otherwise, <see langword="false"/>.
    /// Intuitively, <see langword="true"/> for source entities (from any compilation).
    /// </value>
    internal virtual bool RequiresCompletion => false;

    /// <summary>
    /// Force complete this symbol.
    /// </summary>
    /// <param name="location">Location of this symbol. <see langword="null"/> if not specified.</param>
    /// <param name="cancellationToken">取消操作的标记。</param>
    internal virtual void ForceComplete(SourceLocation? location, CancellationToken cancellationToken)
    {
        // must be overridden by source symbols, no-op for other symbols
        Debug.Assert(!this.RequiresCompletion);
    }

    /// <summary>
    /// Has this symbol finished part of completion.
    /// </summary>
    /// <param name="part">Part of completion for searching.</param>
    /// <returns>Returns <see langword="true"/> if the <paramref name="part"/> of completion
    /// has finished; otherwise, <see langword="false"/>.</returns>
    internal virtual bool HasComplete(CompletionPart part)
    {
        // must be overridden by source symbols, no-op for other symbols
        Debug.Assert(!this.RequiresCompletion);
        return true;
    }
    #endregion

    /// <summary>
    /// Gets a value indicate if this symbol is static.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol is static; otherwise, <see langword="false"/>.
    /// </value>
    public abstract bool IsStatic { get; }

    /// <summary>
    /// Gets a value indicate if this symbol is virtual.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol is virtual; otherwise, <see langword="false"/>.
    /// </value>
    public abstract bool IsVirtual { get; }

    /// <summary>
    /// Gets a value indicate if this symbol is override.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol is override; otherwise, <see langword="false"/>.
    /// </value>
    public abstract bool IsOverride { get; }

    /// <summary>
    /// Gets a value indicate if this symbol is abstract.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol is abstract; otherwise, <see langword="false"/>.
    /// </value>
    public abstract bool IsAbstract { get; }

    /// <summary>
    /// Gets a value indicate if this symbol is sealed.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol is sealed; otherwise, <see langword="false"/>.
    /// </value>
    public abstract bool IsSealed { get; }

    /// <summary>
    /// Gets a value indicate if this symbol is extern.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol is extern; otherwise, <see langword="false"/>.
    /// </value>
    public abstract bool IsExtern { get; }

    #region Equality
    /// <summary>
    /// Compare two symbol objects to see if they refer to the same symbol. You should always
    /// use <see cref="operator =="/> and <see cref="operator !="/>, or the <see cref="Equals(object)"/> method, to compare two symbols for equality.
    /// </summary>
    public static bool operator ==(Symbol? left, Symbol? right)
    {
        //PERF: this function is often called with
        //      1) left referencing same object as the right 
        //      2) right being null
        //      The code attempts to check for these conditions before 
        //      resorting to .Equals

        // the condition is expected to be folded when inlining "someSymbol is null"
        if (right is null)
            return left is null;

        // this part is expected to disappear when inlining "someSymbol is null"
        return ReferenceEquals(left, right) || right.Equals(left);
    }

    /// <summary>
    /// Compare two symbol objects to see if they refer to the different symbol. You should always
    /// use == and !=, or the Equals method, to compare two symbols for equality.
    /// </summary>
    public static bool operator !=(Symbol? left, Symbol? right)
    {
        //PERF: this function is often called with
        //      1) left referencing different object as the right 
        //      2) right being null
        //      The code attempts to check for these conditions before 
        //      resorting to .Equals

        // the condition is expected to be folded when inlining "someSymbol is null"
        if (right is null)
            return left is not null;

        // this part is expected to disappear when inlining "someSymbol is null"
        return !ReferenceEquals(left, right) && !right.Equals(left);
    }

    /// <inheritdoc/>
    public sealed override bool Equals(object? obj) => this.Equals(obj as Symbol, SymbolEqualityComparer.Default.CompareKind);

    /// <summary>
    /// Determines whether the specified symbol is equals to the current symbol.
    /// </summary>
    /// <param name="other">The symbol to compare with the current symbol.</param>
    /// <returns>Returns <see langword="true"/> if the specified symbol is equals to the
    /// current symbol; otherwise, <see langword="false"/>.</returns>
    public bool Equals(Symbol? other) => this.Equals(other, SymbolEqualityComparer.Default.CompareKind);

    /// <summary>
    /// Determines whether the specified symbol is equals to the current symbol.  Use
    /// specified kind of comparison to compare between types.
    /// </summary>
    /// <param name="compareKind">The kind of comparison between types.</param>
    /// <inheritdoc cref="Equals(Symbol?)"/>
    public virtual bool Equals(Symbol? other, TypeCompareKind compareKind) =>
        // By default we don't consider the compareKind, and do reference equality. This can be overridden.
        ReferenceEquals(this, other);

    /// <summary>
    /// Determines whether two symbols are equals.
    /// </summary>
    /// <param name="first">The first symbol to compare.</param>
    /// <param name="second">The second symbol to compare.</param>
    /// <param name="compareKind">The kind of comparison between types.</param>
    /// <returns>Returns <see langword="true"/> if two symbols are equals; otherwise,
    /// <see langword="false"/>.</returns>
    public static bool Equals(Symbol? first, Symbol? second, TypeCompareKind compareKind)
    {
        if (first is null)
            return second is null;
        else if (ReferenceEquals(first, second))
            return true;
        else
            return first.Equals(second, compareKind);
    }

    /// <inheritdoc/>
    public override int GetHashCode() => RuntimeHelpers.GetHashCode(this);
    #endregion

    #region Public Symbol
    /// <summary>
    /// Gets a public symbol of this symbol.
    /// </summary>
    /// <value>
    /// The public symbol of this symbol.  Return values should be reference equal on every call.
    /// </value>
    internal ISymbol ISymbol
    {
        get
        {
            if (this._lazyISymbol is null)
                Interlocked.CompareExchange(ref _lazyISymbol, this.CreateISymbol(), null);

            return _lazyISymbol;
        }
    }

    /// <summary>
    /// Backing method for <see cref="ISymbol"/>.
    /// </summary>
    protected abstract ISymbol CreateISymbol();
    #endregion

    #region Accept
    /// <summary>
    /// Entry point for <see cref="ThisSymbolVisitor"/> to visit this symbol.
    /// </summary>
    /// <param name="visitor">The symbol visitor.</param>
    public abstract void Accept(ThisSymbolVisitor visitor);

    /// <summary>
    /// Entry point for <see cref="ThisSymbolVisitor{TResult}"/> to visit this symbol.
    /// </summary>
    /// <param name="visitor">The symbol visitor.</param>
    public abstract TResult? Accept<TResult>(ThisSymbolVisitor<TResult> visitor);

    /// <summary>
    /// Entry point for <see cref="ThisSymbolVisitor{TResult, TArgument}"/> to visit this symbol.
    /// </summary>
    /// <param name="visitor">The symbol visitor.</param>
    internal abstract TResult? Accept<TArgument, TResult>(ThisSymbolVisitor<TArgument, TResult> visitor, TArgument argument);
    #endregion

    #region Use-Site Diagnostics
    /// <summary>
    /// Does the symbol has a use-site diagnostic with error severity.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if the symbol has a use-site diagnostic with error severity; otherwise, <see langword="false"/>.
    /// </value>
    internal bool HasUseSiteError => this.GetUseSiteInfo().DiagnosticInfo?.Severity == DiagnosticSeverity.Error;

    /// <summary>
    /// Returns diagnostic info that should be reported at the use site of the symbol.
    /// </summary>
    /// <value>
    /// Diagnostic info that should be reported at the use site of the symbol, or default if there is none.
    /// </value>
    internal virtual UseSiteInfo<AssemblySymbol> GetUseSiteInfo() => default;

    /// <summary>
    /// Returns the primary dependency of the symbol.
    /// </summary>
    /// <value>
    /// When not-null, this is primary dependency of the use-site, usually the assembly
    /// defining the used symbol. Never a core library.
    /// </value>
    protected AssemblySymbol? PrimaryDependency
    {
        get
        {
            var dependency = this.ContainingAssembly;
            if (dependency is not null && ReferenceEquals(dependency.CorLibrary, dependency))
                return null;

            return dependency;
        }
    }

    /// <summary>
    /// Returns <see langword="true"/> if the error code is the highest priority while calculating use site error for this symbol. 
    /// </summary>
    /// <remarks>
    /// Supposed to be <see cref="ErrorCode"/>, but it causes inconsistent accessibility error.
    /// </remarks>
    protected virtual bool IsHighestPriorityUseSiteErrorCode(int code) => true;

    /// <summary>
    /// Indicates that this symbol uses metadata that cannot be supported by the language.
    /// </summary>
    /// <remarks>
    /// This is distinguished from, for example, references to metadata symbols defined in assemblies that weren't referenced.
    /// Symbols where this returns true can never be used successfully, and thus should never appear in any IDE feature.
    /// </remarks>
    public virtual bool HasUnsupportedMetadata => false;

    /// <summary>
    /// Merges given diagnostic to the existing result diagnostic.
    /// </summary>
    internal bool MergeUseSiteDiagnostics(ref DiagnosticInfo? result, DiagnosticInfo? info)
    {
        if (info == null)
        {
            return false;
        }

        if (info.Severity == DiagnosticSeverity.Error && IsHighestPriorityUseSiteErrorCode(info.Code))
        {
            // this error is final, no other error can override it:
            result = info;
            return true;
        }

        if (result == null || result.Severity == DiagnosticSeverity.Warning && info.Severity == DiagnosticSeverity.Error)
        {
            // there could be an error of higher-priority
            result = info;
            return false;
        }

        // we have a second low-pri error, continue looking for a higher priority one
        return false;
    }

    /// <summary>
    /// Merges given diagnostic and dependencies to the existing result.
    /// </summary>
    internal bool MergeUseSiteInfo(ref UseSiteInfo<AssemblySymbol> result, UseSiteInfo<AssemblySymbol> info)
    {
        var diagnosticInfo = result.DiagnosticInfo;

        var retVal = MergeUseSiteDiagnostics(ref diagnosticInfo, info.DiagnosticInfo);

        if (diagnosticInfo?.Severity == DiagnosticSeverity.Error)
        {
            result = new UseSiteInfo<AssemblySymbol>(diagnosticInfo);
            return retVal;
        }

        var secondaryDependencies = result.SecondaryDependencies;
        var primaryDependency = result.PrimaryDependency;

        info.MergeDependencies(ref primaryDependency, ref secondaryDependencies);

        result = new UseSiteInfo<AssemblySymbol>(diagnosticInfo, primaryDependency, secondaryDependencies);
        Debug.Assert(!retVal);
        return retVal;
    }

    /// <summary>
    /// Reports specified use-site diagnostic to given diagnostic bag. 
    /// </summary>
    /// <remarks>
    /// This method should be the only method adding use-site diagnostics to a diagnostic bag. 
    /// It performs additional adjustments of the location for unification related diagnostics and 
    /// may be the place where to add more use-site location post-processing.
    /// </remarks>
    /// <returns>True if the diagnostic has error severity.</returns>
    internal static bool ReportUseSiteDiagnostic(DiagnosticInfo info, DiagnosticBag diagnostics, Location location)
    {
        diagnostics.Add(info, location);
        return info.Severity == DiagnosticSeverity.Error;
    }

    internal static bool ReportUseSiteDiagnostic(DiagnosticInfo info, BindingDiagnosticBag diagnostics, Location location)
    {
        return diagnostics.ReportUseSiteDiagnostic(info, location);
    }

    /// <summary>
    /// Derive use-site info from a type symbol.
    /// </summary>
    internal bool DeriveUseSiteInfoFromType(ref UseSiteInfo<AssemblySymbol> result, TypeSymbol type)
    {
        var info = type.GetUseSiteInfo();

        return MergeUseSiteInfo(ref result, info);
    }
    #endregion

    /// <remarks>Prevent anyone else from deriving from this class.</remarks>
    internal Symbol() { }

    #region ISymbolInternal
    ISymbolInternal? ISymbolInternal.ContainingSymbol => this.ContainingSymbol;
    INamedTypeSymbolInternal? ISymbolInternal.ContainingType => this.ContainingType;
    IModuleSymbolInternal? ISymbolInternal.ContainingModule => this.ContainingModule;
    INetmoduleSymbolInternal? ISymbolInternal.ContainingNetmodule => this.ContainingNetmodule;
    IAssemblySymbolInternal? ISymbolInternal.ContainingAssembly => this.ContainingAssembly;
    ISymbol ISymbolInternal.GetISymbol() => this.ISymbol;
    bool ISymbolInternal.Equals(ISymbolInternal? other, TypeCompareKind compareKind) => this.Equals(other as Symbol, compareKind);
    #endregion

    #region Microsoft.CodeAnalysis.Symbols.ISymbolInternal
#nullable disable
    Microsoft.CodeAnalysis.SymbolKind Microsoft.CodeAnalysis.Symbols.ISymbolInternal.Kind => (Microsoft.CodeAnalysis.SymbolKind)this.Kind;

    Microsoft.CodeAnalysis.Symbols.ISymbolInternal Microsoft.CodeAnalysis.Symbols.ISymbolInternal.ContainingSymbol => this.ContainingSymbol;
    Microsoft.CodeAnalysis.Symbols.INamespaceSymbolInternal Microsoft.CodeAnalysis.Symbols.ISymbolInternal.ContainingNamespace => this.ContainingNamespace;
    Microsoft.CodeAnalysis.Symbols.INamedTypeSymbolInternal Microsoft.CodeAnalysis.Symbols.ISymbolInternal.ContainingType => this.ContainingType;
    Microsoft.CodeAnalysis.Symbols.IModuleSymbolInternal Microsoft.CodeAnalysis.Symbols.ISymbolInternal.ContainingModule => this.ContainingNetmodule;
    Microsoft.CodeAnalysis.Symbols.IAssemblySymbolInternal Microsoft.CodeAnalysis.Symbols.ISymbolInternal.ContainingAssembly => this.ContainingAssembly;
    Compilation Microsoft.CodeAnalysis.Symbols.ISymbolInternal.DeclaringCompilation => this.DeclaringCompilation;
    Microsoft.CodeAnalysis.ISymbol Microsoft.CodeAnalysis.Symbols.ISymbolInternal.GetISymbol() => this.ISymbol;
    bool Microsoft.CodeAnalysis.Symbols.ISymbolInternal.Equals(Microsoft.CodeAnalysis.Symbols.ISymbolInternal other, TypeCompareKind compareKind) => this.Equals(other as Symbol, compareKind);
#nullable enable
    #endregion

    #region IFormattable
    /*
    public sealed override string ToString() => this.ToDisplayString();

    public string ToDisplayString(SymbolDisplayFormat? format = null) =>
        SymbolDisplay.ToDisplayString(this.ISymbol, format);

    public ImmutableArray<SymbolDisplayPart> ToDisplayParts(SymbolDisplayFormat? format = null) =>
        SymbolDisplay.ToDisplayParts(this.ISymbol, format);

    public string ToMinimalDisplayString(
        SemanticModel semanticModel,
        int position,
        SymbolDisplayFormat? format = null) =>
        SymbolDisplay.ToMinimalDisplayString(this.ISymbol, semanticModel, position, format);

    public ImmutableArray<SymbolDisplayPart> ToMinimalDisplayParts(
        SemanticModel semanticModel,
        int position,
        SymbolDisplayFormat? format = null) =>
        SymbolDisplay.ToMinimalDisplayParts(this.ISymbol, semanticModel, position, format);
    */
    string IFormattable.ToString(string? format, IFormatProvider? formatProvider) => this.ToString();
    #endregion
}
