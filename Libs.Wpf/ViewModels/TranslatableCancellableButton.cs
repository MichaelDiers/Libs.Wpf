namespace Libs.Wpf.ViewModels;

using System.ComponentModel;
using System.Resources;
using System.Windows.Media;
using Libs.Wpf.Commands;
using Libs.Wpf.Localization;

/// <summary>
///     The data of a cancellable translatable button.
/// </summary>
public class TranslatableCancellableButton : TranslatableButton<ICancellableCommand>
{
    /// <summary>
    ///     The resource key of the cancel label.
    /// </summary>
    private readonly string? cancelLabelResourceKey;

    /// <summary>
    ///     The resource key of the cancel tool tip.
    /// </summary>
    private readonly string? cancelToolTipResourceKey;

    /// <summary>
    ///     The cancel image source.
    /// </summary>
    private ImageSource? cancelImageSource;

    /// <summary>
    ///     The cancel label translation.
    /// </summary>
    private string? cancelLabelTranslation;

    /// <summary>
    ///     The cancel tool tip translation.
    /// </summary>
    private string? cancelToolTipTranslation;

    /// <summary>
    ///     The data of a cancellable translatable button.
    /// </summary>
    /// <param name="command">The command of the button.</param>
    /// <param name="imageSource">The image that is displayed on the button.</param>
    /// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
    /// <param name="labelResourceKey">The resource key of the label.</param>
    /// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
    /// <param name="cancelLabelResourceKey">The resource key of the cancel label.</param>
    /// <param name="cancelToolTipResourceKey">The resource key of the cancel tool tip.</param>
    /// <param name="cancelImageSource">The image that is displayed on the cancel button.</param>
    public TranslatableCancellableButton(
        ICancellableCommand command,
        ImageSource? imageSource,
        ResourceManager resourceManager,
        string? labelResourceKey = null,
        string? toolTipResourceKey = null,
        string? cancelLabelResourceKey = null,
        string? cancelToolTipResourceKey = null,
        ImageSource? cancelImageSource = null
    )
        : base(
            command,
            imageSource,
            resourceManager,
            labelResourceKey,
            toolTipResourceKey)
    {
        this.cancelLabelResourceKey = cancelLabelResourceKey;
        this.cancelToolTipResourceKey = cancelToolTipResourceKey;
        this.cancelImageSource = cancelImageSource;

        TranslationSource.Instance.PropertyChanged += this.OnCurrentCultureChanged;

        this.OnCurrentCultureChanged(
            this,
            new PropertyChangedEventArgs(null));
    }

    /// <summary>
    ///     Gets or sets the cancel image source.
    /// </summary>
    public ImageSource? CancelImageSource
    {
        get => this.cancelImageSource;
        set =>
            this.SetField(
                ref this.cancelImageSource,
                value);
    }

    /// <summary>
    ///     Gets or sets the cancel label translation.
    /// </summary>
    public string? CancelLabelTranslation
    {
        get => this.cancelLabelTranslation;
        set =>
            this.SetField(
                ref this.cancelLabelTranslation,
                value);
    }

    /// <summary>
    ///     Gets or sets the cancel tool tip translation.
    /// </summary>
    public string? CancelToolTipTranslation
    {
        get => this.cancelToolTipTranslation;
        set =>
            this.SetField(
                ref this.cancelToolTipTranslation,
                value);
    }

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public override void Dispose()
    {
        TranslationSource.Instance.PropertyChanged -= this.OnCurrentCultureChanged;

        base.Dispose();
    }

    /// <summary>
    ///     Handles the culture change of the application.
    /// </summary>
    /// <param name="sender">The sender that raised the event.</param>
    /// <param name="e">The args of the event.</param>
    private void OnCurrentCultureChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(this.cancelLabelResourceKey))
        {
            this.CancelLabelTranslation = this.GetTranslation(this.cancelLabelResourceKey);
        }

        if (!string.IsNullOrWhiteSpace(this.cancelToolTipResourceKey))
        {
            this.CancelToolTipTranslation = this.GetTranslation(this.cancelToolTipResourceKey);
        }
    }
}
