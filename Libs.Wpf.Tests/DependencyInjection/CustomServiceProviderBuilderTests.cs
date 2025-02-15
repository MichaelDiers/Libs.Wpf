namespace Libs.Wpf.Tests.DependencyInjection;

using Libs.Wpf.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Tests of <see cref="CustomServiceProviderBuilder" />.
/// </summary>
public class CustomServiceProviderBuilderTests
{
    [Fact]
    public void Build_SucceedsAddingDependencies()
    {
        var provider = CustomServiceProviderBuilder.Build(services => services.AddSingleton<IFoo, Foo>());

        var foo = provider.GetRequiredService<IFoo>();

        Assert.IsAssignableFrom<IFoo>(foo);
    }

    [Fact]
    public void Build_SucceedsWithoutDependencies()
    {
        _ = CustomServiceProviderBuilder.Build();
    }

    private interface IFoo;

    private class Foo : IFoo;
}
