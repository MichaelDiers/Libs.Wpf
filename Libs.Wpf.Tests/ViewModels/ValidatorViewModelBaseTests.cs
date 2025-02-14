namespace Libs.Wpf.Tests.ViewModels;

using System.ComponentModel;
using Libs.Wpf.ViewModels;

/// <summary>
///     Tests for <see cref="ValidatorViewModelBase" />.
/// </summary>
public class ValidatorViewModelBaseTests : ValidatorViewModelBase
{
    private const string ErrorProperty = nameof(ValidatorViewModelBaseTests.ErrorProperty);
    private const string ErrorValue = nameof(ValidatorViewModelBaseTests.ErrorValue);

    [Fact]
    public void ErrorsChangedTest()
    {
        var called = false;

        void HandleErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            Assert.Equal(
                this,
                sender);
            Assert.Equal(
                e.PropertyName,
                ValidatorViewModelBaseTests.ErrorProperty);
            called = true;
        }

        this.ErrorsChanged += HandleErrorsChanged;

        this.SetError(
            ValidatorViewModelBaseTests.ErrorValue,
            ValidatorViewModelBaseTests.ErrorProperty);

        Assert.True(called);

        called = false;

        this.SetErrors(
            [ValidatorViewModelBaseTests.ErrorValue],
            ValidatorViewModelBaseTests.ErrorProperty);

        Assert.True(called);

        this.ErrorsChanged -= HandleErrorsChanged;
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(ValidatorViewModelBaseTests.ErrorProperty)]
    public void GetErrorsTest(string? propertyName)
    {
        this.GetErrors(
            propertyName,
            []);

        this.SetError(
            ValidatorViewModelBaseTests.ErrorValue,
            ValidatorViewModelBaseTests.ErrorProperty);
        this.GetErrors(
            propertyName,
            [ValidatorViewModelBaseTests.ErrorValue]);

        this.SetErrors(
            [ValidatorViewModelBaseTests.ErrorValue, ValidatorViewModelBaseTests.ErrorValue],
            ValidatorViewModelBaseTests.ErrorProperty);
        this.GetErrors(
            propertyName,
            [ValidatorViewModelBaseTests.ErrorValue, ValidatorViewModelBaseTests.ErrorValue]);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void GetMultipleErrorsTest(string? propertyName)
    {
        var errors = new List<string>();
        for (var i = 1; i < 10; ++i)
        {
            var propErrors = Enumerable.Range(
                    0,
                    i)
                .Select(j => $"error_{j}")
                .ToArray();
            this.SetErrors(
                propErrors,
                $"property_{i}");
            errors.AddRange(propErrors);
        }

        this.GetErrors(
            propertyName,
            errors.ToArray());
    }

    [Fact]
    public void HasErrorsTest()
    {
        Assert.False(this.HasErrors);

        this.SetError(
            ValidatorViewModelBaseTests.ErrorValue,
            ValidatorViewModelBaseTests.ErrorProperty);

        Assert.True(this.HasErrors);

        this.ResetErrors(ValidatorViewModelBaseTests.ErrorProperty);

        Assert.False(this.HasErrors);

        this.SetErrors(
            [ValidatorViewModelBaseTests.ErrorValue],
            ValidatorViewModelBaseTests.ErrorProperty);

        Assert.True(this.HasErrors);

        this.ResetErrors(ValidatorViewModelBaseTests.ErrorProperty);

        Assert.False(this.HasErrors);
    }

    [Fact]
    public void ResetErrorsTest()
    {
        Assert.False(this.HasErrors);

        this.ResetErrors(ValidatorViewModelBaseTests.ErrorProperty);
        Assert.False(this.HasErrors);

        this.SetError(
            ValidatorViewModelBaseTests.ErrorValue,
            ValidatorViewModelBaseTests.ErrorProperty);
        Assert.True(this.HasErrors);

        this.ResetErrors($"{ValidatorViewModelBaseTests.ErrorProperty}x");
        Assert.True(this.HasErrors);

        this.ResetErrors($"{ValidatorViewModelBaseTests.ErrorProperty}");
        Assert.False(this.HasErrors);
    }

    [Fact]
    public void SetErrorsTest()
    {
        Assert.False(this.HasErrors);
        for (var i = 0; i < 10; ++i)
        {
            this.SetErrors(
                Enumerable.Range(
                        0,
                        i + 1)
                    .Select(j => $"{ValidatorViewModelBaseTests.ErrorValue}_{i}_{j}")
                    .ToArray(),
                $"{ValidatorViewModelBaseTests.ErrorProperty}_{i}");
        }

        for (var i = 0; i < 10; ++i)
        {
            this.GetErrors(
                $"{ValidatorViewModelBaseTests.ErrorProperty}_{i}",
                Enumerable.Range(
                        0,
                        i + 1)
                    .Select(j => $"{ValidatorViewModelBaseTests.ErrorValue}_{i}_{j}")
                    .ToArray());
        }
    }

    [Fact]
    public void SetErrorTest()
    {
        Assert.False(this.HasErrors);
        for (var i = 0; i < 10; ++i)
        {
            this.SetError(
                $"{ValidatorViewModelBaseTests.ErrorValue}{i}",
                $"{ValidatorViewModelBaseTests.ErrorProperty}{i}");
        }

        for (var i = 0; i < 10; ++i)
        {
            this.GetErrors(
                $"{ValidatorViewModelBaseTests.ErrorProperty}{i}",
                [$"{ValidatorViewModelBaseTests.ErrorValue}{i}"]);
        }
    }

    private void GetErrors(string? propertyName, string[] expectedErrors)
    {
        var actualErrors = new List<string>();
        foreach (string error in this.GetErrors(propertyName))
        {
            actualErrors.Add(error);
        }

        Assert.Equal(
            expectedErrors,
            actualErrors);
    }
}
