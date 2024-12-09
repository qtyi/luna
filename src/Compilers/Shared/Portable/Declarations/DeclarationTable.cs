// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

/// <summary>
/// A declaration table is a device which keeps track of declarations from
/// parse trees. It is optimized for the case where there is one set of declarations that stays
/// constant, and a specific declaration corresponding to the currently edited
/// file which is being added and removed repeatedly.
/// </summary>
internal sealed partial class DeclarationTable
{
    public static readonly DeclarationTable Empty = new(
        allOlderRootDeclarations: ImmutableSetWithInsertionOrder<Lazy<ModuleDeclaration>>.Empty,
        latestLazyRootDeclaration: null);

    // All our root declarations.  We split these so we can separate out the unchanging 'older'
    // declarations from the constantly changing 'latest' declaration.
    private readonly ImmutableSetWithInsertionOrder<Lazy<ModuleDeclaration>> _allOlderRootDeclarations;
    private readonly Lazy<ModuleDeclaration>? _latestLazyRootDeclaration;

    private ICollection<string>? _typeNames;
    private ICollection<string>? _namespaceNames;

    private DeclarationTable(
        ImmutableSetWithInsertionOrder<Lazy<ModuleDeclaration>> allOlderRootDeclarations,
        Lazy<ModuleDeclaration>? latestLazyRootDeclaration)
    {
        _allOlderRootDeclarations = allOlderRootDeclarations;
        _latestLazyRootDeclaration = latestLazyRootDeclaration;
    }

    /// <summary>
    /// Add a root declaration to the decl table.
    /// </summary>
    /// <param name="lazyRootDeclaration">A declaration which is added as a root declaration.</param>
    /// <returns>A new instance of decl table with a root declaration added.</returns>
    public DeclarationTable AddRootDeclaration(Lazy<ModuleDeclaration> lazyRootDeclaration)
    {
        // We can only re-use the cache if we don't already have a 'latest' item for the decl
        // table.
        if (_latestLazyRootDeclaration == null)
        {
            return new(_allOlderRootDeclarations, lazyRootDeclaration);
        }
        else
        {
            // we already had a 'latest' item.  This means we're hearing about a change to a
            // different tree.  Add old latest item to the 'oldest' collection.
            return new(_allOlderRootDeclarations.Add(_latestLazyRootDeclaration), lazyRootDeclaration);
        }
    }

    /// <summary>
    /// Remove a root declaration from the decl table.
    /// </summary>
    /// <param name="lazyRootDeclaration">A declaration which is removed as a root declaration.</param>
    /// <returns>A new instance of decl table with a root declaration removed.</returns>
    public DeclarationTable RemoveRootDeclaration(Lazy<ModuleDeclaration> lazyRootDeclaration)
    {
        // We can only reuse the cache if we're removing the decl that was just added.
        if (_latestLazyRootDeclaration == lazyRootDeclaration)
        {
            return new(_allOlderRootDeclarations, latestLazyRootDeclaration: null);
        }
        else
        {
            // We're removing a different tree than the latest one added.  We need
            // to remove the passed in root from our 'older' list.
            //
            // Note: we can keep around the 'latestLazyRootDeclaration'.
            return new(_allOlderRootDeclarations.Remove(lazyRootDeclaration), _latestLazyRootDeclaration);
        }
    }

#warning 未完成。
}
