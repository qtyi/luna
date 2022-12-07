// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using Roslyn.Utilities;

namespace Qtyi.CodeAnalysis.Lua;

public static partial class SyntaxFacts
{
    public static partial bool IsWhiteSpace(char c) => CharacterInfo.IsWhiteSpace(c);

    public static partial bool IsNewLine(char c) => CharacterInfo.IsNewLine(c);

    public static partial bool IsNewLine(char firstChar, char secondChar) => CharacterInfo.IsNewLine(firstChar, secondChar);

    public static partial bool IsIdentifierStartCharacter(char c) =>
        UnicodeCharacterUtilities.IsIdentifierStartCharacter(c);

    public static partial bool IsIdentifierPartCharacter(char c) =>
        UnicodeCharacterUtilities.IsIdentifierPartCharacter(c);

    public static partial bool IsValidIdentifier(string? name) =>
        UnicodeCharacterUtilities.IsValidIdentifier(name);
}
