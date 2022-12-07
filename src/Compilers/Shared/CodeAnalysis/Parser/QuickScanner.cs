// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

// 此文件中定义了一个基于状态的快速扫描器。
partial class Lexer
{
    internal const int MaxCachedTokenSize = 42;

#if TESTING
    internal
#else
    private
#endif
        SyntaxToken? QuickScanSyntaxToken()
    {
        this.Start();
        var state = QuickScanState.Initial; // 初始状态。
        int i = this.TextWindow.Offset;
        int n = this.TextWindow.CharacterWindowCount;
        n = Math.Min(n, i + MaxCachedTokenSize);

        int hashCode = Hash.FnvOffsetBias;

        var charWindow = this.TextWindow.CharacterWindow;
        var charProp = CharProperties;
        var charPropLength = charProp.Length;

        for (; i < n; i++)
        {
            int uc = charWindow[i];

            // 获取当前字符的属性，超出0x180范围的字符属性默认为Complex。
            var flag = uc < charPropLength ? (CharFlag)charProp[uc] : CharFlag.Complex;

            state = (QuickScanState)s_stateTransitions[(int)state, (int)flag];
            // 所有不小于Done的状态（包括Bad）都将导致扫描过程终止。
            if (state >= QuickScanState.Done)
                goto exitWhile;

            hashCode = unchecked((hashCode ^ uc) * Hash.FnvPrime);
        }

        state = QuickScanState.Bad; // 字符缓冲窗口中的字符已用尽。
exitWhile:

        this.TextWindow.AdvanceChar(i - this.TextWindow.Offset);
        Debug.Assert(state == QuickScanState.Bad || state == QuickScanState.Done, "无法在Bad和Done的状态下退出。");

        if (state == QuickScanState.Done) // 成功扫描到标志。
        {
            var token = _cache.LookupToken(
                this.TextWindow.CharacterWindow,
                this.TextWindow.LexemeRelativeStart,
                i - this.TextWindow.LexemeRelativeStart,
                hashCode,
                _createQuickTokenFunction);
            return token;
        }
        else // 扫描失败。
        {
            this.TextWindow.Reset(this.TextWindow.LexemeStartPosition);
            return null;
        }
    }

    private readonly Func<SyntaxToken> _createQuickTokenFunction;

    private SyntaxToken CreateQuickToken()
    {
#if DEBUG
        var quickWidth = TextWindow.Width;
#endif
        this.TextWindow.Reset(this.TextWindow.LexemeStartPosition);
        var token = this.LexSyntaxToken();
#if DEBUG
        Debug.Assert(quickWidth == token.FullWidth);
#endif
        return token;
    }
}
