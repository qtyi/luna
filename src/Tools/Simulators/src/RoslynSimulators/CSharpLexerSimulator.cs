using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Simulators;

[LexerSimulator("CSharp")]
internal class CSharpLexerSimulator : ILexerSimulator
{
    public TokenKind GetTokenKind(SyntaxKind kind)
    {
        if (SyntaxFacts.IsKeywordKind(kind))
            return TokenKind.Keyword;
        else if (SyntaxFacts.IsAnyOverloadableOperator(kind))
            return TokenKind.Operator;
        else if (SyntaxFacts.IsPunctuation(kind))
            return TokenKind.Punctuation;
        else return kind switch
        {
            SyntaxKind.IdentifierToken => TokenKind.Identifier,
            SyntaxKind.NumericLiteralToken => TokenKind.NumericLiteral,
            SyntaxKind.CharacterLiteralToken or
            SyntaxKind.StringLiteralToken or
            SyntaxKind.InterpolatedStringStartToken or
            SyntaxKind.InterpolatedVerbatimStringStartToken or
            SyntaxKind.InterpolatedStringEndToken or
            SyntaxKind.InterpolatedStringTextToken => TokenKind.StringLiteral,
            SyntaxKind.WhitespaceTrivia => TokenKind.WhiteSpace,
            SyntaxKind.EndOfLineTrivia => TokenKind.NewLine,
            SyntaxKind.SingleLineCommentTrivia or
            SyntaxKind.MultiLineCommentTrivia => TokenKind.Comment,
            SyntaxKind.SingleLineDocumentationCommentTrivia or
            SyntaxKind.MultiLineDocumentationCommentTrivia or
            SyntaxKind.DocumentationCommentExteriorTrivia or
            SyntaxKind.EndOfDocumentationCommentToken => TokenKind.Documentation,
            SyntaxKind.SkippedTokensTrivia => TokenKind.Skipped,
            _ => TokenKind.None
        };
    }

    public IEnumerable<SyntaxToken> LexToEnd(SourceText sourceText) => SyntaxFactory.ParseTokens(sourceText.ToString());

    #region ILexerSimulator
    TokenKind ILexerSimulator.GetTokenKind(int rawKind) => this.GetTokenKind((SyntaxKind)rawKind);
    void ISimulator.Initialize(SimulatorContext context) { }
    #endregion
}
