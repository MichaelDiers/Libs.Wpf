namespace Libs.Wpf.Tests.DependencyProperties;

using System.Windows;
using Libs.Wpf.DependencyProperties;

/// <summary>
///     Tests of <see cref="TextDependencyProperty" />
/// </summary>
public class TextDependencyPropertyTests
{
    [Fact]
    public void GetText()
    {
        var dependencyObject = new DependencyObject();
        var expected = "text";

        TextDependencyProperty.SetText(
            dependencyObject,
            expected);

        var actual = TextDependencyProperty.GetText(dependencyObject);

        Assert.Equal(
            expected,
            actual);
    }

    [Fact]
    public void GetText_ShouldReturnNull_WhenNotSet()
    {
        var dependencyObject = new DependencyObject();

        var actual = TextDependencyProperty.GetText(dependencyObject);

        Assert.Null(actual);
    }

    [Fact]
    public void SetText()
    {
        var dependencyObject = new DependencyObject();

        TextDependencyProperty.SetText(
            dependencyObject,
            "text");
    }
}
