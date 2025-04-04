namespace Libs.Wpf.Tests.Commands;

using Libs.Wpf.Commands;
using Libs.Wpf.Commands.CancelWindow;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Localization;
using Microsoft.Extensions.DependencyInjection;
using Moq;

/// <summary>
///     Tests of <see cref="IAsyncCommand" />.
/// </summary>
public class AsyncCommandTests
{
    private readonly ICommandFactory commandFactory;
    private readonly ICommandSync commandSync;

    public AsyncCommandTests()
    {
        var cancelWindowMock = new Mock<ICancelWindow>();

        var cancelWindowServiceMock = new Mock<ICancelWindowService>();
        cancelWindowServiceMock.Setup(window => window.CreateCancelWindow(It.IsAny<object?>()))
            .Returns(cancelWindowMock.Object);

        var provider = CustomServiceProviderBuilder.Build(
            services => services.AddSingleton(cancelWindowServiceMock.Object),
            CommandsServiceCollectionExtensions.TryAddCommands);
        this.commandFactory = provider.GetRequiredService<ICommandFactory>();
        this.commandSync = provider.GetRequiredService<ICommandSync>();
    }

    [Fact]
    public void CanExecuteFails()
    {
        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value != commandParameter,
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("Should be an error.");
                return false;
            },
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
            });

        Assert.False(command.CanExecute(commandParameter));
    }

    [Fact]
    public void CanExecuteSucceeds()
    {
        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value == commandParameter,
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("Should be an error.");
                return false;
            },
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
            });

        Assert.True(command.CanExecute(commandParameter));
    }

    [Fact]
    public void CanExecuteThrowsException()
    {
        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            _ => throw new NotImplementedException(),
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
                return false;
            },
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
            });

        Assert.Throws<NotImplementedException>(() => command.CanExecute(commandParameter));
    }

    [Fact]
    public async Task Execute_ShouldFail_IfCommandIsCancelled()
    {
        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value == commandParameter,
            async (_, cancellationToken) =>
            {
                await Task.Delay(
                    2000,
                    cancellationToken);
                Assert.Fail("should be an error");
                return false;
            },
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
            },
            translatableCancelButton: new TranslatableCancelButton(
                Commands.ResourceManager,
                nameof(Commands.Label)));

        Assert.True(command.CanExecute(commandParameter));
        command.Execute(commandParameter);

        for (var i = 0; i < 50 && !command.IsActive; i++)
        {
            await Task.Delay(
                100,
                TestContext.Current.CancellationToken);
        }

        Assert.NotNull(command.CancelCommand);
        command.CancelCommand.Execute(null);

        for (var i = 0; i < 50 && command.IsActive; i++)
        {
            await Task.Delay(
                100,
                TestContext.Current.CancellationToken);
        }
    }

    [Fact]
    public async Task Execute_ShouldHandleError_IfExecuteThrowsException()
    {
        var executed = false;

        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value == commandParameter,
            async (_, _) =>
            {
                await Task.CompletedTask;
                throw new NotImplementedException();
            },
            async (ex, _) =>
            {
                await Task.CompletedTask;
                Assert.IsAssignableFrom<NotImplementedException>(ex);
                executed = true;
            });

        Assert.True(command.CanExecute(commandParameter));
        command.Execute(commandParameter);

        for (var i = 0; i < 50 && !executed; i++)
        {
            await Task.Delay(
                100,
                TestContext.Current.CancellationToken);
        }

        Assert.True(executed);
    }

    [Fact]
    public void Execute_ShouldNotStart_IfAnotherCommandIsActive()
    {
        Assert.True(this.commandSync.Enter());

        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value == commandParameter,
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
                return false;
            },
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
            });

        command.Execute(commandParameter);
    }

    [Fact]
    public void Execute_ShouldNotStart_IfCanExecuteFails()
    {
        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value != commandParameter,
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
                return false;
            },
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
            });

        command.Execute(commandParameter);
    }

    [Fact]
    public async Task ExecuteFails_IsActiveCheck()
    {
        var isActivated = false;
        var isDeactivated = false;

        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value == commandParameter,
            async (_, _) =>
            {
                await Task.CompletedTask;
                throw new NotImplementedException();
            },
            async (_, _) => { await Task.CompletedTask; });

        command.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName != nameof(IAsyncCommand.IsActive))
            {
                return;
            }

            if (command.IsActive)
            {
                isActivated = true;
            }
            else
            {
                isDeactivated = true;
            }
        };

        Assert.True(command.CanExecute(commandParameter));
        command.Execute(commandParameter);

        for (var i = 0; i < 50 && !isDeactivated; i++)
        {
            await Task.Delay(
                100,
                TestContext.Current.CancellationToken);
        }

        Assert.True(isActivated);
        Assert.True(isDeactivated);
    }

    [Fact]
    public async Task ExecuteSucceeds()
    {
        var executed = false;

        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value == commandParameter,
            async (value, _) =>
            {
                await Task.CompletedTask;
                Assert.Equal(
                    commandParameter,
                    value);
                executed = true;
                return true;
            },
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
            });

        Assert.True(command.CanExecute(commandParameter));
        command.Execute(commandParameter);

        for (var i = 0; i < 50 && !executed; i++)
        {
            await Task.Delay(
                100,
                TestContext.Current.CancellationToken);
        }

        Assert.True(executed);
    }

    [Fact]
    public async Task ExecuteSucceeds_IsActiveCheck()
    {
        var isActivated = false;
        var isDeactivated = false;

        const int commandParameter = 10;
        var command = this.commandFactory.CreateAsyncCommand<int?, bool>(
            this.commandSync,
            value => value == commandParameter,
            async (value, _) =>
            {
                await Task.CompletedTask;
                Assert.Equal(
                    commandParameter,
                    value);
                return true;
            },
            async (_, _) =>
            {
                await Task.CompletedTask;
                Assert.Fail("should be an error");
            });

        command.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName != nameof(IAsyncCommand.IsActive))
            {
                return;
            }

            if (command.IsActive)
            {
                isActivated = true;
            }
            else
            {
                isDeactivated = true;
            }
        };

        Assert.True(command.CanExecute(commandParameter));
        command.Execute(commandParameter);

        for (var i = 0; i < 50 && !isDeactivated; i++)
        {
            await Task.Delay(
                100,
                TestContext.Current.CancellationToken);
        }

        Assert.True(isActivated);
        Assert.True(isDeactivated);
    }
}
