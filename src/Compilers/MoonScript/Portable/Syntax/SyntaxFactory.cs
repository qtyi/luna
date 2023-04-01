// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;

namespace Qtyi.CodeAnalysis.MoonScript;

using Syntax;

public static partial class SyntaxFactory
{
    #region 标志
    private static partial void ValidateTokenKind(SyntaxKind kind)
    {
        switch (kind)
        {
            case SyntaxKind.IdentifierToken:
                throw new ArgumentException(MoonScriptResources.UseIdentifierForTokens, nameof(kind));

            case SyntaxKind.NumericLiteralToken:
                throw new ArgumentException(MoonScriptResources.UseLiteralForNumeric, nameof(kind));
        }

        if (!SyntaxFacts.IsAnyToken(kind))
            throw new ArgumentException(string.Format(MoonScriptResources.ThisMethodCanOnlyBeUsedToCreateTokens, kind), nameof(kind));
    }

    #region 字面量
    public static partial SyntaxToken Literal(long value) =>
        SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.None), value);

    public static partial SyntaxToken Literal(string text, long value) =>
        new(Syntax.InternalSyntax.SyntaxFactory.Literal(
            SyntaxFactory.ElasticMarker.UnderlyingNode,
            text,
            value,
            SyntaxFactory.ElasticMarker.UnderlyingNode));

    public static partial SyntaxToken Literal(
        SyntaxTriviaList leading,
        string text,
        long value,
        SyntaxTriviaList trailing) =>
        new(Syntax.InternalSyntax.SyntaxFactory.Literal(
            leading.Node,
            text,
            value,
            trailing.Node));

    public static partial SyntaxToken Literal(ulong value) =>
        SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.None), value);

    public static partial SyntaxToken Literal(string text, ulong value) =>
        new(Syntax.InternalSyntax.SyntaxFactory.Literal(
            SyntaxFactory.ElasticMarker.UnderlyingNode,
            text,
            value,
            SyntaxFactory.ElasticMarker.UnderlyingNode));

    public static partial SyntaxToken Literal(
        SyntaxTriviaList leading,
        string text,
        ulong value,
        SyntaxTriviaList trailing) =>
        new(Syntax.InternalSyntax.SyntaxFactory.Literal(
            leading.Node,
            text,
            value,
            trailing.Node));

    public static partial SyntaxToken Literal(double value) =>
        SyntaxFactory.Literal(ObjectDisplay.FormatLiteral(value, ObjectDisplayOptions.None), value);

    public static partial SyntaxToken Literal(string text, double value) =>
        new(Syntax.InternalSyntax.SyntaxFactory.Literal(
            SyntaxFactory.ElasticMarker.UnderlyingNode,
            text,
            value,
            SyntaxFactory.ElasticMarker.UnderlyingNode));

    public static partial SyntaxToken Literal(
        SyntaxTriviaList leading,
        string text,
        double value,
        SyntaxTriviaList trailing) =>
        new(Syntax.InternalSyntax.SyntaxFactory.Literal(
            leading.Node,
            text,
            value,
            trailing.Node));

    public static partial SyntaxToken Literal(string value) =>
        SyntaxFactory.Literal(SymbolDisplay.FormatLiteral(value, quoteStrings: true), value);

    public static partial SyntaxToken Literal(string text, string value) =>
        new(Syntax.InternalSyntax.SyntaxFactory.Literal(
            SyntaxFactory.ElasticMarker.UnderlyingNode,
            text,
            value,
            SyntaxFactory.ElasticMarker.UnderlyingNode));

    public static partial SyntaxToken Literal(
        SyntaxTriviaList leading,
        string text,
        string value,
        SyntaxTriviaList trailing) =>
        new(Syntax.InternalSyntax.SyntaxFactory.Literal(
            leading.Node,
            text,
            value,
            trailing.Node));
    #endregion
    #endregion

    public static ChunkSyntax ParseCompilationUnit(string text, int offset = 0, MoonScriptParseOptions? options = null)
    {
        using var lexer = SyntaxFactory.MakeLexer(text, offset, options);
        using var parser = SyntaxFactory.MakeParser(lexer);
        var node = parser.ParseCompilationUnit();
        return (ChunkSyntax)node.CreateRed();
    }
}
