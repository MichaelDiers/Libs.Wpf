// Modified version of https://github.com/Jinjinov/wpf-localization-multiple-resource-resx-one-language/tree/master

namespace Libs.Wpf.Localization;

using System.Resources;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

/// <summary>
///     Localization extension for multi-language applications.
/// </summary>
/// <param name="resourceKey">A key from a resource file.</param>
public class LocExtension(string resourceKey) : MarkupExtension
{
    /// <summary>
    ///     Gets the resource key.
    /// </summary>
    public string ResourceKey { get; } = resourceKey;

    /// <summary>
    ///     Find a matching <see cref="Translation" /> <see cref="DependencyObject" /> and set the binding.
    /// </summary>
    /// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
    /// <returns>The object to set on this property.</returns>
    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        // targetObject is the control that is using the LocExtension
        var targetObject = (serviceProvider as IProvideValueTarget)?.TargetObject;

        if (targetObject?.GetType().Name == "SharedDp") // is extension used in a control template?
        {
            return targetObject; // required for template re-binding
        }

        var baseName = string.Empty;
        var current = targetObject as DependencyObject;

        while (string.IsNullOrWhiteSpace(baseName) && current is not null)
        {
            baseName = LocExtension.GetResourceManager(current)?.BaseName ?? string.Empty;

            if (string.IsNullOrEmpty(baseName) && current is FrameworkElement frameworkElement) // template re-binding
            {
                baseName = LocExtension.GetResourceManager(frameworkElement.TemplatedParent)?.BaseName ?? string.Empty;
            }

            current = VisualTreeHelper.GetParent(current) ?? (current as FrameworkElement)?.Parent;
        }

        var binding = new Binding
        {
            Mode = BindingMode.OneWay,
            Path = new PropertyPath($"[{baseName}.{this.ResourceKey}]"),
            Source = TranslationSource.Instance,
            FallbackValue = this.ResourceKey
        };

        return binding.ProvideValue(serviceProvider);
    }

    /// <summary>
    ///     Try to find a <see cref="Translation.ResourceManagerProperty" /> on the given <paramref name="control" />.
    /// </summary>
    /// <param name="control">
    ///     The <paramref name="control" /> is checked for the
    ///     <see cref="Translation.ResourceManagerProperty" />.
    /// </param>
    /// <returns>
    ///     The found matching <see cref="ResourceManager" /> or null if
    ///     <see cref="Translation.ResourceManagerProperty" /> is not set.
    /// </returns>
    private static ResourceManager? GetResourceManager(object? control)
    {
        if (control is not DependencyObject dependencyObject)
        {
            return null;
        }

        var localValue = dependencyObject.ReadLocalValue(Translation.ResourceManagerProperty);

        // does this control have a "Translation.ResourceManager" attached property with a set value?
        if (localValue == DependencyProperty.UnsetValue)
        {
            return null;
        }

        if (localValue is not ResourceManager resourceManager)
        {
            return null;
        }

        TranslationSource.Instance.AddResourceManager(resourceManager);

        return resourceManager;
    }
}
