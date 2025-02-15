namespace Libs.Wpf.DependencyProperties;

using System.Windows;

/// <summary>
///     An attached <see cref="DependencyProperty" /> that adds the corner radius property.
/// </summary>
public static class CornerRadiusDependencyProperty
{
    /// <summary>
    ///     The default value of the corner radius.
    /// </summary>
    private const double DefaultCornerRadius = 5;

    /// <summary>
    ///     The corner radius <see cref="DependencyProperty" />.
    /// </summary>
    public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.RegisterAttached(
        nameof(CornerRadiusDependencyProperty.CornerRadiusProperty)[..^8],
        typeof(CornerRadius),
        typeof(CornerRadiusDependencyProperty),
        new PropertyMetadata(new CornerRadius(CornerRadiusDependencyProperty.DefaultCornerRadius)));

    /// <summary>
    ///     Gets the value of the corner radius <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <returns>The value of the <see cref="DependencyProperty" />.</returns>
    public static CornerRadius GetCornerRadius(DependencyObject element)
    {
        return (CornerRadius) element.GetValue(CornerRadiusDependencyProperty.CornerRadiusProperty);
    }

    /// <summary>
    ///     Sets the value of the corner radius <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <param name="value">The new value of the <see cref="DependencyProperty" />.</param>
    public static void SetCornerRadius(DependencyObject element, CornerRadius value)
    {
        element.SetValue(
            CornerRadiusDependencyProperty.CornerRadiusProperty,
            value);
    }
}
