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
        this.Output = output;
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
                this.TestOfficialTestFile_all(source, kind); break;
            case "api.lua":
                this.TestOfficialTestFile_api(source, kind); break;
            case "attrib.lua":
                this.TestOfficialTestFile_attrib(source, kind); break;
            case "big.lua":
                this.TestOfficialTestFile_big(source, kind); break;
            case "bitwise.lua":
                this.TestOfficialTestFile_bitwise(source, kind); break;
            case "bwcoercion.lua":
                this.TestOfficialTestFile_bwcoercion(source, kind); break;
            case "calls.lua":
                this.TestOfficialTestFile_calls(source, kind); break;
            case "closure.lua":
                this.TestOfficialTestFile_closure(source, kind); break;
            case "code.lua":
                this.TestOfficialTestFile_code(source, kind); break;
            case "constructs.lua":
                this.TestOfficialTestFile_constructs(source, kind); break;
            case "coroutine.lua":
                this.TestOfficialTestFile_coroutine(source, kind); break;
            case "cstack.lua":
                this.TestOfficialTestFile_cstack(source, kind); break;
            case "db.lua":
                this.TestOfficialTestFile_db(source, kind); break;
            case "errors.lua":
                this.TestOfficialTestFile_errors(source, kind); break;
            case "events.lua":
                this.TestOfficialTestFile_events(source, kind); break;
            case "files.lua":
                this.TestOfficialTestFile_files(source, kind); break;
            case "gc.lua":
                this.TestOfficialTestFile_gc(source, kind); break;
            case "gengc.lua":
                this.TestOfficialTestFile_gengc(source, kind); break;
            case "goto.lua":
                this.TestOfficialTestFile_goto(source, kind); break;
            case "heavy.lua":
                this.TestOfficialTestFile_heavy(source, kind); break;
            case "literals.lua":
                this.TestOfficialTestFile_literals(source, kind); break;
            case "locals.lua":
                this.TestOfficialTestFile_locals(source, kind); break;
            case "main.lua":
                this.TestOfficialTestFile_main(source, kind); break;
            case "math.lua":
                this.TestOfficialTestFile_math(source, kind); break;
            case "nextvar.lua":
                this.TestOfficialTestFile_nextvar(source, kind); break;
            case "pm.lua":
                this.TestOfficialTestFile_pm(source, kind); break;
            case "sort.lua":
                this.TestOfficialTestFile_sort(source, kind); break;
            case "strings.lua":
                this.TestOfficialTestFile_strings(source, kind); break;
            case "tpack.lua":
                this.TestOfficialTestFile_tpack(source, kind); break;
            case "tracegc.lua":
                this.TestOfficialTestFile_tracegc(source, kind); break;
            case "utf8.lua":
                this.TestOfficialTestFile_utf8(source, kind); break;
            case "vararg.lua":
                this.TestOfficialTestFile_vararg(source, kind); break;
            case "verybig.lua":
                this.TestOfficialTestFile_verybig(source, kind); break;

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
                this.TestOfficialTestFileWithTrivia_all(source, kind); break;
            case "api.lua":
                this.TestOfficialTestFileWithTrivia_api(source, kind); break;
            case "attrib.lua":
                this.TestOfficialTestFileWithTrivia_attrib(source, kind); break;
            case "big.lua":
                this.TestOfficialTestFileWithTrivia_big(source, kind); break;
            case "bitwise.lua":
                this.TestOfficialTestFileWithTrivia_bitwise(source, kind); break;
            case "bwcoercion.lua":
                this.TestOfficialTestFileWithTrivia_bwcoercion(source, kind); break;
            case "calls.lua":
                this.TestOfficialTestFileWithTrivia_calls(source, kind); break;
            case "closure.lua":
                this.TestOfficialTestFileWithTrivia_closure(source, kind); break;
            case "code.lua":
                this.TestOfficialTestFileWithTrivia_code(source, kind); break;
            case "constructs.lua":
                this.TestOfficialTestFileWithTrivia_constructs(source, kind); break;
            case "coroutine.lua":
                this.TestOfficialTestFileWithTrivia_coroutine(source, kind); break;
            case "cstack.lua":
                this.TestOfficialTestFileWithTrivia_cstack(source, kind); break;
            case "db.lua":
                this.TestOfficialTestFileWithTrivia_db(source, kind); break;
            case "errors.lua":
                this.TestOfficialTestFileWithTrivia_errors(source, kind); break;
            case "events.lua":
                this.TestOfficialTestFileWithTrivia_events(source, kind); break;
            case "files.lua":
                this.TestOfficialTestFileWithTrivia_files(source, kind); break;
            case "gc.lua":
                this.TestOfficialTestFileWithTrivia_gc(source, kind); break;
            case "gengc.lua":
                this.TestOfficialTestFileWithTrivia_gengc(source, kind); break;
            case "goto.lua":
                this.TestOfficialTestFileWithTrivia_goto(source, kind); break;
            case "heavy.lua":
                this.TestOfficialTestFileWithTrivia_heavy(source, kind); break;
            case "literals.lua":
                this.TestOfficialTestFileWithTrivia_literals(source, kind); break;
            case "locals.lua":
                this.TestOfficialTestFileWithTrivia_locals(source, kind); break;
            case "main.lua":
                this.TestOfficialTestFileWithTrivia_main(source, kind); break;
            case "math.lua":
                this.TestOfficialTestFileWithTrivia_math(source, kind); break;
            case "nextvar.lua":
                this.TestOfficialTestFileWithTrivia_nextvar(source, kind); break;
            case "pm.lua":
                this.TestOfficialTestFileWithTrivia_pm(source, kind); break;
            case "sort.lua":
                this.TestOfficialTestFileWithTrivia_sort(source, kind); break;
            case "strings.lua":
                this.TestOfficialTestFileWithTrivia_strings(source, kind); break;
            case "tpack.lua":
                this.TestOfficialTestFileWithTrivia_tpack(source, kind); break;
            case "tracegc.lua":
                this.TestOfficialTestFileWithTrivia_tracegc(source, kind); break;
            case "utf8.lua":
                this.TestOfficialTestFileWithTrivia_utf8(source, kind); break;
            case "vararg.lua":
                this.TestOfficialTestFileWithTrivia_vararg(source, kind); break;
            case "verybig.lua":
                this.TestOfficialTestFileWithTrivia_verybig(source, kind); break;

            default:
                throw new XunitException($"Test file '{name}' not tested");
        }
    }
}
