// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Qtyi.CodeAnalysis.Lua.Test.Utilities;
using Xunit;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Qtyi.CodeAnalysis.Lua.UnitTests.Lexing;

public partial class FileTests : LexingTestBase
{
    protected override ITestOutputHelper? Output { get; }

    public FileTests(ITestOutputHelper output)
    {
        Output = output;
    }

    public static IEnumerable<object[]> TestSources { get; } =
        TestResources.LuaTestFiles.GetAllFiles().SelectMany(static file => new object[][]
        {
            new object[] { file.name, file.source, SourceCodeKind.Regular },
            new object[] { file.name, file.source, SourceCodeKind.Script }
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

    [Theory]
    [MemberData(nameof(TestSources))]
    public void TestOfficialTestFilesWithTrivia(string name, string source, SourceCodeKind kind)
    {
        Print(source, options: TestOptions.RegularDefault.WithKind(kind), withTrivia: true);
        switch (name)
        {
            case "all.lua":
                TestOfficialTestFileWithTrivia_all(source, kind); break;
            case "api.lua":
                TestOfficialTestFileWithTrivia_api(source, kind); break;
            case "attrib.lua":
                TestOfficialTestFileWithTrivia_attrib(source, kind); break;
            case "big.lua":
                TestOfficialTestFileWithTrivia_big(source, kind); break;
            case "bitwise.lua":
                TestOfficialTestFileWithTrivia_bitwise(source, kind); break;
            case "bwcoercion.lua":
                TestOfficialTestFileWithTrivia_bwcoercion(source, kind); break;
            case "calls.lua":
                TestOfficialTestFileWithTrivia_calls(source, kind); break;
            case "closure.lua":
                TestOfficialTestFileWithTrivia_closure(source, kind); break;
            case "code.lua":
                TestOfficialTestFileWithTrivia_code(source, kind); break;
            case "constructs.lua":
                TestOfficialTestFileWithTrivia_constructs(source, kind); break;
            case "coroutine.lua":
                TestOfficialTestFileWithTrivia_coroutine(source, kind); break;
            case "cstack.lua":
                TestOfficialTestFileWithTrivia_cstack(source, kind); break;
            case "db.lua":
                TestOfficialTestFileWithTrivia_db(source, kind); break;
            case "errors.lua":
                TestOfficialTestFileWithTrivia_errors(source, kind); break;
            case "events.lua":
                TestOfficialTestFileWithTrivia_events(source, kind); break;
            case "files.lua":
                TestOfficialTestFileWithTrivia_files(source, kind); break;
            case "gc.lua":
                TestOfficialTestFileWithTrivia_gc(source, kind); break;
            case "gengc.lua":
                TestOfficialTestFileWithTrivia_gengc(source, kind); break;
            case "goto.lua":
                TestOfficialTestFileWithTrivia_goto(source, kind); break;
            case "heavy.lua":
                TestOfficialTestFileWithTrivia_heavy(source, kind); break;
            case "literals.lua":
                TestOfficialTestFileWithTrivia_literals(source, kind); break;
            case "locals.lua":
                TestOfficialTestFileWithTrivia_locals(source, kind); break;
            case "main.lua":
                TestOfficialTestFileWithTrivia_main(source, kind); break;
            case "math.lua":
                TestOfficialTestFileWithTrivia_math(source, kind); break;
            case "nextvar.lua":
                TestOfficialTestFileWithTrivia_nextvar(source, kind); break;
            case "pm.lua":
                TestOfficialTestFileWithTrivia_pm(source, kind); break;
            case "sort.lua":
                TestOfficialTestFileWithTrivia_sort(source, kind); break;
            case "strings.lua":
                TestOfficialTestFileWithTrivia_strings(source, kind); break;
            case "tpack.lua":
                TestOfficialTestFileWithTrivia_tpack(source, kind); break;
            case "tracegc.lua":
                TestOfficialTestFileWithTrivia_tracegc(source, kind); break;
            case "utf8.lua":
                TestOfficialTestFileWithTrivia_utf8(source, kind); break;
            case "vararg.lua":
                TestOfficialTestFileWithTrivia_vararg(source, kind); break;
            case "verybig.lua":
                TestOfficialTestFileWithTrivia_verybig(source, kind); break;

            default:
                throw new XunitException($"Test file '{name}' not tested");
        }
    }
}
