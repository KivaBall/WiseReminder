namespace WiseReminder.IntegrationTests.Abstractions.Fixtures;

public sealed record AdminIdsFixture
{
    public required Guid CategoryId { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid QuoteId { get; init; }
    public required Guid UserId { get; init; }
}