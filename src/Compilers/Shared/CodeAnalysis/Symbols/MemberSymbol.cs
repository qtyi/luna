// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Symbols;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class MemberSymbol : IFieldSymbolInternal
{
    /// <inheritdoc cref="Symbol()"/>
    internal MemberSymbol() { }

    public abstract bool IsVolatile { get; }

    public abstract RefKind RefKind { get; }

    public abstract FlowAnalysisAnnotations FlowAnalysisAnnotations { get; }

    #region 类型
    public abstract TypeSymbol Type { get; }
    #endregion

    public virtual bool RequiresInstanceReceiver => !this.IsStatic;

    internal abstract bool HasSpecialName { get; }

    internal abstract bool HasRuntimeSpecialName { get; }

    internal abstract bool IsNotSerialized { get; }

    internal abstract bool IsRequired { get; }

    #region 定义
    /// <summary>
    /// 获取此成员符号的原始定义。
    /// </summary>
    /// <value>
    /// 此成员符号的原始定义。
    /// 若此成员符号是由另一个成员符号通过类型置换创建的，则返回其最原始的在源代码或元数据中的定义。
    /// </value>
    public new MemberSymbol OriginalDefinition => this.OriginalMemberSymbolDefinition;

    /// <summary>
    /// 由子类重写，获取此成员符号的原始定义。
    /// </summary>
    /// <value>
    /// <see cref="OriginalDefinition"/>使用此返回值。
    /// </value>
    protected abstract MemberSymbol OriginalMemberSymbolDefinition { get; }

    protected sealed override Symbol OriginalSymbolDefinition => this.OriginalMemberSymbolDefinition;
    #endregion

    #region 公共符号
#warning 未完成
    protected override ISymbol CreateISymbol()
    {
        throw new NotImplementedException();
    }
    #endregion

}
