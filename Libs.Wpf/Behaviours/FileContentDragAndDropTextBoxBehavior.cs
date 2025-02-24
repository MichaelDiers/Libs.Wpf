﻿namespace Libs.Wpf.Behaviours;

using System.IO;
using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Add the drag and drop behavior for the content of a file to a <see cref="TextBox" />.
/// </summary>
public class FileContentDragAndDropTextBoxBehavior() : DragAndDropUIElementBehavior<TextBox>(DataFormats.FileDrop)
{
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
        if (file is null)
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
