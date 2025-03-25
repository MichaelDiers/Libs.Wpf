namespace Libs.Wpf.TestApplication.Commands;

using System.IO;
using System.Windows.Input;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Threads;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

internal class OpenFolderDialogCommandViewModel : ViewModelBase
{
    /// <summary>
    ///     The folder path.
    /// </summary>
    private string folderPath = string.Empty;

    /// <summary>
    ///     Initializes a new instance of the <see cref="OpenFolderDialogCommandViewModel" /> class.
    /// </summary>
    public OpenFolderDialogCommandViewModel()
    {
        var commandFactory = CustomServiceProviderBuilder.Build(
                CommandsServiceCollectionExtensions.TryAddCommands,
                ThreadsServiceCollectionExtensions.TryAddDispatcherWrapper)
            .GetRequiredService<ICommandFactory>();

        this.OpenFolderDialogCommand = commandFactory.CreateOpenFolderDialogCommand(
            new DirectoryInfo("c:\\"),
            path => this.FolderPath = path);
    }

    /// <summary>
    ///     Gets or sets the folder path.
    /// </summary>
    public string FolderPath
    {
        get => this.folderPath;
        set =>
            this.SetField(
                ref this.folderPath,
                value);
    }

    public ICommand OpenFolderDialogCommand { get; }
}
