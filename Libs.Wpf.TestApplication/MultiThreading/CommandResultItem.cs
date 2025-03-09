namespace Libs.Wpf.TestApplication.MultiThreading;

using Libs.Wpf.ViewModels;

internal class CommandResultItem(string text) : ViewModelBase
{
    /// <summary>
    ///     The text.
    /// </summary>
    private string text = text;

    /// <summary>
    ///     Gets or sets the text.
    /// </summary>
    public string Text
    {
        get => this.text;
        set =>
            this.SetField(
                ref this.text,
                value);
    }
}
