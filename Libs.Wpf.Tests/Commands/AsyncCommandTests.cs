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
    public async Task Cancel()
    {
        const int commandParameter = 10;

        var preExecuteCalled = false;
        var executeCalledPre = false;
        var executeCalledPost = false;
        var postExecuteCalled = false;

        var command = this.commandFactory.CreateAsyncCommand<int?, int>(
            value => value == commandParameter,
            _ => preExecuteCalled = true,
            async (value, cancellationToken) =>
            {
                executeCalledPre = true;
                await Task.Delay(
                    3000,
                    cancellationToken);
                executeCalledPost = true;
                return (value ?? 0) + 1;
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

        await AsyncCommandTests.Wait(
            command,
            () => Assert.True(preExecuteCalled),
            () => Assert.True(executeCalledPre),
            () => Assert.False(executeCalledPost),
            () => Assert.True(postExecuteCalled));

        Assert.True(preExecuteCalled);
        Assert.True(executeCalledPre);
        Assert.False(executeCalledPost);
        Assert.True(postExecuteCalled);
    }

    [Fact]
    public async Task CanExecute()
    {
        const int trueValue = 1;
        var canExecute = new Func<int?, bool>(value => value == trueValue);
        var command = this.commandFactory.CreateAsyncCommand(
            canExecute,
            _ => { },
            async (_, cancellationToken) =>
            {
                await Task.Delay(
                    5000,
                    cancellationToken);
                return Task.FromResult(7);
            },
            _ => { });

        Assert.False(command.CanExecute(trueValue + 1));
        Assert.True(command.CanExecute(trueValue));

        command.Execute(trueValue);
        await Task.Delay(1000);
        Assert.False(command.CanExecute(trueValue));
    }

    [Fact]
    public async Task CanExecute_ReturnFalse_WhenCommandIsExecuting()
    {
        var command = this.commandFactory.CreateAsyncCommand<object, object>(
            null,
            null,
            async (_, cancellationToken) =>
            {
                await Task.Delay(
                    5000,
                    cancellationToken);
                return Task.FromResult(7);
            },
            null);

        command.Execute(null);

        await Task.Delay(500);

        Assert.True(command.IsActive);

        Assert.False(command.CanExecute(null));
    }

    [Fact]
    public void CanExecute_ReturnTrue_WhenCommandIsNotExecutingAndCanExecuteIsNull()
    {
        var command = this.commandFactory.CreateAsyncCommand<object, object>(
            null,
            null,
            async (_, cancellationToken) =>
            {
                await Task.Delay(
                    5000,
                    cancellationToken);
                return Task.FromResult(7);
            },
            null);

        Assert.True(command.CanExecute(null));
    }

    [Fact]
    public async Task Execute()
    {
        var called = false;
        var command = this.commandFactory.CreateAsyncCommand<int, bool>(
            _ => true,
            _ => { },
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

        await AsyncCommandTests.Wait(
            command,
            () => Assert.True(called));

        Assert.True(called);
    }

    [Fact]
    public async Task Execute_PreExecuteOnly()
    {
        var called = false;
        var command = this.commandFactory.CreateAsyncCommand<int, bool>(
            _ => true,
            _ => { called = true; },
            null,
            null);

        command.Execute(10);

        await AsyncCommandTests.Wait(
            command,
            () => Assert.True(called));

        Assert.True(called);
    }

    [Fact]
    public async Task ExecuteAsync()
    {
        const int commandParameter = 10;

        var preExecuteCalled = false;
        var executeCalled = false;
        var postExecuteCalled = false;

        var command = this.commandFactory.CreateAsyncCommand<int, int>(
            value => value == commandParameter,
            _ => preExecuteCalled = true,
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

        await AsyncCommandTests.Wait(
            command,
            () => Assert.True(preExecuteCalled),
            () => Assert.True(executeCalled),
            () => Assert.True(postExecuteCalled));

        Assert.True(preExecuteCalled);
        Assert.True(executeCalled);
        Assert.True(postExecuteCalled);
    }

    [Fact]
    public async Task ExecutionOrder()
    {
        var preExecuteEnter = false;
        var preExecuteExit = false;

        var executeEnter = false;
        var executeExit = false;

        var postExecuteEnter = false;
        var postExecuteExit = false;

        var lockObject = new Lock();

        var command = this.commandFactory.CreateAsyncCommand<int, int>(
            _ => true,
            _ =>
            {
                lock (lockObject)
                {
                    Assert.False(preExecuteEnter);
                    Assert.False(preExecuteExit);
                    Assert.False(executeEnter);
                    Assert.False(executeExit);
                    Assert.False(postExecuteEnter);
                    Assert.False(postExecuteExit);

                    preExecuteEnter = true;
                }

                Thread.Sleep(1000);

                lock (lockObject)
                {
                    Assert.True(preExecuteEnter);
                    Assert.False(preExecuteExit);
                    Assert.False(executeEnter);
                    Assert.False(executeExit);
                    Assert.False(postExecuteEnter);
                    Assert.False(postExecuteExit);

                    preExecuteExit = true;
                }
            },
            async (_, cancellationToken) =>
            {
                lock (lockObject)
                {
                    Assert.True(preExecuteEnter);
                    Assert.True(preExecuteExit);
                    Assert.False(executeEnter);
                    Assert.False(executeExit);
                    Assert.False(postExecuteEnter);
                    Assert.False(postExecuteExit);

                    executeEnter = true;
                }

                await Task.Delay(
                    2000,
                    cancellationToken);

                lock (lockObject)
                {
                    Assert.True(preExecuteEnter);
                    Assert.True(preExecuteExit);
                    Assert.True(executeEnter);
                    Assert.False(executeExit);
                    Assert.False(postExecuteEnter);
                    Assert.False(postExecuteExit);

                    executeExit = true;
                }

                return 7;
            },
            _ =>
            {
                lock (lockObject)
                {
                    Assert.True(preExecuteEnter);
                    Assert.True(preExecuteExit);
                    Assert.True(executeEnter);
                    Assert.True(executeExit);
                    Assert.False(postExecuteEnter);
                    Assert.False(postExecuteExit);

                    postExecuteEnter = true;
                }

                lock (lockObject)
                {
                    Assert.True(preExecuteEnter);
                    Assert.True(preExecuteExit);
                    Assert.True(executeEnter);
                    Assert.True(executeExit);
                    Assert.True(postExecuteEnter);
                    Assert.False(postExecuteExit);

                    postExecuteExit = true;
                }
            });

        command.Execute(10);

        await AsyncCommandTests.Wait(
            command,
            () => Assert.True(preExecuteEnter),
            () => Assert.True(preExecuteExit),
            () => Assert.True(executeEnter),
            () => Assert.True(executeExit),
            () => Assert.True(postExecuteEnter),
            () => Assert.True(postExecuteExit));

        Assert.True(preExecuteEnter);
        Assert.True(preExecuteExit);
        Assert.True(executeEnter);
        Assert.True(executeExit);
        Assert.True(postExecuteEnter);
        Assert.True(postExecuteExit);
    }

    [Fact]
    public void PostExecute()
    {
        var called = false;

        var command = this.commandFactory.CreateAsyncCommand<int, int>(
            _ => true,
            _ => { },
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
            _ => called = true,
            (_, _) => Task.FromResult(7),
            _ => { });

        command.Execute(10);

        Assert.True(called);
    }

    private static async Task Wait(IAsyncCommand command, params Action[] asserts)
    {
        for (var i = 0; command.IsActive && i < 30; i += 1)
        {
            DispatcherHelperCore.DoEvents();

            await Task.Delay(
                200,
                CancellationToken.None);
        }

        for (var i = 0; i < 30; i += 1)
        {
            DispatcherHelperCore.DoEvents();

            await Task.Delay(
                200,
                CancellationToken.None);

            try
            {
                foreach (var assert in asserts)
                {
                    assert();
                }

                return;
            }
            catch
            {
                // next round
            }
        }
    }
}
