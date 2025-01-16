// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

using Symbols;
using MetadataOrDiagnostic = Object;

partial class
#if LANG_LUA
    LuaCompilation
#elif LANG_MOONSCRIPT
    MoonScriptCompilation
#endif
{
    /// <summary>
    /// <see cref="ThisReferenceManager"/> encapsulates functionality to create an underlying <see cref="SourceAssemblySymbol"/> 
    /// (with underlying <see cref="NetmoduleSymbol"/>) for <see cref="Compilation"/> and <see cref="AssemblySymbol"/>s for referenced
    /// assemblies (with underlying <see cref="NetmoduleSymbol"/>) all properly linked together based on
    /// reference resolution between them.
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="ThisReferenceManager"/> is also responsible for reuse of metadata readers for imported modules
    /// and assemblies as well as existing <see cref="AssemblySymbol"/>s for referenced assemblies. In order
    /// to do that, it maintains global cache for metadata readers and <see cref="AssemblySymbol"/>s
    /// associated with them. The cache uses <see cref="WeakReference{T}"/>s to refer to the metadata readers and
    /// <see cref="AssemblySymbol"/>s to allow memory and resources being reclaimed once they are no longer
    /// used. The tricky part about reusing existing <see cref="AssemblySymbol"/>s is to find a set of
    /// <see cref="AssemblySymbol"/>s that are created for the referenced assemblies, which (the
    /// <see cref="AssemblySymbol"/>s from the set) are linked in a way, consistent with the reference
    /// resolution between the referenced assemblies.
    /// </para>
    /// <para>
    /// When existing <see cref="Compilation"/> is used as a metadata reference, there are scenarios when its
    /// underlying <see cref="SourceAssemblySymbol"/> cannot be used to provide symbols in context of the new
    /// <see cref="Compilation"/>. Consider classic multi-targeting scenario: compilation C1 references v1 of
    /// Lib.dll and compilation C2 references C1 and v2 of Lib.dll. In this case,
    /// <see cref="SourceAssemblySymbol"/> for C1 is linked to <see cref="AssemblySymbol"/> for v1 of Lib.dll. However,
    /// given the set of references for C2, the same reference for C1 should be resolved against
    /// v2 of Lib.dll. In other words, in context of C2, all types from v1 of Lib.dll leaking
    /// through C1 (through method signatures, etc.) must be retargeted to the types from v2 of
    /// Lib.dll. In this case, ReferenceManager creates a special <see cref="RetargetingAssemblySymbol"/> for
    /// C1, which is responsible for the type retargeting. The Retargeting<see cref="AssemblySymbol"/>s could
    /// also be reused for different <see cref="Compilation"/>s, ReferenceManager maintains a cache of
    /// <see cref="RetargetingAssemblySymbol"/>s (<see cref="WeakReference{T}"/>s) for each <see cref="Compilation"/>.
    /// </para>
    /// <para>
    /// The only public entry point of this class is <see cref="CreateSourceAssembly()"/> method.
    /// </para>
    /// </remarks>
    internal sealed partial class ReferenceManager : CommonReferenceManager<ThisCompilation, AssemblySymbol>
    {
        /// <inheritdoc/>
        protected override CommonMessageProvider MessageProvider => ThisMessageProvider.Instance;

        public ReferenceManager(
            string simpleAssemblyName,
            AssemblyIdentityComparer identityComparer,
            Dictionary<MetadataReference, MetadataOrDiagnostic>? observedMetadata)
            : base(simpleAssemblyName, identityComparer, observedMetadata)
        { }

        public void CreateSourceAssemblyForCompilation(ThisCompilation compilation)
        {
#warning 未完成
            throw new NotImplementedException();
        }

        #region 未实现
        protected override bool CheckPropertiesConsistency(MetadataReference primaryReference, MetadataReference duplicateReference, DiagnosticBag diagnostics)
        {
            throw new NotImplementedException();
        }

        protected override AssemblyData CreateAssemblyDataForCompilation(CompilationReference compilationReference)
        {
            throw new NotImplementedException();
        }

        protected override AssemblyData CreateAssemblyDataForFile(PEAssembly assembly, WeakList<IAssemblySymbolInternal> cachedSymbols, DocumentationProvider documentationProvider, string sourceAssemblySimpleName, MetadataImportOptions importOptions, bool embedInteropTypes)
        {
            throw new NotImplementedException();
        }

        protected override void GetActualBoundReferencesUsedBy(AssemblySymbol assemblySymbol, List<AssemblySymbol?> referencedAssemblySymbols)
        {
            throw new NotImplementedException();
        }

        protected override AssemblySymbol? GetCorLibrary(AssemblySymbol candidateAssembly)
        {
            throw new NotImplementedException();
        }

        protected override ImmutableArray<AssemblySymbol> GetNoPiaResolutionAssemblies(AssemblySymbol candidateAssembly)
        {
            throw new NotImplementedException();
        }

        protected override bool IsLinked(AssemblySymbol candidateAssembly)
        {
            throw new NotImplementedException();
        }

        protected override bool WeakIdentityPropertiesEquivalent(AssemblyIdentity identity1, AssemblyIdentity identity2)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
