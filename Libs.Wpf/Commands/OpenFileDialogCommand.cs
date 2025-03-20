namespace Libs.Wpf.Commands;

using System.IO;
using System.Windows.Input;
using Microsoft.Win32;

/// <summary>
///     An <see cref="ICommand" /> that opens a <see cref="OpenFileDialog" />. If a file is selected
///     <paramref name="execute" /> is called using the selected file and the command parameter.
/// </summary>
/// <param name="basePath">The default directory of the open file dialog.</param>
/// <param name="execute">If a file is selected this <see cref="Action{T}" /> is called.</param>
/// <param name="filter">An optional <see cref="OpenFileDialog.Filter" /> of the <see cref="OpenFileDialog" />.</param>
internal class OpenFileDialogCommand(DirectoryInfo basePath, Action<string?, string> execute, string? filter = null)
    : SyncCommand<string?>(
        _ => true,
        path =>
        {
            var baseDirectory = basePath.FullName;
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                baseDirectory = Path.GetDirectoryName(path);
            }

            var fileDialog = new OpenFileDialog
            {
                DefaultDirectory = baseDirectory,
                InitialDirectory = baseDirectory,
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
                path,
                fileDialog.FileName);
        });
