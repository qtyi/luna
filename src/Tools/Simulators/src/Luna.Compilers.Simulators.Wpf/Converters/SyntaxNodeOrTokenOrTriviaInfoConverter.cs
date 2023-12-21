// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace Luna.Compilers.Simulators.Converters;

public abstract class SyntaxNodeOrTokenOrTriviaInfoConverter<T> : MultiValueConverter<T>
{
    protected abstract object? Convert(bool isSelected, bool isHighlighted, object?[] values, object? parameter, CultureInfo culture);

    protected abstract object?[] ConvertBack(object? value, object? parameter, CultureInfo culture, out bool isSelected, out bool isHighlighted);

    protected override object? Convert(object?[] values, object? parameter, CultureInfo culture)
    {
        Debug.Assert(values.Length >= 2);
        Debug.Assert(values[0] is bool);
        Debug.Assert(values[1] is bool);

        var copyLength = values.Length - 2;
        var array = new object?[copyLength];
        Array.Copy(values, 2, array, 0, copyLength);
        return this.Convert((bool)values[0]!, (bool)values[1]!, array, parameter, culture);
    }

    protected override object?[] ConvertBack(object? value, object? parameter, CultureInfo culture)
    {
        var array = this.ConvertBack(value, parameter, culture, out var isSelected, out var isHighlighted);
        var result = new object?[array.Length + 2];
        result[0] = isSelected;
        result[1] = isHighlighted;
        array.CopyTo(result, 2);
        return result;
    }
}
