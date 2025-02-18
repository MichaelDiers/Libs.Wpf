namespace Libs.Wpf.Controls;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Extends the <see cref="TextBox" />: Drag and drop a folder and the <see cref="TextBox" /> uses the folder path as
///     <see cref="TextBox.Text" />.
/// </summary>
public class FolderDragAndDropTextBox() : DragAndDropTextBox(DataFormats.FileDrop)
{
    /// <summary>
    ///     Handle the dropped data.
    /// </summary>
    /// <param name="dropped">The dropped data.</param>
    protected override void HandleDropped(object dropped)
    {
        if (dropped is not string[] files)
        {
            return;
        }

        var directory = files.FirstOrDefault(Directory.Exists);
        if (directory is not null)
        {
            this.Text = directory;
        }
    }
}
