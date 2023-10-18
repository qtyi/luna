// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Microsoft.CodeAnalysis;
using Microsoft.Cci;
using Qtyi.CodeAnalysis.Symbols;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;

using ThisDiagnosticInfo = LuaDiagnosticInfo;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;

using ThisDiagnosticInfo = MoonScriptDiagnosticInfo;
#endif

partial class AssemblySymbol : IAssemblySymbolInternal
{
    /// <summary>
    /// Gets the name of the assembly.
    /// </summary>
    /// <value>
    /// The name of the assembly.
    /// </value>
    /// <remarks>
    /// This is equivalent to <see cref="Identity"/>.<see cref="AssemblyIdentity.Name"/>, but may be 
    /// much faster to retrieve for source code assemblies, since it does not require binding
    /// the assembly-level attributes that contain the version number and other assembly
    /// information.
    /// </remarks>
    public override string Name => this.Identity.Name;

    /// <value>Returns <see cref="SymbolKind.Assembly"/>.</value>
    /// <inheritdoc/>
    public sealed override SymbolKind Kind => SymbolKind.Assembly;

    #region Containing
    /// <remarks>Assemblies are always not contained by a symbol.</remarks>
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    public sealed override Symbol? ContainingSymbol => null;

    /// <remarks>Assemblies are always not contained by a named type.</remarks>
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    public sealed override NamedTypeSymbol? ContainingType => null;

    /// <remarks>Assemblies are always not contained by a module.</remarks>
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    public sealed override ModuleSymbol? ContainingModule => null;

    /// <remarks>Assemblies are always not contained by a .NET module.</remarks>
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    internal sealed override NetmoduleSymbol? ContainingNetmodule => null;

    /// <remarks>Assemblies are always not contained by an assembly.</remarks>
    /// <value>Returns <see langword="null"/>.</value>
    /// <inheritdoc/>
    public sealed override AssemblySymbol? ContainingAssembly => null;
    #endregion

    /// <summary>
    /// Gets the identity of this assembly.
    /// </summary>
    /// <value>
    /// The identity of this assembly.
    /// </value>
    public abstract AssemblyIdentity Identity { get; }

    /// <summary>
    /// 获取此程序集符号的程序集版本模式。
    /// </summary>
    /// <value>
    /// 此程序集符号的程序集版本模式。
    /// <para>当在<see cref="AssemblyVersionAttribute"/>中指定的程序集版本字符串包含“<c>*</c>”时，“<c>*</c>”将被替换为<see cref="ushort.MaxValue"/>；</para>
    /// <para>当程序集版本字符串不包含“<c>*</c>”时返回<see langword="null"/>。</para>
    /// </value>
    /// <example>
    /// <c>AssemblyVersion("1.2.*")</c>将被表示为“1.2.65535.65535”
    /// <c>AssemblyVersion("1.2.3.*")</c>将被表示为“1.2.3.65535”
    /// </example>
    public abstract Version? AssemblyVersionPattern { get; }

    /// <summary>
    /// Gets the merged root .NET namespace that contains all .NET namespace defined in
    /// the .NET modules of this assembly. If there is just one .NET module in this
    /// assembly, this property just returns the <see cref="GlobalNamespace"/> of that
    /// .NET module.
    /// </summary>
    internal abstract ModuleSymbol GlobalNamespace { get; }

    /// <summary>
    /// Gets the merged root module that contains all modules defined in the .NET modules
    /// of this assembly. If there is just one .NET module in this assembly, this property
    /// just returns the <see cref="GlobalModule"/> of that .NET module.
    /// </summary>
    public abstract ModuleSymbol GlobalModule { get; }

    /// <summary>
    /// 获取此程序集符号中的所有模块。
    /// </summary>
    /// <value>
    /// 此程序集符号中的所有模块。
    /// <para>返回值至少包含一个项。</para>
    /// <para>返回值第一项表示此程序集中包含清单的主模块。</para>
    /// </value>
    public abstract ImmutableArray<NetmoduleSymbol> Netmodules { get; }

    /// <summary>
    /// 获取此程序集符号的目标机器架构。
    /// </summary>
    /// <value>
    /// 此程序集符号的目标机器架构。
    /// </value>
    internal Machine Machine => this.Netmodules[0].Machine;

    /// <inheritdoc cref="NetmoduleSymbol.Bit32Required"/>
    internal bool Bit32Required => this.Netmodules[0].Bit32Required;

    /// <summary>
    /// 获取一个值，指示此程序集是否缺失。
    /// </summary>
    /// <value>
    /// 若此此程序集缺失，则返回<see langword="true"/>；否则返回<see langword="false"/>。
    /// </value>
    internal abstract bool IsMissing { get; }

    /// <value>返回<see cref="Accessibility.NotApplicable"/>。</value>
    /// <inheritdoc/>
    public sealed override Accessibility DeclaredAccessibility => Accessibility.NotApplicable;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsAbstract => false;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsExtern => false;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsOverride => false;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsSealed => false;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsStatic => false;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsVirtual => false;

    /// <summary>
    /// 获取一个值，指示此程序集是否包含交互式代码。
    /// </summary>
    /// <value>
    /// 若此此程序集包含交互式代码，则返回<see langword="true"/>；否则返回<see langword="false"/>。
    /// </value>
    public virtual bool IsInteractive => false;

    public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences => ImmutableArray<SyntaxReference>.Empty;

    #region 核心库
    private AssemblySymbol? _corLibrary;

    [MemberNotNull(nameof(_corLibrary))]
    internal AssemblySymbol CorLibrary
    {
        get
        {
            Debug.Assert(this._corLibrary is not null);
            return this._corLibrary;
        }
    }

    [MemberNotNull(nameof(_corLibrary))]
    internal void SetCorLibrary(AssemblySymbol corLibrary)
    {
        Debug.Assert(this._corLibrary is null);
        this._corLibrary = corLibrary;
    }
    #endregion

    internal abstract ImmutableArray<byte> PublicKey { get; }

    /// <summary>
    /// Lookup a top level type referenced from metadata, names should be
    /// compared case-sensitively.
    /// </summary>
    /// <param name="emittedName">
    /// Full type name with generic name mangling.
    /// </param>
    /// <remarks></remarks>
    /// <returns>The symbol for the type declared in this assembly, or null.</returns>
    internal abstract NamedTypeSymbol? LookupDeclaredTopLevelMetadataType(ref MetadataTypeName emittedName);

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
    internal abstract NamedTypeSymbol LookupDeclaredOrForwardedTopLevelMetadataType(ref MetadataTypeName emittedName, ConsList<AssemblySymbol>? visitedAssemblies);

    /// <summary>
    /// Returns the type symbol for a forwarded type based on its canonical CLR metadata name.
    /// The name should refer to a non-nested type. If type with this name is not forwarded,
    /// null is returned.
    /// </summary>
    public NamedTypeSymbol? ResolveForwardedType(string fullyQualifiedMetadataName)
    {
        if (fullyQualifiedMetadataName == null)
        {
            throw new ArgumentNullException(nameof(fullyQualifiedMetadataName));
        }

        var emittedName = MetadataTypeName.FromFullName(fullyQualifiedMetadataName);
        return TryLookupForwardedMetadataTypeWithCycleDetection(ref emittedName, visitedAssemblies: null);
    }

    /// <summary>
    /// Look up the given metadata type, if it is forwarded.
    /// </summary>
    internal virtual NamedTypeSymbol? TryLookupForwardedMetadataTypeWithCycleDetection(ref MetadataTypeName emittedName, ConsList<AssemblySymbol>? visitedAssemblies)
    {
        return null;
    }

    internal ErrorTypeSymbol CreateCycleInTypeForwarderErrorTypeSymbol(ref MetadataTypeName emittedName)
    {
        DiagnosticInfo diagnosticInfo = new ThisDiagnosticInfo(ErrorCode.ERR_CycleInTypeForwarder, emittedName.FullName, this.Name);
        return new MissingMetadataTypeSymbol.TopLevel(this.Netmodules[0], ref emittedName, diagnosticInfo);
    }

    internal ErrorTypeSymbol CreateMultipleForwardingErrorTypeSymbol(ref MetadataTypeName emittedName, NetmoduleSymbol forwardingModule, AssemblySymbol destination1, AssemblySymbol destination2)
    {
        var diagnosticInfo = new ThisDiagnosticInfo(ErrorCode.ERR_TypeForwardedToMultipleAssemblies, forwardingModule, this, emittedName.FullName, destination1, destination2);
        return new MissingMetadataTypeSymbol.TopLevel(forwardingModule, ref emittedName, diagnosticInfo);
    }

    internal abstract IEnumerable<NamedTypeSymbol> GetAllTopLevelForwardedTypes();

#nullable disable

    /// <summary>
    /// Lookup declaration for predefined CorLib type in this Assembly.
    /// </summary>
    /// <returns>The symbol for the pre-defined type or an error type if the type is not defined in the core library.</returns>
    internal abstract NamedTypeSymbol GetDeclaredSpecialType(SpecialType type);

    /// <summary>
    /// Register declaration of predefined CorLib type in this Assembly.
    /// </summary>
    /// <param name="corType"></param>
    internal virtual void RegisterDeclaredSpecialType(NamedTypeSymbol corType)
    {
        throw ExceptionUtilities.Unreachable();
    }

    /// <summary>
    /// Continue looking for declaration of predefined CorLib type in this Assembly
    /// while symbols for new type declarations are constructed.
    /// </summary>
    internal virtual bool KeepLookingForDeclaredSpecialTypes
    {
        get
        {
            throw ExceptionUtilities.Unreachable();
        }
    }

    /// <summary>
    /// Gets the set of type identifiers from this assembly.
    /// </summary>
    /// <remarks>
    /// These names are the simple identifiers for the type, and do not include namespaces,
    /// outer type names, or type parameters.
    /// 
    /// This functionality can be used for features that want to quickly know if a name could be
    /// a type for performance reasons.  For example, classification does not want to incur an
    /// expensive binding call cost if it knows that there is no type with the name that they
    /// are looking at.
    /// </remarks>
    public abstract ICollection<string> TypeNames { get; }

    /// <summary>
    /// Gets the set of .NET namespace names from this assembly.
    /// </summary>
    /// <remarks>
    /// These names are the simple identifiers for the .NET namespace, and do not include
    /// outer .NET namespaces names。
    /// 
    /// This functionality can be used for features that want to quickly know if a name could be
    /// a .NET namespace for performance reasons.  For example, classification does not want to
    /// incur an expensive binding call cost if it knows that there is no .NET namespace with the
    /// name that they are looking at.
    /// </remarks>
    internal abstract ICollection<string> NamespaceNames { get; }

    public abstract ICollection<string> ModuleNames { get; }

    /// <summary>
    /// Returns true if this assembly might contain extension methods. If this property
    /// returns false, there are no extension methods in this assembly.
    /// </summary>
    /// <remarks>
    /// This property allows the search for extension methods to be narrowed quickly.
    /// </remarks>
    public abstract bool MightContainExtensionMethods { get; }

    /// <summary>
    /// If this symbol represents a metadata assembly returns the underlying <see cref="AssemblyMetadata"/>.
    /// 
    /// Otherwise, this returns <see langword="null"/>.
    /// </summary>
    public abstract AssemblyMetadata GetMetadata();

    /// <summary>
    /// Gets the symbol for the pre-defined type from core library associated with this assembly.
    /// </summary>
    /// <returns>The symbol for the pre-defined type or an error type if the type is not defined in the core library.</returns>
    internal NamedTypeSymbol GetSpecialType(SpecialType type) => this.CorLibrary.GetDeclaredSpecialType(type);

    /// <summary>
    /// Gets a type symbol for the dynamic type.
    /// </summary>
    /// <value>
    /// The type symbol for the dynamic type.
    /// </value>
    internal static TypeSymbol DynamicType => DynamicTypeSymbol.Instance;

    /// <summary>
    /// A named type symbol for the .NET <see cref="object"/> type.
    /// </summary>
    /// <value>
    /// The named type symbol for the .NET <see cref="object"/> type, which could have a TypeKind of
    /// <see cref="TypeKind.Error"/> if there was no COR Library in a compilation using the assembly.
    /// </value>
    internal NamedTypeSymbol ObjectType => this.GetSpecialType(SpecialType.System_Object);

    /// <summary>
    /// Get symbol for predefined type from Cor Library used by this assembly.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    internal NamedTypeSymbol GetPrimitiveType(PrimitiveTypeCode type) => this.GetSpecialType(SpecialTypes.GetTypeFromMetadataName(type));

    public NamedTypeSymbol? GetTypeByMetadataName(string fullyQualifiedMetadataName)
    {
#warning 未完成
        throw new NotImplementedException();
    }

    /// <inheritdoc cref="Symbol()"/>
    internal AssemblySymbol() { }

    #region IAssemblySymbolInternal
    IAssemblySymbolInternal IAssemblySymbolInternal.CorLibrary => this.CorLibrary;
    #endregion

    #region Microsoft.CodeAnalysis.Symbols.IAssemblySymbolInternal
    Microsoft.CodeAnalysis.Symbols.IAssemblySymbolInternal Microsoft.CodeAnalysis.Symbols.IAssemblySymbolInternal.CorLibrary => this.CorLibrary;
    #endregion
}
