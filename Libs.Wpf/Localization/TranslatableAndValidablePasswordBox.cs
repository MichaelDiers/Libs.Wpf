namespace Libs.Wpf.Localization;

using System.Resources;

/// <summary>
///     The data of a control with translatable and validable resources.
/// </summary>
public class TranslatableAndValidablePasswordBox : Translatable
{
    /// <summary>
    ///     The validator function.
    /// </summary>
    private readonly Func<string?, string?>? validator;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TranslatableAndValidable{TValue}" /> class.
    /// </summary>
    /// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
    /// <param name="labelResourceKey">The resource key of the label.</param>
    /// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
    /// <param name="watermarkResourceKey">The resource key of the watermark.</param>
    public TranslatableAndValidablePasswordBox(
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
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="TranslatableAndValidable{TValue}" /> class.
    /// </summary>
    /// <param name="value">The current value.</param>
    /// <param name="validator">The validator function.</param>
    /// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
    /// <param name="labelResourceKey">The resource key of the label.</param>
    /// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
    /// <param name="watermarkResourceKey">The resource key of the watermark.</param>
    public TranslatableAndValidablePasswordBox(
        string? value,
        Func<string?, string?> validator,
        ResourceManager resourceManager,
        string? labelResourceKey = null,
        string? toolTipResourceKey = null,
        string? watermarkResourceKey = null
    )
        : this(
            resourceManager,
            labelResourceKey,
            toolTipResourceKey,
            watermarkResourceKey)
    {
        this.validator = validator;
        this.ErrorResourceKey = validator(value);
    }

    /// <summary>
    ///     Validates the instance.
    /// </summary>
    public bool Validate(string? password)
    {
        this.ErrorResourceKey = this.validator?.Invoke(password);
        return !this.HasError;
    }
}
