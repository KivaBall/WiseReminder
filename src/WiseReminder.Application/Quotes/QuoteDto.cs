namespace WiseReminder.Application.Quotes;

public sealed record QuoteDto
{
    public required Guid Id { get; init; }
    public required string OriginalText { get; init; }
    public required bool IsTranslated { get; init; }
    public required string? TranslatedText { get; init; }
    public required Guid AuthorId { get; init; }
    public required Guid CategoryId { get; init; }
    public required DateOnly QuoteDate { get; init; }
    public required ReactionStat Reactions { get; init; }
}