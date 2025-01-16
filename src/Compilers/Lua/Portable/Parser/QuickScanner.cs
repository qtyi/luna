// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

partial class Lexer
{
    /// <summary>
    /// Define possible states of a running lexer.
    /// </summary>
    private enum QuickScanState : byte
    {
        /// <summary>The initial state.</summary>
        Initial,
        /// <summary>Is trailing whitespace.</summary>
        FollowingWhite,
        /// <summary>Is trailing carriage return.</summary>
        FollowingCR,
        /// <summary>Is identifier.</summary>
        Identifier,
        /// <summary>Is number.</summary>
        Number,
        /// <summary>Is punctuation.</summary>
        Punctuation,
        /// <summary>Is dot (<c>.</c>).</summary>
        Dot,
        /// <summary>Is doubled dot (<c>..</c>).</summary>
        DoubledDot,
        /// <summary>Is equals (<c>=</c>).</summary>
        Equals,
        /// <summary>Is start of compound punctuation.</summary>
        CompoundPunctuationStart,
        /// <summary>Switch to <see cref="Done"/> state after eating the next character whatever it is.</summary>
        DoneAfterNext,
        /// <summary>The done state.</summary>
        Done,
        /// <summary>The bad state.</summary>
        Bad = Done + 1
    }

    /// <summary>
    /// Classifies character the lexer meets.
    /// </summary>
    private enum CharFlag : byte
    {
        /// <summary>simple whitespace (space/tab).</summary>
        White,
        /// <summary>carriage return.</summary>
        CR,
        /// <summary>line feed.</summary>
        LF,
        /// <summary>letter.</summary>
        Letter,
        /// <summary>digit 0-9.</summary>
        Digit,
        /// <summary>some simple punctuation (parentheses, braces, comma, question).</summary>
        Punctuation,
        /// <summary>dot is different from other punctuation when followed by a digit (Ex: .9 ).</summary>
        Dot,
        /// <summary>equals character.</summary>
        Equals,
        /// <summary>may be a part of compound punctuation. will be used only if followed by (not whitespace) and (not punctuation).</summary>
        CompoundPunctuationStart,
        /// <summary>causes quick-scanning to abort.</summary>
        Complex,
        /// <summary>legal type character.</summary>
        EndOfFile,
    }

    /// <summary>
    /// The following table classifies the next QuickScanState when lexer is in current QuickScanState and eat next character of CharFlag.
    /// </summary>
    /// <remarks>
    /// PERF: Use byte instead of QuickScanState so the compiler can use array literal initialization.
    ///       The most natural type choice, Enum arrays, are not blittable due to a CLR limitation.
    /// </remarks>
    private static readonly byte[,] s_stateTransitions = new byte[,]
    {
        // Initial
        {
            (byte)QuickScanState.Initial,                   // White
            (byte)QuickScanState.Initial,                   // CR
            (byte)QuickScanState.Initial,                   // LF
            (byte)QuickScanState.Identifier,                // Letter
            (byte)QuickScanState.Number,                    // Digit
            (byte)QuickScanState.Punctuation,               // Punctuation
            (byte)QuickScanState.Dot,                       // Dot
            (byte)QuickScanState.Equals,                    // Equals
            (byte)QuickScanState.CompoundPunctuationStart,  // CompoundPunctuationStart
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Bad,                       // EndOfFile
        },

        // FollowingWhite
        {
            (byte)QuickScanState.FollowingWhite,            // White
            (byte)QuickScanState.FollowingCR,               // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Done,                      // Letter
            (byte)QuickScanState.Done,                      // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Done,                      // Dot
            (byte)QuickScanState.Done,                      // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },

        // FollowingCR
        {
            (byte)QuickScanState.Done,                      // White
            (byte)QuickScanState.Done,                      // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Done,                      // Letter
            (byte)QuickScanState.Done,                      // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Done,                      // Dot
            (byte)QuickScanState.Done,                      // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Done,                      // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },

        // Identifier
        {
            (byte)QuickScanState.FollowingWhite,            // White
            (byte)QuickScanState.FollowingCR,               // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Identifier,                // Letter
            (byte)QuickScanState.Identifier,                // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Done,                      // Dot
            (byte)QuickScanState.Done,                      // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },

        // Number
        {
            (byte)QuickScanState.FollowingWhite,            // White
            (byte)QuickScanState.FollowingCR,               // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Bad,                       // Letter（指数后缀过于复杂）
            (byte)QuickScanState.Number,                    // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Bad,                       // Dot（带小数点的数字常量过于复杂）
            (byte)QuickScanState.Done,                      // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },

        // Punctuation
        {
            (byte)QuickScanState.FollowingWhite,            // White
            (byte)QuickScanState.FollowingCR,               // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Done,                      // Letter
            (byte)QuickScanState.Done,                      // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Done,                      // Dot
            (byte)QuickScanState.Done,                      // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },
        
        // Dot
        {
            (byte)QuickScanState.FollowingWhite,            // White
            (byte)QuickScanState.FollowingCR,               // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Done,                      // Letter
            (byte)QuickScanState.Number,                    // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.DoubledDot,                // Dot
            (byte)QuickScanState.Done,                      // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },
        
        // DoubledDot
        {
            (byte)QuickScanState.FollowingWhite,            // White
            (byte)QuickScanState.FollowingCR,               // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Done,                      // Letter
            (byte)QuickScanState.Done,                      // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Punctuation,               // Dot
            (byte)QuickScanState.Done,                      // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },

        // Equals
        {
            (byte)QuickScanState.FollowingWhite,            // White
            (byte)QuickScanState.FollowingCR,               // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Done,                      // Letter
            (byte)QuickScanState.Done,                      // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Done,                      // Dot
            (byte)QuickScanState.Punctuation,             // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },

        // CompoundPunctuationStart
        {
            (byte)QuickScanState.FollowingWhite,            // White
            (byte)QuickScanState.FollowingCR,               // CR
            (byte)QuickScanState.DoneAfterNext,             // LF
            (byte)QuickScanState.Done,                      // Letter
            (byte)QuickScanState.Done,                      // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Done,                      // Dot
            (byte)QuickScanState.Punctuation,             // Equals
            (byte)QuickScanState.Bad,                       // CompoundPunctuationStart（按位左移和按位右移太复杂）
            (byte)QuickScanState.Bad,                       // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },

        // DoneAfterNext
        {
            (byte)QuickScanState.Done,                      // White
            (byte)QuickScanState.Done,                      // CR
            (byte)QuickScanState.Done,                      // LF
            (byte)QuickScanState.Done,                      // Letter
            (byte)QuickScanState.Done,                      // Digit
            (byte)QuickScanState.Done,                      // Punctuation
            (byte)QuickScanState.Done,                      // Dot
            (byte)QuickScanState.Done,                      // Equals
            (byte)QuickScanState.Done,                      // CompoundPunctuationStart
            (byte)QuickScanState.Done,                      // Complex
            (byte)QuickScanState.Done,                      // EndOfFile
        },
    };

    /// <summary>
    /// The following table classifies the first 0x180 Unicode characters. 
    /// </summary>
    /// <remarks>
    /// PERF: Use byte instead of CharFlags so the compiler can use array literal initialization.
    ///       The most natural type choice, Enum arrays, are not blittable due to a CLR limitation.
    /// </remarks>
    private static ReadOnlySpan<byte> CharProperties =>
    [
        // 0 .. 8
#pragma warning disable IDE0055
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
#pragma warning restore IDE0055

        // 9 .. 13
        (byte)CharFlag.White,                      // TAB
        (byte)CharFlag.LF,                         // LF
        (byte)CharFlag.White,                      // VT
        (byte)CharFlag.White,                      // FF
        (byte)CharFlag.CR,                         // CR

        // 14 .. 31
#pragma warning disable IDE0055
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
#pragma warning restore IDE0055

        // 32 .. 126
        (byte)CharFlag.White,                      // SPC
        (byte)CharFlag.Complex,                    // !
        (byte)CharFlag.Complex,                    // "
        (byte)CharFlag.Punctuation,                // #
        (byte)CharFlag.Complex,                    // $
        (byte)CharFlag.Punctuation,                // %
        (byte)CharFlag.Punctuation,                // &
        (byte)CharFlag.Complex,                    // '
        (byte)CharFlag.Punctuation,                // (
        (byte)CharFlag.Punctuation,                // )
        (byte)CharFlag.Punctuation,                // *
        (byte)CharFlag.Punctuation,                // +
        (byte)CharFlag.Punctuation,                // ,
        (byte)CharFlag.Complex,                    // -
        (byte)CharFlag.Dot,                        // .
        (byte)CharFlag.Complex,                    // /
        (byte)CharFlag.Digit,                      // 0
        (byte)CharFlag.Digit,                      // 1
        (byte)CharFlag.Digit,                      // 2
        (byte)CharFlag.Digit,                      // 3
        (byte)CharFlag.Digit,                      // 4
        (byte)CharFlag.Digit,                      // 5
        (byte)CharFlag.Digit,                      // 6
        (byte)CharFlag.Digit,                      // 7
        (byte)CharFlag.Digit,                      // 8
        (byte)CharFlag.Digit,                      // 9
        (byte)CharFlag.Complex,                    // :
        (byte)CharFlag.Punctuation,                // ;
        (byte)CharFlag.CompoundPunctuationStart,   // <
        (byte)CharFlag.Equals,                     // =
        (byte)CharFlag.CompoundPunctuationStart,   // >
        (byte)CharFlag.Complex,                    // ?
        (byte)CharFlag.Punctuation,                // @
        (byte)CharFlag.Letter,                     // A
        (byte)CharFlag.Letter,                     // B
        (byte)CharFlag.Letter,                     // C
        (byte)CharFlag.Letter,                     // D
        (byte)CharFlag.Letter,                     // E
        (byte)CharFlag.Letter,                     // F
        (byte)CharFlag.Letter,                     // G
        (byte)CharFlag.Letter,                     // H
        (byte)CharFlag.Letter,                     // I
        (byte)CharFlag.Letter,                     // J
        (byte)CharFlag.Letter,                     // K
        (byte)CharFlag.Letter,                     // L
        (byte)CharFlag.Letter,                     // M
        (byte)CharFlag.Letter,                     // N
        (byte)CharFlag.Letter,                     // O
        (byte)CharFlag.Letter,                     // P
        (byte)CharFlag.Letter,                     // Q
        (byte)CharFlag.Letter,                     // R
        (byte)CharFlag.Letter,                     // S
        (byte)CharFlag.Letter,                     // T
        (byte)CharFlag.Letter,                     // U
        (byte)CharFlag.Letter,                     // V
        (byte)CharFlag.Letter,                     // W
        (byte)CharFlag.Letter,                     // X
        (byte)CharFlag.Letter,                     // Y
        (byte)CharFlag.Letter,                     // Z
        (byte)CharFlag.Complex,                    // [
        (byte)CharFlag.Complex,                    // \
        (byte)CharFlag.Punctuation,                // ]
        (byte)CharFlag.Punctuation,                // ^
        (byte)CharFlag.Letter,                     // _
        (byte)CharFlag.Complex,                    // `
        (byte)CharFlag.Letter,                     // a
        (byte)CharFlag.Letter,                     // b
        (byte)CharFlag.Letter,                     // c
        (byte)CharFlag.Letter,                     // d
        (byte)CharFlag.Letter,                     // e
        (byte)CharFlag.Letter,                     // f
        (byte)CharFlag.Letter,                     // g
        (byte)CharFlag.Letter,                     // h
        (byte)CharFlag.Letter,                     // i
        (byte)CharFlag.Letter,                     // j
        (byte)CharFlag.Letter,                     // k
        (byte)CharFlag.Letter,                     // l
        (byte)CharFlag.Letter,                     // m
        (byte)CharFlag.Letter,                     // n
        (byte)CharFlag.Letter,                     // o
        (byte)CharFlag.Letter,                     // p
        (byte)CharFlag.Letter,                     // q
        (byte)CharFlag.Letter,                     // r
        (byte)CharFlag.Letter,                     // s
        (byte)CharFlag.Letter,                     // t
        (byte)CharFlag.Letter,                     // u
        (byte)CharFlag.Letter,                     // v
        (byte)CharFlag.Letter,                     // w
        (byte)CharFlag.Letter,                     // x
        (byte)CharFlag.Letter,                     // y
        (byte)CharFlag.Letter,                     // z
        (byte)CharFlag.Punctuation,                // {
        (byte)CharFlag.Punctuation,                // |
        (byte)CharFlag.Punctuation,                // }
        (byte)CharFlag.CompoundPunctuationStart,   // ~

        // 127 ..
#pragma warning disable IDE0055
        (byte)CharFlag.Complex,

        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,

        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Letter, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Letter, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Letter, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,

        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Complex,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,

        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Complex,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,

        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,

        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,

        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,

        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter,
        (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter, (byte)CharFlag.Letter
#pragma warning restore IDE0055
    ];
}
