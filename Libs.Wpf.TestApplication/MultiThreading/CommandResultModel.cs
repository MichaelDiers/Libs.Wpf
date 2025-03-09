namespace Libs.Wpf.TestApplication.MultiThreading;

internal class CommandResultModel
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="CommandResultModel" /> class.
    /// </summary>
    public CommandResultModel(string text)
    {
        this.Text = text;
        this.Items = text.Select(
            c => new CommandResultItemModel(
                new string(
                    c,
                    3)));
    }

    /// <summary>
    ///     Gets or sets the items.
    /// </summary>
    public IEnumerable<CommandResultItemModel> Items { get; }

    /// <summary>
    ///     Gets or sets the text.
    /// </summary>
    public string Text { get; }
}
