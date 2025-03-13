namespace Libs.Wpf.ViewModels;

using System.Resources;
using System.Windows.Media;

/// <summary>
///     The data of the translatable button base.
/// </summary>
/// <param name="imageSource">The image that is displayed on the button.</param>
/// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
/// <param name="labelResourceKey">The resource key of the label.</param>
/// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
public class TranslatableButtonBase(
    ImageSource? imageSource,
    ResourceManager resourceManager,
    string? labelResourceKey = null,
    string? toolTipResourceKey = null
) : Translatable(
    resourceManager,
    labelResourceKey,
    toolTipResourceKey)
{
    /// <summary>
    ///     Gets the image source.
    /// </summary>
    public ImageSource? ImageSource => imageSource;
}
