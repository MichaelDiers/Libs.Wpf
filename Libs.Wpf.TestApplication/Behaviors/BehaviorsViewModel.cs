namespace Libs.Wpf.TestApplication.Behaviors;

using Libs.Wpf.ViewModels;

internal class BehaviorsViewModel : ViewModelBase
{
    /// <summary>
    ///     The file content.
    /// </summary>
    private string fileContent = string.Empty;

    /// <summary>
    ///     The file content using a filter.
    /// </summary>
    private string fileContentFilter = string.Empty;

    /// <summary>
    ///     The file path.
    /// </summary>
    private string filePath = string.Empty;

    /// <summary>
    ///     The filtered file path.
    /// </summary>
    private string filePathFilter = string.Empty;

    /// <summary>
    ///     The file path filter text.
    /// </summary>
    private string filePathFilterText = ".pdf";

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
    ///     Gets or sets the file content using a filter.
    /// </summary>
    public string FileContentFilter
    {
        get => this.fileContentFilter;
        set =>
            this.SetField(
                ref this.fileContentFilter,
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
    ///     Gets or sets the filtered file path.
    /// </summary>
    public string FilePathFilter
    {
        get => this.filePathFilter;
        set =>
            this.SetField(
                ref this.filePathFilter,
                value);
    }

    /// <summary>
    ///     Gets or sets the file path filter text.
    /// </summary>
    public string FilePathFilterText
    {
        get => this.filePathFilterText;
        set =>
            this.SetField(
                ref this.filePathFilterText,
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
