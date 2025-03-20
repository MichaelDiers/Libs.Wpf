namespace Libs.Wpf.TestApplication.MultiThreading;

using System.Windows;
using System.Windows.Input;

internal class CustomCommand(
    Action preExecute,
    Func<string, Task<CommandResultModel>> execute,
    Action<CommandResultModel> dispatchAction
) : ICommand
{
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
        return parameter is string s && !string.IsNullOrWhiteSpace(s);
    }

    /// <summary>Occurs when changes take place that affect whether the command should execute.</summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>Defines the method to be called when the command is invoked.</summary>
    /// <param name="parameter">
    ///     Data used by the command. If the command does not require data to be passed, this object can be
    ///     set to <see langword="null" />.
    /// </param>
    public void Execute(object? parameter)
    {
        if (parameter is not string s || !this.CanExecute(s))
        {
            return;
        }

        preExecute();

        Task.Run(() => this.RunExecute(s));
    }

    private async Task DispatchActionExecute(CommandResultModel commandResultModel)
    {
        if (Application.Current.Dispatcher.Thread == Thread.CurrentThread)
        {
            dispatchAction(commandResultModel);
        }
        else
        {
            await Application.Current.Dispatcher.InvokeAsync(() => this.DispatchActionExecute(commandResultModel));
        }
    }

    private async Task RunExecute(string s)
    {
        var result = await execute(s);

        await this.DispatchActionExecute(result);
    }
}
