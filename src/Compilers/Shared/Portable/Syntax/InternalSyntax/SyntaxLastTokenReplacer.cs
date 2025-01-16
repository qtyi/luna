// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal class SyntaxLastTokenReplacer :
#if LANG_LUA
    LuaSyntaxRewriter
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxRewriter
#endif
{
    private readonly SyntaxToken _oldToken;
    private readonly SyntaxToken _newToken;
    private int _count = 1;
    private bool _found = false;

    private SyntaxLastTokenReplacer(SyntaxToken oldToken, SyntaxToken newToken)
    {
        _oldToken = oldToken;
        _newToken = newToken;
    }

    internal static TRoot Replace<TRoot>(TRoot root, SyntaxToken newToken)
        where TRoot : ThisInternalSyntaxNode
    {
        var oldToken = root as SyntaxToken ?? root.GetLastToken();
        Debug.Assert(oldToken is not null);
        return Replace(root, oldToken, newToken);
    }

    internal static TRoot Replace<TRoot>(TRoot root, SyntaxToken oldToken, SyntaxToken newToken)
        where TRoot : ThisInternalSyntaxNode
    {
        var replacer = new SyntaxLastTokenReplacer(oldToken, newToken);
        var newRoot = (TRoot)replacer.Visit(root)!;
        Debug.Assert(replacer._found);
        return newRoot;
    }

    private static int CountNonNullSlots(ThisInternalSyntaxNode node) => node.ChildNodesAndTokens().Count;

    public override ThisInternalSyntaxNode? Visit(ThisInternalSyntaxNode? node)
    {
        if (node is not null && !_found)
        {
            _count--;
            if (_count == 0)
            {
                if (node is SyntaxToken token)
                {
                    Debug.Assert(token == _oldToken);
                    _found = true;
                    return _newToken;
                }

                _count += CountNonNullSlots(node);
                return base.Visit(node);
            }
        }

        return node;
    }
}
