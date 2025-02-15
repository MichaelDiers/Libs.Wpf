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
        this.TestProperty = "foo";

        this.PropertyChanged += HandlePropertyChanged;

        this.TestProperty = "foo";

        this.PropertyChanged -= HandlePropertyChanged;
        return;

        void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            Assert.Fail("should not occur");
        }
    }

    [Fact]
    public void PropertyChanged_RaisedIfPropertyChanged()
    {
        var propertyChangedRaised = false;

        this.PropertyChanged += HandlePropertyChanged;

        this.TestProperty = "foo";

        this.PropertyChanged -= HandlePropertyChanged;

        Assert.True(propertyChangedRaised);
        return;

        void HandlePropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            propertyChangedRaised = true;
            Assert.Equal(
                nameof(ViewModelBaseTests.TestProperty),
                e.PropertyName);
        }
    }
}
