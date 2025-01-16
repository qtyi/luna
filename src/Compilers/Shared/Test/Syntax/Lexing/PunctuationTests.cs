// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis;
using Xunit;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;

using ICSharpCode.Decompiler.Metadata;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests.Lexing;
#endif

using Syntax.InternalSyntax;
using Test.Utilities;
using Xunit.Abstractions;

public partial class PunctuationTests : LexingTestBase
{
    protected override ITestOutputHelper? Output { get; }

    public PunctuationTests(ITestOutputHelper output)
    {
        Output = output;
    }

    [Theory]
    [MemberData(nameof(EffectiveLanguageVersionsAndSourceCodeKind))]
    public void LexPunctuation(
        LanguageVersion version,
        SourceCodeKind parseKind)
    {
        var parseOptions = TestOptions.Regular.WithLanguageVersion(version);
        foreach (var info in SyntaxFactsTests.GetExpectedSyntaxKindInfos_Punctuation(version))
        {
            Print(info.Text, options: parseOptions, withTrivia: true);
            var V = LexSource(info.Text, options: parseOptions, withTrivia: true).EndOfFile();
            if (info.Kind == SyntaxKind.HashExclamationToken)
            {
                if (parseKind == SourceCodeKind.Regular)
                {
                    V(kind: SyntaxKind.BadDirectiveTrivia, text: info.Text, location: TriviaLocation.Leading);
                    V(SyntaxKind.HashExclamationToken);
                    V(SyntaxKind.EndOfDirectiveToken);
                }
                else if (parseKind == SourceCodeKind.Script)
                {
                    V(kind: SyntaxKind.ShebangDirectiveTrivia, text: info.Text, location: TriviaLocation.Leading);
                    V(SyntaxKind.HashExclamationToken);
                    V(SyntaxKind.EndOfDirectiveToken);
                }
            }
            else
                V(kind: info.Kind, text: info.Text);
        }
    }
}
