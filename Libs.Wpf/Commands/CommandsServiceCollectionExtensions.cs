namespace Libs.Wpf.Commands;

using Libs.Wpf.Commands.CancelWindow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
///     Extensions of <see cref="IServiceCollection" />.
/// </summary>
public static class CommandsServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the <see cref="ICancelWindowService" /> to the given <paramref name="services" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddCancelWindowService(this IServiceCollection services)
    {
        services.TryAddSingleton<ICancelWindowService, CancelWindowService>();

        return services;
    }

    /// <summary>
    ///     Adds the <see cref="ICommandFactory" /> to the given <paramref name="services" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddCommandFactory(this IServiceCollection services)
    {
        services.TryAddSingleton<ICommandFactory, CommandFactory>();

        return services;
    }

    /// <summary>
    ///     Adds all supported dependencies to the given <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddCommands(this IServiceCollection services)
    {
        services.TryAddCommandFactory();
        services.TryAddCommandSync();
        services.TryAddCancelWindowService();

        return services;
    }

    /// <summary>
    ///     Adds the <see cref="ICommandSync" /> to the given <paramref name="services" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddCommandSync(this IServiceCollection services)
    {
        services.TryAddSingleton<ICommandSync, CommandSync>();

        return services;
    }
}
