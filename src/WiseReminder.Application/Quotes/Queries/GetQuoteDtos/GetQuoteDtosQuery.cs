namespace WiseReminder.Application.Quotes.Queries.GetQuoteDtos;

public sealed record GetQuoteDtosQuery : IQuery<ICollection<QuoteDto>>
{
    public required Guid? CategoryId { get; init; }
    public required Guid? AuthorId { get; init; }
    public required ICollection<string>? Keywords { get; init; }
    public required string? DesiredLanguage { get; init; }
}