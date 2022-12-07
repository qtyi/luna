using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Simulators;

[LexerSimulator("VisualBasic")]
internal class VisualBasicSimulator : ILexerSimulator
{
    public TokenKind GetTokenKind(SyntaxKind kind)
    {
        if (SyntaxFacts.IsKeywordKind(kind))
            return TokenKind.Keyword;
        else if (SyntaxFacts.IsOperator(kind))
            return TokenKind.Operator;
        else if (SyntaxFacts.IsPunctuation(kind))
            return TokenKind.Punctuation;
        else return kind switch
        {
            SyntaxKind.IdentifierToken => TokenKind.Identifier,
            SyntaxKind.DateLiteralToken or
            SyntaxKind.DecimalLiteralToken or
            SyntaxKind.FloatingLiteralToken or
            SyntaxKind.IntegerLiteralToken or
            SyntaxKind.NumericLiteralExpression => TokenKind.NumericLiteral,
            SyntaxKind.CharacterLiteralToken or
            SyntaxKind.StringLiteralToken or
            SyntaxKind.InterpolatedStringTextToken => TokenKind.StringLiteral,
            SyntaxKind.WhitespaceTrivia => TokenKind.WhiteSpace,
            SyntaxKind.EndOfLineTrivia => TokenKind.NewLine,
            SyntaxKind.CommentTrivia => TokenKind.Comment,
            SyntaxKind.DocumentationCommentTrivia or
            SyntaxKind.DocumentationCommentExteriorTrivia or
            SyntaxKind.DocumentationCommentLineBreakToken or
            SyntaxKind.XmlDocument => TokenKind.Documentation,
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
