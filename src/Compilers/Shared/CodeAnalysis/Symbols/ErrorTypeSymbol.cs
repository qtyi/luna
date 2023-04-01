// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Symbols;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Symbols;
#endif

partial class ErrorTypeSymbol
{
    /// <inheritdoc cref="TypeSymbol()"/>
    internal ErrorTypeSymbol() : base() { }

    /// <summary>
    /// 表示未知的返回值的错误类型符号。
    /// </summary>
    internal static readonly ErrorTypeSymbol UnknownResultType = new();

    /// <summary>
    /// 获取此错误类型符号基于的诊断信息。
    /// </summary>
    /// <value>
    /// 此错误类型符号基于的诊断信息。
    /// </value>
    internal abstract DiagnosticInfo? ErrorInfo { get; }

    /// <summary>
    /// 获取一段摘要，描述此类型被判定为坏类型的原因。
    /// </summary>
    /// <value>
    /// 描述此类型被判定为坏类型的原因的摘要。
    /// </value>
    internal virtual LookupResultKind ResultKind => LookupResultKind.Empty;
}
