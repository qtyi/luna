// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#else
#error Language not supported.
#endif

/// <summary>
/// 定义一系列用于编译器决定如何处理Unicode字符的方法。
/// </summary>
public static partial class SyntaxFacts
{
    /// <inheritdoc cref="CharacterInfo.IsWhiteSpace(char)"/>
    public static partial bool IsWhiteSpace(char c);

    /// <inheritdoc cref="CharacterInfo.IsNewLine(char)"/>
    public static partial bool IsNewLine(char c);

    /// <inheritdoc cref="CharacterInfo.IsNewLine(char, char)"/>
    public static partial bool IsNewLine(char firstChar, char secondChar);

    /// <inheritdoc cref="CharacterInfo.IsHexDigit(char)"/>
    internal static bool IsHexDigit(char c) => c.IsHexDigit();

    /// <inheritdoc cref="CharacterInfo.IsBinaryDigit(char)"/>
    internal static bool IsBinaryDigit(char c) => c.IsBinaryDigit();

    /// <inheritdoc cref="CharacterInfo.IsDecDigit(char)"/>
    internal static bool IsDecDigit(char c) => c.IsDecDigit();

    /// <inheritdoc cref="CharacterInfo.HexValue(char)"/>
    internal static int HexValue(char c) => c.HexValue();

    /// <inheritdoc cref="CharacterInfo.BinaryValue(char)"/>
    internal static int BinaryValue(char c) => c.BinaryValue();

    /// <inheritdoc cref="CharacterInfo.DecValue(char)"/>
    internal static int DecValue(char c) => c.DecValue();

    /// <summary>
    /// 指定的Unicode字符是否可以是标识符的第一个字符。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns>若<paramref name="c"/>的值可以是标识符的第一个字符则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static partial bool IsIdentifierStartCharacter(char c);

    /// <summary>
    /// 指定的Unicode字符是否可以是标识符的后续字符。
    /// </summary>
    /// <param name="c">一个Unicode字符。</param>
    /// <returns>若<paramref name="c"/>的值可以是标识符的后续字符则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static partial bool IsIdentifierPartCharacter(char c);

    /// <summary>
    /// 指定的名称是否是一个合法的标识符。
    /// </summary>
    /// <param name="name">一个标识符名称。</param>
    /// <returns>若<paramref name="name"/>表示的是一个合法的标识符则返回<see langword="true"/>，否则返回<see langword="false"/>。</returns>
    public static partial bool IsValidIdentifier([NotNullWhen(true)] string? name);
}
