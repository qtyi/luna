// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class Lexer
{
    /// <summary>
    /// Specifies the Ids of value that a syntax token presents.
    /// </summary>
    internal enum TokenValueType : sbyte
    {
        /// <inheritdoc cref="SpecialType.None"/>
        None = SpecialType.None,

        /// <inheritdoc cref="SpecialType.System_Boolean"/>
        Boolean = SpecialType.System_Boolean,

        /// <inheritdoc cref="SpecialType.System_Int64"/>
        Int64 = SpecialType.System_Int64,

        /// <inheritdoc cref="SpecialType.System_UInt64"/>
        UInt64 = SpecialType.System_UInt64,

        /// <inheritdoc cref="SpecialType.System_Double"/>
        Double = SpecialType.System_Double,

        /// <inheritdoc cref="SpecialType.System_String"/>
        String = SpecialType.System_String,

        /// <summary>
        /// Indicates that the type is <see cref="Utf8String"/>.
        /// </summary>
        Utf8String = SpecialType.Count + 1,
    }
}
