// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using Luna.Compilers.Simulators.Converters;
using Roslyn.Utilities;

namespace Luna.Compilers.Tools.Converters;

internal sealed class SyntaxTokenInfoForegroundConverter : SyntaxTokenInfoConverter<Brush>
{
    private static readonly Lazy<SyntaxTokenInfoForegroundConverter> s_lazyInstance = new();
    public static SyntaxTokenInfoForegroundConverter Instance => s_lazyInstance.Value;

    protected override object? ConvertBad(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Black;

    protected override object? ConvertContextualKeyword(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Blue;

    protected override object? ConvertDisabledTextDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.LightGray;

    protected override object? ConvertDocumentation(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => new SolidColorBrush(Color.FromRgb(98, 151, 85));

    protected override object? ConvertEndOfDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertEndOfFile(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Black;

    protected override object? ConvertIdentifierDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertKeyword(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Blue;

    protected override object? ConvertKeywordDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertLeadingDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertLiteralDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertMessageTextDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertNormalIdentifier(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => new SolidColorBrush(Color.FromRgb(31, 55, 127));

    protected override object? ConvertNumericLiteral(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.DarkOrange;

    protected override object? ConvertOperator(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Black;

    protected override object? ConvertOperatorDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertPunctuation(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Black;

    protected override object? ConvertPunctuationDirective(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => Brushes.Gray;

    protected override object? ConvertStringLiteral(bool isSelected, bool isHighlighted, object? parameter, CultureInfo culture) => new SolidColorBrush(Color.FromRgb(163, 21, 21));

    protected override object?[] ConvertBack(object? value, object? parameter, CultureInfo culture, out bool isSelected, out bool isHighlighted) => throw ExceptionUtilities.Unreachable();
}
