namespace Libs.Wpf.Commands;

using System.IO;
using System.Windows.Input;
using Microsoft.Win32;

/// <summary>
///     An <see cref="ICommand" /> that opens a <see cref="OpenFolderDialog" />. If a folder is selected
///     <paramref name="execute" /> is called using the selected folder.
/// </summary>
/// <param name="basePath">The default directory of the open folder dialog.</param>
/// <param name="execute">If a folder is selected this <see cref="Action{T}" /> is called.</param>
internal class OpenFolderDialogCommand(DirectoryInfo basePath, Action<string> execute) : SyncCommand<string>(
    _ => true,
    currentDirectory =>
    {
        var folderDialog = new OpenFolderDialog
        {
            DefaultDirectory =
                string.IsNullOrWhiteSpace(currentDirectory) || !Directory.Exists(currentDirectory)
                    ? basePath.FullName
                    : currentDirectory,
            InitialDirectory = string.IsNullOrWhiteSpace(currentDirectory) || !Directory.Exists(currentDirectory)
                ? basePath.FullName
                : currentDirectory,
            Multiselect = false,
            ValidateNames = true
        };

        if (folderDialog.ShowDialog() != true)
        {
            return;
        }

        execute(folderDialog.FolderName);
    });
