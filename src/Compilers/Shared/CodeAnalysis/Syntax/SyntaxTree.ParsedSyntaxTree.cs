// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

extern alias MSCA;

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using MSCA::Microsoft.CodeAnalysis;
using MSCA::Microsoft.CodeAnalysis.Text;
#if !NETCOREAPP
using NotNullWhenAttribute = MSCA::System.Diagnostics.CodeAnalysis.NotNullWhenAttribute;
#endif
#if !NETCOREAPP || NETCOREAPP3_1
using MemberNotNullAttribute = MSCA::System.Diagnostics.CodeAnalysis.MemberNotNullAttribute;
#endif

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
        public override ThisParseOptions Options => this._options;
        /// <inheritdoc/>
        public override string FilePath => this._path;
        /// <inheritdoc/>
        public override bool HasCompilationUnitRoot => this._hasCompilationUnitRoot;
        /// <inheritdoc/>
        public override Encoding? Encoding => this._encoding;
        /// <inheritdoc/>
        public override int Length => this._root.FullSpan.Length;

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

            this._lazyText = text;
            this._encoding = encoding ?? text?.Encoding;
            this._checksumAlgorithm = checksumAlgorithm;
            this._options = options;
            this._path = path ?? string.Empty;
            this._root = cloneRoot ? this.CloneNodeAsRoot(root) : root;
            this._hasCompilationUnitRoot = root.Kind() == SyntaxKind.Chunk; // 基于Lua的语言的编译单元是Chunk。
        }

        [MemberNotNull(nameof(ParsedSyntaxTree._lazyText))]
        public override SourceText GetText(CancellationToken cancellationToken = default)
        {
            if (this._lazyText is null)
                Interlocked.Exchange(
                    ref this._lazyText,
                    this.GetRoot(cancellationToken).GetText(this._encoding, this._checksumAlgorithm));

            return this._lazyText;
        }

        public override bool TryGetText([NotNullWhen(true)] out SourceText? text)
        {
            text = this._lazyText;
            return text is not null;
        }

        public override ThisSyntaxNode GetRoot(CancellationToken cancellationToken = default) => this._root;

        public override bool TryGetRoot(out ThisSyntaxNode root)
        {
            root = this._root;
            return true;
        }

        public override SyntaxReference GetReference(SyntaxNode node) => new SimpleSyntaxReference(node);

        public override SyntaxTree WithRootAndOptions(SyntaxNode root, ParseOptions options)
        {
            if (object.ReferenceEquals(this._root, root) && object.ReferenceEquals(this._options, options))
                return this;
            else
                return new ParsedSyntaxTree(
                    text: null,
                    encoding: this._encoding,
                    checksumAlgorithm: this._checksumAlgorithm,
                    path: this._path,
                    options: (ThisParseOptions)options,
                    root: (ThisSyntaxNode)root,
                    cloneRoot: true);
        }

        public override SyntaxTree WithFilePath(string path)
        {
            if (this._path == path)
                return this;
            else
                return new ParsedSyntaxTree(
                    text: this._lazyText,
                    encoding: this._encoding,
                    checksumAlgorithm: this._checksumAlgorithm,
                    path: path,
                    options: this._options,
                    root: this._root,
                    cloneRoot: true);
        }
    }
}
