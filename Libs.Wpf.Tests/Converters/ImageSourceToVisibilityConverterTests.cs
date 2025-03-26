namespace Libs.Wpf.Tests.Converters;

using System.Globalization;
using System.Windows;
using Libs.Wpf.Converters;

public class ImageSourceToVisibilityConverterTests
{
    private readonly ImageSourceToVisibilityConverter converter = new();

    [Fact]
    public void Convert_ShouldReturnCollapsed_WhenValueIsImageSource()
    {
        const string value = "image";
        Assert.Equal(
            Visibility.Visible,
            this.converter.Convert(
                value,
                typeof(Visibility),
                null,
                CultureInfo.CurrentCulture));
    }

    [Fact]
    public void Convert_ShouldReturnCollapsed_WhenValueIsNotImageSource()
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
