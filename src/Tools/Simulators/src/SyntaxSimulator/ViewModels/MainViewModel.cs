// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Luna.Compilers.Simulators;
using Luna.Compilers.Simulators.Syntax;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Win32;

namespace Luna.Compilers.Tools.ViewModels;

[ObservableObject]
internal partial class MainViewModel
{
    [ObservableProperty]
    private ISyntaxInfoProvider? syntaxInfoProvider = null;

    [ObservableProperty]
    private SourceText? sourceText = null;

    [RelayCommand]
    private async Task OpenSourceAsync(CancellationToken cancellationToken)
    {
        var filter = (from pair in Simulator.s_fileExtensionMap
                      let extension = pair.Key
                      from name in pair.Value
                      group extension by name)
                      .OrderBy(static group => group.Key)
                      .Select(group => (Language: $"{group.Key} file", Extensions: (IEnumerable<string>)group.OrderBy(e => e.ToLower())))
                      .ToList();
        filter.Add(("All files", new[] { ".*" }));
        var dialog = new OpenFileDialog()
        {
            CheckFileExists = true,
            CheckPathExists = true,
            Filter = string.Join("|",
                from f in filter
                let list = from e in f.Extensions select $"*{e}"
                select $"{f.Language} ({string.Join(";", list)})|{string.Join("|", list)}"),
            FilterIndex = filter.Count,
            Multiselect = false,
            ShowReadOnly = false,
            Title = "Open",
            ValidateNames = true
        };
        if (dialog.ShowDialog() == true)
        {
            var text = SourceText.From(dialog.OpenFile());
            var extension = Path.GetExtension(dialog.FileName);
            var type = Simulator.GetExportedComponents<ISyntaxParser>(extension).FirstOrDefault();
            if (type is null)
            {
#warning TODO: Do something if there is no proper SyntaxParser.
                return;
            }
            var syntaxParser = Activator.CreateInstance(type) as ISyntaxParser;
            Debug.Assert(syntaxParser != null);

            var initializationContext = new SyntaxParserInitializationContext();
            syntaxParser.Initialize(initializationContext);

            var executionContext = new SyntaxParserExecutionContext(
                options: new Dictionary<string, object?>()
                {
                    { "LanguageVersion", 0 },
                    { nameof(ParseOptions.Kind), SourceCodeKind.Regular },
                    { nameof(ParseOptions.DocumentationMode), DocumentationMode.Parse }
                },
                sourceText: text,
                filePath: Path.GetFullPath(dialog.FileName),
                cancellationToken: cancellationToken);
            var tree = syntaxParser.Parse(executionContext);
            if (tree is null)
            {
#warning TODO: Do something if we cannot get syntax tree.
                return;
            }
            this.SourceText = text;
            this.SyntaxInfoProvider = new SyntaxInfoProvider(tree, cancellationToken);

            type = Simulator.GetExportedComponents<ISyntaxClassifier>(extension).FirstOrDefault();
            if (type is null)
            {
#warning TODO: Do something if there is no proper SyntaxClassifier.
                return;
            }
            var syntaxClassifier = Activator.CreateInstance(type) as ISyntaxClassifier;
            Debug.Assert(syntaxClassifier != null);
            syntaxClassifier.Classify(new(this.SyntaxInfoProvider, cancellationToken));
        }
    }
}


internal sealed class SyntaxInfoProvider : ISyntaxInfoProvider
{
    private readonly SyntaxTree _tree;
    private readonly SyntaxNode _root;

    private readonly Dictionary<SyntaxToken, SyntaxTokenInfo> _tokenMap = new();
    private readonly Dictionary<SyntaxTrivia, SyntaxTriviaInfo> _triviaMap = new();
    private readonly Dictionary<SyntaxNode, SyntaxNodeInfo> _nodeMap = new();

    public SyntaxNodeInfo Root => this.VisitNode(this._root);

    public SyntaxInfoProvider(SyntaxTree tree, CancellationToken cancellationToken)
    {
        this._tree = tree;
        this._root = tree.GetRoot(cancellationToken);
    }

    public bool TryGetSyntaxNodeInfo(SyntaxNode node, [NotNullWhen(true)] out SyntaxNodeInfo? info)
    {
        if (!ReferenceEquals(this._tree, node.SyntaxTree))
        {
            info = null;
            return false;
        }

        if (this._nodeMap.TryGetValue(node, out info))
            return true;

        info = VisitNode(node);
        return true;
    }

    public bool TryGetSyntaxTokenInfo(SyntaxToken token, [NotNullWhen(true)] out SyntaxTokenInfo? info)
    {
        if (!ReferenceEquals(this._tree, token.SyntaxTree))
        {
            info = null;
            return false;
        }

        if (this._tokenMap.TryGetValue(token, out info))
            return true;

        info = VisitToken(token);
        return true;
    }

    public bool TryGetSyntaxTriviaInfo(SyntaxTrivia trivia, [NotNullWhen(true)] out SyntaxTriviaInfo? info)
    {
        if (!ReferenceEquals(this._tree, trivia.SyntaxTree))
        {
            info = null;
            return false;
        }

        if (this._triviaMap.TryGetValue(trivia, out info))
            return true;

        info = VisitTrivia(trivia);
        return true;
    }

    private SyntaxNodeInfo VisitNode(SyntaxNode node)
    {
        if (node.Parent is null)
        {
            // Is structured trivia.
            if (node.ParentTrivia != default)
            {
                var baseInfo = this.VisitTrivia(node.ParentTrivia);
                Debug.Assert(baseInfo.HasStructure);
                var structure = baseInfo.Structure;
                this._nodeMap.TryAdd(structure.Node, structure);
            }
            // Is root.
            else
                return this._nodeMap.GetOrAdd(node, (SyntaxNodeInfo)node);
        }
        else
        {
            var baseInfo = this.VisitNode(node.Parent!);
            foreach (var nodeOrToken in baseInfo.ChildNodesAndTokens)
            {
                if (nodeOrToken.IsNode)
                {
                    var childNode = (SyntaxNodeInfo)nodeOrToken;
                    this._nodeMap.TryAdd(childNode.Node, childNode);
                }
                else
                {
                    var childToken = (SyntaxTokenInfo)nodeOrToken;
                    this._tokenMap.TryAdd(childToken.Token, childToken);
                }
            }
            Debug.Assert(this._nodeMap.ContainsKey(node));
        }

        return this._nodeMap[node];
    }

    private SyntaxTokenInfo VisitToken(SyntaxToken token)
    {
        var baseInfo = this.VisitNode(token.Parent!);
        foreach (var nodeOrToken in baseInfo.ChildNodesAndTokens)
        {
            if (nodeOrToken.IsNode)
            {
                var childNode = (SyntaxNodeInfo)nodeOrToken;
                this._nodeMap.TryAdd(childNode.Node, childNode);
            }
            else
            {
                var childToken = (SyntaxTokenInfo)nodeOrToken;
                this._tokenMap.TryAdd(childToken.Token, childToken);
            }
        }
        Debug.Assert(this._tokenMap.ContainsKey(token));

        return this._tokenMap[token];
    }

    private SyntaxTriviaInfo VisitTrivia(SyntaxTrivia trivia)
    {
        var baseInfo = this.VisitToken(trivia.Token);
        foreach (var leading in baseInfo.LeadingTrivia)
            this._triviaMap.TryAdd(leading.Trivia, leading);
        foreach (var trailing in baseInfo.TrailingTrivia)
            this._triviaMap.TryAdd(trailing.Trivia, trailing);
        Debug.Assert(this._triviaMap.ContainsKey(trivia));

        return this._triviaMap[trivia];
    }
}
