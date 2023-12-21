// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Qtyi.CodeAnalysis.MoonScript.Test.Utilities;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Qtyi.CodeAnalysis.MoonScript.UnitTests.Lexing;

public class FileTests : LexingTestBase
{
    protected override ITestOutputHelper? Output { get; }

    public FileTests(ITestOutputHelper output)
    {
        this.Output = output;
    }

    public static IEnumerable<object[]> TestSources { get; } =
        TestResources.MoonScriptTestFiles.GetAllFiles().SelectMany(static file => new object[][]
        {
            new object[] { file.inputName, file.inputSource, SourceCodeKind.Regular },
            new object[] { file.inputName, file.inputSource, SourceCodeKind.Script },
        });

    [Theory]
    [MemberData(nameof(TestSources))]
    public void TestOfficialTestFiles(string name, string source, SourceCodeKind kind)
    {
        Print(source, options: TestOptions.RegularDefault.WithKind(kind));
        switch (name)
        {
            default:
                throw new XunitException($"Test file '{name}' not tested");
        }
    }

    [Theory]
    [MemberData(nameof(TestSources))]
    public void TestOfficialTestFilesWithTrivia(string name, string source, SourceCodeKind kind)
    {
        Print(source, options: TestOptions.RegularDefault.WithKind(kind), withTrivia: true);
        switch (name)
        {
            default:
                throw new XunitException($"Test file '{name}' not tested");
        }
    }
}
