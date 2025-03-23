namespace Libs.Wpf.TestApplication.Commands;

using System.Windows;
using Libs.Wpf.Commands;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.Localization;
using Libs.Wpf.Threads;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

public class CancelCommandViewModel : ViewModelBase
{
    /// <summary>
    ///     The translatable button.
    /// </summary>
    private TranslatableCancellableButton translatableButton;

    /// <summary>
    ///     Initializes a new instance of the <see cref="CancelCommandViewModel" /> class.
    /// </summary>
    public CancelCommandViewModel()
    {
        var commandFactory = CustomServiceProviderBuilder.Build(
                ServiceCollectionExtensions.TryAddCommandFactory,
                ThreadsServiceCollectionExtensions.TryAddDispatcherWrapper)
            .GetRequiredService<ICommandFactory>();

        this.translatableButton = new TranslatableCancellableButton(
            commandFactory.CreateAsyncCommand<object, bool>(
                null,
                null,
                async (_, cancellationToken) =>
                {
                    await Task.Delay(
                        10000,
                        cancellationToken);
                    return true;
                },
                task =>
                {
                    try
                    {
                        var result = task.Result;
                        MessageBox.Show(
                            "command executed",
                            string.Empty,
                            MessageBoxButton.OK,
                            result ? MessageBoxImage.Information : MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(
                            ex.Message,
                            string.Empty,
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                    }
                }),
            "pack://application:,,,/Libs.Wpf.TestApplication;component/Assets/material_symbol_edit_square.png",
            Translations.ResourceManager,
            nameof(Translations.Label),
            nameof(Translations.ToolTip),
            nameof(Translations.CancelLabel),
            nameof(Translations.CancelToolTip),
            "pack://application:,,,/Libs.Wpf.TestApplication;component/Assets/material_symbol_cancel.png");
    }

    /// <summary>
    ///     Gets or sets the translatable button.
    /// </summary>
    public TranslatableCancellableButton TranslatableButton
    {
        get => this.translatableButton;
        set =>
            this.SetField(
                ref this.translatableButton,
                value);
    }
}
