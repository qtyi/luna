﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Reflection.Metadata;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilationOptions = LuaCompilationOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilationOptions = MoonScriptCompilationOptions;
#endif

#warning 未实现。
public sealed partial class
#if LANG_LUA
    LuaCompilation
#elif LANG_MOONSCRIPT
    MoonScriptCompilation
#endif
    : Compilation
{
    private readonly ThisCompilationOptions _options;

    public new ThisCompilationOptions Options => this._options;

    protected override CompilationOptions CommonOptions => this._options;

    public new ImmutableArray<SyntaxTree> SyntaxTrees => throw new NotImplementedException();

    public SemanticModel GetSemanticModel(SyntaxTree syntaxTree, bool ignoreAccessibility) => throw new NotImplementedException();

    public LanguageVersion LanguageVersion { get; }

    public
#if LANG_LUA
        LuaCompilation
#elif LANG_MOONSCRIPT
        MoonScriptCompilation
#endif
        () : base(null, default, null, false, null, null) { }

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
}
