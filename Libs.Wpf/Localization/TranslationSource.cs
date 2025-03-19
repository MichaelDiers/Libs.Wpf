namespace Libs.Wpf.Localization;

using System.Globalization;
using Libs.Wpf.ViewModels;

/// <summary>
///     Handles the active <see cref="CultureInfo" />.
/// </summary>
public class TranslationSource : ViewModelBase
{
    /// <summary>
    ///     The current <see cref="CultureInfo" />.
    /// </summary>
    private CultureInfo currentCulture = CultureInfo.InvariantCulture;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TranslationSource" /> class.
    /// </summary>
    private TranslationSource()
    {
    }

    /// <summary>
    ///     Gets or sets the current <see cref="CultureInfo" />.
    /// </summary>
    public CultureInfo CurrentCulture
    {
        get => this.currentCulture;
        set =>
            this.SetField(
                ref this.currentCulture,
                value);
    }

    /// <summary>
    ///     A singleton that handles the localization of the application.
    /// </summary>
    public static TranslationSource Instance { get; } = new();
}
