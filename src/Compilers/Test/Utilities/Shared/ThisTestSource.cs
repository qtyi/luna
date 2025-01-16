// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Test.Utilities;

using ThisParseOptions = LuaParseOptions;
using ThisTestBase = LuaTestBase;
using ThisTestSource = LuaTestSource;
using ThisTestSourceBuilder = LuaTestSource.LuaTestSourceBuilder;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Test.Utilities;

using ThisParseOptions = MoonScriptParseOptions;
using ThisTestBase = MoonScriptTestBase;
using ThisTestSource = MoonScriptTestSource;
using ThisTestSourceBuilder = MoonScriptTestSource.MoonScriptTestSourceBuilder;
#endif

[System.Runtime.CompilerServices.CollectionBuilder(typeof(ThisTestSourceBuilder), nameof(ThisTestSourceBuilder.Create))]
public readonly partial struct
#if LANG_LUA
    LuaTestSource
#elif LANG_MOONSCRIPT
    MoonScriptTestSource
#endif
    : IEnumerable<ThisTestSource>
{
    /// <summary>
    /// An instance that represents no test source used.
    /// </summary>
    public static ThisTestSource None => new(null);

    /// <summary>
    /// Content of this text source.
    /// </summary>
    /// <value>
    /// <list type="table">
    ///   <listheader>
    ///     <term>Type</term>
    ///     <description>Description</description>
    ///   </listheader>
    ///   <item>
    ///     <term><see cref="string"/></term>
    ///     <description>Single text of source file.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="string"/>[]</term>
    ///     <description>An array of texts of source files</description>
    ///   </item>
    ///   <item>
    ///     <term>(<see cref="string"/> Source, <see cref="string"/> FileName)</term>
    ///     <description>Single tuple of text and file name of source file.</description>
    ///   </item>
    ///   <item>
    ///     <term>(<see cref="string"/> Source, <see cref="string"/> FileName)[]</term>
    ///     <description>An array of tuples of text and file name of source files.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="SyntaxTree"/></term>
    ///     <description>Single syntax tree of source file.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="SyntaxTree"/>[]</term>
    ///     <description>An array of syntax trees of source files.</description>
    ///   </item>
    ///   <item>
    ///     <term><see cref="ThisTestSource"/>[]</term>
    ///     <description>An array of test sources.</description>
    ///   </item>
    ///   <item>
    ///     <term><see langword="null"/></term>
    ///     <description>No test source.</description>
    ///   </item>
    /// </list>
    /// </value>
    public object? Value { get; }

    private
#if LANG_LUA
        LuaTestSource
#elif LANG_MOONSCRIPT
        MoonScriptTestSource
#endif
        (object? value) => Value = value;

    /// <summary>
    /// Source text parsing method for test sources
    /// </summary>
    /// <param name="text">Content of source text.</param>
    /// <param name="path">File path of source text.</param>
    /// <param name="options">Parse options for parsing. <see cref="TestOptions.RegularPreview"/> if not specified.</param>
    /// <param name="encoding">Encoding of the file that the <paramref name="text"/> was read from or is going to be saved to. <see cref="Encoding.UTF8"/> if not specified.</param>
    /// <param name="checksumAlgorithm">Hash algorithm to use to calculate checksum of the text that's saved to PDB. <see cref="SourceHashAlgorithms.Default"/> if not specified.</param>
    /// <returns></returns>
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
        return tree;
    }

    /// <summary>
    /// Get an array of <see cref="SyntaxTree"/>s from this test source with specified parse options and source file name.
    /// </summary>
    /// <param name="parseOptions">The parse options to parse source text.</param>
    /// <param name="sourceFileName">The name of source file for parsing.</param>
    /// <returns><see cref="SyntaxTree"/>s this test source contains, or parse source texts with specified parse options and source file name.</returns>
    public SyntaxTree[] GetSyntaxTrees(ThisParseOptions parseOptions, string sourceFileName = "")
    {
        switch (Value)
        {
            case string source:
                return [Parse(source, path: sourceFileName, parseOptions)];
            case string[] sources:
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return ThisTestBase.Parse(parseOptions, sources);
            case (string source, string fileName):
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return [ThisTestBase.Parse(source, fileName, parseOptions)];
            case (string Source, string FileName)[] sources:
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return sources.Select(source => Parse(source.Source, source.FileName, parseOptions)).ToArray();
            case SyntaxTree tree:
                Debug.Assert(parseOptions is null);
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return [tree];
            case SyntaxTree[] trees:
                Debug.Assert(parseOptions is null);
                Debug.Assert(string.IsNullOrEmpty(sourceFileName));
                return trees;
            case ThisTestSource[] testSources:
                return testSources.SelectMany(s => s.GetSyntaxTrees(parseOptions, sourceFileName)).ToArray();
            case null:
                return [];
            default:
                throw ExceptionUtilities.UnexpectedValue(Value);
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

    // Dummy IEnumerable support to satisfy the collection expression and CollectionBuilder requirements
    IEnumerator<ThisTestSource> IEnumerable<ThisTestSource>.GetEnumerator() => throw new NotImplementedException();
    IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();

    /// <summary>
    /// Provides helper method to support creating a <see cref="ThisTestSource"/> via collection expression.
    /// </summary>
    internal static class
#if LANG_LUA
        LuaTestSourceBuilder
#elif LANG_MOONSCRIPT
        MoonScriptTestSourceBuilder
#endif
    {
        /// <summary>
        /// Helper method to support creating a <see cref="ThisTestSource"/> via collection expression.
        /// </summary>
        /// <param name="source">A collection of test sources that combine into a single test source.</param>
        /// <returns>The test source that <paramref name="source"/> combine into.</returns>
        public static ThisTestSource Create(ReadOnlySpan<ThisTestSource> source) => source.ToArray();
    }
}
