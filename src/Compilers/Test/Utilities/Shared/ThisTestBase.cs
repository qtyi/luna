// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeGen;
using Microsoft.CodeAnalysis.Test.Utilities;
using Microsoft.CodeAnalysis.Text;
using Xunit;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Test.Utilities;

using ThisParseOptions = LuaParseOptions;
using ThisTestSource = LuaTestSource;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Test.Utilities;

using ThisParseOptions = MoonScriptParseOptions;
using ThisTestSource = MoonScriptTestSource;
#endif

public abstract partial class
#if LANG_LUA
    LuaTestBase
#elif LANG_MOONSCRIPT
    MoonScriptTestBase
#endif
    : CommonTestBase
{
    #region SyntaxTree Factories
    public static SyntaxTree Parse(
        string text,
        string filename = "",
        ThisParseOptions? options = null,
        Encoding? encoding = null,
        SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithm.Sha1)
        => ThisTestSource.Parse(text, filename, options, encoding, checksumAlgorithm);

    public static SyntaxTree[] Parse(
        IEnumerable<string> sources,
        ThisParseOptions? options = null)
    {
        Debug.Assert(sources is not null);

        if (!sources.Any())
            return Array.Empty<SyntaxTree>();

        return Parse(options, sources.ToArray());
    }

    public static SyntaxTree[] Parse(
        ThisParseOptions? options = null,
        params string[] sources)
    {
        Debug.Assert(sources is not null && sources.All(static src => src is not null));

        if (sources.Length == 0)
            return Array.Empty<SyntaxTree>();

        return sources.Select((src, index) => Parse(src, filename: $"{index}.cs", options: options)).ToArray();
    }

    public static SyntaxTree ParseWithRoundTripCheck(
        string text,
        ThisParseOptions? options = null)
    {
        var tree = Parse(text, options: options ?? TestOptions.RegularPreview);
        var parsedText = tree.GetRoot();
        // we validate the text roundtrips
        Assert.Equal(text, parsedText.ToFullString());
        return tree;
    }
    #endregion

    #region IL Validation
    internal override string VisualizeRealIL(IModuleSymbol peNetmodule, CompilationTestData.MethodData methodData, IReadOnlyDictionary<int, string> markers, bool areLocalsZeroed)
    {
#warning Not implemented.
        throw new NotImplementedException();
    }
    #endregion

    #region Diagnostics
    internal static DiagnosticDescription Diagnostic(
        ErrorCode code,
        string? squiggledText = null,
        object[]? arguments = null,
        LinePosition? startLocation = null,
        Func<SyntaxNode, bool>? syntaxNodePredicate = null,
        bool argumentOrderDoesNotMatter = false,
        bool isSuppressed = false)
    {
        return new(code: (int)code,
                   isWarningAsError: false,
                   squiggledText: squiggledText,
                   arguments: arguments,
                   startLocation: startLocation,
                   syntaxNodePredicate: syntaxNodePredicate,
                   argumentOrderDoesNotMatter: argumentOrderDoesNotMatter,
                   errorCodeType: typeof(ErrorCode),
                   isSuppressed: isSuppressed);
    }
    #endregion
}
