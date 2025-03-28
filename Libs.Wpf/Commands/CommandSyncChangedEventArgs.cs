namespace Libs.Wpf.Commands;

/// <summary>
///     The <see cref="EventArgs" /> of the <see cref="ICommandSync.CommandSyncChanged" />.
/// </summary>
/// <param name="isCommandActive">Indicates if a command is active.</param>
public class CommandSyncChangedEventArgs(bool isCommandActive) : EventArgs
{
    /// <summary>
    ///     Gets a value that indicates if a command is active.
    /// </summary>
    public bool IsCommandActive => isCommandActive;
}
