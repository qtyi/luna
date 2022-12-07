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

        if (this.ScanIdentifier(ref info))
        {
            Debug.Assert(info.Text is not null);

            if (!this._cache.TryGetKeywordKind(info.Text, out info.Kind))
            {
                info.Kind = SyntaxKind.IdentifierToken;
                info.ContextualKind = info.Kind;
            }
            else if (SyntaxFacts.IsContextualKeyword(info.Kind))
            {
                info.ContextualKind = info.Kind;
                info.Kind = SyntaxKind.IdentifierToken;
            }

            // 排除关键字，剩下的必然是标识符。
            if (info.Kind == SyntaxKind.None)
                info.Kind = SyntaxKind.IdentifierToken;

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

    /// <summary>快速扫描标识符。</summary>
    private bool ScanIdentifier_FastPath(ref TokenInfo info) =>
        this.ScanIdentifierCore(ref info, isFastPath: true);

    /// <summary>慢速扫描标识符。</summary>
    private bool ScanIdentifier_SlowPath(ref TokenInfo info) =>
        this.ScanIdentifierCore(ref info, isFastPath: false);

    /// <summary>扫描标识符的实现方法。</summary>
    /// <remarks>此方法应尽可能地内联以减少调用深度。</remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private bool ScanIdentifierCore(ref TokenInfo info, bool isFastPath)
    {
        var currentOffset = this.TextWindow.Offset;
        var characterWindow = this.TextWindow.CharacterWindow;
        var characterWindowCount = this.TextWindow.CharacterWindowCount;

        var startOffset = currentOffset;
        this.ResetIdentifierBuffer();

        while (true)
        {
            // 缓冲窗口中的字符用尽。
            if (currentOffset == characterWindowCount)
            {
                if (isFastPath) return false; // 由于要移动缓冲字符窗口，所以留给慢速扫描处理。

                var length = currentOffset - startOffset;
                this.TextWindow.Reset(this.TextWindow.LexemeStartPosition + length);
                if (!this.TextWindow.IsReallyAtEnd() && this.TextWindow.MoreChars()) // 缓冲窗口成功更新。
                {
                    if (currentOffset != this.TextWindow.Offset) // 移动了缓冲窗口。
                    {
                        currentOffset = this.TextWindow.Offset;
                        startOffset = currentOffset - length;
                    }
                    characterWindow = this.TextWindow.CharacterWindow;
                    characterWindowCount = this.TextWindow.CharacterWindowCount;

                    continue;
                }

                // 已抵达输入的结尾。
                if (this._identifierLength == 0) return false; // 标识符长度为零，意味着分析失败。

                this.TextWindow.AdvanceChar(length);
                info.Text = this.TextWindow.Intern(characterWindow, startOffset, length);
                info.StringValue = this.TextWindow.Intern(this._identifierBuffer, 0, this._identifierLength);
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
                    this.AddIdentifierChar(c);
                    continue;
                }
            }
            // 拉丁字符
            else if (c is (>= 'a' and <= 'z') or (>= 'A' and <= 'Z'))
            {
                this.AddIdentifierChar(c);
                continue;
            }
            // 下划线
            else if (c == '_')
            {
                this.AddIdentifierChar(c);
                continue;
            }

            // 处理终止字符。
            else if (
                SyntaxFacts.IsWhiteSpace(c) || // 属于空白字符
                SyntaxFacts.IsNewLine(c) || // 属于换行符
                (c >= 32 && c <= 126)) // 属于ASCII可显示字符范围
            {
                var length = --currentOffset - startOffset; // 在上方获取c的时候currentOffset向后移了一位，这里需要恢复再计算长度。
                this.TextWindow.AdvanceChar(length - (this.TextWindow.Position - this.TextWindow.LexemeStartPosition));
                info.Text = this.TextWindow.Intern(characterWindow, startOffset, length);
                info.StringValue = this.TextWindow.Intern(this._identifierBuffer, 0, this._identifierLength);
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
                    this.AddIdentifierChar(c);
                    continue;
                }
                else
                    return false;
            }
        }
    }
}
