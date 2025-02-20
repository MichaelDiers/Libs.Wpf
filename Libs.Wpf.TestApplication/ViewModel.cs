namespace Libs.Wpf.TestApplication;

using System.Windows.Input;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

internal class ViewModel : ViewModelBase
{
    /// <summary>
    ///     The command.
    /// </summary>
    private ICommand command = null!;

    public ViewModel()
    {
        var commandFactory = CustomServiceProviderBuilder.Build(ServiceCollectionExtensions.TryAddCommandFactory)
            .GetRequiredService<ICommandFactory>();
        this.Command = commandFactory.CreateSyncCommand(
            _ => true,
            _ => { });
    }

    /// <summary>
    ///     Gets or sets the command.
    /// </summary>
    public ICommand Command
    {
        get => this.command;
        set =>
            this.SetField(
                ref this.command,
                value);
    }
}
