﻿namespace Libs.Wpf.Commands;

using System.Windows.Input;

/// <summary>
///     A synchronous implementation of <see cref="ICommand" /> that blocks the UI thread.
/// </summary>
/// <param name="canExecute">A function that determines whether the command can execute in its current state.</param>
/// <param name="execute">Defines the method to be called when the command is invoked.</param>
/// <seealso cref="ICommand" />
internal class SyncBaseCommand(Func<object?, bool>? canExecute, Action<object?> execute) : ICommand
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="SyncBaseCommand" /> class.
    /// </summary>
    /// <param name="execute">Defines the method to be called when the command is invoked.</param>
    public SyncBaseCommand(Action<object?> execute)
        : this(
            null,
            execute)
    {
    }

    /// <summary>Determines whether the command can execute in its current state.</summary>
    /// <param name="parameter">
    ///     Data used by the command. If the command does not require data to be passed, this object can be
    ///     set to <see langword="null" />.
    /// </param>
    /// <returns>
    ///     <see langword="true" /> if this command can be executed; otherwise, <see langword="false" />.
    /// </returns>
    public bool CanExecute(object? parameter)
    {
        return canExecute is null || canExecute(parameter);
    }

    /// <summary>
    ///     Occurs when changes take place that affect whether the command should execute.
    /// </summary>
    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    /// <summary>Defines the method to be called when the command is invoked.</summary>
    /// <param name="parameter">
    ///     Data used by the command. If the command does not require data to be passed, this object can be
    ///     set to <see langword="null" />.
    /// </param>
    public void Execute(object? parameter)
    {
        execute(parameter);
    }
}
