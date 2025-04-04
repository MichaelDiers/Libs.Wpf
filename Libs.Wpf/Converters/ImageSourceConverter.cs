namespace Libs.Wpf.Converters;

using System.Globalization;
using System.Windows;
using System.Windows.Data;

/// <summary>
///     Converts <c>null</c> and <see cref="string.Empty" /> to <see cref="DependencyProperty.UnsetValue" />.
/// </summary>
public class ImageSourceConverter : IValueConverter
{
    /// <summary>Converts a value.</summary>
    /// <param name="value">The value produced by the binding source.</param>
    /// <param name="targetType">The type of the binding target property.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
    public object? Convert(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return value is null || (value is string s && string.IsNullOrWhiteSpace(s))
            ? DependencyProperty.UnsetValue
            : value;
    }

    /// <summary>Converts a value.</summary>
    /// <param name="value">The value that is produced by the binding target.</param>
    /// <param name="targetType">The type to convert to.</param>
    /// <param name="parameter">The converter parameter to use.</param>
    /// <param name="culture">The culture to use in the converter.</param>
    /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
    /// <remarks>The method is not implemented.</remarks>
    public object? ConvertBack(
        object? value,
        Type targetType,
        object? parameter,
        CultureInfo culture
    )
    {
        return Binding.DoNothing;
    }
}
