namespace Libs.Wpf.Commands;

using System.Windows.Input;

/// <summary>
///     Extends <see cref="IAsyncCommand" /> and provides an <see cref="IAsyncCommand" /> that can cancel the execution of
///     <see cref="ICommand.Execute" />.
/// </summary>
/// <seealso cref="IAsyncCommand" />
public interface ICancellableCommand : IAsyncCommand
{
    /// <summary>
    ///     Gets the command that stops the execution of <see cref="ICommand.Execute" />.
    /// </summary>
    IAsyncCommand? CancelCommand { get; }
}
