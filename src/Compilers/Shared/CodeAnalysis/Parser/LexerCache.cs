// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.PooledObjects;
using Roslyn.Utilities;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal partial class LexerCache
{
    private static readonly ObjectPool<CachingIdentityFactory<string, SyntaxKind>> s_keywordKindPool = CachingIdentityFactory<string, SyntaxKind>.CreatePool(
        512,
        key =>
        {
            var kind = SyntaxFacts.GetKeywordKind(key);
            if (kind == SyntaxKind.None)
                kind = SyntaxFacts.GetContextualKeywordKind(key);

            return kind;
        }
    );
    /// <summary>琐碎内容映射表。</summary>
    private readonly TextKeyedCache<SyntaxTrivia> _triviaMap;
    /// <summary>标志映射表。</summary>
    private readonly TextKeyedCache<SyntaxToken> _tokenMap;
    /// <summary>关键词映射表。</summary>
    private readonly CachingIdentityFactory<string, SyntaxKind> _keywordKindMap;
    /// <summary>
    /// 关键词的最大字符长度。
    /// </summary>
    internal const int MaxKeywordLength = 10;

    internal LexerCache()
    {
        this._triviaMap = TextKeyedCache<SyntaxTrivia>.GetInstance();
        this._tokenMap = TextKeyedCache<SyntaxToken>.GetInstance();
        this._keywordKindMap = LexerCache.s_keywordKindPool.Allocate();
    }

    internal void Free()
    {
        this._keywordKindMap.Free();
        this._triviaMap.Free();
        this._tokenMap.Free();
    }

    /// <summary>
    /// 尝试获取缓存的关键词语法类型。
    /// </summary>
    /// <param name="key">关键词的文本表示</param>
    /// <param name="kind">找到的关键词语法类型。</param>
    /// <returns>若找到<paramref name="key"/>对应的关键词语法类型，则返回<see langword="true"/>；否则返回<see langword="false"/>。</returns>
    internal bool TryGetKeywordKind(string key, out SyntaxKind kind)
    {
        if (key.Length > MaxKeywordLength)
        {
            kind = SyntaxKind.None;
            return false;
        }

        kind = this._keywordKindMap.GetOrMakeValue(key);
        return kind != SyntaxKind.None;
    }

    /// <summary>
    /// 在缓存中查找语法琐碎内容。
    /// </summary>
    /// <param name="textBuffer">读取表示语法琐碎内容的字符缓存。</param>
    /// <param name="keyStart">开始读取的字符位置</param>
    /// <param name="keyLength">读取的字符数量。</param>
    /// <param name="hashCode">指定的哈希码。</param>
    /// <param name="createTriviaFunction">从新创建语法琐碎内容的函数。</param>
    /// <returns>查找到的语法琐碎内容。</returns>
    internal SyntaxTrivia LookupTrivia(
        char[] textBuffer,
        int keyStart,
        int keyLength,
        int hashCode,
        Func<SyntaxTrivia> createTriviaFunction)
    {
        var value = this._triviaMap.FindItem(textBuffer, keyStart, keyLength, hashCode);

        if (value is null)
        {
            value = createTriviaFunction();
            this._triviaMap.AddItem(textBuffer, keyStart, keyLength, hashCode, value);
        }

        return value;
    }

    /// <summary>
    /// 在缓存中查找语法标志。
    /// </summary>
    /// <param name="textBuffer">读取表示语法标志的字符缓存。</param>
    /// <param name="keyStart">开始读取的字符位置</param>
    /// <param name="keyLength">读取的字符数量。</param>
    /// <param name="hashCode">指定的哈希码。</param>
    /// <param name="createTokenFunction">从新创建语法标志的函数。</param>
    /// <returns>查找到的语法标志。</returns>
    internal SyntaxToken LookupToken(
        char[] textBuffer,
        int keyStart,
        int keyLength,
        int hashCode,
        Func<SyntaxToken> createTokenFunction)
    {
        var value = this._tokenMap.FindItem(textBuffer, keyStart, keyLength, hashCode);

        if (value is null)
        {
            value = createTokenFunction();
            this._tokenMap.AddItem(textBuffer, keyStart, keyLength, hashCode, value);
        }

        return value;
    }

}
