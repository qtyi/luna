// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.Lua;

public static partial class SyntaxFacts
{
    public static partial string GetText(SyntaxKind kind) => kind switch
    {
        // Punctuations
        SyntaxKind.PlusToken => "+",
        SyntaxKind.MinusToken => "-",
        SyntaxKind.AsteriskToken => "*",
        SyntaxKind.SlashToken => "/",
        SyntaxKind.CaretToken => "^",
        SyntaxKind.PercentToken => "%",
        SyntaxKind.CommercialAtToken => "@",
        SyntaxKind.HashToken => "#",
        SyntaxKind.AmpersandToken => "&",
        SyntaxKind.TildeToken => "~",
        SyntaxKind.BarToken => "|",
        SyntaxKind.LessThanToken => "<",
        SyntaxKind.GreaterThanToken => ">",
        SyntaxKind.EqualsToken => "=",
        SyntaxKind.OpenParenToken => "(",
        SyntaxKind.CloseParenToken => ")",
        SyntaxKind.OpenBraceToken => "{",
        SyntaxKind.CloseBraceToken => "}",
        SyntaxKind.OpenBracketToken => "[",
        SyntaxKind.CloseBracketToken => "]",
        SyntaxKind.ColonToken => ":",
        SyntaxKind.SemicolonToken => ";",
        SyntaxKind.CommaToken => ",",
        SyntaxKind.DotToken => ".",
        SyntaxKind.LessThanLessThanToken => "<<",
        SyntaxKind.LessThanEqualsToken => "<=",
        SyntaxKind.GreaterThanGreaterThanToken => ">>",
        SyntaxKind.GreaterThanEqualsToken => ">=",
        SyntaxKind.SlashSlashToken => "//",
        SyntaxKind.EqualsEqualsToken => "==",
        SyntaxKind.TildeEqualsToken => "~=",
        SyntaxKind.ColonColonToken => "::",
        SyntaxKind.DotDotToken => "..",
        SyntaxKind.DotDotDotToken => "...",
        SyntaxKind.HashExclamationToken => "#!",

        // Keywords
        SyntaxKind.AndKeyword => "and",
        SyntaxKind.BreakKeyword => "break",
        SyntaxKind.DoKeyword => "do",
        SyntaxKind.ElseKeyword => "else",
        SyntaxKind.ElseIfKeyword => "elseif",
        SyntaxKind.EndKeyword => "end",
        SyntaxKind.FalseKeyword => "false",
        SyntaxKind.ForKeyword => "for",
        SyntaxKind.FunctionKeyword => "function",
        SyntaxKind.GotoKeyword => "goto",
        SyntaxKind.IfKeyword => "if",
        SyntaxKind.InKeyword => "in",
        SyntaxKind.LocalKeyword => "local",
        SyntaxKind.NilKeyword => "nil",
        SyntaxKind.NotKeyword => "not",
        SyntaxKind.OrKeyword => "or",
        SyntaxKind.RepeatKeyword => "repeat",
        SyntaxKind.ReturnKeyword => "return",
        SyntaxKind.ThenKeyword => "then",
        SyntaxKind.TrueKeyword => "true",
        SyntaxKind.UntilKeyword => "until",
        SyntaxKind.WhileKeyword => "while",
        SyntaxKind.EnvironmentKeyword => "_ENV",
        SyntaxKind.CloseKeyword => "close",
        SyntaxKind.ConstKeyword => "const",
        SyntaxKind.AbstractKeyword => "abstract",
        SyntaxKind.AnnotatedWithKeyword => "annotatedwith",
        SyntaxKind.AssemblyKeyword => "assembly",
        SyntaxKind.ClassKeyword => "class",
        SyntaxKind.ConstrainAsKeyword => "constrainas",
        SyntaxKind.EventKeyword => "event",
        SyntaxKind.ExtendsKeyword => "extends",
        SyntaxKind.FieldKeyword => "field",
        SyntaxKind.FinalKeyword => "final",
        SyntaxKind.ImplementsKeyword => "implements",
        SyntaxKind.InterfaceKeyword => "interface",
        SyntaxKind.MethodKeyword => "method",
        SyntaxKind.ModuleKeyword => "module",
        SyntaxKind.NamespaceKeyword => "namespace",
        SyntaxKind.NewKeyword => "new",
        SyntaxKind.OutKeyword => "out",
        SyntaxKind.ParameterKeyword => "parameter",
        SyntaxKind.PrivateKeyword => "private",
        SyntaxKind.PropertyKeyword => "property",
        SyntaxKind.ProtectedKeyword => "protected",
        SyntaxKind.PublicKeyword => "public",
        SyntaxKind.ReadonlyKeyword => "readonly",
        SyntaxKind.RefKeyword => "ref",
        SyntaxKind.TypeParameterKeyword => "typeparameter",
        SyntaxKind.StaticKeyword => "static",

        _ => string.Empty
    };

    #region Keyword
    #region Reserved Keyword
    private static partial IEnumerable<SyntaxKind> GetReservedKeywordKindsInternal(LanguageVersion version)
    {
        yield return SyntaxKind.AndKeyword;
        if (version >= LanguageVersion.Lua4) yield return SyntaxKind.BreakKeyword;
        yield return SyntaxKind.DoKeyword;
        yield return SyntaxKind.ElseKeyword;
        yield return SyntaxKind.ElseIfKeyword;
        yield return SyntaxKind.EndKeyword;
        if (version >= LanguageVersion.Lua5) yield return SyntaxKind.FalseKeyword;
        if (version >= LanguageVersion.Lua4) yield return SyntaxKind.ForKeyword;
        yield return SyntaxKind.FunctionKeyword;
        if (version >= LanguageVersion.Lua5_2) yield return SyntaxKind.GotoKeyword;
        yield return SyntaxKind.IfKeyword;
        if (version >= LanguageVersion.Lua4) yield return SyntaxKind.InKeyword;
        yield return SyntaxKind.LocalKeyword;
        yield return SyntaxKind.NilKeyword;
        yield return SyntaxKind.NotKeyword;
        yield return SyntaxKind.OrKeyword;
        yield return SyntaxKind.RepeatKeyword;
        yield return SyntaxKind.ReturnKeyword;
        yield return SyntaxKind.ThenKeyword;
        if (version >= LanguageVersion.Lua5) yield return SyntaxKind.TrueKeyword;
        yield return SyntaxKind.UntilKeyword;
        yield return SyntaxKind.WhileKeyword;
    }

    private static partial bool IsReservedKeywordInternal(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.AndKeyword => true,
        SyntaxKind.BreakKeyword => version >= LanguageVersion.Lua4,
        SyntaxKind.DoKeyword => true,
        SyntaxKind.ElseKeyword => true,
        SyntaxKind.ElseIfKeyword => true,
        SyntaxKind.EndKeyword => true,
        SyntaxKind.FalseKeyword => version >= LanguageVersion.Lua5,
        SyntaxKind.ForKeyword => version >= LanguageVersion.Lua4,
        SyntaxKind.FunctionKeyword => true,
        SyntaxKind.GotoKeyword => version >= LanguageVersion.Lua5_2,
        SyntaxKind.IfKeyword => true,
        SyntaxKind.InKeyword => version >= LanguageVersion.Lua4,
        SyntaxKind.LocalKeyword => true,
        SyntaxKind.NilKeyword => true,
        SyntaxKind.NotKeyword => true,
        SyntaxKind.OrKeyword => true,
        SyntaxKind.RepeatKeyword => true,
        SyntaxKind.ReturnKeyword => true,
        SyntaxKind.ThenKeyword => true,
        SyntaxKind.TrueKeyword => version >= LanguageVersion.Lua5,
        SyntaxKind.UntilKeyword => true,
        SyntaxKind.WhileKeyword => true,

        _ => false
    };

    public static partial SyntaxKind GetReservedKeywordKind(string text) => text switch
    {
        "and" => SyntaxKind.AndKeyword,
        "break" => SyntaxKind.BreakKeyword,
        "do" => SyntaxKind.DoKeyword,
        "else" => SyntaxKind.ElseKeyword,
        "elseif" => SyntaxKind.ElseIfKeyword,
        "end" => SyntaxKind.EndKeyword,
        "false" => SyntaxKind.FalseKeyword,
        "for" => SyntaxKind.ForKeyword,
        "function" => SyntaxKind.FunctionKeyword,
        "goto" => SyntaxKind.GotoKeyword,
        "if" => SyntaxKind.IfKeyword,
        "in" => SyntaxKind.InKeyword,
        "local" => SyntaxKind.LocalKeyword,
        "nil" => SyntaxKind.NilKeyword,
        "not" => SyntaxKind.NotKeyword,
        "or" => SyntaxKind.OrKeyword,
        "repeat" => SyntaxKind.RepeatKeyword,
        "return" => SyntaxKind.ReturnKeyword,
        "then" => SyntaxKind.ThenKeyword,
        "true" => SyntaxKind.TrueKeyword,
        "until" => SyntaxKind.UntilKeyword,
        "while" => SyntaxKind.WhileKeyword,

        _ => SyntaxKind.None
    };
    #endregion

    #region Contextual Keyword
    private static partial IEnumerable<SyntaxKind> GetContextualKeywordKindsInternal(LanguageVersion version)
    {
        var kinds = Enumerable.Empty<SyntaxKind>();

        // Environment keywords
        kinds = kinds.Concat(GetEnvironmentKeywordKindsInternal(version));

        // Variable attributes
        kinds = kinds.Concat(GetVariableAttributeKeywords(version));

        // Dotnet keywords
        kinds = kinds.Concat(GetDotnetKeywords(version));

        return kinds;
    }

    private static partial bool IsContextualKeywordInternal(SyntaxKind kind, LanguageVersion version) =>
        // Environment keywords
        IsEnvironmentKeywordsInternal(kind, version) ||

        // Variable attribute keywords.
        IsVariableAttributeKeywordInternal(kind, version) ||

        // Dotnet keywords
        IsDotnetKeywordInternal(kind, version);

    public static partial SyntaxKind GetContextualKeywordKind(string text) => text switch
    {
        // Environment keywords
        "_ENV" => SyntaxKind.EnvironmentKeyword,

        // Variable attribute keywords
        "close" => SyntaxKind.CloseKeyword,
        "const" => SyntaxKind.ConstKeyword,

        // Dotnet keywords
        "abstract" => SyntaxKind.AbstractKeyword,
        "annotatedwith" => SyntaxKind.AnnotatedWithKeyword,
        "assembly" => SyntaxKind.AssemblyKeyword,
        "class" => SyntaxKind.ClassKeyword,
        "constrainas" => SyntaxKind.ConstrainAsKeyword,
        "event" => SyntaxKind.EventKeyword,
        "extends" => SyntaxKind.ExtendsKeyword,
        "field" => SyntaxKind.FieldKeyword,
        "final" => SyntaxKind.FinalKeyword,
        "implements" => SyntaxKind.ImplementsKeyword,
        "interface" => SyntaxKind.InterfaceKeyword,
        "method" => SyntaxKind.MethodKeyword,
        "module" => SyntaxKind.ModuleKeyword,
        "namespace" => SyntaxKind.NamespaceKeyword,
        "new" => SyntaxKind.NewKeyword,
        "out" => SyntaxKind.OutKeyword,
        "parameter" => SyntaxKind.ParameterKeyword,
        "private" => SyntaxKind.PrivateKeyword,
        "property" => SyntaxKind.PropertyKeyword,
        "protected" => SyntaxKind.ProtectedKeyword,
        "public" => SyntaxKind.PublicKeyword,
        "readonly" => SyntaxKind.ReadonlyKeyword,
        "ref" => SyntaxKind.RefKeyword,
        "typeparameter" => SyntaxKind.TypeParameterKeyword,
        "static" => SyntaxKind.StaticKeyword,

        _ => SyntaxKind.None
    };

    #region Environment Keywords
    /// <summary>
    /// Gets all syntax kinds that is environment keyword kinds.
    /// </summary>
    /// <param name="version">Version of language.</param>
    /// <returns>All environment keyword kinds.</returns>
    public static IEnumerable<SyntaxKind> GetEnvironmentKeywordKinds(LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return GetEnvironmentKeywordKindsInternal(version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="GetEnvironmentKeywordKinds(LanguageVersion)"/>
    private static IEnumerable<SyntaxKind> GetEnvironmentKeywordKindsInternal(LanguageVersion version)
    {
        if (version >= LanguageVersion.Lua5_2) yield return SyntaxKind.EnvironmentKeyword;
    }

    /// <summary>
    /// Checks if a syntax kind in specified language version is a environment keyword kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a environment keyword kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsEnvironmentKeywords(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsEnvironmentKeywordsInternal(kind, version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="IsEnvironmentKeywords(SyntaxKind, LanguageVersion)"/>
    private static bool IsEnvironmentKeywordsInternal(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.EnvironmentKeyword => version >= LanguageVersion.Lua5_2,

        _ => false
    };
    #endregion

    #region Variable Attribute Keywords
    /// <summary>
    /// Gets all syntax kinds that is variable attribute keyword kinds.
    /// </summary>
    /// <param name="version">Version of language.</param>
    /// <returns>All variable attribute keyword kinds.</returns>
    public static IEnumerable<SyntaxKind> GetVariableAttributeKeywords(LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return GetVariableAttributeKeywordsInternal(version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="GetVariableAttributeKeywords(LanguageVersion)"/>
    private static IEnumerable<SyntaxKind> GetVariableAttributeKeywordsInternal(LanguageVersion version)
    {
        if (version >= LanguageVersion.Lua5_4) yield return SyntaxKind.CloseKeyword;
        if (version >= LanguageVersion.Lua5_4) yield return SyntaxKind.ConstKeyword;
    }

    /// <summary>
    /// Checks if a syntax kind in specified language version is a variable attribute keyword kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a variable attribute keyword kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsVariableAttributeKeyword(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsVariableAttributeKeywordInternal(kind, version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="IsVariableAttributeKeyword(SyntaxKind, LanguageVersion)"/>
    private static bool IsVariableAttributeKeywordInternal(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.CloseKeyword => version >= LanguageVersion.Lua5_4,
        SyntaxKind.ConstKeyword => version >= LanguageVersion.Lua5_4,

        _ => false
    };
    #endregion

    #region Dotnet Keywords
    /// <summary>
    /// Gets all syntax kinds that is dotnet keyword kinds.
    /// </summary>
    /// <param name="version">Version of language.</param>
    /// <returns>All dotnet keyword kinds.</returns>
    public static IEnumerable<SyntaxKind> GetDotnetKeywords(LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return GetDotnetKeywordsInternal(version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="GetDotnetKeywords(LanguageVersion)"/>
    private static IEnumerable<SyntaxKind> GetDotnetKeywordsInternal(LanguageVersion version)
    {
        // Only available in DotNet version.
        if (version != LanguageVersion.DotNet)
            yield break;

        yield return SyntaxKind.AbstractKeyword;
        yield return SyntaxKind.AnnotatedWithKeyword;
        yield return SyntaxKind.AssemblyKeyword;
        yield return SyntaxKind.ClassKeyword;
        yield return SyntaxKind.ConstrainAsKeyword;
        yield return SyntaxKind.EventKeyword;
        yield return SyntaxKind.ExtendsKeyword;
        yield return SyntaxKind.FieldKeyword;
        yield return SyntaxKind.FinalKeyword;
        yield return SyntaxKind.ImplementsKeyword;
        yield return SyntaxKind.InterfaceKeyword;
        yield return SyntaxKind.ModuleKeyword;
        yield return SyntaxKind.MethodKeyword;
        yield return SyntaxKind.NamespaceKeyword;
        yield return SyntaxKind.NewKeyword;
        yield return SyntaxKind.OutKeyword;
        yield return SyntaxKind.ParameterKeyword;
        yield return SyntaxKind.PrivateKeyword;
        yield return SyntaxKind.PropertyKeyword;
        yield return SyntaxKind.ProtectedKeyword;
        yield return SyntaxKind.PublicKeyword;
        yield return SyntaxKind.ReadonlyKeyword;
        yield return SyntaxKind.RefKeyword;
        yield return SyntaxKind.TypeParameterKeyword;
        yield return SyntaxKind.StaticKeyword;
    }

    /// <summary>
    /// Checks if a syntax kind in specified language version is a dotnet keyword kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a dotnet keyword kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsDotnetKeyword(SyntaxKind kind, LanguageVersion version)
    {
        CheckAndAdjustLanguageVersion(ref version);

        return IsDotnetKeywordInternal(kind, version);
    }

    /// <remarks>
    /// This is an implemental method without validating <paramref name="version"/>.
    /// </remarks>
    /// <inheritdoc cref="IsDotnetKeyword(SyntaxKind, LanguageVersion)"/>
    public static bool IsDotnetKeywordInternal(SyntaxKind kind, LanguageVersion version)
    {
        // Only avaliable in DotNet version.
        if (version != LanguageVersion.DotNet)
            return false;

        return kind switch
        {
            SyntaxKind.AbstractKeyword or
            SyntaxKind.AnnotatedWithKeyword or
            SyntaxKind.AssemblyKeyword or
            SyntaxKind.ClassKeyword or
            SyntaxKind.ConstrainAsKeyword or
            SyntaxKind.EventKeyword or
            SyntaxKind.ExtendsKeyword or
            SyntaxKind.FieldKeyword or
            SyntaxKind.FinalKeyword or
            SyntaxKind.ImplementsKeyword or
            SyntaxKind.InterfaceKeyword or
            SyntaxKind.MethodKeyword or
            SyntaxKind.ModuleKeyword or
            SyntaxKind.NamespaceKeyword or
            SyntaxKind.NewKeyword or
            SyntaxKind.OutKeyword or
            SyntaxKind.ParameterKeyword or
            SyntaxKind.PrivateKeyword or
            SyntaxKind.PropertyKeyword or
            SyntaxKind.ProtectedKeyword or
            SyntaxKind.PublicKeyword or
            SyntaxKind.ReadonlyKeyword or
            SyntaxKind.RefKeyword or
            SyntaxKind.TypeParameterKeyword or
            SyntaxKind.StaticKeyword => true,

            _ => false
        };
    }
    #endregion
    #endregion
    #endregion

    #region Punctuation
    private static partial IEnumerable<SyntaxKind> GetPunctuationKindsInternal(LanguageVersion version)
    {
        yield return SyntaxKind.PlusToken;
        yield return SyntaxKind.MinusToken;
        yield return SyntaxKind.AsteriskToken;
        yield return SyntaxKind.SlashToken;
        if (version >= LanguageVersion.Lua5) yield return SyntaxKind.CaretToken;
        if (version != LanguageVersion.Lua5) yield return SyntaxKind.PercentToken;
        if (version == LanguageVersion.Lua1_1) yield return SyntaxKind.CommercialAtToken;
        if (version >= LanguageVersion.Lua5_1) yield return SyntaxKind.HashToken;
        if (version >= LanguageVersion.Lua5_3) yield return SyntaxKind.AmpersandToken;
        if (version >= LanguageVersion.Lua5_3) yield return SyntaxKind.TildeToken;
        if (version >= LanguageVersion.Lua5_3) yield return SyntaxKind.BarToken;
        yield return SyntaxKind.LessThanToken;
        yield return SyntaxKind.GreaterThanToken;
        yield return SyntaxKind.EqualsToken;
        yield return SyntaxKind.OpenParenToken;
        yield return SyntaxKind.CloseParenToken;
        yield return SyntaxKind.OpenBraceToken;
        yield return SyntaxKind.CloseBraceToken;
        yield return SyntaxKind.OpenBracketToken;
        yield return SyntaxKind.CloseBracketToken;
        if (version >= LanguageVersion.Lua5) yield return SyntaxKind.ColonToken;
        yield return SyntaxKind.SemicolonToken;
        yield return SyntaxKind.CommaToken;
        yield return SyntaxKind.DotToken;
        if (version >= LanguageVersion.Lua5_3) yield return SyntaxKind.LessThanLessThanToken;
        yield return SyntaxKind.LessThanEqualsToken;
        if (version >= LanguageVersion.Lua5_3) yield return SyntaxKind.GreaterThanGreaterThanToken;
        yield return SyntaxKind.GreaterThanEqualsToken;
        if (version >= LanguageVersion.Lua5_3) yield return SyntaxKind.SlashSlashToken;
        if (version >= LanguageVersion.Lua2_1) yield return SyntaxKind.EqualsEqualsToken;
        yield return SyntaxKind.TildeEqualsToken;
        if (version >= LanguageVersion.Lua5_2) yield return SyntaxKind.ColonColonToken;
        yield return SyntaxKind.DotDotToken;
        if (version >= LanguageVersion.Lua3_1) yield return SyntaxKind.DotDotDotToken;
        if (version >= LanguageVersion.Lua2_5) yield return SyntaxKind.HashExclamationToken;
    }

    private static partial bool IsPunctuationInternal(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.PlusToken => true,
        SyntaxKind.MinusToken => true,
        SyntaxKind.AsteriskToken => true,
        SyntaxKind.SlashToken => true,
        SyntaxKind.CaretToken => version >= LanguageVersion.Lua5,
        SyntaxKind.PercentToken => version != LanguageVersion.Lua5,
        SyntaxKind.CommercialAtToken => version == LanguageVersion.Lua1_1,
        SyntaxKind.HashToken => version >= LanguageVersion.Lua5_1,
        SyntaxKind.AmpersandToken => version >= LanguageVersion.Lua5_3,
        SyntaxKind.TildeToken => version >= LanguageVersion.Lua5_3,
        SyntaxKind.BarToken => version >= LanguageVersion.Lua5_3,
        SyntaxKind.LessThanToken => true,
        SyntaxKind.GreaterThanToken => true,
        SyntaxKind.EqualsToken => true,
        SyntaxKind.OpenParenToken => true,
        SyntaxKind.CloseParenToken => true,
        SyntaxKind.OpenBraceToken => true,
        SyntaxKind.CloseBraceToken => true,
        SyntaxKind.OpenBracketToken => true,
        SyntaxKind.CloseBracketToken => true,
        SyntaxKind.ColonToken => version >= LanguageVersion.Lua5,
        SyntaxKind.SemicolonToken => true,
        SyntaxKind.CommaToken => true,
        SyntaxKind.DotToken => true,
        SyntaxKind.LessThanLessThanToken => version >= LanguageVersion.Lua5_3,
        SyntaxKind.LessThanEqualsToken => true,
        SyntaxKind.GreaterThanGreaterThanToken => version >= LanguageVersion.Lua5_3,
        SyntaxKind.GreaterThanEqualsToken => true,
        SyntaxKind.SlashSlashToken => version >= LanguageVersion.Lua5_3,
        SyntaxKind.EqualsEqualsToken => version >= LanguageVersion.Lua2_1,
        SyntaxKind.TildeEqualsToken => true,
        SyntaxKind.ColonColonToken => version >= LanguageVersion.Lua5_2,
        SyntaxKind.DotDotToken => true,
        SyntaxKind.DotDotDotToken => version >= LanguageVersion.Lua3_1,
        SyntaxKind.HashExclamationToken => version >= LanguageVersion.Lua2_5,

        _ => false
    };

    public static partial SyntaxKind GetPunctuationKind(string text) => text switch
    {
        "+" => SyntaxKind.PlusToken,
        "-" => SyntaxKind.MinusToken,
        "*" => SyntaxKind.AsteriskToken,
        "/" => SyntaxKind.SlashToken,
        "^" => SyntaxKind.CaretToken,
        "%" => SyntaxKind.PercentToken,
        "@" => SyntaxKind.CommercialAtToken,
        "#" => SyntaxKind.HashToken,
        "&" => SyntaxKind.AmpersandToken,
        "~" => SyntaxKind.TildeToken,
        "|" => SyntaxKind.BarToken,
        "<" => SyntaxKind.LessThanToken,
        ">" => SyntaxKind.GreaterThanToken,
        "=" => SyntaxKind.EqualsToken,
        "(" => SyntaxKind.OpenParenToken,
        ")" => SyntaxKind.CloseParenToken,
        "{" => SyntaxKind.OpenBraceToken,
        "}" => SyntaxKind.CloseBraceToken,
        "[" => SyntaxKind.OpenBracketToken,
        "]" => SyntaxKind.CloseBracketToken,
        ":" => SyntaxKind.ColonToken,
        ";" => SyntaxKind.SemicolonToken,
        "," => SyntaxKind.CommaToken,
        "." => SyntaxKind.DotToken,
        "<<" => SyntaxKind.LessThanLessThanToken,
        "<=" => SyntaxKind.LessThanEqualsToken,
        ">>" => SyntaxKind.GreaterThanGreaterThanToken,
        ">=" => SyntaxKind.GreaterThanEqualsToken,
        "//" => SyntaxKind.SlashSlashToken,
        "==" => SyntaxKind.EqualsEqualsToken,
        "~=" => SyntaxKind.TildeEqualsToken,
        "::" => SyntaxKind.ColonColonToken,
        ".." => SyntaxKind.DotDotToken,
        "..." => SyntaxKind.DotDotDotToken,
        "#!" => SyntaxKind.HashExclamationToken,

        _ => SyntaxKind.None
    };
    #endregion

    private static partial bool IsAnyTokenInternal(SyntaxKind kind, LanguageVersion version) =>
        // Punctuations
        IsPunctuationInternal(kind, version) ||

        // Keywords
        IsKeywordKindInternal(kind, version) ||

        kind switch
        {
            // Tokens without text
            >= SyntaxKind.EndOfDirectiveToken and <= SyntaxKind.EndOfFileToken => true,

            // Tokens with text
            >= SyntaxKind.BadToken and <= SyntaxKind.StringLiteralToken => true,
            SyntaxKind.MultiLineRawStringLiteralToken => version >= LanguageVersion.Lua2_2,

            _ => false
        };

    public static partial bool IsWhitespaceTrivia(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.EndOfLineTrivia or
        SyntaxKind.WhitespaceTrivia => true,

        _ => false
    };

    public static partial bool IsCommentTrivia(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.SingleLineCommentTrivia => true,
        SyntaxKind.MultiLineCommentTrivia => version >= LanguageVersion.Lua5,

        _ => false
    };

    public static partial bool IsPreprocessorDirective(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.PreprocessingMessageTrivia or
        SyntaxKind.BadDirectiveTrivia or
        SyntaxKind.ShebangDirectiveTrivia => version >= LanguageVersion.Lua2_5,
        SyntaxKind.DebugDirectiveTrivia or
        SyntaxKind.IfDirectiveTrivia or
        SyntaxKind.IfNotDirectiveTrivia or
        SyntaxKind.ElseDirectiveTrivia or
        SyntaxKind.EndDirectiveTrivia or
        SyntaxKind.EndInputDirectiveTrivia => version is LanguageVersion.Lua3_1 or LanguageVersion.Lua3_2 or LanguageVersion.DotNet,

        _ => false
    };

    public static partial bool IsStructuredTrivia(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.SkippedTokensTrivia => true,

        _ => false
    };

    public static partial bool IsName(SyntaxKind kind) => kind switch
    {
        SyntaxKind.IdentifierName => true,

        _ => false
    };

    public static partial bool IsLiteralExpression(SyntaxKind expression) => expression switch
    {
        SyntaxKind.FalseLiteralExpression or
        SyntaxKind.NilLiteralExpression or
        SyntaxKind.TrueLiteralExpression or
        SyntaxKind.NumericLiteralExpression or
        SyntaxKind.StringLiteralExpression or
        SyntaxKind.VariousArgumentsExpression => true,

        _ => false
    };

    public static partial SyntaxKind GetLiteralExpression(SyntaxKind token, LanguageVersion version) => token switch
    {
        SyntaxKind.FalseKeyword => version >= LanguageVersion.Lua5 ? SyntaxKind.FalseLiteralExpression : SyntaxKind.None,
        SyntaxKind.NilKeyword => SyntaxKind.NilLiteralExpression,
        SyntaxKind.TrueKeyword => version >= LanguageVersion.Lua5 ? SyntaxKind.TrueLiteralExpression : SyntaxKind.None,
        SyntaxKind.NumericLiteralToken => SyntaxKind.NumericLiteralExpression,
        SyntaxKind.StringLiteralToken => SyntaxKind.StringLiteralExpression,
        SyntaxKind.MultiLineRawStringLiteralToken => version >= LanguageVersion.Lua2_2 ? SyntaxKind.StringLiteralExpression : SyntaxKind.None,
        SyntaxKind.DotDotDotToken => version >= LanguageVersion.Lua3_1 ? SyntaxKind.VariousArgumentsExpression : SyntaxKind.None,

        _ => SyntaxKind.None
    };

    public static partial bool IsUnaryExpression(SyntaxKind expression) => expression switch
    {
        SyntaxKind.UnaryMinusExpression => true,
        SyntaxKind.LogicalNotExpression => true,
        SyntaxKind.LengthExpression => true,
        SyntaxKind.BitwiseNotExpression => true,

        _ => false
    };

    public static partial SyntaxKind GetUnaryExpression(SyntaxKind token, LanguageVersion version) => token switch
    {
        SyntaxKind.MinusToken => SyntaxKind.UnaryMinusExpression,
        SyntaxKind.NotKeyword => SyntaxKind.LogicalNotExpression,
        SyntaxKind.HashToken => version >= LanguageVersion.Lua5_1 ? SyntaxKind.LengthExpression : SyntaxKind.None,
        SyntaxKind.TildeToken => version >= LanguageVersion.Lua5_3 ? SyntaxKind.BitwiseNotExpression : SyntaxKind.None,

        _ => SyntaxKind.None
    };

    public static partial SyntaxKind GetUnaryExpressionOperatorToken(SyntaxKind expression, LanguageVersion version) =>
        expression switch
        {
            SyntaxKind.UnaryMinusExpression => SyntaxKind.MinusToken,
            SyntaxKind.LogicalNotExpression => SyntaxKind.NotKeyword,
            SyntaxKind.LengthExpression => version >= LanguageVersion.Lua5_1 ? SyntaxKind.HashToken : SyntaxKind.None,
            SyntaxKind.BitwiseNotExpression => version >= LanguageVersion.Lua5_3 ? SyntaxKind.TildeToken : SyntaxKind.None,

            _ => SyntaxKind.None
        };

    public static partial bool IsBinaryExpression(SyntaxKind expression) => expression switch
    {
        SyntaxKind.AdditionExpression => true,
        SyntaxKind.SubtractionExpression => true,
        SyntaxKind.MultiplicationExpression => true,
        SyntaxKind.DivisionExpression => true,
        SyntaxKind.FloorDivisionExpression => true,
        SyntaxKind.ExponentiationExpression => true,
        SyntaxKind.ModuloExpression => true,
        SyntaxKind.BitwiseAndExpression => true,
        SyntaxKind.BitwiseExclusiveOrExpression => true,
        SyntaxKind.BitwiseOrExpression => true,
        SyntaxKind.BitwiseLeftShiftExpression => true,
        SyntaxKind.BitwiseRightShiftExpression => true,
        SyntaxKind.ConcatenationExpression => true,
        SyntaxKind.LessThanExpression => true,
        SyntaxKind.LessThanOrEqualExpression => true,
        SyntaxKind.GreaterThanExpression => true,
        SyntaxKind.GreaterThanOrEqualExpression => true,
        SyntaxKind.EqualExpression => true,
        SyntaxKind.NotEqualExpression => true,
        SyntaxKind.AndExpression => true,
        SyntaxKind.OrExpression => true,

        _ => false
    };

    internal static partial bool IsRightAssociativeBinaryExpressionOperatorToken(SyntaxKind token, LanguageVersion version) => token switch
    {
        SyntaxKind.CaretToken or
        SyntaxKind.DotDotToken => true,

        _ => false
    };

    public static partial SyntaxKind GetBinaryExpression(SyntaxKind token, LanguageVersion version) => token switch
    {
        SyntaxKind.PlusToken => SyntaxKind.AdditionExpression,
        SyntaxKind.MinusToken => SyntaxKind.SubtractionExpression,
        SyntaxKind.AsteriskToken => SyntaxKind.MultiplicationExpression,
        SyntaxKind.SlashToken => SyntaxKind.DivisionExpression,
        SyntaxKind.SlashSlashToken => version >= LanguageVersion.Lua5_3 ? SyntaxKind.FloorDivisionExpression : SyntaxKind.None,
        SyntaxKind.CaretToken => version >= LanguageVersion.Lua5 ? SyntaxKind.ExponentiationExpression : SyntaxKind.None,
        SyntaxKind.PercentToken => SyntaxKind.ModuloExpression,
        SyntaxKind.AmpersandToken => version >= LanguageVersion.Lua5_3 ? SyntaxKind.BitwiseAndExpression : SyntaxKind.None,
        SyntaxKind.TildeToken => version >= LanguageVersion.Lua5_3 ? SyntaxKind.BitwiseExclusiveOrExpression : SyntaxKind.None,
        SyntaxKind.BarToken => version >= LanguageVersion.Lua5_3 ? SyntaxKind.BitwiseOrExpression : SyntaxKind.None,
        SyntaxKind.LessThanLessThanToken => version >= LanguageVersion.Lua5_3 ? SyntaxKind.BitwiseLeftShiftExpression : SyntaxKind.None,
        SyntaxKind.GreaterThanGreaterThanToken => version >= LanguageVersion.Lua5_3 ? SyntaxKind.BitwiseRightShiftExpression : SyntaxKind.None,
        SyntaxKind.DotDotToken => SyntaxKind.ConcatenationExpression,
        SyntaxKind.LessThanToken => SyntaxKind.LessThanExpression,
        SyntaxKind.LessThanEqualsToken => SyntaxKind.LessThanOrEqualExpression,
        SyntaxKind.GreaterThanToken => SyntaxKind.GreaterThanExpression,
        SyntaxKind.GreaterThanEqualsToken => SyntaxKind.GreaterThanOrEqualExpression,
        SyntaxKind.EqualsToken => version == LanguageVersion.Lua1_1 ? SyntaxKind.EqualExpression : SyntaxKind.None,
        SyntaxKind.EqualsEqualsToken => version >= LanguageVersion.Lua2_1 ? SyntaxKind.EqualExpression : SyntaxKind.None,
        SyntaxKind.TildeEqualsToken => SyntaxKind.NotEqualExpression,
        SyntaxKind.AndKeyword => SyntaxKind.AndExpression,
        SyntaxKind.OrKeyword => SyntaxKind.OrExpression,

        _ => SyntaxKind.None
    };

    public static partial SyntaxKind GetBinaryExpressionOperatorToken(SyntaxKind expression, LanguageVersion version) => expression switch
    {
        SyntaxKind.AdditionExpression => SyntaxKind.PlusToken,
        SyntaxKind.SubtractionExpression => SyntaxKind.MinusToken,
        SyntaxKind.MultiplicationExpression => SyntaxKind.AsteriskToken,
        SyntaxKind.DivisionExpression => SyntaxKind.SlashToken,
        SyntaxKind.FloorDivisionExpression => version >= LanguageVersion.Lua5_3 ? SyntaxKind.SlashSlashToken : SyntaxKind.None,
        SyntaxKind.ExponentiationExpression => version >= LanguageVersion.Lua5 ? SyntaxKind.CaretToken : SyntaxKind.None,
        SyntaxKind.ModuloExpression => SyntaxKind.PercentToken,
        SyntaxKind.BitwiseAndExpression => version >= LanguageVersion.Lua5_3 ? SyntaxKind.AmpersandToken : SyntaxKind.None,
        SyntaxKind.BitwiseExclusiveOrExpression => version >= LanguageVersion.Lua5_3 ? SyntaxKind.TildeToken : SyntaxKind.None,
        SyntaxKind.BitwiseOrExpression => version >= LanguageVersion.Lua5_3 ? SyntaxKind.BarToken : SyntaxKind.None,
        SyntaxKind.BitwiseLeftShiftExpression => version >= LanguageVersion.Lua5_3 ? SyntaxKind.LessThanLessThanToken : SyntaxKind.None,
        SyntaxKind.BitwiseRightShiftExpression => version >= LanguageVersion.Lua5_3 ? SyntaxKind.GreaterThanGreaterThanToken : SyntaxKind.None,
        SyntaxKind.ConcatenationExpression => SyntaxKind.DotDotToken,
        SyntaxKind.LessThanExpression => SyntaxKind.LessThanToken,
        SyntaxKind.LessThanOrEqualExpression => SyntaxKind.LessThanEqualsToken,
        SyntaxKind.GreaterThanExpression => SyntaxKind.GreaterThanToken,
        SyntaxKind.GreaterThanOrEqualExpression => SyntaxKind.GreaterThanEqualsToken,
        SyntaxKind.EqualExpression => version == LanguageVersion.Lua1_1 ? SyntaxKind.EqualsToken : SyntaxKind.EqualsEqualsToken,
        SyntaxKind.NotEqualExpression => SyntaxKind.TildeEqualsToken,
        SyntaxKind.AndExpression => SyntaxKind.AndKeyword,
        SyntaxKind.OrExpression => SyntaxKind.OrKeyword,

        _ => SyntaxKind.None
    };

    internal static partial int GetOperatorPrecedence(SyntaxKind token, bool isUnary) =>
        token switch
        {
            SyntaxKind.CaretToken => 12,

            SyntaxKind.NotKeyword or
            SyntaxKind.HashToken => 11,
            SyntaxKind.MinusToken => isUnary ? 11 : 9,
            SyntaxKind.TildeToken => isUnary ? 11 : 5,

            SyntaxKind.AsteriskToken or
            SyntaxKind.SlashToken or
            SyntaxKind.SlashSlashToken or
            SyntaxKind.PercentToken => 10,

            SyntaxKind.PlusToken => 9,
            //SyntaxKind.MinusToken => 9,

            SyntaxKind.DotDotToken => 8,

            SyntaxKind.LessThanLessThanToken or
            SyntaxKind.GreaterThanGreaterThanToken => 7,

            SyntaxKind.AmpersandToken => 6,

            //SyntaxKind.Tilde == 5,

            SyntaxKind.BarToken => 4,

            SyntaxKind.LessThanToken or
            SyntaxKind.GreaterThanToken or
            SyntaxKind.LessThanEqualsToken or
            SyntaxKind.GreaterThanEqualsToken or
            SyntaxKind.TildeEqualsToken or
            SyntaxKind.EqualsToken or
            SyntaxKind.EqualsEqualsToken => 3,

            SyntaxKind.AndKeyword => 2,

            SyntaxKind.OrKeyword => 1,

            _ => 0
        };
}
