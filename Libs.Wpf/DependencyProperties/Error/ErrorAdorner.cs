namespace Libs.Wpf.DependencyProperties.Error;

using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

/// <summary>
///     An adorner for an error element.
/// </summary>
/// <param name="adornedElement">The element to bind the adorner to.</param>
/// <param name="error">The error text.</param>
/// <param name="imageSource">The source of the error image.</param>
public class ErrorAdorner(UIElement adornedElement, string error, ImageSource imageSource) : Adorner(adornedElement)
{
    /// <summary>
    ///     When overridden in a derived class, participates in rendering operations that are directed by the layout
    ///     system. The rendering instructions for this element are not used directly when this method is invoked, and are
    ///     instead preserved for later asynchronous use by layout and drawing.
    /// </summary>
    /// <param name="drawingContext">
    ///     The drawing instructions for a specific element. This context is provided to the layout
    ///     system.
    /// </param>
    protected override void OnRender(DrawingContext drawingContext)
    {
        this.IsHitTestVisible = false;

        base.OnRender(drawingContext);

        if (this.AdornedElement is not Control control)
        {
            return;
        }

        var formattedText = new FormattedText(
            error,
            CultureInfo.CurrentUICulture,
            FlowDirection.LeftToRight,
            new Typeface(control.FontFamily.Source),
            control.FontSize,
            new SolidColorBrush(Colors.Red),
            new NumberSubstitution(),
            VisualTreeHelper.GetDpi(control).PixelsPerDip);

        formattedText.SetFontStyle(FontStyles.Italic);

        var origin = new Point(
            0,
            control.ActualHeight + 5);

        var rect = new Size(
            formattedText.Height,
            formattedText.Height);

        drawingContext.DrawImage(
            imageSource,
            new Rect(
                origin,
                rect));

        origin = origin with {X = origin.X + rect.Width + 10};

        drawingContext.DrawText(
            formattedText,
            origin);
    }
}
