namespace Libs.Wpf.TestApplication.MultiThreading;

using System.Collections.ObjectModel;
using Libs.Wpf.ViewModels;

internal class CommandResult : ViewModelBase
{
    /// <summary>
    ///     The items.
    /// </summary>
    private ObservableCollection<CommandResultItem>? items;

    /// <summary>
    ///     The text.
    /// </summary>
    private TranslatableAndValidable<string>? text;

    public CommandResult(CommandResultModel result)
    {
        this.Text = new TranslatableAndValidable<string>(
            result.Text,
            null,
            false,
            Translation.ResourceManager,
            nameof(Translation.CommandResult));
        this.Items =
            new ObservableCollection<CommandResultItem>(result.Items.Select(item => new CommandResultItem(item.Text)));
    }

    /// <summary>
    ///     Gets or sets the items.
    /// </summary>
    public ObservableCollection<CommandResultItem>? Items
    {
        get => this.items;
        set =>
            this.SetField(
                ref this.items,
                value);
    }

    /// <summary>
    ///     Gets or sets the text.
    /// </summary>
    public TranslatableAndValidable<string>? Text
    {
        get => this.text;
        set =>
            this.SetField(
                ref this.text,
                value);
    }
}
