namespace Libs.Wpf.Tests.Controls.CustomMessageBox;

using Libs.Wpf.Controls.CustomMessageBox;

public class MessageBoxButtonsTests
{
    [Fact]
    public void NoneIsUndefined()
    {
        Assert.False(Enum.IsDefined((MessageBoxButtons) 0));
    }

    [Fact]
    public void UniqueTest()
    {
        Assert.NotEqual(
            MessageBoxButtons.Ok,
            MessageBoxButtons.Cancel);
        Assert.NotEqual(
            MessageBoxButtons.Cancel,
            MessageBoxButtons.Yes);
        Assert.NotEqual(
            MessageBoxButtons.Yes,
            MessageBoxButtons.No);
    }

    [Fact]
    public void ValuesShouldBeFlagValues()
    {
        var total = Enum.GetNames<MessageBoxButtons>().Length;

        for (var i = 0; i < total; i++)
        {
            Assert.True(
                Enum.IsDefined(
                    (MessageBoxButtons) Math.Pow(
                        2,
                        i)));
        }
    }
}
