namespace Libs.Wpf.Behaviours;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Add the drag and drop behavior for the path of a file to a <see cref="TextBox" />.
/// </summary>
public class FilePathDragAndDropTextBoxBehavior : PathDragAndDropUIElementBehavior<TextBox>
{
    /// <summary>
    ///     Extends the <see cref="FilePathDragAndDropTextBoxBehavior" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="FileEndsWithFilter" />.
    /// </summary>
    public static readonly DependencyProperty FileEndsWithFilterProperty = DependencyProperty.Register(
        nameof(FilePathDragAndDropTextBoxBehavior.FileEndsWithFilter),
        typeof(string),
        typeof(FilePathDragAndDropTextBoxBehavior),
        new PropertyMetadata(string.Empty));

    /// <summary>
    ///     Gets or sets the value of <see cref="FileEndsWithFilterProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string FileEndsWithFilter
    {
        get => (string) this.GetValue(FilePathDragAndDropTextBoxBehavior.FileEndsWithFilterProperty);
        set =>
            this.SetValue(
                FilePathDragAndDropTextBoxBehavior.FileEndsWithFilterProperty,
                value);
    }

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
        return File.Exists(path) &&
        path.EndsWith(
            this.FileEndsWithFilter,
            StringComparison.OrdinalIgnoreCase)
            ? DragDropEffects.Link
            : DragDropEffects.None;
    }
}
