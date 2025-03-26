namespace Libs.Wpf.Commands;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
///     Extensions of <see cref="IServiceCollection" />.
/// </summary>
public static class CommandsServiceCollectionExtensions
{
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
        services.TryAddExtendedCommandSync();

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

    /// <summary>
    ///     Adds the <see cref="IExtendedCommandSync" /> to the given <paramref name="services" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddExtendedCommandSync(this IServiceCollection services)
    {
        services.TryAddSingleton<IExtendedCommandSync, ExtendedCommandSync>();

        return services;
    }
}
