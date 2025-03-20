namespace Libs.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;

/// <summary>
///     If <see cref="UIElement.IsEnabled" /> is set on an <see cref="UIElement" /> all child elements are set to the set
///     <see cref="UIElement.IsEnabled" /> value. The <see cref="ResetIsEnabled" /> prevents the inheritance of
///     <see cref="UIElement.IsEnabled" />.
/// </summary>
public class ResetIsEnabled : ContentControl
{
    /// <summary>
    ///     A static constructor that overrides the metadata of the element.
    /// </summary>
    static ResetIsEnabled()
    {
        UIElement.IsEnabledProperty.OverrideMetadata(
            typeof(ResetIsEnabled),
            new UIPropertyMetadata(
                true,
                (_, _) => { },
                (_, x) => x));
    }
}
