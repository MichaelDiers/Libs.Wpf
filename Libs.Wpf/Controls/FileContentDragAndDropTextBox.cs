namespace Libs.Wpf.Controls;

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Windows.Controls;

/// <summary>
///     Extends the <see cref="TextBox" />: Drag and drop a file and the <see cref="TextBox" /> uses the file content as
///     <see cref="TextBox.Text" />.
/// </summary>
[ExcludeFromCodeCoverage]
public class FileContentDragAndDropTextBox : DragAndDropTextBox
{
    /// <summary>
    ///     Handle the dropped file.
    /// </summary>
    /// <param name="fileInfo">The file info of the dropped file.</param>
    protected override void HandleDroppedFile(FileInfo fileInfo)
    {
        if (!fileInfo.Exists)
        {
            return;
        }

        this.Text = File.ReadAllText(
            fileInfo.FullName,
            Encoding.UTF8);
    }
}
