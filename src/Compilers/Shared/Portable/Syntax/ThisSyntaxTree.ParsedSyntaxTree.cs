// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

#if LANG_LUA
namespace Qtyi.CodeAnalysis.Lua;
#elif LANG_MOONSCRIPT
namespace Qtyi.CodeAnalysis.MoonScript;
#endif

partial class
#if LANG_LUA
    LuaSyntaxTree
#elif LANG_MOONSCRIPT
    MoonScriptSyntaxTree
#endif
{
    /// <summary>
    /// 已解析的语法树，此类的实例应仅由现有的根语法节点创建。
    /// </summary>
    private class ParsedSyntaxTree : ThisSyntaxTree
    {
        private readonly ThisParseOptions _options;
        private readonly string _path;
        private readonly ThisSyntaxNode _root;
        private readonly bool _hasCompilationUnitRoot;
        private readonly Encoding? _encoding;
        private readonly SourceHashAlgorithm _checksumAlgorithm;
        private SourceText? _lazyText;

        /// <inheritdoc/>
        public override ThisParseOptions Options => _options;
        /// <inheritdoc/>
        public override string FilePath => _path;
        /// <inheritdoc/>
        public override bool HasCompilationUnitRoot => _hasCompilationUnitRoot;
        /// <inheritdoc/>
        public override Encoding? Encoding => _encoding;
        /// <inheritdoc/>
        public override int Length => _root.FullSpan.Length;

        /// <summary>
        /// 创建<see cref="ParsedSyntaxTree"/>的新实例。
        /// </summary>
        /// <param name="text">包含的代码文本。</param>
        /// <param name="encoding">代码文本的编码。</param>
        /// <param name="checksumAlgorithm">代码文本的校验算法。</param>
        /// <param name="path">代码文件的路径。代码文本并非必须从文件中创建，只要此参数的值能在之后的流程中得到对应的处理即可。</param>
        /// <param name="options">解析器导出选项集。</param>
        /// <param name="root">要设置为根节点的语法节点。</param>
        /// <param name="cloneRoot">若要创建<paramref name="root"/>的副本作为根语法节点，则传入<see langword="true"/>；否则传入<see langword="false"/>。</param>
        /// <remarks>
        /// 当<paramref name="text"/>不为<see langword="null"/>时，<paramref name="encoding"/>和<paramref name="checksumAlgorithm"/>必须与<paramref name="text"/>的编码和校验算法一致。
        /// </remarks>
        internal ParsedSyntaxTree(
            SourceText? text,
            Encoding? encoding,
            SourceHashAlgorithm checksumAlgorithm,
            string? path,
            ThisParseOptions options,
            ThisSyntaxNode root,
            bool cloneRoot)
        {
            Debug.Assert(
                text is null ||
                text.Encoding == encoding &&
                text.ChecksumAlgorithm == checksumAlgorithm);

            _lazyText = text;
            _encoding = encoding ?? text?.Encoding;
            _checksumAlgorithm = checksumAlgorithm;
            _options = options;
            _path = path ?? string.Empty;
            _root = cloneRoot ? CloneNodeAsRoot(root) : root;
            _hasCompilationUnitRoot = root.Kind() == SyntaxKind.Chunk; // 基于Lua的语言的编译单元是Chunk。
        }

        [MemberNotNull(nameof(_lazyText))]
        public override SourceText GetText(CancellationToken cancellationToken = default)
        {
            if (_lazyText is null)
                Interlocked.Exchange(
                    ref _lazyText,
                    GetRoot(cancellationToken).GetText(_encoding, _checksumAlgorithm));

            return _lazyText;
        }

        public override bool TryGetText([NotNullWhen(true)] out SourceText? text)
        {
            text = _lazyText;
            return text is not null;
        }

        public override ThisSyntaxNode GetRoot(CancellationToken cancellationToken = default) => _root;

        public override bool TryGetRoot(out ThisSyntaxNode root)
        {
            root = _root;
            return true;
        }

        public override SyntaxReference GetReference(SyntaxNode node) => new SimpleSyntaxReference(node);

        public override SyntaxTree WithRootAndOptions(SyntaxNode root, Microsoft.CodeAnalysis.ParseOptions options)
        {
            if (ReferenceEquals(_root, root) && ReferenceEquals(_options, options))
                return this;
            else
                return new ParsedSyntaxTree(
                    text: null,
                    encoding: _encoding,
                    checksumAlgorithm: _checksumAlgorithm,
                    path: _path,
                    options: (ThisParseOptions)options,
                    root: (ThisSyntaxNode)root,
                    cloneRoot: true);
        }

        public override SyntaxTree WithFilePath(string path)
        {
            if (_path == path)
                return this;
            else
                return new ParsedSyntaxTree(
                    text: _lazyText,
                    encoding: _encoding,
                    checksumAlgorithm: _checksumAlgorithm,
                    path: path,
                    options: _options,
                    root: _root,
                    cloneRoot: true);
        }
    }
}
