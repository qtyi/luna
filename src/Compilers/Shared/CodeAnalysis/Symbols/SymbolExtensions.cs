// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

internal static partial class SymbolExtensions
{
    #region GetPublicSymbol
    /// <summary>
    /// Helper for GetPublicSymbol methods.
    /// </summary>
    /// <typeparam name="TSymbol">Type of internal symbol.</typeparam>
    /// <typeparam name="TISymbol">Type of corresponding public symbol of <typeparamref name="TSymbol"/>.</typeparam>
    /// <param name="symbol">The internal symbol to get public symbol of.</param>
    /// <returns>Public symbol of <paramref name="symbol"/>.</returns>
    [return: NotNullIfNotNull(nameof(symbol))]
    private static TISymbol? GetPublicSymbol<TSymbol, TISymbol>(this TSymbol? symbol)
        where TSymbol : Symbol
        where TISymbol : class, ISymbol
        => (TISymbol?)symbol?.ISymbol;

    /// <summary>
    /// Get internal symbol of a particular <see cref="ISymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static ISymbol? GetPublicSymbol(this Symbol? symbol) => symbol.GetPublicSymbol<Symbol, ISymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IAssemblySymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static IAssemblySymbol? GetPublicSymbol(this AssemblySymbol? symbol) => symbol.GetPublicSymbol<AssemblySymbol, IAssemblySymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="INetmoduleSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static INetmoduleSymbol? GetPublicSymbol(this NetmoduleSymbol? symbol) => symbol.GetPublicSymbol<NetmoduleSymbol, INetmoduleSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IModuleSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static IModuleSymbol? GetPublicSymbol(this ModuleSymbol? symbol) => symbol.GetPublicSymbol<ModuleSymbol, IModuleSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="ITypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static ITypeSymbol? GetPublicSymbol(this TypeSymbol? symbol) => symbol.GetPublicSymbol<TypeSymbol, ITypeSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IFieldSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static IFieldSymbol? GetPublicSymbol(this FieldSymbol? symbol) => symbol.GetPublicSymbol<FieldSymbol, IFieldSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IDynamicTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static IDynamicTypeSymbol? GetPublicSymbol(this DynamicTypeSymbol? symbol) => symbol.GetPublicSymbol<DynamicTypeSymbol, IDynamicTypeSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="INamedTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static INamedTypeSymbol? GetPublicSymbol(this NamedTypeSymbol? symbol) => symbol.GetPublicSymbol<NamedTypeSymbol, INamedTypeSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IErrorTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static IErrorTypeSymbol? GetPublicSymbol(this ErrorTypeSymbol? symbol) => symbol.GetPublicSymbol<ErrorTypeSymbol, IErrorTypeSymbol>();

#if LANG_MOONSCRIPT
    /// <summary>
    /// Get internal symbol of a particular <see cref="ILabelSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static ILabelSymbol? GetPublicSymbol(this LabelSymbol? symbol) => symbol.GetPublicSymbol<LabelSymbol, ILabelSymbol>();
#endif

    /// <summary>
    /// Get internal symbol of a particular <see cref="ILocalSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static ILocalSymbol? GetPublicSymbol(this LocalSymbol? symbol) => symbol.GetPublicSymbol<LocalSymbol, ILocalSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IParameterSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static IParameterSymbol? GetPublicSymbol(this ParameterSymbol? symbol) => symbol.GetPublicSymbol<ParameterSymbol, IParameterSymbol>();

    /// <summary>
    /// Get corresponding public symbols of a enumerable of particular <see cref="Symbol"/>s.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbol{TSymbol, TISymbol}(TSymbol)"/>
    internal static IEnumerable<ISymbol?> GetPublicSymbols(this IEnumerable<Symbol?> symbols)
        => symbols.Select(GetPublicSymbol);

    /// <summary>
    /// Helper for GetPublicSymbols methods.
    /// </summary>
    /// <typeparam name="TSymbol">Type of internal symbol.</typeparam>
    /// <typeparam name="TISymbol">Type of corresponding public symbol of <typeparamref name="TSymbol"/>.</typeparam>
    /// <param name="symbols">The internal symbols to get corresponding public symbols of.</param>
    /// <returns>Corresponding public symbols of of <paramref name="symbols"/>.</returns>
    private static ImmutableArray<TISymbol> GetPublicSymbols<TSymbol, TISymbol>(this ImmutableArray<TSymbol> symbols)
        where TSymbol : Symbol
        where TISymbol : class, ISymbol
        => symbols.SelectAsArray(GetPublicSymbol<TSymbol, TISymbol>)!;

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="Symbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<ISymbol> GetPublicSymbols(this ImmutableArray<Symbol> symbols) => symbols.GetPublicSymbols<Symbol, ISymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="AssemblySymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<IAssemblySymbol> GetPublicSymbols(this ImmutableArray<AssemblySymbol> symbols) => symbols.GetPublicSymbols<AssemblySymbol, IAssemblySymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="NetmoduleSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<INetmoduleSymbol> GetPublicSymbols(this ImmutableArray<NetmoduleSymbol> symbols) => symbols.GetPublicSymbols<NetmoduleSymbol, INetmoduleSymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="ModuleSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<IModuleSymbol> GetPublicSymbols(this ImmutableArray<ModuleSymbol> symbols) => symbols.GetPublicSymbols<ModuleSymbol, IModuleSymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="TypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<ITypeSymbol> GetPublicSymbols(this ImmutableArray<TypeSymbol> symbols) => symbols.GetPublicSymbols<TypeSymbol, ITypeSymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="FieldSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<IFieldSymbol> GetPublicSymbols(this ImmutableArray<FieldSymbol> symbols) => symbols.GetPublicSymbols<FieldSymbol, IFieldSymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="DynamicTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<IDynamicTypeSymbol> GetPublicSymbols(this ImmutableArray<DynamicTypeSymbol> symbols) => symbols.GetPublicSymbols<DynamicTypeSymbol, IDynamicTypeSymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="NamedTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<INamedTypeSymbol> GetPublicSymbols(this ImmutableArray<NamedTypeSymbol> symbols) => symbols.GetPublicSymbols<NamedTypeSymbol, INamedTypeSymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="ErrorTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<IErrorTypeSymbol> GetPublicSymbols(this ImmutableArray<ErrorTypeSymbol> symbols) => symbols.GetPublicSymbols<ErrorTypeSymbol, IErrorTypeSymbol>();

#if LANG_MOONSCRIPT
    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="LabelSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<ILabelSymbol> GetPublicSymbols(this ImmutableArray<LabelSymbol> symbols) => symbols.GetPublicSymbols<LabelSymbol, ILabelSymbol>();
#endif

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="LocalSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<ILocalSymbol> GetPublicSymbols(this ImmutableArray<LocalSymbol> symbols) => symbols.GetPublicSymbols<LocalSymbol, ILocalSymbol>();

    /// <summary>
    /// Get corresponding public symbols of an array of particular <see cref="ParameterSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetPublicSymbols{TSymbol, TISymbol}(ImmutableArray{TSymbol})"/>
    internal static ImmutableArray<IParameterSymbol> GetPublicSymbols(this ImmutableArray<ParameterSymbol> symbols) => symbols.GetPublicSymbols<ParameterSymbol, IParameterSymbol>();
    #endregion

    #region GetSymbol
    /// <summary>
    /// Helper for GetSymbol methods.
    /// </summary>
    /// <typeparam name="TISymbol">Type of public symbol.</typeparam>
    /// <typeparam name="TSymbol">Type of corresponding internal symbol of <typeparamref name="TISymbol"/>.</typeparam>
    /// <param name="symbol">The public symbol to get internal symbol of.</param>
    /// <returns>Internal symbol of <paramref name="symbol"/>.</returns>
    [return: NotNullIfNotNull(nameof(symbol))]
    private static TSymbol? GetSymbol<TISymbol, TSymbol>(this TISymbol? symbol)
        where TISymbol : class, ISymbol
        where TSymbol : Symbol
        => (TSymbol?)((PublicModel.Symbol?)(ISymbol?)symbol)?.UnderlyingSymbol;

    /// <summary>
    /// Get internal symbol of a particular <see cref="ISymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static Symbol? GetSymbol(this ISymbol? symbol) => symbol.GetSymbol<ISymbol, Symbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IAssemblySymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static AssemblySymbol? GetSymbol(this IAssemblySymbol? symbol) => symbol.GetSymbol<IAssemblySymbol, AssemblySymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="INetmoduleSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static NetmoduleSymbol? GetSymbol(this INetmoduleSymbol? symbol) => symbol.GetSymbol<INetmoduleSymbol, NetmoduleSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IModuleSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static ModuleSymbol? GetSymbol(this IModuleSymbol? symbol) => symbol.GetSymbol<IModuleSymbol, ModuleSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="ITypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static TypeSymbol? GetSymbol(this ITypeSymbol? symbol) => symbol.GetSymbol<ITypeSymbol, TypeSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IFieldSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static FieldSymbol? GetSymbol(this IFieldSymbol? symbol) => symbol.GetSymbol<IFieldSymbol, FieldSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IDynamicTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static DynamicTypeSymbol? GetSymbol(this IDynamicTypeSymbol? symbol) => symbol.GetSymbol<IDynamicTypeSymbol, DynamicTypeSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="INamedTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static NamedTypeSymbol? GetSymbol(this INamedTypeSymbol? symbol) => symbol.GetSymbol<INamedTypeSymbol, NamedTypeSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IErrorTypeSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static ErrorTypeSymbol? GetSymbol(this IErrorTypeSymbol? symbol) => symbol.GetSymbol<IErrorTypeSymbol, ErrorTypeSymbol>();

#if LANG_MOONSCRIPT
    /// <summary>
    /// Get internal symbol of a particular <see cref="ILabelSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static LabelSymbol? GetSymbol(this ILabelSymbol? symbol) => symbol.GetSymbol<ILabelSymbol, LabelSymbol>();
#endif

    /// <summary>
    /// Get internal symbol of a particular <see cref="ILocalSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static LocalSymbol? GetSymbol(this ILocalSymbol? symbol) => symbol.GetSymbol<ILocalSymbol, LocalSymbol>();

    /// <summary>
    /// Get internal symbol of a particular <see cref="IParameterSymbol"/>.
    /// </summary>
    /// <inheritdoc cref="GetSymbol{TISymbol, TSymbol}(TISymbol)"/>
    [return: NotNullIfNotNull(nameof(symbol))]
    internal static ParameterSymbol? GetSymbol(this IParameterSymbol? symbol) => symbol.GetSymbol<IParameterSymbol, ParameterSymbol>();
    #endregion
}
