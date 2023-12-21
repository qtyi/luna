// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Text;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
using Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
using static Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.Lexer;

namespace Qtyi.CodeAnalysis.Lua.Parser.UnitTests;
#elif LANG_MOONSCRIPT
using Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
using static Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.Lexer;

namespace Qtyi.CodeAnalysis.MoonScript.Parser.UnitTests;
#endif

[TestClass]
public partial class QuickScannerTests
{
#pragma warning disable CS8618
    public TestContext TestContext { get; set; }
#pragma warning restore CS8618

    internal static IEnumerable<char> GetCharByFlag(CharFlag flag)
    {
        var properties = CharProperties.ToArray();
        for (int i = 0, n = properties.Length; i < n; i++)
        {
            var b = properties[i];
            if (b == (byte)flag)
                yield return (char)i;
        }
        if (flag == CharFlag.Complex)
            yield return (char)0x181;
    }

    internal static CharFlag GetFlag(char c) => c < 0x180 ? (CharFlag)CharProperties[c] : CharFlag.Complex;

    /// <summary>
    /// 从Unicode字符集第1到第0x181（共385个字符）范围中枚举字符组成序列，作为状态机的输入，检测状态机的每个状态是否正常对应。
    /// 逐渐增长字符序列，直到状态机达到终止状态，返回接收的字符串。
    /// </summary>
    /// <returns>状态机接收的所有字符串。</returns>
    internal static IEnumerable<string> Run()
    {
        return RunInternal(QuickScanState.Initial, string.Empty);

        static IEnumerable<string> RunInternal(QuickScanState state, string buffer)
        {
            for (int uc = 0; uc < 0x181; uc++)
            {
                var nextChar = (char)uc;
                var nextFlag = QuickScannerTests.GetFlag(nextChar);

                var newState = (QuickScanState)s_stateTransitions[(int)state, (int)nextFlag];
                // 防止堆栈溢出。
                if (newState == state)
                    yield return buffer + nextChar;
                else
                {
                    switch (newState)
                    {
                        case QuickScanState.Done:
                            yield return buffer;
                            break;
                        case QuickScanState.Bad:
                            continue;
                        default:
                            foreach (var s in RunInternal(newState, buffer + nextChar))
                                yield return s;
                            break;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 将状态机接收的字符串与广范围词法分析逻辑产生的语法标志交叉检查是否对应。
    /// </summary>
    /// <param name="result">状态机接收的字符串。</param>
    /// <returns>若状态机逻辑是广范围词法分析逻辑的子集，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    public static bool Check(string result)
    {
        var lexer = LexerTests.CreateLexer(result);
        lexer._mode = LexerMode.Syntax;

        var token = lexer.LexSyntaxToken();

        if (token.Kind == SyntaxKind.BadToken)
            return false;
        if (token.FullWidth != result.Length)
            return false;

        return true;
    }

    [TestMethod]
    public void StateTest()
    {
        long count = 0;
        Assert.IsTrue(
            QuickScannerTests.Run()
                .Select(r =>
                {
                    count++;
                    return r;
                })
                .All(QuickScannerTests.Check)
        );
        this.TestContext.WriteLine($"共 {count} 种状态组合均通过测试。"); // 7位数数量级。
    }
}
