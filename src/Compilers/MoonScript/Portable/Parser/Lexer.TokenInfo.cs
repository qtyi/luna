// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
    partial struct TokenInfo
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
        /// 语法标记的文本表示。
        /// </summary>
        internal string? Text;
        /// <summary>
        /// 语法标记的值类别。
        /// </summary>
        internal SpecialType ValueKind;
        /// <summary>
        /// 语法标记的UTF-8字符串类型值。
        /// </summary>
        /// <remarks>
        /// UTF-8字符串由不可变<see cref="byte"/>数组表示。
        /// </remarks>
        internal ImmutableArray<byte> Utf8StringValue;
        /// <summary>
        /// 语法标记的字符串类型值。
        /// </summary>
        internal string? StringValue;
        /// <summary>
        /// 语法标记的64位整数类型值。
        /// </summary>
        internal long LongValue;
        /// <summary>
        /// 语法标记的64位整数类型值。
        /// </summary>
        /// <remarks>
        /// 主要用于承载紧跟着一个负号（<c>-</c>）的<c>0x8000000000000000</c>。
        /// </remarks>
        internal ulong ULongValue;
        /// <summary>
        /// 语法标记的64位双精度浮点数类型值。
        /// </summary>
        internal double DoubleValue;
        internal int InnerIndent;
        /// <summary>
        /// 语法标记的语法标记列表类型值。
        /// </summary>
        internal ImmutableArray<SyntaxToken> SyntaxTokenArrayValue;
    }
}
