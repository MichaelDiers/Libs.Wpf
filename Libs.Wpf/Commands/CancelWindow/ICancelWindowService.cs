namespace Libs.Wpf.Commands.CancelWindow;

/// <summary>
///     A factory for creating an <see cref="ICancelWindow" />.
/// </summary>
public interface ICancelWindowService
{
    /// <summary>
    ///     Creates a new <see cref="ICancelWindow" />.
    /// </summary>
    /// <param name="dataContext">The data context of the window.</param>
    /// <returns>A new <see cref="ICancelWindow" />.</returns>
    ICancelWindow CreateCancelWindow(object? dataContext);
}
