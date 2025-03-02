namespace Libs.Wpf.TestApplication.Localization;

using System.Globalization;
using System.Windows.Input;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Localization;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public class LocalizationViewModel : ValidatorViewModelBase
{
    /// <summary>
    ///     The text.
    /// </summary>
    private string text = null!;

    /// <summary>
    ///     The toggle language command.
    /// </summary>
    private ICommand toggleLanguageCommand = null!;

    /// <summary>
    ///     Initializes a new instance of the <see cref="LocalizationViewModel" /> class.
    /// </summary>
    public LocalizationViewModel()
    {
        this.Text = string.Empty;
        this.ToggleLanguageCommand = CustomServiceProviderBuilder
            .Build(ServiceCollectionExtensions.TryAddCommandFactory)
            .GetRequiredService<ICommandFactory>()
            .CreateSyncCommand(
                _ =>
                {
                    TranslationSource.Instance.CurrentCulture =
                        TranslationSource.Instance.CurrentCulture.TwoLetterISOLanguageName == "en"
                            ? new CultureInfo("de-DE")
                            : new CultureInfo("en-US");
                });
    }

    /// <summary>
    ///     Gets or sets the text.
    /// </summary>
    public string Text
    {
        get => this.text;
        set
        {
            this.SetField(
                ref this.text,
                value);
            if (string.IsNullOrWhiteSpace(value))
            {
                this.SetError(nameof(Translation.Error));
            }
            else
            {
                this.ResetErrors();
            }
        }
    }

    /// <summary>
    ///     Gets or sets the toggle language command.
    /// </summary>
    public ICommand ToggleLanguageCommand
    {
        get => this.toggleLanguageCommand;
        set =>
            this.SetField(
                ref this.toggleLanguageCommand,
                value);
    }

    internal static string GetTranslation(Func<string> getTranslation)
    {
        // Todo: language change
        var old = Translation.Culture;
        Translation.Culture = TranslationSource.Instance.CurrentCulture;
        var translation = getTranslation();
        Translation.Culture = old;
        return translation;
    }

    private string GetTranslation()
    {
        var old = Translation.Culture;
        Translation.Culture = TranslationSource.Instance.CurrentCulture;
        var translation = Translation.Error;
        Translation.Culture = old;
        return translation;
    }
}
