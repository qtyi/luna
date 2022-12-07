// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

partial class Lexer
{
#if TESTING
    internal
#else
    private
#endif
        enum QuickScanState : byte
    {
        /// <summary>初始状态。</summary>
        Initial,
        /// <summary>后方空白字符状态。</summary>
        FollowingWhite,
        /// <summary>后方回车符状态。</summary>
        FollowingCR,
        /// <summary>标识符状态。</summary>
        Identifier,
        /// <summary>数字状态。</summary>
        Number,
        /// <summary>标点状态。</summary>
        Punctuation,
        /// <summary>点状态。</summary>
        Dot,
        /// <summary>两个点状态。</summary>
        DoubledDot,
        /// <summary>等号状态。</summary>
        Equals,
        /// <summary>复合标点起始状态。</summary>
        CompoundPunctuationStart,
        /// <summary>下一个后立即是<see cref="Done"/>状态。</summary>
        DoneAfterNext,
        /// <summary>完成状态。</summary>
        Done,
        /// <summary>错误状态。</summary>
        Bad = Done + 1
    }

#if TESTING
    internal
#else
    private
#endif
        enum CharFlag : byte
    {
        /// <summary>空白字符（空格符或制表符）。</summary>
        White,
        /// <summary>回车符。</summary>
        CR,
        /// <summary>换行符。</summary>
        LF,
        /// <summary>字母。</summary>
        Letter,
        /// <summary>数字。</summary>
        Digit,
        /// <summary>简单的标点。</summary>
        Punctuation,
        /// <summary>数字小数点。</summary>
        Dot,
        /// <summary>等号。</summary>
        Equals,
        /// <summary>组合标点的起始。</summary>
        CompoundPunctuationStart,
        /// <summary>复杂内容，将导致扫描终止。</summary>
        Complex,
        EndOfFile,
    }

#if TESTING
    internal
#else
    private
#endif
         static readonly byte[,] s_stateTransitions = new byte[,]
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
            (byte)QuickScanState.Punctuation,               // Equals
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
    /// 获取Unicode字符的前0x180个字符的属性。
    /// </summary>
#if TESTING
    internal
#else
    private
#endif
         static ReadOnlySpan<byte> CharProperties => new[]
    {
        // 0 .. 31
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex,
        (byte)CharFlag.White,   // TAB
        (byte)CharFlag.LF,      // LF
        (byte)CharFlag.White,   // VT
        (byte)CharFlag.White,   // FF
        (byte)CharFlag.CR,      // CR
        (byte)CharFlag.Complex,
        (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,

        // 32 .. 63
        (byte)CharFlag.White,                      // SPC
        (byte)CharFlag.CompoundPunctuationStart,   // !
        (byte)CharFlag.Complex,                    // "
        (byte)CharFlag.Punctuation,                // #
        (byte)CharFlag.Complex,                    // $
        (byte)CharFlag.CompoundPunctuationStart,   // %
        (byte)CharFlag.Punctuation,                // &
        (byte)CharFlag.Complex,                    // '
        (byte)CharFlag.Punctuation,                // (
        (byte)CharFlag.Punctuation,                // )
        (byte)CharFlag.CompoundPunctuationStart,   // *
        (byte)CharFlag.CompoundPunctuationStart,   // +
        (byte)CharFlag.Punctuation,                // ,
        (byte)CharFlag.Complex,                    // -
        (byte)CharFlag.Dot,                        // .
        (byte)CharFlag.CompoundPunctuationStart,   // /
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
        (byte)CharFlag.Complex,                    // ;
        (byte)CharFlag.CompoundPunctuationStart,   // <
        (byte)CharFlag.Equals,                     // =
        (byte)CharFlag.CompoundPunctuationStart,   // >
        (byte)CharFlag.Complex,                    // ?

        // 64 .. 95
        (byte)CharFlag.Complex,                    // @
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

        // 96 .. 127
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
        (byte)CharFlag.Complex,

        // 128 .. 159
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,

        // 160 .. 191
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Letter, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Letter, (byte)CharFlag.Complex, (byte)CharFlag.Complex,
        (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Letter, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex, (byte)CharFlag.Complex,

        // 192 .. 
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
    };
}
