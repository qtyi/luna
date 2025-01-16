// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

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
    private sealed class LazySyntaxTree : ThisSyntaxTree
    {
        private readonly SourceText _text;
        private readonly ThisParseOptions _options;
        private readonly string _path;
        private ThisSyntaxNode? _lazyRoot;

        public override Encoding? Encoding => _text.Encoding;
        public override string FilePath => _path;
        public override int Length => _text.Length;
        public override bool HasCompilationUnitRoot => true;
        public override ThisParseOptions Options => _options;

        internal LazySyntaxTree(
            SourceText text,
            ThisParseOptions options,
            string? path
        )
        {
            _text = text;
            _options = options;
            _path = path ?? String.Empty;
        }

        public override SourceText GetText(CancellationToken cancellationToken = default) => _text;

        public override bool TryGetText([NotNullWhen(true)] out SourceText? text)
        {
            text = _text;
            return true;
        }

        public override ThisSyntaxNode GetRoot(CancellationToken cancellationToken = default)
        {
            if (_lazyRoot is null)
            {
                var tree = SyntaxFactory.ParseSyntaxTree(_text, _options, _path, cancellationToken);
                var root = CloneNodeAsRoot((ThisSyntaxNode)tree.GetRoot(cancellationToken));

                Interlocked.CompareExchange(ref _lazyRoot, root, null);
            }

            return _lazyRoot;
        }

        public override bool TryGetRoot([NotNullWhen(true)] out ThisSyntaxNode? root)
        {
            root = _lazyRoot;
            return root is not null;
        }

        public override SyntaxReference GetReference(SyntaxNode node) => new SimpleSyntaxReference(node);

        public override SyntaxTree WithRootAndOptions(SyntaxNode root, Microsoft.CodeAnalysis.ParseOptions options)
        {
            if (ReferenceEquals(_lazyRoot, root) && ReferenceEquals(_options, options)) return this;

            return new ParsedSyntaxTree(
                text: null,
                _text.Encoding,
                _text.ChecksumAlgorithm,
                _path,
                (ThisParseOptions)Options,
                (ThisSyntaxNode)root,
                cloneRoot: true
            );
        }

        public override SyntaxTree WithFilePath(string path)
        {
            if (_path == path) return this;

            if (TryGetRoot(out var root))
                return new ParsedSyntaxTree(
                    _text,
                    _text.Encoding,
                    _text.ChecksumAlgorithm,
                    _path,
                    _options,
                    root,
                    cloneRoot: true);
            else
                return new LazySyntaxTree(
                    _text,
                    _options,
                    _path);
        }
    }
}
