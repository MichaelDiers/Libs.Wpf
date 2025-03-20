namespace Libs.Wpf.Tests.Controls.CustomMessageBox;

using Libs.Wpf.Controls.CustomMessageBox;

public class MessageBoxImageTests
{
    [Fact]
    public void CheckDuplicates()
    {
        Assert.Equal(
            MessageBoxImage.Asterisk,
            MessageBoxImage.Information);

        Assert.Equal(
            MessageBoxImage.Exclamation,
            MessageBoxImage.Warning);

        Assert.Equal(
            MessageBoxImage.Error,
            MessageBoxImage.Hand);
        Assert.Equal(
            MessageBoxImage.Error,
            MessageBoxImage.Stop);
    }

    [Theory]
    [InlineData(MessageBoxImage.None)]
    [InlineData(MessageBoxImage.Question)]
    public void CheckUniques(MessageBoxImage messageBoxImage)
    {
        var names = Enum.GetNames<MessageBoxImage>();
        Assert.Single(names.Where(name => Enum.Parse<MessageBoxImage>(name) == messageBoxImage));
    }
}
