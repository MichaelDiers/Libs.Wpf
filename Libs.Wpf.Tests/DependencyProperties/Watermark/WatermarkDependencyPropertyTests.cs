namespace Libs.Wpf.Tests.DependencyProperties.Watermark;

using System.Windows;
using Libs.Wpf.DependencyProperties.Watermark;

/// <summary>
///     Tests of <see cref="WatermarkDependencyProperty" />
/// </summary>
public class WatermarkDependencyPropertyTests
{
    [Fact]
    public void GetWatermark()
    {
        var dependencyObject = new DependencyObject();
        const string expected = "watermark";

        WatermarkDependencyProperty.SetWatermark(
            dependencyObject,
            expected);

        var actual = WatermarkDependencyProperty.GetWatermark(dependencyObject);

        Assert.Equal(
            expected,
            actual);
    }

    [Fact]
    public void GetWatermark_ShouldReturnNull_WhenNotSet()
    {
        var dependencyObject = new DependencyObject();

        var actual = WatermarkDependencyProperty.GetWatermark(dependencyObject);

        Assert.Null(actual);
    }

    [Fact]
    public void SetWatermark_UsingNoControl()
    {
        var dependencyObject = new DependencyObject();

        WatermarkDependencyProperty.SetWatermark(
            dependencyObject,
            "watermark");
    }
}
