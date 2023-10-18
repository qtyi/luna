#if LANG_LUA
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

namespace Qtyi.CodeAnalysis.Lua.Parser.UnitTests.Utilities;
#elif LANG_MOONSCRIPT
using Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

namespace Qtyi.CodeAnalysis.MoonScript.Parser.UnitTests.Utilities;
#endif

public static class LexerTestUtilities
{
    #region IsMissing
    internal static void IsMissing(this Assert assert, SyntaxToken token) => Assert.IsTrue(token.IsMissing);

    internal static void IsNotMissing(this Assert assert, SyntaxToken token) => Assert.IsFalse(token.IsMissing);
    #endregion

    #region IsPunctuation
    internal static void IsPunctuation(this Assert assert, SyntaxToken token, string? punctuation = null) =>
        Assert.IsTrue(LexerTestUtilities.IsPunctuationCore(token, punctuation));

    internal static void IsPunctuation(this Assert assert, SyntaxToken token, string message, string? punctuation = null) =>
        Assert.IsTrue(LexerTestUtilities.IsPunctuationCore(token, punctuation), message);

    internal static void IsPunctuation(this Assert assert, SyntaxToken token, string message, string? punctuation = null, params object[] parameters) =>
        Assert.IsTrue(LexerTestUtilities.IsPunctuationCore(token, punctuation), message, parameters);

    internal static bool IsPunctuationCore(SyntaxToken token, string? punctuation = null) =>
        SyntaxFacts.IsPunctuation(token.Kind) &&
            punctuation is null ? true : token.ValueText == punctuation;
    #endregion

    #region IsKeyword
    internal static void IsKeyword(this Assert assert, SyntaxToken token, string? keywordName = null) =>
        Assert.IsTrue(LexerTestUtilities.IsKeywordCore(token, keywordName));

    internal static void IsKeyword(this Assert assert, SyntaxToken token, string message, string? keywordName = null) =>
        Assert.IsTrue(LexerTestUtilities.IsKeywordCore(token, keywordName), message);

    internal static void IsKeyword(this Assert assert, SyntaxToken token, string message, string? keywordName = null, params object[] parameters) =>
        Assert.IsTrue(LexerTestUtilities.IsKeywordCore(token, keywordName), message, parameters);

    internal static bool IsKeywordCore(SyntaxToken token, string? keywordName = null) =>
        SyntaxFacts.IsKeywordKind(token.Kind) &&
            keywordName is null ? true : token.ValueText == keywordName;
    #endregion

    #region IsIdentifier
    internal static void IsIdentifier(this Assert assert, SyntaxToken token, string? identifierName = null) =>
        Assert.IsTrue(LexerTestUtilities.IsIdentifierCore(token, identifierName));

    internal static void IsIdentifier(this Assert assert, SyntaxToken token, string message, string? identifierName = null) =>
        Assert.IsTrue(LexerTestUtilities.IsIdentifierCore(token, identifierName), message);

    internal static void IsIdentifier(this Assert assert, SyntaxToken token, string message, string? identifierName = null, params object[] parameters) =>
        Assert.IsTrue(LexerTestUtilities.IsIdentifierCore(token, identifierName), message, parameters);

    internal static bool IsIdentifierCore(SyntaxToken token, string? identifierName = null) =>
        token.Kind == SyntaxKind.IdentifierToken &&
            identifierName is null ? true : token.ValueText == identifierName && SyntaxFacts.IsValidIdentifier(identifierName);
    #endregion

    #region IsLiteral
    internal static void IsLiteral(this Assert assert, SyntaxToken token) =>
        LexerTestUtilities.IsLiteralCore(token);

    internal static void IsLiteral<T>(this Assert assert, SyntaxToken token, T? value) =>
        LexerTestUtilities.IsLiteralCore(token, value);

    internal static void IsLiteralCore(SyntaxToken token, [CallerMemberName] string? memberName = null)
    {
        if (!SyntaxFacts.IsLiteral(token.Kind))
            Assert.That.Raise<UnexpectedSyntaxKindException<SyntaxKind>>(UnexpectedSyntaxKindException<SyntaxKind>.MakeMessage(expected: null, actual: token.Kind), memberName: memberName);
    }

    internal static void IsLiteralCore<T>(SyntaxToken token, T? value, [CallerMemberName] string? memberName = null)
    {
        LexerTestUtilities.IsLiteralCore(token, memberName: memberName);
        try
        {
            Assert.AreEqual(value, token.Value);
        }
        catch (AssertFailedException ex)
        {
            Assert.That.Raise(ex, memberName: memberName);
        }
    }
    #endregion

    #region IsEndOfFile
    internal static void IsEndOfFile(this Assert assert, SyntaxToken token) =>
        Assert.IsTrue(LexerTestUtilities.IsEndOfFileCore(token));

    internal static void IsEndOfFile(this Assert assert, SyntaxToken token, string message) =>
        Assert.IsTrue(LexerTestUtilities.IsEndOfFileCore(token), message);

    internal static void IsEndOfFile(this Assert assert, SyntaxToken token, string message, params object[] parameters) =>
        Assert.IsTrue(LexerTestUtilities.IsEndOfFileCore(token), message, parameters);

    internal static bool IsEndOfFileCore(SyntaxToken token) =>
        token.Kind == SyntaxKind.EndOfFileToken;
    #endregion
}
