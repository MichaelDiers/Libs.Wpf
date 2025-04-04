﻿namespace Libs.Wpf.Commands;

using System.IO;
using System.Windows.Input;
using Libs.Wpf.Localization;
using Microsoft.Win32;

/// <summary>
///     A factory for <see cref="ICommand" /> and <see cref="ICancellableCommand" /> implementations.
/// </summary>
public interface ICommandFactory
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ICancellableCommand" /> implementation.
    /// </summary>
    /// <param name="commandSync">Synchronizes the command execution to ensure only one command at the same is executed.</param>
    /// <param name="canExecute">Determines whether the command can execute in its current state.</param>
    /// <param name="executeAsync">Defines the method to be called when the command is invoked.</param>
    /// <param name="handleErrorAsync">
    ///     Handles command execution errors. If an <see cref="Exception" /> is thrown at
    ///     <see cref="ICommand.Execute" /> this error handler is called.
    /// </param>
    /// <param name="force">Allow to run the command in parallel to other commands.</param>
    /// <param name="translatableCancelButton">
    ///     The data of the cancel button.
    /// </param>
    /// <param name="postCommandFunc">The function is called after command termination.</param>
    public ICancellableCommand CreateAsyncCommand(
        ICommandSync commandSync,
        Func<bool> canExecute,
        Func<CancellationToken, Task> executeAsync,
        Func<Exception, CancellationToken, Task> handleErrorAsync,
        bool force = false,
        TranslatableCancelButton? translatableCancelButton = null,
        Func<Task>? postCommandFunc = null
    );

    /// <summary>
    ///     Initializes a new instance of the <see cref="ICancellableCommand" /> implementation.
    /// </summary>
    /// <typeparam name="TCommandParameter">The type of the command parameter.</typeparam>
    /// <typeparam name="TExecuteResult">The result type of the execution function.</typeparam>
    /// <param name="commandSync">Synchronizes the command execution to ensure only one command at the same is executed.</param>
    /// <param name="canExecute">Determines whether the command can execute in its current state.</param>
    /// <param name="executeAsync">Defines the method to be called when the command is invoked.</param>
    /// <param name="handleErrorAsync">
    ///     Handles command execution errors. If an <see cref="Exception" /> is thrown at
    ///     <see cref="ICommand.Execute" /> this error handler is called.
    /// </param>
    /// <param name="force">Allow to run the command in parallel to other commands.</param>
    /// <param name="translatableCancelButton">
    ///     The data of the cancel button.
    /// </param>
    /// <param name="postCommandFunc">The function is called after command termination.</param>
    ICancellableCommand CreateAsyncCommand<TCommandParameter, TExecuteResult>(
        ICommandSync commandSync,
        Func<TCommandParameter?, bool> canExecute,
        Func<TCommandParameter?, CancellationToken, Task<TExecuteResult?>> executeAsync,
        Func<Exception, CancellationToken, Task> handleErrorAsync,
        bool force = false,
        TranslatableCancelButton? translatableCancelButton = null,
        Func<TExecuteResult?, Task>? postCommandFunc = null
    );

    /// <summary>
    ///     Initializes a new instance of the <see cref="ICancellableCommand" /> implementation.
    /// </summary>
    /// <param name="commandSync">Synchronizes the command execution to ensure only one command at the same is executed.</param>
    /// <param name="canExecute">Determines whether the command can execute in its current state.</param>
    /// <param name="executeAsync">Defines the method to be called when the command is invoked.</param>
    /// <param name="handleErrorAsync">
    ///     Handles command execution errors. If an <see cref="Exception" /> is thrown at
    ///     <see cref="ICommand.Execute" /> this error handler is called.
    /// </param>
    /// <param name="force">Allow to run the command in parallel to other commands.</param>
    /// <param name="translatableCancelButton">
    ///     The data of the cancel button.
    /// </param>
    /// <param name="postCommandFunc">The function is called after command termination.</param>
    ICancellableCommand CreateAsyncCommand<TExecuteResult>(
        ICommandSync commandSync,
        Func<bool> canExecute,
        Func<CancellationToken, Task<TExecuteResult?>> executeAsync,
        Func<Exception, CancellationToken, Task> handleErrorAsync,
        bool force = false,
        TranslatableCancelButton? translatableCancelButton = null,
        Func<TExecuteResult?, Task>? postCommandFunc = null
    );

    /// <summary>
    ///     An <see cref="ICommand" /> that opens a file dialog. If a file is selected the <paramref name="execute" /> is
    ///     called using the selected file and the command parameter.
    /// </summary>
    /// <param name="basePath">Specifies the base path of the open file dialog.</param>
    /// <param name="execute">An <see cref="Action{T,TT}" /> called with the selected file and the command parameter.</param>
    /// <param name="filter">An optional <see cref="OpenFileDialog.Filter" /> of the <see cref="OpenFileDialog" />.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    ICommand CreateOpenFileDialogCommand(
        DirectoryInfo basePath,
        Action<string?, string> execute,
        string? filter = null
    );

    /// <summary>
    ///     An <see cref="ICommand" /> that opens a file dialog. If a file is selected the <paramref name="execute" /> is
    ///     called using the selected file and the command parameter.
    /// </summary>
    /// <param name="execute">An <see cref="Action{T,TT}" /> called with the selected file and the command parameter.</param>
    /// <param name="filter">An optional <see cref="OpenFileDialog.Filter" /> of the <see cref="OpenFileDialog" />.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    ICommand CreateOpenFileDialogCommand(Action<string?, string> execute, string? filter);

    /// <summary>
    ///     An <see cref="ICommand" /> that opens a <see cref="OpenFolderDialog" />. If a folder is selected
    ///     <paramref name="execute" /> is called using the selected folder.
    /// </summary>
    /// <param name="basePath">The default directory of the open folder dialog.</param>
    /// <param name="execute">If a folder is selected this <see cref="Action{T}" /> is called.</param>
    ICommand CreateOpenFolderDialogCommand(DirectoryInfo basePath, Action<string> execute);

    /// <summary>
    ///     Initializes a new instance of an <see cref="ICommand" /> implementing class. The command does block the ui thread
    ///     during execution and cannot be cancelled.
    /// </summary>
    /// <param name="canExecute">A function that determines whether the command can execute in its current state.</param>
    /// <param name="execute">Defines the method to be called when the command is invoked.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    ICommand CreateSyncCommand<TCommandParameter>(
        Func<TCommandParameter?, bool>? canExecute,
        Action<TCommandParameter?> execute
    );

    /// <summary>
    ///     Initializes a new instance of an <see cref="ICommand" /> implementing class. The command does block the ui thread
    ///     during execution and cannot be cancelled.
    /// </summary>
    /// <param name="canExecute">A function that determines whether the command can execute in its current state.</param>
    /// <param name="execute">Defines the method to be called when the command is invoked.</param>
    /// <returns>A new <see cref="ICommand" />.</returns>
    ICommand CreateSyncCommand(Func<object?, bool>? canExecute, Action<object?> execute);

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
    ICommand CreateSyncCommand<TCommandParameter>(Action<TCommandParameter?> execute);

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
    ICommand CreateSyncCommand(Action<object?> execute);
}
