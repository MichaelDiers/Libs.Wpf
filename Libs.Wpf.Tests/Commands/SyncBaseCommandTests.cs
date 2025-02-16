namespace Libs.Wpf.Tests.Commands;

using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Tests of <see cref="SyncBaseCommand" />.
/// </summary>
public class SyncBaseCommandTests
{
    private readonly ICommandFactory commandFactory;

    public SyncBaseCommandTests()
    {
        var serviceProvider = CustomServiceProviderBuilder.Build(ServiceCollectionExtensions.TryAddCommandFactory);
        this.commandFactory = serviceProvider.GetRequiredService<ICommandFactory>();
    }

    [Fact]
    public void CanExecute()
    {
        var canExecute = new Func<object?, bool>(_ => true);
        var command = this.commandFactory.CreateSyncCommand(
            canExecute,
            _ => { });

        Assert.True(command.CanExecute(null));
        Assert.True(command.CanExecute(new object()));
    }

    [Fact]
    public void Execute()
    {
        var expectedValue = new object();
        var called = false;
        var execute = new Action<object?>(
            value =>
            {
                Assert.Equal(
                    expectedValue,
                    value);
                called = true;
            });

        var command = this.commandFactory.CreateSyncCommand(execute);
        command.Execute(expectedValue);

        Assert.True(called);
    }
}
