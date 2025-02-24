namespace Libs.Wpf.Commands;

using System.Windows.Input;

/// <summary>
///     A synchronous implementation of <see cref="ICommand" /> that blocks the UI thread.
/// </summary>
/// <param name="canExecute">A function that determines whether the command can execute in its current state.</param>
/// <param name="execute">Defines the method to be called when the command is invoked.</param>
/// <seealso cref="ICommand" />
internal class SyncCommand<T>(Func<T?, bool>? canExecute, Action<T?> execute) : SyncBaseCommand(
    parameter => canExecute is null || canExecute((T?) parameter),
    parameter => execute((T?) parameter))
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SyncCommand{T}" /> class.
    /// </summary>
    /// <param name="execute">Defines the method to be called when the command is invoked.</param>
    public SyncCommand(Action<T?> execute)
        : this(
            null,
            execute)
    {
    }
}
