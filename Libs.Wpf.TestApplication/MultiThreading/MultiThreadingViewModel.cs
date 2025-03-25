namespace Libs.Wpf.TestApplication.MultiThreading;

using System.Windows.Input;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Threads;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

internal class MultiThreadingViewModel : ViewModelBase
{
    /// <summary>
    ///     The async command.
    /// </summary>
    private ICommand asyncCommand;

    /// <summary>
    ///     The command result.
    /// </summary>
    private CommandResult? commandResult;

    /// <summary>
    ///     The command.
    /// </summary>
    private ICommand customCommand;

    /// <summary>
    ///     The is running.
    /// </summary>
    private bool isRunning;

    /// <summary>
    ///     Initializes a new instance of the <see cref="MultiThreadingViewModel" /> class.
    /// </summary>
    public MultiThreadingViewModel()
    {
        this.customCommand = new CustomCommand(
            () =>
            {
                this.IsRunning = true;
                Mouse.OverrideCursor = Cursors.Wait;
                this.CommandResult = null;
            },
            async text =>
            {
                await Task.Delay(2000);
                return new CommandResultModel($"{text}{text}");
            },
            model =>
            {
                this.CommandResult = new CommandResult(model);
                this.IsRunning = false;
                Mouse.OverrideCursor = Cursors.Arrow;
            });

        this.asyncCommand = CustomServiceProviderBuilder.Build(
                CommandsServiceCollectionExtensions.TryAddCommands,
                ThreadsServiceCollectionExtensions.TryAddDispatcherWrapper)
            .GetRequiredService<ICommandFactory>()
            .CreateAsyncCommand<string, CommandResultModel>(
                _ => true,
                _ =>
                {
                    this.IsRunning = true;
                    Mouse.OverrideCursor = Cursors.Wait;
                    this.CommandResult = null;
                },
                async (text, _) =>
                {
                    await Task.Delay(2000);
                    return new CommandResultModel($"{text}{text}");
                },
                task =>
                {
                    this.CommandResult = new CommandResult(task.Result);
                    this.IsRunning = false;
                    Mouse.OverrideCursor = Cursors.Arrow;
                });
    }

    /// <summary>
    ///     Gets or sets the async command.
    /// </summary>
    public ICommand AsyncCommand
    {
        get => this.asyncCommand;
        set =>
            this.SetField(
                ref this.asyncCommand,
                value);
    }

    /// <summary>
    ///     Gets or sets the command result.
    /// </summary>
    public CommandResult? CommandResult
    {
        get => this.commandResult;
        set =>
            this.SetField(
                ref this.commandResult,
                value);
    }

    /// <summary>
    ///     Gets or sets the command.
    /// </summary>
    public ICommand CustomCommand
    {
        get => this.customCommand;
        set =>
            this.SetField(
                ref this.customCommand,
                value);
    }

    /// <summary>
    ///     Gets or sets the is running.
    /// </summary>
    public bool IsRunning
    {
        get => this.isRunning;
        set =>
            this.SetField(
                ref this.isRunning,
                value);
    }
}
