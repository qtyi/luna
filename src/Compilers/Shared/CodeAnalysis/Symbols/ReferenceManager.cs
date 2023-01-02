// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Symbols;
using MSCA::Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisCompilation = LuaCompilation;
using ThisMessageProvider = MessageProvider;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisCompilation = MoonScriptCompilation;
using ThisMessageProvider = MessageProvider;
#endif

using Symbols;
using MetadataOrDiagnostic = System.Object;

partial class
#if LANG_LUA
    LuaCompilation
#elif LANG_MOONSCRIPT
    MoonScriptCompilation
#endif
{
    internal sealed partial class ReferenceManager : CommonReferenceManager<ThisCompilation, AssemblySymbol>
    {
        protected override CommonMessageProvider MessageProvider => ThisMessageProvider.Instance;

        public ReferenceManager(
            string simpleAssemblyName,
            AssemblyIdentityComparer identityComparer,
            Dictionary<MetadataReference, MetadataOrDiagnostic>? observedMetadata) :
            base(simpleAssemblyName, identityComparer, observedMetadata)
        { }

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
