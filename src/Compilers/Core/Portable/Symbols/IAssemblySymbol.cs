// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents a .NET assembly, consisting of one or more modules.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
public interface IAssemblySymbol : ISymbol,
    Microsoft.CodeAnalysis.IAssemblySymbol
{
    /// <summary>
    /// Gets the merged root module that contains all module and types defined in the .NET
    /// modules of this assembly. If there is just one .NET module in this assembly, this
    /// property just returns the GlobalModule of that .NET module.
    /// </summary>
    IModuleSymbol GlobalModule { get; }

    /// <summary>
    /// Gets the .NET modules in this assembly. (There must be at least one.) The first one is the main .NET module
    /// that holds the assembly manifest.
    /// </summary>
    IEnumerable<INetmoduleSymbol> Netmodules { get; }

    /// <summary>
    /// Gets the set of module names from this assembly.
    /// </summary>
    ICollection<string> ModuleNames { get; }

    /// <summary>
    /// Gets a value indicating whether this assembly gives 
    /// <paramref name="toAssembly"/> access to internal symbols</summary>
    bool GivesAccessTo(IAssemblySymbol toAssembly);

    /// <summary>
    /// Lookup a type within the assembly using the canonical CLR metadata name of the type.
    /// </summary>
    /// <param name="fullyQualifiedMetadataName">Type name.</param>
    /// <returns>Symbol for the type or null if type cannot be found or is ambiguous. </returns>
    new INamedTypeSymbol? GetTypeByMetadataName(string fullyQualifiedMetadataName);

    /// <summary>
    /// Returns the type symbol for a forwarded type based its canonical CLR metadata name.
    /// The name should refer to a non-nested type. If type with this name is not forwarded,
    /// null is returned.
    /// </summary>
    new INamedTypeSymbol? ResolveForwardedType(string fullyQualifiedMetadataName);

    /// <summary>
    /// Returns type symbols for top-level (non-nested) types forwarded by this assembly.
    /// </summary>
    new ImmutableArray<INamedTypeSymbol> GetForwardedTypes();
}
