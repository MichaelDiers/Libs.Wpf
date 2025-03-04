namespace Libs.Wpf.ViewModels;

using System.Resources;

/// <summary>
///     The data of a control with translatable and validable resources.
/// </summary>
public class TranslatableAndValidable<TValue> : Translatable
{
    /// <summary>
    ///     The validator function.
    /// </summary>
    private readonly Func<TranslatableAndValidable<TValue>, string?>? validator;

    /// <summary>
    ///     The value.
    /// </summary>
    private TValue? value;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TranslatableAndValidable{TValue}" /> class.
    /// </summary>
    /// <param name="value">The current value.</param>
    /// <param name="validator">The validator function.</param>
    /// <param name="validateOnInitialize">
    ///     Indicates weather the <paramref name="value" /> is validated in the constructor
    ///     init.
    /// </param>
    /// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
    /// <param name="labelResourceKey">The resource key of the label.</param>
    /// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
    /// <param name="watermarkResourceKey">The resource key of the watermark.</param>
    public TranslatableAndValidable(
        TValue? value,
        Func<TranslatableAndValidable<TValue>, string?>? validator,
        bool validateOnInitialize,
        ResourceManager resourceManager,
        string? labelResourceKey = null,
        string? toolTipResourceKey = null,
        string? watermarkResourceKey = null
    )
        : base(
            resourceManager,
            labelResourceKey,
            toolTipResourceKey,
            watermarkResourceKey)
    {
        this.validator = validator;
        this.ErrorResourceKey = validateOnInitialize && validator is not null ? validator(this) : null;
        this.value = value;
    }

    /// <summary>
    ///     Gets or sets the value.
    /// </summary>
    public TValue? Value
    {
        get => this.value;
        set
        {
            this.SetField(
                ref this.value,
                value);

            this.Validate();
        }
    }

    /// <summary>
    ///     Validates the instance.
    /// </summary>
    public void Validate()
    {
        this.ErrorResourceKey = this.validator?.Invoke(this);
        if (string.IsNullOrWhiteSpace(this.ErrorResourceKey))
        {
            this.HasError = false;
            this.ErrorTranslation = null;
        }
        else
        {
            this.HasError = true;
            this.ErrorTranslation = this.GetTranslation(this.ErrorResourceKey);
        }
    }
}
