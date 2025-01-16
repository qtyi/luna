// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.Cci;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

using Symbols;

// Implementations for IReference
internal abstract partial class
#if DEBUG
    SymbolAdapter
#else
    Symbol
#endif
    : IReference
{
    IDefinition IReference.AsDefinition(EmitContext context) => throw ExceptionUtilities.Unreachable();

    Microsoft.CodeAnalysis.Symbols.ISymbolInternal IReference.GetInternalSymbol() => AdaptedSymbol;

    void IReference.Dispatch(MetadataVisitor visitor) => throw ExceptionUtilities.Unreachable();
}

#if DEBUG
/// <summary>
/// Represents an <see cref="Symbol"/> adapter to implement <see cref="IReference"/>.
/// This class only used for debug.
/// </summary>
internal partial class SymbolAdapter
{
    /// <summary>
    /// Gets the symbol adapted by this adapter.
    /// </summary>
    internal abstract Symbol AdaptedSymbol { get; }

    /// <inheritdoc/>
    public sealed override string? ToString() => AdaptedSymbol.ToString();

    /// <remarks>
    /// It is not supported to rely on default equality of these Cci objects, an explicit way to compare and hash them should be used.
    /// </remarks>
    /// <exception cref="Exception">Should be unreachable.</exception>
    /// <inheritdoc/>
    public sealed override bool Equals(object? obj) => throw ExceptionUtilities.Unreachable();

    /// <remarks>
    /// It is not supported to rely on default equality of these Cci objects, an explicit way to compare and hash them should be used.
    /// </remarks>
    /// <exception cref="Exception">Should be unreachable.</exception>
    /// <inheritdoc/>
    public sealed override int GetHashCode() => throw ExceptionUtilities.Unreachable();

    /// <summary>
    /// Checks if <see cref="AdaptedSymbol"/> is a definition and its containing .NET module is a <see cref="SourceModuleSymbol"/>.
    /// </summary>
    [Conditional("DEBUG")]
    protected internal void CheckDefinitionInvariant() => AdaptedSymbol.CheckDefinitionInvariant();

    /// <summary>
    /// Return whether <see cref="AdaptedSymbol"/> is either the original definition
    /// or distinct from the original.
    /// </summary>
    internal bool IsDefinitionOrDistinct() => AdaptedSymbol.IsDefinitionOrDistinct();
}
#endif

partial class Symbol
{
#if DEBUG
    internal SymbolAdapter GetCciAdapter() => GetCciAdapterImpl();
    protected virtual SymbolAdapter GetCciAdapterImpl() => throw ExceptionUtilities.Unreachable();
#else
    internal Symbol AdaptedSymbol => this;
    internal Symbol GetCciAdapter() => this;
#endif

    /// <summary>
    /// Checks if this symbol is a definition and its containing .NET module is a <see cref="SourceModuleSymbol"/>.
    /// </summary>
    [Conditional("DEBUG")]
    protected internal void CheckDefinitionInvariant()
    {
        // can't be generic instantiation
        Debug.Assert(IsDefinition);

        // must be declared in the .NET module we are building
        Debug.Assert(ContainingModule is SourceModuleSymbol ||
                     (Kind == SymbolKind.Assembly && this is SourceAssemblySymbol) ||
                     (Kind == SymbolKind.NetModule && this is SourceModuleSymbol));
    }

    IReference Microsoft.CodeAnalysis.Symbols.ISymbolInternal.GetCciAdapter() => GetCciAdapter();

    /// <summary>
    /// Return whether the symbol is either the original definition
    /// or distinct from the original. Intended for use in Debug.Assert
    /// only since it may include a deep comparison.
    /// </summary>
    internal bool IsDefinitionOrDistinct()
    {
        return IsDefinition || !Equals(OriginalDefinition, SymbolEqualityComparer.ConsiderEverything.CompareKind);
    }
}
