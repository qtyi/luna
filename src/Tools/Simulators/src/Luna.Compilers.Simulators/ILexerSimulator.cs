using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Simulators;

public interface ILexerSimulator : ISimulator
{
    IEnumerable<SyntaxToken> LexToEnd(SourceText sourceText);

    TokenKind GetTokenKind(int rawKind);
}
