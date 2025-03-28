﻿namespace Libs.Wpf.Controls.CustomMessageBox;

using System.Globalization;
using System.Resources;
using System.Windows;
using Libs.Wpf.Commands;

/// <summary>
///     A service that shows a message box using the given parameters.
/// </summary>
public class MessageBoxService(ICommandFactory commandFactory, Window? window = null) : IMessageBoxService
{
    /// <summary>
    ///     Shows a custom message box.
    /// </summary>
    /// <param name="message">The displayed message.</param>
    /// <param name="caption">The caption of the message box.</param>
    /// <param name="messageBoxButtons">The buttons to display.</param>
    /// <param name="defaultMessageBoxButtons">The default result of the message box.</param>
    /// <param name="messageBoxImage">The image that is displayed.</param>
    /// <param name="resourceManager">A resource manager that provides the button names.</param>
    /// <param name="cultureInfo">The current culture info.</param>
    /// <returns>The user selected message box result.</returns>
    public MessageBoxButtons Show(
        string message,
        string caption,
        MessageBoxButtons messageBoxButtons,
        MessageBoxButtons defaultMessageBoxButtons,
        MessageBoxImage messageBoxImage,
        ResourceManager? resourceManager = null,
        CultureInfo? cultureInfo = null
    )
    {
        var messageBoxWindow = new MessageBoxWindow {Owner = window};
        var viewModel = new MessageBoxViewModel
        {
            Result = defaultMessageBoxButtons,
            Caption = caption,
            Message = message,
            ImageSource = MessageBoxService.ToImageSource(messageBoxImage)
        };
        var buttons = Enum.GetValues<MessageBoxButtons>()
            .Where(value => (value & messageBoxButtons) == value)
            .Select(
                value => new ButtonViewModel(
                    resourceManager?.GetString(
                        value.ToString(),
                        cultureInfo) ??
                    value.ToString(),
                    commandFactory.CreateSyncCommand<object>(
                        _ =>
                        {
                            viewModel.Result = value;
                            messageBoxWindow.Close();
                        })))
            .ToArray();
        viewModel.Buttons = buttons;
        messageBoxWindow.DataContext = viewModel;
        messageBoxWindow.ShowDialog();

        return viewModel.Result;
    }

    /// <summary>
    ///     Converts from <see cref="MessageBoxImage" /> to an image.
    /// </summary>
    /// <param name="messageBoxImage">The specification of the image.</param>
    /// <returns>The source of the image to display.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Raised if no image is available.</exception>
    private static string? ToImageSource(MessageBoxImage messageBoxImage)
    {
        return messageBoxImage switch
        {
            MessageBoxImage.None => null,
            MessageBoxImage.Asterisk =>
                "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_information.png",
            MessageBoxImage.Error => "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_error.png",
            MessageBoxImage.Exclamation =>
                "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_warning.png",
            MessageBoxImage.Question => "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_question.png",
            _ => throw new ArgumentOutOfRangeException(
                nameof(messageBoxImage),
                messageBoxImage,
                null)
        };
    }
}
