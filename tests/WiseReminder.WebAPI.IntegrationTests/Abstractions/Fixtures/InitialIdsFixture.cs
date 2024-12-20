namespace WiseReminder.IntegrationTests.Abstractions.Fixtures;

public sealed record InitialIdsFixture
{
    public Guid CategoryId { get; init; }
    public Guid AuthorId { get; init; }
    public Guid QuoteId { get; init; }
}