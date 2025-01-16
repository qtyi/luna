// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

public static partial class SyntaxFacts
{
    /// <summary>
    /// Gets text representation of syntax kind.
    /// </summary>
    /// <param name="kind">Syntax kind to get text representation of.</param>
    /// <returns>A string represents the syntax kind.</returns>
    public static partial string GetText(SyntaxKind kind);

    #region Keyword
    /// <summary>
    /// Gets all syntax kinds that is keyword kinds.
    /// </summary>
    /// <param name="version">Version of language.</param>
    /// <returns>All keyword kinds.</returns>
    public static IEnumerable<SyntaxKind> GetKeywordKinds(LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return GetReservedKeywordKindsInternal(version).Concat(GetContextualKeywordKindsInternal(version));
    }

    /// <summary>
    /// Checks if a syntax kind in specified language version is a keyword kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a keyword kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsKeywordKind(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsKeywordKindInternal(kind, version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="IsKeywordKind(SyntaxKind, LanguageVersion)"/>
    private static bool IsKeywordKindInternal(SyntaxKind kind, LanguageVersion version) =>
        // Reserved Keywords
        IsReservedKeywordInternal(kind, version) ||

        // Contextual Keywords
        IsContextualKeywordInternal(kind, version);

    /// <summary>
    /// Gets syntax kind which represents a keyword from raw text.
    /// </summary>
    /// <param name="text">Raw text to get syntax kind from.</param>
    /// <returns>A syntax kind which represents a keyword.</returns>
    public static SyntaxKind GetKeywordKind(string text)
    {
        SyntaxKind kind;

        if ((kind = GetReservedKeywordKind(text)) is not SyntaxKind.None)
            return kind;

        if ((kind = GetContextualKeywordKind(text)) is not SyntaxKind.None)
            return kind;

        return SyntaxKind.None;
    }

    #region Reserved Keyword
    /// <summary>
    /// Gets all syntax kinds that is reserved keyword kinds.
    /// </summary>
    /// <param name="version">Version of language.</param>
    /// <returns>All reserved keyword kinds.</returns>
    public static IEnumerable<SyntaxKind> GetReservedKeywordKinds(LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return GetReservedKeywordKindsInternal(version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="GetReservedKeywordKinds(LanguageVersion)"/>
    private static partial IEnumerable<SyntaxKind> GetReservedKeywordKindsInternal(LanguageVersion version);

    /// <summary>
    /// Checks if a syntax kind in specified language version is a reserved keyword kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a reserved keyword kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsReservedKeyword(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsReservedKeywordInternal(kind, version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="IsReservedKeyword(SyntaxKind, LanguageVersion)"/>
    private static partial bool IsReservedKeywordInternal(SyntaxKind kind, LanguageVersion version);

    /// <summary>
    /// Gets syntax kind which represents a reserved keyword from raw text.
    /// </summary>
    /// <param name="text">Raw text to get syntax kind from.</param>
    /// <returns>A syntax kind which represents a reserved keyword.</returns>
    public static partial SyntaxKind GetReservedKeywordKind(string text);
    #endregion

    #region Contextual Keyword
    /// <summary>
    /// Gets all syntax kinds that is contextual keyword kinds.
    /// </summary>
    /// <param name="version">Version of language.</param>
    /// <returns>All contextual keyword kinds.</returns>
    public static IEnumerable<SyntaxKind> GetContextualKeywordKinds(LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return GetContextualKeywordKindsInternal(version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="GetContextualKeywordKinds(LanguageVersion)"/>
    private static partial IEnumerable<SyntaxKind> GetContextualKeywordKindsInternal(LanguageVersion version);

    /// <summary>
    /// Checks if a syntax kind in specified language version is a contextual keyword kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a contextual keyword kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsContextualKeyword(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsContextualKeywordInternal(kind, version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="IsContextualKeyword(SyntaxKind, LanguageVersion)"/>
    private static partial bool IsContextualKeywordInternal(SyntaxKind kind, LanguageVersion version);

    /// <summary>
    /// Gets syntax kind which represents a contextual keyword from raw text.
    /// </summary>
    /// <param name="text">Raw text to get syntax kind from.</param>
    /// <returns>A syntax kind which represents a contextual keyword.</returns>
    public static partial SyntaxKind GetContextualKeywordKind(string text);
    #endregion
    #endregion

    #region Punctuation
    /// <summary>
    /// Gets all syntax kinds that is punctuation kinds.
    /// </summary>
    /// <param name="version">Version of language.</param>
    /// <returns>All punctuation kinds.</returns>
    public static IEnumerable<SyntaxKind> GetPunctuationKinds(LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return GetPunctuationKindsInternal(version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="GetPunctuationKinds(LanguageVersion)"/>
    private static partial IEnumerable<SyntaxKind> GetPunctuationKindsInternal(LanguageVersion version);

    /// <summary>
    /// Checks if a syntax kind in specified language version is a punctuation kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a punctuation kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsPunctuation(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsPunctuationInternal(kind, version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="IsPunctuation(SyntaxKind, LanguageVersion)"/>
    private static partial bool IsPunctuationInternal(SyntaxKind kind, LanguageVersion version);

    /// <summary>
    /// Gets syntax kind which represents a punctuation from raw text.
    /// </summary>
    /// <param name="text">Raw text to get syntax kind from.</param>
    /// <returns>A syntax kind which represents a punctuation.</returns>
    public static partial SyntaxKind GetPunctuationKind(string text);
    #endregion

    /// <summary>
    /// Checks if a syntax kind in specified language version is a punctuation kind or a keyword kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a punctuation kind or a keyword kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsPunctuationOrKeyword(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsPunctuationInternal(kind, version) ||
               IsKeywordKindInternal(kind, version) ||
               kind == SyntaxKind.EndOfFileToken;
    }

    /// <summary>
    /// Checks if a syntax kind in specified language version is a token kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a token kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsAnyToken(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsAnyTokenInternal(kind, version);
    }

    /// <summary>
    /// Checks if a syntax kind in any language version is a token kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a token kind in any language; otherwise, <see langword="false"/>.</returns>
    public static bool IsAnyToken(SyntaxKind kind)
    {
        foreach (LanguageVersion version in Enum.GetValues(typeof(LanguageVersion)))
        {
            if (!version.IsValid())
                continue;

            if (IsAnyTokenInternal(kind, version))
                return true;
        }

        return false;
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="IsAnyToken(SyntaxKind, LanguageVersion)"/>
    private static partial bool IsAnyTokenInternal(SyntaxKind kind, LanguageVersion version);

    /// <summary>
    /// 指定语法种类是否表示琐碎内容。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示琐碎内容，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsTrivia(SyntaxKind kind, LanguageVersion version) =>
        // Whitespace trivia
        IsWhitespaceTrivia(kind, version) ||
        // Comment trivia
        IsCommentTrivia(kind, version) ||
        // Disabled text
        kind == SyntaxKind.DisabledTextTrivia ||
        // Preprocessor directive
        IsPreprocessorDirective(kind, version) ||
        // Structured trivia
        IsStructuredTrivia(kind, version);

    public static partial bool IsWhitespaceTrivia(SyntaxKind kind, LanguageVersion version);

    /// <summary>
    /// 指定语法种类是否表示注释琐碎内容。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示注释琐碎内容，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static partial bool IsCommentTrivia(SyntaxKind kind, LanguageVersion version);

    public static partial bool IsPreprocessorDirective(SyntaxKind kind, LanguageVersion version);

    public static partial bool IsStructuredTrivia(SyntaxKind kind, LanguageVersion version);

    /// <summary>
    /// 指定语法种类是否表示名称。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示名称，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static partial bool IsName(SyntaxKind kind);

    public static partial bool IsLiteralExpression(SyntaxKind expression);

    public static bool IsLiteralExpressionToken(SyntaxKind token, LanguageVersion version) => GetLiteralExpression(token, version) != SyntaxKind.None;

    public static partial SyntaxKind GetLiteralExpression(SyntaxKind token, LanguageVersion version);

    public static partial bool IsUnaryExpression(SyntaxKind expression);

    public static bool IsUnaryExpressionOperatorToken(SyntaxKind token, LanguageVersion version) => GetUnaryExpression(token, version) != SyntaxKind.None;

    public static partial SyntaxKind GetUnaryExpression(SyntaxKind token, LanguageVersion version);

    public static partial SyntaxKind GetUnaryExpressionOperatorToken(SyntaxKind expression, LanguageVersion version);

    public static partial bool IsBinaryExpression(SyntaxKind expression);

    public static bool IsBinaryExpressionOperatorToken(SyntaxKind token, LanguageVersion version) => GetBinaryExpression(token, version) != SyntaxKind.None;

    internal static bool IsLeftAssociativeBinaryExpressionOperatorToken(SyntaxKind token, LanguageVersion version) => IsBinaryExpressionOperatorToken(token, version) && !IsRightAssociativeBinaryExpressionOperatorToken(token, version);

    internal static partial bool IsRightAssociativeBinaryExpressionOperatorToken(SyntaxKind token, LanguageVersion version);

    public static partial SyntaxKind GetBinaryExpression(SyntaxKind token, LanguageVersion version);

    public static partial SyntaxKind GetBinaryExpressionOperatorToken(SyntaxKind expression, LanguageVersion version);

    internal static partial int GetOperatorPrecedence(SyntaxKind token, bool isUnary);

    /// <summary>
    /// Checks and adjusts to effective language version enum value.
    /// </summary>
    /// <param name="version">Enum value to check and adjust.</param>
    private static void CheckAndAdjustLanguageVersion(ref LanguageVersion version)
    {
        if (!version.IsValid())
        {
            version = version.MapSpecifiedToEffectiveVersion();
            Debug.Assert(version.IsValid());
        }
    }
}
