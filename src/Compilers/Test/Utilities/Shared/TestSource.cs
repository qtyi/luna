// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Test.Utilities;

using ThisParseOptions = LuaParseOptions;
using ThisSyntaxNode = LuaSyntaxNode;
using ThisTestBase = LuaTestBase;
using ThisTestSource = LuaTestSource;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Test.Utilities;

using ThisParseOptions = MoonScriptParseOptions;
using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisTestBase = MoonScriptTestBase;
using ThisTestSource = MoonScriptTestSource;
#endif

public readonly struct
#if LANG_LUA
    LuaTestSource
#elif LANG_MOONSCRIPT
    MoonScriptTestSource
#endif
{
    public static ThisTestSource None => new(null);

    public object? Value { get; }

    private
#if LANG_LUA
        LuaTestSource
#elif LANG_MOONSCRIPT
        MoonScriptTestSource
#endif
        (object? value) => this.Value = value;

    public static SyntaxTree Parse(
        string text,
        string path = "",
        ThisParseOptions? options = null,
        Encoding? encoding = null,
        SourceHashAlgorithm checksumAlgorithm = SourceHashAlgorithms.Default)
    {
        var stringText = SourceText.From(text, encoding ?? Encoding.UTF8, checksumAlgorithm);
        var tree = SyntaxFactory.ParseSyntaxTree(
            stringText,
            options ?? TestOptions.RegularPreview,
            path);
        CheckSerializable(tree);
        return tree;
    }

    private static void CheckSerializable(SyntaxTree tree)
    {
        using var stream = new MemoryStream();
        var root = tree.GetRoot();
        root.SerializeTo(stream);
        stream.Position = 0;

        // verify absence of exception:
        _ = ThisSyntaxNode.DeserializeFrom(stream);
    }

    public SyntaxTree[] GetSyntaxTrees(ThisParseOptions parseOptions, string sourceFileName = "")
    {
        switch (this.Value)
        {
            case string source:
                return new[] { Parse(source, path: sourceFileName, parseOptions) };
            case string[] sources:
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return ThisTestBase.Parse(parseOptions, sources);
            case (string source, string fileName):
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return new[] { ThisTestBase.Parse(source, fileName, parseOptions) };
            case (string Source, string FileName)[] sources:
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return sources.Select(source => Parse(source.Source, source.FileName, parseOptions)).ToArray();
            case SyntaxTree tree:
                Debug.Assert(parseOptions == null);
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return new[] { tree };
            case SyntaxTree[] trees:
                Debug.Assert(parseOptions == null);
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return trees;
            case ThisTestSource[] testSources:
                return testSources.SelectMany(s => s.GetSyntaxTrees(parseOptions, sourceFileName)).ToArray();
            case null:
                return Array.Empty<SyntaxTree>();
            default:
                throw ExceptionUtilities.UnexpectedValue(this.Value);
        }
    }

    public static implicit operator ThisTestSource(string source) => new(source);
    public static implicit operator ThisTestSource(string[] source) => new(source);
    public static implicit operator ThisTestSource((string Source, string FileName) source) => new(source);
    public static implicit operator ThisTestSource((string Source, string FileName)[] source) => new(source);
    public static implicit operator ThisTestSource(SyntaxTree source) => new(source);
    public static implicit operator ThisTestSource(SyntaxTree[] source) => new(source);
    public static implicit operator ThisTestSource(List<SyntaxTree> source) => new(source.ToArray());
    public static implicit operator ThisTestSource(ImmutableArray<SyntaxTree> source) => new(source.ToArray());
    public static implicit operator ThisTestSource(ThisTestSource[] source) => new(source);
}
