namespace Libs.Wpf.Commands;

using Libs.Wpf.ViewModels;

/// <summary>
///     Synchronizes the command execution to ensure only one command at the same is executed.
/// </summary>
/// <seealso cref="ViewModelBase" />
/// <seealso cref="ICommandSync" />
internal class CommandSync : ViewModelBase, ICommandSync
{
    /// <summary>
    ///     A synchronization object for accessing <seealso cref="activeCommands" />.
    /// </summary>
    private static readonly Lock LockObject = new();

    /// <summary>
    ///     The number of active commands.
    /// </summary>
    private int activeCommands;

    /// <summary>
    ///     A value that indicates weather the command is executing: <c>true</c> the command is running; otherwise
    ///     <c>false</c>.
    /// </summary>
    private bool isActive;

    /// <summary>
    ///     Gets a value that indicates weather the command is executing: <c>true</c> the command is running; otherwise
    ///     <c>false</c>.
    /// </summary>
    public bool IsActive
    {
        get => this.isActive;
        private set =>
            this.SetField(
                ref this.isActive,
                value);
    }

    /// <summary>
    ///     Requests the permission to run a command.
    /// </summary>
    /// <param name="force">
    ///     If <c>false</c> permission is granted if no other command is running; <c>false</c> grants
    ///     permission without that restriction.
    /// </param>
    /// <returns><c>True</c> if execution permission is granted.</returns>
    public virtual bool Enter(bool force = false)
    {
        if (this.IsActive && !force)
        {
            return false;
        }

        lock (CommandSync.LockObject)
        {
            if (this.IsActive && !force)
            {
                return false;
            }

            this.IsActive = true;
            this.activeCommands++;
            return true;
        }
    }

    /// <summary>
    ///     Indicates that a command has terminated.
    /// </summary>
    public virtual void Exit()
    {
        lock (CommandSync.LockObject)
        {
            this.activeCommands = Math.Max(
                0,
                this.activeCommands - 1);
            this.IsActive = this.activeCommands != 0;
        }
    }
}
