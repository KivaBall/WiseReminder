namespace WiseReminder.IntegrationTests.Abstractions.Fixtures;

public sealed record UserIdsFixture
{
    public required Guid UserId { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid QuoteId { get; init; }
}