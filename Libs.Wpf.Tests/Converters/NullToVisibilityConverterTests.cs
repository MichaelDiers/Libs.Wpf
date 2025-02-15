namespace Libs.Wpf.Tests.Converters;

using System.Globalization;
using System.Windows;
using Libs.Wpf.Converters;

/// <summary>
///     Tests of <see cref="NullToVisibilityConverter" />.
/// </summary>
public class NullToVisibilityConverterTests
{
    [Theory]
    [InlineData(
        null,
        Visibility.Collapsed)]
    [InlineData(
        "not null",
        Visibility.Visible)]
    [InlineData(
        false,
        Visibility.Visible)]
    public void Convert(object? value, Visibility expectedVisibility)
    {
        Assert.Equal(
            expectedVisibility,
            new NullToVisibilityConverter().Convert(
                value,
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }

    [Fact]
    public void ConvertBack_ThrowsNotImplementedException()
    {
        Assert.Throws<NotImplementedException>(
            () => new NullToVisibilityConverter().ConvertBack(
                null,
                typeof(int),
                null,
                CultureInfo.CurrentCulture));
    }
}
