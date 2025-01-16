// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;

namespace Qtyi.CodeAnalysis.MoonScript;

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
        SyntaxKind.HashToken => "#",
        SyntaxKind.AmpersandToken => "&",
        SyntaxKind.TildeToken => "~",
        SyntaxKind.BarToken => "|",
        SyntaxKind.LessThanToken => "<",
        SyntaxKind.GreaterThanToken => ">",
        SyntaxKind.EqualsToken => "=",
        SyntaxKind.ExclamationToken => "!",
        SyntaxKind.OpenParenToken => "(",
        SyntaxKind.CloseParenToken => ")",
        SyntaxKind.OpenBraceToken => "{",
        SyntaxKind.CloseBraceToken => "}",
        SyntaxKind.OpenBracketToken => "[",
        SyntaxKind.CloseBracketToken => "]",
        SyntaxKind.ColonToken => ":",
        SyntaxKind.CommaToken => ",",
        SyntaxKind.DotToken => ".",
        SyntaxKind.CommercialAtToken => "@",
        SyntaxKind.PlusEqualsToken => "+=",
        SyntaxKind.MinusGreaterThanToken => "->",
        SyntaxKind.MinusEqualsToken => "-=",
        SyntaxKind.AsteriskEqualsToken => "*=",
        SyntaxKind.SlashEqualsToken => "/=",
        SyntaxKind.CaretEqualsToken => "^=",
        SyntaxKind.PersentEqualsToken => "%=",
        SyntaxKind.AmpersandEqualsToken => "&=",
        SyntaxKind.TildeEqualsToken => "~=",
        SyntaxKind.BarEqualsToken => "|=",
        SyntaxKind.LessThanLessThanToken => "<<",
        SyntaxKind.LessThanLessThanEqualsToken => "<<=",
        SyntaxKind.LessThanEqualsToken => "<=",
        SyntaxKind.GreaterThanGreaterThanToken => ">>",
        SyntaxKind.GreaterThanGreaterThanEqualsToken => ">>=",
        SyntaxKind.GreaterThanEqualsToken => ">=",
        SyntaxKind.SlashSlashToken => "//",
        SyntaxKind.EqualsGreaterThanToken => "=>",
        SyntaxKind.EqualsEqualsToken => "==",
        SyntaxKind.ExclamationEqualsToken => "!=",
        SyntaxKind.DotDotToken => "..",
        SyntaxKind.DotDotEqualsToken => "..=",
        SyntaxKind.DotDotDotToken => "...",
        SyntaxKind.CommercialAtCommercialAtToken => "@@",
        SyntaxKind.AndEqualsToken => "and=",
        SyntaxKind.OrEqualsToken => "or=",
        SyntaxKind.HashExclamationToken => "#!",

        // Keywords
        SyntaxKind.AndKeyword => "and",
        SyntaxKind.BreakKeyword => "break",
        SyntaxKind.ClassKeyword => "class",
        SyntaxKind.ContinueKeyword => "continue",
        SyntaxKind.DoKeyword => "do",
        SyntaxKind.ElseKeyword => "else",
        SyntaxKind.ElseIfKeyword => "elseif",
        SyntaxKind.EndKeyword => "end",
        SyntaxKind.ExportKeyword => "export",
        SyntaxKind.ExtendsKeyword => "extend",
        SyntaxKind.FalseKeyword => "false",
        SyntaxKind.ForKeyword => "for",
        SyntaxKind.FromKeyword => "from",
        SyntaxKind.IfKeyword => "if",
        SyntaxKind.ImportKeyword => "import",
        SyntaxKind.InKeyword => "in",
        SyntaxKind.LocalKeyword => "local",
        SyntaxKind.NilKeyword => "nil",
        SyntaxKind.NotKeyword => "not",
        SyntaxKind.OrKeyword => "or",
        SyntaxKind.ReturnKeyword => "return",
        SyntaxKind.SwitchKeyword => "switch",
        SyntaxKind.ThenKeyword => "then",
        SyntaxKind.TrueKeyword => "true",
        SyntaxKind.UnlessKeyword => "unless",
        SyntaxKind.UsingKeyword => "using",
        SyntaxKind.WhenKeyword => "when",
        SyntaxKind.WhileKeyword => "while",
        SyntaxKind.WithKeyword => "with",
        SyntaxKind.GlobalEnvironmentKeyword => "_G",
        SyntaxKind.EnvironmentKeyword => "_ENV",
        SyntaxKind.SelfKeyword => "self",
        SyntaxKind.SuperKeyword => "super",
        SyntaxKind.NewKeyword => "new",
        SyntaxKind.MetatableMetafield => "__metatable",
        SyntaxKind.ClassMetafield => "__class",
        SyntaxKind.NameMetafield => "__name",
        SyntaxKind.InheritedMetamethod => "__inherited",
        SyntaxKind.BaseMetafield => "__base",
        SyntaxKind.ParentMetafield => "__parent",
        SyntaxKind.AdditionMetamethod => "__add",
        SyntaxKind.SubtractionMetamethod => "__sub",
        SyntaxKind.MultiplicationMetamethod => "__mul",
        SyntaxKind.DivisionMetamethod => "__div",
        SyntaxKind.ModuloMetamethod => "__mod",
        SyntaxKind.ExponentiationMetamethod => "__pow",
        SyntaxKind.NegationMetamethod => "__unm",
        SyntaxKind.FloorDivisionMetamethod => "__idiv",
        SyntaxKind.BitwiseAndMetamethod => "__band",
        SyntaxKind.BitwiseOrMetamethod => "__bor",
        SyntaxKind.BitwiseExclusiveOrMetamethod => "__bxor",
        SyntaxKind.BitwiseNotMetamethod => "__bnot",
        SyntaxKind.BitwiseLeftShiftMetamethod => "__shl",
        SyntaxKind.BitwiseRightShiftMetamethod => "__shr",
        SyntaxKind.ConcatenationMetamethod => "__concat",
        SyntaxKind.LengthMetamethod => "__len",
        SyntaxKind.EqualMetamethod => "__eq",
        SyntaxKind.LessThanMetamethod => "__lt",
        SyntaxKind.LessEqualMetamethod => "__le",
        SyntaxKind.IndexingAccessMetamethod => "__index",
        SyntaxKind.CallMetamethod => "__call",
        SyntaxKind.PairsMetamethod => "__pairs",
        SyntaxKind.ToStringMetamethod => "__tostring",
        SyntaxKind.GarbageCollectionMetamethod => "__gc",
        SyntaxKind.ToBeClosedMetamethod => "__close",
        SyntaxKind.WeakModeMetafield => "__mode",

        _ => string.Empty
    };

    #region Keyword
    #region Reserved Keyword
    private static partial IEnumerable<SyntaxKind> GetReservedKeywordKindsInternal(LanguageVersion version)
    {
        for (var i = (int)SyntaxKind.AndKeyword; i <= (int)SyntaxKind.WithKeyword; i++)
            yield return (SyntaxKind)i;
    }

    private static partial bool IsReservedKeywordInternal(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        >= SyntaxKind.AndKeyword and <= SyntaxKind.WithKeyword => true,

        _ => false
    };

    public static partial SyntaxKind GetReservedKeywordKind(string text) => text switch
    {
        "and" => SyntaxKind.AndKeyword,
        "break" => SyntaxKind.BreakKeyword,
        "class" => SyntaxKind.ClassKeyword,
        "continue" => SyntaxKind.ContinueKeyword,
        "do" => SyntaxKind.DoKeyword,
        "else" => SyntaxKind.ElseKeyword,
        "elseif" => SyntaxKind.ElseIfKeyword,
        "end" => SyntaxKind.EndKeyword,
        "export" => SyntaxKind.ExportKeyword,
        "extends" => SyntaxKind.ExtendsKeyword,
        "false" => SyntaxKind.FalseKeyword,
        "for" => SyntaxKind.ForKeyword,
        "from" => SyntaxKind.FromKeyword,
        "if" => SyntaxKind.IfKeyword,
        "import" => SyntaxKind.ImportKeyword,
        "in" => SyntaxKind.InKeyword,
        "local" => SyntaxKind.LocalKeyword,
        "nil" => SyntaxKind.NilKeyword,
        "not" => SyntaxKind.NotKeyword,
        "or" => SyntaxKind.OrKeyword,
        "return" => SyntaxKind.ReturnKeyword,
        "switch" => SyntaxKind.SwitchKeyword,
        "then" => SyntaxKind.ThenKeyword,
        "true" => SyntaxKind.TrueKeyword,
        "unless" => SyntaxKind.UnlessKeyword,
        "using" => SyntaxKind.UsingKeyword,
        "when" => SyntaxKind.WhenKeyword,
        "while" => SyntaxKind.WhileKeyword,
        "with" => SyntaxKind.WithKeyword,

        _ => SyntaxKind.None
    };
    #endregion

    #region Contextual Keyword
    private static partial IEnumerable<SyntaxKind> GetContextualKeywordKindsInternal(LanguageVersion version)
    {
        var kinds = Enumerable.Empty<SyntaxKind>();

        // Environment keywords
        kinds = kinds.Concat(GetEnvironmentKeywordKinds(version));

        return kinds;
    }

    private static partial bool IsContextualKeywordInternal(SyntaxKind kind, LanguageVersion version) =>
        // Environment keywords
        IsEnvironmentKeywords(kind, version);

    /// <summary>
    /// Gets all syntax kinds that is environment keyword kinds.
    /// </summary>
    /// <param name="version">Version of language.</param>
    /// <returns>All environment keyword kinds.</returns>
#pragma warning disable IDE0060
    public static IEnumerable<SyntaxKind> GetEnvironmentKeywordKinds(LanguageVersion version)
    {
        yield return SyntaxKind.EnvironmentKeyword;
    }
#pragma warning restore IDE0060

    /// <summary>
    /// Checks if a syntax kind in specified language version is a environment keyword kind.
    /// </summary>
    /// <param name="kind">Syntax kind to be checked.</param>
    /// <param name="version">Version of language.</param>
    /// <returns>Returns <see langword="true"/> if <paramref name="kind"/> is a environment keyword kind in language of <paramref name="version"/>; otherwise, <see langword="false"/>.</returns>
#pragma warning disable IDE0060
    public static bool IsEnvironmentKeywords(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.EnvironmentKeyword => true,

        _ => false
    };
#pragma warning restore IDE0060

    public static partial SyntaxKind GetContextualKeywordKind(string text) => text switch
    {
        // Environment keywords
        "_ENV" => SyntaxKind.EnvironmentKeyword,

        _ => SyntaxKind.None
    };
    #endregion
    #endregion

    #region Punctuation
    private static partial IEnumerable<SyntaxKind> GetPunctuationKindsInternal(LanguageVersion version)
    {
        for (var i = (int)SyntaxKind.PlusToken; i <= (int)SyntaxKind.HashExclamationToken; i++)
            yield return (SyntaxKind)i;
    }

    private static partial bool IsPunctuationInternal(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        >= SyntaxKind.PlusToken and <= SyntaxKind.HashExclamationToken => true,

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
        "#" => SyntaxKind.HashToken,
        "&" => SyntaxKind.AmpersandToken,
        "~" => SyntaxKind.TildeToken,
        "|" => SyntaxKind.BarToken,
        "<" => SyntaxKind.LessThanToken,
        ">" => SyntaxKind.GreaterThanToken,
        "=" => SyntaxKind.EqualsToken,
        "!" => SyntaxKind.ExclamationToken,
        "(" => SyntaxKind.OpenParenToken,
        ")" => SyntaxKind.CloseParenToken,
        "{" => SyntaxKind.OpenBraceToken,
        "}" => SyntaxKind.CloseBraceToken,
        "[" => SyntaxKind.OpenBracketToken,
        "]" => SyntaxKind.CloseBracketToken,
        ":" => SyntaxKind.ColonToken,
        "," => SyntaxKind.CommaToken,
        "." => SyntaxKind.DotToken,
        "@" => SyntaxKind.CommercialAtToken,
        "+=" => SyntaxKind.PlusEqualsToken,
        "->" => SyntaxKind.MinusGreaterThanToken,
        "-=" => SyntaxKind.MinusEqualsToken,
        "*=" => SyntaxKind.AsteriskEqualsToken,
        "/=" => SyntaxKind.SlashEqualsToken,
        "^=" => SyntaxKind.CaretEqualsToken,
        "%=" => SyntaxKind.PersentEqualsToken,
        "&=" => SyntaxKind.AmpersandEqualsToken,
        "~=" => SyntaxKind.TildeEqualsToken,
        "|=" => SyntaxKind.BarEqualsToken,
        "<<" => SyntaxKind.LessThanLessThanToken,
        "<<=" => SyntaxKind.LessThanLessThanEqualsToken,
        "<=" => SyntaxKind.LessThanEqualsToken,
        ">>" => SyntaxKind.GreaterThanGreaterThanToken,
        ">>=" => SyntaxKind.GreaterThanGreaterThanEqualsToken,
        ">=" => SyntaxKind.GreaterThanEqualsToken,
        "//" => SyntaxKind.SlashSlashToken,
        "=>" => SyntaxKind.EqualsGreaterThanToken,
        "==" => SyntaxKind.EqualsEqualsToken,
        "!=" => SyntaxKind.ExclamationEqualsToken,
        ".." => SyntaxKind.DotDotToken,
        "..=" => SyntaxKind.DotDotEqualsToken,
        "..." => SyntaxKind.DotDotDotToken,
        "@@" => SyntaxKind.CommercialAtCommercialAtToken,
        "and=" => SyntaxKind.AndEqualsToken,
        "or=" => SyntaxKind.OrEqualsToken,
        "#!" => SyntaxKind.HashExclamationToken,

        _ => SyntaxKind.None
    };
    #endregion

    private static partial bool IsAnyTokenInternal(SyntaxKind kind, LanguageVersion version)
    {
        Debug.Assert(Enum.IsDefined(typeof(SyntaxKind), kind));
        return kind switch
        {
            >= SyntaxKind.PlusToken and < SyntaxKind.EndOfLineTrivia => true,

            _ => false
        };
    }

    public static partial bool IsWhitespaceTrivia(SyntaxKind kind, LanguageVersion version) => kind switch
    {
        SyntaxKind.EndOfLineTrivia or
        SyntaxKind.WhitespaceTrivia => true,

        _ => false
    };

    public static partial bool IsCommentTrivia(SyntaxKind kind, LanguageVersion version) =>
        kind switch
        {
            >= SyntaxKind.SingleLineCommentTrivia and <= SyntaxKind.MultiLineCommentTrivia => true,

            _ => false
        };

    public static partial bool IsPreprocessorDirective(SyntaxKind kind, LanguageVersion version) =>
        kind switch
        {
            >= SyntaxKind.PreprocessingMessageTrivia and <= SyntaxKind.ShebangDirectiveTrivia => true,

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
        SyntaxKind.NilLiteralExpression or
        SyntaxKind.FalseLiteralExpression or
        SyntaxKind.TrueLiteralExpression or
        SyntaxKind.NumericLiteralExpression or
        SyntaxKind.StringLiteralExpression or
        SyntaxKind.VariousArgumentsExpression => true,

        _ => false
    };

    public static partial SyntaxKind GetLiteralExpression(SyntaxKind token, LanguageVersion version) => token switch
    {
        SyntaxKind.NilKeyword => SyntaxKind.NilLiteralExpression,
        SyntaxKind.FalseKeyword => SyntaxKind.FalseLiteralExpression,
        SyntaxKind.TrueKeyword => SyntaxKind.TrueLiteralExpression,
        SyntaxKind.NumericLiteralToken => SyntaxKind.NumericLiteralExpression,
        SyntaxKind.StringLiteralToken or
        SyntaxKind.MultiLineRawStringLiteralToken => SyntaxKind.StringLiteralExpression,
        SyntaxKind.DotDotDotToken => SyntaxKind.VariousArgumentsExpression,

        _ => SyntaxKind.None
    };

    public static partial bool IsUnaryExpression(SyntaxKind expression) => expression switch
    {
        SyntaxKind.UnaryMinusExpression or
        SyntaxKind.LogicalNotExpression or
        SyntaxKind.LengthExpression or
        SyntaxKind.BitwiseNotExpression => true,

        _ => false
    };

    public static partial SyntaxKind GetUnaryExpression(SyntaxKind token, LanguageVersion version) => token switch
    {
        SyntaxKind.MinusToken => SyntaxKind.UnaryMinusExpression,
        SyntaxKind.NotKeyword => SyntaxKind.LogicalNotExpression,
        SyntaxKind.HashToken => SyntaxKind.LengthExpression,
        SyntaxKind.TildeToken => SyntaxKind.BitwiseNotExpression,

        _ => SyntaxKind.None
    };

    public static partial SyntaxKind GetUnaryExpressionOperatorToken(SyntaxKind expression, LanguageVersion version) =>
        expression switch
        {
            SyntaxKind.UnaryMinusExpression => SyntaxKind.MinusToken,
            SyntaxKind.LogicalNotExpression => SyntaxKind.NotKeyword,
            SyntaxKind.LengthExpression => SyntaxKind.HashToken,
            SyntaxKind.BitwiseNotExpression => SyntaxKind.TildeToken,

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
        SyntaxKind.BitwiseRightShiftExpression => true,
        SyntaxKind.BitwiseLeftShiftExpression => true,
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
        _ => false
    };

    public static partial SyntaxKind GetBinaryExpression(SyntaxKind token, LanguageVersion version) => token switch
    {
        SyntaxKind.PlusToken => SyntaxKind.AdditionExpression,
        SyntaxKind.MinusToken => SyntaxKind.SubtractionExpression,
        SyntaxKind.AsteriskToken => SyntaxKind.MultiplicationExpression,
        SyntaxKind.SlashToken => SyntaxKind.DivisionExpression,
        SyntaxKind.SlashSlashToken => SyntaxKind.FloorDivisionExpression,
        SyntaxKind.CaretToken => SyntaxKind.ExponentiationExpression,
        SyntaxKind.PercentToken => SyntaxKind.ModuloExpression,
        SyntaxKind.AmpersandToken => SyntaxKind.BitwiseAndExpression,
        SyntaxKind.TildeToken => SyntaxKind.BitwiseExclusiveOrExpression,
        SyntaxKind.BarToken => SyntaxKind.BitwiseOrExpression,
        SyntaxKind.GreaterThanGreaterThanToken => SyntaxKind.BitwiseRightShiftExpression,
        SyntaxKind.LessThanLessThanToken => SyntaxKind.BitwiseLeftShiftExpression,
        SyntaxKind.DotDotToken => SyntaxKind.ConcatenationExpression,
        SyntaxKind.LessThanToken => SyntaxKind.LessThanExpression,
        SyntaxKind.LessThanEqualsToken => SyntaxKind.LessThanOrEqualExpression,
        SyntaxKind.GreaterThanToken => SyntaxKind.GreaterThanExpression,
        SyntaxKind.GreaterThanEqualsToken => SyntaxKind.GreaterThanOrEqualExpression,
        SyntaxKind.EqualsEqualsToken => SyntaxKind.EqualExpression,
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
        SyntaxKind.FloorDivisionExpression => SyntaxKind.SlashSlashToken,
        SyntaxKind.ExponentiationExpression => SyntaxKind.CaretToken,
        SyntaxKind.ModuloExpression => SyntaxKind.PercentToken,
        SyntaxKind.BitwiseAndExpression => SyntaxKind.AmpersandToken,
        SyntaxKind.BitwiseExclusiveOrExpression => SyntaxKind.TildeToken,
        SyntaxKind.BitwiseOrExpression => SyntaxKind.BarToken,
        SyntaxKind.BitwiseRightShiftExpression => SyntaxKind.GreaterThanGreaterThanToken,
        SyntaxKind.BitwiseLeftShiftExpression => SyntaxKind.LessThanLessThanToken,
        SyntaxKind.ConcatenationExpression => SyntaxKind.DotDotToken,
        SyntaxKind.LessThanExpression => SyntaxKind.LessThanToken,
        SyntaxKind.LessThanOrEqualExpression => SyntaxKind.LessThanEqualsToken,
        SyntaxKind.GreaterThanExpression => SyntaxKind.GreaterThanToken,
        SyntaxKind.GreaterThanOrEqualExpression => SyntaxKind.GreaterThanEqualsToken,
        SyntaxKind.EqualExpression => SyntaxKind.EqualsEqualsToken,
        SyntaxKind.NotEqualExpression => SyntaxKind.TildeEqualsToken,
        SyntaxKind.AndExpression => SyntaxKind.AndKeyword,
        SyntaxKind.OrExpression => SyntaxKind.OrKeyword,

        _ => SyntaxKind.None
    };

    public static bool IsInstanceExpression(SyntaxKind token) =>
        GetInstanceExpression(token) != SyntaxKind.None;

    public static SyntaxKind GetInstanceExpression(SyntaxKind token) => token switch
    {
        SyntaxKind.SelfKeyword => SyntaxKind.SelfExpression,
        SyntaxKind.SuperKeyword => SyntaxKind.SuperExpression,

        _ => SyntaxKind.None,
    };

    public static bool IsAssignmentExpression(SyntaxKind kind) =>
        kind switch
        {
            // 赋值表达式
            SyntaxKind.SimpleAssignmentExpression => true,

            // 更新赋值表达式
            SyntaxKind.AdditionAssignmentExpression or
            SyntaxKind.SubtractionAssignmentExpression or
            SyntaxKind.MultiplicationAssignmentExpression or
            SyntaxKind.DivisionAssignmentExpression or
            SyntaxKind.FloorDivisionAssignmentExpression or
            SyntaxKind.ExponentiationAssignmentExpression or
            SyntaxKind.ModuloAssignmentExpression or
            SyntaxKind.BitwiseAndAssignmentExpression or
            SyntaxKind.BitwiseExclusiveOrAssignmentExpression or
            SyntaxKind.BitwiseOrAssignmentExpression or
            SyntaxKind.BitwiseRightShiftAssignmentExpression or
            SyntaxKind.BitwiseLeftShiftAssignmentExpression or
            SyntaxKind.ConcatenationAssignmentExpression or
            SyntaxKind.AndAssignmentExpression or
            SyntaxKind.OrAssignmentExpression => true,

            _ => false
        };

    public static bool IsAssignmentExpressionOperatorToken(SyntaxKind token) =>
        GetAssignmentExpression(token) != SyntaxKind.None;

    public static SyntaxKind GetAssignmentExpression(SyntaxKind token) =>
        token switch
        {
            // Regular assignment
            SyntaxKind.EqualsToken => SyntaxKind.SimpleAssignmentExpression,

            // Updating assignment
            SyntaxKind.PlusEqualsToken => SyntaxKind.AdditionAssignmentExpression,
            SyntaxKind.MinusEqualsToken => SyntaxKind.SubtractionAssignmentExpression,
            SyntaxKind.AsteriskEqualsToken => SyntaxKind.MultiplicationAssignmentExpression,
            SyntaxKind.SlashEqualsToken => SyntaxKind.DivisionAssignmentExpression,
            SyntaxKind.SlashSlashEqualsToken => SyntaxKind.FloorDivisionAssignmentExpression,
            SyntaxKind.CaretEqualsToken => SyntaxKind.ExponentiationAssignmentExpression,
            SyntaxKind.PersentEqualsToken => SyntaxKind.ModuloAssignmentExpression,
            SyntaxKind.AmpersandEqualsToken => SyntaxKind.BitwiseAndAssignmentExpression,
            SyntaxKind.BarEqualsToken => SyntaxKind.BitwiseOrAssignmentExpression,
            SyntaxKind.LessThanLessThanEqualsToken => SyntaxKind.BitwiseLeftShiftAssignmentExpression,
            SyntaxKind.GreaterThanGreaterThanEqualsToken => SyntaxKind.BitwiseRightShiftAssignmentExpression,
            SyntaxKind.DotDotEqualsToken => SyntaxKind.ConcatenationAssignmentExpression,
            SyntaxKind.AndEqualsToken => SyntaxKind.AndAssignmentExpression,
            SyntaxKind.OrEqualsToken => SyntaxKind.OrAssignmentExpression,

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
            SyntaxKind.ExclamationEqualsToken or
            SyntaxKind.EqualsEqualsToken => 3,

            SyntaxKind.AndKeyword => 2,

            SyntaxKind.OrKeyword => 1,

            _ => 0
        };
}
