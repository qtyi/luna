// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection.PortableExecutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
using Qtyi.CodeAnalysis.Lua;
namespace Qtyi.CodeAnalysis.Lua.Symbols;

using ThisReferenceManager = LuaCompilation.ReferenceManager;
#elif LANG_MOONSCRIPT
using Qtyi.CodeAnalysis.MoonScript;
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;

using ThisReferenceManager = MoonScriptCompilation.ReferenceManager;
#endif

partial class NetModuleSymbol : IModuleSymbolInternal
{
    /// <inheritdoc cref="Symbol()"/>
    internal NetModuleSymbol() { }

    /// <value>返回<see cref="SymbolKind.NetModule"/>。</value>
    /// <inheritdoc/>
    public sealed override SymbolKind Kind => SymbolKind.NetModule;

    /// <remarks>.NET模块符号必定不被另一个名称类型符号包含。</remarks>
    /// <value>返回<see langword="null"/>。</value>
    /// <inheritdoc/>
    public sealed override NamedTypeSymbol? ContainingType => null;

    /// <remarks>.NET模块符号必定不被另一个模块符号包含。</remarks>
    /// <value>返回<see langword="null"/>。</value>
    /// <inheritdoc/>
    public sealed override ModuleSymbol? ContainingModule => null;

    /// <remarks>模块必定被程序集包含。</remarks>
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

    /// <remarks>模块必定不被另一个模块包含。</remarks>
    /// <value>返回<see langword="null"/>。</value>
    /// <inheritdoc/>
    internal sealed override NetModuleSymbol? ContainingNetModule => null;

    /// <summary>
    /// 获取此模块符号在程序集符号模块数组中的序数。
    /// </summary>
    /// <value>
    /// 此模块符号在程序集符号模块数组中的序数。
    /// <para>返回<c>0</c>时，表示此模块是源代码模块。</para>
    /// <para>返回<c>-1</c>时，表示此模块没有被程序集包含，或被包含但不在其模块数组中。</para>
    /// </value>
    internal abstract int Ordinal { get; }

    /// <summary>
    /// 获取此模块符号的目标机器架构。
    /// </summary>
    /// <value>
    /// 此模块符号的目标机器架构。
    /// </value>
    internal abstract Machine Machine { get; }

    /// <summary>
    /// 获取一个值，指示此PE文件是否进行Win32调用。
    /// </summary>
    /// <value>
    /// 此PE文件要进行Win32调用时返回<see langword="true"/>；否则返回<see langword="false"/>。
    /// </value>
    internal abstract bool Bit32Required { get; }

    /// <summary>
    /// 获取一个值，指示此符号是否表示一个缺失的.NET模块。
    /// </summary>
    /// <value>
    /// 此符号表示一个缺失的.NET模块时返回<see langword="true"/>；否则返回<see langword="false"/>。
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
    /// 返回所有表示此模块符号引用的程序集的程序集身份。
    /// </summary>
    /// <value>
    /// 所有表示此模块符号引用的程序集的程序集身份。
    /// <para>此属性与<see cref="ReferencedAssemblySymbols"/>返回值的每一位相互对应。</para>
    /// </value>
    /// <remarks>
    /// 返回的数组及内容均由<see cref="ThisReferenceManager"/>产生，且不应被修改。
    /// </remarks>
    public abstract ImmutableArray<AssemblyIdentity> ReferencedAssemblies { get; }

    /// <summary>
    /// 返回所有表示此模块符号引用的程序集的程序集符号。
    /// </summary>
    /// <value>
    /// 所有表示此模块符号引用的程序集的程序集身份。
    /// <para>此属性与<see cref="ReferencedAssemblies"/>返回值的每一位相互对应。</para>
    /// </value>
    /// <remarks>
    /// 返回的数组及内容均由<see cref="ThisReferenceManager"/>产生，且不应被修改。
    /// </remarks>
    public abstract ImmutableArray<AssemblySymbol> ReferencedAssemblySymbols { get; }

    internal AssemblySymbol? GetReferencedAssemblySymbol(int index)
    {
        var symbols = this.ReferencedAssemblySymbols;
        if (index < symbols.Length)
            return symbols[index];

        // 当包含此模块的程序集不是核心库时，原始原数据不包含程序集引用。
        // https://github.com/dotnet/roslyn/issues/13275
        var assembly = this.ContainingAssembly;
        if (object.ReferenceEquals(assembly, assembly.CorLibrary))
            return null;

        throw new ArgumentOutOfRangeException(nameof(index));
    }

    internal abstract void SetReferences(ModuleReferences<AssemblySymbol> moduleReferences, SourceAssemblySymbol originatingSourceAssemblyDebugOnly = null);

    internal abstract bool HasUnifiedReferences { get; }

    internal abstract bool GetUnificationUseSiteDiagnostic(ref DiagnosticInfo result, TypeSymbol dependentType);

    public override ImmutableArray<SyntaxReference> DeclaringSyntaxReferences => ImmutableArray<SyntaxReference>.Empty;

    public sealed override bool IsImplicitlyDeclared => base.IsImplicitlyDeclared;
}
