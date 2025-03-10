namespace Libs.Wpf.Behaviours;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Add the drag and drop behavior for the content of a file to a <see cref="TextBox" />.
/// </summary>
public class FileContentDragAndDropTextBoxBehavior() : DragAndDropUIElementBehavior<TextBox>(DataFormats.FileDrop)
{
    /// <summary>
    ///     Extends the <see cref="FileContentDragAndDropTextBoxBehavior" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="FileEndsWithFilter" />.
    /// </summary>
    public static readonly DependencyProperty FileEndsWithFilterProperty = DependencyProperty.Register(
        nameof(FileContentDragAndDropTextBoxBehavior.FileEndsWithFilter),
        typeof(string),
        typeof(FileContentDragAndDropTextBoxBehavior),
        new PropertyMetadata(string.Empty));

    /// <summary>
    ///     Gets or sets the value of <see cref="FileEndsWithFilterProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string FileEndsWithFilter
    {
        get => (string) this.GetValue(FileContentDragAndDropTextBoxBehavior.FileEndsWithFilterProperty);
        set =>
            this.SetValue(
                FileContentDragAndDropTextBoxBehavior.FileEndsWithFilterProperty,
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
        if (file is null ||
            !file.EndsWith(
                this.FileEndsWithFilter,
                StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        try
        {
            this.AssociatedObject.Text = File.ReadAllText(file);
        }
        catch
        {
            // ignore error
        }
    }
}
