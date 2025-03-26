namespace Libs.Wpf.Commands;

using System.Windows.Input;
using Libs.Wpf.Localization;
using Libs.Wpf.ViewModels;

/// <summary>
///     An implementation of <see cref="ICommand" /> using async and await.
/// </summary>
/// <typeparam name="TCommandParameter">The <see cref="Type" /> of the command parameter.</typeparam>
/// <seealso cref="ViewModelBase" />
/// <seealso cref="ICancellableCommand" />
public class AsyncCommand<TCommandParameter> : ViewModelBase, ICancellableCommand
{
    /// <summary>
    ///     Determines whether the command can execute in its current state.
    /// </summary>
    private readonly Func<TCommandParameter?, bool> canExecute;

    /// <summary>
    ///     Defines the method to be called when the command is invoked.
    /// </summary>
    private readonly Func<TCommandParameter?, CancellationToken, Task> executeAsync;

    /// <summary>
    ///     Synchronizes the command execution to ensure only one command at the same is executed.
    /// </summary>
    private readonly IExtendedCommandSync extendedCommandSync;

    /// <summary>
    ///     Allow to run the command in parallel to other commands.
    /// </summary>
    /// <seealso cref="ICommandSync.Enter" />
    private readonly bool force;

    /// <summary>
    ///     Handles command execution errors. If an <see cref="Exception" /> is thrown at <see cref="ICommand.Execute" /> this
    ///     error handler is called.
    /// </summary>
    private readonly Func<Exception, CancellationToken, Task> handleErrorAsync;

    /// <summary>
    ///     Indicates if the only purpose of this <see cref="AsyncCommand{TCommandParameter}" /> is to cancel the execution of
    ///     a different <see cref="AsyncCommand{TCommandParameter}" />.
    /// </summary>
    private readonly bool isCancelCommand;

    /// <summary>
    ///     The data of the <see cref="IExtendedCommandSync.ExtendedCommandSyncChanged" /> event if the
    ///     method succeeds.
    /// </summary>
    private readonly TranslatableCancellableButton? translatableCancellableButton;

    /// <summary>
    ///     The command that stops the execution of <see cref="ICommand.Execute" />.
    /// </summary>
    private IAsyncCommand? cancelCommand;

    /// <summary>
    ///     A value that indicates weather the command is executing: <c>true</c> the command is running; otherwise
    ///     <c>false</c>.
    /// </summary>
    private bool isActive;

    /// <summary>
    ///     Initializes a new instance of the <see cref="AsyncCommand{TCommandParameter}" /> class.
    /// </summary>
    /// <param name="extendedCommandSync">
    ///     Synchronizes the command execution to ensure only one command at the same is
    ///     executed.
    /// </param>
    /// <param name="canExecute">Determines whether the command can execute in its current state.</param>
    /// <param name="executeAsync">Defines the method to be called when the command is invoked.</param>
    /// <param name="handleErrorAsync">
    ///     Handles command execution errors. If an <see cref="Exception" /> is thrown at
    ///     <see cref="ICommand.Execute" /> this error handler is called.
    /// </param>
    /// <param name="force">Allow to run the command in parallel to other commands.</param>
    /// <param name="translatableCancellableButton">
    ///     The data of the <see cref="IExtendedCommandSync.ExtendedCommandSyncChanged" /> event if the
    ///     method succeeds.
    /// </param>
    public AsyncCommand(
        IExtendedCommandSync extendedCommandSync,
        Func<TCommandParameter?, bool> canExecute,
        Func<TCommandParameter?, CancellationToken, Task> executeAsync,
        Func<Exception, CancellationToken, Task> handleErrorAsync,
        bool force = false,
        TranslatableCancellableButton? translatableCancellableButton = null
    )
        : this(
            extendedCommandSync,
            canExecute,
            executeAsync,
            handleErrorAsync,
            force,
            false,
            translatableCancellableButton)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AsyncCommand{TCommandParameter}" /> class.
    /// </summary>
    /// <param name="extendedCommandSync">
    ///     Synchronizes the command execution to ensure only one command at the same is
    ///     executed.
    /// </param>
    /// <param name="canExecute">Determines whether the command can execute in its current state.</param>
    /// <param name="executeAsync">Defines the method to be called when the command is invoked.</param>
    /// <param name="handleErrorAsync">
    ///     Handles command execution errors. If an <see cref="Exception" /> is thrown at
    ///     <see cref="ICommand.Execute" /> this error handler is called.
    /// </param>
    /// <param name="force">Allow to run the command in parallel to other commands.</param>
    /// <param name="isCancelCommand">
    ///     Indicates if the only purpose of this <see cref="AsyncCommand{TCommandParameter}" /> is
    ///     to cancel the execution of a different <see cref="AsyncCommand{TCommandParameter}" />.
    /// </param>
    /// <param name="translatableCancellableButton">
    ///     The data of the <see cref="IExtendedCommandSync.ExtendedCommandSyncChanged" /> event if the
    ///     method succeeds.
    /// </param>
    private AsyncCommand(
        IExtendedCommandSync extendedCommandSync,
        Func<TCommandParameter?, bool> canExecute,
        Func<TCommandParameter?, CancellationToken, Task> executeAsync,
        Func<Exception, CancellationToken, Task> handleErrorAsync,
        bool force,
        bool isCancelCommand,
        TranslatableCancellableButton? translatableCancellableButton
    )
    {
        this.extendedCommandSync = extendedCommandSync;
        this.extendedCommandSync = extendedCommandSync;
        this.canExecute = canExecute;
        this.executeAsync = executeAsync;
        this.handleErrorAsync = handleErrorAsync;
        this.force = force;
        this.isCancelCommand = isCancelCommand;
        this.translatableCancellableButton = translatableCancellableButton;
    }

    /// <summary>
    ///     Gets the command that stops the execution of <see cref="ICommand.Execute" />.
    /// </summary>
    public IAsyncCommand? CancelCommand
    {
        get => this.cancelCommand;
        set =>
            this.SetField(
                ref this.cancelCommand,
                value);
    }

    /// <summary>
    ///     Gets a value that indicates weather the command is executing: <c>true</c> the command is running; otherwise
    ///     <c>false</c>.
    /// </summary>
    public bool IsActive
    {
        get => this.isActive;
        set =>
            this.SetField(
                ref this.isActive,
                value);
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
        if (!this.isCancelCommand)
        {
            return !this.IsActive &&
                   (this.force || !this.extendedCommandSync.IsActive) &&
                   this.canExecute((TCommandParameter?) parameter);
        }

        return this.canExecute((TCommandParameter?) parameter);
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
    public async void Execute(object? parameter)
    {
        try
        {
            await this.ExecuteAsync(parameter);
        }
        catch
        {
            // suppress exceptions in async void context
        }
    }

    /// <summary>Defines the method to be called when the command is invoked.</summary>
    /// <param name="parameter">
    ///     Data used by the command. If the command does not require data to be passed, this object can be
    ///     set to <see langword="null" />.
    /// </param>
    private async Task ExecuteAsync(object? parameter)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        try
        {
            if (!this.CanExecute(parameter))
            {
                return;
            }

            if (!this.extendedCommandSync.Enter(
                    this.translatableCancellableButton,
                    this.force))
            {
                return;
            }

            try
            {
                this.IsActive = true;

                this.CancelCommand = new AsyncCommand<object?>(
                    new ExtendedCommandSync(),
                    _ => this.IsActive &&
                         this.CancelCommand?.IsActive == false &&
                         cancellationTokenSource.IsCancellationRequested != true,
                    async (_, _) => { await cancellationTokenSource.CancelAsync(); },
                    (_, _) => Task.CompletedTask,
                    false,
                    true,
                    this.translatableCancellableButton);

                await this.executeAsync(
                    (TCommandParameter?) parameter,
                    cancellationTokenSource.Token);
            }
            finally
            {
                this.IsActive = false;
                this.CancelCommand = null;
                this.extendedCommandSync.Exit();
                CommandManager.InvalidateRequerySuggested();
            }
        }
        catch (OperationCanceledException)
        {
            // do not handle cancelled operations
        }
        catch (Exception ex)
        {
            await this.handleErrorAsync(
                ex,
                cancellationTokenSource.Token);
        }
    }
}
