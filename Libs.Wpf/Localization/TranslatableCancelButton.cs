namespace Libs.Wpf.Localization;

using System.ComponentModel;
using System.Resources;
using Libs.Wpf.Commands;

/// <summary>
///     The data of a cancel button.
/// </summary>
public class TranslatableCancelButton : TranslatableButtonBase
{
    /// <summary>
    ///     The resource key of the info text.
    /// </summary>
    private readonly string? infoTextResourceKey;

    /// <summary>
    ///     The command.
    /// </summary>
    private IAsyncCommand? command;

    /// <summary>
    ///     The cancel info text translation.
    /// </summary>
    private string? infoTextTranslation;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TranslatableCancelButton" /> class.
    /// </summary>
    /// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
    /// <param name="labelResourceKey">The resource key of the label.</param>
    /// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
    /// <param name="imageSource">The image that is displayed on the button.</param>
    /// <param name="infoTextResourceKey">The resource key of the info text.</param>
    public TranslatableCancelButton(
        ResourceManager resourceManager,
        string? labelResourceKey = null,
        string? toolTipResourceKey = null,
        string? imageSource = null,
        string? infoTextResourceKey = null
    )
        : base(
            imageSource,
            resourceManager,
            labelResourceKey,
            toolTipResourceKey)
    {
        this.infoTextResourceKey = infoTextResourceKey;
        TranslationSource.Instance.PropertyChanged += this.OnCurrentCultureChanged;

        this.OnCurrentCultureChanged(
            this,
            new PropertyChangedEventArgs(string.Empty));
    }

    /// <summary>
    ///     Gets or sets the command.
    /// </summary>
    public IAsyncCommand? Command
    {
        get => this.command;
        set =>
            this.SetField(
                ref this.command,
                value);
    }

    /// <summary>
    ///     Gets or sets the cancel info text translation.
    /// </summary>
    public string? InfoTextTranslation
    {
        get => this.infoTextTranslation;
        private set =>
            this.SetField(
                ref this.infoTextTranslation,
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
        if (this.infoTextResourceKey is not null)
        {
            this.InfoTextTranslation = this.GetTranslation(this.infoTextResourceKey);
        }
    }
}
