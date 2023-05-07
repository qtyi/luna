// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.Lua;

public static partial class SyntaxFacts
{
    /// <summary>
    /// 获取部分语法种类对应的文本表示。
    /// </summary>
    /// <param name="kind">要获取的语法种类。</param>
    /// <returns>表示<paramref name="kind"/>的文本。</returns>
    public static string GetText(SyntaxKind kind) =>
        kind switch
        {
            // 标点
            SyntaxKind.PlusToken => "+",
            SyntaxKind.MinusToken => "-",
            SyntaxKind.AsteriskToken => "*",
            SyntaxKind.SlashToken => "/",
            SyntaxKind.CaretToken => "^",
            SyntaxKind.PersentToken => "%",
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

            // 关键字
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
            SyntaxKind.GlobalEnvironmentKeyword => "_G",
            SyntaxKind.EnvironmentKeyword => "_ENV",
            SyntaxKind.MetatableMetafield => "__metatable",
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
            SyntaxKind.NameMetafield => "__name",
            SyntaxKind.ConstKeyword => "const",
            SyntaxKind.CloseKeyword => "close",

            _ => string.Empty
        };

    /// <summary>
    /// 指定语法种类是否表示关键字。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示关键字，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsKeywordKind(SyntaxKind kind) =>
        SyntaxFacts.IsReservedKeyword(kind) || SyntaxFacts.IsContextualKeyword(kind);

    /// <summary>
    /// 获取所有关键字语法种类。
    /// </summary>
    /// <returns>所有关键字语法种类。</returns>
    public static IEnumerable<SyntaxKind> GetKeywordKinds() =>
        SyntaxFacts.GetReservedKeywordKinds().Concat(SyntaxFacts.GetContextualKeywordKinds());

    #region 保留关键字
    /// <summary>
    /// 获取所有保留关键字语法种类。
    /// </summary>
    /// <returns>所有保留关键字语法种类。</returns>
    public static IEnumerable<SyntaxKind> GetReservedKeywordKinds()
    {
        for (var i = (int)SyntaxKind.AndKeyword; i <= (int)SyntaxKind.WhileKeyword; i++)
            yield return (SyntaxKind)i;
    }

    /// <summary>
    /// 指定语法种类是否表示保留关键字。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示保留关键字，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsReservedKeyword(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.AndKeyword and <= SyntaxKind.WhileKeyword => true,

            _ => false
        };

    public static SyntaxKind GetKeywordKind(string text) =>
        text switch
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

    #region 上下文关键字
    /// <summary>
    /// 获取所有上下文关键字语法种类。
    /// </summary>
    /// <returns>所有上下文关键字语法种类。</returns>
    public static IEnumerable<SyntaxKind> GetContextualKeywordKinds()
    {
        // 上下文关键词
        for (var i = (int)SyntaxKind.GlobalEnvironmentKeyword; i <= (int)SyntaxKind.EnvironmentKeyword; i++)
            yield return (SyntaxKind)i;

        // 元字段和元方法
        for (var i = (int)SyntaxKind.MetatableMetafield; i <= (int)SyntaxKind.NameMetafield; i++)
            yield return (SyntaxKind)i;

        // 特性
        for (var i = (int)SyntaxKind.CloseKeyword; i <= (int)SyntaxKind.ConstKeyword; i++)
            yield return (SyntaxKind)i;
    }

    /// <summary>
    /// 指定语法种类是否表示上下文关键字。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示上下文关键字，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsContextualKeyword(SyntaxKind kind) =>
        // 元字段和元方法
        SyntaxFacts.IsMetafield(kind) || SyntaxFacts.IsAttribute(kind) ||

        // 上下文关键词
        kind switch
        {
            >= SyntaxKind.GlobalEnvironmentKeyword and <= SyntaxKind.EnvironmentKeyword => true,

            _ => false
        };

    /// <summary>
    /// 指定语法种类是否表示元字段和元方法。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示元字段和元方法，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsMetafield(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.MetatableMetafield and <= SyntaxKind.NameMetafield => true,

            _ => false
        };

    /// <summary>
    /// 指定语法种类是否表示特性。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    public static bool IsAttribute(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.CloseKeyword and <= SyntaxKind.ConstKeyword => true,

            _ => false
        };

    public static SyntaxKind GetContextualKeywordKind(string text) =>
        text switch
        {
            // 上下文关键字
            "_G" => SyntaxKind.GlobalEnvironmentKeyword,
            "_ENV" => SyntaxKind.EnvironmentKeyword,

            _ => text.StartsWith("__") ?
                // 元字段和元方法
                SyntaxFacts.GetMetafieldKind(text) :
                // 特性
                SyntaxFacts.GetAttributeKind(text)
        };
    #endregion

    #region 标点
    /// <summary>
    /// 获取所有标点语法种类。
    /// </summary>
    /// <returns>所有标点语法种类。</returns>
    public static IEnumerable<SyntaxKind> GetPunctuationKinds()
    {
        for (var i = (int)SyntaxKind.PlusToken; i <= (int)SyntaxKind.DotDotDotToken; i++)
            yield return (SyntaxKind)i;
    }

    /// <summary>
    /// 指定语法种类是否表示标点。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示标点，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsPunctuation(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.PlusToken and <= SyntaxKind.DotDotDotToken => true,

            _ => false
        };
    #endregion

    /// <summary>
    /// 指定语法种类是否表示标点或关键字（包含文件结尾标记）。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示标点或关键字，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsPunctuationOrKeyword(SyntaxKind kind) =>
        kind == SyntaxKind.EndOfFileToken ||
        SyntaxFacts.IsPunctuation(kind) ||
        SyntaxFacts.IsKeywordKind(kind);

    /// <summary>
    /// 指定语法种类是否表示字面量。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示字面量，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal static bool IsLiteral(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.NumericLiteralToken and <= SyntaxKind.MultiLineRawStringLiteralToken => true,

            _ => false
        };

    /// <summary>
    /// 指定语法种类是否表示标记。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示标记，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsAnyToken(SyntaxKind kind)
    {
        Debug.Assert(Enum.IsDefined(typeof(SyntaxKind), kind));
        return kind switch
        {
            >= SyntaxKind.PlusToken and < SyntaxKind.EndOfLineTrivia => true,

            _ => false
        };
    }

    /// <summary>
    /// 指定语法种类是否表示琐碎内容。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示琐碎内容，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsTrivia(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.EndOfLineTrivia and <= SyntaxKind.SkippedTokensTrivia => true,

            _ => false
        };

    /// <summary>
    /// 指定语法种类是否表示注释琐碎内容。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示注释琐碎内容，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsCommentTrivia(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.SingleLineCommentTrivia and <= SyntaxKind.MultiLineCommentTrivia => true,

            _ => false
        };

    /// <summary>
    /// 指定语法种类是否表示名称。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示名称，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsName(SyntaxKind kind) =>
        kind switch
        {
            SyntaxKind.IdentifierName => true,

            _ => false
        };

    public static bool IsLiteralExpression(SyntaxKind expression) =>
        expression switch
        {
            SyntaxKind.NilLiteralExpression or
            SyntaxKind.FalseLiteralExpression or
            SyntaxKind.TrueLiteralExpression or
            SyntaxKind.NumericLiteralExpression or
            SyntaxKind.StringLiteralExpression or
            SyntaxKind.VariousArgumentsExpression => true,

            _ => false
        };

    public static bool IsLiteralToken(SyntaxKind token) =>
        SyntaxFacts.GetLiteralExpression(token) != SyntaxKind.None;

    public static SyntaxKind GetLiteralExpression(SyntaxKind token) =>
        token switch
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

    public static bool IsUnaryExpression(SyntaxKind expression) =>
        SyntaxFacts.GetUnaryExpressionOperatorToken(expression) != SyntaxKind.None;

    public static bool IsUnaryExpressionOperatorToken(SyntaxKind token) => SyntaxFacts.GetUnaryExpression(token) != SyntaxKind.None;

    public static SyntaxKind GetUnaryExpression(SyntaxKind token) =>
        token switch
        {
            SyntaxKind.MinusToken => SyntaxKind.UnaryMinusExpression,
            SyntaxKind.NotKeyword => SyntaxKind.LogicalNotExpression,
            SyntaxKind.HashToken => SyntaxKind.LengthExpression,
            SyntaxKind.TildeToken => SyntaxKind.BitwiseNotExpression,

            _ => SyntaxKind.None
        };

    public static SyntaxKind GetUnaryExpressionOperatorToken(SyntaxKind expression) =>
        expression switch
        {
            SyntaxKind.UnaryMinusExpression => SyntaxKind.MinusToken,
            SyntaxKind.LogicalNotExpression => SyntaxKind.NotKeyword,
            SyntaxKind.LengthExpression => SyntaxKind.HashToken,
            SyntaxKind.BitwiseNotExpression => SyntaxKind.TildeToken,

            _ => SyntaxKind.None
        };

    public static bool IsBinaryExpression(SyntaxKind expression) =>
        SyntaxFacts.GetBinaryExpressionOperatorToken(expression) != SyntaxKind.None;

    public static bool IsBinaryExpressionOperatorToken(SyntaxKind token) => SyntaxFacts.GetBinaryExpression(token) != SyntaxKind.None;

    internal static bool IsLeftAssociativeBinaryExpressionOperatorToken(SyntaxKind token) => SyntaxFacts.IsBinaryExpressionOperatorToken(token) && !SyntaxFacts.IsRightAssociativeBinaryExpressionOperatorToken(token);

    internal static bool IsRightAssociativeBinaryExpressionOperatorToken(SyntaxKind token) =>
        token switch
        {
            SyntaxKind.CaretToken or
            SyntaxKind.DotDotToken => true,

            _ => false
        };

    public static SyntaxKind GetBinaryExpression(SyntaxKind token) =>
        token switch
        {
            SyntaxKind.PlusToken => SyntaxKind.AdditionExpression,
            SyntaxKind.MinusToken => SyntaxKind.SubtractionExpression,
            SyntaxKind.AsteriskToken => SyntaxKind.MultiplicationExpression,
            SyntaxKind.SlashToken => SyntaxKind.DivisionExpression,
            SyntaxKind.SlashSlashToken => SyntaxKind.FloorDivisionExpression,
            SyntaxKind.CaretToken => SyntaxKind.ExponentiationExpression,
            SyntaxKind.PersentToken => SyntaxKind.ModuloExpression,
            SyntaxKind.AmpersandToken => SyntaxKind.BitwiseAndExpression,
            SyntaxKind.TildeToken => SyntaxKind.BitwiseExclusiveOrExpression,
            SyntaxKind.BarToken => SyntaxKind.BitwiseOrExpression,
            SyntaxKind.LessThanLessThanToken => SyntaxKind.BitwiseLeftShiftExpression,
            SyntaxKind.GreaterThanGreaterThanToken => SyntaxKind.BitwiseRightShiftExpression,
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

    internal static int GetOperatorPrecedence(SyntaxKind token, bool isUnary) =>
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
            SyntaxKind.PersentToken => 10,

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
            SyntaxKind.EqualsEqualsToken => 3,

            SyntaxKind.AndKeyword => 2,

            SyntaxKind.OrKeyword => 1,

            _ => 0
        };

    public static SyntaxKind GetBinaryExpressionOperatorToken(SyntaxKind expression) =>
        expression switch
        {
            SyntaxKind.AdditionExpression => SyntaxKind.PlusToken,
            SyntaxKind.SubtractionExpression => SyntaxKind.MinusToken,
            SyntaxKind.MultiplicationExpression => SyntaxKind.AsteriskToken,
            SyntaxKind.DivisionExpression => SyntaxKind.SlashToken,
            SyntaxKind.FloorDivisionExpression => SyntaxKind.SlashSlashToken,
            SyntaxKind.ExponentiationExpression => SyntaxKind.CaretToken,
            SyntaxKind.ModuloExpression => SyntaxKind.PersentToken,
            SyntaxKind.BitwiseAndExpression => SyntaxKind.AmpersandToken,
            SyntaxKind.BitwiseExclusiveOrExpression => SyntaxKind.TildeToken,
            SyntaxKind.BitwiseOrExpression => SyntaxKind.BarToken,
            SyntaxKind.BitwiseLeftShiftExpression => SyntaxKind.LessThanLessThanToken,
            SyntaxKind.BitwiseRightShiftExpression => SyntaxKind.GreaterThanGreaterThanToken,
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

    public static SyntaxKind GetMetafieldKind(string metafieldname) =>
        metafieldname switch
        {
            "__metatable" => SyntaxKind.MetatableMetafield,
            "__add" => SyntaxKind.AdditionMetamethod,
            "__sub" => SyntaxKind.SubtractionMetamethod,
            "__mul" => SyntaxKind.MultiplicationMetamethod,
            "__div" => SyntaxKind.DivisionMetamethod,
            "__mod" => SyntaxKind.ModuloMetamethod,
            "__pow" => SyntaxKind.ExponentiationMetamethod,
            "__unm" => SyntaxKind.NegationMetamethod,
            "__idiv" => SyntaxKind.FloorDivisionMetamethod,
            "__band" => SyntaxKind.BitwiseAndMetamethod,
            "__bor" => SyntaxKind.BitwiseOrMetamethod,
            "__bxor" => SyntaxKind.BitwiseExclusiveOrMetamethod,
            "__bnot" => SyntaxKind.BitwiseNotMetamethod,
            "__shl" => SyntaxKind.BitwiseLeftShiftMetamethod,
            "__shr" => SyntaxKind.BitwiseRightShiftMetamethod,
            "__concat" => SyntaxKind.ConcatenationMetamethod,
            "__len" => SyntaxKind.LengthMetamethod,
            "__eq" => SyntaxKind.EqualMetamethod,
            "__lt" => SyntaxKind.LessThanMetamethod,
            "__le" => SyntaxKind.LessEqualMetamethod,
            "__index" => SyntaxKind.IndexingAccessMetamethod,
            "__call" => SyntaxKind.CallMetamethod,
            "__pairs" => SyntaxKind.PairsMetamethod,
            "__tostring" => SyntaxKind.ToStringMetamethod,
            "__gc" => SyntaxKind.GarbageCollectionMetamethod,
            "__close" => SyntaxKind.ToBeClosedMetamethod,
            "__mode" => SyntaxKind.WeakModeMetafield,
            "__name" => SyntaxKind.NameMetafield,

            _ => SyntaxKind.None
        };

    public static SyntaxKind GetAttributeKind(string attributeName) =>
        attributeName switch
        {
            "close" => SyntaxKind.CloseKeyword,
            "const" => SyntaxKind.ConstKeyword,

            _ => SyntaxKind.None
        };

    public static SyntaxKind GetOperatorKind(string operatorMetafieldName) =>
        operatorMetafieldName switch
        {
            "__add" => SyntaxKind.PlusToken,
            "__sub" => SyntaxKind.MinusToken,
            "__mul" => SyntaxKind.AsteriskToken,
            "__div" => SyntaxKind.SlashToken,
            "__mod" => SyntaxKind.PersentToken,
            "__pow" => SyntaxKind.CaretToken,
            "__unm" => SyntaxKind.MinusToken,
            "__idiv" => SyntaxKind.SlashSlashToken,
            "__band" => SyntaxKind.AmpersandToken,
            "__bor" => SyntaxKind.BarToken,
            "__bxor" => SyntaxKind.TildeToken,
            "__bnot" => SyntaxKind.TildeToken,
            "__shl" => SyntaxKind.LessThanLessThanToken,
            "__shr" => SyntaxKind.GreaterThanGreaterThanToken,
            "__concat" => SyntaxKind.DotDotToken,
            "__len" => SyntaxKind.HashToken,
            "__eq" => SyntaxKind.EqualsToken,
            "__lt" => SyntaxKind.LessThanToken,
            "__le" => SyntaxKind.LessThanEqualsToken,

            _ => SyntaxKind.None
        };
}
