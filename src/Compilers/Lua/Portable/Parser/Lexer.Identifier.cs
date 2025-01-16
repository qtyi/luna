// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Runtime.CompilerServices;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class Lexer
{
    private partial bool ScanIdentifierOrKeyword(ref TokenInfo info)
    {
        info.ContextualKind = SyntaxKind.None;

        if (ScanIdentifier(ref info))
        {
            Debug.Assert(info.Text is not null);

            // Gets keyword kind by name.
            if (_cache.TryGetKeywordKind(info.Text, out var keywordKind))
            {
                // Is reserved keyword.
                if (SyntaxFacts.IsReservedKeyword(keywordKind, _options.LanguageVersion))
                {
                    info.Kind = keywordKind;
                }
                // Is contextual keyword.
                else if (SyntaxFacts.IsContextualKeyword(keywordKind, _options.LanguageVersion))
                {
                    info.Kind = SyntaxKind.IdentifierToken;
                    info.ContextualKind = keywordKind;
                }
            }

            // The rest must be identifier.
            if (info.Kind == SyntaxKind.None)
            {
                info.Kind = SyntaxKind.IdentifierToken;
                info.ContextualKind = SyntaxKind.IdentifierToken;
            }

            return true;
        }
        else
        {
            info.Kind = SyntaxKind.None;
            return false;
        }
    }

    private bool ScanIdentifier(ref TokenInfo info) =>
        ScanIdentifier_FastPath(ref info) || ScanIdentifier_SlowPath(ref info);

    /// <summary>
    /// Implements a faster identifier lexer for the common case in the language where:
    ///   a) identifiers are not verbatim.
    ///   b) identifiers don't contain unicode characters.
    ///   c) identifiers don't contain unicode escapes.
    /// </summary>
    /// <remarks>
    /// <para>Given that nearly all identifiers will contain [_a-zA-Z0-9] and will be terminated by a small set of known characters (like dot, comma, etc.), we can sit in a tight loop looking for this pattern and only falling back to the slower (but correct) path if we see something we can't handle.</para>
    /// 
    /// <para>Note: this function also only works if the identifier (and terminator) can be found in the current sliding window of chars we have from our source text.  With this constraint we can avoid the costly overhead incurred with peek/advance/next.
    /// Because of this we can also avoid the unnecessary stores/reads from identBuffer and all other instance state while lexing.  Instead we just keep track of our start, end, and max positions and use those for quick checks internally.</para>
    ///
    /// <para>Note: it is critical that this method must only be called from a code path that checked for IsIdentifierStartChar or '@' first.</para>
    /// </remarks>
    private bool ScanIdentifier_FastPath(ref TokenInfo info) =>
        ScanIdentifierCore(ref info, isFastPath: true);

    /// <summary>慢速扫描标识符。</summary>
    private bool ScanIdentifier_SlowPath(ref TokenInfo info) =>
        ScanIdentifierCore(ref info, isFastPath: false);

    /// <summary>扫描标识符的实现方法。</summary>
    /// <remarks>此方法应尽可能地内联以减少调用深度。</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool ScanIdentifierCore(ref TokenInfo info, bool isFastPath)
    {
        var currentOffset = TextWindow.Offset;
        var characterWindow = TextWindow.CharacterWindow;
        var characterWindowCount = TextWindow.CharacterWindowCount;

        var startOffset = currentOffset;
        ResetIdentifierBuffer();

        while (true)
        {
            // 缓冲窗口中的字符用尽。
            if (currentOffset == characterWindowCount)
            {
                if (isFastPath) return false; // 由于要移动缓冲字符窗口，所以留给慢速扫描处理。

                var length = currentOffset - startOffset;
                TextWindow.Reset(TextWindow.LexemeStartPosition + length);
                if (!TextWindow.IsReallyAtEnd() && TextWindow.MoreChars()) // 缓冲窗口成功更新。
                {
                    if (currentOffset != TextWindow.Offset) // 移动了缓冲窗口。
                    {
                        currentOffset = TextWindow.Offset;
                        startOffset = currentOffset - length;
                    }
                    characterWindow = TextWindow.CharacterWindow;
                    characterWindowCount = TextWindow.CharacterWindowCount;

                    continue;
                }

                // 已抵达输入的结尾。
                if (_identifierLength == 0) return false; // 标识符长度为零，意味着分析失败。

                TextWindow.AdvanceChar(length);
                info.Text = TextWindow.Intern(characterWindow, startOffset, length);
                info.StringValue = TextWindow.Intern(_identifierBuffer, 0, _identifierLength);
                return true;
            }

            var c = characterWindow[currentOffset++];

            // 数字
            if (c is >= '0' and <= '9')
            {
                // 首字符不能是数字。
                if (currentOffset == startOffset)
                    return false;
                else
                {
                    AddIdentifierChar(c);
                    continue;
                }
            }
            // 拉丁字符
            else if (c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z'))
            {
                AddIdentifierChar(c);
                continue;
            }
            // 下划线
            else if (c == '_')
            {
                AddIdentifierChar(c);
                continue;
            }

            // 处理终止字符。
            else if (
                SyntaxFacts.IsWhitespace(c) || // 属于空白字符
                SyntaxFacts.IsNewLine(c) || // 属于换行符
                (c >= 32 && c <= 126)) // 属于ASCII可显示字符范围
            {
                var length = --currentOffset - startOffset; // 在上方获取c的时候currentOffset向后移了一位，这里需要恢复再计算长度。
                TextWindow.AdvanceChar(length - (TextWindow.Position - TextWindow.LexemeStartPosition));
                info.Text = TextWindow.Intern(characterWindow, startOffset, length);
                info.StringValue = TextWindow.Intern(_identifierBuffer, 0, _identifierLength);
                return true;
            }

            // 其余字符一律留给慢速扫描处理。
            else if (isFastPath)
                return false;

            // 慢速扫描处理ASCII可显示字符范围外的可用的Unicode字符。
            // 因为SyntaxFacts.IsIdentifierStartCharacter和SyntaxFacts.IsIdentifierPartCharacter是高开销的方法，所以在快速扫描阶段不进行。
            else
            {
                if (currentOffset == startOffset ?
                    SyntaxFacts.IsIdentifierStartCharacter(c) :
                    SyntaxFacts.IsIdentifierPartCharacter(c)
                )
                {
                    AddIdentifierChar(c);
                    continue;
                }
                else
                    return false;
            }
        }
    }
}
