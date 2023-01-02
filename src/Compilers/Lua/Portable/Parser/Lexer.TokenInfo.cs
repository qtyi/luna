// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using MSCA::Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal partial class Lexer
{
    /// <summary>
    /// 存放语法标志的必要信息。
    /// </summary>
    internal partial struct TokenInfo
    {
        /// <summary>
        /// 直接语法类别。
        /// </summary>
        internal SyntaxKind Kind;
        /// <summary>
        /// 上下文语法类别。
        /// </summary>
        internal SyntaxKind ContextualKind;
        /// <summary>
        /// 语法标志的文本表示。
        /// </summary>
        internal string? Text;
        /// <summary>
        /// 语法标志的值类别。
        /// </summary>
        internal SpecialType ValueKind;
        /// <summary>
        /// 语法标志的字符串类型值。
        /// </summary>
        internal string? StringValue;
        /// <summary>
        /// 语法标志的64位整数类型值。
        /// </summary>
        internal long LongValue;
        /// <summary>
        /// 语法标志的64位整数类型值。
        /// </summary>
        /// <remarks>
        /// 主要用于承载紧跟着一个负号（<c>-</c>）的<c>0x8000000000000000</c>。
        /// </remarks>
        internal ulong ULongValue;
        /// <summary>
        /// 语法标志的64位双精度浮点数类型值。
        /// </summary>
        internal double DoubleValue;
    }

}
