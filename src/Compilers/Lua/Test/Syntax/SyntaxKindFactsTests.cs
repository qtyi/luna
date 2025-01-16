// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Roslyn.Test.Utilities;
using Roslyn.Utilities;
using Xunit;

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
        _ => throw ExceptionUtilities.UnexpectedValue(version)
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
        LanguageVersion.DotNet =>
        [
            (SyntaxKind.EnvironmentKeyword, "_ENV")
        ],
        _ => throw ExceptionUtilities.UnexpectedValue(version)
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
        LanguageVersion.DotNet =>
        [
            (SyntaxKind.CloseKeyword, "close"),
            (SyntaxKind.ConstKeyword, "const")
        ],
        _ => throw ExceptionUtilities.UnexpectedValue(version)
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
        LanguageVersion.Lua5_4 =>
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
        _ => throw ExceptionUtilities.UnexpectedValue(version)
    };
}
