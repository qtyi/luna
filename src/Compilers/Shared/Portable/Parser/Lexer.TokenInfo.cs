// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif  LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal partial class Lexer
{
    /// <summary>
    /// Stores information about a syntax token.
    /// </summary>
    internal partial struct TokenInfo
    {
        /// <summary>
        /// Syntax kind of this token.
        /// </summary>
        internal SyntaxKind Kind;
        /// <summary>
        /// Contextual syntax kind of this token.
        /// </summary>
        internal SyntaxKind ContextualKind;
        /// <summary>
        /// Raw text presentation of this token.
        /// </summary>
        internal string? Text;
        /// <summary>
        /// Value kind of the token represents.
        /// </summary>
        internal TokenValueType ValueKind;
        /// <summary>
        /// The <see cref="Utf8String"/> value this token represents.
        /// </summary>
        internal Utf8String? Utf8StringValue;
        /// <summary>
        /// The <see cref="string"/> value this token represents.
        /// </summary>
        internal string? StringValue;
        /// <summary>
        /// The <see cref="long"/> value this token represents.
        /// </summary>
        internal long LongValue;
        /// <summary>
        /// The <see cref="ulong"/> value this token represents.
        /// </summary>
        /// <remarks>
        /// Represents <c>-0x8000000000000000</c> if <see cref="ValueKind"/> is <see cref="TokenValueType.UInt64"/>.
        /// </remarks>
        internal ulong ULongValue;
        /// <summary>
        /// The <see cref="double"/> value this token represents.
        /// </summary>
        internal double DoubleValue;
    }

}
