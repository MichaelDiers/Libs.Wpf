namespace Libs.Wpf.Tests.Commands;

using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Tests.Helper;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Tests of <see cref="SyncBaseCommand" />.
/// </summary>
[Collection(ApplicationCollectionDefinition.CollectionName)]
public class SyncBaseCommandTests
{
    private readonly ICommandFactory commandFactory;

    public SyncBaseCommandTests(ApplicationFixture applicationFixture)
    {
        var serviceProvider = CustomServiceProviderBuilder.Build(
            CommandsServiceCollectionExtensions.TryAddCommands,
            applicationFixture.TryAddDispatcherWrapper);
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

        command = this.commandFactory.CreateSyncCommand(
            null,
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
