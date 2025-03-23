namespace Libs.Wpf.Controls.CustomMessageBox;

using Libs.Wpf.ViewModels;

/// <summary>
///     The view model of a message box.
/// </summary>
internal class MessageBoxViewModel : ViewModelBase
{
    /// <summary>
    ///     The buttons.
    /// </summary>
    private IEnumerable<ButtonViewModel> buttons = [];

    /// <summary>
    ///     The caption.
    /// </summary>
    private string caption = string.Empty;

    /// <summary>
    ///     The image source.
    /// </summary>
    private string? imageSource;

    /// <summary>
    ///     The message.
    /// </summary>
    private string message = string.Empty;

    /// <summary>
    ///     The result.
    /// </summary>
    private MessageBoxButtons result;

    /// <summary>
    ///     Gets or sets the buttons.
    /// </summary>
    public IEnumerable<ButtonViewModel> Buttons
    {
        get => this.buttons;
        set =>
            this.SetField(
                ref this.buttons,
                value);
    }

    /// <summary>
    ///     Gets or sets the caption.
    /// </summary>
    public string Caption
    {
        get => this.caption;
        set =>
            this.SetField(
                ref this.caption,
                value);
    }

    /// <summary>
    ///     Gets or sets the image source.
    /// </summary>
    public string? ImageSource
    {
        get => this.imageSource;
        set =>
            this.SetField(
                ref this.imageSource,
                value);
    }

    /// <summary>
    ///     Gets or sets the message.
    /// </summary>
    public string Message
    {
        get => this.message;
        set =>
            this.SetField(
                ref this.message,
                value);
    }

    /// <summary>
    ///     Gets or sets the result.
    /// </summary>
    public MessageBoxButtons Result
    {
        get => this.result;
        set =>
            this.SetField(
                ref this.result,
                value);
    }
}
