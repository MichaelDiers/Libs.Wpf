namespace Libs.Wpf.Localization;

using System.Resources;
using System.Windows.Input;

/// <summary>
///     The data of a translatable button.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
/// <param name="command">The command of the button.</param>
/// <param name="imageSource">The image that is displayed on the button.</param>
/// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
/// <param name="labelResourceKey">The resource key of the label.</param>
/// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
public class TranslatableButton<TCommand>(
    TCommand command,
    string? imageSource,
    ResourceManager resourceManager,
    string? labelResourceKey = null,
    string? toolTipResourceKey = null
) : TranslatableButtonBase(
    imageSource,
    resourceManager,
    labelResourceKey,
    toolTipResourceKey) where TCommand : ICommand
{
    /// <summary>
    ///     Gets the command.
    /// </summary>
    public TCommand Command => command;
}
