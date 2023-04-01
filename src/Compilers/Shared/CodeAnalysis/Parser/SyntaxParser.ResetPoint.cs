// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

partial class SyntaxParser
{
    protected struct ResetPoint
    {
        internal readonly int ResetCount;
        internal readonly LexerMode Mode;
        internal readonly int Position;
        internal readonly GreenNode? PrevTokenTrailingTrivia;

        internal ResetPoint(
            int resetCount,
            LexerMode mode,
            int position,
            GreenNode? prevTokenTrailingTrivia
        )
        {
            this.ResetCount = resetCount;
            this.Mode = mode;
            this.Position = position;
            this.PrevTokenTrailingTrivia = prevTokenTrailingTrivia;
        }
    }

    protected ResetPoint GetResetPoint()
    {
        var pos = this.CurrentTokenPosition;
        if (this._resetCount == 0)
            this._resetStart = pos;

        this._resetCount++;
        return new(this._resetCount, this._mode, pos, this.prevTokenTrailingTrivia);
    }

    protected void Reset(ref ResetPoint point)
    {
        var offset = point.Position - this._firstToken;
        Debug.Assert(offset >= 0);

        if (offset >= this._tokenCount)
        {
            this.PeekToken(offset - this._tokenOffset);

            offset = point.Position - this._firstToken;
        }

        this._mode = point.Mode;
        Debug.Assert(offset >= 0 && offset < this._tokenCount);
        this._tokenOffset = offset;
        this._currentToken = null;
        this._currentNode = default;
        this.prevTokenTrailingTrivia = point.PrevTokenTrailingTrivia;
        if (this.IsBlending())
        {
            for (int i = this._tokenOffset; i < this._tokenCount; i++)
            {
                if (this._blendedTokens[i].Token is null)
                {
                    this._tokenCount = i;
                    if (this._tokenCount == this._tokenOffset)
                        this.FetchCurrentToken();
                    break;
                }
            }
        }
    }

    protected void Release(ref ResetPoint point)
    {
        Debug.Assert(this._resetCount == point.ResetCount);
        this._resetCount--;
        if (this._resetCount == 0)
            this._resetStart = -1;
    }
}
