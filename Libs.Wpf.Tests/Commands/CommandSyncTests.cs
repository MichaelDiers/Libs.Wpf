namespace Libs.Wpf.Tests.Commands;

using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

public class CommandSyncTests
{
    private readonly ICommandSync commandSync;

    public CommandSyncTests()
    {
        var provider = CustomServiceProviderBuilder.Build(CommandsServiceCollectionExtensions.TryAddCommands);
        this.commandSync = provider.GetRequiredService<ICommandSync>();
    }

    [Fact]
    public void Enter()
    {
        Assert.False(this.commandSync.IsActive);

        Assert.True(this.commandSync.Enter());
        Assert.True(this.commandSync.IsActive);

        Assert.False(this.commandSync.Enter());

        this.commandSync.Exit();
        Assert.False(this.commandSync.IsActive);

        Assert.True(this.commandSync.Enter(true));
        Assert.True(this.commandSync.IsActive);

        Assert.False(this.commandSync.Enter());
        Assert.True(this.commandSync.IsActive);

        Assert.True(this.commandSync.Enter(true));
        Assert.True(this.commandSync.IsActive);

        this.commandSync.Exit();
        Assert.True(this.commandSync.IsActive);

        this.commandSync.Exit();
        Assert.False(this.commandSync.IsActive);
    }

    [Fact]
    public void Exit()
    {
        Assert.False(this.commandSync.IsActive);

        this.commandSync.Exit();

        Assert.False(this.commandSync.IsActive);

        Assert.True(this.commandSync.Enter());
        Assert.True(this.commandSync.IsActive);

        this.commandSync.Exit();
        Assert.False(this.commandSync.IsActive);

        this.commandSync.Exit();
        Assert.False(this.commandSync.IsActive);
    }

    [Fact]
    public void IsActive()
    {
        var activated = 0;
        var deactivated = 0;

        this.commandSync.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName != nameof(this.commandSync.IsActive))
            {
                return;
            }

            if (this.commandSync.IsActive)
            {
                activated++;
            }
            else
            {
                deactivated++;
            }
        };

        Assert.True(this.commandSync.Enter());
        Assert.Equal(
            1,
            activated);

        Assert.False(this.commandSync.Enter());
        Assert.Equal(
            1,
            activated);

        this.commandSync.Exit();

        Assert.Equal(
            1,
            activated);
        Assert.Equal(
            1,
            deactivated);
    }
}
