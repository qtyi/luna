using System.CodeDom;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using Luna.Compilers.Simulators;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace Luna.Compilers.Tools.Converters;

internal sealed class SourceTextConverter : MarkupExtension, IValueConverter
{
    public object? Convert(object? value, Type targetType, object parameter, CultureInfo? culture)
    {
        if (value is not SourceText sourceText) return null;
        if (parameter is not ILexerSimulator lexerSimulator) throw new ArgumentNullException(nameof(parameter));
        Debug.Assert(targetType == typeof(FlowDocument));

        var block = new Paragraph();
        var document = new FlowDocument(block);

        foreach (var token in lexerSimulator.LexToEnd(sourceText))
        {
            processTriviaList(token.LeadingTrivia);
            append(makeRun(lexerSimulator.GetTokenKind(token.RawKind), token.Text));
            processTriviaList(token.TrailingTrivia);

            void processTriviaList(SyntaxTriviaList triviaList)
            {
                foreach (var trivia in triviaList)
                {
                    var run = makeRun(lexerSimulator.GetTokenKind(trivia.RawKind), sourceText.GetSubText(trivia.Span).ToString());
                    append(run);
                }
            }
        }

        return document;

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
    }

    object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotSupportedException();

    public override object ProvideValue(IServiceProvider serviceProvider) => new SourceTextConverter();
}
