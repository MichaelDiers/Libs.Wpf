namespace Libs.Wpf.Controls.CustomMessageBox;

using System.Windows.Input;
using Libs.Wpf.ViewModels;

/// <summary>
///     View model of a message box button.
/// </summary>
/// <param name="content">The content of the message box button.</param>
/// <param name="command">The command of the message box button.</param>
internal class ButtonViewModel(string content, ICommand command) : ViewModelBase
{
    /// <summary>
    ///     The command.
    /// </summary>
    private ICommand command = command;

    /// <summary>
    ///     The content.
    /// </summary>
    private string content = content;

    /// <summary>
    ///     Gets or sets the command.
    /// </summary>
    public ICommand Command
    {
        get => this.command;
        set =>
            this.SetField(
                ref this.command,
                value);
    }

    /// <summary>
    ///     Gets or sets the content.
    /// </summary>
    public string Content
    {
        get => this.content;
        set =>
            this.SetField(
                ref this.content,
                value);
    }
}
