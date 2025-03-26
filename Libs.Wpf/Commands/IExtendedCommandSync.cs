namespace Libs.Wpf.Commands;

using Libs.Wpf.Localization;

/// <summary>
///     Extends <see cref="ICommandSync" /> by an additional <see cref="ExtendedCommandSyncChanged" /> event.
/// </summary>
/// <seealso cref="ICommandSync" />
public interface IExtendedCommandSync : ICommandSync
{
    /// <summary>
    ///     Requests the permission to run a command.
    /// </summary>
    /// <param name="translatableCancellableButton">
    ///     The data of the <see cref="ExtendedCommandSync.ExtendedCommandSyncChanged" /> event if the
    ///     method succeeds.
    /// </param>
    /// <param name="force">
    ///     If <c>false</c> permission is granted if no other command is running; <c>false</c> grants
    ///     permission without that restriction.
    /// </param>
    /// <returns><c>True</c> if execution permission is granted.</returns>
    bool Enter(TranslatableCancellableButton translatableCancellableButton, bool force = false);

    /// <summary>
    ///     An event raised if <see cref="CommandSync.IsActive" /> changed.
    /// </summary>
    event EventHandler<ExtendedCommandSyncChangedEventArgs>? ExtendedCommandSyncChanged;
}
