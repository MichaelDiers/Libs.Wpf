namespace Libs.Wpf.Tests.DependencyProperties;

using System.Windows;
using System.Windows.Media.Imaging;
using Libs.Wpf.DependencyProperties;

/// <summary>
///     Tests of <see cref="ImageSourceDependencyProperty" />
/// </summary>
public class ImageSourceDependencyPropertyTests
{
    [Fact]
    public void GetImage()
    {
        var dependencyObject = new DependencyObject();
        var expected = new BitmapImage();

        ImageSourceDependencyProperty.SetImage(
            dependencyObject,
            expected);

        var actual = ImageSourceDependencyProperty.GetImage(dependencyObject);

        Assert.Equal(
            expected,
            actual);
    }

    [Fact]
    public void GetImage_ShouldReturnNull_WhenNotSet()
    {
        var dependencyObject = new DependencyObject();

        var actual = ImageSourceDependencyProperty.GetImage(dependencyObject);

        Assert.Null(actual);
    }

    [Fact]
    public void SetImage()
    {
        var dependencyObject = new DependencyObject();

        ImageSourceDependencyProperty.SetImage(
            dependencyObject,
            new BitmapImage());
    }
}
