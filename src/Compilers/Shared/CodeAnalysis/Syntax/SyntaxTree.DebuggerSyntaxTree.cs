// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisParseOptions = LuaParseOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisParseOptions = MoonScriptParseOptions;
#endif

public partial class
#if LANG_LUA
    LuaSyntaxTree
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxTree
#endif
{
    private sealed class DebuggerSyntaxTree : ParsedSyntaxTree
    {
        internal override bool SupportsLocations => true;

        internal DebuggerSyntaxTree(
            ThisSyntaxNode root,
            SourceText text,
            ThisParseOptions options
        ) : base(
            text,
            text.Encoding,
            text.ChecksumAlgorithm,
            path: string.Empty,
            options: options,
            root: root,
            cloneRoot: true)
        {
        }
    }
}
