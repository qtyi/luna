// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.UnitTests;

partial class SyntaxFactsTests
{
    internal static partial IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_ReservedKeyword(LanguageVersion version) => version switch
    {
        LanguageVersion.Lua1_1 or
        LanguageVersion.Lua2_1 or
        LanguageVersion.Lua2_2 or
        LanguageVersion.Lua2_4 or
        LanguageVersion.Lua2_5 or
        LanguageVersion.Lua3_1 or
        LanguageVersion.Lua3_2 =>
        [
            (SyntaxKind.AndKeyword, "and"),
            (SyntaxKind.DoKeyword, "do"),
            (SyntaxKind.ElseKeyword, "else"),
            (SyntaxKind.ElseIfKeyword, "elseif"),
            (SyntaxKind.EndKeyword, "end"),
            (SyntaxKind.FunctionKeyword, "function"),
            (SyntaxKind.IfKeyword, "if"),
            (SyntaxKind.LocalKeyword, "local"),
            (SyntaxKind.NilKeyword, "nil"),
            (SyntaxKind.NotKeyword, "not"),
            (SyntaxKind.OrKeyword, "or"),
            (SyntaxKind.RepeatKeyword, "repeat"),
            (SyntaxKind.ReturnKeyword, "return"),
            (SyntaxKind.ThenKeyword, "then"),
            (SyntaxKind.UntilKeyword, "until"),
            (SyntaxKind.WhileKeyword, "while")
        ],
        LanguageVersion.Lua4 =>
        [
            (SyntaxKind.AndKeyword, "and"),
            (SyntaxKind.BreakKeyword, "break"),
            (SyntaxKind.DoKeyword, "do"),
            (SyntaxKind.ElseKeyword, "else"),
            (SyntaxKind.ElseIfKeyword, "elseif"),
            (SyntaxKind.EndKeyword, "end"),
            (SyntaxKind.ForKeyword, "for"),
            (SyntaxKind.FunctionKeyword, "function"),
            (SyntaxKind.IfKeyword, "if"),
            (SyntaxKind.InKeyword, "in"),
            (SyntaxKind.LocalKeyword, "local"),
            (SyntaxKind.NilKeyword, "nil"),
            (SyntaxKind.NotKeyword, "not"),
            (SyntaxKind.OrKeyword, "or"),
            (SyntaxKind.RepeatKeyword, "repeat"),
            (SyntaxKind.ReturnKeyword, "return"),
            (SyntaxKind.ThenKeyword, "then"),
            (SyntaxKind.UntilKeyword, "until"),
            (SyntaxKind.WhileKeyword, "while")
        ],
        LanguageVersion.Lua5 or
        LanguageVersion.Lua5_1 =>
        [
            (SyntaxKind.AndKeyword, "and"),
            (SyntaxKind.BreakKeyword, "break"),
            (SyntaxKind.DoKeyword, "do"),
            (SyntaxKind.ElseKeyword, "else"),
            (SyntaxKind.ElseIfKeyword, "elseif"),
            (SyntaxKind.EndKeyword, "end"),
            (SyntaxKind.FalseKeyword, "false"),
            (SyntaxKind.ForKeyword, "for"),
            (SyntaxKind.FunctionKeyword, "function"),
            (SyntaxKind.IfKeyword, "if"),
            (SyntaxKind.InKeyword, "in"),
            (SyntaxKind.LocalKeyword, "local"),
            (SyntaxKind.NilKeyword, "nil"),
            (SyntaxKind.NotKeyword, "not"),
            (SyntaxKind.OrKeyword, "or"),
            (SyntaxKind.RepeatKeyword, "repeat"),
            (SyntaxKind.ReturnKeyword, "return"),
            (SyntaxKind.ThenKeyword, "then"),
            (SyntaxKind.TrueKeyword, "true"),
            (SyntaxKind.UntilKeyword, "until"),
            (SyntaxKind.WhileKeyword, "while")
        ],
        LanguageVersion.Lua5_2 or
        LanguageVersion.Lua5_3 or
        LanguageVersion.Lua5_4 or
        LanguageVersion.Preview or
        LanguageVersion.DotNet =>
        [
            (SyntaxKind.AndKeyword, "and"),
            (SyntaxKind.BreakKeyword, "break"),
            (SyntaxKind.DoKeyword, "do"),
            (SyntaxKind.ElseKeyword, "else"),
            (SyntaxKind.ElseIfKeyword, "elseif"),
            (SyntaxKind.EndKeyword, "end"),
            (SyntaxKind.FalseKeyword, "false"),
            (SyntaxKind.ForKeyword, "for"),
            (SyntaxKind.FunctionKeyword, "function"),
            (SyntaxKind.GotoKeyword, "goto"),
            (SyntaxKind.IfKeyword, "if"),
            (SyntaxKind.InKeyword, "in"),
            (SyntaxKind.LocalKeyword, "local"),
            (SyntaxKind.NilKeyword, "nil"),
            (SyntaxKind.NotKeyword, "not"),
            (SyntaxKind.OrKeyword, "or"),
            (SyntaxKind.RepeatKeyword, "repeat"),
            (SyntaxKind.ReturnKeyword, "return"),
            (SyntaxKind.ThenKeyword, "then"),
            (SyntaxKind.TrueKeyword, "true"),
            (SyntaxKind.UntilKeyword, "until"),
            (SyntaxKind.WhileKeyword, "while")
        ],
        _ => []
    };

    internal static partial IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_ContextualKeyword(LanguageVersion version) => [
        .. GetExpectedSyntaxKindInfos_EnvironmentKeyword(version),
        .. GetExpectedSyntaxKindInfos_VariableAttributeKeyword(version),
        .. GetExpectedSyntaxKindInfos_DotnetKeyword(version)
    ];

    private static IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_EnvironmentKeyword(LanguageVersion version) => version switch
    {
        LanguageVersion.Lua1_1 or
        LanguageVersion.Lua2_1 or
        LanguageVersion.Lua2_2 or
        LanguageVersion.Lua2_4 or
        LanguageVersion.Lua2_5 or
        LanguageVersion.Lua3_1 or
        LanguageVersion.Lua3_2 or
        LanguageVersion.Lua4 or
        LanguageVersion.Lua5 or
        LanguageVersion.Lua5_1 =>
        [
        ],
        LanguageVersion.Lua5_2 or
        LanguageVersion.Lua5_3 or
        LanguageVersion.Lua5_4 or
        LanguageVersion.Preview or
        LanguageVersion.DotNet =>
        [
            (SyntaxKind.EnvironmentKeyword, "_ENV")
        ],
        _ => []
    };

    private static IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_VariableAttributeKeyword(LanguageVersion version) => version switch
    {
        LanguageVersion.Lua1_1 or
        LanguageVersion.Lua2_1 or
        LanguageVersion.Lua2_2 or
        LanguageVersion.Lua2_4 or
        LanguageVersion.Lua2_5 or
        LanguageVersion.Lua3_1 or
        LanguageVersion.Lua3_2 or
        LanguageVersion.Lua4 or
        LanguageVersion.Lua5 or
        LanguageVersion.Lua5_1 or
        LanguageVersion.Lua5_2 or
        LanguageVersion.Lua5_3 =>
        [
        ],
        LanguageVersion.Lua5_4 or
        LanguageVersion.Preview or
        LanguageVersion.DotNet =>
        [
            (SyntaxKind.CloseKeyword, "close"),
            (SyntaxKind.ConstKeyword, "const")
        ],
        _ => []
    };

    private static IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_DotnetKeyword(LanguageVersion version) => version switch
    {
        LanguageVersion.Lua1_1 or
        LanguageVersion.Lua2_1 or
        LanguageVersion.Lua2_2 or
        LanguageVersion.Lua2_4 or
        LanguageVersion.Lua2_5 or
        LanguageVersion.Lua3_1 or
        LanguageVersion.Lua3_2 or
        LanguageVersion.Lua4 or
        LanguageVersion.Lua5 or
        LanguageVersion.Lua5_1 or
        LanguageVersion.Lua5_2 or
        LanguageVersion.Lua5_3 or
        LanguageVersion.Lua5_4 or
        LanguageVersion.Preview =>
        [
        ],
        LanguageVersion.DotNet =>
        [
            (SyntaxKind.AbstractKeyword, "abstract"),
            (SyntaxKind.AnnotatedWithKeyword, "annotatedwith"),
            (SyntaxKind.AssemblyKeyword, "assembly"),
            (SyntaxKind.ClassKeyword, "class"),
            (SyntaxKind.ConstrainAsKeyword, "constrainas"),
            (SyntaxKind.EventKeyword, "event"),
            (SyntaxKind.ExtendsKeyword, "extends"),
            (SyntaxKind.FieldKeyword, "field"),
            (SyntaxKind.FinalKeyword, "final"),
            (SyntaxKind.ImplementsKeyword, "implements"),
            (SyntaxKind.InterfaceKeyword, "interface"),
            (SyntaxKind.MethodKeyword, "method"),
            (SyntaxKind.ModuleKeyword, "module"),
            (SyntaxKind.NamespaceKeyword, "namespace"),
            (SyntaxKind.NewKeyword, "new"),
            (SyntaxKind.OutKeyword, "out"),
            (SyntaxKind.ParameterKeyword, "parameter"),
            (SyntaxKind.PrivateKeyword, "private"),
            (SyntaxKind.PropertyKeyword, "property"),
            (SyntaxKind.ProtectedKeyword, "protected"),
            (SyntaxKind.PublicKeyword, "public"),
            (SyntaxKind.ReadonlyKeyword, "readonly"),
            (SyntaxKind.RefKeyword, "ref"),
            (SyntaxKind.TypeParameterKeyword, "typeparameter"),
            (SyntaxKind.StaticKeyword, "static")
        ],
        _ => []
    };

    internal static partial IEnumerable<SyntaxKindInfo> GetExpectedSyntaxKindInfos_Punctuation(LanguageVersion version) => version switch
    {
        LanguageVersion.Lua1_1 =>
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.AsteriskToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.PercentToken, "%"),
            (SyntaxKind.CommercialAtToken, "@"),
            (SyntaxKind.LessThanToken, "<"),
            (SyntaxKind.GreaterThanToken, ">"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.OpenParenToken, "("),
            (SyntaxKind.CloseParenToken, ")"),
            (SyntaxKind.OpenBraceToken, "{"),
            (SyntaxKind.CloseBraceToken, "}"),
            (SyntaxKind.OpenBracketToken, "["),
            (SyntaxKind.CloseBracketToken, "]"),
            (SyntaxKind.SemicolonToken, ";"),
            (SyntaxKind.CommaToken, ","),
            (SyntaxKind.DotToken, "."),
            (SyntaxKind.LessThanEqualsToken, "<="),
            (SyntaxKind.GreaterThanEqualsToken, ">="),
            (SyntaxKind.TildeEqualsToken, "~="),
            (SyntaxKind.DotDotToken, "..")
        ],
        LanguageVersion.Lua2_1 or
        LanguageVersion.Lua2_2 or
        LanguageVersion.Lua2_4 =>
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.AsteriskToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.PercentToken, "%"),
            (SyntaxKind.LessThanToken, "<"),
            (SyntaxKind.GreaterThanToken, ">"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.OpenParenToken, "("),
            (SyntaxKind.CloseParenToken, ")"),
            (SyntaxKind.OpenBraceToken, "{"),
            (SyntaxKind.CloseBraceToken, "}"),
            (SyntaxKind.OpenBracketToken, "["),
            (SyntaxKind.CloseBracketToken, "]"),
            (SyntaxKind.SemicolonToken, ";"),
            (SyntaxKind.CommaToken, ","),
            (SyntaxKind.DotToken, "."),
            (SyntaxKind.LessThanEqualsToken, "<="),
            (SyntaxKind.GreaterThanEqualsToken, ">="),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.TildeEqualsToken, "~="),
            (SyntaxKind.DotDotToken, "..")
        ],
        LanguageVersion.Lua2_5 =>
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.AsteriskToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.PercentToken, "%"),
            (SyntaxKind.LessThanToken, "<"),
            (SyntaxKind.GreaterThanToken, ">"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.OpenParenToken, "("),
            (SyntaxKind.CloseParenToken, ")"),
            (SyntaxKind.OpenBraceToken, "{"),
            (SyntaxKind.CloseBraceToken, "}"),
            (SyntaxKind.OpenBracketToken, "["),
            (SyntaxKind.CloseBracketToken, "]"),
            (SyntaxKind.SemicolonToken, ";"),
            (SyntaxKind.CommaToken, ","),
            (SyntaxKind.DotToken, "."),
            (SyntaxKind.LessThanEqualsToken, "<="),
            (SyntaxKind.GreaterThanEqualsToken, ">="),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.TildeEqualsToken, "~="),
            (SyntaxKind.DotDotToken, ".."),
            (SyntaxKind.HashExclamationToken, "#!")
        ],
        LanguageVersion.Lua3_1 or
        LanguageVersion.Lua3_2 or
        LanguageVersion.Lua4 =>
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.AsteriskToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.PercentToken, "%"),
            (SyntaxKind.LessThanToken, "<"),
            (SyntaxKind.GreaterThanToken, ">"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.OpenParenToken, "("),
            (SyntaxKind.CloseParenToken, ")"),
            (SyntaxKind.OpenBraceToken, "{"),
            (SyntaxKind.CloseBraceToken, "}"),
            (SyntaxKind.OpenBracketToken, "["),
            (SyntaxKind.CloseBracketToken, "]"),
            (SyntaxKind.SemicolonToken, ";"),
            (SyntaxKind.CommaToken, ","),
            (SyntaxKind.DotToken, "."),
            (SyntaxKind.LessThanEqualsToken, "<="),
            (SyntaxKind.GreaterThanEqualsToken, ">="),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.TildeEqualsToken, "~="),
            (SyntaxKind.DotDotToken, ".."),
            (SyntaxKind.DotDotDotToken, "..."),
            (SyntaxKind.HashExclamationToken, "#!")
        ],
        LanguageVersion.Lua5 =>
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.AsteriskToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.CaretToken, "^"),
            (SyntaxKind.LessThanToken, "<"),
            (SyntaxKind.GreaterThanToken, ">"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.OpenParenToken, "("),
            (SyntaxKind.CloseParenToken, ")"),
            (SyntaxKind.OpenBraceToken, "{"),
            (SyntaxKind.CloseBraceToken, "}"),
            (SyntaxKind.OpenBracketToken, "["),
            (SyntaxKind.CloseBracketToken, "]"),
            (SyntaxKind.ColonToken, ":"),
            (SyntaxKind.SemicolonToken, ";"),
            (SyntaxKind.CommaToken, ","),
            (SyntaxKind.DotToken, "."),
            (SyntaxKind.LessThanEqualsToken, "<="),
            (SyntaxKind.GreaterThanEqualsToken, ">="),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.TildeEqualsToken, "~="),
            (SyntaxKind.DotDotToken, ".."),
            (SyntaxKind.DotDotDotToken, "..."),
            (SyntaxKind.HashExclamationToken, "#!")
        ],
        LanguageVersion.Lua5_1 =>
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.AsteriskToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.CaretToken, "^"),
            (SyntaxKind.PercentToken, "%"),
            (SyntaxKind.HashToken, "#"),
            (SyntaxKind.LessThanToken, "<"),
            (SyntaxKind.GreaterThanToken, ">"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.OpenParenToken, "("),
            (SyntaxKind.CloseParenToken, ")"),
            (SyntaxKind.OpenBraceToken, "{"),
            (SyntaxKind.CloseBraceToken, "}"),
            (SyntaxKind.OpenBracketToken, "["),
            (SyntaxKind.CloseBracketToken, "]"),
            (SyntaxKind.ColonToken, ":"),
            (SyntaxKind.SemicolonToken, ";"),
            (SyntaxKind.CommaToken, ","),
            (SyntaxKind.DotToken, "."),
            (SyntaxKind.LessThanEqualsToken, "<="),
            (SyntaxKind.GreaterThanEqualsToken, ">="),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.TildeEqualsToken, "~="),
            (SyntaxKind.DotDotToken, ".."),
            (SyntaxKind.DotDotDotToken, "..."),
            (SyntaxKind.HashExclamationToken, "#!")
        ],
        LanguageVersion.Lua5_2 =>
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.AsteriskToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.CaretToken, "^"),
            (SyntaxKind.PercentToken, "%"),
            (SyntaxKind.HashToken, "#"),
            (SyntaxKind.LessThanToken, "<"),
            (SyntaxKind.GreaterThanToken, ">"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.OpenParenToken, "("),
            (SyntaxKind.CloseParenToken, ")"),
            (SyntaxKind.OpenBraceToken, "{"),
            (SyntaxKind.CloseBraceToken, "}"),
            (SyntaxKind.OpenBracketToken, "["),
            (SyntaxKind.CloseBracketToken, "]"),
            (SyntaxKind.ColonToken, ":"),
            (SyntaxKind.SemicolonToken, ";"),
            (SyntaxKind.CommaToken, ","),
            (SyntaxKind.DotToken, "."),
            (SyntaxKind.LessThanEqualsToken, "<="),
            (SyntaxKind.GreaterThanEqualsToken, ">="),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.TildeEqualsToken, "~="),
            (SyntaxKind.ColonColonToken, "::"),
            (SyntaxKind.DotDotToken, ".."),
            (SyntaxKind.DotDotDotToken, "..."),
            (SyntaxKind.HashExclamationToken, "#!")
        ],
        LanguageVersion.Lua5_3 or
        LanguageVersion.Lua5_4 or
        LanguageVersion.Preview or
        LanguageVersion.DotNet =>
        [
            (SyntaxKind.PlusToken, "+"),
            (SyntaxKind.MinusToken, "-"),
            (SyntaxKind.AsteriskToken, "*"),
            (SyntaxKind.SlashToken, "/"),
            (SyntaxKind.CaretToken, "^"),
            (SyntaxKind.PercentToken, "%"),
            (SyntaxKind.HashToken, "#"),
            (SyntaxKind.AmpersandToken, "&"),
            (SyntaxKind.TildeToken, "~"),
            (SyntaxKind.BarToken, "|"),
            (SyntaxKind.LessThanToken, "<"),
            (SyntaxKind.GreaterThanToken, ">"),
            (SyntaxKind.EqualsToken, "="),
            (SyntaxKind.OpenParenToken, "("),
            (SyntaxKind.CloseParenToken, ")"),
            (SyntaxKind.OpenBraceToken, "{"),
            (SyntaxKind.CloseBraceToken, "}"),
            (SyntaxKind.OpenBracketToken, "["),
            (SyntaxKind.CloseBracketToken, "]"),
            (SyntaxKind.ColonToken, ":"),
            (SyntaxKind.SemicolonToken, ";"),
            (SyntaxKind.CommaToken, ","),
            (SyntaxKind.DotToken, "."),
            (SyntaxKind.LessThanLessThanToken, "<<"),
            (SyntaxKind.LessThanEqualsToken, "<="),
            (SyntaxKind.GreaterThanGreaterThanToken, ">>"),
            (SyntaxKind.GreaterThanEqualsToken, ">="),
            (SyntaxKind.SlashSlashToken, "//"),
            (SyntaxKind.EqualsEqualsToken, "=="),
            (SyntaxKind.TildeEqualsToken, "~="),
            (SyntaxKind.ColonColonToken, "::"),
            (SyntaxKind.DotDotToken, ".."),
            (SyntaxKind.DotDotDotToken, "..."),
            (SyntaxKind.HashExclamationToken, "#!")
        ],
        _ => []
    };
}
