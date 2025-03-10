﻿namespace Libs.Wpf.ViewModels;

using System.Resources;
using System.Windows.Input;
using System.Windows.Media;

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
    ImageSource? imageSource,
    ResourceManager resourceManager,
    string? labelResourceKey = null,
    string? toolTipResourceKey = null
) : Translatable(
    resourceManager,
    labelResourceKey,
    toolTipResourceKey) where TCommand : ICommand
{
    /// <summary>
    ///     The command.
    /// </summary>
    private TCommand command = command;

    /// <summary>
    ///     The image source.
    /// </summary>
    private ImageSource? imageSource = imageSource;

    /// <summary>
    ///     Gets or sets the command.
    /// </summary>
    public TCommand Command
    {
        get => this.command;
        set =>
            this.SetField(
                ref this.command,
                value);
    }

    /// <summary>
    ///     Gets or sets the image source.
    /// </summary>
    public ImageSource? ImageSource
    {
        get => this.imageSource;
        set =>
            this.SetField(
                ref this.imageSource,
                value);
    }
}
