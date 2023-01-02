// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using System.Diagnostics;
using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;

using ThisDiagnosticInfo = LuaDiagnosticInfo;
using ThisReferenceManager = LuaCompilation.ReferenceManager;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;

using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
using ThisReferenceManager = MoonScriptCompilation.ReferenceManager;
#endif

/// <summary>
/// 一种特殊的<see cref="NetModuleSymbol"/>，表示未缺失的.NET模块符号（与<see cref="MissingNetModuleSymbol"/>相对）。
/// </summary>
internal abstract partial class NonMissingNetModuleSymbol : NetModuleSymbol
{
    /// <summary>此模块直接引用的程序集符号集合。</summary>
    /// <remarks>
    /// 返回的数组及内容均由<see cref="ThisReferenceManager"/>产生，且不应被修改。</remarks>
    private ModuleReferences<AssemblySymbol>? _moduleReferences;

    internal sealed override bool IsMissing => false;

    public sealed override ImmutableArray<AssemblyIdentity> ReferencedAssemblies
    {
        get
        {
            Debug.Assert(this._moduleReferences is not null);
            return this._moduleReferences.Identities;
        }
    }

    public sealed override ImmutableArray<AssemblySymbol> ReferencedAssemblySymbols
    {
        get
        {
            Debug.Assert(this._moduleReferences is not null);
            return this._moduleReferences.Symbols;
        }
    }

    internal ImmutableArray<UnifiedAssembly<AssemblySymbol>> GetUnifiedAssemblies()
    {
        Debug.Assert(this._moduleReferences is not null);
        return this._moduleReferences.UnifiedAssemblies;
    }

    internal override bool HasUnifiedReferences => this.GetUnifiedAssemblies().Length > 0;

    internal override bool GetUnificationUseSiteDiagnostic(ref DiagnosticInfo result, TypeSymbol dependentType)
    {
        Debug.Assert(this._moduleReferences is not null);

        var ownerModule = this;
        var ownerAssembly = ownerModule.ContainingAssembly;
        var dependentAssembly = dependentType.ContainingAssembly;
        if (object.ReferenceEquals(ownerAssembly, dependentAssembly)) return false;

        foreach (var unifiedAssembly in this.GetUnifiedAssemblies())
        {
            if (!object.ReferenceEquals(unifiedAssembly.TargetAssembly, dependentAssembly)) continue;

            var referenceId = unifiedAssembly.OriginalReference;
            var definitionId = dependentAssembly.Identity;
            var involvedAssemblies = ImmutableArray.Create<Symbol>(ownerAssembly, dependentAssembly);

            DiagnosticInfo info;
            if (definitionId.Version > referenceId.Version)
            {
                var warning = (definitionId.Version.Major == referenceId.Version.Major && definitionId.Version.Minor == referenceId.Version.Minor) ?
                    ErrorCode.WRN_UnifyReferenceBldRev : ErrorCode.WRN_UnifyReferenceMajMin;

                info = new ThisDiagnosticInfo(
                    code: warning,
                    args: new object[]
                    {
                        referenceId.GetDisplayName(),
                        ownerAssembly.Name,
                        definitionId.GetDisplayName(),
                        dependentAssembly.Name
                    },
                    symbols: involvedAssemblies,
                    additionalLocations: ImmutableArray<Location>.Empty);
            }
            else
            {
                info = new ThisDiagnosticInfo(
                    code: ErrorCode.ERR_AssemblyMatchBadVersion,
                    args: new object[]
                    {
                        ownerAssembly.Name,
                        ownerAssembly.Identity.GetDisplayName(),
                        referenceId.GetDisplayName(),
                        dependentAssembly.Name,
                        definitionId.GetDisplayName()
                    },
                    symbols: involvedAssemblies,
                    additionalLocations: ImmutableArray<Location>.Empty);
            }

            if (this.MergeUseSiteDiagnostics(ref result, info)) return true;
        }

        return false;
    }

    internal override void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol? originatingSourceAssemblyDebugOnly = null)
    {
        Debug.Assert(this._moduleReferences is null);
        Interlocked.CompareExchange(ref this._moduleReferences, moduleReferences, null);
    }
}
