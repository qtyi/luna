// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Qtyi.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua;

namespace Luna.Compilers.Simulators.Lua;

[Export(LanguageNames.Lua)]
public sealed class LuaSyntaxParser : AbstractSyntaxParser
{
    public override Microsoft.CodeAnalysis.SyntaxTree Parse(SyntaxParserExecutionContext context)
    {
        return SyntaxFactory.ParseSyntaxTree(context.SourceText, LuaParseOptions.Default, context.FilePath, context.CancellationToken);
    }
}
