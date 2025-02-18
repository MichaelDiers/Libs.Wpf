// Modified version of https://github.com/Jinjinov/wpf-localization-multiple-resource-resx-one-language/tree/master

namespace Libs.Wpf.Localization;

using System.ComponentModel;
using System.Globalization;
using System.Resources;

/// <summary>
///     Handles localization and the <see cref="ResourceManager" />s of the applications.
/// </summary>
internal class TranslationSource : INotifyPropertyChanged
{
    /// <summary>
    ///     The <see cref="Dictionary{TKey,TValue}" /> of added <see cref="ResourceManager" />-
    /// </summary>
    private readonly Dictionary<string, ResourceManager> resourceManagerDictionary = new();

    /// <summary>
    ///     The current <see cref="CompareInfo" />.
    /// </summary>
    private CultureInfo currentCulture = CultureInfo.InstalledUICulture;

    /// <summary>
    ///     Initializes a new instance of the <see cref="TranslationSource" /> class.
    /// </summary>
    private TranslationSource()
    {
    }

    /// <summary>
    ///     Gets or sets the current <see cref="CompareInfo" />.
    /// </summary>
    public CultureInfo CurrentCulture
    {
        get => this.currentCulture;
        set
        {
            if (this.currentCulture.Equals(value))
            {
                return;
            }

            this.currentCulture = value;
            // string.Empty/null indicates that all properties have changed
            this.PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(string.Empty));
        }
    }

    /// <summary>
    ///     A singleton that handles the localization of the application.
    /// </summary>
    public static TranslationSource Instance { get; } = new();

    /// <summary>
    ///     An indexer to get a translation.
    /// </summary>
    /// <param name="key">The resource key.</param>
    /// <returns>The translated key.</returns>
    public string this[string key]
    {
        get
        {
            var (resourceName, resourceKey) = TranslationSource.SplitName(key);
            if (this.resourceManagerDictionary.TryGetValue(
                    resourceName,
                    out var translation))
            {
                return translation.GetString(
                           resourceKey,
                           this.currentCulture) ??
                       key;
            }

            return key;
        }
    }

    /// <summary>
    ///     Adds an additional <see cref="ResourceManager" />.
    /// </summary>
    /// <param name="resourceManager">The <see cref="ResourceManager" /> to add.</param>
    public void AddResourceManager(ResourceManager resourceManager)
    {
        this.resourceManagerDictionary.TryAdd(
            resourceManager.BaseName,
            resourceManager);
    }

    /// <summary>
    ///     WPF bindings register PropertyChanged event if the object supports it and update themselves when it is raised
    /// </summary>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    ///     Split the <paramref name="name" /> into resource name and resource key.
    /// </summary>
    /// <param name="name">The name to split.</param>
    /// <returns>The split result.</returns>
    private static (string resourceName, string resourceKey) SplitName(string name)
    {
        var index = name.LastIndexOf('.');
        return (name[..index], name[(index + 1)..]);
    }
}
