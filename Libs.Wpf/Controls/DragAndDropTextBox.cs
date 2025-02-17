namespace Libs.Wpf.Controls;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Drag and drop support of a <see cref="TextBox" />.
/// </summary>
public abstract class DragAndDropTextBox : TextBox, IDisposable
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DragAndDropTextBox" /> class.
    /// </summary>
    protected DragAndDropTextBox()
    {
        this.AllowDrop = true;
        this.PreviewDragOver += DragAndDropTextBox.OnPreviewDragOver;
        this.Drop += this.OnDrop;
    }

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.PreviewDragOver -= DragAndDropTextBox.OnPreviewDragOver;
        this.Drop -= this.OnDrop;
    }

    /// <summary>
    ///     Handle the dropped file.
    /// </summary>
    /// <param name="fileInfo">The file info of the dropped file.</param>
    protected abstract void HandleDroppedFile(FileInfo fileInfo);

    /// <summary>
    ///     Handles the <see cref="TextBox.Drop" /> event.
    /// </summary>
    /// <param name="sender">The event is raised by this <paramref name="sender" />.</param>
    /// <param name="e">The information about the dropped files.</param>
    private void OnDrop(object sender, DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(DataFormats.FileDrop) || e.Data.GetData(DataFormats.FileDrop) is not string[] files)
        {
            return;
        }

        var file = files.FirstOrDefault(File.Exists);
        if (file is not null)
        {
            this.HandleDroppedFile(new FileInfo(file));
        }
    }

    /// <summary>
    ///     Handles the <see cref="TextBox.PreviewDragOver" /> event.
    /// </summary>
    /// <param name="sender">The event is raised by this <paramref name="sender" />.</param>
    /// <param name="e">The information about the dragged files.</param>
    private static void OnPreviewDragOver(object sender, DragEventArgs e)
    {
        e.Handled = true;
    }
}
