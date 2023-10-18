// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;

namespace Qtyi.CodeAnalysis;

/// <summary>
/// Represents a .NET module within an assembly. Every assembly contains one or more .NET modules.
/// </summary>
/// <remarks>
/// This interface is reserved for implementation by its associated APIs. We reserve the right to
/// change it in the future.
/// </remarks>
public interface INetmoduleSymbol : ISymbol,
    Microsoft.CodeAnalysis.IModuleSymbol
{
    /// <summary>
    /// Returns a ModuleSymbol representing the global (root) module, with .NET module extent,
    /// that can be used to browse all of the symbols defined in this .NET module.
    /// </summary>
    IModuleSymbol GlobalModule { get; }

    /// <summary>
    /// Given a module symbol, returns the corresponding .NET module specific module symbol
    /// </summary>
    IModuleSymbol? GetNetmoduleModule(IModuleSymbol moduleSymbol);

    /// <summary>
    /// Returns an array of AssemblySymbol objects corresponding to assemblies referenced 
    /// by this .NET module. Items at the same position from ReferencedAssemblies and 
    /// from ReferencedAssemblySymbols correspond to each other.
    /// </summary>
    new ImmutableArray<IAssemblySymbol> ReferencedAssemblySymbols { get; }
}
