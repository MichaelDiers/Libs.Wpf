namespace Libs.Wpf.Commands.CancelWindow;

/// <summary>
///     A factory for creating an <see cref="ICancelWindow" />.
/// </summary>
internal class CancelWindowService : ICancelWindowService
{
    /// <summary>
    ///     Creates a new <see cref="ICancelWindow" />.
    /// </summary>
    /// <param name="dataContext">The data context of the window.</param>
    /// <returns>A new <see cref="ICancelWindow" />.</returns>
    public ICancelWindow CreateCancelWindow(object? dataContext)
    {
        return new CustomCancelWindow {DataContext = dataContext};
    }
}
