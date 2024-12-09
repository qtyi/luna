// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Reflection.PortableExecutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;

using ThisReferenceManager = LuaCompilation.ReferenceManager;
#elif LANG_MOONSCRIPT
using Qtyi.CodeAnalysis.MoonScript;
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;

using ThisReferenceManager = MoonScriptCompilation.ReferenceManager;
#endif

/// <summary>
/// Represents a .NET module within an assembly. Every assembly contains one or more modules.
/// </summary>
partial class NetmoduleSymbol : Symbol, INetmoduleSymbolInternal
{
    /// <value>Returns <see cref="SymbolKind.Netmodule"/>.</value>
    /// <inheritdoc/>
    public sealed override SymbolKind Kind => SymbolKind.Netmodule;

    #region Containing
    /// <remarks>.NET modules are always not contained by a named type.</remarks>
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    public sealed override NamedTypeSymbol? ContainingType => null;

    /// <remarks>.NET modules are always not contained by a module.</remarks>
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    public sealed override ModuleSymbol? ContainingModule => null;

    /// <remarks>Modules are always directly contained by an assembly.</remarks>
    /// <value>Returns the same as <see cref="Symbol.ContainingSymbol"/>.</value>
    /// <inheritdoc/>
    public override AssemblySymbol ContainingAssembly
    {
        get
        {
            var assemblySymbol = this.ContainingSymbol as AssemblySymbol;
            Debug.Assert(assemblySymbol is not null);
            return assemblySymbol;
        }
    }

    /// <remarks>.NET modules are always not contained by a .NET module.</remarks>
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    internal sealed override NetmoduleSymbol? ContainingNetmodule => null;
    #endregion

    /// <summary>
    /// Returns a ModuleSymbol representing the global (root) .NET namespace.
    /// </summary>
    /// <value>
    /// The ModuleSymbol representing the global (root) .NET namespace, with .NET module extent,
    /// that can be used to browse all of the symbols defined in this .NET module.
    /// </value>
    internal abstract ModuleSymbol GlobalNamespace { get; }

    /// <summary>
    /// Returns a ModuleSymbol representing the global (root) module.
    /// </summary>
    /// <value>
    /// The ModuleSymbol representing the global (root) module, with .NET module extent,
    /// that can be used to browse all of the symbols defined in this .NET module.
    /// </value>
    public abstract ModuleSymbol GlobalModule { get; }

    /// <summary>
    /// Returns module's ordinal within containing assembly's Modules array.
    /// </summary>
    /// <value>
    /// Module's ordinal within containing assembly's Modules array.
    /// <para>0 - for a source module, etc.</para>
    /// <para>-1 - for a module that doesn't have containing assembly, or has it, but is not part of Modules array.</para>
    /// </value>
    internal abstract int Ordinal { get; }

    /// <summary>
    /// Returns target architecture of the machine.
    /// </summary>
    /// <value>
    /// Target CPU architecture of the machine.
    /// </value>
    internal abstract Machine Machine { get; }

    /// <summary>
    /// Indicates that this PE file makes Win32 calls. See CorPEKind.pe32BitRequired for more information (http://msdn.microsoft.com/en-us/library/ms230275.aspx).
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this PE file makes Win32 calls; otherwise, <see langword="false"/>.
    /// </value>
    internal abstract bool Bit32Required { get; }

    /// <summary>
    /// Does this symbol represent a missing module.
    /// </summary>
    /// <value>
    /// Returns <see langword="true"/> if this symbol represent a missing module; otherwise, <see langword="false"/>.
    /// </value>
    internal abstract bool IsMissing { get; }

    /// <value>Returns <see cref="Accessibility.NotApplicable"/>.</value>
    /// <inheritdoc/>
    public sealed override Accessibility DeclaredAccessibility => Accessibility.NotApplicable;

    /// <value>Returns <see langword="false"/>.</value>
    /// <inheritdoc/>
    public sealed override bool IsAbstract => false;

    /// <value>Returns <see langword="false"/>.</value>
    /// <inheritdoc/>
    public sealed override bool IsExtern => false;

    /// <value>Returns <see langword="false"/>.</value>
    /// <inheritdoc/>
    public sealed override bool IsOverride => false;

    /// <value>Returns <see langword="false"/>.</value>
    /// <inheritdoc/>
    public sealed override bool IsSealed => false;

    /// <value>Returns <see langword="false"/>.</value>
    /// <inheritdoc/>
    public sealed override bool IsStatic => false;

    /// <value>Returns <see langword="false"/>.</value>
    /// <inheritdoc/>
    public sealed override bool IsVirtual => false;

    /// <summary>
    /// Returns an array of assembly identities for assemblies referenced by this module.
    /// </summary>
    /// <value>
    /// An array of assembly identities for assemblies referenced by this module.
    /// </value>
    /// <remarks>
    /// Items at the same position from <see cref="ReferencedAssemblies"/> and from <see cref="ReferencedAssemblySymbols"/> 
    /// correspond to each other.
    /// 
    /// The array and its content is provided by <see cref="ThisReferenceManager"/> and must not be modified.
    /// </remarks>
    public abstract ImmutableArray<AssemblyIdentity> ReferencedAssemblies { get; }

    /// <summary>
    /// Returns an array of assembly identities for assemblies referenced by this module.
    /// </summary>
    /// <value>
    /// An array of assembly identities for assemblies referenced by this module.
    /// </value>
    /// <remarks>
    /// Items at the same position from <see cref="ReferencedAssemblies"/> and from <see cref="ReferencedAssemblySymbols"/> 
    /// correspond to each other.
    /// 
    /// The array and its content is provided by <see cref="ThisReferenceManager"/> and must not be modified.
    /// </remarks>
    public abstract ImmutableArray<AssemblySymbol> ReferencedAssemblySymbols { get; }

    internal AssemblySymbol? GetReferencedAssemblySymbol(int index)
    {
        var symbols = this.ReferencedAssemblySymbols;
        if (index < symbols.Length)
            return symbols[index];

        // This .NET module must be a corlib where the original metadata contains assembly
        // references (see https://github.com/dotnet/roslyn/issues/13275).
        var assembly = this.ContainingAssembly;
        if (ReferenceEquals(assembly, assembly.CorLibrary))
            return null;

        throw new ArgumentOutOfRangeException(nameof(index));
    }

    internal abstract void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol? originatingSourceAssemblyDebugOnly = null);

    internal abstract bool HasUnifiedReferences { get; }

    internal abstract bool GetUnificationUseSiteDiagnostic(ref DiagnosticInfo result, TypeSymbol dependentType);

    /// <summary>
    /// Lookup a top level type referenced from metadata, names should be
    /// compared case-sensitively.
    /// </summary>
    /// <param name="emittedName">
    /// Full type name, possibly with generic name mangling.
    /// </param>
    /// <returns>
    /// Symbol for the type, or <see langword="null"/> if the type isn't found.
    /// </returns>
    internal abstract NamedTypeSymbol? LookupTopLevelMetadataType(ref MetadataTypeName emittedName);

    public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences => ImmutableArray<SyntaxReference>.Empty;

    public sealed override bool IsImplicitlyDeclared => base.IsImplicitlyDeclared;

    /// <summary>
    /// Given a module symbol, returns the corresponding module specific module symbol
    /// </summary>
    public ModuleSymbol GetNetmoduleModule(IModuleSymbol namespaceSymbol)
    {
#warning 未完成
        throw new NotImplementedException();
    }

    /// <summary>
    /// Given a .NET namespace symbol, returns the corresponding module specific .NET namespace symbol
    /// </summary>
    internal ModuleSymbol GetNetmoduleNamespace(IModuleSymbol namespaceSymbol)
    {
#warning 未完成
        throw new NotImplementedException();
    }

    public abstract bool AreLocalsZeroed { get; }

    /// <summary>
    /// If this symbol represents a metadata module returns the underlying <see cref="ModuleMetadata"/>.
    /// 
    /// Otherwise, this returns <see langword="null"/>.
    /// </summary>
    public abstract ModuleMetadata GetMetadata();

    /// <inheritdoc cref="Symbol()"/>
    internal NetmoduleSymbol() { }
}
