﻿// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Xml.Linq;
using Luna.Test.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;

public partial class FileTests(ITestOutputHelper output) : LexingTestBase
{
    protected override ITestOutputHelper? Output { get; } = output;

    public static readonly TheoryData<string, SourceCodeKind> FileNames =
        new TheoryData<string>(TestResources.LuaTestFiles.GetAllFileNames())
        .Combine(new EnumTheoryData<SourceCodeKind>());

    [Theory]
    [MemberData(nameof(FileNames))]
    public void TestOfficialTestFilesWithTrivia(string name, SourceCodeKind kind)
    {
        var source = SourceText.From(TestResources.LuaTestFiles.GetSource(name));
        Print(source, options: TestOptions.RegularDefault.WithKind(kind), withTrivia: true);
        switch (name)
        {

            default:
                throw new XunitException($"Test file '{name}' not tested");
        }
    }
}
