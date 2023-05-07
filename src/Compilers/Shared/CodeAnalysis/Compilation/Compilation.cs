// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilation = LuaCompilation;
using ThisCompilationOptions = LuaCompilationOptions;
using ThisSyntaxTree = LuaSyntaxTree;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilation = MoonScriptCompilation;
using ThisCompilationOptions = MoonScriptCompilationOptions;
using ThisSyntaxTree = MoonScriptSyntaxTree;
#endif

using Symbols;

#warning 未实现。
/// <summary>
/// The compilation object is an immutable representation of a single invocation of the
/// compiler. Although immutable, a compilation is also on-demand, and will realize and cache
/// data as necessary. A compilation can produce a new compilation from existing compilation
/// with the application of small deltas. In many cases, it is more efficient than creating a
/// new compilation from scratch, as the new compilation can reuse information from the old
/// compilation.
/// </summary>
public sealed partial class
#if LANG_LUA
    LuaCompilation
#elif LANG_MOONSCRIPT
    MoonScriptCompilation
#endif
    : Compilation
{
    private readonly ThisCompilationOptions _options;

    /// <summary>
    /// Gets the options the compilation was created with.
    /// </summary>
    /// <value>
    /// An object that is the options of this compilation.
    /// </value>
    public new ThisCompilationOptions Options => this._options;

    /// <summary>
    /// The <see cref="SourceAssemblySymbol"/> for this compilation. Do not access directly, use Assembly property
    /// instead. This field is lazily initialized by <see cref="ReferenceManager"/>, <see cref="ReferenceManager.CacheLockObject"/> must be locked
    /// while <see cref="ReferenceManager"/> "calculates" the value and assigns it, several threads must not perform duplicate
    /// "calculation" simultaneously.
    /// </summary>
    private SourceAssemblySymbol? _lazyAssemblySymbol;

    /// <summary>
    /// Holds onto data related to reference binding.
    /// The manager is shared among multiple compilations that we expect to have the same result of reference binding.
    /// In most cases this can be determined without performing the binding. If the compilation however contains a circular
    /// metadata reference (a metadata reference that refers back to the compilation) we need to avoid sharing of the binding results.
    /// We do so by creating a new reference manager for such compilation.
    /// </summary>
    private ReferenceManager _referenceManager;

    private readonly SyntaxAndDeclarationManager _syntaxAndDeclarations;

    /// <summary>
    /// Contains the main method of this assembly, if there is one.
    /// </summary>
    private EntryPoint? _lazyEntryPoint;

    #region Language Version and Features
    /// <summary>
    /// Gets the language version that was used to parse the syntax trees of this compilation.
    /// </summary>
    /// <value>
    /// The language version that was used to parse the syntax trees of this compilation.
    /// </value>
    public LanguageVersion LanguageVersion { get; }

    /// <summary>
    /// Gets a value indicating whether the compiler is run in "strict" mode, in which it enforces the language specification
    /// in some cases even at the expense of full compatibility. Such differences typically arise when
    /// earlier versions of the compiler failed to enforce the full language specification.
    /// </summary>
    /// <value>
    /// <see langword="true"/> when the compiler is run in "strict" mode; otherwise, <see langword="false"/>.
    /// </value>
    internal bool FeatureStrictEnabled => this.Feature("strict") is not null;
    #endregion

    #region Constructors and Factories
    /// <summary>The default compilation options - to compile as a console application.</summary>
    private static readonly ThisCompilationOptions s_defaultOptions = new(OutputKind.ConsoleApplication);
    /// <summary>The default compilation options of submission - to compile as a dynamically linked library.</summary>
    private static readonly ThisCompilationOptions s_defaultSubmissionOptions = new ThisCompilationOptions(OutputKind.DynamicallyLinkedLibrary).WithReferencesSupersedeLowerVersions(true);

    private
#if LANG_LUA
        LuaCompilation
#elif LANG_MOONSCRIPT
        MoonScriptCompilation
#endif
    (
        string? assemblyName,
        ThisCompilationOptions options,
        ImmutableArray<MetadataReference> references,
        ThisCompilation? previousSubmission,
        Type? submissionReturnType,
        Type? hostObjectType,
        bool isSubmission,
        ReferenceManager? referenceManager,
        bool reuseReferenceManager,
        SyntaxAndDeclarationManager syntaxAndDeclarations,
        SemanticModelProvider? semanticModelProvider,
        AsyncQueue<CompilationEvent>? eventQueue = null) :
        this(
            assemblyName,
            options,
            references,
            previousSubmission,
            submissionReturnType,
            hostObjectType,
            isSubmission,
            referenceManager,
            reuseReferenceManager,
            syntaxAndDeclarations,
            ThisCompilation.SyntaxTreeCommonFeatures(syntaxAndDeclarations.ExternalSyntaxTrees),
            semanticModelProvider,
            eventQueue)
    { }

    private
#if LANG_LUA
        LuaCompilation
#elif LANG_MOONSCRIPT
        MoonScriptCompilation
#endif
    (
        string? assemblyName,
        ThisCompilationOptions options,
        ImmutableArray<MetadataReference> references,
        ThisCompilation? previousSubmission,
        Type? submissionReturnType,
        Type? hostObjectType,
        bool isSubmission,
        ReferenceManager? referenceManager,
        bool reuseReferenceManager,
        SyntaxAndDeclarationManager syntaxAndDeclarations,
        IReadOnlyDictionary<string, string> features,
        SemanticModelProvider? semanticModelProvider,
        AsyncQueue<CompilationEvent>? eventQueue = null) :
        base(assemblyName, references, features, isSubmission, semanticModelProvider, eventQueue);

    /// <summary>
    /// Creates a new compilation from scratch. Methods such as <see cref="AddSyntaxTrees(IEnumerable{SyntaxTree})"/> or <see cref="AddReferences"/>
    /// on the returned object will allow to continue building up the compilation incrementally.
    /// </summary>
    /// <param name="assemblyName">Simple assembly name.</param>
    /// <param name="syntaxTrees">The syntax trees with the source code for the new compilation.</param>
    /// <param name="references">The references for the new compilation.</param>
    /// <param name="options">The compiler options to use.</param>
    /// <returns>A new compilation.</returns>
    public static ThisCompilation Create(
        string? assemblyName,
        IEnumerable<SyntaxTree>? syntaxTrees = null,
        IEnumerable<MetadataReference>? references = null,
        ThisCompilationOptions? options = null) =>
        ThisCompilation.Create(
            assemblyName,
            options ?? ThisCompilation.s_defaultOptions,
            syntaxTrees,
            references,
            previousSubmission: null,
            returnType: null,
            hostObjectType: null,
            isSubmission: false);

    private static ThisCompilation Create(
        string? assemblyName,
        ThisCompilationOptions options,
        IEnumerable<SyntaxTree>? syntaxTrees,
        IEnumerable<MetadataReference>? references,
        ThisCompilation? previousSubmission,
        Type? returnType,
        Type? hostObjectType,
        bool isSubmission)
    {
        Debug.Assert(!isSubmission || options.ReferencesSupersedeLowerVersions);
    }
    #endregion

    #region Syntax Trees (maintain an ordered list)
    /// <summary>
    /// Gets the syntax trees (parsed from source code) that this compilation was created with.
    /// </summary>
    /// <value>
    /// The syntax trees that this compilation was created with.
    /// </value>
    public new ImmutableArray<SyntaxTree> SyntaxTrees => this._syntaxAndDeclarations.GetLazyState().SyntaxTrees;

    public new bool ContainsSyntaxTree(SyntaxTree? syntaxTree)
    {
        return syntaxTree != null && _syntaxAndDeclarations.GetLazyState().Modules.ContainsKey(syntaxTree);
    }
    #endregion

    #region Compilation
    protected override CompilationOptions CommonOptions => this._options;

    public new SemanticModel GetSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility) => throw new NotImplementedException();

    public override ImmutableArray<MetadataReference> DirectiveReferences => throw new NotImplementedException();

    public override IEnumerable<AssemblyIdentity> ReferencedAssemblyNames => throw new NotImplementedException();

    protected internal override ImmutableArray<SyntaxTree> CommonSyntaxTrees => throw new NotImplementedException();

    protected override IAssemblySymbol CommonAssembly => throw new NotImplementedException();

    protected override IModuleSymbol CommonSourceModule => throw new NotImplementedException();

    protected override INamespaceSymbol CommonGlobalNamespace => throw new NotImplementedException();

    protected override INamedTypeSymbol CommonObjectType => throw new NotImplementedException();

    protected override ITypeSymbol CommonDynamicType => throw new NotImplementedException();

    protected override ITypeSymbol? CommonScriptGlobalsType => throw new NotImplementedException();

    protected override INamedTypeSymbol? CommonScriptClass => throw new NotImplementedException();

    internal override ScriptCompilationInfo? CommonScriptCompilationInfo => throw new NotImplementedException();

    internal override IEnumerable<ReferenceDirective> ReferenceDirectives => throw new NotImplementedException();

    internal override IDictionary<(string path, string content), MetadataReference> ReferenceDirectiveMap => throw new NotImplementedException();

    internal override CommonAnonymousTypeManager CommonAnonymousTypeManager => throw new NotImplementedException();

    internal override CommonMessageProvider MessageProvider => throw new NotImplementedException();

    internal override byte LinkerMajorVersion => throw new NotImplementedException();

    internal override bool IsDelaySigned => throw new NotImplementedException();

    internal override StrongNameKeys StrongNameKeys => throw new NotImplementedException();

    internal override Guid DebugSourceDocumentLanguageId => throw new NotImplementedException();

    public override CommonConversion ClassifyCommonConversion(ITypeSymbol source, ITypeSymbol destination)
    {
        throw new NotImplementedException();
    }

    public override bool ContainsSymbolsWithName(Func<string, bool> predicate, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override bool ContainsSymbolsWithName(string name, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<Diagnostic> GetDeclarationDiagnostics(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<Diagnostic> GetDiagnostics(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<Diagnostic> GetMethodBodyDiagnostics(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<Diagnostic> GetParseDiagnostics(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<ISymbol> GetSymbolsWithName(Func<string, bool> predicate, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override IEnumerable<ISymbol> GetSymbolsWithName(string name, SymbolFilter filter = SymbolFilter.TypeAndMember, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override ImmutableArray<MetadataReference> GetUsedAssemblyReferences(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public override CompilationReference ToMetadataReference(ImmutableArray<string> aliases = default, bool embedInteropTypes = false)
    {
        throw new NotImplementedException();
    }

    protected override void AppendDefaultVersionResource(Stream resourceStream)
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonAddSyntaxTrees(IEnumerable<SyntaxTree> trees)
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonClone()
    {
        throw new NotImplementedException();
    }

    protected override bool CommonContainsSyntaxTree(SyntaxTree? syntaxTree)
    {
        throw new NotImplementedException();
    }

    protected override INamedTypeSymbol CommonCreateAnonymousTypeSymbol(ImmutableArray<ITypeSymbol> memberTypes, ImmutableArray<string> memberNames, ImmutableArray<Location> memberLocations, ImmutableArray<bool> memberIsReadOnly, ImmutableArray<NullableAnnotation> memberNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    protected override IArrayTypeSymbol CommonCreateArrayTypeSymbol(ITypeSymbol elementType, int rank, NullableAnnotation elementNullableAnnotation)
    {
        throw new NotImplementedException();
    }

    protected override INamespaceSymbol CommonCreateErrorNamespaceSymbol(INamespaceSymbol container, string name)
    {
        throw new NotImplementedException();
    }

    protected override INamedTypeSymbol CommonCreateErrorTypeSymbol(INamespaceOrTypeSymbol? container, string name, int arity)
    {
        throw new NotImplementedException();
    }

    protected override IFunctionPointerTypeSymbol CommonCreateFunctionPointerTypeSymbol(ITypeSymbol returnType, RefKind returnRefKind, ImmutableArray<ITypeSymbol> parameterTypes, ImmutableArray<RefKind> parameterRefKinds, SignatureCallingConvention callingConvention, ImmutableArray<INamedTypeSymbol> callingConventionTypes)
    {
        throw new NotImplementedException();
    }

    protected override INamedTypeSymbol CommonCreateNativeIntegerTypeSymbol(bool signed)
    {
        throw new NotImplementedException();
    }

    protected override IPointerTypeSymbol CommonCreatePointerTypeSymbol(ITypeSymbol elementType)
    {
        throw new NotImplementedException();
    }

    protected override INamedTypeSymbol CommonCreateTupleTypeSymbol(ImmutableArray<ITypeSymbol> elementTypes, ImmutableArray<string?> elementNames, ImmutableArray<Location?> elementLocations, ImmutableArray<NullableAnnotation> elementNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    protected override INamedTypeSymbol CommonCreateTupleTypeSymbol(INamedTypeSymbol underlyingType, ImmutableArray<string?> elementNames, ImmutableArray<Location?> elementLocations, ImmutableArray<NullableAnnotation> elementNullableAnnotations)
    {
        throw new NotImplementedException();
    }

    protected override ISymbol? CommonGetAssemblyOrModuleSymbol(MetadataReference reference)
    {
        throw new NotImplementedException();
    }

    protected override INamespaceSymbol? CommonGetCompilationNamespace(INamespaceSymbol namespaceSymbol)
    {
        throw new NotImplementedException();
    }

    protected override IMethodSymbol? CommonGetEntryPoint(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    protected override SemanticModel CommonGetSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility)
    {
        throw new NotImplementedException();
    }

    protected override INamedTypeSymbol? CommonGetTypeByMetadataName(string metadataName)
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonRemoveAllSyntaxTrees()
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonRemoveSyntaxTrees(IEnumerable<SyntaxTree> trees)
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonReplaceSyntaxTree(SyntaxTree oldTree, SyntaxTree newTree)
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonWithAssemblyName(string? outputName)
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonWithOptions(CompilationOptions options)
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonWithReferences(IEnumerable<MetadataReference> newReferences)
    {
        throw new NotImplementedException();
    }

    protected override Compilation CommonWithScriptCompilationInfo(ScriptCompilationInfo? info)
    {
        throw new NotImplementedException();
    }

    internal override void AddDebugSourceDocumentsForChecksumDirectives(DebugDocumentsBuilder documentsBuilder, SyntaxTree tree, DiagnosticBag diagnostics)
    {
        throw new NotImplementedException();
    }

    internal override IConvertibleConversion ClassifyConvertibleConversion(IOperation source, ITypeSymbol destination, out ConstantValue? constantValue)
    {
        throw new NotImplementedException();
    }

    internal override CommonReferenceManager CommonGetBoundReferenceManager()
    {
        throw new NotImplementedException();
    }

    internal override ISymbolInternal CommonGetSpecialTypeMember(SpecialMember specialMember)
    {
        throw new NotImplementedException();
    }

    internal override ITypeSymbolInternal CommonGetWellKnownType(WellKnownType wellknownType)
    {
        throw new NotImplementedException();
    }

    internal override ISymbolInternal? CommonGetWellKnownTypeMember(WellKnownMember member)
    {
        throw new NotImplementedException();
    }

    internal override int CompareSourceLocations(Location loc1, Location loc2)
    {
        throw new NotImplementedException();
    }

    internal override int CompareSourceLocations(SyntaxReference loc1, SyntaxReference loc2)
    {
        throw new NotImplementedException();
    }

    internal override int CompareSourceLocations(SyntaxNode loc1, SyntaxNode loc2)
    {
        throw new NotImplementedException();
    }

    internal override bool CompileMethods(CommonPEModuleBuilder moduleBuilder, bool emittingPdb, bool emitMetadataOnly, bool emitTestCoverageData, DiagnosticBag diagnostics, Predicate<ISymbolInternal>? filterOpt, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal override void CompleteTrees(SyntaxTree? filterTree)
    {
        throw new NotImplementedException();
    }

    internal override AnalyzerDriver CreateAnalyzerDriver(ImmutableArray<DiagnosticAnalyzer> analyzers, AnalyzerManager analyzerManager, SeverityFilter severityFilter)
    {
        throw new NotImplementedException();
    }

    internal override CommonPEModuleBuilder? CreateModuleBuilder(EmitOptions emitOptions, IMethodSymbol? debugEntryPoint, Stream? sourceLinkStream, IEnumerable<EmbeddedText>? embeddedTexts, IEnumerable<ResourceDescription>? manifestResources, CompilationTestData? testData, DiagnosticBag diagnostics, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal override SemanticModel CreateSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility)
    {
        throw new NotImplementedException();
    }

    internal override EmitDifferenceResult EmitDifference(EmitBaseline baseline, IEnumerable<SemanticEdit> edits, Func<ISymbol, bool> isAddedSymbol, Stream metadataStream, Stream ilStream, Stream pdbStream, CompilationTestData? testData, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal override bool GenerateDocumentationComments(Stream? xmlDocStream, string? outputNameOverride, DiagnosticBag diagnostics, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal override bool GenerateResources(CommonPEModuleBuilder moduleBuilder, Stream? win32Resources, bool useRawWin32Resources, DiagnosticBag diagnostics, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal override void GetDiagnostics(CompilationStage stage, bool includeEarlierStages, DiagnosticBag diagnostics, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    internal override int GetSyntaxTreeOrdinal(SyntaxTree tree)
    {
        throw new NotImplementedException();
    }

    internal override bool HasCodeToEmit()
    {
        throw new NotImplementedException();
    }

    internal override bool HasSubmissionResult()
    {
        throw new NotImplementedException();
    }

    internal override bool IsAttributeType(ITypeSymbol type)
    {
        throw new NotImplementedException();
    }

    internal override bool IsSystemTypeReference(ITypeSymbolInternal type)
    {
        throw new NotImplementedException();
    }

    internal override bool IsUnreferencedAssemblyIdentityDiagnosticCode(int code)
    {
        throw new NotImplementedException();
    }

    internal override void ReportUnusedImports(DiagnosticBag diagnostics, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    internal override void SerializePdbEmbeddedCompilationOptions(BlobBuilder builder)
    {
        throw new NotImplementedException();
    }

    internal override void ValidateDebugEntryPoint(IMethodSymbol debugEntryPoint, DiagnosticBag diagnostics)
    {
        throw new NotImplementedException();
    }

    internal override Compilation WithEventQueue(AsyncQueue<CompilationEvent>? eventQueue)
    {
        throw new NotImplementedException();
    }

    internal override Compilation WithSemanticModelProvider(SemanticModelProvider semanticModelProvider)
    {
        throw new NotImplementedException();
    }

    private protected override MetadataReference? CommonGetMetadataReference(IAssemblySymbol assemblySymbol)
    {
        throw new NotImplementedException();
    }

    private protected override INamedTypeSymbolInternal CommonGetSpecialType(SpecialType specialType)
    {
        throw new NotImplementedException();
    }

    private protected override bool IsSymbolAccessibleWithinCore(ISymbol symbol, ISymbol within, ITypeSymbol? throughType)
    {
        throw new NotImplementedException();
    }

    protected override IMethodSymbol CommonCreateBuiltinOperator(string name, ITypeSymbol returnType, ITypeSymbol leftType, ITypeSymbol rightType)
    {
        throw new NotImplementedException();
    }

    protected override IMethodSymbol CommonCreateBuiltinOperator(string name, ITypeSymbol returnType, ITypeSymbol operandType)
    {
        throw new NotImplementedException();
    }
    #endregion
}
