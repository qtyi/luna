using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Qtyi.CodeAnalysis.Lua;
using Lua = Qtyi.CodeAnalysis.Lua;

namespace Luna.Compilers.Simulators;

[LexerSimulator("Lua")]
public sealed class LuaLexerSimulator : ILexerSimulator
{
    public IEnumerable<SyntaxToken> LexToEnd(SourceText sourceText)
    {
        return Lua.SyntaxFactory.ParseTokens(sourceText.ToString());
    }

    private TokenKind GetTokenKind(SyntaxKind kind)
    {
        if (SyntaxFacts.IsKeywordKind(kind)) return TokenKind.Keyword;
        else if (SyntaxFacts.IsUnaryExpressionOperatorToken(kind) || SyntaxFacts.IsBinaryExpressionOperatorToken(kind)) return TokenKind.Operator;
        else if (SyntaxFacts.IsPunctuation(kind)) return TokenKind.Punctuation;
        else return kind switch
        {
            SyntaxKind.IdentifierToken => TokenKind.Identifier,
            SyntaxKind.NumericLiteralToken => TokenKind.NumericLiteral,
            SyntaxKind.StringLiteralToken or
            SyntaxKind.MultiLineRawStringLiteralToken => TokenKind.StringLiteral,
            SyntaxKind.WhiteSpaceTrivia => TokenKind.WhiteSpace,
            SyntaxKind.EndOfLineTrivia => TokenKind.NewLine,
            SyntaxKind.SingleLineCommentTrivia or
            SyntaxKind.MultiLineCommentTrivia => TokenKind.Comment,
            SyntaxKind.SkippedTokensTrivia => TokenKind.Skipped,
            _ => TokenKind.None
        };
    }

    TokenKind ILexerSimulator.GetTokenKind(int rawKind) => this.GetTokenKind((SyntaxKind)rawKind);

    void ISimulator.Initialize(SimulatorContext context) { }
}
