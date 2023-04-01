// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Runtime.CompilerServices;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax.InternalSyntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax.InternalSyntax;
#endif

internal partial class SyntaxToken
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected static void InitializeWithTrivia(
        SyntaxToken self,
        ref GreenNode? _leading, ref GreenNode? _trailing,
        GreenNode? leading = null, GreenNode? trailing = null
    )
    {
        if (leading is not null)
        {
            self.AdjustFlagsAndWidth(leading);
            _leading = leading;
        }
        if (trailing is not null)
        {
            self.AdjustFlagsAndWidth(trailing);
            _trailing = trailing;
        }
    }
}
