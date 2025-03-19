namespace Libs.Wpf.Behaviours;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Add the drag and drop behavior for the path of a folder to a <see cref="TextBox" />.
/// </summary>
public class FolderPathDragAndDropTextBoxBehavior : PathDragAndDropUIElementBehavior<TextBox>
{
    /// <summary>
    ///     Handle the dropped path.
    /// </summary>
    /// <param name="path">The path that is dropped.</param>
    protected override void HandlePath(string path)
    {
        this.AssociatedObject.Text = path;
    }

    /// <summary>
    ///     Validate the dropped path.
    /// </summary>
    /// <param name="path">The path that is dropped.</param>
    /// <returns>The effect that should be displayed if the path is valid; <see cref="DragDropEffects.None" /> otherwise.</returns>
    protected override DragDropEffects ValidatePath(string path)
    {
        return Directory.Exists(path) ? DragDropEffects.Link : DragDropEffects.None;
    }
}
