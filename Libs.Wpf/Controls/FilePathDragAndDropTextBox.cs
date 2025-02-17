namespace Libs.Wpf.Controls;

using System.IO;
using System.Windows.Controls;

/// <summary>
///     Extends the <see cref="TextBox" />: Drag and drop a file and the <see cref="TextBox" /> uses the file path as
///     <see cref="TextBox.Text" />.
/// </summary>
public class FilePathDragAndDropTextBox : DragAndDropTextBox
{
    /// <summary>
    ///     Handle the dropped file.
    /// </summary>
    /// <param name="fileInfo">The file info of the dropped file.</param>
    protected override void HandleDroppedFile(FileInfo fileInfo)
    {
        this.Text = fileInfo.FullName;
    }
}
