// Licensed to the Qtyi under one or more agreements.
// The Qtyi licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Luna.Compilers.Simulators.Converters;

public abstract class MultiValueConverter<T> : IMultiValueConverter
{
    protected abstract object? Convert(object?[] values, object? parameter, CultureInfo culture);

    protected abstract object?[] ConvertBack(object? value, object? parameter, CultureInfo culture);

    object? IMultiValueConverter.Convert(object?[] values, Type targetType, object? parameter, CultureInfo culture)
    {
        Debug.Assert(targetType.IsAssignableFrom(typeof(T)));
        return this.Convert(values, parameter, culture);
    }

    object?[] IMultiValueConverter.ConvertBack(object? value, Type[] targetTypes, object? parameter, CultureInfo culture)
    {
        Debug.Assert(value is null || typeof(T).IsAssignableFrom(value.GetType()) ||
            ReferenceEquals(value, DependencyProperty.UnsetValue) ||
            ReferenceEquals(value, Binding.DoNothing));

        // Initialize result array with DependencyProperty.UnsetValue.
        var length = targetTypes.Length;
        var result = new object?[length];
        for (var i = 0; i < length; i++) result[i] = DependencyProperty.UnsetValue;

        var array = this.ConvertBack(value, parameter, culture);
        var copyLength = Math.Min(array.Length, length);
        for (var i = 0; i < copyLength; i++)
        {
            var item = array[i];
            if (item is not null &&
                !ReferenceEquals(item, DependencyProperty.UnsetValue) &&
                !ReferenceEquals(item, Binding.DoNothing))
                Debug.Assert(targetTypes[i].IsAssignableFrom(item.GetType()));
        }
        Array.Copy(array, result, copyLength);

        return result;
    }
}
