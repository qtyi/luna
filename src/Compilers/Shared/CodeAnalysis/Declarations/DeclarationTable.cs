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

}
