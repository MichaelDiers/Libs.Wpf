namespace Libs.Wpf.DependencyProperties;

using System.Windows;

/// <summary>
///     An attached <see cref="DependencyProperty" /> that adds the text property.
/// </summary>
public static class TextDependencyProperty
{
    /// <summary>
    ///     The text <see cref="DependencyProperty" />.
    /// </summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.RegisterAttached(
        nameof(TextDependencyProperty.TextProperty)[..^8],
        typeof(string),
        typeof(TextDependencyProperty),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Gets the value of the text <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <returns>The value of the <see cref="DependencyProperty" />.</returns>
    public static string GetText(DependencyObject element)
    {
        return (string) element.GetValue(TextDependencyProperty.TextProperty);
    }

    /// <summary>
    ///     Sets the value of the text <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <param name="value">The new value of the <see cref="DependencyProperty" />.</param>
    public static void SetText(DependencyObject element, string value)
    {
        element.SetValue(
            TextDependencyProperty.TextProperty,
            value);
    }
}
