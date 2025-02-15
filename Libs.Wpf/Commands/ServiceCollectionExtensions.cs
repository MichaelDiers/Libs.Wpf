namespace Libs.Wpf.Commands;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
///     Extensions for <see cref="IServiceCollection" />.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    ///     Add an <see cref="ICommandFactory" /> to the given <paramref name="services" /> as a dependency.
    /// </summary>
    /// <param name="services">The dependency is added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddCommandFactory(this IServiceCollection services)
    {
        services.TryAddSingleton<ICommandFactory, CommandFactory>();
        return services;
    }
}
