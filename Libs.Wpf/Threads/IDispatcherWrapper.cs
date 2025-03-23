namespace Libs.Wpf.Threads;

using System.Windows.Threading;

/// <summary>
///     A wrapper for the current dispatcher of the application.
/// </summary>
public interface IDispatcherWrapper
{
    /// <summary>
    ///     Gets the current dispatcher.
    /// </summary>
    Dispatcher Dispatcher { get; }
}
