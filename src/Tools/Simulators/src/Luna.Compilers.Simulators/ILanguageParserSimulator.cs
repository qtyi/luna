using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Simulators;

public interface ILanguageParserSimulator : ISimulator
{
    SyntaxTree ParseSyntaxTree(SourceText sourceText);

    string GetKindText(int rawKind);
}
