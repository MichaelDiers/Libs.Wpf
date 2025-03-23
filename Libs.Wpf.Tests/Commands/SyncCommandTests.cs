namespace Libs.Wpf.Tests.Commands;

using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Tests.Helper;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Tests of <see cref="SyncCommand{T}" />.
/// </summary>
[Collection(ApplicationCollectionDefinition.CollectionName)]
public class SyncCommandTests
{
    private readonly ICommandFactory commandFactory;

    public SyncCommandTests(ApplicationFixture applicationFixture)
    {
        var serviceProvider = CustomServiceProviderBuilder.Build(
            ServiceCollectionExtensions.TryAddCommandFactory,
            applicationFixture.TryAddDispatcherWrapper);
        this.commandFactory = serviceProvider.GetRequiredService<ICommandFactory>();
    }

    [Fact]
    public void CanExecute()
    {
        const int trueValue = 1;
        var canExecute = new Func<int, bool>(value => value == trueValue);
        var command = this.commandFactory.CreateSyncCommand(
            canExecute,
            _ => { });

        Assert.False(command.CanExecute(trueValue + 1));
        Assert.True(command.CanExecute(trueValue));
    }

    [Fact]
    public void Execute()
    {
        const int expectedValue = 2;
        var called = false;
        var execute = new Action<int>(
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
