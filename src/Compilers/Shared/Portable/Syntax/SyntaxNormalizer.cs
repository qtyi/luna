// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua.Syntax;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript.Syntax;
#endif

internal class SyntaxNormalizer : ThisSyntaxRewriter
{
    internal static TNode Normalize<TNode>(TNode node, string indentWhitespace, string eolWhitespace, bool useElasticTrivia = false)
    {
#warning 未实现。
        throw new NotImplementedException();
    }
}
