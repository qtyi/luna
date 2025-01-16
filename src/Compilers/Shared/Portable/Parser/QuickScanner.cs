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

partial class Lexer
{
    // Maximum size of tokens/trivia that we cache and use in quick scanner.
    // From what I see in our own codebase, tokens longer then 40-50 chars are 
    // not very common. 
    // So it seems reasonable to limit the sizes to some round number like 42.
    internal const int MaxCachedTokenSize = 42;

    private SyntaxToken? QuickScanSyntaxToken()
    {
        Start();
        var state = QuickScanState.Initial; // Set initial state.
        var i = TextWindow.Offset;
        var n = TextWindow.CharacterWindowCount;
        n = Math.Min(n, i + MaxCachedTokenSize);

        var hashCode = Hash.FnvOffsetBias;

        var charWindow = TextWindow.CharacterWindow;
        var charProp = CharProperties;
        var charPropLength = charProp.Length;

        for (; i < n; i++)
        {
            int uc = charWindow[i];

            // Get CharFlag of current character, Complex if not in CharProperties.
            var flag = uc < charPropLength ? (CharFlag)charProp[uc] : CharFlag.Complex;

            state = (QuickScanState)s_stateTransitions[(int)state, (int)flag];
            // NOTE: that Bad > Done and it is the only state like that as a result, we will exit the loop on either Bad or Done.
            // The assert below will validate that these are the only states on which we exit.
            // Also note that we must exit on Done or Bad since the state machine does not have transitions for these states and will promptly fail if we do not exit.
            if (state >= QuickScanState.Done)
                goto exitWhile;

            hashCode = unchecked((hashCode ^ uc) * Hash.FnvPrime);
        }

        state = QuickScanState.Bad; // ran out of characters in window
exitWhile:

        TextWindow.AdvanceChar(i - TextWindow.Offset);
        Debug.Assert(state == QuickScanState.Bad || state == QuickScanState.Done, "Can only exit with Bad or Done");

        if (state == QuickScanState.Done) // Scanning succeeded.
        {
            var token = _cache.LookupToken(
                TextWindow.CharacterWindow,
                TextWindow.LexemeRelativeStart,
                i - TextWindow.LexemeRelativeStart,
                hashCode,
                CreateQuickToken,
                this);
            return token;
        }
        else // Scanning failed.
        {
            TextWindow.Reset(TextWindow.LexemeStartPosition);
            return null;
        }
    }

    private static SyntaxToken CreateQuickToken(Lexer lexer)
    {
#if DEBUG
        var quickWidth = lexer.TextWindow.Width;
#endif
        lexer.TextWindow.Reset(lexer.TextWindow.LexemeStartPosition);
        var token = lexer.LexSyntaxToken();
#if DEBUG
        Debug.Assert(quickWidth == token.FullWidth);
#endif
        return token;
    }
}
