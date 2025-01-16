// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Parsing;

public partial class FileTests : ParsingTestBase
{
    protected override ITestOutputHelper? Output { get; }

    public FileTests(ITestOutputHelper output)
    {
        Output = output;
    }

    public static IEnumerable<object[]> TestSources { get; } =
        TestResources.LuaTestFiles.GetAllFiles().SelectMany(static file => new object[][]
        {
            [file.name, file.source, SourceCodeKind.Regular],
            [file.name, file.source, SourceCodeKind.Script]
        });

    [Theory]
    [MemberData(nameof(TestSources))]
    public void TestOfficialTestFiles(string name, string source, SourceCodeKind kind)
    {
        Print(source, options: TestOptions.RegularDefault.WithKind(kind));
        switch (name)
        {
            case "all.lua":
                TestOfficialTestFile_all(source, kind); break;
            case "api.lua":
                TestOfficialTestFile_api(source, kind); break;
            case "attrib.lua":
                TestOfficialTestFile_attrib(source, kind); break;
            case "big.lua":
                TestOfficialTestFile_big(source, kind); break;
            case "bitwise.lua":
                TestOfficialTestFile_bitwise(source, kind); break;
            case "bwcoercion.lua":
                TestOfficialTestFile_bwcoercion(source, kind); break;
            case "calls.lua":
                TestOfficialTestFile_calls(source, kind); break;
            case "closure.lua":
                TestOfficialTestFile_closure(source, kind); break;
            case "code.lua":
                TestOfficialTestFile_code(source, kind); break;
            case "constructs.lua":
                TestOfficialTestFile_constructs(source, kind); break;
            case "coroutine.lua":
                TestOfficialTestFile_coroutine(source, kind); break;
            case "cstack.lua":
                TestOfficialTestFile_cstack(source, kind); break;
            case "db.lua":
                TestOfficialTestFile_db(source, kind); break;
            case "errors.lua":
                TestOfficialTestFile_errors(source, kind); break;
            case "events.lua":
                TestOfficialTestFile_events(source, kind); break;
            case "files.lua":
                TestOfficialTestFile_files(source, kind); break;
            case "gc.lua":
                TestOfficialTestFile_gc(source, kind); break;
            case "gengc.lua":
                TestOfficialTestFile_gengc(source, kind); break;
            case "goto.lua":
                TestOfficialTestFile_goto(source, kind); break;
            case "heavy.lua":
                TestOfficialTestFile_heavy(source, kind); break;
            case "literals.lua":
                TestOfficialTestFile_literals(source, kind); break;
            case "locals.lua":
                TestOfficialTestFile_locals(source, kind); break;
            case "main.lua":
                TestOfficialTestFile_main(source, kind); break;
            case "math.lua":
                TestOfficialTestFile_math(source, kind); break;
            case "nextvar.lua":
                TestOfficialTestFile_nextvar(source, kind); break;
            case "pm.lua":
                TestOfficialTestFile_pm(source, kind); break;
            case "sort.lua":
                TestOfficialTestFile_sort(source, kind); break;
            case "strings.lua":
                TestOfficialTestFile_strings(source, kind); break;
            case "tpack.lua":
                TestOfficialTestFile_tpack(source, kind); break;
            case "tracegc.lua":
                TestOfficialTestFile_tracegc(source, kind); break;
            case "utf8.lua":
                TestOfficialTestFile_utf8(source, kind); break;
            case "vararg.lua":
                TestOfficialTestFile_vararg(source, kind); break;
            case "verybig.lua":
                TestOfficialTestFile_verybig(source, kind); break;

            default:
                throw new XunitException($"Test file '{name}' not tested");
        }
    }
}
