﻿namespace Libs.Wpf.TestApplication.Behaviors;

using Libs.Wpf.ViewModels;

internal class BehaviorsViewModel : ViewModelBase
{
    /// <summary>
    ///     The file content.
    /// </summary>
    private string fileContent = string.Empty;

    /// <summary>
    ///     The file path.
    /// </summary>
    private string filePath = string.Empty;

    /// <summary>
    ///     The folder path.
    /// </summary>
    private string folderPath = string.Empty;

    /// <summary>
    ///     Gets or sets the file content.
    /// </summary>
    public string FileContent
    {
        get => this.fileContent;
        set =>
            this.SetField(
                ref this.fileContent,
                value);
    }

    /// <summary>
    ///     Gets or sets the file path.
    /// </summary>
    public string FilePath
    {
        get => this.filePath;
        set =>
            this.SetField(
                ref this.filePath,
                value);
    }

    /// <summary>
    ///     Gets or sets the folder path.
    /// </summary>
    public string FolderPath
    {
        get => this.folderPath;
        set =>
            this.SetField(
                ref this.folderPath,
                value);
    }
}
