﻿namespace Libs.Wpf.TestApplication.Controls;

using System.Windows.Input;
using Libs.Wpf.Commands;
using Libs.Wpf.Controls.CustomMessageBox;
using Libs.Wpf.DependencyInjection;
using Libs.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;

internal class ControlsViewModel : ViewModelBase
{
    /// <summary>
    ///     The message box result.
    /// </summary>
    private string messageBoxResult;

    /// <summary>
    ///     The show message box command.
    /// </summary>
    private ICommand showMessageBoxCommand;

    /// <summary>
    ///     Initializes a new instance of the <see cref="ControlsViewModel" /> class.
    /// </summary>
    public ControlsViewModel()
    {
        var provider = CustomServiceProviderBuilder.Build(
            CustomMessageBoxServiceCollectionExtensions.TryAddCustomMessageBoxServiceCollectionExtensions,
            ServiceCollectionExtensions.TryAddCommandFactory);

        this.showMessageBoxCommand = provider.GetRequiredService<ICommandFactory>()
            .CreateSyncCommand(
                _ =>
                {
                    var messageBoxService = provider.GetRequiredService<IMessageBoxService>();
                    var result = messageBoxService.Show(
                        "The Message!",
                        "The Caption!",
                        MessageBoxButtons.Yes | MessageBoxButtons.No,
                        MessageBoxButtons.Yes,
                        MessageBoxImage.Question);
                    this.MessageBoxResult = result.ToString();
                });

        this.messageBoxResult = string.Empty;
    }

    /// <summary>
    ///     Gets or sets the message box result.
    /// </summary>
    public string MessageBoxResult
    {
        get => this.messageBoxResult;
        set =>
            this.SetField(
                ref this.messageBoxResult,
                value);
    }

    /// <summary>
    ///     Gets or sets the show message box command.
    /// </summary>
    public ICommand ShowMessageBoxCommand
    {
        get => this.showMessageBoxCommand;
        set =>
            this.SetField(
                ref this.showMessageBoxCommand,
                value);
    }
}
