namespace Libs.Wpf.Commands;

using System.Windows.Input;
using Microsoft.Win32;

/// <summary>
///     An <see cref="ICommand" /> that opens a <see cref="OpenFileDialog" />. If a file is selected
///     <paramref name="execute" /> is called using the selected file and the command parameter.
/// </summary>
/// <typeparam name="T">The type of the command parameter.</typeparam>
/// <param name="execute">If a file is selected this <see cref="Action{T}" /> is called.</param>
internal class OpenFileDialogCommand<T>(Action<T?, string> execute) : SyncCommand<T>(
    _ => true,
    commandParameter =>
    {
        var fileDialog = new OpenFileDialog
        {
            CheckFileExists = true,
            CheckPathExists = true,
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
