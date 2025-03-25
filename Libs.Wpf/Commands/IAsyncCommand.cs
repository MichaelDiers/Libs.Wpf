namespace Libs.Wpf.Commands;

using System.ComponentModel;
using System.Windows.Input;

/// <summary>
///     An implementation of <see cref="ICommand" /> using async and await.
/// </summary>
/// <seealso cref="ICommand" />
/// <seealso cref="INotifyPropertyChanged" />
public interface IAsyncCommand : ICommand, INotifyPropertyChanged
{
    /// <summary>
    ///     Gets a value that indicates weather the command is executing: <c>true</c> the command is running; otherwise
    ///     <c>false</c>.
    /// </summary>
    bool IsActive { get; }
}
