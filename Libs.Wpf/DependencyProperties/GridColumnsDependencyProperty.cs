namespace Libs.Wpf.DependencyProperties;

using System.Windows;
using System.Windows.Controls;

/// <summary>
///     An attached <see cref="DependencyProperty" /> that adds the GridColumns property.
/// </summary>
/// <remarks>See also https://github.com/thomasclaudiushuber/Wpf-Grid-Extensions/tree/main</remarks>
public static class GridColumnsDependencyProperty
{
    /// <summary>
    ///     The format separator.
    /// </summary>
    public const string Separator = "|";

    /// <summary>
    ///     The GridColumns <see cref="DependencyProperty" />.
    /// </summary>
    public static readonly DependencyProperty GridColumnsProperty = DependencyProperty.RegisterAttached(
        nameof(GridColumnsDependencyProperty.GridColumnsProperty)[..^8],
        typeof(string),
        typeof(GridColumnsDependencyProperty),
        new PropertyMetadata(
            null,
            GridColumnsDependencyProperty.PropertyChangedCallback));

    /// <summary>
    ///     Gets the value of the GridColumns <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <returns>The value of the <see cref="DependencyProperty" />.</returns>
    // ReSharper disable once UnusedMember.Global
    public static string GetGridColumns(DependencyObject element)
    {
        return (string) element.GetValue(GridColumnsDependencyProperty.GridColumnsProperty);
    }

    /// <summary>
    ///     Sets the value of the GridColumns <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <param name="value">The new value of the <see cref="DependencyProperty" />.</param>
    public static void SetGridColumns(DependencyObject element, string value)
    {
        element.SetValue(
            GridColumnsDependencyProperty.GridColumnsProperty,
            value);
    }

    /// <summary>
    ///     Handles changes of the <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="d">The <see cref="Grid" /> to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <param name="e">The <see cref="EventArgs" /> that describe the changes.</param>
    /// <exception cref="ArgumentException">
    ///     Is thrown if the new value
    ///     <see cref="DependencyPropertyChangedEventArgs.NewValue" /> is not valid.
    /// </exception>
    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not Grid grid)
        {
            return;
        }

        if (e.NewValue is not string columnDefinitions || string.IsNullOrWhiteSpace(columnDefinitions))
        {
            throw new ArgumentException($"Unsupported column format: {e.NewValue}");
        }

        var converter = new GridLengthConverter();

        var gridLengths = columnDefinitions.Split(GridColumnsDependencyProperty.Separator)
            .Select(gridLength => converter.ConvertFromString(gridLength))
            .ToArray();

        if (gridLengths.Any(gridLength => gridLength is null))
        {
            throw new ArgumentException($"Unsupported column format: {columnDefinitions}");
        }

        grid.ColumnDefinitions.Clear();

        foreach (var gridLength in gridLengths)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition {Width = (GridLength) gridLength!});
        }
    }
}
