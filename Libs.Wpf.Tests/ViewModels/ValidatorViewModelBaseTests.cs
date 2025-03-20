namespace Libs.Wpf.Tests.ViewModels;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using Libs.Wpf.ViewModels;

/// <summary>
///     Tests for <see cref="ValidatorViewModelBase" />.
/// </summary>
public class ValidatorViewModelBaseTests : ValidatorViewModelBase
{
    private const string ErrorValueMultiple = nameof(ValidatorViewModelBaseTests.ErrorValueMultiple);

    private const string ErrorValueSingle = nameof(ValidatorViewModelBaseTests.ErrorValueSingle);
    private const string NoErrorValue = nameof(ValidatorViewModelBaseTests.NoErrorValue);

    /// <summary>
    ///     The another error handling property.
    /// </summary>
    private string anotherErrorProperty = string.Empty;

    /// <summary>
    ///     The value of the error handling property.
    /// </summary>
    private string errorProperty = string.Empty;

    /// <summary>
    ///     The error free property.
    /// </summary>
    private string noErrorProperty = string.Empty;

    /// <summary>
    ///     Gets or sets the another error handling property.
    /// </summary>
    public string AnotherErrorProperty
    {
        get => this.anotherErrorProperty;
        set
        {
            this.SetField(
                ref this.anotherErrorProperty,
                value);
            this.ValidateErrorProperty(value);
        }
    }

    /// <summary>
    ///     Gets or sets the error handling property.
    /// </summary>
    public string ErrorProperty
    {
        get => this.errorProperty;
        set
        {
            this.SetField(
                ref this.errorProperty,
                value);
            this.ValidateErrorProperty(value);
        }
    }

    /// <summary>
    ///     Gets or sets the error free property.
    /// </summary>
    public string NoErrorProperty
    {
        get => this.noErrorProperty;
        set =>
            this.SetField(
                ref this.noErrorProperty,
                value);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(nameof(ValidatorViewModelBaseTests.ErrorProperty))]
    public void GetErrors_ShouldReturnMultipleValidationErrors_WhenErrorsAreSet(string? propertyName)
    {
        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueMultiple;

        this.GetErrors(
            propertyName,
            [ValidatorViewModelBaseTests.ErrorValueSingle, ValidatorViewModelBaseTests.ErrorValueMultiple]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(nameof(ValidatorViewModelBaseTests.ErrorProperty))]
    public void GetErrors_ShouldReturnNoValidationErrors_WhenNoErrorIsSet(string? propertyName)
    {
        this.GetErrors(
            propertyName,
            []);
    }

    [Fact]
    public void GetErrors_ShouldReturnNoValidationErrors_WhenNoErrorPropertyIsRequested()
    {
        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueSingle;

        this.GetErrors(
            nameof(ValidatorViewModelBaseTests.NoErrorProperty),
            []);
    }

    [Fact]
    public void GetErrors_ShouldReturnSpecificMultipleValidationErrors_WhenMultipleValidationErrorsAreSet()
    {
        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueMultiple;
        this.AnotherErrorProperty = ValidatorViewModelBaseTests.ErrorValueSingle;

        this.GetErrors(
            nameof(ValidatorViewModelBaseTests.ErrorProperty),
            [ValidatorViewModelBaseTests.ErrorValueSingle, ValidatorViewModelBaseTests.ErrorValueMultiple]);
        this.GetErrors(
            nameof(ValidatorViewModelBaseTests.AnotherErrorProperty),
            [ValidatorViewModelBaseTests.ErrorValueSingle]);
        this.GetErrors(
            nameof(ValidatorViewModelBaseTests.NoErrorProperty),
            []);
        this.GetErrors(
            null,
            [
                ValidatorViewModelBaseTests.ErrorValueSingle, ValidatorViewModelBaseTests.ErrorValueMultiple,
                ValidatorViewModelBaseTests.ErrorValueSingle
            ]);
        this.GetErrors(
            string.Empty,
            [
                ValidatorViewModelBaseTests.ErrorValueSingle, ValidatorViewModelBaseTests.ErrorValueMultiple,
                ValidatorViewModelBaseTests.ErrorValueSingle
            ]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(nameof(ValidatorViewModelBaseTests.ErrorProperty))]
    public void GetErrors_ShouldReturnValidationErrors_WhenErrorIsSet(string? propertyName)
    {
        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueSingle;

        this.GetErrors(
            propertyName,
            [ValidatorViewModelBaseTests.ErrorValueSingle]);
    }

    [Fact]
    public void ResetErrors_ShouldNotRaiseErrorsChanged_WhenNoErrorIsSet()
    {
        this.ErrorsChanged += OnErrorsChangedFail;

        this.ErrorProperty = ValidatorViewModelBaseTests.NoErrorValue;

        this.ErrorsChanged -= OnErrorsChangedFail;
        return;

        void OnErrorsChangedFail(object? sender, DataErrorsChangedEventArgs e)
        {
            Assert.Fail("should not be called.");
        }
    }

    [Fact]
    public void ResetErrors_ShouldRaiseErrorsChanged_WhenErrorIsSet()
    {
        var called = false;

        this.ErrorsChanged += OnErrorsChangedCalled;

        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueSingle;

        this.ErrorsChanged -= OnErrorsChangedCalled;

        Assert.True(called);
        return;

        void OnErrorsChangedCalled(object? sender, DataErrorsChangedEventArgs e)
        {
            called = true;
        }
    }

    [Fact]
    public void ResetErrors_ShouldRemoveValidationError()
    {
        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueSingle;

        Assert.True(this.HasErrors);

        this.ErrorProperty = ValidatorViewModelBaseTests.NoErrorValue;

        Assert.False(this.HasErrors);
    }

    [Fact]
    public void SetError_RaisesArgumentException_WhenErrorIsNotSet()
    {
        Assert.Throws<ArgumentException>(
            () => this.SetError(
                string.Empty,
                nameof(ValidatorViewModelBaseTests.ErrorProperty)));
    }

    [Fact]
    public void SetError_RaisesErrorsChanged()
    {
        var raised = false;

        this.ErrorsChanged += OnErrorsChangedRaised;

        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueSingle;

        this.ErrorsChanged -= OnErrorsChangedRaised;

        Assert.True(raised);
        return;

        void OnErrorsChangedRaised(object? sender, DataErrorsChangedEventArgs e)
        {
            raised = true;
            Assert.Equal(
                nameof(ValidatorViewModelBaseTests.ErrorProperty),
                e.PropertyName);
        }
    }

    [Theory]
    [InlineData(
        null,
        typeof(ArgumentNullException))]
    [InlineData(
        "",
        typeof(ArgumentException))]
    public void SetError_RaisesException_WhenPropertyNameIsNotSet(string? propertyName, Type expectedException)
    {
        Assert.Throws(
            expectedException,
            () => this.SetError(
                ValidatorViewModelBaseTests.ErrorValueSingle,
                propertyName!));
    }

    [Fact]
    public void SetError_Succeeds()
    {
        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueSingle;

        this.GetErrors(
            nameof(ValidatorViewModelBaseTests.ErrorProperty),
            [ValidatorViewModelBaseTests.ErrorValueSingle]);
    }

    [Fact]
    public void SetErrors_RaisesArgumentException_WhenErrorsAreNotSet()
    {
        Assert.Throws<ArgumentException>(
            () => this.SetErrors(
                [],
                nameof(ValidatorViewModelBaseTests.ErrorProperty)));
    }

    [Fact]
    public void SetErrors_RaisesErrorsChanged()
    {
        var raised = false;

        this.ErrorsChanged += OnErrorsChangedRaised;

        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueMultiple;

        this.ErrorsChanged -= OnErrorsChangedRaised;

        Assert.True(raised);
        return;

        void OnErrorsChangedRaised(object? sender, DataErrorsChangedEventArgs e)
        {
            raised = true;
            Assert.Equal(
                nameof(ValidatorViewModelBaseTests.ErrorProperty),
                e.PropertyName);
        }
    }

    [Theory]
    [InlineData(
        null,
        typeof(ArgumentNullException))]
    [InlineData(
        "",
        typeof(ArgumentException))]
    public void SetErrors_RaisesException_WhenPropertyNameIsNotSet(string? propertyName, Type expectedException)
    {
        Assert.Throws(
            expectedException,
            () => this.SetErrors(
                [ValidatorViewModelBaseTests.ErrorValueSingle],
                propertyName!));
    }

    [Fact]
    public void SetErrors_Succeeds()
    {
        this.ErrorProperty = ValidatorViewModelBaseTests.ErrorValueMultiple;

        this.GetErrors(
            nameof(ValidatorViewModelBaseTests.ErrorProperty),
            [ValidatorViewModelBaseTests.ErrorValueSingle, ValidatorViewModelBaseTests.ErrorValueMultiple]);
    }

    private void GetErrors(string? propertyName, string[] expectedErrors)
    {
        var actualErrors = this.GetErrors(propertyName).Cast<string>().ToList();

        Assert.Equal(
            expectedErrors,
            actualErrors);
    }

    private void ValidateErrorProperty(string value, [CallerMemberName] string propertyName = "")
    {
        switch (value)
        {
            case ValidatorViewModelBaseTests.ErrorValueSingle:
                this.SetError(
                    ValidatorViewModelBaseTests.ErrorValueSingle,
                    propertyName);
                break;
            case ValidatorViewModelBaseTests.ErrorValueMultiple:
                this.SetErrors(
                    [ValidatorViewModelBaseTests.ErrorValueSingle, ValidatorViewModelBaseTests.ErrorValueMultiple],
                    propertyName);
                break;
            default:
                this.ResetErrors(propertyName);
                break;
        }
    }
}
