namespace Libs.Wpf.Commands.CancelWindow;

/// <summary>
///     Describes a cancel window.
/// </summary>
public interface ICancelWindow
{
    /// <summary>
    ///     Gets or sets the data context.
    /// </summary>
    object DataContext { get; set; }

    /// <summary>
    ///     Closes the window.
    /// </summary>
    void Close();

    /// <summary>
    ///     Opens the window.
    /// </summary>
    void Show();
}
