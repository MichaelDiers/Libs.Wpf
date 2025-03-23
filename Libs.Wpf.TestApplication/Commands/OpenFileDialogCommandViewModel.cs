namespace Libs.Wpf.TestApplication.Commands;

using System.IO;
using System.Windows.Input;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Threads;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

internal class OpenFileDialogCommandViewModel : ViewModelBase
{
    /// <summary>
    ///     The file path.
    /// </summary>
    private string filePath = string.Empty;

    /// <summary>
    ///     The open file dialog command.
    /// </summary>
    private ICommand openFileDialogCommand;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OpenFileDialogCommandViewModel" /> class.
    /// </summary>
    public OpenFileDialogCommandViewModel()
    {
        var commandFactory = CustomServiceProviderBuilder.Build(
                ServiceCollectionExtensions.TryAddCommandFactory,
                ThreadsServiceCollectionExtensions.TryAddDispatcherWrapper)
            .GetRequiredService<ICommandFactory>();

        this.openFileDialogCommand = commandFactory.CreateOpenFileDialogCommand(
            (_, path) => this.FilePath = path,
            "Text documents (.txt)|*.txt");

        this.OpenFileDialogCommandWithBasePath = commandFactory.CreateOpenFileDialogCommand(
            new DirectoryInfo("c:\\"),
            (_, path) => this.FilePath = path);
    }

    /// <summary>
    ///     Gets or sets the file path.
    /// </summary>
    public string FilePath
    {
        get => this.filePath;
        set =>
            this.SetField(
                ref this.filePath,
                value);
    }

    /// <summary>
    ///     Gets or sets the open file dialog command.
    /// </summary>
    public ICommand OpenFileDialogCommand
    {
        get => this.openFileDialogCommand;
        set =>
            this.SetField(
                ref this.openFileDialogCommand,
                value);
    }

    public ICommand OpenFileDialogCommandWithBasePath { get; }
}
