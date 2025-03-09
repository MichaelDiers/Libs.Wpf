namespace Libs.Wpf.Commands;

using System.IO;
using System.Windows.Input;
using Microsoft.Win32;

/// <summary>
///     An <see cref="ICommand" /> that opens a <see cref="OpenFileDialog" />. If a file is selected
///     <paramref name="execute" /> is called using the selected file and the command parameter.
/// </summary>
/// <typeparam name="T">The type of the command parameter.</typeparam>
/// <param name="basePath">The default directory of the open file dialog.</param>
/// <param name="execute">If a file is selected this <see cref="Action{T}" /> is called.</param>
/// <param name="filter">An optional <see cref="OpenFileDialog.Filter" /> of the <see cref="OpenFileDialog" />.</param>
internal class OpenFileDialogCommand<T>(DirectoryInfo basePath, Action<T?, string> execute, string? filter = null)
    : SyncCommand<T>(
        _ => true,
        commandParameter =>
        {
            var fileDialog = new OpenFileDialog
            {
                DefaultDirectory = basePath.FullName,
                CheckFileExists = true,
                CheckPathExists = true,
                Filter = filter ?? string.Empty,
                Multiselect = false,
                ValidateNames = true
            };

            if (fileDialog.ShowDialog() != true)
            {
                return;
            }

            execute(
                commandParameter,
                fileDialog.FileName);
        });
