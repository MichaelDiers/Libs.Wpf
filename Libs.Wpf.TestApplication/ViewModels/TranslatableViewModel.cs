namespace Libs.Wpf.TestApplication.ViewModels;

using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Localization;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public class TranslatableViewModel : ViewModelBase
{
    /// <summary>
    ///     The checkbox data.
    /// </summary>
    private TranslatableAndValidable<bool> checkBoxData1;

    /// <summary>
    ///     The checkbox data.
    /// </summary>
    private TranslatableAndValidable<bool> checkBoxData2;

    /// <summary>
    ///     The combobox data.
    /// </summary>
    private TranslatableAndValidableComboBox<string> comboBoxData1;

    /// <summary>
    ///     The combobox data.
    /// </summary>
    private TranslatableAndValidableComboBox<string> comboBoxData2;

    /// <summary>
    ///     The expander data.
    /// </summary>
    private Translatable expanderData;

    /// <summary>
    ///     The groupbox data.
    /// </summary>
    private Translatable groupBoxData;

    /// <summary>
    ///     The password box data.
    /// </summary>
    private Translatable passwordBoxData;

    /// <summary>
    ///     The button control data.
    /// </summary>
    private TranslatableButton<ICommand> submitButtonData;

    /// <summary>
    ///     The text block Data.
    /// </summary>
    private Translatable textBlockData;

    /// <summary>
    ///     The text box data.
    /// </summary>
    private TranslatableAndValidable<string> textBoxData1;

    /// <summary>
    ///     The text box data.
    /// </summary>
    private TranslatableAndValidable<string> textBoxData2;

    /// <summary>
    ///     The button control data.
    /// </summary>
    private TranslatableButton<ICommand> toggleLanguageButtonData;

    /// <summary>
    ///     The button control data.
    /// </summary>
    private TranslatableButton<ICommand> translatableButton1;

    /// <summary>
    ///     The button control data.
    /// </summary>
    private TranslatableButton<ICommand> translatableButton2;

    /// <summary>
    ///     The button control data.
    /// </summary>
    private TranslatableButton<ICommand> translatableButton3;

    /// <summary>
    ///     The button control data.
    /// </summary>
    private TranslatableButton<ICommand> translatableButton4;

    public TranslatableViewModel()
    {
        // buttons
        var commandFactory = CustomServiceProviderBuilder.Build(ServiceCollectionExtensions.TryAddCommandFactory)
            .GetRequiredService<ICommandFactory>();
        this.toggleLanguageButtonData = new TranslatableButton<ICommand>(
            commandFactory.CreateSyncCommand(
                _ =>
                {
                    TranslationSource.Instance.CurrentCulture =
                        TranslationSource.Instance.CurrentCulture.TwoLetterISOLanguageName == "en"
                            ? CultureInfo.InvariantCulture
                            : new CultureInfo("en-US");
                }),
            null,
            Translations.ResourceManager,
            nameof(Translations.ToggleLanguageButtonLabel),
            nameof(Translations.ToggleLanguageButtonToolTip));
        this.submitButtonData = new TranslatableButton<ICommand>(
            commandFactory.CreateSyncCommand<PasswordBox>(
                passwordBox =>
                {
                    if (this.passwordBoxData is not null)
                    {
                        this.passwordBoxData.ErrorResourceKey = string.IsNullOrWhiteSpace(passwordBox?.Password)
                            ? nameof(Translations.PasswordBoxIsRequired)
                            : null;
                        return string.IsNullOrWhiteSpace(this.passwordBoxData.ErrorResourceKey);
                    }

                    return false;
                },
                passwordBox =>
                {
                    this.CheckBoxData1.Validate();
                    this.CheckBoxData2.Validate();
                    this.ComboBoxData1.Validate();
                    this.ComboBoxData2.Validate();
                    this.TextBoxData1.Validate();
                    this.TextBoxData2.Validate();

                    if (this.CheckBoxData1.HasError ||
                        this.CheckBoxData2.HasError ||
                        this.ComboBoxData1.HasError ||
                        this.ComboBoxData2.HasError ||
                        this.PasswordBoxData.HasError ||
                        this.TextBoxData1.HasError ||
                        this.TextBoxData2.HasError)
                    {
                        MessageBox.Show("Rejected");
                        return;
                    }

                    MessageBox.Show("Executed");
                }),
            null,
            Translations.ResourceManager,
            nameof(Translations.SubmitButtonLabel),
            nameof(Translations.SubmitButtonToolTip));

        // checkbox
        this.checkBoxData1 = new TranslatableAndValidable<bool>(
            false,
            data => data.Value ? null : nameof(Translations.CheckBoxErrorIsRequired),
            false,
            Translations.ResourceManager,
            nameof(Translations.CheckBoxLabel),
            nameof(Translations.CheckBoxToolTip));
        this.checkBoxData2 = new TranslatableAndValidable<bool>(
            false,
            data => data.Value ? null : nameof(Translations.CheckBoxErrorIsRequired),
            true,
            Translations.ResourceManager,
            nameof(Translations.CheckBoxLabel),
            nameof(Translations.CheckBoxToolTip));

        // combobox
        this.comboBoxData1 = new TranslatableAndValidableComboBox<string>(
            Enumerable.Range(
                    0,
                    3)
                .Select(
                    i => new TranslatableAndValidable<string>(
                        $"value_{i}",
                        null,
                        false,
                        Translations.ResourceManager,
                        $"ComboBoxItemLabel_{i}")),
            data => data is TranslatableAndValidableComboBox<string> {SelectedValue: not null}
                ? null
                : nameof(Translations.ComboBoxErrorIsRequired),
            false,
            Translations.ResourceManager,
            nameof(Translations.ComboBoxLabel),
            nameof(Translations.ComboBoxToolTip),
            nameof(Translations.ComboBoxWatermark));
        this.comboBoxData2 = new TranslatableAndValidableComboBox<string>(
            Enumerable.Range(
                    0,
                    3)
                .Select(
                    i => new TranslatableAndValidable<string>(
                        $"value_{i}",
                        null,
                        false,
                        Translations.ResourceManager,
                        $"ComboBoxItemLabel_{i}")),
            data => data is TranslatableAndValidableComboBox<string> {SelectedValue: not null}
                ? null
                : nameof(Translations.ComboBoxErrorIsRequired),
            true,
            Translations.ResourceManager,
            nameof(Translations.ComboBoxLabel),
            nameof(Translations.ComboBoxToolTip),
            nameof(Translations.ComboBoxWatermark));

        this.expanderData = new Translatable(
            Translations.ResourceManager,
            nameof(Translations.ExpanderLabel));
        this.groupBoxData = new Translatable(
            Translations.ResourceManager,
            nameof(Translations.GroupBoxLabel));
        this.passwordBoxData = new Translatable(
            Translations.ResourceManager,
            nameof(Translations.PasswordBoxLabel),
            nameof(Translations.PasswordBoxToolTip),
            nameof(Translations.PasswordBoxWatermark));
        this.textBlockData = new Translatable(
            Translations.ResourceManager,
            nameof(Translations.TextBlockLabel));
        this.textBoxData1 = new TranslatableAndValidable<string>(
            null,
            data => string.IsNullOrWhiteSpace(data.Value) ? nameof(Translations.TextBoxValueIsRequired) : null,
            false,
            Translations.ResourceManager,
            nameof(Translations.TextBoxLabel),
            nameof(Translations.TextBoxToolTip),
            nameof(Translations.TextBoxWatermark));
        this.textBoxData2 = new TranslatableAndValidable<string>(
            null,
            data => string.IsNullOrWhiteSpace(data.Value) ? nameof(Translations.TextBoxValueIsRequired) : null,
            true,
            Translations.ResourceManager,
            nameof(Translations.TextBoxLabel),
            nameof(Translations.TextBoxToolTip),
            nameof(Translations.TextBoxWatermark));
        // buttons
        this.translatableButton1 = new TranslatableButton<ICommand>(
            commandFactory.CreateSyncCommand(_ => { }),
            null,
            Translations.ResourceManager,
            null,
            nameof(Translations.ButtonToolTip));
        this.translatableButton2 = new TranslatableButton<ICommand>(
            commandFactory.CreateSyncCommand(_ => { }),
            new BitmapImage(
                new Uri(
                    "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_attach_file.png",
                    UriKind.Absolute)),
            Translations.ResourceManager,
            null,
            nameof(Translations.ButtonToolTip));
        this.translatableButton3 = new TranslatableButton<ICommand>(
            commandFactory.CreateSyncCommand(_ => { }),
            null,
            Translations.ResourceManager,
            nameof(Translations.ButtonLabel),
            nameof(Translations.ButtonToolTip));
        this.translatableButton4 = new TranslatableButton<ICommand>(
            commandFactory.CreateSyncCommand(_ => { }),
            new BitmapImage(
                new Uri(
                    "pack://application:,,,/Libs.Wpf;component/Assets/material_symbol_attach_file.png",
                    UriKind.Absolute)),
            Translations.ResourceManager,
            nameof(Translations.ButtonLabel),
            nameof(Translations.ButtonToolTip));
    }

    /// <summary>
    ///     Gets or sets the checkbox data.
    /// </summary>
    public TranslatableAndValidable<bool> CheckBoxData1
    {
        get => this.checkBoxData1;
        set =>
            this.SetField(
                ref this.checkBoxData1,
                value);
    }

    /// <summary>
    ///     Gets or sets the checkbox data.
    /// </summary>
    public TranslatableAndValidable<bool> CheckBoxData2
    {
        get => this.checkBoxData2;
        set =>
            this.SetField(
                ref this.checkBoxData2,
                value);
    }

    /// <summary>
    ///     Gets or sets the combobox data.
    /// </summary>
    public TranslatableAndValidableComboBox<string> ComboBoxData1
    {
        get => this.comboBoxData1;
        set =>
            this.SetField(
                ref this.comboBoxData1,
                value);
    }

    /// <summary>
    ///     Gets or sets the combobox data.
    /// </summary>
    public TranslatableAndValidableComboBox<string> ComboBoxData2
    {
        get => this.comboBoxData2;
        set =>
            this.SetField(
                ref this.comboBoxData2,
                value);
    }

    /// <summary>
    ///     Gets or sets the expander data.
    /// </summary>
    public Translatable ExpanderData
    {
        get => this.expanderData;
        set =>
            this.SetField(
                ref this.expanderData,
                value);
    }

    /// <summary>
    ///     Gets or sets the groupbox data.
    /// </summary>
    public Translatable GroupBoxData
    {
        get => this.groupBoxData;
        set =>
            this.SetField(
                ref this.groupBoxData,
                value);
    }

    /// <summary>
    ///     Gets or sets the password box data.
    /// </summary>
    public Translatable PasswordBoxData
    {
        get => this.passwordBoxData;
        set =>
            this.SetField(
                ref this.passwordBoxData,
                value);
    }

    /// <summary>
    ///     Gets or sets the button control data.
    /// </summary>
    public TranslatableButton<ICommand> SubmitButtonData
    {
        get => this.submitButtonData;
        set =>
            this.SetField(
                ref this.submitButtonData,
                value);
    }

    /// <summary>
    ///     Gets or sets the text block Data.
    /// </summary>
    public Translatable TextBlockData
    {
        get => this.textBlockData;
        set =>
            this.SetField(
                ref this.textBlockData,
                value);
    }

    /// <summary>
    ///     Gets or sets the text box data.
    /// </summary>
    public TranslatableAndValidable<string> TextBoxData1
    {
        get => this.textBoxData1;
        set =>
            this.SetField(
                ref this.textBoxData1,
                value);
    }

    /// <summary>
    ///     Gets or sets the text box data.
    /// </summary>
    public TranslatableAndValidable<string> TextBoxData2
    {
        get => this.textBoxData2;
        set =>
            this.SetField(
                ref this.textBoxData2,
                value);
    }

    /// <summary>
    ///     Gets or sets the button control data.
    /// </summary>
    public TranslatableButton<ICommand> ToggleLanguageButtonData
    {
        get => this.toggleLanguageButtonData;
        set =>
            this.SetField(
                ref this.toggleLanguageButtonData,
                value);
    }

    /// <summary>
    ///     Gets or sets the button control data.
    /// </summary>
    public TranslatableButton<ICommand> TranslatableButton1
    {
        get => this.translatableButton1;
        set =>
            this.SetField(
                ref this.translatableButton1,
                value);
    }

    /// <summary>
    ///     Gets or sets the button control data.
    /// </summary>
    public TranslatableButton<ICommand> TranslatableButton2
    {
        get => this.translatableButton2;
        set =>
            this.SetField(
                ref this.translatableButton2,
                value);
    }

    /// <summary>
    ///     Gets or sets the button control data.
    /// </summary>
    public TranslatableButton<ICommand> TranslatableButton3
    {
        get => this.translatableButton3;
        set =>
            this.SetField(
                ref this.translatableButton3,
                value);
    }

    /// <summary>
    ///     Gets or sets the button control data.
    /// </summary>
    public TranslatableButton<ICommand> TranslatableButton4
    {
        get => this.translatableButton4;
        set =>
            this.SetField(
                ref this.translatableButton4,
                value);
    }
}
