namespace Libs.Wpf.Tests.Configuration;

using Libs.Wpf.Configuration;

/// <summary>
///     Tests of <see cref="CustomConfigurationBuilder" />.
/// </summary>
public class CustomConfigurationBuilderTests
{
    [Fact]
    public void GetConfiguration_Succeeds()
    {
        var configuration = CustomConfigurationBuilder.GetConfiguration<CustomConfig>();

        Assert.Equal(
            "Bar",
            configuration.Foo);
    }

    [Fact]
    public void GetConfiguration_ThrowsDirectoryNotFoundException_WhenAppSettingsDirectoryDoesNotExist()
    {
        Assert.Throws<DirectoryNotFoundException>(
            () => CustomConfigurationBuilder.GetConfiguration<CustomConfig>(Guid.NewGuid().ToString()));
    }

    [Theory]
    [InlineData(
        null,
        typeof(ArgumentNullException))]
    [InlineData(
        "",
        typeof(ArgumentException))]
    [InlineData(
        "  ",
        typeof(ArgumentException))]
    public void GetConfiguration_ThrowsException_WhenAppSettingsFileNameIsNotSet(
        string appSettingsFileName,
        Type expectedExceptionType
    )
    {
        Assert.Throws(
            expectedExceptionType,
            () => CustomConfigurationBuilder.GetConfiguration<CustomConfig>(
                null,
                appSettingsFileName));
    }

    [Fact]
    public void GetConfiguration_ThrowsFileNotFoundExceptionException_WhenAppSettingsFileDoesNotExist()
    {
        Assert.Throws<FileNotFoundException>(
            () => CustomConfigurationBuilder.GetConfiguration<CustomConfig>(
                null,
                Guid.NewGuid().ToString()));
    }

    [Fact]
    public void GetConfiguration_ThrowsInvalidOperationException_WhenConfigurationDoNotMatch()
    {
        Assert.Throws<InvalidOperationException>(
            () => CustomConfigurationBuilder.GetConfiguration<AnotherCustomConfig>());
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private class CustomConfig(string foo)
    {
        public string Foo => foo;
    }

    // ReSharper disable once ClassNeverInstantiated.Local
    private class AnotherCustomConfig(string fooBar)
    {
        // ReSharper disable once UnusedMember.Local
        public string FooBar => fooBar;
    }
}
