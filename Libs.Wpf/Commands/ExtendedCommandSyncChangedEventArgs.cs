namespace Libs.Wpf.Commands;

using Libs.Wpf.Localization;

/// <summary>
///     The <see cref="EventArgs" /> of the <see cref="IExtendedCommandSync.ExtendedCommandSyncChanged" />.
/// </summary>
/// <param name="isCommandActive">Indicates if a command is active.</param>
/// <param name="translatableCancellableButton">The optional data to cancel an active command.</param>
public class ExtendedCommandSyncChangedEventArgs(
    bool isCommandActive,
    TranslatableCancellableButton? translatableCancellableButton
) : EventArgs
{
    /// <summary>
    ///     Gets a value that indicates if a command is active.
    /// </summary>
    public bool IsCommandActive => isCommandActive;

    /// <summary>
    ///     Gets the data to cancel an active command.
    /// </summary>
    public TranslatableCancellableButton? TranslatableCancellableButton => translatableCancellableButton;
}
