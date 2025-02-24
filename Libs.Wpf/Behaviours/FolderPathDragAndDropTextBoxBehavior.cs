namespace Libs.Wpf.Behaviours;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Add the drag and drop behavior for the path of a folder to a <see cref="TextBox" />.
/// </summary>
public class FolderPathDragAndDropTextBoxBehavior() : DragAndDropUIElementBehavior<TextBox>(DataFormats.FileDrop)
{
    /// <summary>
    ///     Handle the drag and dropped element.
    /// </summary>
    /// <param name="dropped">The dropped element to handle.</param>
    protected override void HandleDropped(object dropped)
    {
        if (dropped is not string[] folders)
        {
            return;
        }

        var folder = folders.FirstOrDefault(Directory.Exists);
        if (folder is not null)
        {
            this.AssociatedObject.Text = folder;
        }
    }
}
