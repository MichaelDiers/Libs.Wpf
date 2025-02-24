namespace Libs.Wpf.Behaviours;

using System.Windows;
using Microsoft.Xaml.Behaviors;

/// <summary>
///     Add drag and drop functionality to a derived class of <see cref="UIElement" />.
/// </summary>
/// <typeparam name="T">The behaviour is added to that type of elements derived from <see cref="UIElement" />.</typeparam>
/// <param name="format">The format of the drag and drop element. A format defined in <see cref="DataFormats" />.</param>
// ReSharper disable once InconsistentNaming
public abstract class DragAndDropUIElementBehavior<T>(string format) : Behavior<T> where T : UIElement
{
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
        this.AssociatedObject.PreviewDragOver += DragAndDropUIElementBehavior<T>.OnPreviewDragOver;
        this.AssociatedObject.Drop += this.OnDrop;
    }

    /// <summary>
    ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
    /// </summary>
    /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
    protected override void OnDetaching()
    {
        this.AssociatedObject.AllowDrop = false;
        this.AssociatedObject.PreviewDragOver -= DragAndDropUIElementBehavior<T>.OnPreviewDragOver;
        this.AssociatedObject.Drop -= this.OnDrop;
    }

    /// <summary>
    ///     Handle the <see cref="UIElement.Drop" /> event.
    /// </summary>
    /// <param name="sender">The element that raised the event.</param>
    /// <param name="e">The associated event args.</param>
    private void OnDrop(object sender, DragEventArgs e)
    {
        if (!e.Data.GetDataPresent(format))
        {
            return;
        }

        var dropped = e.Data.GetData(format);
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
    private static void OnPreviewDragOver(object sender, DragEventArgs e)
    {
        e.Handled = true;
    }
}
