// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

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
    private sealed class LazySyntaxTree : ThisSyntaxTree
    {
        private readonly SourceText _text;
        private readonly ThisParseOptions _options;
        private readonly string _path;
        private ThisSyntaxNode? _lazyRoot;

        public override Encoding? Encoding => this._text.Encoding;
        public override string FilePath => this._path;
        public override int Length => this._text.Length;
        public override bool HasCompilationUnitRoot => true;
        public override ThisParseOptions Options => this._options;

        internal LazySyntaxTree(
            SourceText text,
            ThisParseOptions options,
            string? path
        )
        {
            this._text = text;
            this._options = options;
            this._path = path ?? String.Empty;
        }

        public override SourceText GetText(CancellationToken cancellationToken = default) => this._text;

        public override bool TryGetText([NotNullWhen(true)] out SourceText? text)
        {
            text = this._text;
            return true;
        }

        public override ThisSyntaxNode GetRoot(CancellationToken cancellationToken = default)
        {
            if (this._lazyRoot is null)
            {
                var tree = SyntaxFactory.ParseSyntaxTree(this._text, this._options, this._path, cancellationToken);
                var root = this.CloneNodeAsRoot((ThisSyntaxNode)tree.GetRoot(cancellationToken));

                Interlocked.CompareExchange(ref this._lazyRoot, root, null);
            }

            return this._lazyRoot;
        }

        public override bool TryGetRoot([NotNullWhen(true)] out ThisSyntaxNode? root)
        {
            root = this._lazyRoot;
            return root is not null;
        }

        public override SyntaxReference GetReference(SyntaxNode node) => new SimpleSyntaxReference(node);

        public override SyntaxTree WithRootAndOptions(SyntaxNode root, ParseOptions options)
        {
            if (object.ReferenceEquals(this._lazyRoot, root) && object.ReferenceEquals(this._options, options)) return this;

            return new ParsedSyntaxTree(
                text: null,
                this._text.Encoding,
                this._text.ChecksumAlgorithm,
                this._path,
                (ThisParseOptions)this.Options,
                (ThisSyntaxNode)root,
                cloneRoot: true
            );
        }

        public override SyntaxTree WithFilePath(string path)
        {
            if (this._path == path) return this;

            if (this.TryGetRoot(out var root))
                return new ParsedSyntaxTree(
                    this._text,
                    this._text.Encoding,
                    this._text.ChecksumAlgorithm,
                    this._path,
                    this._options,
                    root,
                    cloneRoot: true);
            else
                return new LazySyntaxTree(
                    this._text,
                    this._options,
                    this._path);
        }
    }
}
