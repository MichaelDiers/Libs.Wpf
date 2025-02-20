namespace Libs.Wpf.DependencyProperties;

using System.Windows;
using System.Windows.Media;

/// <summary>
///     An attached <see cref="DependencyProperty" /> that adds an image property.
/// </summary>
public static class ImageSourceDependencyProperty
{
    /// <summary>
    ///     The image <see cref="DependencyProperty" />.
    /// </summary>
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.RegisterAttached(
        nameof(ImageSourceDependencyProperty.ImageSourceProperty)[..^8],
        typeof(ImageSource),
        typeof(ImageSourceDependencyProperty),
        new PropertyMetadata(default(ImageSource)));

    /// <summary>
    ///     Gets the value of the image property.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <returns>The value of the <see cref="DependencyProperty" />.</returns>
    public static ImageSource GetImageSource(DependencyObject element)
    {
        return (ImageSource) element.GetValue(ImageSourceDependencyProperty.ImageSourceProperty);
    }

    /// <summary>
    ///     Sets the value of the image property.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <param name="value">The new value of the <see cref="DependencyProperty" />.</param>
    public static void SetImageSource(DependencyObject element, ImageSource value)
    {
        element.SetValue(
            ImageSourceDependencyProperty.ImageSourceProperty,
            value);
    }
}
