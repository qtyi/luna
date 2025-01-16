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

    public static readonly TheoryData<string, string, SourceCodeKind> TestSources;

    static FileTests()
    {
        TestSources = new();
        foreach ((var name, var source) in TestResources.LuaTestFiles.GetAllFiles())
        {
            TestSources.Add(name, source, SourceCodeKind.Regular);
            TestSources.Add(name, source, SourceCodeKind.Script);
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
