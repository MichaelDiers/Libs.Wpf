namespace Libs.Wpf.Commands;

using System.IO;
using System.Windows.Input;
using Libs.Wpf.Threads;
using Microsoft.Win32;

/// <summary>
///     A factory for <see cref="ICommand" /> and <see cref="ICancellableCommand" /> implementations.
/// </summary>
internal class CommandFactory(IDispatcherWrapper dispatcherWrapper) : ICommandFactory
{
    /// <summary>
    ///     Initializes a new instance of an <see cref="ICancellableCommand" /> implementing class. The command does not block
    ///     the ui thread during execution and can be cancelled.
    /// </summary>
    /// <param name="canExecute">
    ///     A function to check whether a command can be executed. if <paramref name="canExecute" /> is
    ///     <c>null</c> the execution of the command is not restricted.
    /// </param>
    /// <param name="preExecute">
    ///     The optional action is executed before <paramref name="execute" />. The action executes in the
    ///     ui thread.
    /// </param>
    /// <param name="execute">
    ///     The function is executed in a new background <see cref="Task" /> and does not block the UI
    ///     thread.
    /// </param>
    /// <param name="postExecute">
    ///     The action is called with the result of <paramref name="execute" />. If required the action
    ///     executes using a UI thread dispatcher.
    /// </param>
    /// <returns>A new <see cref="ICancellableCommand" />.</returns>
    public ICancellableCommand CreateAsyncCommand<TCommandParameter, TExecuteResult>(
        Func<TCommandParameter?, bool>? canExecute,
        Action<TCommandParameter?>? preExecute,
        Func<TCommandParameter?, CancellationToken, Task<TExecuteResult?>>? execute,
        Action<Task<TExecuteResult?>>? postExecute
    )
    {
        return new AsyncCommand<TCommandParameter, TExecuteResult>(
            canExecute,
            preExecute,
            execute,
            postExecute,
            dispatcherWrapper.Dispatcher);
    }

    /// <summary>
    ///     An <see cref="ICommand" /> that opens a file dialog. If a file is selected the <paramref name="execute" /> is
    ///     called using the selected file and the command parameter.
    /// </summary>
    /// <param name="basePath">Specifies the base path of the open file dialog.</param>
    /// <param name="execute">An <see cref="Action{T,TT}" /> called with the selected file and the command parameter.</param>
    /// <param name="filter">An optional <see cref="OpenFileDialog.Filter" /> of the <see cref="OpenFileDialog" />.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    public ICommand CreateOpenFileDialogCommand(
        DirectoryInfo basePath,
        Action<string?, string> execute,
        string? filter = null
    )
    {
        return new OpenFileDialogCommand(
            basePath,
            execute,
            filter);
    }

    /// <summary>
    ///     An <see cref="ICommand" /> that opens a file dialog. If a file is selected the <paramref name="execute" /> is
    ///     called using the selected file and the command parameter.
    /// </summary>
    /// <param name="execute">An <see cref="Action{T,TT}" /> called with the selected file and the command parameter.</param>
    /// <param name="filter">An optional <see cref="OpenFileDialog.Filter" /> of the <see cref="OpenFileDialog" />.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    public ICommand CreateOpenFileDialogCommand(Action<string?, string> execute, string? filter = null)
    {
        return this.CreateOpenFileDialogCommand(
            new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)),
            execute,
            filter);
    }

    /// <summary>
    ///     An <see cref="ICommand" /> that opens a <see cref="OpenFolderDialog" />. If a folder is selected
    ///     <paramref name="execute" /> is called using the selected folder.
    /// </summary>
    /// <param name="basePath">The default directory of the open folder dialog.</param>
    /// <param name="execute">If a folder is selected this <see cref="Action{T}" /> is called.</param>
    public ICommand CreateOpenFolderDialogCommand(DirectoryInfo basePath, Action<string> execute)
    {
        return new OpenFolderDialogCommand(
            basePath,
            execute);
    }

    /// <summary>
    ///     Initializes a new instance of an <see cref="ICommand" /> implementing class. The command does block the ui thread
    ///     during execution and cannot be cancelled.
    /// </summary>
    /// <param name="canExecute">A function that determines whether the command can execute in its current state.</param>
    /// <param name="execute">Defines the method to be called when the command is invoked.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    public ICommand CreateSyncCommand<TCommandParameter>(
        Func<TCommandParameter?, bool>? canExecute,
        Action<TCommandParameter?> execute
    )
    {
        return new SyncCommand<TCommandParameter>(
            canExecute,
            execute);
    }

    /// <summary>
    ///     Initializes a new instance of an <see cref="ICommand" /> implementing class. The command does block the ui thread
    ///     during execution and cannot be cancelled.
    /// </summary>
    /// <param name="canExecute">A function that determines whether the command can execute in its current state.</param>
    /// <param name="execute">Defines the method to be called when the command is invoked.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    public ICommand CreateSyncCommand(Func<object?, bool>? canExecute, Action<object?> execute)
    {
        return new SyncBaseCommand(
            canExecute,
            execute);
    }

    /// <summary>
    ///     Initializes a new instance of an <see cref="ICommand" /> implementing class. The command does block the ui thread
    ///     during execution and cannot be cancelled.
    /// </summary>
    /// <param name="execute">Defines the method to be called when the command is invoked.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    /// <remarks>
    ///     The command is executed with restrictions and therefore <see cref="ICommand.CanExecute" /> provides in any
    ///     case <c>true</c>.
    /// </remarks>
    public ICommand CreateSyncCommand<TCommandParameter>(Action<TCommandParameter?> execute)
    {
        return new SyncCommand<TCommandParameter>(execute);
    }

    /// <summary>
    ///     Initializes a new instance of an <see cref="ICommand" /> implementing class. The command does block the ui thread
    ///     during execution and cannot be cancelled.
    /// </summary>
    /// <param name="execute">Defines the method to be called when the command is invoked.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    /// <remarks>
    ///     The command is executed with restrictions and therefore <see cref="ICommand.CanExecute" /> provides in any
    ///     case <c>true</c>.
    /// </remarks>
    public ICommand CreateSyncCommand(Action<object?> execute)
    {
        return new SyncBaseCommand(execute);
    }
}
