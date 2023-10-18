// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

#if DEBUG

using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using Qtyi.CodeAnalysis.Lua.Syntax;
using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxTree = LuaSyntaxTree;
using ThisSyntaxVisitor = LuaSyntaxVisitor;
using ThisSyntaxVisitor<TResult> = LuaSyntaxVisitor<TResult>;
using InternalSyntaxNode = Syntax.InternalSyntax.LuaSyntaxNode;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using Qtyi.CodeAnalysis.MoonScript.Syntax;
using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxTree = MoonScriptSyntaxTree;
using ThisSyntaxVisitor = MoonScriptSyntaxVisitor;
using ThisSyntaxVisitor<TResult> = MoonScriptSyntaxVisitor<TResult>;
using InternalSyntaxNode = Syntax.InternalSyntax.MoonScriptSyntaxNode;
#endif

partial class
#if LANG_LUA
        LuaSyntaxNode
#elif LANG_MOONSCRIPT
        MoonScriptSyntaxNode
#endif
{
    internal class MockNode : ThisSyntaxNode
    {
        public MockNode(InternalSyntaxNode green) : this(green, null, 0) { }
        public MockNode(InternalSyntaxNode green, ThisSyntaxNode? parent, int position) : base(green, parent, position) { }

        public override TResult? Accept<TResult>(ThisSyntaxVisitor<TResult> visitor) where TResult : default => default;

        public override void Accept(ThisSyntaxVisitor visitor) { }

        internal override SyntaxNode? GetCachedSlot(int index) => null;

        internal override SyntaxNode? GetNodeSlot(int slot) => null;
    }
}

#endif
