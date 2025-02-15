namespace Libs.Wpf.Tests.Commands;

using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Tests.Helper;
using Microsoft.Extensions.DependencyInjection;

/// <summary>
///     Tests of <see cref="AsyncCommand{T,TT}" />.
/// </summary>
public class AsyncCommandTests
{
    private readonly ICommandFactory commandFactory;

    public AsyncCommandTests()
    {
        var serviceProvider = CustomServiceProviderBuilder.Build(ServiceCollectionExtensions.TryAddCommandFactory);
        this.commandFactory = serviceProvider.GetRequiredService<ICommandFactory>();
    }

    [Fact]
    public async Task CancelAsync()
    {
        var commandParameter = 10;

        var preExecuteCalled = false;
        var executeCalledPre = false;
        var executeCalledPost = false;
        var postExecuteCalled = false;

        var command = this.commandFactory.CreateAsyncCommand<int, int>(
            value => value == commandParameter,
            () => preExecuteCalled = true,
            async (value, cancellationToken) =>
            {
                executeCalledPre = true;
                await Task.Delay(
                    2000,
                    cancellationToken);
                executeCalledPost = true;
                return value + 1;
            },
            task =>
            {
                postExecuteCalled = true;

                Assert.Equal(
                    commandParameter + 1,
                    task.Result);
            });

        Assert.True(command.CanExecute(commandParameter));

        command.Execute(commandParameter);
        DispatcherHelperCore.DoEvents();

        Assert.True(command.IsActive);
        Assert.NotNull(command.CancelCommand);
        command.CancelCommand.Execute(null);

        Assert.True(preExecuteCalled);
        Assert.True(executeCalledPre);
        Assert.False(executeCalledPost);
        Assert.False(postExecuteCalled);
    }

    [Fact]
    public void CanExecute()
    {
        const int trueValue = 1;
        var canExecute = new Func<int, bool>(value => value == trueValue);
        var command = this.commandFactory.CreateAsyncCommand(
            canExecute,
            () => { },
            (_, _) => Task.FromResult(7),
            _ => { });

        Assert.False(command.CanExecute(trueValue + 1));
        Assert.True(command.CanExecute(trueValue));
    }

    [Fact]
    public void Execute()
    {
        var called = false;
        var command = this.commandFactory.CreateAsyncCommand<int, bool>(
            _ => true,
            () => { },
            (value, _) =>
            {
                called = true;
                Assert.Equal(
                    10,
                    value);
                return Task.FromResult(true);
            },
            task => { });

        command.Execute(10);
        DispatcherHelperCore.DoEvents();

        Assert.True(called);
    }

    [Fact]
    public async Task ExecuteAsync()
    {
        var commandParameter = 10;

        var preExecuteCalled = false;
        var executeCalled = false;
        var postExecuteCalled = false;

        var command = this.commandFactory.CreateAsyncCommand<int, int>(
            value => value == commandParameter,
            () => preExecuteCalled = true,
            (value, _) =>
            {
                executeCalled = true;
                return Task.FromResult(value + 1);
            },
            task =>
            {
                postExecuteCalled = true;

                Assert.Equal(
                    commandParameter + 1,
                    task.Result);
            });

        Assert.True(command.CanExecute(commandParameter));

        command.Execute(commandParameter);
        DispatcherHelperCore.DoEvents();

        await Task.Delay(100);

        Assert.True(preExecuteCalled);
        Assert.True(executeCalled);
        Assert.True(postExecuteCalled);
    }

    [Fact]
    public void PostExecute()
    {
        var called = false;

        var command = this.commandFactory.CreateAsyncCommand<int, int>(
            _ => true,
            () => { },
            null,
            _ => { called = true; });

        command.Execute(10);

        Assert.True(called);
    }

    [Fact]
    public void PreExecute()
    {
        var called = false;

        var command = this.commandFactory.CreateAsyncCommand<int, int>(
            _ => true,
            () => called = true,
            (_, _) => Task.FromResult(7),
            _ => { });

        command.Execute(10);

        Assert.True(called);
    }
}
