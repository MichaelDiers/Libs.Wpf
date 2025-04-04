namespace Libs.Wpf.Controls.CustomMessageBox;

using System.Globalization;
using System.Resources;

/// <summary>
///     The required data of the <see cref="CustomMessageBox" />.
/// </summary>
/// <param name="message">The displayed message.</param>
/// <param name="caption">The caption of the <see cref="CustomMessageBox" />.</param>
/// <param name="messageBoxButtons">The buttons displayed on the <see cref="CustomMessageBox" />.</param>
/// <param name="defaultMessageBoxButtons">The default button of the <see cref="CustomMessageBox" />.</param>
/// <param name="messageBoxImage">The image that is displayed.</param>
/// <param name="resourceManager">
///     An optional <see cref="ResourceManager" /> to retrieve the text of the
///     <see cref="CustomMessageBox" /> buttons.
/// </param>
/// <param name="cultureInfo">
///     An optional <see cref="CultureInfo" /> that is used to get the translation of the
///     <see cref="CustomMessageBox" /> button texts.
/// </param>
public class MessageBoxData(
    string message,
    string caption,
    MessageBoxButtons messageBoxButtons,
    MessageBoxButtons defaultMessageBoxButtons,
    MessageBoxImage messageBoxImage,
    ResourceManager? resourceManager = null,
    CultureInfo? cultureInfo = null
)
{
    /// <summary>
    ///     Gets the caption of the <see cref="CustomMessageBox" />.
    /// </summary>
    public string Caption { get; } = caption;

    /// <summary>
    ///     Gets an optional <see cref="CultureInfo" /> that is used to get the translation of the
    ///     <see cref="CustomMessageBox" /> button texts.
    /// </summary>
    public CultureInfo? CultureInfo { get; } = cultureInfo;

    /// <summary>
    ///     Gets the default button of the <see cref="CustomMessageBox" />.
    /// </summary>
    public MessageBoxButtons DefaultMessageBoxButtons { get; } = defaultMessageBoxButtons;

    /// <summary>
    ///     Gets the displayed message.
    /// </summary>
    public string Message { get; } = message;

    /// <summary>
    ///     Gets the buttons displayed on the <see cref="CustomMessageBox" />.
    /// </summary>
    public MessageBoxButtons MessageBoxButtons { get; } = messageBoxButtons;

    /// <summary>
    ///     Gets the image that is displayed.
    /// </summary>
    public MessageBoxImage MessageBoxImage { get; } = messageBoxImage;

    /// <summary>
    ///     Gets an optional <see cref="ResourceManager" /> to retrieve the text of the <see cref="CustomMessageBox" />
    ///     buttons.
    /// </summary>
    public ResourceManager? ResourceManager { get; } = resourceManager;
}
