namespace Libs.Wpf.TestApplication.Commands;

using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Localization;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public class CancelCommandViewModel : ViewModelBase
{
    /// <summary>
    ///     The translatable button.
    /// </summary>
    private TranslatableButton<IAsyncCommand> translatableButton;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CancelCommandViewModel" /> class.
    /// </summary>
    public CancelCommandViewModel()
    {
        var provider = CustomServiceProviderBuilder.Build(
            CommandsServiceCollectionExtensions.TryAddCommands,
            CommandsServiceCollectionExtensions.TryAddCommandSync);
        var commandFactory = provider.GetRequiredService<ICommandFactory>();
        var commandSync = provider.GetRequiredService<ICommandSync>();

        this.translatableButton = new TranslatableButton<IAsyncCommand>(
            commandFactory.CreateAsyncCommand(
                commandSync,
                () => true,
                async cancellationToken =>
                {
                    await Task.Delay(1000);
                    await Task.Delay(
                        50000,
                        cancellationToken);
                },
                async (_, _) => { await Task.CompletedTask; },
                translatableCancelButton: new TranslatableCancelButton(
                    Translations.ResourceManager,
                    nameof(Translations.CancelLabel),
                    nameof(Translations.CancelToolTip),
                    "pack://application:,,,/Libs.Wpf.TestApplication;component/Assets/material_symbol_cancel.png",
                    nameof(Translations.CancelInfoText))),
            "pack://application:,,,/Libs.Wpf.TestApplication;component/Assets/material_symbol_edit_square.png",
            Translations.ResourceManager,
            nameof(Translations.Label),
            nameof(Translations.ToolTip));
    }

    /// <summary>
    ///     Gets or sets the translatable button.
    /// </summary>
    public TranslatableButton<IAsyncCommand> TranslatableButton
    {
        get => this.translatableButton;
        set =>
            this.SetField(
                ref this.translatableButton,
                value);
    }
}
