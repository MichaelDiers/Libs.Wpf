namespace Libs.Wpf.Controls.CustomMessageBox;

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
    /// <param name="messageBoxData">The required data to create the <see cref="CustomMessageBox" />.</param>
    /// <returns>The user selected message box result.</returns>
    public MessageBoxButtons Show(MessageBoxData messageBoxData)
    {
        var messageBoxWindow = new MessageBoxWindow {Owner = window};
        var viewModel = new MessageBoxViewModel
        {
            Result = messageBoxData.DefaultMessageBoxButtons,
            Caption = messageBoxData.Caption,
            Message = messageBoxData.Message,
            ImageSource = MessageBoxService.ToImageSource(messageBoxData.MessageBoxImage)
        };
        var buttons = Enum.GetValues<MessageBoxButtons>()
            .Where(value => (value & messageBoxData.MessageBoxButtons) == value)
            .Select(
                value => new ButtonViewModel(
                    messageBoxData.ResourceManager?.GetString(
                        value.ToString(),
                        messageBoxData.CultureInfo) ??
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
