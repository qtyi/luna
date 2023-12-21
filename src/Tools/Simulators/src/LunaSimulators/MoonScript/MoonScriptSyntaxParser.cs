// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Qtyi.CodeAnalysis;
using Qtyi.CodeAnalysis.MoonScript;

namespace Luna.Compilers.Simulators.MoonScript;

[Export(LanguageNames.MoonScript)]
public sealed class MoonScriptSyntaxParser : AbstractSyntaxParser
{
    public override Microsoft.CodeAnalysis.SyntaxTree Parse(SyntaxParserExecutionContext context)
    {
        return SyntaxFactory.ParseSyntaxTree(context.SourceText, MoonScriptParseOptions.Default, context.FilePath, context.CancellationToken);
    }
}
