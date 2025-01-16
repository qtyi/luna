// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

/// <summary>
/// Represents a module (.NET namespace, type, field, etc.).
/// </summary>
partial class ModuleSymbol : Symbol, IModuleSymbolInternal
{
    /// <inheritdoc/>
    public ModuleKind ModuleKind => Kind switch
    {
        SymbolKind.Namespace => ModuleKind.Namespace,

        SymbolKind.ArrayType or
        SymbolKind.DynamicType or
        SymbolKind.ErrorType or
        SymbolKind.NamedType => ModuleKind.Type,

        SymbolKind.Event or
        SymbolKind.Field or
        SymbolKind.Method or
        SymbolKind.Property => ModuleKind.Field,

        _ => ModuleKind.Unknown
    };

    /// <summary>
    /// Returns whether this module is the unnamed, global .NET namespace that is 
    /// at the root of all .NET namespaces.
    /// </summary>
    internal bool IsGlobalNamespace => IsGlobalModule && IsNamespace;

    /// <inheritdoc/>
    public abstract bool IsGlobalModule { get; }

    /// <summary>
    /// Gets a value indicate if this module is a .NET namespace.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this module is a .NET namespace; otherwise, <see langword="false"/>.
    /// </value>
    internal bool IsNamespace => ModuleKind == ModuleKind.Namespace;

    /// <summary>
    /// Gets a value indicate if this module is a type.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this module is a type; otherwise, <see langword="false"/>.
    /// </value>
    public bool IsType => ModuleKind == ModuleKind.Type;

    /// <summary>
    /// Gets a value indicate if this module is a field.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this module is a field; otherwise, <see langword="false"/>.
    /// </value>
    public bool IsField => ModuleKind == ModuleKind.Field;

    #region GetMembers
    /// <summary>
    /// Gets all the members of this symbol.
    /// </summary>
    /// <returns>
    /// An ImmutableArray containing all the modules that are members of this symbol.
    /// If this symbol has no members, returns an empty ImmutableArray. Never returns
    /// null.
    /// </returns>
    public abstract ImmutableArray<ModuleSymbol> GetMembers();

    /// <summary>
    /// Gets all the members of this symbol that have a particular name.
    /// </summary>
    /// <param name="name">Name of member to match.</param>
    /// <returns>
    /// An ImmutableArray containing all the modules that are members of this symbol with
    /// the given name.
    /// If this symbol has no members with this name, returns an empty ImmutableArray.
    /// Never returnsnull.
    /// </returns>
    public abstract ImmutableArray<ModuleSymbol> GetMembers(string name);

    /// <summary>
    /// Gets all the members of this symbol that are .NET namespaces.
    /// </summary>
    /// <returns>
    /// An ImmutableArray containing all the .NET namespaces that are members of this
    /// symbol.
    /// If this symbol has no .NET namespace members, returns an empty ImmutableArray.
    /// Never returns null.
    /// </returns>
    internal abstract ImmutableArray<ModuleSymbol> GetNamespaceMembers();

    /// <summary>
    /// Gets all the members of this symbol that are .NET namespaces that have a particular
    /// name.
    /// </summary>
    /// <param name="name">Name of .NET namespace to match.</param>
    /// <returns>
    /// An ImmutableArray containing all the .NET namespaces that are members of this symbol with
    /// the given name.
    /// If this symbol has no .NET namespace members with this name, returns an empty
    /// ImmutableArray. Never returns null.
    /// </returns>
    internal abstract ImmutableArray<ModuleSymbol> GetNamespaceMembers(string name);

    /// <summary>
    /// Gets all the members of this symbol that are types.
    /// </summary>
    /// <returns>
    /// An ImmutableArray containing all the types that are members of this symbol.
    /// If this symbol has no type members, returns an empty ImmutableArray. Never returns
    /// null.
    /// </returns>
    public abstract ImmutableArray<NamedTypeSymbol> GetTypeMembers();

    /// <summary>
    /// Gets all the members of this symbol that are types that have a particular name.
    /// </summary>
    /// <param name="name">Name of type to match.</param>
    /// <returns>
    /// An ImmutableArray containing all the types that are members of this symbol with
    /// the given name.
    /// If this symbol has no type members with this name, returns an empty ImmutableArray.
    /// Never returns null.
    /// </returns>
    public abstract ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name);

    /// <summary>
    /// Gets all the members of this symbol that are types that have a particular name and
    /// arity.
    /// </summary>
    /// <param name="name">Name of type to match.</param>
    /// <param name="arity">Arity of type to match.</param>
    /// <returns>
    /// An ImmutableArray containing all the types that are members of this symbol with
    /// the given name and arity.
    /// If this symbol has no type members with this name and arity, returns an empty
    /// ImmutableArray. Never returns null.
    /// </returns>
    public virtual ImmutableArray<NamedTypeSymbol> GetTypeMembers(string name, int arity)
    {
        // Default implementation does a post-filter. We can override this if its a performance burden, but experience is that it won't be.
        return GetTypeMembers(name).WhereAsArray(static (t, a) => t.Arity == a, arity);
    }

    /// <summary>
    /// Gets all the members of this symbol that are fields.
    /// </summary>
    /// <returns>
    /// An ImmutableArray containing all the fields that are members of this symbol.
    /// If this symbol has no field members, returns an empty ImmutableArray. Never returns
    /// null.
    /// </returns>
    public abstract ImmutableArray<FieldSymbol> GetFieldMembers();

    /// <summary>
    /// Gets all the members of this symbol that are fields that have a particular name.
    /// </summary>
    /// <param name="name">Name of field to match.</param>
    /// <returns>
    /// An ImmutableArray containing all the fields that are members of this symbol with
    /// the given name.
    /// If this symbol has no field members with this name, returns an empty ImmutableArray.
    /// Never returns null.
    /// </returns>
    public abstract ImmutableArray<FieldSymbol> GetFieldMembers(string name);

    /// <summary>
    /// Gets all the members of this symbol that are fields that have a particular name and
    /// arity.
    /// </summary>
    /// <param name="name">Name of field to match.</param>
    /// <param name="arity">Arity of field to match.</param>
    /// <returns>
    /// An ImmutableArray containing all the fields that are members of this symbol with
    /// the given name and arity.
    /// If this symbol has no field members with this name and arity, returns an empty
    /// ImmutableArray. Never returns null.
    /// </returns>
    public abstract ImmutableArray<FieldSymbol> GetFieldMembers(string name, int arity);
    #endregion

    /// <inheritdoc cref="Symbol()"/>
    internal ModuleSymbol() { }
}
