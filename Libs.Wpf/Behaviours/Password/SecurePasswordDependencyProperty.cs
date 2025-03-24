namespace Libs.Wpf.Behaviours.Password;

using System.Security;
using System.Windows;

/// <summary>
///     An attached <see cref="DependencyProperty" /> that adds the secure string property.
/// </summary>
public static class SecurePasswordDependencyProperty
{
    /// <summary>
    ///     The secure string <see cref="DependencyProperty" />.
    /// </summary>
    public static readonly DependencyProperty SecureStringProperty = DependencyProperty.RegisterAttached(
        nameof(SecurePasswordDependencyProperty.SecureStringProperty)[..^8],
        typeof(SecureString),
        typeof(SecurePasswordDependencyProperty),
        new PropertyMetadata(default(SecureString)));

    /// <summary>
    ///     Gets the value of the secure string <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <returns>The value of the <see cref="DependencyProperty" />.</returns>
    public static SecureString GetSecureString(DependencyObject element)
    {
        return (SecureString) element.GetValue(SecurePasswordDependencyProperty.SecureStringProperty);
    }

    /// <summary>
    ///     Sets the value of the secure string <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <param name="value">The new value of the <see cref="DependencyProperty" />.</param>
    public static void SetSecureString(DependencyObject element, SecureString value)
    {
        element.SetValue(
            SecurePasswordDependencyProperty.SecureStringProperty,
            value);
    }
}
