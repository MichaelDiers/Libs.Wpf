namespace Libs.Wpf.Tests.Helper;

using System.Windows.Threading;
using Libs.Wpf.Threads;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;

public class ApplicationFixture
{
    public IServiceCollection TryAddDispatcherWrapper(IServiceCollection services)
    {
        var mock = new Mock<IDispatcherWrapper>();
        mock.Setup(wrapper => wrapper.Dispatcher).Returns(Dispatcher.CurrentDispatcher);

        services.TryAddSingleton<IDispatcherWrapper>(_ => mock.Object);

        return services;
    }
}
