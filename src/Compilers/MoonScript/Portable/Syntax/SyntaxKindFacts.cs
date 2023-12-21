// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

namespace Qtyi.CodeAnalysis.MoonScript;

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

            // 关键字
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

    /// <summary>
    /// 指定语法种类是否表示关键字。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示关键字，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsKeywordKind(SyntaxKind kind) =>
        IsReservedKeyword(kind) || IsContextualKeyword(kind);

    /// <summary>
    /// 获取所有关键字语法种类。
    /// </summary>
    /// <returns>所有关键字语法种类。</returns>
    public static IEnumerable<SyntaxKind> GetKeywordKinds() =>
        GetReservedKeywordKinds().Concat(GetContextualKeywordKinds());

    #region 保留关键字
    /// <summary>
    /// 获取所有保留关键字语法种类。
    /// </summary>
    /// <returns>所有保留关键字语法种类。</returns>
    public static IEnumerable<SyntaxKind> GetReservedKeywordKinds()
    {
        for (var i = (int)SyntaxKind.AndKeyword; i <= (int)SyntaxKind.WithKeyword; i++)
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
            >= SyntaxKind.AndKeyword and <= SyntaxKind.WithKeyword => true,

            _ => false
        };

    public static SyntaxKind GetKeywordKind(string text) =>
        text switch
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

    #region 上下文关键字
    /// <summary>
    /// 获取所有上下文关键字语法种类。
    /// </summary>
    /// <returns>所有上下文关键字语法种类。</returns>
    public static IEnumerable<SyntaxKind> GetContextualKeywordKinds()
    {
        // 上下文关键词
        for (var i = (int)SyntaxKind.GlobalEnvironmentKeyword; i <= (int)SyntaxKind.SuperKeyword; i++)
            yield return (SyntaxKind)i;

        // 元字段和元方法
        for (var i = (int)SyntaxKind.MetatableMetafield; i <= (int)SyntaxKind.WeakModeMetafield; i++)
            yield return (SyntaxKind)i;
    }

    /// <summary>
    /// 指定语法种类是否表示上下文关键字。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示上下文关键字，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsContextualKeyword(SyntaxKind kind) =>
        // 元字段和元方法
        IsMetafield(kind) ||

        // 上下文关键词
        kind switch
        {
            >= SyntaxKind.GlobalEnvironmentKeyword and <= SyntaxKind.SuperKeyword => true,

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
            >= SyntaxKind.MetatableMetafield and <= SyntaxKind.WeakModeMetafield => true,

            _ => false
        };

    public static SyntaxKind GetContextualKeywordKind(string text) =>
        text switch
        {
            // 上下文关键字
            "_G" => SyntaxKind.GlobalEnvironmentKeyword,
            "_ENV" => SyntaxKind.EnvironmentKeyword,
            "new" => SyntaxKind.NewKeyword,
            "self" => SyntaxKind.SelfKeyword,
            "super" => SyntaxKind.SuperKeyword,

            // 元字段和元方法
            _ => GetMetafieldKind(text)
        };
    #endregion

    #region 标点
    /// <summary>
    /// 获取所有标点语法种类。
    /// </summary>
    /// <returns>所有标点语法种类。</returns>
    public static IEnumerable<SyntaxKind> GetPunctuationKinds()
    {
        for (var i = (int)SyntaxKind.PlusToken; i <= (int)SyntaxKind.HashExclamationToken; i++)
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
            >= SyntaxKind.PlusToken and <= SyntaxKind.HashExclamationToken => true,

            _ => false
        };
    #endregion

    /// <summary>
    /// 指定语法种类是否表示标点或关键字（包含文件结尾标志）。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示标点或关键字，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool IsPunctuationOrKeyword(SyntaxKind kind) =>
        kind == SyntaxKind.EndOfFileToken ||
        IsPunctuation(kind) ||
        IsKeywordKind(kind);

    /// <summary>
    /// 指定语法种类是否表示字面量。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示字面量，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal static bool IsLiteral(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.NumericLiteralToken and <= SyntaxKind.InterpolatedStringTextToken => true,

            _ => false
        };

    /// <summary>
    /// 指定语法种类是否表示标志。
    /// </summary>
    /// <param name="kind">要查询的语法种类。</param>
    /// <returns>若<paramref name="kind"/>表示标志，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
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

    public static bool IsPreprocessorDirective(SyntaxKind kind) =>
        kind switch
        {
            >= SyntaxKind.PreprocessingMessageTrivia and <= SyntaxKind.CommentDirectiveTrivia => true,

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

    public static bool IsUnaryExpression(SyntaxKind token) =>
        GetUnaryExpression(token) != SyntaxKind.None;

    public static bool IsUnaryExpressionOperatorToken(SyntaxKind token) => GetUnaryExpression(token) != SyntaxKind.None;

    public static SyntaxKind GetUnaryExpression(SyntaxKind token) =>
        token switch
        {
            SyntaxKind.MinusToken => SyntaxKind.UnaryMinusExpression,
            SyntaxKind.NotKeyword => SyntaxKind.LogicalNotExpression,
            SyntaxKind.HashToken => SyntaxKind.LengthExpression,
            SyntaxKind.TildeToken => SyntaxKind.BitwiseNotExpression,

            _ => SyntaxKind.None
        };

    public static bool IsLiteralExpression(SyntaxKind token) =>
        GetLiteralExpression(token) != SyntaxKind.None;

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

    public static bool IsInstanceExpression(SyntaxKind token) =>
        GetInstanceExpression(token) != SyntaxKind.None;

    public static SyntaxKind GetInstanceExpression(SyntaxKind token) =>
        token switch
        {
            SyntaxKind.SelfKeyword => SyntaxKind.SelfExpression,
            SyntaxKind.SuperKeyword => SyntaxKind.SuperExpression,

            _ => SyntaxKind.None,
        };

    public static bool IsBinaryExpression(SyntaxKind token) =>
        GetBinaryExpression(token) != SyntaxKind.None;

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
            // 赋值表达式
            SyntaxKind.EqualsToken => SyntaxKind.SimpleAssignmentExpression,

            // 更新赋值表达式
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

    public static SyntaxKind GetMetafieldKind(string metafieldname) =>
        metafieldname switch
        {
            "__metatable" => SyntaxKind.MetatableMetafield,
            "__class" => SyntaxKind.ClassMetafield,
            "__name" => SyntaxKind.NameMetafield,
            "__inherited" => SyntaxKind.InheritedMetamethod,
            "__base" => SyntaxKind.BaseMetafield,
            "__parent" => SyntaxKind.ParentMetafield,
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
