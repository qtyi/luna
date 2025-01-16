// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

using Qtyi.CodeAnalysis;
using Qtyi.CodeAnalysis.Symbols;

internal abstract partial class MetadataOrSourceAssemblySymbol : AssemblySymbol
{
    /// <summary>
    /// An array of cached Cor types defined in this assembly.
    /// Lazily filled by GetDeclaredSpecialType method.
    /// </summary>
    private NamedTypeSymbol[] _lazySpecialTypes;

    /// <summary>
    /// How many Cor types have we cached so far.
    /// </summary>
    private int _cachedSpecialTypes;

    /// <summary>
    /// Lookup declaration for predefined CorLib type in this Assembly.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    internal sealed override NamedTypeSymbol GetDeclaredSpecialType(SpecialType type)
    {
#if DEBUG
        foreach (var module in Netmodules)
            Debug.Assert(module.ReferencedAssemblies.Length == 0);
#endif

        if (_lazySpecialTypes == null || _lazySpecialTypes[(int)type] is null)
        {
            var emittedName = MetadataTypeName.FromFullName(type.GetMetadataName(), useCLSCompliantNameArityEncoding: true);
            var module = Netmodules[0];
            var result = module.LookupTopLevelMetadataType(ref emittedName);

            Debug.Assert(result?.IsErrorType() != true);

            if (result is null || result.DeclaredAccessibility != Accessibility.Public)
                result = new MissingMetadataTypeSymbol.TopLevel(module, ref emittedName, type);

            RegisterDeclaredSpecialType(result);
        }

        Debug.Assert(_lazySpecialTypes is not null);
        return _lazySpecialTypes[(int)type];
    }

    /// <summary>
    /// Register declaration of predefined CorLib type in this Assembly.
    /// </summary>
    /// <param name="corType"></param>
    internal sealed override void RegisterDeclaredSpecialType(NamedTypeSymbol corType)
    {
        var typeId = corType.SpecialType;
        Debug.Assert(typeId != SpecialType.None);
        Debug.Assert(ReferenceEquals(corType.ContainingAssembly, this));
        Debug.Assert(corType.ContainingNetmodule.Ordinal == 0);
        Debug.Assert(ReferenceEquals(CorLibrary, this));

        if (_lazySpecialTypes == null)
            Interlocked.CompareExchange(ref _lazySpecialTypes, new NamedTypeSymbol[(int)SpecialType.Count + 1], null);

        if (Interlocked.CompareExchange(ref _lazySpecialTypes[(int)typeId], corType, null) is not null)
            Debug.Assert(
                ReferenceEquals(corType, _lazySpecialTypes[(int)typeId]) ||
                (corType.Kind == SymbolKind.ErrorType &&
                _lazySpecialTypes[(int)typeId].Kind == SymbolKind.ErrorType));
        else
        {
            Interlocked.Increment(ref _cachedSpecialTypes);
            Debug.Assert(_cachedSpecialTypes > 0 && _cachedSpecialTypes <= (int)SpecialType.Count);
        }
    }

    /// <summary>
    /// Continue looking for declaration of predefined CorLib type in this Assembly
    /// while symbols for new type declarations are constructed.
    /// </summary>
    internal override bool KeepLookingForDeclaredSpecialTypes =>
        ReferenceEquals(CorLibrary, this) && _cachedSpecialTypes < (int)SpecialType.Count;

}
