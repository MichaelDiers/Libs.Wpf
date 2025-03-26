namespace Libs.Wpf.Commands;

using Libs.Wpf.Localization;

/// <summary>
///     Extends <see cref="CommandSync" /> by an additional <see cref="ExtendedCommandSyncChanged" /> event.
/// </summary>
/// <seealso cref="CommandSync" />
internal class ExtendedCommandSync : CommandSync, IExtendedCommandSync
{
    /// <summary>
    ///     Requests the permission to run a command.
    /// </summary>
    /// <param name="translatableCancellableButton">
    ///     The data of the <see cref="ExtendedCommandSyncChanged" /> event if the
    ///     method succeeds.
    /// </param>
    /// <param name="force">
    ///     If <c>false</c> permission is granted if no other command is running; <c>false</c> grants
    ///     permission without that restriction.
    /// </param>
    /// <returns><c>True</c> if execution permission is granted.</returns>
    public bool Enter(TranslatableCancellableButton translatableCancellableButton, bool force = false)
    {
        if (!base.Enter(force))
        {
            return false;
        }

        this.ExtendedCommandSyncChanged?.Invoke(
            this,
            new ExtendedCommandSyncChangedEventArgs(
                this.IsActive,
                translatableCancellableButton));

        return true;
    }

    /// <summary>
    ///     Requests the permission to run a command.
    /// </summary>
    /// <param name="force">
    ///     If <c>false</c> permission is granted if no other command is running; <c>false</c> grants
    ///     permission without that restriction.
    /// </param>
    /// <returns><c>True</c> if execution permission is granted.</returns>
    public override bool Enter(bool force = false)
    {
        var result = base.Enter(force);
        if (!result)
        {
            return false;
        }

        this.ExtendedCommandSyncChanged?.Invoke(
            this,
            new ExtendedCommandSyncChangedEventArgs(
                true,
                null));
        return true;
    }

    /// <summary>
    ///     Indicates that a command has terminated.
    /// </summary>
    public override void Exit()
    {
        base.Exit();

        if (this.IsActive)
        {
            return;
        }

        this.ExtendedCommandSyncChanged?.Invoke(
            this,
            new ExtendedCommandSyncChangedEventArgs(
                false,
                null));
    }

    /// <summary>
    ///     An event raised if <see cref="CommandSync.IsActive" /> changed.
    /// </summary>
    public event EventHandler<ExtendedCommandSyncChangedEventArgs>? ExtendedCommandSyncChanged;
}
