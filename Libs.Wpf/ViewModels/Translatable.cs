namespace Libs.Wpf.ViewModels;

using System.ComponentModel;
using System.Resources;
using Libs.Wpf.Localization;

/// <summary>
///     The data of a control with translatable resources.
/// </summary>
public class Translatable : ViewModelBase, IDisposable
{
    /// <summary>
    ///     The resource key of the label.
    /// </summary>
    private readonly string? labelResourceKey;

    /// <summary>
    ///     The resource manager from that translations are retrieved.
    /// </summary>
    private readonly ResourceManager resourceManager;

    /// <summary>
    ///     The resource key of the tool tip.
    /// </summary>
    private readonly string? toolTipResourceKey;

    /// <summary>
    ///     The resource key of the watermark.
    /// </summary>
    private readonly string? watermarkResourceKey;

    /// <summary>
    ///     The resource key of the error.
    /// </summary>
    private string? errorResourceKey;

    /// <summary>
    ///     The error.
    /// </summary>
    private string? errorTranslation;

    /// <summary>
    ///     The translation of the <see cref="errorResourceKey" />.
    /// </summary>
    private bool hasError;

    /// <summary>
    ///     The translation of the <see cref="labelResourceKey" />.
    /// </summary>
    private string? labelTranslation;

    /// <summary>
    ///     The translation of the <see cref="toolTipResourceKey" />.
    /// </summary>
    private string? toolTipTranslation;

    /// <summary>
    ///     Gets or sets the translation of the <see cref="watermarkResourceKey" />.
    /// </summary>
    private string? watermarkTranslation;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Translatable" /> class.
    /// </summary>
    /// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
    /// <param name="labelResourceKey">The resource key of the label.</param>
    /// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
    /// <param name="watermarkResourceKey">The resource key of the watermark.</param>
    public Translatable(
        ResourceManager resourceManager,
        string? labelResourceKey = null,
        string? toolTipResourceKey = null,
        string? watermarkResourceKey = null
    )
    {
        this.resourceManager = resourceManager;
        this.labelResourceKey = labelResourceKey;
        this.toolTipResourceKey = toolTipResourceKey;
        this.watermarkResourceKey = watermarkResourceKey;

        TranslationSource.Instance.PropertyChanged += this.OnCurrentCultureChanged;

        this.OnCurrentCultureChanged(
            this,
            new PropertyChangedEventArgs(string.Empty));
    }

    /// <summary>
    ///     Gets or sets the resource key of the error.
    /// </summary>
    public string? ErrorResourceKey
    {
        get => this.errorResourceKey;
        set
        {
            this.errorResourceKey = value;
            this.ErrorTranslation = string.IsNullOrWhiteSpace(value) ? null : this.GetTranslation(value);
            this.HasError = !string.IsNullOrWhiteSpace(value);
        }
    }

    /// <summary>
    ///     Gets or sets the translation of the <see cref="errorResourceKey" />.
    /// </summary>
    public string? ErrorTranslation
    {
        get => this.errorTranslation;
        private set =>
            this.SetField(
                ref this.errorTranslation,
                value);
    }

    /// <summary>
    ///     Gets or sets a value indicating weather the validation failed.
    /// </summary>
    public bool HasError
    {
        get => this.hasError;
        private set =>
            this.SetField(
                ref this.hasError,
                value);
    }

    /// <summary>
    ///     Gets or sets the translation of the <see cref="labelResourceKey" />.
    /// </summary>
    public string? LabelTranslation
    {
        get => this.labelTranslation;
        private set =>
            this.SetField(
                ref this.labelTranslation,
                value);
    }

    /// <summary>
    ///     Gets or sets the translation of the <see cref="toolTipResourceKey" />.
    /// </summary>
    public string? ToolTipTranslation
    {
        get => this.toolTipTranslation;
        private set =>
            this.SetField(
                ref this.toolTipTranslation,
                value);
    }

    /// <summary>
    ///     Gets or sets the translation of the <see cref="watermarkResourceKey" />.
    /// </summary>
    public string? WatermarkTranslation
    {
        get => this.watermarkTranslation;
        private set =>
            this.SetField(
                ref this.watermarkTranslation,
                value);
    }

    /// <summary>
    ///     Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public virtual void Dispose()
    {
        TranslationSource.Instance.PropertyChanged -= this.OnCurrentCultureChanged;
    }

    /// <summary>
    ///     Gets the translation of the <paramref name="resourceKey" />.
    /// </summary>
    /// <param name="resourceKey">The resource key that translation is requested.</param>
    /// <returns>
    ///     The translation of the <paramref name="resourceKey" /> if available; the <paramref name="resourceKey" />
    ///     otherwise.
    /// </returns>
    protected string GetTranslation(string resourceKey)
    {
        return this.resourceManager.GetString(
                   resourceKey,
                   TranslationSource.Instance.CurrentCulture) ??
               resourceKey;
    }

    /// <summary>
    ///     Handles the culture change of the application.
    /// </summary>
    /// <param name="sender">The sender that raised the event.</param>
    /// <param name="e">The args of the event.</param>
    private void OnCurrentCultureChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (this.labelResourceKey is not null)
        {
            this.LabelTranslation = this.GetTranslation(this.labelResourceKey);
        }

        if (this.toolTipResourceKey is not null)
        {
            this.ToolTipTranslation = this.GetTranslation(this.toolTipResourceKey);
        }

        if (this.watermarkResourceKey is not null)
        {
            this.WatermarkTranslation = this.GetTranslation(this.watermarkResourceKey);
        }

        this.ErrorTranslation = string.IsNullOrWhiteSpace(this.ErrorResourceKey)
            ? null
            : this.GetTranslation(this.ErrorResourceKey);
    }
}
