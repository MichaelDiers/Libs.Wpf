namespace Libs.Wpf.Threads;

using System.Windows;
using System.Windows.Threading;

/// <summary>
///     A wrapper for the current dispatcher of the application.
/// </summary>
public class DispatcherWrapper : IDispatcherWrapper
{
    /// <summary>
    ///     Gets the current dispatcher.
    /// </summary>
    public Dispatcher Dispatcher { get; } = Application.Current.Dispatcher;
}
