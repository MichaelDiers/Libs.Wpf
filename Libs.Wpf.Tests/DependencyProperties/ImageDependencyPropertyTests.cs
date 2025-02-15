namespace Libs.Wpf.Tests.DependencyProperties;

using System.Windows;
using System.Windows.Media.Imaging;
using Libs.Wpf.DependencyProperties;

/// <summary>
///     Tests of <see cref="ImageDependencyProperty" />
/// </summary>
public class ImageDependencyPropertyTests
{
    [Fact]
    public void GetImage()
    {
        var dependencyObject = new DependencyObject();
        var expected = new BitmapImage();

        ImageDependencyProperty.SetImage(
            dependencyObject,
            expected);

        var actual = ImageDependencyProperty.GetImage(dependencyObject);

        Assert.Equal(
            expected,
            actual);
    }

    [Fact]
    public void GetImage_ShouldReturnNull_WhenNotSet()
    {
        var dependencyObject = new DependencyObject();

        var actual = ImageDependencyProperty.GetImage(dependencyObject);

        Assert.Null(actual);
    }

    [Fact]
    public void SetImage()
    {
        var dependencyObject = new DependencyObject();

        ImageDependencyProperty.SetImage(
            dependencyObject,
            new BitmapImage());
    }
}
