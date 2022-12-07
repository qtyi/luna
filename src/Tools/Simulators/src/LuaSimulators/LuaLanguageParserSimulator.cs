using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Qtyi.CodeAnalysis.Lua;

namespace Luna.Compilers.Simulators;

[LanguageParserSimulator("Lua")]
public sealed class LuaLanguageParserSimulator : ILanguageParserSimulator
{
    public string GetKindText(int rawKind)
    {
        return ((SyntaxKind)rawKind).ToString();
    }

    public SyntaxTree ParseSyntaxTree(SourceText sourceText)
    {
        return SyntaxFactory.ParseSyntaxTree(sourceText, null, "", default);
    }

    void ISimulator.Initialize(SimulatorContext context) { }
}
