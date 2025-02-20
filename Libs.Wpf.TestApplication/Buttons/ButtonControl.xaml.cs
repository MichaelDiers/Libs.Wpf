namespace Libs.Wpf.TestApplication.Buttons;

using System.Windows;
using System.Windows.Controls;

/// <summary>
///     Interaction logic for ButtonControl.xaml
/// </summary>
public partial class ButtonControl
{
    /// <summary>
    ///     Extends the <see cref="ButtonControl" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="ButtonRenderSize" />.
    /// </summary>
    public static readonly DependencyProperty ButtonRenderSizeProperty = DependencyProperty.Register(
        nameof(ButtonControl.ButtonRenderSize),
        typeof(string),
        typeof(ButtonControl),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Extends the <see cref="ButtonControl" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="ButtonStyle" />.
    /// </summary>
    public static readonly DependencyProperty ButtonStyleProperty = DependencyProperty.Register(
        nameof(ButtonControl.ButtonStyle),
        typeof(Style),
        typeof(ButtonControl),
        new PropertyMetadata(default(Style)));

    /// <summary>
    ///     Extends the <see cref="ButtonControl" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="IsButtonFocused" />.
    /// </summary>
    public static readonly DependencyProperty IsButtonFocusedProperty = DependencyProperty.Register(
        nameof(ButtonControl.IsButtonFocused),
        typeof(bool),
        typeof(ButtonControl),
        new PropertyMetadata(false));

    public ButtonControl()
    {
        this.InitializeComponent();
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="ButtonRenderSizeProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string ButtonRenderSize
    {
        get => (string) this.GetValue(ButtonControl.ButtonRenderSizeProperty);
        set =>
            this.SetValue(
                ButtonControl.ButtonRenderSizeProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="ButtonStyleProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public Style ButtonStyle
    {
        get => (Style) this.GetValue(ButtonControl.ButtonStyleProperty);
        set =>
            this.SetValue(
                ButtonControl.ButtonStyleProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="IsButtonFocusedProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public bool IsButtonFocused
    {
        get => (bool) this.GetValue(ButtonControl.IsButtonFocusedProperty);
        set =>
            this.SetValue(
                ButtonControl.IsButtonFocusedProperty,
                value);
    }

    private void ButtonOnLoaded(object sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (this.IsButtonFocused)
            {
                button.Focus();
            }

            this.ButtonRenderSize = $"{button.RenderSize.Width:0.00} x {button.RenderSize.Height:0.00}";
        }
    }
}
