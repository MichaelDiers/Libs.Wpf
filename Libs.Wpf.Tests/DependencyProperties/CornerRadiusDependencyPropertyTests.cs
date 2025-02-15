namespace Libs.Wpf.Tests.DependencyProperties;

using System.Windows;
using Libs.Wpf.DependencyProperties;

/// <summary>
///     Tests of <see cref="CornerRadiusDependencyProperty" />
/// </summary>
public class CornerRadiusDependencyPropertyTests
{
    [Fact]
    public void GetCornerRadius()
    {
        var dependencyObject = new DependencyObject();
        var expected = new CornerRadius();

        CornerRadiusDependencyProperty.SetCornerRadius(
            dependencyObject,
            expected);

        var actual = CornerRadiusDependencyProperty.GetCornerRadius(dependencyObject);

        Assert.Equal(
            expected,
            actual);
    }

    [Fact]
    public void GetCornerRadius_ShouldReturnNull_WhenNotSet()
    {
        var dependencyObject = new DependencyObject();

        var actual = CornerRadiusDependencyProperty.GetCornerRadius(dependencyObject);

        Assert.Equal(
            5,
            actual.BottomLeft);
        Assert.Equal(
            5,
            actual.BottomRight);
        Assert.Equal(
            5,
            actual.TopLeft);
        Assert.Equal(
            5,
            actual.TopRight);
    }

    [Fact]
    public void SetCornerRadius()
    {
        var dependencyObject = new DependencyObject();

        CornerRadiusDependencyProperty.SetCornerRadius(
            dependencyObject,
            new CornerRadius());
    }
}
