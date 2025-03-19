namespace Libs.Wpf.Behaviours;

using System.Windows;
using Microsoft.Xaml.Behaviors;

/// <summary>
///     Add drag and drop functionality to a derived class of <see cref="UIElement" />.
/// </summary>
/// <typeparam name="T">The behaviour is added to that type of elements derived from <see cref="UIElement" />.</typeparam>
/// <param name="dataFormat">The format of the drag and drop element. A format defined in <see cref="DataFormats" />.</param>
// ReSharper disable once InconsistentNaming
public abstract class DragAndDropUIElementBehavior<T>(string dataFormat) : Behavior<T> where T : UIElement
{
    /// <summary>
    ///     Gets the <see cref="DragDropEffects" /> for the <paramref name="dropped" /> element.
    /// </summary>
    /// <param name="dropped">The dropped element.</param>
    /// <returns>The effect that should be displayed.</returns>
    protected abstract DragDropEffects GetDragDropEffects(object dropped);

    /// <summary>
    ///     Handle the drag and dropped element.
    /// </summary>
    /// <param name="dropped">The dropped element to handle.</param>
    protected abstract void HandleDropped(object dropped);

    /// <summary>
    ///     Called after the behavior is attached to an AssociatedObject.
    /// </summary>
    /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
    protected override void OnAttached()
    {
        this.AssociatedObject.AllowDrop = true;
        this.AssociatedObject.PreviewDragOver += this.OnPreviewDragOver;
        this.AssociatedObject.Drop += this.OnDrop;
    }

    /// <summary>
    ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
    protected override void OnDetaching()
    {
        this.AssociatedObject.AllowDrop = false;
        this.AssociatedObject.PreviewDragOver -= this.OnPreviewDragOver;
        this.AssociatedObject.Drop -= this.OnDrop;
    }

    /// <summary>
    ///     Reads the data of the dropped element.
    /// </summary>
    /// <param name="e">The associated event args of the drag and drop event.</param>
    /// <returns>The dropped data.</returns>
    private object? GetData(DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(dataFormat))
        {
            return null;
        }

        return e.Data.GetData(dataFormat);
    }

    /// <summary>
    ///     Handle the <see cref="UIElement.Drop" /> event.
    /// </summary>
    /// <param name="sender">The element that raised the event.</param>
    /// <param name="e">The associated event args.</param>
    private void OnDrop(object sender, DragEventArgs e)
    {
        var dropped = this.GetData(e);
        if (dropped is null)
        {
            return;
        }

        this.HandleDropped(dropped);
    }

    /// <summary>
    ///     Handle the <see cref="UIElement.PreviewDragOverEvent" /> event.
    /// </summary>
    /// <param name="sender">The element that raised the event.</param>
    /// <param name="e">The associated event args.</param>
    private void OnPreviewDragOver(object sender, DragEventArgs e)
    {
        var dropped = this.GetData(e);
        if (dropped is null)
        {
            return;
        }

        e.Effects = this.GetDragDropEffects(dropped);
        e.Handled = true;
    }
}
