// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;

using ThisSyntaxNode = LuaSyntaxNode;
using ThisSyntaxTree = LuaSyntaxTree;
using ThisParseOptions = LuaParseOptions;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;

using ThisSyntaxNode = MoonScriptSyntaxNode;
using ThisSyntaxTree = MoonScriptSyntaxTree;
using ThisParseOptions = MoonScriptParseOptions;
#endif

public partial class
#if LANG_LUA
    LuaSyntaxTree
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxTree
#endif
{
    internal sealed class DummySyntaxTree : ThisSyntaxTree
    {
        private readonly Syntax.ChunkSyntax _node;

        public override Encoding? Encoding => Encoding.UTF8;

        public override int Length => 0;

        public override ThisParseOptions Options => ThisParseOptions.Default;

        public override string FilePath => string.Empty;

        public override bool HasCompilationUnitRoot => true;

        public DummySyntaxTree() =>
            this._node = this.CloneNodeAsRoot(SyntaxFactory.ParseCompilationUnit(string.Empty));

        public override string ToString() => string.Empty;

        public override SourceText GetText(CancellationToken cancellationToken = default) => SourceText.From(string.Empty, Encoding.UTF8);

        public override bool TryGetText(out SourceText text)
        {
            text = SourceText.From(string.Empty, Encoding.UTF8);
            return true;
        }

        public override SyntaxReference GetReference(SyntaxNode node) => new SimpleSyntaxReference(node);

        public override ThisSyntaxNode GetRoot(CancellationToken cancellationToken = default) => this._node;

        public override bool TryGetRoot(out ThisSyntaxNode root)
        {
            root = this._node;
            return true;
        }

        public override FileLinePositionSpan GetLineSpan(TextSpan span, CancellationToken cancellationToken = default) => default;

        public override SyntaxTree WithRootAndOptions(SyntaxNode root, ParseOptions options) => SyntaxFactory.SyntaxTree(root, options: options, path: this.FilePath);

        public override SyntaxTree WithFilePath(string path) => SyntaxFactory.SyntaxTree(this._node, options: this.Options, path: path);
    }
}
