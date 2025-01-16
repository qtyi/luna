// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Text;
using Xunit.Abstractions;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

using ThisParseOptions = LuaParseOptions;
using ThisTestBase = Test.Utilities.LuaTestBase;
using ThisInternalSyntaxNode = Syntax.InternalSyntax.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.UnitTests.Parsing;

using ThisParseOptions = MoonScriptParseOptions;
using ThisTestBase = Test.Utilities.MoonScriptTestBase;
using ThisInternalSyntaxNode = Syntax.InternalSyntax.MoonScriptSyntaxNode;
#endif

using Test.Utilities;
using Microsoft.CodeAnalysis.Test.Utilities;
using Microsoft.CodeAnalysis;

public abstract partial class ParsingTestBase : ThisTestBase
{
    protected virtual ITestOutputHelper? Output => null;

    protected static NodeValidator ParseSource(
        string source,
        ThisParseOptions? options = null,
        params DiagnosticDescription[] expectedErrors) => ParseSource(SourceText.From(source), options, expectedErrors);

    private static NodeValidator ParseSource(
        SourceText sourceText,
        ThisParseOptions? options = null,
        params DiagnosticDescription[] expectedErrors)
    {
        var tree = SyntaxFactory.ParseSyntaxTree(sourceText, options ?? TestOptions.RegularDefault, path: "");
        tree.GetDiagnostics().Verify(expectedErrors);
        var validator = new ValidatorImpl((ThisInternalSyntaxNode)tree.GetRoot().Green, withNode: true);
        return validator.NextNode;
    }

    protected void Print(string source, ThisParseOptions? options = null) => Print(SourceText.From(source), options);

    protected void Print(SourceText sourceText, ThisParseOptions? options = null)
    {
        var output = Output;
        if (output is null) return;

        var tree = SyntaxFactory.ParseSyntaxTree(sourceText, options ?? TestOptions.RegularDefault, path: "");
        var validator = new ValidatorImpl((ThisInternalSyntaxNode)tree.GetRoot().Green, withNode: true, output: output);
        while (validator.TryWriteNextValidation()) { }
    }
}
