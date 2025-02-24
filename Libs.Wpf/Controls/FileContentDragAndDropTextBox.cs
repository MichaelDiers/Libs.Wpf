namespace Libs.Wpf.Controls;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Extends the <see cref="TextBox" />: Drag and drop a file and the <see cref="TextBox" /> uses the file content as
///     <see cref="TextBox.Text" />.
/// </summary>
public class FileContentDragAndDropTextBox() : DragAndDropTextBox(DataFormats.FileDrop)
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

        var file = files.FirstOrDefault(File.Exists);
        if (file is null)
        {
            return;
        }

        try
        {
            this.Text = File.ReadAllText(file);
        }
        catch
        {
            // ignore errors
        }
    }
}
