namespace Libs.Wpf.Localization;

using System.Collections.ObjectModel;
using System.Resources;

/// <summary>
///     The data of a control with translatable and validable resources.
/// </summary>
public class TranslatableAndValidableComboBox<T>
    : TranslatableAndValidable<ObservableCollection<TranslatableAndValidable<T>>>
{
    /// <summary>
    ///     The selected value.
    /// </summary>
    private TranslatableAndValidable<T>? selectedValue;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TranslatableAndValidableComboBox{TValue}" /> class.
    /// </summary>
    /// <param name="values">The current values.</param>
    /// <param name="validator">The validator function.</param>
    /// <param name="validateOnInitialize">
    ///     Indicates weather the validation is executed during constructor
    ///     init.
    /// </param>
    /// <param name="resourceManager">Translations are retrieved from this resource manager.</param>
    /// <param name="labelResourceKey">The resource key of the label.</param>
    /// <param name="toolTipResourceKey">The resource key of the tool tip.</param>
    /// <param name="watermarkResourceKey">The resource key of the watermark.</param>
    public TranslatableAndValidableComboBox(
        IEnumerable<TranslatableAndValidable<T>> values,
        Func<TranslatableAndValidable<ObservableCollection<TranslatableAndValidable<T>>>, string?>? validator,
        bool validateOnInitialize,
        ResourceManager resourceManager,
        string? labelResourceKey = null,
        string? toolTipResourceKey = null,
        string? watermarkResourceKey = null
    )
        : base(
            new ObservableCollection<TranslatableAndValidable<T>>(values),
            validator,
            validateOnInitialize,
            resourceManager,
            labelResourceKey,
            toolTipResourceKey,
            watermarkResourceKey)
    {
    }

    /// <summary>
    ///     Gets or sets the selected value.
    /// </summary>
    public TranslatableAndValidable<T>? SelectedValue
    {
        get => this.selectedValue;
        set
        {
            this.SetField(
                ref this.selectedValue,
                value);

            this.Validate();
        }
    }
}
