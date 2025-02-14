namespace Libs.Wpf.Tests.ViewModels;

using System.ComponentModel;
using Libs.Wpf.ViewModels;

/// <summary>
///     Tests of <see cref="ViewModelBase" />.
/// </summary>
public class ViewModelBaseTests : ViewModelBase
{
    /// <summary>
    ///     The test property.
    /// </summary>
    private string testProperty = string.Empty;

    /// <summary>
    ///     Gets or sets the testProperty.
    /// </summary>
    public string TestProperty
    {
        get => this.testProperty;
        set =>
            this.SetField(
                ref this.testProperty,
                value);
    }

    [Fact]
    public void PropertyChanged_NotRaisedIfPropertyNotChanged()
    {
        void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Assert.Fail("should not occur");
        }

        this.TestProperty = "foo";

        this.PropertyChanged += HandlePropertyChanged;

        this.TestProperty = "foo";

        this.PropertyChanged -= HandlePropertyChanged;
    }

    [Fact]
    public void PropertyChanged_RaisedIfPropertyChanged()
    {
        var propertyChangedRaised = false;

        void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            propertyChangedRaised = true;
            Assert.Equal(
                nameof(ViewModelBaseTests.TestProperty),
                e.PropertyName);
        }

        this.PropertyChanged += HandlePropertyChanged;

        this.TestProperty = "foo";

        this.PropertyChanged -= HandlePropertyChanged;

        Assert.True(propertyChangedRaised);
    }
}
