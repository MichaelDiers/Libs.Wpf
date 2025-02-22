namespace Libs.Wpf.TestApplication.Controls;

using System.Windows;
using System.Windows.Input;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

internal class ControlsViewModel : ViewModelBase
{
    private readonly ICommandFactory commandFactory = CustomServiceProviderBuilder
        .Build(ServiceCollectionExtensions.TryAddCommandFactory)
        .GetRequiredService<ICommandFactory>();

    /// <summary>
    ///     The command.
    /// </summary>
    private ICommand command;

    /// <summary>
    ///     The file content.
    /// </summary>
    private string fileContent;

    /// <summary>
    ///     The folder.
    /// </summary>
    private string folder = string.Empty;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ControlsViewModel" /> class.
    /// </summary>
    public ControlsViewModel()
    {
        this.command = this.commandFactory.CreateSyncCommand(
            _ => true,
            _ => MessageBox.Show(
                "Button Clicked",
                "Command Executed",
                MessageBoxButton.OK,
                MessageBoxImage.Information));
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

    /// <summary>
    ///     Gets or sets the file content.
    /// </summary>
    public string FileContent
    {
        get => this.fileContent;
        set =>
            this.SetField(
                ref this.fileContent,
                value);
    }

    /// <summary>
    ///     Gets or sets the folder.
    /// </summary>
    public string Folder
    {
        get => this.folder;
        set =>
            this.SetField(
                ref this.folder,
                value);
    }
}
