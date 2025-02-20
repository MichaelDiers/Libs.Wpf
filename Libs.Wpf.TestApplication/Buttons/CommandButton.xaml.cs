namespace Libs.Wpf.TestApplication.Buttons;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

/// <summary>
///     Interaction logic for CommandButton.xaml
/// </summary>
public partial class CommandButton : UserControl
{
    /// <summary>
    ///     Extends the <see cref="CommandButton" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="CommandParameter" />.
    /// </summary>
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register(
        nameof(CommandButton.CommandParameter),
        typeof(object),
        typeof(CommandButton),
        new PropertyMetadata(default(object)));

    /// <summary>
    ///     Extends the <see cref="CommandButton" /> by a <see cref="DependencyProperty" /> wrapped by <see cref="Command" />.
    /// </summary>
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register(
        nameof(CommandButton.Command),
        typeof(ICommand),
        typeof(CommandButton),
        new PropertyMetadata(default(ICommand)));

    /// <summary>
    ///     Extends the <see cref="CommandButton" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="ImageSource" />.
    /// </summary>
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
        nameof(CommandButton.ImageSource),
        typeof(ImageSource),
        typeof(CommandButton),
        new PropertyMetadata(default(ImageSource)));

    public CommandButton()
    {
        this.InitializeComponent();
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="CommandProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand) this.GetValue(CommandButton.CommandProperty);
        set =>
            this.SetValue(
                CommandButton.CommandProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="CommandParameterProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public object CommandParameter
    {
        get => (object) this.GetValue(CommandButton.CommandParameterProperty);
        set =>
            this.SetValue(
                CommandButton.CommandParameterProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="ImageSourceProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public ImageSource ImageSource
    {
        get => (ImageSource) this.GetValue(CommandButton.ImageSourceProperty);
        set =>
            this.SetValue(
                CommandButton.ImageSourceProperty,
                value);
    }
}
