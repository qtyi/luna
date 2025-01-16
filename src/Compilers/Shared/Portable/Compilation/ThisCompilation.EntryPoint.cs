// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

using Symbols;

partial class
#if LANG_LUA
    LuaCompilation
#elif LANG_MOONSCRIPT
    MoonScriptCompilation
#endif
{
    /// <summary>
    /// Contains the main method of this assembly, if there is one.
    /// </summary>
    private EntryPoint? _lazyEntryPoint;

    /// <summary>
    /// Represents a combination of an field symbol, that is the program entry point, and binding diagnostics.
    /// </summary>
    internal class EntryPoint
    {
        public readonly MethodSymbol? MethodSymbol;
        public readonly ReadOnlyBindingDiagnostic<AssemblySymbol> Diagnostic;

        public static readonly EntryPoint None = new(null, ReadOnlyBindingDiagnostic<AssemblySymbol>.Empty);

        public EntryPoint(MethodSymbol? methodSymbol, ReadOnlyBindingDiagnostic<AssemblySymbol> diagnostic)
        {
            MethodSymbol = methodSymbol;
            Diagnostic = diagnostic;
        }
    }

    internal new MethodSymbol? GetEntryPoint(CancellationToken cancellationToken)
    {
        EntryPoint entryPoint = GetEntryPointAndDiagnostics(cancellationToken);
        return entryPoint.MethodSymbol;
    }

    internal EntryPoint GetEntryPointAndDiagnostics(CancellationToken cancellationToken)
    {
        var entryPoint = _lazyEntryPoint;
        if (entryPoint is null)
        {
#warning 未实现
            throw new NotImplementedException();

            entryPoint = Interlocked.CompareExchange(ref _lazyEntryPoint, entryPoint, null);
        }

        return entryPoint;
    }
}
