namespace Libs.Wpf.Behaviours;

using System.IO;
using System.Windows;

/// <summary>
///     Add drag and drop functionality to a derived class of <see cref="UIElement" />.
/// </summary>
/// <typeparam name="T">The behaviour is added to that type of elements derived from <see cref="UIElement" />.</typeparam>
// ReSharper disable once InconsistentNaming
public abstract class PathDragAndDropUIElementBehavior<T>() : DragAndDropUIElementBehavior<T>(DataFormats.FileDrop)
    where T : UIElement
{
    /// <summary>
    ///     Gets the <see cref="DragDropEffects" /> for the <paramref name="dropped" /> element.
    /// </summary>
    /// <param name="dropped">The dropped element.</param>
    /// <returns>The effect that should be displayed.</returns>
    protected override DragDropEffects GetDragDropEffects(object dropped)
    {
        var path = PathDragAndDropUIElementBehavior<T>.GetPath(dropped);
        if (string.IsNullOrWhiteSpace(path))
        {
            return DragDropEffects.None;
        }

        try
        {
            return this.ValidatePath(path);
        }
        catch
        {
            return DragDropEffects.None;
        }
    }

    /// <summary>
    ///     Handle the drag and dropped element.
    /// </summary>
    /// <param name="dropped">The dropped element to handle.</param>
    protected override void HandleDropped(object dropped)
    {
        var path = PathDragAndDropUIElementBehavior<T>.GetPath(dropped);
        if (string.IsNullOrWhiteSpace(path))
        {
            return;
        }

        try
        {
            if (this.ValidatePath(path) == DragDropEffects.None)
            {
                return;
            }

            this.HandlePath(path);
        }
        catch
        {
            // ignore errors
        }
    }

    /// <summary>
    ///     Handle the dropped path.
    /// </summary>
    /// <param name="path">The path that is dropped.</param>
    protected abstract void HandlePath(string path);

    /// <summary>
    ///     Validate the dropped path.
    /// </summary>
    /// <param name="path">The path that is dropped.</param>
    /// <returns>The effect that should be displayed if the path is valid; <see cref="DragDropEffects.None" /> otherwise.</returns>
    protected abstract DragDropEffects ValidatePath(string path);

    /// <summary>
    ///     Extracts the path from the dropped element.
    /// </summary>
    /// <param name="dropped">The dropped element.</param>
    /// <returns>The extracted path or <c>null</c>.</returns>
    private static string? GetPath(object dropped)
    {
        return dropped is not string[] folders ? null : folders.FirstOrDefault(Path.Exists);
    }
}
