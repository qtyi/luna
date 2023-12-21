// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Windows.Media;
using Luna.Compilers.Simulators.Converters;
using Roslyn.Utilities;

namespace Luna.Compilers.Tools.Converters;

internal sealed class SyntaxTriviaInfoForegroundConverter : SyntaxTriviaInfoConverter<Brush>
{
    private static readonly Lazy<SyntaxTriviaInfoForegroundConverter> s_lazyInstance = new();
    public static SyntaxTriviaInfoForegroundConverter Instance => s_lazyInstance.Value;

    protected override object? ConvertDirectiveTrivia(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertDocumentation(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => new SolidColorBrush(Color.FromRgb(98, 151, 85));

    protected override object? ConvertEndOfLine(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Black;

    protected override object? ConvertMultiLineComment(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Green;

    protected override object? ConvertSingleLineComment(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Green;

    protected override object? ConvertWhitespace(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Black;

    protected override object?[] ConvertBack(object? value, object? parameter, CultureInfo culture, out bool isSelected, out bool isHighlighted) => throw ExceptionUtilities.Unreachable();
}
