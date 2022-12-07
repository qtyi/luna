using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Xml.Linq;
using Luna.Compilers.Simulators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using Microsoft.Win32;

namespace Luna.Compilers.Tools;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private ILexerSimulator _lexerSimulator;
    private ILanguageParserSimulator _languageParserSimulator;

    public MainWindow()
    {
        InitializeComponent();
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new OpenFileDialog()
        {
            CheckFileExists = true,
            CheckPathExists = true,
            Filter = "Lua 文件 (*.lua)|*.lua|MoonScript 文件 (*.moon)|*.moon|所有文件(*.*)|*.*",
            FilterIndex = 3,
            Multiselect = false,
            ShowReadOnly = false,
            Title = "打开文件",
            ValidateNames = true
        };
        if (dialog.ShowDialog() == true)
        {
            var extension = Path.GetExtension(dialog.FileName);
            if (
                Simulator.TryGetLexerSimulatorByFileExtension(extension, out var lexerSimulators) &&
                Simulator.TryGetLanguageParserSimulatorByFileExtension(extension, out var languageParserSimulators)
            )
            {
                this._lexerSimulator = lexerSimulators[0];
                this._languageParserSimulator = languageParserSimulators[0];
                var sourceText = SourceText.From(dialog.OpenFile());
                var tree = this._languageParserSimulator.ParseSyntaxTree(sourceText);
                this.FillinSyntaxTree(tree);
                this.FillInDocument(sourceText);
                this.ShowProperties();
            }
        }
    }

    private void FillinSyntaxTree(SyntaxTree tree)
    {
        this.treeView.Items.Clear();
        this.treeView.Items.Add(ProcessNodeOrToken(tree.GetRoot()));

        TreeViewItem ProcessNodeOrToken(SyntaxNodeOrToken nodeOrToken)
        {
            var item = new TreeViewItem
            {
                Header = $"{this._languageParserSimulator.GetKindText(nodeOrToken.RawKind)} {nodeOrToken.FullSpan}",
                Foreground = nodeOrToken.IsNode ? Brushes.DarkBlue : Brushes.DarkGreen
            };

            if (nodeOrToken.IsToken)
            {
                var token = (SyntaxToken)nodeOrToken;
                item.Tag = token;
                foreach (var trivia in token.LeadingTrivia)
                    item.Items.Add(ProcessTrivia(trivia, trailing: false));
                foreach (var trivia in token.TrailingTrivia)
                    item.Items.Add(ProcessTrivia(trivia, trailing: true));
            }
            else if (nodeOrToken.IsNode)
            {
                var node = (SyntaxNode)nodeOrToken!;
                item.Tag = node;
                foreach (var child in node.ChildNodesAndTokens())
                    item.Items.Add(ProcessNodeOrToken(child));
            }
            return item;
        }
        TreeViewItem ProcessTrivia(SyntaxTrivia trivia, bool trailing)
        {
            var item = new TreeViewItem
            {
                Header = $"{(trailing ? "Trail" : "Lead")}: {this._languageParserSimulator.GetKindText(trivia.RawKind)} {trivia.FullSpan}",
                Foreground = Brushes.OrangeRed,
                Tag = trivia
            };
            return item;
        }
    }

    private void FillInDocument(SourceText sourceText)
    {
        var block = new Paragraph();
        var document = new FlowDocument(block)
        {
            FontFamily = new("Fira Code")
        };

        foreach (var token in this._lexerSimulator.LexToEnd(sourceText))
        {
            processTriviaList(token.LeadingTrivia);
            append(makeRun(this._lexerSimulator.GetTokenKind(token.RawKind), token.GetDiagnostics(), token.Text));
            processTriviaList(token.TrailingTrivia);

            void processTriviaList(SyntaxTriviaList triviaList)
            {
                foreach (var trivia in triviaList)
                {
                    var run = makeRun(this._lexerSimulator.GetTokenKind(trivia.RawKind), Enumerable.Empty<Diagnostic>(), trivia.ToString());
                    append(run);
                }
            }
        }

        this.docViewer.Document = document;

        Run makeRun(TokenKind kind, IEnumerable<Diagnostic> diagnostics, string text)
        {
            var run = new Run()
            {
                Text = text,
                Foreground = kind switch
                {
                    TokenKind.None => Brushes.Black,
                    TokenKind.Keyword => Brushes.Blue,
                    TokenKind.Identifier => Brushes.CornflowerBlue,
                    TokenKind.Operator => Brushes.DarkBlue,
                    TokenKind.Punctuation => Brushes.DarkSlateBlue,
                    TokenKind.NumericLiteral => Brushes.Goldenrod,
                    TokenKind.StringLiteral => Brushes.DarkRed,
                    TokenKind.WhiteSpace => Brushes.Transparent,
                    TokenKind.Comment => Brushes.OliveDrab,
                    TokenKind.Documentation => Brushes.Olive,
                    TokenKind.Skipped => Brushes.DarkGray,
                    _ => Brushes.Black,
                },
                Background = kind switch
                {
                    _ => Brushes.Transparent
                },
            };
            foreach (var diagnostic in diagnostics)
            {
                var decoration = new TextDecoration()
                {
                    Location = TextDecorationLocation.Underline,
                    Pen = new()
                    {
                        Brush = diagnostic.Severity switch
                        {
                            DiagnosticSeverity.Error => Brushes.Red,
                            DiagnosticSeverity.Warning => Brushes.Green,
                            DiagnosticSeverity.Info => Brushes.Blue,
                            DiagnosticSeverity.Hidden => Brushes.LightGray,
                            _ => Brushes.Transparent
                        },
                        Thickness = 5D
                    }
                };
                run.TextDecorations.Add(decoration);
            }
            return run;
        }
        void append(Run run) => block.Inlines.Add(run);
    }

    private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        var nodeOrTokenOrTrivia = (e.NewValue as TreeViewItem)?.Tag;
        this.ShowProperties(nodeOrTokenOrTrivia);
    }

    private void ShowProperties(object? nodeOrTokenOrTrivia = null)
    {
        this.gridBasicProperties.RowDefinitions.Clear();
        this.gridBasicProperties.Children.Clear();

        this.gridProperties.RowDefinitions.Clear();
        this.gridProperties.Children.Clear();

        if (nodeOrTokenOrTrivia is null) return;

        {
            var basicProperties = new Dictionary<string, string>();
            var properties = nodeOrTokenOrTrivia.GetType()
                .GetProperties()
                .Where(pi => pi.CanRead && !pi.IsSpecialName)
                .OrderBy(pi => pi.Name)
                .ToDictionary(
                    pi => pi.Name,
                    pi => pi.GetValue(nodeOrTokenOrTrivia)?.ToString()
                );
            if (nodeOrTokenOrTrivia is SyntaxNode)
            {
                var node = (SyntaxNode)nodeOrTokenOrTrivia;
                basicProperties.Add("类型", node.GetType().Name);
                basicProperties.Add("种类", this._languageParserSimulator.GetKindText(node.RawKind));
            }
            else if (nodeOrTokenOrTrivia is SyntaxToken)
            {
                var token = (SyntaxToken)nodeOrTokenOrTrivia;
                basicProperties.Add("类型", nameof(SyntaxToken));
                basicProperties.Add("种类", this._languageParserSimulator.GetKindText(token.RawKind));
            }
            else if (nodeOrTokenOrTrivia is SyntaxTrivia)
            {
                var trivia = (SyntaxTrivia)nodeOrTokenOrTrivia;
                basicProperties.Add("类型", nameof(SyntaxTrivia));
                basicProperties.Add("种类", this._languageParserSimulator.GetKindText(trivia.RawKind));
            }

            UpdateBasicProperties();
            UpdateProperties();

            void UpdateBasicProperties()
            {
                var index = 0;
                foreach (var basicProperty in basicProperties)
                {
                    var key = new TextBlock()
                    {
                        Text = basicProperty.Key,
                        HorizontalAlignment = HorizontalAlignment.Center
                    };
                    this.gridBasicProperties.Children.Add(key);
                    Grid.SetRow(key, index);
                    Grid.SetColumn(key, 0);

                    var value = new TextBlock() { Text = basicProperty.Value };
                    this.gridBasicProperties.Children.Add(value);
                    Grid.SetRow(value, index);
                    Grid.SetColumn(value, 1);

                    index++;
                }
                for (var i = 0; i < index; i++)
                {
                    this.gridBasicProperties.RowDefinitions.Add(new() { Height = new(18D) });
                }
            }
            void UpdateProperties()
            {
                var index = 0;
                foreach (var property in properties)
                {
                    if (property.Value is null) continue;

                    var key = new TextBlock()
                    {
                        Text = property.Key,
                        Padding = new(2D, 1D, 20D, 1D)
                    };
                    this.gridProperties.Children.Add(key);
                    Grid.SetRow(key, index);
                    Grid.SetColumn(key, 0);

                    var value = new TextBlock()
                    {
                        Text = property.Value,
                        Padding = new(2D, 1D, 2D, 1D)
                    };
                    this.gridProperties.Children.Add(value);
                    Grid.SetRow(value, index);
                    Grid.SetColumn(value, 1);

                    index++;
                }
                for (var i = 0; i < index; i++)
                {
                    this.gridProperties.RowDefinitions.Add(new() { Height = new(18D) });
                }
            }
        }

        {
            var document = this.docViewer.Document;

            TextSpan span = default;
            if (nodeOrTokenOrTrivia is SyntaxNode)
            {
                var node = (SyntaxNode)nodeOrTokenOrTrivia;
                span = node.Span;
            }
            else if (nodeOrTokenOrTrivia is SyntaxToken)
            {
                var token = (SyntaxToken)nodeOrTokenOrTrivia;
                span = token.Span;
            }
            else if (nodeOrTokenOrTrivia is SyntaxTrivia)
            {
                var trivia = (SyntaxTrivia)nodeOrTokenOrTrivia;
                span = trivia.FullSpan;
            }

            this.HighlightTextRange(document,
                new TextRange(GetAbsoluteCharaterPosition(span.Start), GetAbsoluteCharaterPosition(span.End))
            );

            TextPointer GetAbsoluteCharaterPosition(int offset)
            {
                Debug.Assert(offset >= 0, "偏移量为负数。");

                var pointer = document.ContentStart;
                while (pointer is not null)
                {
                    if (pointer.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.ElementStart)
                    {
                        if (pointer.Parent is Run run)
                        {
                            var length = run.Text.Length;
                            if (length <= offset)
                            {
                                offset -= length;
                            }
                            else
                            {
                                pointer = run.ContentStart.GetPositionAtOffset(offset);
                                break;
                            }
                        }
                    }

                    pointer = pointer.GetNextContextPosition(LogicalDirection.Forward);
                }

                if (pointer is null)
                {
                    Debug.Assert(offset == 0, "偏移量超出文本长度。");
                    pointer = document.ContentEnd;
                }
                return pointer;
            }
        }
    }

    private TextRange? _textRange;
    private void HighlightTextRange(FlowDocument document, TextRange? newRange = null)
    {
        if (this._textRange is not null)
        {
            this._textRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Transparent);
            this._textRange = null;
        }

        if (newRange is not null)
        {
            this._textRange = newRange;
            newRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.LightGray);
        }
    }
}
