// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Diagnostics.CodeAnalysis;
#if !NETCOREAPP
using NotNullWhenAttribute = MSCA.System.Diagnostics.CodeAnalysis.NotNullWhenAttribute;
#endif

#if LANG_LUA
using SyntaxKind = Qtyi.CodeAnalysis.Lua.SyntaxKind;
#elif LANG_MOONSCRIPT
using SyntaxKind = Qtyi.CodeAnalysis.MoonScript.SyntaxKind;
#endif

namespace Microsoft.CodeAnalysis
{
    using MSCA::Microsoft.CodeAnalysis;

    public static partial class
#if LANG_LUA
        LuaExtensions
#elif LANG_MOONSCRIPT
        MoonScriptExtensions
#endif
    {
        public static bool IsKind(this SyntaxToken token, SyntaxKind kind) => token.RawKind == (int)kind;

        public static bool IsKind(this SyntaxTrivia trivia, SyntaxKind kind) => trivia.RawKind == (int)kind;

        public static bool IsKind([NotNullWhen(true)] this SyntaxNode? node, SyntaxKind kind) => node?.RawKind == (int)kind;

        public static bool IsKind(this SyntaxNodeOrToken nodeOrToken, SyntaxKind kind) => nodeOrToken.RawKind == (int)kind;

        internal static SyntaxKind ContextualKind(this SyntaxToken token) =>
            token.Language ==
#if LANG_LUA
                Qtyi.CodeAnalysis.LanguageNames.Lua
#elif LANG_MOONSCRIPT
                Qtyi.CodeAnalysis.LanguageNames.MoonScript
#endif
                ? (SyntaxKind)token.RawContextualKind : SyntaxKind.None;

        public static int IndexOf<TNode>(this SyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode => list.IndexOf((int)kind);

        public static bool Any<TNode>(this SyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode => list.IndexOf(kind) >= 0;

        public static int IndexOf<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode => list.IndexOf((int)kind);

        public static bool Any<TNode>(this SeparatedSyntaxList<TNode> list, SyntaxKind kind) where TNode : SyntaxNode => list.IndexOf(kind) >= 0;

        public static int IndexOf(this SyntaxTriviaList list, SyntaxKind kind) => list.IndexOf((int)kind);

        public static bool Any(this SyntaxTriviaList list, SyntaxKind kind) => list.IndexOf(kind) >= 0;

        public static int IndexOf(this SyntaxTokenList list, SyntaxKind kind) => list.IndexOf((int)kind);

        public static bool Any(this SyntaxTokenList list, SyntaxKind kind) => list.IndexOf(kind) >= 0;

        internal static SyntaxToken FirstOrDefault(this SyntaxTokenList list, SyntaxKind kind)
        {
            int index = list.IndexOf(kind);
            return (index >= 0) ? list[index] : default;
        }
    }
}

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript
#endif
{
    using MSCA::Microsoft.CodeAnalysis;

    public static class
#if LANG_LUA
        LuaExtensions
#elif LANG_MOONSCRIPT
        MoonScriptExtensions
#endif
    {
        public static SyntaxKind Kind(this SyntaxToken token) => (SyntaxKind)token.RawKind;

        public static SyntaxKind Kind(this SyntaxTrivia trivia) => (SyntaxKind)trivia.RawKind;

        public static SyntaxKind Kind(this SyntaxNode token) => (SyntaxKind)token.RawKind;

        public static SyntaxKind Kind(this SyntaxNodeOrToken nodeOrToken) => (SyntaxKind)nodeOrToken.RawKind;

        public static bool IsKeyword(this SyntaxToken token) => SyntaxFacts.IsKeywordKind(token.Kind());

        public static bool IsContextualKeyword(this SyntaxToken token) => SyntaxFacts.IsContextualKeyword(token.Kind());

        public static bool IsReservedKeyword(this SyntaxToken token) => SyntaxFacts.IsReservedKeyword(token.Kind());

#warning 未完成
    }
}
