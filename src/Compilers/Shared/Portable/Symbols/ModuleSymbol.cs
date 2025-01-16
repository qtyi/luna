// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Reflection.PortableExecutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

/// <summary>
/// Represents a .NET module within an assembly. Every assembly contains one or more modules.
/// </summary>
abstract partial class ModuleSymbol : Symbol, IModuleSymbolInternal
{
    /// <inheritdoc/>
    internal ModuleSymbol() { }

    /// <summary>
    /// Module's ordinal within containing assembly's Modules array.
    /// 0 - for a source module, etc.
    /// -1 - for a module that doesn't have containing assembly, or has it, but is not part of Modules array. 
    /// </summary>
    internal abstract int Ordinal { get; }

    /// <summary>
    /// Target architecture of the machine.
    /// </summary>
    internal abstract Machine Machine { get; }

    /// <summary>
    /// Indicates that this PE file makes Win32 calls. See CorPEKind.pe32BitRequired for more information (http://msdn.microsoft.com/en-us/library/ms230275.aspx).
    /// </summary>
    internal abstract bool Bit32Required { get; }

    /// <summary>
    /// Returns an array of assembly identities for assemblies referenced by this module.
    /// Items at the same position from ReferencedAssemblies and from ReferencedAssemblySymbols 
    /// correspond to each other.
    /// </summary>
    public ImmutableArray<AssemblyIdentity> ReferencedAssemblies
    {
        get
        {
#warning Not implemented.
            throw new NotImplementedException();
        }
    }
}
