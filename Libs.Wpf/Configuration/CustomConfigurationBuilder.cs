namespace Libs.Wpf.Configuration;

using System.IO;
using Microsoft.Extensions.Configuration;

/// <summary>
///     Build the configuration from an <c>appsettings.json</c> file.
/// </summary>
public static class CustomConfigurationBuilder
{
    /// <summary>
    ///     The default name of the settings file.
    /// </summary>
    private const string AppSettingsJson = "appsettings.json";

    /// <summary>
    ///     Read the application settings file and get the specified configuration.
    /// </summary>
    /// <typeparam name="T">The type of the configuration.</typeparam>
    /// <param name="appSettingsDirectory">
    ///     The directory that the <paramref name="appSettingsFileName" /> contains. Defaults to
    ///     <see cref="Directory.GetCurrentDirectory" />.
    /// </param>
    /// <param name="appSettingsFileName">
    ///     The name of the application settings file. Defaults to <see cref="AppSettingsJson" />
    ///     .
    /// </param>
    /// <returns>The requested configuration of type <typeparamref name="T" />.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="appSettingsFileName" /> is null or whitespace.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the configuration initialization fails.</exception>
    public static T GetConfiguration<T>(
        string? appSettingsDirectory = null,
        string? appSettingsFileName = CustomConfigurationBuilder.AppSettingsJson
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(appSettingsFileName);

        var basePath = appSettingsDirectory ?? Directory.GetCurrentDirectory();

        if (!Directory.Exists(basePath))
        {
            throw new DirectoryNotFoundException(basePath);
        }

        var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(
                appSettingsFileName,
                false,
                false);

        var configuration = builder.Build();

        return configuration.Get<T>() ?? throw new InvalidOperationException($"Cannot initialize {typeof(T).Name}.");
    }
}
