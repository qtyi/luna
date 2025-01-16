// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

/// <summary>
/// 表示应用于一个<see cref="Symbol"/>上的一个特性。
/// </summary>
internal abstract partial class
#if LANG_LUA
    LuaAttributeData
#elif LANG_MOONSCRIPT
    MoonScriptAttributeData
#endif
    : AttributeData
{
    private ThreeState _lazyInSecurityAttribute = ThreeState.Unknown;

    /// <summary>
    /// 获取应用的特性的有名类型符号。
    /// </summary>
    public new abstract NamedTypeSymbol? AttributeClass { get; }

    /// <summary>
    /// 获取应用的特性的构造函数。
    /// </summary>
    public new abstract ModuleSymbol? AttributeConstructor { get; }

    public new abstract SyntaxReference? ApplicationSyntaxReference { get; }

    // 重写以应用MemberNotNullWhen特性。
    [MemberNotNullWhen(true, nameof(AttributeClass), nameof(AttributeConstructor))]
    internal override bool HasErrors
    {
        get
        {
            var hasErrors = base.HasErrors;
            if (!hasErrors)
            {
                Debug.Assert(AttributeClass is not null);
                Debug.Assert(AttributeConstructor is not null);
            }

            return hasErrors;
        }
    }
}
