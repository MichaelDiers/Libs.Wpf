namespace Libs.Wpf.Tests.Commands;

using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Localization;
using Microsoft.Extensions.DependencyInjection;

public class ExtendedCommandSyncTests
{
    private readonly ICommandFactory commandFactory;
    private readonly IExtendedCommandSync extendedCommandSync;

    public ExtendedCommandSyncTests()
    {
        var provider = CustomServiceProviderBuilder.Build(CommandsServiceCollectionExtensions.TryAddCommands);
        this.extendedCommandSync = provider.GetRequiredService<IExtendedCommandSync>();
        this.commandFactory = provider.GetRequiredService<ICommandFactory>();
    }

    [Fact]
    public void Enter()
    {
        Assert.False(this.extendedCommandSync.IsActive);

        Assert.True(this.extendedCommandSync.Enter());
        Assert.True(this.extendedCommandSync.IsActive);

        Assert.False(this.extendedCommandSync.Enter());

        this.extendedCommandSync.Exit();
        Assert.False(this.extendedCommandSync.IsActive);

        Assert.True(this.extendedCommandSync.Enter(true));
        Assert.True(this.extendedCommandSync.IsActive);

        Assert.False(this.extendedCommandSync.Enter());
        Assert.True(this.extendedCommandSync.IsActive);

        Assert.True(this.extendedCommandSync.Enter(true));
        Assert.True(this.extendedCommandSync.IsActive);

        this.extendedCommandSync.Exit();
        Assert.True(this.extendedCommandSync.IsActive);

        this.extendedCommandSync.Exit();
        Assert.False(this.extendedCommandSync.IsActive);
    }

    [Fact]
    public void Exit()
    {
        Assert.False(this.extendedCommandSync.IsActive);

        this.extendedCommandSync.Exit();

        Assert.False(this.extendedCommandSync.IsActive);

        Assert.True(this.extendedCommandSync.Enter());
        Assert.True(this.extendedCommandSync.IsActive);

        this.extendedCommandSync.Exit();
        Assert.False(this.extendedCommandSync.IsActive);

        this.extendedCommandSync.Exit();
        Assert.False(this.extendedCommandSync.IsActive);
    }

    [Fact]
    public async Task ExtendedCommandSyncChanged()
    {
        var data = new TranslatableCancellableButton(
            this.commandFactory.CreateAsyncCommand(
                this.extendedCommandSync,
                () => true,
                async cancellationToken => await Task.Delay(
                    1,
                    cancellationToken),
                async (_, cancellationToken) =>
                {
                    await Task.Delay(
                        1,
                        cancellationToken);
                }),
            null,
            Commands.ResourceManager);

        bool? isActive = null;
        TranslatableCancellableButton? translatable = null;
        this.extendedCommandSync.ExtendedCommandSyncChanged += (_, e) =>
        {
            isActive = e.IsCommandActive;
            translatable = e.TranslatableCancellableButton;
        };

        Assert.True(this.extendedCommandSync.Enter(data));
        for (var i = 0; i < 20 && isActive is null; i++)
        {
            await Task.Delay(100);
        }

        Assert.True(isActive);
        Assert.Equal(
            data,
            translatable);

        isActive = null;
        translatable = null;

        Assert.False(this.extendedCommandSync.Enter(data));
        await Task.Delay(
            500,
            TestContext.Current.CancellationToken);

        Assert.Null(isActive);
        Assert.Null(translatable);

        this.extendedCommandSync.Exit();
        for (var i = 0; i < 20 && isActive is null; i++)
        {
            await Task.Delay(100);
        }

        Assert.False(isActive);
        Assert.Null(translatable);
    }

    [Fact]
    public void IsActive()
    {
        var activated = 0;
        var deactivated = 0;

        this.extendedCommandSync.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(this.extendedCommandSync.IsActive))
            {
                if (this.extendedCommandSync.IsActive)
                {
                    activated++;
                }
                else
                {
                    deactivated++;
                }
            }
        };

        Assert.True(this.extendedCommandSync.Enter());
        Assert.Equal(
            1,
            activated);

        Assert.False(this.extendedCommandSync.Enter());
        Assert.Equal(
            1,
            activated);

        this.extendedCommandSync.Exit();

        Assert.Equal(
            1,
            activated);
        Assert.Equal(
            1,
            deactivated);
    }
}
