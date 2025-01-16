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
            ResetCount = resetCount;
            Mode = mode;
            Position = position;
            PrevTokenTrailingTrivia = prevTokenTrailingTrivia;
        }
    }

    protected ResetPoint GetResetPoint()
    {
        var pos = CurrentTokenPosition;
        if (_resetCount == 0)
            _resetStart = pos;

        _resetCount++;
        return new(_resetCount, _mode, pos, prevTokenTrailingTrivia);
    }

    protected void Reset(ref ResetPoint point)
    {
        var offset = point.Position - _firstToken;
        Debug.Assert(offset >= 0);

        if (offset >= _tokenCount)
        {
            PeekToken(offset - _tokenOffset);

            offset = point.Position - _firstToken;
        }

        _mode = point.Mode;
        Debug.Assert(offset >= 0 && offset < _tokenCount);
        _tokenOffset = offset;
        _currentToken = null;
        _currentNode = default;
        prevTokenTrailingTrivia = point.PrevTokenTrailingTrivia;
        if (IsBlending())
        {
            for (var i = _tokenOffset; i < _tokenCount; i++)
            {
                if (_blendedTokens[i].Token is null)
                {
                    _tokenCount = i;
                    if (_tokenCount == _tokenOffset)
                        FetchCurrentToken();
                    break;
                }
            }
        }
    }

    protected void Release(ref ResetPoint point)
    {
        Debug.Assert(_resetCount == point.ResetCount);
        _resetCount--;
        if (_resetCount == 0)
            _resetStart = -1;
    }
}
