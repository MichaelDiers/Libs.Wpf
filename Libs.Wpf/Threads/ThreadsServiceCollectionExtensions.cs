namespace Libs.Wpf.Threads;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

/// <summary>
///     Extensions of <see cref="IServiceCollection" />.
/// </summary>
public static class ThreadsServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the <see cref="IDispatcherWrapper" /> to the given <paramref name="services" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddDispatcherWrapper(this IServiceCollection services)
    {
        services.TryAddSingleton<IDispatcherWrapper, DispatcherWrapper>();

        return services;
    }

    /// <summary>
    ///     Adds all supported dependencies to the given <see cref="IServiceCollection" />.
    /// </summary>
    /// <param name="services">The dependencies are added to this <see cref="IServiceCollection" />.</param>
    /// <returns>The given <paramref name="services" />.</returns>
    public static IServiceCollection TryAddThreads(this IServiceCollection services)
    {
        services.TryAddDispatcherWrapper();

        return services;
    }
}
