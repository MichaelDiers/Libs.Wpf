namespace Libs.Wpf.Controls.CustomMessageBox;

using System.Globalization;
using System.Resources;

/// <summary>
///     A service that shows a message box using the given parameters.
/// </summary>
public interface IMessageBoxService
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
    MessageBoxButtons Show(
        string message,
        string caption,
        MessageBoxButtons messageBoxButtons,
        MessageBoxButtons defaultMessageBoxButtons,
        MessageBoxImage messageBoxImage,
        ResourceManager? resourceManager = null,
        CultureInfo? cultureInfo = null
    );
}
