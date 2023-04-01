// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class ModuleSymbol : INamespaceOrTypeSymbolInternal
{
    /// <inheritdoc cref="Symbol()"/>
    internal ModuleSymbol() { }

    /// <summary>
    /// 获取一个值，指示此模块符号是否为命名空间符号。
    /// </summary>
    /// <value>
    /// 若此模块符号为命名空间符号，则返回<see langword="true"/>；否则返回<see langword="false"/>。
    /// </value>
    public bool IsNamespace => this.Kind == SymbolKind.Namespace;

    /// <summary>
    /// 获取一个值，指示此模块符号是否为类型符号。
    /// </summary>
    /// <value>
    /// 若此模块符号为类型符号，则返回<see langword="true"/>；否则返回<see langword="false"/>。
    /// </value>
    public bool IsType => !this.IsNamespace;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsExtern => false;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsOverride => false;

    /// <value>返回<see langword="false"/>。</value>
    /// <inheritdoc/>
    public sealed override bool IsVirtual => false;

    #region 获取成员
    /// <summary>
    /// 获取此模块符号中的所有成员符号。
    /// </summary>
    /// <returns>含有此模块符号中的所有成员符号的不可变数组。若找不到成员符号，则返回空列表。</returns>
    public abstract ImmutableArray<Symbol> GetMembers();

    /// <summary>
    /// 获取此模块符号中的所有指定名称的成员符号。
    /// </summary>
    /// <param name="name">要查找的成员符号的名称。</param>
    /// <returns>含有此模块符号中的所有指定名称的成员符号的不可变数组。若找不到符合条件的成员符号，则返回空列表。</returns>
    public abstract ImmutableArray<Symbol> GetMembers(string name);

    /// <summary>
    /// 获取此模块符号中的所有名称类型成员符号。
    /// </summary>
    /// <returns>含有此模块符号中的所有名称类型成员符号的不可变数组。若找不到符合条件的成员符号，则返回空列表。</returns>
    public abstract ImmutableArray<NamedTypeSymbol> GetTypeMembers();

    /// <summary>
    /// 获取此模块符号中的所有指定名称的名称类型成员符号。
    /// </summary>
    /// <param name="name">要查找的名称类型成员符号的名称。</param>
    /// <returns>含有此模块符号中的所有指定名称的名称类型成员符号的不可变数组。若找不到符合条件的成员符号，则返回空列表。</returns>
    public abstract ImmutableArray<Symbol> GetTypeMembers(string name);

    /// <summary>
    /// 获取此模块符号中的所有指定名称和类型参数个数的名称类型成员符号。
    /// </summary>
    /// <param name="name">要查找的名称类型成员符号的名称。</param>
    /// <param name="arity">要查找的名称类型成员符号的类型参数个数。</param>
    /// <returns>含有此模块符号中的所有指定名称和类型参数个数的名称类型成员符号的不可变数组。若找不到符合条件的成员符号，则返回空列表。</returns>
    public abstract ImmutableArray<Symbol> GetTypeMembers(string name, int arity);
    #endregion

#warning 未完成
}
