namespace Libs.Wpf.Tests.Converters;

using System.Globalization;
using System.Windows;
using Libs.Wpf.Converters;

/// <summary>
///     Tests of <see cref="BoolToVisibilityConverter" />.
/// </summary>
public class BoolToVisibilityConverterTests
{
    [Theory]
    [InlineData(
        null,
        Visibility.Collapsed)]
    [InlineData(
        "string",
        Visibility.Collapsed)]
    [InlineData(
        false,
        Visibility.Collapsed)]
    [InlineData(
        true,
        Visibility.Visible)]
    public void Convert(object? value, Visibility expectedVisibility)
    {
        Assert.Equal(
            expectedVisibility,
            new BoolToVisibilityConverter().Convert(
                value,
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }

    [Fact]
    public void ConvertBack_ThrowsNotImplementedException()
    {
        Assert.Throws<NotImplementedException>(
            () => new BoolToVisibilityConverter().ConvertBack(
                null,
                typeof(int),
                null,
                CultureInfo.CurrentCulture));
    }
}
