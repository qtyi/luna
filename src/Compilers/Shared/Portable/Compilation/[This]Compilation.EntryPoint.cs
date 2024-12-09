// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#else
#error Language not supported.
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
        public readonly FieldSymbol? FieldSymbol;
        public readonly ImmutableBindingDiagnostic<AssemblySymbol> Diagnostic;

        public static readonly EntryPoint None = new(null, ImmutableBindingDiagnostic<AssemblySymbol>.Empty);

        public EntryPoint(FieldSymbol? fieldSymbol, ImmutableBindingDiagnostic<AssemblySymbol> diagnostic)
        {
            this.FieldSymbol = fieldSymbol;
            this.Diagnostic = diagnostic;
        }
    }

    internal new FieldSymbol? GetEntryPoint(CancellationToken cancellationToken)
    {
        EntryPoint entryPoint = this.GetEntryPointAndDiagnostics(cancellationToken);
        return entryPoint.FieldSymbol;
    }

    internal EntryPoint GetEntryPointAndDiagnostics(CancellationToken cancellationToken)
    {
        var entryPoint = this._lazyEntryPoint;
        if (entryPoint is null)
        {
#warning 未实现
            throw new NotImplementedException();

            entryPoint = Interlocked.CompareExchange(ref this._lazyEntryPoint, entryPoint, null);
        }

        return entryPoint;
    }
}
