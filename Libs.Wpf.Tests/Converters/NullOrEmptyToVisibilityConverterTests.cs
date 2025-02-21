namespace Libs.Wpf.Tests.Converters;

using System.Globalization;
using System.Windows;
using Libs.Wpf.Converters;

public class NullOrEmptyToVisibilityConverterTests
{
    private readonly NullOrEmptyToVisibilityConverter converter = new();

    [Fact]
    public void Convert_ShouldReturnCollapsed_WhenValueIsEmptyString()
    {
        Assert.Equal(
            Visibility.Collapsed,
            this.converter.Convert(
                string.Empty,
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }

    [Fact]
    public void Convert_ShouldReturnCollapsed_WhenValueIsNotString()
    {
        Assert.Equal(
            Visibility.Collapsed,
            this.converter.Convert(
                true,
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }

    [Fact]
    public void Convert_ShouldReturnCollapsed_WhenValueIsNull()
    {
        Assert.Equal(
            Visibility.Collapsed,
            this.converter.Convert(
                null,
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }

    [Fact]
    public void Convert_ShouldReturnCollapsed_WhenValueIsWhiteSpace()
    {
        Assert.Equal(
            Visibility.Collapsed,
            this.converter.Convert(
                "  ",
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }

    [Fact]
    public void Convert_ShouldReturnVisible_WhenValueNonEmptyString()
    {
        Assert.Equal(
            Visibility.Visible,
            this.converter.Convert(
                "test",
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }

    [Fact]
    public void ConvertBack()
    {
        Assert.Throws<NotImplementedException>(
            () => this.converter.ConvertBack(
                null,
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }
}
