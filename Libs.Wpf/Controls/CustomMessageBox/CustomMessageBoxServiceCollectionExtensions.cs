namespace Libs.Wpf.Controls.CustomMessageBox;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
///     Extensions of <see cref="IServiceCollection" />.
/// </summary>
public static class CustomMessageBoxServiceCollectionExtensions
{
    /// <summary>
    ///     Adds all supported dependencies to the given <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddCustomMessageBoxServiceCollectionExtensions(this IServiceCollection services)
    {
        services.TryAddMessageBoxService();

        return services;
    }

    /// <summary>
    ///     Adds the <see cref="IMessageBoxService" /> to the given <paramref name="services" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddMessageBoxService(this IServiceCollection services)
    {
        services.TryAddSingleton<IMessageBoxService, MessageBoxService>();

        return services;
    }
}
