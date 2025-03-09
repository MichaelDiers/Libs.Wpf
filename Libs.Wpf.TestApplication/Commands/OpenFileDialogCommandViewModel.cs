namespace Libs.Wpf.TestApplication.Commands;

using System.Windows.Input;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
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
        this.openFileDialogCommand = CustomServiceProviderBuilder
            .Build(ServiceCollectionExtensions.TryAddCommandFactory)
            .GetRequiredService<ICommandFactory>()
            .CreateOpenFileDialogCommand<object?>(
                (_, path) => this.FilePath = path,
                "Text documents (.txt)|*.txt");
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
}
