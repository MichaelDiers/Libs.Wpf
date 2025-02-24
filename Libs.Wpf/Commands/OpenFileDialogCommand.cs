namespace Libs.Wpf.Commands;

using System.Windows.Input;
using Microsoft.Win32;

/// <summary>
///     An <see cref="ICommand" /> that opens a <see cref="OpenFileDialog" />. If a file is selected
///     <paramref name="execute" /> is called using the selected file.
/// </summary>
/// <param name="execute">If a file is selected this <see cref="Action{T}" /> is called.</param>
internal class OpenFileDialogCommand(Action<string> execute) : SyncBaseCommand(
    _ => true,
    _ =>
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

        execute(fileDialog.FileName);
    });
