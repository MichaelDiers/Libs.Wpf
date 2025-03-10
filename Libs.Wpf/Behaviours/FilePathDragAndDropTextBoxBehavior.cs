namespace Libs.Wpf.Behaviours;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Add the drag and drop behavior for the path of a file to a <see cref="TextBox" />.
/// </summary>
public class FilePathDragAndDropTextBoxBehavior() : DragAndDropUIElementBehavior<TextBox>(DataFormats.FileDrop)
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
    ///     Handle the drag and dropped element.
    /// </summary>
    /// <param name="dropped">The dropped element to handle.</param>
    protected override void HandleDropped(object dropped)
    {
        if (dropped is not string[] files)
        {
            return;
        }

        var file = files.FirstOrDefault(File.Exists);
        if (file is not null &&
            file.EndsWith(
                this.FileEndsWithFilter,
                StringComparison.OrdinalIgnoreCase))
        {
            this.AssociatedObject.Text = file;
        }
    }
}
