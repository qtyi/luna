// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.PortableExecutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class AssemblySymbol : IAssemblySymbolInternal
{
    /// <inheritdoc cref="Symbol()"/>
    internal AssemblySymbol() { }

    /// <summary>
    /// 获取此程序集符号的名称。
    /// </summary>
    /// <value>
    /// 此程序集符号的名称。
    /// </value>
    public override string Name => this.Identity.Name;

    /// <value>返回<see cref="SymbolKind.Assembly"/>。</value>
    /// <inheritdoc/>
    public sealed override SymbolKind Kind => SymbolKind.Assembly;

    /// <remarks>程序集符号必定不被另一个符号包含。</remarks>
    /// <value>返回<see langword="null"/>。</value>
    /// <inheritdoc/>
    public sealed override Symbol? ContainingSymbol => null;

    /// <remarks>程序集符号必定不被另一个名称类型符号包含。</remarks>
    /// <value>返回<see langword="null"/>。</value>
    /// <inheritdoc/>
    public sealed override NamedTypeSymbol? ContainingType => null;

    /// <remarks>程序集符号必定不被另一个模块符号包含。</remarks>
    /// <value>返回<see langword="null"/>。</value>
    /// <inheritdoc/>
    public sealed override ModuleSymbol? ContainingModule => null;

    /// <remarks>程序集必定不被另一个.NET模块包含。</remarks>
    /// <value>返回<see langword="null"/>。</value>
    /// <inheritdoc/>
    internal sealed override NetModuleSymbol? ContainingNetModule => null;

    /// <remarks>程序集必定不被另一个程序集包含。</remarks>
    /// <value>返回<see langword="null"/>。</value>
    /// <inheritdoc/>
    public sealed override AssemblySymbol? ContainingAssembly => null;

    /// <summary>
    /// 获取此程序集符号的程序集身份。
    /// </summary>
    /// <value>
    /// 此程序集符号的程序集身份。
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
    /// 获取此程序集符号中的所有模块。
    /// </summary>
    /// <value>
    /// 此程序集符号中的所有模块。
    /// <para>返回值至少包含一个项。</para>
    /// <para>返回值第一项表示此程序集中包含清单的主模块。</para>
    /// </value>
    public abstract ImmutableArray<NetModuleSymbol> NetModules { get; }

    /// <summary>
    /// 获取此程序集符号的目标机器架构。
    /// </summary>
    /// <value>
    /// 此程序集符号的目标机器架构。
    /// </value>
    internal Machine Machine => this.NetModules[0].Machine;

    /// <inheritdoc cref="NetModuleSymbol.Bit32Required"/>
    internal bool Bit32Required => this.NetModules[0].Bit32Required;

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

    IAssemblySymbolInternal IAssemblySymbolInternal.CorLibrary => this.CorLibrary;
    #endregion

    internal abstract ImmutableArray<byte> PublicKey { get; }

}
