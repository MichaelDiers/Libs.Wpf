// Modified version of https://github.com/Jinjinov/wpf-localization-multiple-resource-resx-one-language/tree/master

namespace Libs.Wpf.Localization;

using System.Resources;
using System.Windows;

/// <summary>
///     A <see cref="DependencyObject" /> that attaches the <see cref="ResourceManagerProperty" />.
/// </summary>
public class Translation : DependencyObject
{
    /// <summary>
    ///     The attached <see cref="DependencyProperty" /> that adds <see cref="ResourceManager" /> information.
    /// </summary>
    public static readonly DependencyProperty ResourceManagerProperty = DependencyProperty.RegisterAttached(
        nameof(Translation.ResourceManagerProperty)[..^8],
        typeof(ResourceManager),
        typeof(Translation));

    /// <summary>
    ///     Gets the <see cref="ResourceManager" /> information.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The <see cref="ResourceManagerProperty" /> is attached to this
    ///     <see cref="DependencyObject" />.
    /// </param>
    /// <returns>The set <see cref="ResourceManager" />.</returns>
    public static ResourceManager GetResourceManager(DependencyObject dependencyObject)
    {
        return (ResourceManager) dependencyObject.GetValue(Translation.ResourceManagerProperty);
    }

    /// <summary>
    ///     Sets the <see cref="ResourceManager" /> information.
    /// </summary>
    /// <param name="dependencyObject">
    ///     The <see cref="ResourceManagerProperty" /> is attached to this
    ///     <see cref="DependencyObject" />.
    /// </param>
    /// <param name="value">The new <see cref="ResourceManager" />.</param>
    public static void SetResourceManager(DependencyObject dependencyObject, ResourceManager value)
    {
        dependencyObject.SetValue(
            Translation.ResourceManagerProperty,
            value);
    }
}
