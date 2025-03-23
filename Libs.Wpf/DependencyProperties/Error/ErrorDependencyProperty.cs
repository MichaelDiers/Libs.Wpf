namespace Libs.Wpf.DependencyProperties.Error;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

/// <summary>
///     An attached <see cref="DependencyProperty" /> that adds the error property.
/// </summary>
public static class ErrorDependencyProperty
{
    /// <summary>
    ///     The error <see cref="DependencyProperty" />.
    /// </summary>
    public static readonly DependencyProperty ErrorProperty = DependencyProperty.RegisterAttached(
        nameof(ErrorDependencyProperty.ErrorProperty)[..^8],
        typeof(string),
        typeof(ErrorDependencyProperty),
        new PropertyMetadata(
            null,
            ErrorDependencyProperty.OnPropertyChanged));

    /// <summary>
    ///     Gets the value of the error <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <returns>The value of the <see cref="DependencyProperty" />.</returns>
    public static string GetError(DependencyObject element)
    {
        return (string) element.GetValue(ErrorDependencyProperty.ErrorProperty);
    }

    /// <summary>
    ///     Sets the value of the error <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <param name="value">The new value of the <see cref="DependencyProperty" />.</param>
    public static void SetError(DependencyObject element, string value)
    {
        element.SetValue(
            ErrorDependencyProperty.ErrorProperty,
            value);
    }

    /// <summary>
    ///     Adds the error adorner to the <paramref name="control" />.
    /// </summary>
    /// <param name="control">The control to that the <see cref="ErrorAdorner" /> is added.</param>
    private static void AddError(Control control)
    {
        // check if the control has an adorner layer
        var adornerLayer = AdornerLayer.GetAdornerLayer(control);
        if (adornerLayer is null)
        {
            return;
        }

        // check if the adorner is already added
        var adorners = adornerLayer.GetAdorners(control);
        if (adorners is not null && adorners.Any(adorner => adorner.GetType() == typeof(ErrorAdorner)))
        {
            return;
        }

        // add the adorner
        adornerLayer.Add(
            new ErrorAdorner(
                control,
                ErrorDependencyProperty.GetError(control),
                "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_error.png"));
    }

    /// <summary>
    ///     Checks the display conditions of the <see cref="ErrorAdorner" /> for the supported controls. Handles adding and
    ///     removing the <see cref="ErrorAdorner" />.
    /// </summary>
    /// <param name="sender">The sender that has raised an event that can change the display condition of the error.</param>
    private static void HandleError(object? sender)
    {
        // reject non-controls
        if (sender is not Control control)
        {
            return;
        }

        // reject if adorner layer is not available
        var adornerLayer = AdornerLayer.GetAdornerLayer(control);
        if (adornerLayer is null)
        {
            return;
        }

        // control is not visible
        if (!control.IsVisible)
        {
            ErrorDependencyProperty.RemoveError(control);
            return;
        }

        // check display conditions for controls
        var addError = !string.IsNullOrWhiteSpace(ErrorDependencyProperty.GetError(control));

        // add the watermark
        if (addError)
        {
            ErrorDependencyProperty.AddError(control);
        }
        else
        {
            // remove the watermark
            ErrorDependencyProperty.RemoveError(control);
        }
    }

    /// <summary>
    ///     Called when an event is raised that could change the error visibility.
    /// </summary>
    /// <param name="sender">The sender that raised the event.</param>
    /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
    private static void OnHandleEvent(object sender, EventArgs e)
    {
        ErrorDependencyProperty.HandleError(sender);
    }

    /// <summary>
    ///     Called when an event is raised that could change the error visibility.
    /// </summary>
    /// <param name="sender">The sender that raised the event.</param>
    /// <param name="e">
    ///     The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event
    ///     data.
    /// </param>
    private static void OnIsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        ErrorDependencyProperty.HandleError(sender);
    }

    /// <summary>
    ///     Called when the dependency property <see cref="ErrorProperty" /> changed.
    /// </summary>
    /// <param name="d">The dependency object to the <see cref="ErrorProperty" /> is attached.</param>
    /// <param name="e">
    ///     The <see cref="System.Windows.DependencyPropertyChangedEventArgs" /> instance containing the event
    ///     data.
    /// </param>
    private static void OnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Control control)
        {
            return;
        }

        ErrorDependencyProperty.OnUnloaded(
            control,
            new RoutedEventArgs());
        ErrorDependencyProperty.RemoveError(control);
        ErrorDependencyProperty.HandleError(control);

        control.Loaded += ErrorDependencyProperty.OnHandleEvent;
        control.Unloaded += ErrorDependencyProperty.OnUnloaded;

        control.IsVisibleChanged += ErrorDependencyProperty.OnIsVisibleChanged;
    }

    /// <summary>
    ///     Called when the control to that the <see cref="ErrorProperty" /> is attached is unloaded.
    /// </summary>
    /// <param name="sender">The sender that raised the event.</param>
    /// <param name="e">The <see cref="System.Windows.RoutedEventArgs" /> instance containing the event data.</param>
    private static void OnUnloaded(object sender, RoutedEventArgs e)
    {
        if (sender is not Control control)
        {
            return;
        }

        control.Loaded -= ErrorDependencyProperty.OnHandleEvent;
        control.Unloaded -= ErrorDependencyProperty.OnUnloaded;

        control.IsVisibleChanged -= ErrorDependencyProperty.OnIsVisibleChanged;
    }

    /// <summary>
    ///     Removes the error from the adorner layer.
    /// </summary>
    /// <param name="control">The control from that the adorner is removed.</param>
    private static void RemoveError(Control control)
    {
        var adornerLayer = AdornerLayer.GetAdornerLayer(control);
        // ReSharper disable once UseNullPropagation
        if (adornerLayer is null)
        {
            return;
        }

        var adorners = adornerLayer.GetAdorners(control);
        if (adorners is null)
        {
            return;
        }

        foreach (var adorner in adorners.Where(adorner => adorner.GetType() == typeof(ErrorAdorner)))
        {
            adornerLayer.Remove(adorner);
        }
    }
}
