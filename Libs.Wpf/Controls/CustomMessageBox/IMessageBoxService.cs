namespace Libs.Wpf.Controls.CustomMessageBox;

/// <summary>
///     A service that shows a message box using the given parameters.
/// </summary>
public interface IMessageBoxService
{
    /// <summary>
    ///     Shows a custom message box.
    /// </summary>
    /// <param name="messageBoxData">The required data to create the <see cref="CustomMessageBox" />.</param>
    /// <returns>The user selected message box result.</returns>
    MessageBoxButtons Show(MessageBoxData messageBoxData);
}
