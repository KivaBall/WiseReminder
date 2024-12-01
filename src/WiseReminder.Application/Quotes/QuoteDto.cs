namespace WiseReminder.Application.Quotes;

public sealed record QuoteDto
{
    public required Guid Id { get; init; }
    public required string Text { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
}