// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Luna.Compilers.Simulators;
using Luna.Compilers.Simulators.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Tools.Converters;

internal sealed class SourceTextConverter : MarkupExtension, IMultiValueConverter
{
    public FlowDocument? Convert(SourceText? text, ISyntaxInfoProvider? syntaxInfoProvider, object? parameter, CultureInfo culture)
    {
        if (text is null) return null;

        var block = new Paragraph();
        var document = new FlowDocument(block) { FontFamily = new("Fira Code") };

        if (syntaxInfoProvider is null)
        {
            block.Inlines.Add(text.ToString());
            return document;
        }

        block.Inlines.Add(VisitNode(syntaxInfoProvider.Root));
        return document;

        Inline VisitNode(SyntaxNodeInfo node)
        {
            var span = new Span(); 
#warning Do some binding job.
            foreach (var nodeOrToken in node.ChildNodesAndTokens)
            {
                if (nodeOrToken.IsNode)
                    span.Inlines.Add(VisitNode((SyntaxNodeInfo)nodeOrToken));
                else
                    span.Inlines.Add(VisitToken((SyntaxTokenInfo)nodeOrToken));
            }
            return span;
        }

        Inline VisitToken(SyntaxTokenInfo token)
        {
            var span = new Span();
            var binding = CreateBinding();
            binding.Converter = SyntaxTokenInfoForegroundConverter.Instance;
            span.SetBinding(Span.ForegroundProperty, binding);
#warning Do some binding job.
            foreach (var leading in token.LeadingTrivia)
                span.Inlines.Add(VisitTrivia(leading));
            {
                var run = new Run(token.Token.Text);
#warning Do some binding job.
                span.Inlines.Add(run);
            }
            foreach (var trialing in token.TrailingTrivia)
                span.Inlines.Add(VisitTrivia(trialing));
            return span;

            MultiBinding CreateBinding()
            {
                var binding = new MultiBinding();
                binding.Bindings.Add(new Binding(nameof(SyntaxTokenInfo.IsSelected)) { Source = token, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTokenInfo.IsHighlighted)) { Source = token, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTokenInfo.TokenClassification)) { Source = token, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTokenInfo.IdentifierTokenClassification)) { Source = token, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTokenInfo.LiteralTokenClassification)) { Source = token, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTokenInfo.DirectiveTokenClassification)) { Source = token, Mode = BindingMode.OneWay });
                return binding;
            }
        }

        Inline VisitTrivia(SyntaxTriviaInfo trivia)
        {
            if (trivia.HasStructure)
                return VisitNode(trivia.Structure!);

            var run = new Run(trivia.Trivia.ToString());
            var binding = CreateBinding();
            binding.Converter = SyntaxTriviaInfoForegroundConverter.Instance;
            run.SetBinding(Run.ForegroundProperty, binding);
#warning Do some binding job.
            return run;

            MultiBinding CreateBinding()
            {
                var binding = new MultiBinding();
                binding.Bindings.Add(new Binding(nameof(SyntaxTriviaInfo.IsSelected)) { Source = trivia, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTriviaInfo.IsHighlighted)) { Source = trivia, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTriviaInfo.TriviaClassification)) { Source = trivia, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTriviaInfo.CommentTriviaClassification)) { Source = trivia, Mode = BindingMode.OneWay });
                binding.Bindings.Add(new Binding(nameof(SyntaxTriviaInfo.StructuredTriviaClassification)) { Source = trivia, Mode = BindingMode.OneWay });
                return binding;
            }
        }

#if false
        Run makeRun(TokenKind kind, string text) => new()
        {
            Foreground = kind switch
            {
                TokenKind.None => Brushes.Black,
                TokenKind.Keyword => Brushes.Blue,
                TokenKind.Operator => Brushes.DarkGray,
                TokenKind.Punctuation => Brushes.DarkBlue,
                TokenKind.NumericLiteral => Brushes.DarkOrange,
                TokenKind.StringLiteral => Brushes.DarkRed,
                TokenKind.WhiteSpace => Brushes.Transparent,
                TokenKind.Comment => Brushes.DarkGreen,
                TokenKind.Documentation => Brushes.DarkOliveGreen,
                _ => Brushes.Black,
            },
            Background = kind switch
            {
                _ => Brushes.Transparent
            }
        };
        void append(Run run) => block.Inlines.Add(run);
#endif
    }

    object? IMultiValueConverter.Convert(object?[] value, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Assert(value.Length >= 2);
        Debug.Assert(value[0] is null or SourceText);
        Debug.Assert(value[1] is null or ISyntaxInfoProvider);
        Debug.Assert(targetType.IsAssignableFrom(typeof(FlowDocument)));

        return this.Convert(value[0] as SourceText, value[1] as ISyntaxInfoProvider, parameter, culture);
    }

    object?[] IMultiValueConverter.ConvertBack(object? value, Type[] targetType, object? parameter, CultureInfo culture) => throw new NotSupportedException();

    public override object ProvideValue(IServiceProvider serviceProvider) => new SourceTextConverter();
}
