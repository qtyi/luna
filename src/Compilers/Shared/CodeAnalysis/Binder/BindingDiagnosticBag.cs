// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Collections.Immutable;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.PooledObjects;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

using Symbols;

internal sealed class BindingDiagnosticBag : BindingDiagnosticBag<AssemblySymbol>
{
    public static readonly BindingDiagnosticBag Discarded = new(null, null);

    public BindingDiagnosticBag() : this(usePool: false) { }

    private BindingDiagnosticBag(bool usePool) : base(usePool) { }

    public BindingDiagnosticBag(DiagnosticBag? diagnosticBag) : base(diagnosticBag, dependenciesBag: null) { }

    public BindingDiagnosticBag(DiagnosticBag? diagnosticBag, ICollection<AssemblySymbol>? dependenciesBag) : base(diagnosticBag, dependenciesBag) { }

    internal static BindingDiagnosticBag GetInstance() => new(usePool: true);

    internal static BindingDiagnosticBag GetInstance(bool withDiagnostics, bool withDependencies) =>
        (withDiagnostics, withDependencies) switch
        {
            (true, true) => BindingDiagnosticBag.GetInstance(),
            (true, false) => new(DiagnosticBag.GetInstance()),
            (false, true) => new(diagnosticBag: null, PooledHashSet<AssemblySymbol>.GetInstance()),
            _ => BindingDiagnosticBag.Discarded
        };

    internal static BindingDiagnosticBag GetInstance(BindingDiagnosticBag template) =>
        BindingDiagnosticBag.GetInstance(template.AccumulatesDiagnostics, template.AccumulatesDependencies);

    internal static BindingDiagnosticBag Create(BindingDiagnosticBag template) =>
        (template.AccumulatesDiagnostics, template.AccumulatesDiagnostics) switch
        {
            (true, true) => new(),
            (true, false) => new(new DiagnosticBag()),
            (false, true) => new(diagnosticBag: null, new HashSet<AssemblySymbol>()),
            _ => BindingDiagnosticBag.Discarded
        };

    internal void AddDependencies(Symbol? symbol)
    {
        if (symbol is not null && this.DependenciesBag is not null)
        {
            this.AddDependencies(symbol.GetUseSiteInfo());
        }
    }

    internal bool ReportUseSite(Symbol? symbol, SyntaxNode node) =>
        this.ReportUseSite(symbol, node.Location);

    internal bool ReportUseSite(Symbol? symbol, Location location) =>
        symbol is not null ?
            this.Add(symbol.GetUseSiteInfo(), location) :
            false;

    protected override bool ReportUseSiteDiagnostic(DiagnosticInfo diagnosticInfo, DiagnosticBag diagnosticBag, Location location) =>
        Symbol.ReportUseSiteDiagnostic(diagnosticInfo, diagnosticBag, location);

    internal ThisDiagnosticInfo Add(ErrorCode code, Location location)
    {
        var info = new ThisDiagnosticInfo(code);
        this.Add(info, location);
        return info;
    }

    internal ThisDiagnosticInfo Add(ErrorCode code, Location location, params object[] args)
    {
        var info = new ThisDiagnosticInfo(code, args);
        this.Add(info, location);
        return info;
    }

    internal ThisDiagnosticInfo Add(ErrorCode code, Location location, ImmutableArray<Symbol> symbols, params object[] args)
    {
        var info = new ThisDiagnosticInfo(code, args, symbols, ImmutableArray<Location>.Empty);
        this.Add(info, location);
        return info;
    }

    internal void Add(DiagnosticInfo? info, Location location)
    {
        if (info is not null)
            this.DiagnosticBag?.Add(info, location);
    }
}
