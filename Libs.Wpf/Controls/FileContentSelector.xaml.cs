namespace Libs.Wpf.Controls;

using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

/// <summary>
///     Interaction logic for FileContentSelector.xaml
/// </summary>
public partial class FileContentSelector : IDisposable
{
    /// <summary>
    ///     Extends the <see cref="FileContentSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="ButtonToolTip" />: Adds a <see cref="ToolTip" /> for the open file button.
    /// </summary>
    public static readonly DependencyProperty ButtonToolTipProperty = DependencyProperty.Register(
        nameof(FileContentSelector.ButtonToolTip),
        typeof(string),
        typeof(FileContentSelector),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Extends the <see cref="FileContentSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="ImageSource" />: Add an image to the open file button.
    /// </summary>
    public static readonly DependencyProperty ImageSourceProperty = DependencyProperty.Register(
        nameof(FileContentSelector.ImageSource),
        typeof(ImageSource),
        typeof(FileContentSelector),
        new PropertyMetadata(
            new BitmapImage(
                new Uri(
                    "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_attach_file.png",
                    UriKind.Absolute))));

    /// <summary>
    ///     Extends the <see cref="FileContentSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="TextBoxToolTip" />.
    /// </summary>
    public static readonly DependencyProperty TextBoxToolTipProperty = DependencyProperty.Register(
        nameof(FileContentSelector.TextBoxToolTip),
        typeof(string),
        typeof(FileContentSelector),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Extends the <see cref="FileContentSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="TextBoxWatermark" />: Adds a watermark to the textbox.
    /// </summary>
    public static readonly DependencyProperty TextBoxWatermarkProperty = DependencyProperty.Register(
        nameof(FileContentSelector.TextBoxWatermark),
        typeof(string),
        typeof(FileContentSelector),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Extends the <see cref="FileContentSelector" /> by a <see cref="DependencyProperty" /> wrapped by
    ///     <see cref="Text" />.
    ///     The text is displayed in a <see cref="TextBox" />..
    /// </summary>
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register(
        nameof(FileContentSelector.Text),
        typeof(string),
        typeof(FileContentSelector),
        new PropertyMetadata(default(string)));

    /// <summary>
    ///     Initializes a new instance of the <see cref="FileContentSelector" /> class.
    /// </summary>
    public FileContentSelector()
    {
        this.InitializeComponent();
        this.FileContentDragAndDropTextBoxName.TextChanged += this.FileContentDragAndDropTextBoxNameOnTextChanged;
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="ButtonToolTipProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string ButtonToolTip
    {
        get => (string) this.GetValue(FileContentSelector.ButtonToolTipProperty);
        set =>
            this.SetValue(
                FileContentSelector.ButtonToolTipProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="ImageSourceProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public ImageSource ImageSource
    {
        get => (ImageSource) this.GetValue(FileContentSelector.ImageSourceProperty);
        set =>
            this.SetValue(
                FileContentSelector.ImageSourceProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="TextProperty" /> <see cref="DependencyProperty" />. The text is displayed in a
    ///     <see cref="TextBox" />.
    /// </summary>
    public string Text
    {
        get => (string) this.GetValue(FileContentSelector.TextProperty);
        set =>
            this.SetValue(
                FileContentSelector.TextProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="TextBoxToolTipProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string TextBoxToolTip
    {
        get => (string) this.GetValue(FileContentSelector.TextBoxToolTipProperty);
        set =>
            this.SetValue(
                FileContentSelector.TextBoxToolTipProperty,
                value);
    }

    /// <summary>
    ///     Gets or sets the value of <see cref="TextBoxWatermarkProperty" /> <see cref="DependencyProperty" />.
    /// </summary>
    public string TextBoxWatermark
    {
        get => (string) this.GetValue(FileContentSelector.TextBoxWatermarkProperty);
        set =>
            this.SetValue(
                FileContentSelector.TextBoxWatermarkProperty,
                value);
    }

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.FileContentDragAndDropTextBoxName.TextChanged -= this.FileContentDragAndDropTextBoxNameOnTextChanged;
    }

    /// <summary>
    ///     Handles the <see cref="TextBox.TextChanged" /> event for the <see cref="FileContentDragAndDropTextBoxName" />
    ///     element.
    /// </summary>
    /// <param name="sender">The element that raised the event.</param>
    /// <param name="e">The <see cref="TextChangedEventArgs" />.</param>
    private void FileContentDragAndDropTextBoxNameOnTextChanged(object sender, TextChangedEventArgs e)
    {
        this.Text = this.FileContentDragAndDropTextBoxName.Text;
    }

    /// <summary>
    ///     Handles the click event for the button and opens an <see cref="OpenFileDialog" />.
    /// </summary>
    /// <param name="sender">The element that raised the event.</param>
    /// <param name="e">The event args.</param>
    private void OnClick(object sender, RoutedEventArgs e)
    {
        var fileDialog = new OpenFileDialog
        {
            InitialDirectory = Directory.Exists(this.Text)
                ? this.Text
                : Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
            Multiselect = false,
            ValidateNames = true
        };
        if (fileDialog.ShowDialog() != true || !File.Exists(fileDialog.FileName))
        {
            return;
        }

        try
        {
            this.Text = File.ReadAllText(fileDialog.FileName);
        }
        catch
        {
            // no error handling
        }
    }
}
