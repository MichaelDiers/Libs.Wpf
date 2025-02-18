namespace Libs.Wpf.Controls;

using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Drag and drop support of a <see cref="TextBox" />.
/// </summary>
public abstract class DragAndDropTextBox : TextBox, IDisposable
{
    /// <summary>
    ///     A format defined in <see cref="DataFormats" />.
    /// </summary>
    private readonly string format;

    /// <summary>
    ///     Initializes a new instance of the <see cref="DragAndDropTextBox" /> class.
    /// </summary>
    /// <param name="format">A format defined in <see cref="DataFormats" />.</param>
    protected DragAndDropTextBox(string format)
    {
        this.format = format;
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
    ///     Handle the dropped data.
    /// </summary>
    /// <param name="dropped">The dropped data.</param>
    protected abstract void HandleDropped(object dropped);

    /// <summary>
    ///     Handles the <see cref="TextBox.Drop" /> event.
    /// </summary>
    /// <param name="sender">The event is raised by this <paramref name="sender" />.</param>
    /// <param name="e">The information about the dropped files.</param>
    private void OnDrop(object sender, DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(this.format))
        {
            return;
        }

        if (e.Data.GetData(DataFormats.FileDrop) is not object dropped)
        {
            return;
        }

        this.HandleDropped(dropped);
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
