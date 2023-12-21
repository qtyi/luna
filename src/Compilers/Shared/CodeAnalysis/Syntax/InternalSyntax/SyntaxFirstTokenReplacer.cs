// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;

using ThisInternalSyntaxNode = Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax.MoonScriptSyntaxNode;
#endif

internal class SyntaxFirstTokenReplacer :
#if LANG_LUA
    LuaSyntaxRewriter
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxRewriter
#endif
{
    private readonly SyntaxToken _oldToken;
    private readonly SyntaxToken _newToken;
    private readonly int _diagnosticOffsetDelta;
    private bool _foundOldToken = false;

    private SyntaxFirstTokenReplacer(SyntaxToken oldToken, SyntaxToken newToken, int diagnosticOffsetDelta)
    {
        this._oldToken = oldToken;
        this._newToken = newToken;
        this._diagnosticOffsetDelta = diagnosticOffsetDelta;
    }

    internal static TRoot Replace<TRoot>(TRoot root, SyntaxToken newToken, int diagnosticOffsetDelta)
        where TRoot : ThisInternalSyntaxNode
    {
        var oldToken = root as SyntaxToken ?? root.GetLastToken();
        Debug.Assert(oldToken is not null);
        return Replace(root, oldToken, newToken, diagnosticOffsetDelta);
    }

    internal static TRoot Replace<TRoot>(TRoot root, SyntaxToken oldToken, SyntaxToken newToken, int diagnosticOffsetDelta)
        where TRoot : ThisInternalSyntaxNode
    {
        var replacer = new SyntaxFirstTokenReplacer(oldToken, newToken, diagnosticOffsetDelta);
        var newRoot = (TRoot)replacer.Visit(root)!;
        Debug.Assert(replacer._foundOldToken);
        return newRoot;
    }

    public override ThisInternalSyntaxNode? Visit(ThisInternalSyntaxNode? node)
    {
        if (node is not null && !this._foundOldToken)
        {
            if (node is SyntaxToken token)
            {
                Debug.Assert(token == this._oldToken);
                this._foundOldToken = true;
                return _newToken;
            }
            else
                return UpdateDiagnosticOffset(base.Visit(node)!, this._diagnosticOffsetDelta);
        }

        return node;
    }

    private static TSyntax UpdateDiagnosticOffset<TSyntax>(TSyntax node, int diagnosticOffsetDelta) where TSyntax : ThisInternalSyntaxNode
    {
        var oldDiagnostics = node.GetDiagnostics();
        var numDiagnostics = oldDiagnostics.Length;
        if (numDiagnostics == 0) return node;

        var newDiagnostics = new DiagnosticInfo[numDiagnostics];
        for (var i = 0; i < numDiagnostics; i++)
        {
            var oldDiagnostic = oldDiagnostics[i];
            newDiagnostics[i] = oldDiagnostic is SyntaxDiagnosticInfo oldSyntaxDiagnostic ?
                new SyntaxDiagnosticInfo(
                    oldSyntaxDiagnostic.Offset + diagnosticOffsetDelta,
                    oldSyntaxDiagnostic.Width,
                    (ErrorCode)oldSyntaxDiagnostic.Code,
                    oldSyntaxDiagnostic.Arguments
                ) :
                oldDiagnostic;
        }
        return node.WithDiagnosticsGreen(newDiagnostics);
    }
}
