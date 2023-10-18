// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis;

public interface IModuleSymbol : ISymbol,
    Microsoft.CodeAnalysis.INamespaceSymbol,
    Microsoft.CodeAnalysis.ITypeSymbol,
    Microsoft.CodeAnalysis.IEventSymbol,
    Microsoft.CodeAnalysis.IFieldSymbol,
    Microsoft.CodeAnalysis.IMethodSymbol,
    Microsoft.CodeAnalysis.IPropertySymbol
{
    ModuleKind ModuleKind { get; }

    bool IsField { get; }

    /// <summary>
    /// Get all the members of this symbol.
    /// </summary>
    /// <returns>An ImmutableArray containing all the members of this symbol. If this symbol has no members,
    /// returns an empty ImmutableArray. Never returns Null.</returns>
    new ImmutableArray<IModuleSymbol> GetMembers();

    /// <summary>
    /// Get all the members of this symbol that have a particular name.
    /// </summary>
    /// <returns>An ImmutableArray containing all the members of this symbol with the given name. If there are
    /// no members with this name, returns an empty ImmutableArray. Never returns Null.</returns>
    new ImmutableArray<IModuleSymbol> GetMembers(string name);

    /// <summary>
    /// Get all the members of this symbol that are .NET namespaces.
    /// </summary>
    new IEnumerable<IModuleSymbol> GetNamespaceMembers();

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamespaceOrTypeSymbol.GetTypeMembers()"/>
    new IEnumerable<INamedTypeSymbol> GetTypeMembers();

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamespaceOrTypeSymbol.GetTypeMembers(string)"/>
    new ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name);

    /// <inheritdoc cref="Microsoft.CodeAnalysis.INamespaceOrTypeSymbol.GetTypeMembers(string, int)"/>
    new ImmutableArray<INamedTypeSymbol> GetTypeMembers(string name, int arity);

    /// <summary>
    /// Get all the members of this symbol that are fields.
    /// </summary>
    IEnumerable<IFieldSymbol> GetFieldSymbols();

    /// <summary>
    /// Get all the members of this symbol that are fields that have a particular name, of any arity.
    /// </summary>
    /// <returns>An ImmutableArray containing all the fields that are members of this symbol with the given name.
    /// If this symbol has no type members with this name,
    /// returns an empty ImmutableArray. Never returns null.</returns>
    IEnumerable<IFieldSymbol> GetFieldSymbols(string name);

    /// <summary>
    /// Returns whether this module is the unnamed, global module that is 
    /// at the root of all modules.
    /// </summary>
    bool IsGlobalModule { get; }

    /// <summary>
    /// If a module is an assembly or compilation module, it may be composed of multiple
    /// modules that are merged together. If so, ConstituentModules returns
    /// all the modules that were merged. If this module was not merged, returns
    /// an array containing only this module.
    /// </summary>
    ImmutableArray<IModuleSymbol> ConstituentModules { get; }
}
