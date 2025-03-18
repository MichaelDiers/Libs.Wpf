namespace Libs.Wpf.Controls.CustomMessageBox;

/// <summary>
///     The available button of a message box.
/// </summary>
[Flags]
public enum MessageBoxButtons
{
    /// <summary>
    ///     The ok button.
    /// </summary>
    Ok = 1,

    /// <summary>
    ///     The cancel button.
    /// </summary>
    Cancel = 2,

    /// <summary>
    ///     The yes button.
    /// </summary>
    Yes = 4,

    /// <summary>
    ///     The no button.
    /// </summary>
    No = 8
}
