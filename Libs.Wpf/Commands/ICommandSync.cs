namespace Libs.Wpf.Commands;

using System.ComponentModel;

/// <summary>
///     Synchronizes the command execution to ensure only one command at the same is executed.
/// </summary>
public interface ICommandSync : INotifyPropertyChanged
{
    /// <summary>
    ///     Gets a value that indicates weather the command is executing: <c>true</c> the command is running; otherwise
    ///     <c>false</c>.
    /// </summary>
    public bool IsActive { get; }

    /// <summary>
    ///     Requests the permission to run a command.
    /// </summary>
    /// <param name="force">
    ///     If <c>false</c> permission is granted if no other command is running; <c>false</c> grants
    ///     permission without that restriction.
    /// </param>
    /// <returns><c>True</c> if execution permission is granted.</returns>
    public bool Enter(bool force = false);

    /// <summary>
    ///     Indicates that a command has terminated.
    /// </summary>
    public void Exit();
}
