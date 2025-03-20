﻿namespace Libs.Wpf.DependencyProperties;

using System.Windows;
using System.Windows.Controls;

/// <summary>
///     An attached <see cref="DependencyProperty" /> that adds the GridRows property.
/// </summary>
/// <remarks>See also https://github.com/thomasclaudiushuber/Wpf-Grid-Extensions/tree/main</remarks>
public static class GridRowsDependencyProperty
{
    /// <summary>
    ///     The format separator.
    /// </summary>
    public const string Separator = "|";

    /// <summary>
    ///     The GridRows <see cref="DependencyProperty" />.
    /// </summary>
    public static readonly DependencyProperty GridRowsProperty = DependencyProperty.RegisterAttached(
        nameof(GridRowsDependencyProperty.GridRowsProperty)[..^8],
        typeof(string),
        typeof(GridRowsDependencyProperty),
        new PropertyMetadata(
            null,
            GridRowsDependencyProperty.PropertyChangedCallback));

    /// <summary>
    ///     Gets the value of the GridRows <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <returns>The value of the <see cref="DependencyProperty" />.</returns>
    // ReSharper disable once UnusedMember.Global
    public static string GetGridRows(DependencyObject element)
    {
        return (string) element.GetValue(GridRowsDependencyProperty.GridRowsProperty);
    }

    /// <summary>
    ///     Sets the value of the GridRows <see cref="DependencyProperty" />.
    /// </summary>
    /// <param name="element">The element to that the <see cref="DependencyProperty" /> is attached to.</param>
    /// <param name="value">The new value of the <see cref="DependencyProperty" />.</param>
    public static void SetGridRows(DependencyObject element, string value)
    {
        element.SetValue(
            GridRowsDependencyProperty.GridRowsProperty,
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

        if (e.NewValue is not string rowDefinitions || string.IsNullOrWhiteSpace(rowDefinitions))
        {
            throw new ArgumentException($"Unsupported row format: {e.NewValue}");
        }

        var converter = new GridLengthConverter();

        var gridLengths = rowDefinitions.Split(GridRowsDependencyProperty.Separator)
            .Select(gridLength => converter.ConvertFromString(gridLength))
            .ToArray();

        if (gridLengths.Any(gridLength => gridLength is null))
        {
            throw new ArgumentException($"Unsupported row format: {rowDefinitions}");
        }

        grid.RowDefinitions.Clear();

        foreach (var gridLength in gridLengths)
        {
            grid.RowDefinitions.Add(new RowDefinition {Height = (GridLength) gridLength!});
        }
    }
}
