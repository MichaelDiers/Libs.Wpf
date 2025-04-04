namespace Libs.Wpf.Commands;

using System.Windows.Input;
using Libs.Wpf.Commands.CancelWindow;
using Libs.Wpf.Localization;
using Libs.Wpf.ViewModels;

/// <summary>
///     An implementation of <see cref="ICommand" /> using async and await.
/// </summary>
/// <typeparam name="TCommandParameter">The <see cref="Type" /> of the command parameter.</typeparam>
/// <typeparam name="TExecuteResult">The result type of the execution function.</typeparam>
/// <seealso cref="ViewModelBase" />
/// <seealso cref="ICancellableCommand" />
public class AsyncCommand<TCommandParameter, TExecuteResult> : ViewModelBase, ICancellableCommand
{
    /// <summary>
    ///     A factory for creating an <see cref="ICancelWindow" />.
    /// </summary>
    private readonly ICancelWindowService cancelWindowService;

    /// <summary>
    ///     Determines whether the command can execute in its current state.
    /// </summary>
    private readonly Func<TCommandParameter?, bool> canExecute;

    /// <summary>
    ///     Synchronizes the command execution to ensure only one command at the same is executed.
    /// </summary>
    private readonly ICommandSync commandSync;

    /// <summary>
    ///     Defines the method to be called when the command is invoked.
    /// </summary>
    private readonly Func<TCommandParameter?, CancellationToken, Task<TExecuteResult?>> executeAsync;

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
    ///     Indicates if the only purpose of this <see cref="AsyncCommand{TCommandParameter,TExecuteResult}" /> is to cancel
    ///     the execution of
    ///     a different <see cref="AsyncCommand{TCommandParameter,TExecuteResult}" />.
    /// </summary>
    private readonly bool isCancelCommand;

    private readonly Func<TExecuteResult?, Task>? postCommandFunc;

    /// <summary>
    ///     The data of the cancel button.
    /// </summary>
    private readonly TranslatableCancelButton? translatableCancelButton;

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
    ///     Initializes a new instance of the <see cref="AsyncCommand{TCommandParameter,TExecuteResult}" /> class.
    /// </summary>
    /// <param name="commandSync">
    ///     Synchronizes the command execution to ensure only one command at the same is
    ///     executed.
    /// </param>
    /// <param name="canExecute">Determines whether the command can execute in its current state.</param>
    /// <param name="executeAsync">Defines the method to be called when the command is invoked.</param>
    /// <param name="handleErrorAsync">
    ///     Handles command execution errors. If an <see cref="Exception" /> is thrown at
    ///     <see cref="ICommand.Execute" /> this error handler is called.
    /// </param>
    /// <param name="cancelWindowService">A factory for creating an <see cref="ICancelWindow" />.</param>
    /// <param name="force">Allow to run the command in parallel to other commands.</param>
    /// <param name="translatableCancelButton">
    ///     The data of the cancel button.
    /// </param>
    /// <param name="postCommandFunc">The function is called after command termination.</param>
    public AsyncCommand(
        ICommandSync commandSync,
        Func<TCommandParameter?, bool> canExecute,
        Func<TCommandParameter?, CancellationToken, Task<TExecuteResult?>> executeAsync,
        Func<Exception, CancellationToken, Task> handleErrorAsync,
        ICancelWindowService cancelWindowService,
        bool force = false,
        TranslatableCancelButton? translatableCancelButton = null,
        Func<TExecuteResult?, Task>? postCommandFunc = null
    )
        : this(
            commandSync,
            canExecute,
            executeAsync,
            handleErrorAsync,
            cancelWindowService,
            force,
            false,
            translatableCancelButton,
            postCommandFunc)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="AsyncCommand{TCommandParameter,TExecuteResult}" /> class.
    /// </summary>
    /// <param name="commandSync">
    ///     Synchronizes the command execution to ensure only one command at the same is
    ///     executed.
    /// </param>
    /// <param name="canExecute">Determines whether the command can execute in its current state.</param>
    /// <param name="executeAsync">Defines the method to be called when the command is invoked.</param>
    /// <param name="handleErrorAsync">
    ///     Handles command execution errors. If an <see cref="Exception" /> is thrown at
    ///     <see cref="ICommand.Execute" /> this error handler is called.
    /// </param>
    /// <param name="cancelWindowService">A factory for creating an <see cref="ICancelWindow" />.</param>
    /// <param name="force">Allow to run the command in parallel to other commands.</param>
    /// <param name="isCancelCommand">
    ///     Indicates if the only purpose of this <see cref="AsyncCommand{TCommandParameter,TExecuteResult}" /> is
    ///     to cancel the execution of a different <see cref="AsyncCommand{TCommandParameter,TExecuteResult}" />.
    /// </param>
    /// <param name="translatableCancelButton">
    ///     The data of the cancel button.
    /// </param>
    /// <param name="postCommandFunc">The function is called after command termination.</param>
    private AsyncCommand(
        ICommandSync commandSync,
        Func<TCommandParameter?, bool> canExecute,
        Func<TCommandParameter?, CancellationToken, Task<TExecuteResult?>> executeAsync,
        Func<Exception, CancellationToken, Task> handleErrorAsync,
        ICancelWindowService cancelWindowService,
        bool force,
        bool isCancelCommand,
        TranslatableCancelButton? translatableCancelButton,
        Func<TExecuteResult?, Task>? postCommandFunc = null
    )
    {
        this.commandSync = commandSync;
        this.commandSync = commandSync;
        this.canExecute = canExecute;
        this.executeAsync = executeAsync;
        this.handleErrorAsync = handleErrorAsync;
        this.cancelWindowService = cancelWindowService;
        this.force = force;
        this.isCancelCommand = isCancelCommand;
        this.translatableCancelButton = translatableCancelButton;
        this.postCommandFunc = postCommandFunc;
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
                   (this.force || !this.commandSync.IsActive) &&
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

            if (!this.commandSync.Enter(this.force))
            {
                return;
            }

            ICancelWindow? cancelWindow = null;

            try
            {
                this.IsActive = true;

                if (!this.isCancelCommand && this.translatableCancelButton is not null)
                {
                    this.CancelCommand = new AsyncCommand<object?, object?>(
                        new CommandSync(),
                        _ => this.IsActive && cancellationTokenSource.IsCancellationRequested != true,
                        async (_, _) =>
                        {
                            await cancellationTokenSource.CancelAsync();
                            return null;
                        },
                        (_, _) => Task.CompletedTask,
                        this.cancelWindowService,
                        false,
                        true,
                        null);
                    this.translatableCancelButton.Command = this.CancelCommand;

                    cancelWindow = this.cancelWindowService.CreateCancelWindow(this.translatableCancelButton);
                    cancelWindow.Show();
                }

                var executionResult = await this.executeAsync(
                    (TCommandParameter?) parameter,
                    cancellationTokenSource.Token);

                if (this.postCommandFunc is not null)
                {
                    this.IsActive = false;
                    cancelWindow?.Close();
                    await this.postCommandFunc(executionResult);
                }
            }
            finally
            {
                this.IsActive = false;
                this.CancelCommand = null;
                this.commandSync.Exit();
                CommandManager.InvalidateRequerySuggested();
                cancelWindow?.Close();
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
