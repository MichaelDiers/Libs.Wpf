namespace Libs.Wpf.Tests.Helper;

/// <summary>
///     The <see cref="ICollectionFixture{TFixture}" /> of <see cref="ApplicationFixture" /> is available in all tests
///     within the
///     same test collection with the name <see cref="CollectionName" />.
/// </summary>
[CollectionDefinition(ApplicationCollectionDefinition.CollectionName)]
public class ApplicationCollectionDefinition : ICollectionFixture<ApplicationFixture>
{
    public const string CollectionName = nameof(ApplicationCollectionDefinition.CollectionName);
}
