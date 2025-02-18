namespace Libs.Wpf.Controls;

using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

/// <summary>
///     Interaction logic for FolderSelector.xaml
/// </summary>
public partial class FolderSelector
{
    /// <summary>
    ///     Extends the <see cref="FolderSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="ButtonToolTip" />: Adds a <see cref="ToolTip" /> for the open folder button.
    /// </summary>
    public static readonly DependencyProperty ButtonToolTipProperty = DependencyProperty.Register(
        nameof(FolderSelector.ButtonToolTip),
        typeof(string),
        typeof(FolderSelector),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Extends the <see cref="FolderSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="Image" />: Add an image to the open folder button.
    /// </summary>
    public static readonly DependencyProperty ImageProperty = DependencyProperty.Register(
        nameof(FolderSelector.Image),
        typeof(ImageSource),
        typeof(FolderSelector),
        new PropertyMetadata(
            new BitmapImage(
                new Uri(
                    "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_folder.png",
                    UriKind.Absolute))));

    /// <summary>
    ///     Extends the <see cref="FolderSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="TextBoxToolTip" />.
    /// </summary>
    public static readonly DependencyProperty TextBoxToolTipProperty = DependencyProperty.Register(
        nameof(FolderSelector.TextBoxToolTip),
        typeof(string),
        typeof(FolderSelector),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Extends the <see cref="FolderSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="TextBoxWatermark" />: Adds a watermark to the textbox.
    /// </summary>
    public static readonly DependencyProperty TextBoxWatermarkProperty = DependencyProperty.Register(
        nameof(FolderSelector.TextBoxWatermark),
        typeof(string),
        typeof(FolderSelector),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Extends the <see cref="FolderSelector" /> by a <see cref="DependencyProperty" /> wrapped by <see cref="Text" />.
    ///     The text is displayed in a <see cref="TextBox" />..
    /// </summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(FolderSelector.Text),
        typeof(string),
        typeof(FolderSelector),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Initializes a new instance of the <see cref="FolderSelector" /> class.
    /// </summary>
    public FolderSelector()
    {
        this.InitializeComponent();
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="ButtonToolTipProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string ButtonToolTip
    {
        get => (string) this.GetValue(FolderSelector.ButtonToolTipProperty);
        set =>
            this.SetValue(
                FolderSelector.ButtonToolTipProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="ImageProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public ImageSource Image
    {
        get => (ImageSource) this.GetValue(FolderSelector.ImageProperty);
        set =>
            this.SetValue(
                FolderSelector.ImageProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="TextProperty" /> <see cref="DependencyProperty" />. The text is displayed in a
    ///     <see cref="TextBox" />.
    /// </summary>
    public string Text
    {
        get => (string) this.GetValue(FolderSelector.TextProperty);
        set =>
            this.SetValue(
                FolderSelector.TextProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="TextBoxToolTipProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string TextBoxToolTip
    {
        get => (string) this.GetValue(FolderSelector.TextBoxToolTipProperty);
        set =>
            this.SetValue(
                FolderSelector.TextBoxToolTipProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="TextBoxWatermarkProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string TextBoxWatermark
    {
        get => (string) this.GetValue(FolderSelector.TextBoxWatermarkProperty);
        set =>
            this.SetValue(
                FolderSelector.TextBoxWatermarkProperty,
                value);
    }

    /// <summary>
    ///     Handles the click event for the button and opens an <see cref="OpenFolderDialog" />.
    /// </summary>
    /// <param name="sender">The element that raised the event.</param>
    /// <param name="e">The event args.</param>
    private void OnClick(object sender, RoutedEventArgs e)
    {
        var folderDialog = new OpenFolderDialog
        {
            InitialDirectory = Directory.Exists(this.Text)
                ? this.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Multiselect = false,
            ValidateNames = true
        };
        if (folderDialog.ShowDialog() != true || !Directory.Exists(folderDialog.FolderName))
        {
            return;
        }

        this.Text = folderDialog.FolderName;
    }
}
