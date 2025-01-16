// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Text;
using Microsoft.CodeAnalysis.Text;
using Microsoft.CodeAnalysis;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
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
            _node = CloneNodeAsRoot(SyntaxFactory.ParseCompilationUnit(string.Empty));

        public override string ToString() => string.Empty;

        public override SourceText GetText(CancellationToken cancellationToken = default) => SourceText.From(string.Empty, Encoding.UTF8);

        public override bool TryGetText(out SourceText text)
        {
            text = SourceText.From(string.Empty, Encoding.UTF8);
            return true;
        }

        public override SyntaxReference GetReference(SyntaxNode node) => new SimpleSyntaxReference(node);

        public override ThisSyntaxNode GetRoot(CancellationToken cancellationToken = default) => _node;

        public override bool TryGetRoot(out ThisSyntaxNode root)
        {
            root = _node;
            return true;
        }

        public override FileLinePositionSpan GetLineSpan(TextSpan span, CancellationToken cancellationToken = default) => default;

        public override SyntaxTree WithRootAndOptions(SyntaxNode root, Microsoft.CodeAnalysis.ParseOptions options) => SyntaxFactory.SyntaxTree(root, options: (ThisParseOptions)options, path: FilePath);

        public override SyntaxTree WithFilePath(string path) => SyntaxFactory.SyntaxTree(_node, options: Options, path: path);
    }
}
