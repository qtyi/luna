// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using System.Collections.Concurrent;
using System.Diagnostics;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

/// <summary>
/// A <see cref="NonMissingAssemblySymbol"/> is a special kind of <see cref="AssemblySymbol"/> that represents
/// an assembly that is not missing, i.e. the "real" thing.
/// </summary>
internal abstract class NonMissingAssemblySymbol : AssemblySymbol
{
    /// <summary>
    /// This is a cache for searching NamedTypeSymbol by MetadataTypeName.
    /// </summary>
    /// <remarks></remarks>
    private readonly ConcurrentDictionary<MetadataTypeName.Key, NamedTypeSymbol> _emittedNameToTypeMap = new();

    private ModuleSymbol? _lazyGlobalModule;

    /// <summary>
    /// Gets a value that indicate if this symbol represent a missing assembly.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol represent a missing assembly; otherwise <see langword="false"/>.
    /// </value>
    internal sealed override bool IsMissing => false;

    /// <summary>
    /// Gets the merged root module that contains all modules defined in the .NET modules
    /// of this assembly. If there is just one .NET module in this assembly, this property
    /// just returns the <see cref="GlobalModule"/> of that .NET module.
    /// </summary>
    public sealed override ModuleSymbol GlobalModule
    {
        get
        {
            var result = this._lazyGlobalModule;
            if (result is null)
            {
                // Get the root module from each .NET module, and merge them all together.
                // If there is only one, then MergedModuleSymbol.Create will just return
                // that one.

                var allGlobalModules = from m in this.Netmodules select m.GlobalModule;
#warning 未完成
                throw new NotImplementedException();

                result = Interlocked.CompareExchange(ref this._lazyGlobalModule, result, null);
            }

            return result;
        }
    }

    /// <summary>
    /// Lookup a top level type referenced from metadata, names should be
    /// compared case-sensitively.  Detect cycles during lookup.
    /// </summary>
    /// <param name="emittedName">
    /// Full type name, possibly with generic name mangling.
    /// </param>
    internal sealed override NamedTypeSymbol? LookupDeclaredTopLevelMetadataType(ref MetadataTypeName emittedName)
    {
        NamedTypeSymbol? result = null;

        result = LookupTopLevelMetadataTypeInCache(ref emittedName);

        if (result is not null)
        {
            // We cache result equivalent to digging through type forwarders, which
            // might produce a forwarder specific ErrorTypeSymbol. We don't want to 
            // return that error symbol.
            if (!result.IsErrorType() && (object)result.ContainingAssembly == (object)this)
            {
                return result;
            }

            // According to the cache, the type wasn't found, or isn't declared in this assembly (forwarded).
            return null;
        }
        else
        {
            result = LookupDeclaredTopLevelMetadataTypeInModules(ref emittedName);

            Debug.Assert(result is null || ((object)result.ContainingAssembly == (object)this && !result.IsErrorType()));

            if (result is null)
            {
                return null;
            }

            // Add result of the lookup into the cache
            return CacheTopLevelMetadataType(ref emittedName, result);
        }
    }

    private NamedTypeSymbol? LookupDeclaredTopLevelMetadataTypeInModules(ref MetadataTypeName emittedName)
    {
        // Now we will look for the type in each .NET module of the assembly and pick the
        // first type we find.

        foreach (var module in this.Netmodules)
        {
            NamedTypeSymbol? result = module.LookupTopLevelMetadataType(ref emittedName);

            if (result is not null)
            {
                return result;
            }
        }

        return null;
    }

    /// <summary>
    /// Lookup a top level type referenced from metadata, names should be
    /// compared case-sensitively.  Detect cycles during lookup.
    /// </summary>
    /// <param name="emittedName">
    /// Full type name, possibly with generic name mangling.
    /// </param>
    /// <param name="visitedAssemblies">
    /// List of assemblies lookup has already visited (since type forwarding can introduce cycles).
    /// </param>
    internal sealed override NamedTypeSymbol LookupDeclaredOrForwardedTopLevelMetadataType(ref MetadataTypeName emittedName, ConsList<AssemblySymbol>? visitedAssemblies)
    {
        NamedTypeSymbol? result = LookupTopLevelMetadataTypeInCache(ref emittedName);

        if ((object?)result != null)
        {
            return result;
        }
        else
        {
            result = LookupDeclaredTopLevelMetadataTypeInModules(ref emittedName);

            Debug.Assert(result is null || ((object)result.ContainingAssembly == (object)this && !result.IsErrorType()));

            if (result is null)
            {
                // We didn't find the type
                result = TryLookupForwardedMetadataTypeWithCycleDetection(ref emittedName, visitedAssemblies);
            }

            // Add result of the lookup into the cache
            return CacheTopLevelMetadataType(ref emittedName, result ?? new MissingMetadataTypeSymbol.TopLevel(this.Netmodules[0], ref emittedName));
        }
    }

    internal abstract override NamedTypeSymbol? TryLookupForwardedMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol>? visitedAssemblies);

    private NamedTypeSymbol? LookupTopLevelMetadataTypeInCache(ref MetadataTypeName emittedName)
    {
        NamedTypeSymbol? result;
        if (_emittedNameToTypeMap.TryGetValue(emittedName.ToKey(), out result))
        {
            return result;
        }

        return null;
    }

    /// <summary>
    /// For test purposes only.
    /// </summary>
    internal NamedTypeSymbol CachedTypeByEmittedName(string emittedname)
    {
        MetadataTypeName mdName = MetadataTypeName.FromFullName(emittedname);
        return _emittedNameToTypeMap[mdName.ToKey()];
    }

    /// <summary>
    /// For test purposes only.
    /// </summary>
    internal int EmittedNameToTypeMapCount
    {
        get
        {
            return _emittedNameToTypeMap.Count;
        }
    }

    private NamedTypeSymbol CacheTopLevelMetadataType(
        ref MetadataTypeName emittedName,
        NamedTypeSymbol result)
    {
        NamedTypeSymbol result1;
        result1 = _emittedNameToTypeMap.GetOrAdd(emittedName.ToKey(), result);
        System.Diagnostics.Debug.Assert(TypeSymbol.Equals(result1, result, TypeCompareKind.ConsiderEverything2)); // object identity may differ in error cases
        return result1;
    }
}
